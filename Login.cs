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
            radSqlserver.Select();
            btnLogin.Focus();
            conf = new ConnectConf();
            DataTransfer.connnect = conf;
            foreach (var item in conf.Items)
            {
                comConns.Items.Add(item.ItemName);
            }
            if (conf.DefaultItem != null)
            {
                comConns.SelectedItem = conf.DefaultItem.ItemName;
            }
            InitLoginItem();
        }

        private void InitLoginItem()
        {
            if (comConns.SelectedItem != null && comConns.SelectedItem.ToString() != "")
            {
                ConnectConf.ConntectItem item = conf.Items.Find(i => i.ItemName == comConns.SelectedItem.ToString());
                if (item != null)
                {
                    txtIP.Text = item.IP;
                    txtDB.Text = item.DBName;
                    txtUID.Text = item.UserID;
                    txtPWD.Text = item.PWD;
                    radSqlserver.Checked = false;
                    radOracle.Checked = false;
                    radMysql.Checked = false;
                    if (item.DBType == "SqlServer")
                    {
                        radSqlserver.Checked = true;
                    }
                    else if (item.DBType == "Oracle")
                    {
                        radOracle.Checked = true;
                    }
                    else if (item.DBType == "MySql")
                    {
                        radOracle.Checked = true;
                    }
                    else if (item.DBType == "PostgreSql")
                    {
                        radPostgresql.Checked = true;
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (btnExit.Text == "退出")
            {
                Application.Exit();
            }
            else
            {
                flag = false;
                EnableControls();
                picLoad.Hide();
                btnExit.Text = "退出";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = true;
            if (radSqlserver.Checked)
            {
                #region Sqlserver登录
                string connStr = string.Format(
                            "Data Source={0};Initial Catalog={1};User ID={2};Password={3};",
                            txtIP.Text.Trim(),
                            txtDB.Text.Trim(),
                            txtUID.Text.Trim(),
                            txtPWD.Text.Trim());
                IDbAccess iDb = IDBFactory.CreateIDB(connStr, "SQLSERVER");
                picLoad.Visible = true;
                picLoad.Refresh();
                btnExit.Text = "取消";
                DisableControls();
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    Result res = iDb.OpenTest();
                    if (!flag) { return; }
                    if (res.Success)
                    {
                        DataTransfer.iDb = iDb;
                        this.Invoke(
                    new MethodInvoker(() =>
                    {
                        MSMain msMain = new MSMain();
                        msMain.Text = "链接到:" + txtIP.Text.Trim() + "(" + txtDB.Text.Trim() + "/" + txtUID.Text.Trim() + ")";
                        msMain.Show();
                        this.Hide();
                    }));
                    }
                    else
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            picLoad.Hide();
                            flag = false;
                            EnableControls();
                            btnExit.Text = "退出";
                            MessageBox.Show(res.Data.ToString());
                        }));
                    }
                });
                #endregion
            }
            else if (radPostgresql.Checked)
            {
                #region PostgreSql登录
                string ip = txtIP.Text.Contains(',') ? txtIP.Text.Substring(0, txtIP.Text.IndexOf(',')) : txtIP.Text;
                string port = txtIP.Text.Contains(',') ? txtIP.Text.Substring(txtIP.Text.IndexOf(',') + 1) : "5432";
                string connStr = string.Format("Server={0};Port={1};UserId={2};Password={3};Database={4};",
                            ip,
                            port,
                            txtUID.Text.Trim(),
                            txtPWD.Text.Trim(),
                            txtDB.Text);
                IDbAccess iDb = IDBFactory.CreateIDB(connStr, "POSTGRESQL");
                picLoad.Visible = true;
                picLoad.Refresh();
                btnExit.Text = "取消";
                DisableControls();
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    Result res = iDb.OpenTest();
                    if (!flag) { return; }
                    if (res.Success)
                    {
                        DataTransfer.iDb = iDb;
                        this.Invoke(
                    new MethodInvoker(() =>
                    {
                        PostgreSqlMain main = new PostgreSqlMain();
                        main.Text = "链接到:" + txtIP.Text.Trim() + "(" + txtDB.Text.Trim() + "/" + txtUID.Text.Trim() + ")";
                        main.Show();
                        this.Hide();
                    }));
                    }
                    else
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            picLoad.Hide();
                            flag = false;
                            EnableControls();
                            btnExit.Text = "退出";
                            MessageBox.Show(res.Data.ToString());
                        }));
                    }
                });
                #endregion
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLoginItem();
        }

        private void DisableControls()
        {
            comConns.Enabled = false;
            txtDB.Enabled = false;
            txtIP.Enabled = false;
            txtPWD.Enabled = false;
            txtUID.Enabled = false;
            btnLogin.Enabled = false;
        }

        private void EnableControls()
        {
            comConns.Enabled = true;
            txtDB.Enabled = true;
            txtIP.Enabled = true;
            txtPWD.Enabled = true;
            txtUID.Enabled = true;
            btnLogin.Enabled = true;
        }
    }
}
