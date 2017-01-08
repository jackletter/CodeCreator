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
    public partial class AddTable : Form
    {
        public AddTable()
        {
            InitializeComponent();
        }

        private void AddTable_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("表名称和表说明不能为空!");
                return;
            }
            DBUtil.TableStruct tbl = new DBUtil.TableStruct();
            tbl.Name = textBox1.Text.Trim();
            tbl.Desc = textBox2.Text.Trim();
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((dataGridView1.Rows[i].Cells[0].Value ?? "").ToString().Trim() == "")
                    {
                        continue;
                    }

                    DBUtil.TableStruct.Column col = new DBUtil.TableStruct.Column();
                    tbl.Columns.Add(col);
                    col.Name = (dataGridView1.Rows[i].Cells[0].Value ?? "").ToString().Trim();
                    col.Type = (dataGridView1.Rows[i].Cells[1].Value ?? "").ToString().Trim();
                    col.IsNullable = (bool)(dataGridView1.Rows[i].Cells[2].Value ?? false);
                    col.IsPrimaryKey = (bool)(dataGridView1.Rows[i].Cells[3].Value ?? false);
                    col.IsIdentity = (bool)(dataGridView1.Rows[i].Cells[4].Value ?? false);
                    col.Start = int.Parse((dataGridView1.Rows[i].Cells[5].Value ?? "0").ToString());
                    col.Incre = int.Parse((dataGridView1.Rows[i].Cells[6].Value ?? "0").ToString());
                    col.Default = (dataGridView1.Rows[i].Cells[7].Value ?? "").ToString().Trim();
                    col.IsUnique = (bool)(dataGridView1.Rows[i].Cells[8].Value ?? false);
                    col.Desc = (dataGridView1.Rows[i].Cells[9].Value ?? "").ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入有误:" + ex.ToString());
                return;
            }
            tbl.Columns.ForEach(i =>
            {
                if (i.IsPrimaryKey)
                {
                    tbl.PrimaryKey += "," + i.Name;
                }
            });
            tbl.PrimaryKey = tbl.PrimaryKey.Trim(',');
            DBUtil.SqlServerIDbAccess iDb = DataTransfer.iDb as DBUtil.SqlServerIDbAccess;
            iDb.BeginTrans();
            try
            {
                iDb.CreateTable(tbl);
                iDb.Commit();
                MessageBox.Show("创建成功!");
            }
            catch (Exception ex)
            {
                iDb.Rollback();
                MessageBox.Show("创建出错:" + ex.ToString());
            }

        }
    }
}
