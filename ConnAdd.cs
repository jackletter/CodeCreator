using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace CodeCreator
{
    public partial class ConnAdd : Form
    {
        public ConnAdd()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("SqlServer");
            comboBox1.Items.Add("Oracle");
            comboBox1.Items.Add("MySql");
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" ||
                textBox2.Text.Trim() == "" ||
                textBox3.Text.Trim() == "" ||
                textBox4.Text.Trim() == "" ||
                textBox5.Text.Trim() == "")
            {
                MessageBox.Show("请填写完整!");
                return;
            }
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conn.conf");
            ConnectConf.ConntectItem conf = new ConnectConf.ConntectItem();
            conf.ItemName = textBox5.Text.Trim();
            conf.DBType = comboBox1.SelectedItem.ToString().Trim();
            conf.IP = textBox1.Text.Trim();
            conf.DBName = textBox2.Text.Trim();
            conf.UserID = textBox3.Text.Trim();
            conf.PWD = textBox4.Text.Trim();
            if (checkBox1.Checked)
            {
                DataTransfer.connnect.DefaultItem = conf;
            }
            else
            {
                DataTransfer.connnect.Items.Add(conf);
            }
            DataTransfer.connnect.WriteConf();            
            MessageBox.Show("添加成功,你可以继续添加!");

        }
    }
}
