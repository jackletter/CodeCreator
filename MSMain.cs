using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Novacode;
using DBUtil;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CodeCreator
{
    public partial class MSMain : Form
    {
        private TableStruct tblStruct = null;
        public MSMain()
        {
            InitializeComponent();
            this.FormClosed += Form4_FormClosed;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView2.DataError += dataGridView2_DataError;
            radTable.Checked = true;
            EnableAllBtn();
            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);
            dataGridView2.Visible = true;
            richTextBox1.Visible = false;
            CHKTable();
        }

        void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void CHKTable()
        {
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            string sql = @"
select 
	序号=ROW_NUMBER() OVER(order BY table_name),
	表名=table_name,
	说明=(SELECT value   
FROM sys.extended_properties ds  
LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id  
WHERE  ds.minor_id=0 and  
 tbs.name=table_name )
from INFORMATION_SCHEMA.TABLES t where t.TABLE_TYPE='BASE TABLE'
";
            DataSet ds = iDb.GetDataSet(sql);
            dataGridView1.DataSource = ds.Tables[0];
            int wid = dataGridView1.Width;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = Convert.ToInt32((wid - 150 - 60) * 0.4);
            dataGridView1.Columns[3].Width = Convert.ToInt32((wid - 150 - 60) * 0.6);
            if (ds.Tables[0].Rows.Count > 0)
            {

                tblStruct = iDb.GetTableStruct(ds.Tables[0].Rows[0]["表名"].ToString());
                InitTableInfo();
            }
        }

        void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitViewInfo(string viewName)
        {
            richTextBox1.Visible = true;
            dataGridView2.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
            picLoad.Show();
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                string viewSql = "";
                if (!string.IsNullOrEmpty(viewName))
                {
                    SqlServerIDbAccess iDb = IDBFactory.CreateIDB(DataTransfer.iDb.ConnectionString, "SQLSERVER") as SqlServerIDbAccess;
                    tblStruct = iDb.GetTableStruct(viewName);
                    viewSql = iDb.CreateViewSql(tblStruct.Name);
                    iDb.conn.Close();
                    iDb.conn.Dispose();
                    iDb = null;
                }

                this.Invoke(new MethodInvoker(() =>
                {
                    richTextBox1.Text = viewSql;
                    DisableAllRad();
                    radStruct.Enabled = true;
                    radStruct.Checked = false;
                    radData.Enabled = true;
                    radData.Checked = false;
                    picLoad.Hide();
                }));
            });
        }

        private void InitTableInfo(string tableName = "")
        {
            richTextBox1.Visible = false;
            dataGridView2.Visible = true;
            textBox1.Text = tblStruct.Name;
            label4.Text = tblStruct.Desc;
            picLoad.Show();
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                if (!string.IsNullOrEmpty(tableName))
                {
                    SqlServerIDbAccess iDb = IDBFactory.CreateIDB(DataTransfer.iDb.ConnectionString, "SQLSERVER") as SqlServerIDbAccess;
                    tblStruct = iDb.GetTableStruct(tableName);
                    iDb.conn.Close();
                    iDb.conn.Dispose();
                    iDb = null;
                }
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("序号"));
                dt.Columns.Add(new DataColumn("列名"));
                dt.Columns.Add(new DataColumn("类型"));
                dt.Columns.Add(new DataColumn("说明"));
                dt.Columns.Add(new DataColumn("是否可空"));
                dt.Columns.Add(new DataColumn("是否唯一"));
                dt.Columns.Add(new DataColumn("自增说明"));
                dt.Columns.Add(new DataColumn("默认值"));
                for (int i = 0; i < tblStruct.Columns.Count; i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = i + 1;
                    row[1] = tblStruct.Columns[i].Name;
                    row[2] = tblStruct.Columns[i].FinalType;

                    row[3] = tblStruct.Columns[i].Desc;
                    row[4] = tblStruct.Columns[i].IsNullable ? "是" : "否";
                    row[5] = tblStruct.Columns[i].IsUnique ? "是" : "否";
                    row[6] = tblStruct.Columns[i].FinalIdentity;
                    row[7] = tblStruct.Columns[i].Default;
                    dt.Rows.Add(row);
                }

                this.Invoke(new MethodInvoker(() =>
                {
                    textBox1.Text = tblStruct.Name;
                    dataGridView2.DataSource = dt.DefaultView;
                    textBox2.Text = tblStruct.PrimaryKey;
                    EnableAllRad();
                    if (radView.Checked)
                    {
                        radConstraint.Enabled = false;
                        radTritbl.Enabled = false;
                        radIndex.Enabled = false;
                    }
                    radStruct.Checked = true;
                    picLoad.Hide();
                }));
            });
        }

        private void DisableAllRad()
        {
            radStruct.Enabled = false;
            radData.Enabled = false;
            radConstraint.Enabled = false;
            radTritbl.Enabled = false;
            radIndex.Enabled = false;
        }

        private void EnableAllRad()
        {
            radStruct.Enabled = true;
            radData.Enabled = true;
            radConstraint.Enabled = true;
            radTritbl.Enabled = true;
            radIndex.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 添加连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConnAdd().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TmpSolutions");
            if (!Directory.Exists(path))
            {
                MessageBox.Show("TmpSolutions文件夹找不到!(" + path + ")");
                return;
            }
            openFileDialog1.InitialDirectory = path;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "生成方案文件|*.msln";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;
            textBox3.Text = fileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!PreData())
            {
                return;
            }
            Hashtable ht = (Hashtable)DataTransfer.data;
            string mslnPath = ht["MslnPath"].ToString();
            Hashtable htres = new Hashtable();
            MslnConf mslnConf = new MslnConf(mslnPath);
            foreach (var item in mslnConf.tmpItems)
            {
                htres.Add(item.OutName, DynamicCreator.Create(item.Path, item.ClassFullName, item.CreateMethod));
            }
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "请选择文件路径";
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK || folderBrowserDialog1.ShowDialog() == DialogResult.Yes)
            {
                string str = folderBrowserDialog1.SelectedPath;
                str = Path.Combine(str, ((Hashtable)DataTransfer.data)["TableName"].ToString());
                if (Directory.Exists(str))
                {
                    if (MessageBox.Show("发现已存在文件夹" + str + "是否覆盖已存在的文件？", "文件输出选择", MessageBoxButtons.OKCancel) != DialogResult.OK) { return; }
                }
                else
                {
                    Directory.CreateDirectory(str);
                }

                foreach (var key in htres.Keys)
                {
                    File.WriteAllText(Path.Combine(str, key.ToString()), htres[key].ToString());
                }
                MessageBox.Show("生成成功!");
                System.Diagnostics.Process.Start("Explorer.exe", "/select," + str);
            }
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <returns></returns>
        private bool PreData()
        {
            string MslnPath = textBox3.Text;
            Hashtable ht = new Hashtable();
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请选中要生成代码的表再操作!");
                return false;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("请选中要生成代码的方案文件!");
                return false;
            }
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("选中的表 " + textBox1.Text + " 无列可生成!");
                return false;
            }
            ht.Add("TableName", textBox1.Text);
            ht.Add("TableInstructions", label4.Text);
            List<string> colNames = new List<string>();
            List<string> colTypes = new List<string>();
            List<bool> colIsIdentities = new List<bool>();
            List<string> colInstructions = new List<string>();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                colNames.Add(dataGridView2.Rows[i].Cells[1].Value.ToString());
                colTypes.Add(dataGridView2.Rows[i].Cells[2].Value.ToString());
                colInstructions.Add(dataGridView2.Rows[i].Cells[3].Value.ToString());
                colIsIdentities.Add(dataGridView2.Rows[i].Cells[6].Value.ToString() == "是");
            }

            ht.Add("listColumnNames", colNames);
            ht.Add("listColumnTypes", colTypes);
            ht.Add("listColumnInstructions", colInstructions);
            ht.Add("listColumnIsIdentities", colIsIdentities);
            ht.Add("PrimaryKey", textBox2.Text);
            ht.Add("MslnPath", textBox3.Text);
            DataTransfer.data = ht;
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.OnLoad(null);
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //点击表头直接返回
            if (e.RowIndex == -1 || e.ColumnIndex == -1) { return; }

            //数据区域
            //如果是左键
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (e.ColumnIndex == 0)
                {
                    //如果是第一列就选中复选框
                    DataGridViewCheckBoxCell chk = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                    if (chk.Value == null || (chk.Value as bool?) == false)
                    {
                        chk.Value = true;
                    }
                    else
                    {
                        chk.Value = false;
                    }
                    return;

                }
                //刷新右侧
                int rowIndex = e.RowIndex;
                string objName = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                if (radTable.Checked)
                {
                    InitTableInfo(objName);
                }
                else if (radView.Checked)
                {
                    InitViewInfo(objName);
                }
                else if (radProc.Checked)
                {
                    InitProcInfo(objName);
                }
                else if (radFunc.Checked)
                {
                    InitFuncInfo(objName);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex > 1)  //点击的是鼠标右键，不是第一列和序号列
            {
                //右键选中单元格
                if (dataGridView2.SelectedCells.Count > 1)
                {
                    //多选情况下的右键菜单
                }
                else
                {
                    //单元情况下的右键菜单
                    tblEdit = e.ColumnIndex;//记录编辑的列索引
                    tblRowEdit = e.RowIndex;//记录编辑的行索引
                    tblOldval = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Selected = false;
                    }
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    if (radTable.Checked)
                    {
                        toolStripMenuItem1.Enabled = true;
                        删除表ToolStripMenuItem.Enabled = true;
                        新建表ToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        toolStripMenuItem1.Enabled = false;
                        删除表ToolStripMenuItem.Enabled = false;
                        新建表ToolStripMenuItem.Enabled = false;
                    }
                    this.tblContext.Show(MousePosition.X, MousePosition.Y); //MousePosition.X, MousePosition.Y 是为了让菜单在所选行的位置显示
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.BeginEdit(false);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除这条数据?", "删除确认框", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("删除成功!");
            }
        }

        int tblEdit = 0;
        int tblRowEdit = 0;
        string tblOldval = "";

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //是否可以进行编辑的条件检查 
            if (e.ColumnIndex == 0)
            {
                // 取消编辑 
                e.Cancel = true;
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string newVal = dataGridView1.CurrentCell.Value.ToString();
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            if (newVal != tblOldval)
            {
                if (System.Windows.Forms.DialogResult.OK == MessageBox.Show("是否更新到数据库?", "是否更新", MessageBoxButtons.OKCancel))
                {
                    if (tblEdit == 2)
                    {
                        try
                        {
                            iDb.RenameTable(tblOldval, newVal);
                            MessageBox.Show("更新成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("更新失败:" + ex.ToString());
                        }
                        dataGridView1.CurrentCell.ReadOnly = false;

                    }
                    else if (tblEdit == 3)
                    {
                        try
                        {
                            iDb.SaveTableDesc(dataGridView1.Rows[tblRowEdit].Cells[2].Value.ToString(), newVal);
                            MessageBox.Show("更新成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("更新失败:" + ex.ToString());
                        }
                        dataGridView1.CurrentCell.ReadOnly = false;
                    }
                }
                else
                {
                    dataGridView1.CurrentCell.Value = tblOldval;
                }
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!radStruct.Checked) { return; }
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex > -1 && e.RowIndex > -1)  //点击的是鼠标右键，并且不是表头
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 7)
                {
                    //右键选中单元格
                    colColIndex = e.ColumnIndex;//记录编辑的列索引
                    colRowIndex = e.RowIndex;//记录编辑的行索引
                    colOldval = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        dataGridView2.Rows[i].Selected = false;
                    }
                    this.dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    this.colContext.Show(MousePosition.X, MousePosition.Y); //MousePosition.X, MousePosition.Y 是为了让菜单在所选行的位置显示
                }
                else if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    //右键选中单元格
                    colColIndex = e.ColumnIndex;//记录编辑的列索引
                    colRowIndex = e.RowIndex;//记录编辑的行索引
                    this.alterContext.Show(MousePosition.X, MousePosition.Y); //MousePosition.X, MousePosition.Y 是为了让菜单在所选行的位置显示
                }
            }
        }

        int colColIndex = 0;//记录列框单元格列索引
        int colRowIndex = 0;//记录列框单元格行
        string colOldval = "";

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.dataGridView2.BeginEdit(false);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(dataGridView2.GetClipboardContent());
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell == null)
                return;
            int insertRowIndex = dataGridView2.CurrentCell.RowIndex;
            // 获取剪切板的内容，并按行分割 
            string pasteText = Clipboard.GetText();
            dataGridView2.CurrentCell.Value = pasteText;
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string newVal = dataGridView2.CurrentCell.Value.ToString();
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            if (newVal != colOldval)
            {
                if (System.Windows.Forms.DialogResult.OK == MessageBox.Show("是否更新到数据库?", "是否更新", MessageBoxButtons.OKCancel))
                {
                    if (colColIndex == 1)
                    {
                        try
                        {
                            iDb.RenameColumn(textBox1.Text, colOldval, dataGridView2.CurrentCell.Value.ToString());
                            MessageBox.Show("更新成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("更新失败:" + ex.ToString());
                        }
                        dataGridView2.CurrentCell.ReadOnly = false;
                    }
                    else if (colColIndex == 2)
                    {
                        try
                        {
                            iDb.AlterColumnType(textBox1.Text, dataGridView2.Rows[colRowIndex].Cells[1].Value.ToString(), dataGridView2.Rows[colRowIndex].Cells[2].Value.ToString(), false);
                            MessageBox.Show("更新成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("更新失败:" + ex.ToString());
                        }
                        dataGridView2.CurrentCell.ReadOnly = false;
                    }
                    else if (colColIndex == 3)
                    {
                        try
                        {
                            iDb.SaveColumnDesc(textBox1.Text, dataGridView2.Rows[colRowIndex].Cells[1].Value.ToString(), newVal);
                            MessageBox.Show("更新成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("更新失败:" + ex.ToString());
                        }
                        dataGridView2.CurrentCell.ReadOnly = false;
                    }
                    else if (colColIndex == 7)
                    {
                        try
                        {
                            newVal = newVal.Trim().Trim(new char[] { ' ', '(', ')' }).Trim();
                            iDb.SaveColumnDefault(textBox1.Text, dataGridView2.Rows[colRowIndex].Cells[1].Value.ToString(), newVal);
                            MessageBox.Show("更新成功!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("更新失败:" + ex.ToString());
                        }
                        dataGridView2.CurrentCell.ReadOnly = false;
                    }
                }
                else
                {
                    dataGridView2.CurrentCell.Value = colOldval;
                }
            }
        }

        private void 切换连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            btnDoc.Text = "执行中...";
            btnDoc.Enabled = false;
            List<TableStruct> res = null;
            try
            {
                res = await CreateTableList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                btnDoc.Text = "生成表结构说明";
                btnDoc.Enabled = true;
                return;
            }
            if (res.Count == 0)
            {
                MessageBox.Show("请选中要生成表结构的文档!");
                btnDoc.Text = "生成表结构说明";
                btnDoc.Enabled = true;
                return;
            }
            try
            {
                DocX doc = null;
                await Task.Factory.StartNew((Action)(() =>
               {
                   doc = DocX.Load("表结构说明模板.docx");

                   for (int i = 0; i < res.Count; i++)
                   {
                       Paragraph ptmp = doc.InsertParagraph(doc.Paragraphs[0]);
                       Novacode.Table tbltmp = doc.InsertTable(doc.Tables[0]);
                       SetBorder(tbltmp);
                       ptmp.ReplaceText("#TableName#", res[i].Name);
                       tbltmp.Rows[0].Cells[1].ReplaceText("#TableName#", res[i].Name);
                       tbltmp.Rows[1].Cells[1].ReplaceText("#TableDesc#", res[i].Desc ?? "");
                       tbltmp.Rows[2].Cells[1].ReplaceText("#PrimaryKey#", res[i].PrimaryKey);
                       for (int ii = 0; ii < res[i].Columns.Count; ii++)
                       {
                           Novacode.Row rowtmp = tbltmp.InsertRow(tbltmp.Rows[4]);
                           rowtmp.Cells[0].ReplaceText("#ColumnName#", res[i].Columns[ii].Name);
                           rowtmp.Cells[1].ReplaceText("#ColumnType#", res[i].Columns[ii].Type);
                           rowtmp.Cells[2].ReplaceText("#ColumnDesc#", res[i].Columns[ii].Desc ?? "");
                           string remark = "";
                           if (res[i].Columns[ii].IsIdentity)
                           {
                               remark += "自增;";
                           }
                           if (res[i].Columns[ii].IsNullable)
                           {
                               remark += "可空;";
                           }
                           if (!string.IsNullOrWhiteSpace(res[i].Columns[ii].Default))
                           {
                               remark += "默认(" + res[i].Columns[ii].Default + ");";
                           }
                           rowtmp.Cells[3].ReplaceText("#ColumnRemark#", remark);
                       }
                       tbltmp.RemoveRow(4);
                   }
                   doc.RemoveParagraph(doc.Paragraphs[0]);
                   doc.Tables[0].Remove();
               }));
                btnDoc.Text = "生成表结构说明";
                btnDoc.Enabled = true;
                folderBrowserDialog1.ShowNewFolderButton = true;
                folderBrowserDialog1.Description = "请选择文件路径";
                folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                System.Windows.Forms.DialogResult diares = folderBrowserDialog1.ShowDialog();
                if (diares == DialogResult.OK || diares == DialogResult.Yes)
                {
                    string str = folderBrowserDialog1.SelectedPath;
                    string filepath = Path.Combine(str, DateTime.Now.ToString("yyyy-MM-dd") + "结构说明.docx");
                    if (File.Exists(filepath))
                    {
                        if (MessageBox.Show("发现已存在文件" + filepath + "是否覆盖？", "文件输出选择", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        {
                            return;
                        }
                        else
                        {
                            File.Delete(filepath);
                        }
                    }
                    doc.SaveAs(filepath);
                    MessageBox.Show("生成成功!");
                    System.Diagnostics.Process.Start("Explorer.exe", "/select," + filepath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnDoc.Text = "生成表结构说明";
                btnDoc.Enabled = true;
            }
        }

        private async Task<List<TableStruct>> CreateTableList()
        {
            SqlServerIDbAccess iDb = IDBFactory.CreateIDB(DataTransfer.iDb.ConnectionString, "SQLSERVER") as SqlServerIDbAccess;
            List<string> tblNames = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((dataGridView1.Rows[i].Cells[0].Value as bool?) == true)
                {
                    tblNames.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                }
            }
            return await Task.Factory.StartNew<List<TableStruct>>(() => { return iDb.GetTableStructs(tblNames); });
        }

        private void SetBorder(Novacode.Table tbl)
        {
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                for (int ii = 0; ii < tbl.Rows[i].Cells.Count; ii++)
                {
                    tbl.Rows[i].Cells[ii].SetBorder(TableCellBorderType.Top, new Border(Novacode.BorderStyle.Tcbs_single, 0, 0, Color.Black));
                    tbl.Rows[i].Cells[ii].SetBorder(TableCellBorderType.Bottom, new Border(Novacode.BorderStyle.Tcbs_single, 0, 0, Color.Black));
                    tbl.Rows[i].Cells[ii].SetBorder(TableCellBorderType.Left, new Border(Novacode.BorderStyle.Tcbs_single, 0, 0, Color.Black));
                    tbl.Rows[i].Cells[ii].SetBorder(TableCellBorderType.Right, new Border(Novacode.BorderStyle.Tcbs_single, 0, 0, Color.Black));
                }
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            btnXML.Text = "执行中...";
            btnXML.Enabled = false;
            List<TableStruct> res = null;
            try
            {
                res = await CreateTableList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                btnXML.Text = "导出表结构XML";
                btnXML.Enabled = true;
                return;
            }
            if (res.Count == 0)
            {
                MessageBox.Show("请选中表!");
                btnXML.Text = "导出表结构XML";
                btnXML.Enabled = true;
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
            for (int i = 0; i < res.Count; i++)
            {
                sb.AppendLine("<" + res[i].Name + " prikey=\"" + res[i].PrimaryKey + "\" desc=\"" + res[i].Desc + "\">");
                for (int j = 0; j < res[i].Columns.Count; j++)
                {
                    sb.AppendLine(string.Format("    <{0} type=\"" + res[i].Columns[j].FinalType + "\"><![CDATA[{1}]]></{0}>", res[i].Columns[j].Name, res[i].Columns[j].Desc));
                }
                sb.AppendLine("</" + res[i].Name + ">");
            }
            btnXML.Text = "导出表结构XML";
            btnXML.Enabled = true;
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "请选择文件路径";
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            System.Windows.Forms.DialogResult diares = folderBrowserDialog1.ShowDialog();
            if (diares == DialogResult.OK || diares == DialogResult.Yes)
            {
                string str = folderBrowserDialog1.SelectedPath;
                string filepath = Path.Combine(str, DateTime.Now.ToString("yyyy-MM-dd") + "结构说明.xml");
                if (File.Exists(filepath))
                {
                    if (MessageBox.Show("发现已存在文件" + filepath + "是否覆盖？", "文件输出选择", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                    else
                    {
                        File.Delete(filepath);
                    }
                }
                System.IO.File.WriteAllText(filepath, sb.ToString());
                MessageBox.Show("生成成功!");
                System.Diagnostics.Process.Start("Explorer.exe", "/select," + filepath);
            }

        }

        private void 编辑连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnEdit edit = new ConnEdit();
            edit.ShowDialog();
        }

        private void 删除表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除表?", "确认框", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DBUtil.SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
                string tblname = dataGridView1.Rows[tblRowEdit].Cells[2].Value.ToString();
                iDb.DropTable(tblname);
                this.OnLoad(null);
            }
        }

        private void 新建表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTable table = new AddTable();
            table.ShowDialog();
        }

        private void 删除列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string colname = dataGridView2.Rows[colRowIndex].Cells[1].Value.ToString();
            if (MessageBox.Show("是否删除列?", "确认框", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DBUtil.SqlServerIDbAccess iDb = DataTransfer.iDb as DBUtil.SqlServerIDbAccess;
                try
                {
                    iDb.DropColumn(textBox1.Text, colname);
                    InitTableInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败:" + ex.ToString());
                }
            }
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            DBUtil.SqlServerIDbAccess iDb = DataTransfer.iDb as DBUtil.SqlServerIDbAccess;
            string colname = dataGridView2.Rows[colRowIndex].Cells[1].Value.ToString();
            string colType = dataGridView2.Rows[colRowIndex].Cells[2].Value.ToString();
            string tblname = textBox1.Text;
            try
            {
                if (colColIndex == 4)
                {
                    iDb.AlterColumnNullAble(tblname, colname, colType, true);
                }
                else if (colColIndex == 5)
                {
                    iDb.SaveColumnUnique(tblname, colname, true);
                }
                dataGridView2.Rows[colRowIndex].Cells[colColIndex].Value = "是";
                MessageBox.Show("切换成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("切换出错:" + ex.ToString());
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DBUtil.SqlServerIDbAccess iDb = DataTransfer.iDb as DBUtil.SqlServerIDbAccess;
            string colname = dataGridView2.Rows[colRowIndex].Cells[1].Value.ToString();
            string colType = dataGridView2.Rows[colRowIndex].Cells[2].Value.ToString();
            string tblname = textBox1.Text;
            try
            {
                if (colColIndex == 4)
                {
                    iDb.AlterColumnNullAble(tblname, colname, colType, false);
                }
                else if (colColIndex == 5)
                {
                    iDb.SaveColumnUnique(tblname, colname, false);
                }
                dataGridView2.Rows[colRowIndex].Cells[colColIndex].Value = "否";
                MessageBox.Show("切换成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("切换出错:" + ex.ToString());
            }
        }

        private void 添加列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCol col = new AddCol();
            DataTransfer.TableName = textBox1.Text;
            col.ShowDialog();
        }
        private void DisableAllBtn()
        {
            btnTMP.Enabled = false;
            btnCode.Enabled = false;
            btnDoc.Enabled = false;
            btnSQL.Enabled = false;
            btnXML.Enabled = false;
        }

        private void EnableAllBtn()
        {
            btnTMP.Enabled = true;
            btnCode.Enabled = true;
            btnDoc.Enabled = true;
            btnSQL.Enabled = true;
            btnXML.Enabled = true;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            CHKTable();
            EnableAllBtn();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            CHKView();
            EnableAllBtn();
            btnCode.Enabled = false;
            btnTMP.Enabled = false;
            btnDoc.Enabled = false;
            btnXML.Enabled = false;
        }

        private void CHKView()
        {
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            string sql = @"
select 
	序号=ROW_NUMBER() OVER(order BY table_name),
	视图名称=table_name,
	说明=(SELECT value   
FROM sys.extended_properties ds  
LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id  
WHERE  ds.minor_id=0 and  
 tbs.name=table_name )
from INFORMATION_SCHEMA.TABLES t where t.TABLE_TYPE='VIEW'
";

            DataSet ds = iDb.GetDataSet(sql);
            dataGridView1.DataSource = ds.Tables[0];
            int wid = dataGridView1.Width;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = Convert.ToInt32((wid - 150 - 60) * 0.4);
            dataGridView1.Columns[3].Width = Convert.ToInt32((wid - 150 - 60) * 0.6);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tblStruct = iDb.GetTableStruct(ds.Tables[0].Rows[0]["视图名称"].ToString());
                InitViewInfo(tblStruct.Name);
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            CHKProc();
            DisableAllBtn();
            btnSQL.Enabled = true;
        }
        private void CHKProc()
        {
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            List<Proc> procs = iDb.GetProcs();
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("名称");
            dt.Columns.Add("最近更新");
            for (int i = 0; i < procs.Count; i++)
            {
                DataRow row = dt.NewRow();
                row["序号"] = i + 1;
                row["名称"] = procs[i].Name;
                row["最近更新"] = procs[i].LastUpdate;
                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
            int wid = dataGridView1.Width;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = Convert.ToInt32((wid - 150 - 60) * 0.4);
            dataGridView1.Columns[3].Width = Convert.ToInt32((wid - 150 - 60) * 0.6);
            if (dt.Rows.Count > 0)
            {
                InitProcInfo(dt.Rows[0]["名称"].ToString());
            }
        }

        private void InitProcInfo(string procName)
        {
            richTextBox1.Location = dataGridView2.Location;
            richTextBox1.Size = dataGridView2.Size;
            dataGridView2.Visible = false;
            richTextBox1.Visible = true;
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            richTextBox1.Text = iDb.CreateProcSql(procName);
            DisableAllRad();
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            CHKFunc();
            DisableAllBtn();
            btnSQL.Enabled = true;
        }

        private void CHKFunc()
        {
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            List<Func> funcs = iDb.GetFuncs();
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("名称");
            dt.Columns.Add("类型");
            dt.Columns.Add("最近更新");
            for (int i = 0; i < funcs.Count; i++)
            {
                DataRow row = dt.NewRow();
                row["序号"] = i + 1;
                row["名称"] = funcs[i].Name;
                row["类型"] = funcs[i].Type;
                row["最近更新"] = funcs[i].LastUpdate;
                dt.Rows.Add(row);
            }
            dataGridView1.DataSource = dt;
            int wid = dataGridView1.Width;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = Convert.ToInt32((wid - 150 - 60) * 0.3);
            dataGridView1.Columns[3].Width = Convert.ToInt32((wid - 150 - 60) * 0.4);
            dataGridView1.Columns[4].Width = Convert.ToInt32((wid - 150 - 60) * 0.3);
            if (dt.Rows.Count > 0)
            {
                InitFuncInfo(dt.Rows[0]["名称"].ToString());
            }
        }

        private void InitFuncInfo(string funcName)
        {
            richTextBox1.Location = dataGridView2.Location;
            richTextBox1.Size = dataGridView2.Size;
            dataGridView2.Visible = false;
            richTextBox1.Visible = true;
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            richTextBox1.Text = iDb.CreateFuncSql(funcName);
            DisableAllRad();
        }

        private void 全选ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                chk.Value = true;
            }
        }

        private void 反选ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                if (chk.Value == null || (chk.Value as bool?) == false)
                {
                    chk.Value = true;
                }
                else
                {
                    chk.Value = false;
                }
            }
        }

        private void 取消选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                chk.Value = false;
            }
        }

        private void radData_Click(object sender, EventArgs e)
        {
            if (radView.Checked)
            {
                radConstraint.Enabled = false;
                radTritbl.Enabled = false;
                radIndex.Enabled = false;
            }
            IDbAccess iDb = DataTransfer.iDb;
            string sql = "select * from " + tblStruct.Name;
            DataTable dt = iDb.GetDataTable(sql);
            dataGridView2.DataSource = dt.DefaultView;
            richTextBox1.Visible = false;
            dataGridView2.Visible = true;
        }

        private void radConstraint_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("序号"));
            dt.Columns.Add(new DataColumn("类型"));
            dt.Columns.Add(new DataColumn("名称"));
            dt.Columns.Add(new DataColumn("约束列"));
            dt.Columns.Add(new DataColumn("备注"));
            for (int i = 0; i < tblStruct.Constraints.Count; i++)
            {
                DataRow row = dt.NewRow();
                row["序号"] = i + 1;
                row["名称"] = tblStruct.Constraints[i].Name;
                row["类型"] = tblStruct.Constraints[i].Type;
                row["约束列"] = tblStruct.Constraints[i].Keys;
                row["备注"] = tblStruct.Constraints[i].Remark;

                dt.Rows.Add(row);
            }
            dataGridView2.DataSource = dt;
        }

        private void radTritbl_Click(object sender, EventArgs e)
        {
            DataTable dtsor = new DataTable();
            dtsor.Columns.Add(new DataColumn("序号"));
            dtsor.Columns.Add(new DataColumn("名称"));
            dtsor.Columns.Add(new DataColumn("说明"));
            for (int i = 0; i < tblStruct.Triggers.Count; i++)
            {
                DataRow row = dtsor.NewRow();
                row["序号"] = (i + 1);
                row["名称"] = tblStruct.Triggers[i].Name;
                row["说明"] = tblStruct.Triggers[i].Type;
                dtsor.Rows.Add(row);
            }
            dataGridView2.DataSource = dtsor;
        }

        private void radStruct_Click(object sender, EventArgs e)
        {
            InitTableInfo();
        }

        private void radioButton1_Click_1(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(); dt.Columns.Add(); dt.Columns.Add();
            dt.Columns[0].ColumnName = "索引名字";
            dt.Columns[1].ColumnName = "描述";
            dt.Columns[2].ColumnName = "作用列";
            for (int i = 0; i < tblStruct.Indexs.Count; i++)
            {
                DataRow row = dt.NewRow();
                row["索引名字"] = tblStruct.Indexs[i].Name;
                row["作用列"] = tblStruct.Indexs[i].Keys;
                row["描述"] = tblStruct.Indexs[i].Desc;
                dt.Rows.Add(row);
            }
            dataGridView2.DataSource = dt;
        }

        private async void btnSQL_Click(object sender, EventArgs e)
        {
            string res = "";
            if (radTable.Checked)
            {
                if (MessageBox.Show("是否希望导出带数据的insert语句(image、text数据类型的字段不参与导出)?", "确认框", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    btnSQL.Text = "执行中...";
                    btnSQL.Enabled = false;
                    try
                    {
                        res = await CreateTblSql(true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        btnSQL.Text = "生成SQL";
                        btnSQL.Enabled = true;
                    }
                }
                else
                {
                    btnSQL.Text = "执行中...";
                    btnSQL.Enabled = false;
                    try
                    {
                        res = await CreateTblSql(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        btnSQL.Text = "生成SQL";
                        btnSQL.Enabled = true;
                    }
                }

            }
            else if (radView.Checked)
            {
                res = CreateViewSql();
            }
            else if (radProc.Checked)
            {
                res = CreateProcSql();
            }
            else if (radFunc.Checked)
            {
                res = CreateFuncSql();
            }
            if (res == "") { return; }
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "请选择文件路径";
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            System.Windows.Forms.DialogResult diares = folderBrowserDialog1.ShowDialog();
            if (diares == DialogResult.OK || diares == DialogResult.Yes)
            {
                string str = folderBrowserDialog1.SelectedPath;
                string filepath = Path.Combine(str, DateTime.Now.ToString("yyyy-MM-dd") + "Gene.sql");
                if (File.Exists(filepath))
                {
                    if (MessageBox.Show("发现已存在文件" + filepath + "是否覆盖？", "文件输出选择", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    {
                        return;
                    }
                    else
                    {
                        File.Delete(filepath);
                    }
                }
                File.WriteAllText(filepath, res, Encoding.UTF8);
                MessageBox.Show("生成成功!");
                System.Diagnostics.Process.Start("Explorer.exe", "/select," + filepath);
            }

        }
        private async Task<string> CreateTblSql(bool isContainInsert)
        {
            List<TableStruct> li = await CreateTableList();
            if (li.Count == 0) { MessageBox.Show("请先选择表!"); return ""; }
            StringBuilder sb = new StringBuilder();
            SqlServerIDbAccess iDb = IDBFactory.CreateIDB(DataTransfer.iDb.ConnectionString, "SQLSERVER") as SqlServerIDbAccess;
            await Task.Factory.StartNew(() =>
            {
                if (isContainInsert)
                {
                    iDb.PreExportDataProc();
                }
                li.ForEach(tbl =>
                {
                    sb.AppendLine("\r\n--****************<" + tbl.Name + ">**************");
                    sb.AppendLine(string.Format(@"if exists (select 1  
            from  sysobjects  
           where  id = object_id('{0}')  
            and   type = 'U')  
begin
   drop table {0}  
   print '已删除表:{0}'
end
go 
", tbl.Name));
                    sb.AppendLine(iDb.CreateTableSql(tbl));
                    sb.AppendLine(string.Format("print '已创建:{0}'\r\ngo", tbl.Name));
                    if (isContainInsert)
                    {
                        sb.AppendLine("print '开始插入数据...'");
                        int count = 0;
                        sb.AppendLine(iDb.GeneInsertSql(tbl.Name, ref count));
                        sb.AppendLine("print '插入成功,总共:" + count + "行'");
                    }
                    sb.AppendLine("--****************</" + tbl.Name + ">**************");
                });
            });

            return sb.ToString();
        }
        private string CreateViewSql()
        {
            SqlServerIDbAccess iDb = IDBFactory.CreateIDB(DataTransfer.iDb.ConnectionString, "SQLSERVER") as SqlServerIDbAccess;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((dataGridView1.Rows[i].Cells[0].Value as bool?) == true)
                {
                    string viewName = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    sb.Append("\r\n--****************<" + viewName + ">**************\r\n");
                    sb.Append(string.Format(@"if exists (select 1  
            from  sys.views  
           where  name = '{0}')
begin
   drop view {0}  
   print '已删除视图:{0}'
end
go
", viewName));
                    sb.Append(iDb.CreateViewSql(viewName));
                    sb.AppendLine(string.Format("\r\ngo\r\nprint '已创建视图:{0}'\r\ngo\r\n--****************</", viewName) + viewName + ">**************");
                }
            }
            return sb.ToString();
        }
        private string CreateProcSql()
        {
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((dataGridView1.Rows[i].Cells[0].Value as bool?) == true)
                {
                    string procName = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    sb.Append("\r\n--****************<" + procName + ">**************\r\n");
                    sb.Append(string.Format(@"if exists (select 1  
            from  sys.procedures  
           where  name = '{0}')  
begin
   drop proc {0}  
   print '已删除存储过程:{0}'
end
go
", procName));
                    sb.AppendLine(iDb.CreateProcSql(procName));
                    sb.AppendLine(string.Format("print '已创建:{0}'\r\ngo\r\n--****************</", procName) + procName + ">**************");
                }
            }
            return sb.ToString();
        }
        private string CreateFuncSql()
        {
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((dataGridView1.Rows[i].Cells[0].Value as bool?) == true)
                {
                    string funName = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    sb.AppendLine("--****************<" + funName + ">**************");
                    sb.AppendLine(string.Format(@"if exists (select 1 from sysobjects where xtype in('FN','IF') and name='{0}')  
begin
   drop function {0}  
   print '已删除函数:{0}'
end
go", funName));
                    sb.Append(iDb.CreateFuncSql(funName));
                    sb.AppendLine(string.Format("go\r\nprint '已创建:{0}'\r\ngo\r\n--****************</", funName) + funName + ">**************");
                }
            }
            return sb.ToString();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
    }

    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}