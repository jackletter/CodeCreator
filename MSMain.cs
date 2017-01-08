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

namespace CodeCreator
{
    public partial class MSMain : Form
    {
        public MSMain()
        {
            InitializeComponent();
            this.FormClosed += Form4_FormClosed;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            IDbAccess iDb = DataTransfer.iDb;
            string sql = @"
select 
	序号=ROW_NUMBER() OVER(order BY table_name),
	表名=table_name,
	说明=(SELECT value   
FROM sys.extended_properties ds  
LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id  
WHERE  ds.minor_id=0 and  
 tbs.name=table_name )/*,
	类型=table_type,
	数据库=table_catalog,
	架构=TABLE_SCHEMA	*/
from INFORMATION_SCHEMA.TABLES t
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
                InitTableInfo(ds.Tables[0].Rows[0]["表名"].ToString());
            }
        }

        void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitTableInfo(string tableName)
        {
            textBox1.Text = tableName;
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            TableStruct tblstruct = iDb.GetTableStruct(tableName);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("序号"));
            dt.Columns.Add(new DataColumn("列名"));
            dt.Columns.Add(new DataColumn("类型"));
            dt.Columns.Add(new DataColumn("说明"));
            dt.Columns.Add(new DataColumn("是否可空"));
            dt.Columns.Add(new DataColumn("是否唯一"));
            dt.Columns.Add(new DataColumn("是否自增"));
            dt.Columns.Add(new DataColumn("默认值"));
            for (int i = 0; i < tblstruct.Columns.Count; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = i + 1;
                row[1] = tblstruct.Columns[i].Name;
                if (tblstruct.Columns[i].Type.Contains("char"))
                {
                    row[2] = tblstruct.Columns[i].Type + "(" + (tblstruct.Columns[i].MaxLength == -1 ? "max" : tblstruct.Columns[i].MaxLength.ToString()) + ")";
                }
                else
                {
                    row[2] = tblstruct.Columns[i].Type;
                }
                row[3] = tblstruct.Columns[i].Desc;
                row[4] = tblstruct.Columns[i].IsNullable ? "是" : "否";
                row[5] = tblstruct.Columns[i].IsUnique ? "是" : "否";
                row[6] = tblstruct.Columns[i].IsIdentity ? "是" : "否";
                row[7] = tblstruct.Columns[i].Default;
                dt.Rows.Add(row);
            }
            dataGridView2.DataSource = dt.DefaultView;
            textBox2.Text = tblstruct.PrimaryKey;
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
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex > -1 && e.RowIndex > -1)  //点击的是鼠标右键，并且不是表头
            {
                if (e.ColumnIndex < 2)
                {
                    return;
                }
                //右键选中单元格
                tblEdit = e.ColumnIndex;//记录编辑的列索引
                tblRowEdit = e.RowIndex;//记录编辑的行索引
                tblOldval = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                }
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                this.tblContext.Show(MousePosition.X, MousePosition.Y); //MousePosition.X, MousePosition.Y 是为了让菜单在所选行的位置显示
            }
            else
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

                }
                else
                {
                    //其他列刷新列说明列表
                    int rowIndex = e.RowIndex;
                    if (e.RowIndex < 0) { return; }
                    string tableName = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                    InitTableInfo(tableName);
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
                    if (tblEdit == 1)
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
                    else if (tblEdit == 2)
                    {
                        try
                        {
                            iDb.SaveTableDesc(dataGridView1.Rows[tblRowEdit].Cells[1].Value.ToString(), newVal);
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

        private void button5_Click(object sender, EventArgs e)
        {
            List<TableStruct> res = CreateTableList();
            if (res.Count == 0)
            {
                MessageBox.Show("请选中要生成表结构的文档!");
                return;
            }
            DocX doc = DocX.Load("表结构说明模板.docx");

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
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "请选择文件路径";
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK || folderBrowserDialog1.ShowDialog() == DialogResult.Yes)
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

        private List<TableStruct> CreateTableList()
        {
            SqlServerIDbAccess iDb = DataTransfer.iDb as SqlServerIDbAccess;
            List<string> tblNames = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((dataGridView1.Rows[i].Cells[0].Value as bool?) == true)
                {
                    tblNames.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
                }
            }
            return iDb.GetTableStructs(tblNames);
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

        private void button6_Click(object sender, EventArgs e)
        {
            List<TableStruct> res = CreateTableList();
            if (res.Count == 0)
            {
                MessageBox.Show("请选中表!");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
            for (int i = 0; i < res.Count; i++)
            {
                sb.AppendLine("<" + res[i].Name + ">");
                for (int j = 0; j < res[i].Columns.Count; j++)
                {
                    sb.AppendLine(string.Format("    <{0}><![CDATA[{1}]]></{0}>", res[i].Columns[j].Name, res[i].Columns[j].Desc));
                }
                sb.AppendLine("</" + res[i].Name + ">");
            }

            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "请选择文件路径";
            folderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK || folderBrowserDialog1.ShowDialog() == DialogResult.Yes)
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
                    InitTableInfo(textBox1.Text);
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
    }
}
