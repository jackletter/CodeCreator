using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBUtil;

namespace CodeCreator
{
    public partial class Login : Form
    {
        public bool flag = false;
        public Login()
        {
            InitializeComponent();
        }

        private ConnectConf conf = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (flag) { return; }
            radioButton1.Select();
            button1.Focus();
            conf = new ConnectConf();
            DataTransfer.connnect = conf;
            foreach (var item in conf.Items)
            {
                comboBox1.Items.Add(item.ItemName);
            }
            if (conf.DefaultItem != null)
            {
                comboBox1.SelectedItem = conf.DefaultItem.ItemName;
            }
            InitLoginItem();
            flag = true;
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
                    IDbAccess iDb = IDBFactory.CreateIDB(connStr, "SQLSERVER");
                    DataTransfer.iDb = iDb;
                    MSMain msMain = new MSMain();
                    msMain.Text = "链接到:" + textBox1.Text.Trim() + "(" + textBox2.Text.Trim() + "/" + textBox3.Text.Trim() + ")";
                    msMain.Show();
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
