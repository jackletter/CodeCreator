using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeCreator
{
    public partial class AddCol : Form
    {
        public AddCol()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCol_Load(object sender, EventArgs e)
        {
            textBox1.Text = DataTransfer.TableName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBUtil.TableStruct.Column col = new DBUtil.TableStruct.Column();
            col.Name = textBox2.Text;
            col.Type = textBox3.Text;
            col.Desc = textBox4.Text;
            col.IsNullable = checkBox1.Checked;
            col.IsUnique = checkBox2.Checked;
            col.Default = textBox6.Text;
            DBUtil.SqlServerIDbAccess iDb = DataTransfer.iDb as DBUtil.SqlServerIDbAccess;
            try
            {
                iDb.AddColumn(textBox1.Text, col);
                MessageBox.Show("添加成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败:" + ex.ToString());
            }
        }
    }
}
