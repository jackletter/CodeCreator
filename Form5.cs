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
    public partial class Form5 : Form
    {
        public Form5()
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
            Hashtable ht = new Hashtable();
            ht.Add("ItemName", textBox5.Text);
            ht.Add("DBType", comboBox1.SelectedItem.ToString().Trim());
            ht.Add("IP", textBox1.Text.Trim());
            ht.Add("DBName", textBox2.Text.Trim());
            ht.Add("UserID", textBox3.Text.Trim());
            ht.Add("PWD", textBox4.Text.Trim());
            Add(path, ht);
            textBox5.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            MessageBox.Show("添加成功,你可以继续添加!");

        }
        private void Add(string path, Hashtable ht)
        {
            if (!File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Close();
            }
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine("#Item_Start");
            sw.WriteLine("\t#ItemName=" + ht["ItemName"].ToString());
            sw.WriteLine("\t#DBType=" + ht["DBType"].ToString());
            sw.WriteLine("\t#IP=" + ht["IP"].ToString());
            sw.WriteLine("\t#DBName=" + ht["DBName"].ToString());
            sw.WriteLine("\t#UserID=" + ht["UserID"].ToString());
            sw.WriteLine("\t#PWD=" + ht["PWD"].ToString());
            sw.WriteLine("#Item_End");
            sw.Flush();
            sw.Close();
        }
    }
}
