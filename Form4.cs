using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace CodeCreator
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.FormClosed += Form4_FormClosed;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            SqlConnection conn = DataTransfer.conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = @"
select 
	序号=ROW_NUMBER() OVER(order BY table_name),
	表名=table_name,
	说明=(SELECT value   
FROM sys.extended_properties ds  
LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id  
WHERE  ds.minor_id=0 and  
 tbs.name=table_name ),
	类型=table_type,
	数据库=table_catalog,
	架构=TABLE_SCHEMA	
from INFORMATION_SCHEMA.TABLES t
";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            dataGridView1.DataSource = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                InitTableInfo(ds.Tables[0].Rows[0]["表名"].ToString());
            }
        }

        void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (e.RowIndex < 0) { return; }
            string tableName = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            InitTableInfo(tableName);
        }

        private void InitTableInfo(string tableName)
        {
            textBox1.Text = tableName;
            SqlConnection conn = DataTransfer.conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sql = string.Format(@"
select 
	序号=ROW_NUMBER() OVER(order BY ORDINAL_POSITION),
	列名=t.COLUMN_NAME,
	类型=DATA_TYPE,
	说明=tt.说明,
	是否可空=IS_NULLABLE,
	默认值=COLUMN_DEFAULT,
	是否自增 
from INFORMATION_SCHEMA.COLUMNS T
left outer join
(select 列名=c.name,说明=p.value 
from sys.columns c 
	left outer join sys.objects o on c.object_id =o.object_id
	left outer join sys.extended_properties p on c.column_id = p.minor_id and c.object_id=p.major_id
where o.name='{0}')  tt
on t.COLUMN_NAME=tt.列名 
left outer join
(SELECT TABLE_NAME,COLUMN_NAME,是否自增=case (COLUMNPROPERTY(      
      OBJECT_ID('{0}'),COLUMN_NAME,'IsIdentity')) when 1 then '是' else '否' end  FROM INFORMATION_SCHEMA.columns) c
on c.COLUMN_NAME =T.COLUMN_NAME

where t.TABLE_NAME='{0}'
and c.TABLE_NAME='{0}' 
", tableName);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            dataGridView2.DataSource = ds.Tables[0];

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd.CommandText = string.Format(@"
select b.column_name
from information_schema.table_constraints a
inner join information_schema.constraint_column_usage b
on a.constraint_name = b.constraint_name
where a.constraint_type = 'PRIMARY KEY' and a.table_name = '{0}'
", tableName);
            adp = new SqlDataAdapter(cmd);
            ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string primaryKeys = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    primaryKeys += ds.Tables[0].Rows[i][0].ToString() + ",";
                }
                primaryKeys = primaryKeys.TrimEnd(',');
                textBox2.Text = primaryKeys;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 添加连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form5().ShowDialog();
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
                htres.Add(item.OutName, DynamicCreator.Create(item.Path,item.ClassFullName,item.CreateMethod));
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
    }
}
