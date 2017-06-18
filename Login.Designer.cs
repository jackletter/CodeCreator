namespace CodeCreator
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.txtUID = new System.Windows.Forms.TextBox();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.radSqlserver = new System.Windows.Forms.RadioButton();
            this.radOracle = new System.Windows.Forms.RadioButton();
            this.radMysql = new System.Windows.Forms.RadioButton();
            this.comConns = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picLoad = new System.Windows.Forms.PictureBox();
            this.radPostgresql = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.picLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("宋体", 12F);
            this.txtIP.Location = new System.Drawing.Point(123, 102);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(285, 26);
            this.txtIP.TabIndex = 0;
            // 
            // txtDB
            // 
            this.txtDB.Font = new System.Drawing.Font("宋体", 12F);
            this.txtDB.Location = new System.Drawing.Point(123, 168);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(285, 26);
            this.txtDB.TabIndex = 1;
            // 
            // txtUID
            // 
            this.txtUID.Font = new System.Drawing.Font("宋体", 12F);
            this.txtUID.Location = new System.Drawing.Point(123, 222);
            this.txtUID.Name = "txtUID";
            this.txtUID.Size = new System.Drawing.Size(285, 26);
            this.txtUID.TabIndex = 2;
            // 
            // txtPWD
            // 
            this.txtPWD.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPWD.Location = new System.Drawing.Point(123, 267);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(285, 26);
            this.txtPWD.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(21, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP端口地址:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(21, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "数据库名称:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(39, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "账户:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(39, 276);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "密码:";
            // 
            // btnLogin
            // 
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Font = new System.Drawing.Font("宋体", 14F);
            this.btnLogin.Location = new System.Drawing.Point(42, 325);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(134, 51);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("宋体", 14F);
            this.btnExit.Location = new System.Drawing.Point(231, 325);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(124, 51);
            this.btnExit.TabIndex = 5;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button2_Click);
            // 
            // radSqlserver
            // 
            this.radSqlserver.AutoSize = true;
            this.radSqlserver.Font = new System.Drawing.Font("宋体", 12F);
            this.radSqlserver.Location = new System.Drawing.Point(24, 64);
            this.radSqlserver.Name = "radSqlserver";
            this.radSqlserver.Size = new System.Drawing.Size(98, 20);
            this.radSqlserver.TabIndex = 10;
            this.radSqlserver.TabStop = true;
            this.radSqlserver.Text = "SqlServer";
            this.radSqlserver.UseVisualStyleBackColor = true;
            // 
            // radOracle
            // 
            this.radOracle.AutoSize = true;
            this.radOracle.Enabled = false;
            this.radOracle.Font = new System.Drawing.Font("宋体", 12F);
            this.radOracle.Location = new System.Drawing.Point(135, 64);
            this.radOracle.Name = "radOracle";
            this.radOracle.Size = new System.Drawing.Size(74, 20);
            this.radOracle.TabIndex = 11;
            this.radOracle.TabStop = true;
            this.radOracle.Text = "Oracle";
            this.radOracle.UseVisualStyleBackColor = true;
            // 
            // radMysql
            // 
            this.radMysql.AutoSize = true;
            this.radMysql.Enabled = false;
            this.radMysql.Font = new System.Drawing.Font("宋体", 12F);
            this.radMysql.Location = new System.Drawing.Point(227, 64);
            this.radMysql.Name = "radMysql";
            this.radMysql.Size = new System.Drawing.Size(66, 20);
            this.radMysql.TabIndex = 12;
            this.radMysql.TabStop = true;
            this.radMysql.Text = "MySql";
            this.radMysql.UseVisualStyleBackColor = true;
            // 
            // comConns
            // 
            this.comConns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comConns.Font = new System.Drawing.Font("宋体", 12F);
            this.comConns.FormattingEnabled = true;
            this.comConns.Location = new System.Drawing.Point(126, 21);
            this.comConns.Name = "comConns";
            this.comConns.Size = new System.Drawing.Size(296, 24);
            this.comConns.TabIndex = 13;
            this.comConns.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(40, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "使用连接:";
            // 
            // picLoad
            // 
            this.picLoad.Image = global::CodeCreator.Properties.Resources.loading;
            this.picLoad.Location = new System.Drawing.Point(168, 153);
            this.picLoad.Name = "picLoad";
            this.picLoad.Size = new System.Drawing.Size(74, 63);
            this.picLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoad.TabIndex = 15;
            this.picLoad.TabStop = false;
            this.picLoad.Visible = false;
            // 
            // radPostgresql
            // 
            this.radPostgresql.AutoSize = true;
            this.radPostgresql.Font = new System.Drawing.Font("宋体", 12F);
            this.radPostgresql.Location = new System.Drawing.Point(303, 64);
            this.radPostgresql.Name = "radPostgresql";
            this.radPostgresql.Size = new System.Drawing.Size(106, 20);
            this.radPostgresql.TabIndex = 16;
            this.radPostgresql.TabStop = true;
            this.radPostgresql.Text = "PostgreSql";
            this.radPostgresql.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 396);
            this.Controls.Add(this.radPostgresql);
            this.Controls.Add(this.picLoad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comConns);
            this.Controls.Add(this.radMysql);
            this.Controls.Add(this.radOracle);
            this.Controls.Add(this.radSqlserver);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUID);
            this.Controls.Add(this.txtDB);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtPWD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库登录";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLoad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.TextBox txtUID;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton radSqlserver;
        private System.Windows.Forms.RadioButton radOracle;
        private System.Windows.Forms.RadioButton radMysql;
        private System.Windows.Forms.ComboBox comConns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picLoad;
        private System.Windows.Forms.RadioButton radPostgresql;
    }
}

