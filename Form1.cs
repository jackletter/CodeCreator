using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CodeCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ConnectConf conf = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Select();
            //textBox1.Text = ".";
            //textBox2.Text = "yh";
            //textBox3.Text = "sa";
            //textBox4.Text = "sa";
            button1.Focus();
            conf = new ConnectConf();
            foreach (var item in conf.Items)
            {
                comboBox1.Items.Add(item.ItemName);
            }
            if (conf.DefaultItem != null)
            {
                comboBox1.SelectedItem = conf.DefaultItem.ItemName;
            }
            InitLoginItem();
        }

        private void InitLoginItem()
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() != "")
            {
                ConnectConf.ConntectItem item = conf.Items.Find(i => i.ItemName == comboBox1.SelectedItem.ToString());
                if (item != null)
                {
                    textBox1.Text = item.IP;
                    textBox2.Text = item.DBName;
                    textBox3.Text = item.UserID;
                    textBox4.Text = item.PWD;
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    if (item.DBType == "SqlServer")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (item.DBType == "Oracle")
                    {
                        radioButton2.Checked = true;
                    }
                    else if (item.DBType == "MySql")
                    {
                        radioButton2.Checked = true;
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                try
                {
                    string connStr = string.Format(
                        "Data Source={0};Initial Catalog={1};User ID={2};Password={3};",
                        textBox1.Text.Trim(),
                        textBox2.Text.Trim(),
                        textBox3.Text.Trim(),
                        textBox4.Text.Trim());
                    DataTransfer.conn = new SqlConnection(connStr);
                    //new Form2().Show();
                    new Form4().Show();
                    this.Hide();
                }
                catch (Exception ee)
                {
                    MessageBox.Show("连接失败;\n" + ee.ToString());
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLoginItem();
        }
    }
}
