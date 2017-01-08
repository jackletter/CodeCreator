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
    public partial class ConnEdit : Form
    {
        public ConnEdit()
        {
            InitializeComponent();
        }

        private string cuurentID { set; get; }

        private void ConnEdit_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("SqlServer");
            comboBox1.Items.Add("Oracle");
            comboBox1.Items.Add("MySql");
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Clear();
            if (DataTransfer.connnect.DefaultItem != null)
            {
                comboBox2.Items.Add(DataTransfer.connnect.DefaultItem.ItemName);
                comboBox2.SelectedIndex = 0;
                cuurentID = DataTransfer.connnect.DefaultItem.ItemID;
                textBox1.Text = DataTransfer.connnect.DefaultItem.IP;
                textBox2.Text = DataTransfer.connnect.DefaultItem.DBName;
                textBox3.Text = DataTransfer.connnect.DefaultItem.UserID;
                textBox4.Text = DataTransfer.connnect.DefaultItem.PWD;
                checkBox1.Checked = true;
            }
            DataTransfer.connnect.Items.ForEach(i =>
            {
                if (i.ItemName != DataTransfer.connnect.DefaultItem.ItemName)
                {
                    comboBox2.Items.Add(i.ItemName);
                }
            });
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox box = sender as System.Windows.Forms.ComboBox;
            string itemname = box.SelectedItem.ToString();
            for (int i = 0; i < DataTransfer.connnect.Items.Count; i++)
            {
                if (DataTransfer.connnect.Items[i].ItemName == itemname)
                {
                    LoadItem(DataTransfer.connnect.Items[i]);
                    cuurentID = DataTransfer.connnect.Items[i].ItemID;
                    break;
                }
            }
        }

        private void LoadItem(ConnectConf.ConntectItem item)
        {
            cuurentID = item.ItemID;
            textBox1.Text = item.IP;
            textBox2.Text = item.DBName;
            textBox3.Text = item.UserID;
            textBox4.Text = item.PWD;
            if (DataTransfer.connnect.DefaultItem.ItemID == item.ItemID)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectConf.ConntectItem item = DataTransfer.connnect.Items.Find(i => i.ItemID == cuurentID);
            item.IP = textBox1.Text;
            item.DBName = textBox2.Text;
            item.UserID = textBox3.Text;
            item.PWD = textBox4.Text;
            if (checkBox1.Checked)
            {
                DataTransfer.connnect.DefaultItem = item;
            }
            DataTransfer.connnect.WriteConf();
            MessageBox.Show("保存成功");
        }
    }
}
