namespace CodeCreator
{
    partial class MSMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MSMain));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnTMP = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCode = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.连接管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全选ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.反选ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.取消选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tblContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.alterContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.selContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.反选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radTable = new System.Windows.Forms.RadioButton();
            this.radView = new System.Windows.Forms.RadioButton();
            this.radProc = new System.Windows.Forms.RadioButton();
            this.radFunc = new System.Windows.Forms.RadioButton();
            this.radTri = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSQL = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radIndex = new System.Windows.Forms.RadioButton();
            this.radTritbl = new System.Windows.Forms.RadioButton();
            this.radConstraint = new System.Windows.Forms.RadioButton();
            this.radData = new System.Windows.Forms.RadioButton();
            this.radStruct = new System.Windows.Forms.RadioButton();
            this.picLoad = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnXML = new System.Windows.Forms.Button();
            this.btnDoc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tblContext.SuspendLayout();
            this.colContext.SuspendLayout();
            this.alterContext.SuspendLayout();
            this.selContext.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoad)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.选择});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(12, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(556, 259);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // 选择
            // 
            this.选择.FalseValue = "false";
            this.选择.HeaderText = "选择";
            this.选择.Name = "选择";
            this.选择.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.选择.TrueValue = "true";
            this.选择.Width = 513;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView2.Location = new System.Drawing.Point(596, 57);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.Size = new System.Drawing.Size(728, 259);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.dataGridView2.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Font = new System.Drawing.Font("宋体", 16F);
            this.label1.Location = new System.Drawing.Point(271, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择表:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Font = new System.Drawing.Font("宋体", 16F);
            this.textBox1.Location = new System.Drawing.Point(362, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(212, 34);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Font = new System.Drawing.Font("宋体", 16F);
            this.label2.Location = new System.Drawing.Point(578, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "主键:";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Font = new System.Drawing.Font("宋体", 16F);
            this.textBox2.Location = new System.Drawing.Point(650, 20);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(173, 34);
            this.textBox2.TabIndex = 5;
            // 
            // btnTMP
            // 
            this.btnTMP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTMP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTMP.Font = new System.Drawing.Font("宋体", 14F);
            this.btnTMP.Location = new System.Drawing.Point(17, 78);
            this.btnTMP.Name = "btnTMP";
            this.btnTMP.Size = new System.Drawing.Size(176, 50);
            this.btnTMP.TabIndex = 6;
            this.btnTMP.Text = "选择模板";
            this.btnTMP.UseVisualStyleBackColor = true;
            this.btnTMP.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Font = new System.Drawing.Font("宋体", 14F);
            this.btnRefresh.Location = new System.Drawing.Point(165, 337);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(131, 43);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnCode
            // 
            this.btnCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCode.Font = new System.Drawing.Font("宋体", 14F);
            this.btnCode.Location = new System.Drawing.Point(17, 20);
            this.btnCode.Name = "btnCode";
            this.btnCode.Size = new System.Drawing.Size(248, 50);
            this.btnCode.TabIndex = 8;
            this.btnCode.Text = "生成代码";
            this.btnCode.UseVisualStyleBackColor = true;
            this.btnCode.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("宋体", 14F);
            this.btnExit.Location = new System.Drawing.Point(319, 394);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 43);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Font = new System.Drawing.Font("宋体", 16F);
            this.label3.Location = new System.Drawing.Point(199, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 29);
            this.label3.TabIndex = 10;
            this.label3.Text = "模板方案路径:";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox3.Font = new System.Drawing.Font("宋体", 16F);
            this.textBox3.Location = new System.Drawing.Point(362, 79);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(461, 35);
            this.textBox3.TabIndex = 11;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接管理ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1336, 29);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 连接管理ToolStripMenuItem
            // 
            this.连接管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加连接ToolStripMenuItem,
            this.编辑连接ToolStripMenuItem});
            this.连接管理ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.连接管理ToolStripMenuItem.Name = "连接管理ToolStripMenuItem";
            this.连接管理ToolStripMenuItem.Size = new System.Drawing.Size(86, 25);
            this.连接管理ToolStripMenuItem.Text = "连接管理";
            // 
            // 添加连接ToolStripMenuItem
            // 
            this.添加连接ToolStripMenuItem.Name = "添加连接ToolStripMenuItem";
            this.添加连接ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.添加连接ToolStripMenuItem.Text = "添加连接";
            this.添加连接ToolStripMenuItem.Click += new System.EventHandler(this.添加连接ToolStripMenuItem_Click);
            // 
            // 编辑连接ToolStripMenuItem
            // 
            this.编辑连接ToolStripMenuItem.Name = "编辑连接ToolStripMenuItem";
            this.编辑连接ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.编辑连接ToolStripMenuItem.Text = "编辑连接";
            this.编辑连接ToolStripMenuItem.Click += new System.EventHandler(this.编辑连接ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem1,
            this.反选ToolStripMenuItem1,
            this.取消选择ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 全选ToolStripMenuItem1
            // 
            this.全选ToolStripMenuItem1.Name = "全选ToolStripMenuItem1";
            this.全选ToolStripMenuItem1.Size = new System.Drawing.Size(144, 26);
            this.全选ToolStripMenuItem1.Text = "全选";
            this.全选ToolStripMenuItem1.Click += new System.EventHandler(this.全选ToolStripMenuItem1_Click);
            // 
            // 反选ToolStripMenuItem1
            // 
            this.反选ToolStripMenuItem1.Name = "反选ToolStripMenuItem1";
            this.反选ToolStripMenuItem1.Size = new System.Drawing.Size(144, 26);
            this.反选ToolStripMenuItem1.Text = "反选";
            this.反选ToolStripMenuItem1.Click += new System.EventHandler(this.反选ToolStripMenuItem1_Click);
            // 
            // 取消选择ToolStripMenuItem
            // 
            this.取消选择ToolStripMenuItem.Name = "取消选择ToolStripMenuItem";
            this.取消选择ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.取消选择ToolStripMenuItem.Text = "取消选择";
            this.取消选择ToolStripMenuItem.Click += new System.EventHandler(this.取消选择ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tblContext
            // 
            this.tblContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.复制ToolStripMenuItem,
            this.删除表ToolStripMenuItem,
            this.新建表ToolStripMenuItem});
            this.tblContext.Name = "tblContext";
            this.tblContext.Size = new System.Drawing.Size(113, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem1.Text = "编辑";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 删除表ToolStripMenuItem
            // 
            this.删除表ToolStripMenuItem.Name = "删除表ToolStripMenuItem";
            this.删除表ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.删除表ToolStripMenuItem.Text = "删除表";
            this.删除表ToolStripMenuItem.Click += new System.EventHandler(this.删除表ToolStripMenuItem_Click);
            // 
            // 新建表ToolStripMenuItem
            // 
            this.新建表ToolStripMenuItem.Name = "新建表ToolStripMenuItem";
            this.新建表ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.新建表ToolStripMenuItem.Text = "新建表";
            this.新建表ToolStripMenuItem.Click += new System.EventHandler(this.新建表ToolStripMenuItem_Click);
            // 
            // colContext
            // 
            this.colContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem5,
            this.删除列ToolStripMenuItem,
            this.添加列ToolStripMenuItem});
            this.colContext.Name = "colContext";
            this.colContext.Size = new System.Drawing.Size(113, 92);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem3.Text = "编辑";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem5.Text = "复制";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // 删除列ToolStripMenuItem
            // 
            this.删除列ToolStripMenuItem.Name = "删除列ToolStripMenuItem";
            this.删除列ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.删除列ToolStripMenuItem.Text = "删除列";
            this.删除列ToolStripMenuItem.Click += new System.EventHandler(this.删除列ToolStripMenuItem_Click);
            // 
            // 添加列ToolStripMenuItem
            // 
            this.添加列ToolStripMenuItem.Name = "添加列ToolStripMenuItem";
            this.添加列ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.添加列ToolStripMenuItem.Text = "添加列";
            this.添加列ToolStripMenuItem.Click += new System.EventHandler(this.添加列ToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1253, 537);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 13;
            this.label4.Visible = false;
            // 
            // alterContext
            // 
            this.alterContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem4});
            this.alterContext.Name = "alterContext";
            this.alterContext.Size = new System.Drawing.Size(113, 48);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem2.Text = "设为是";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click_1);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem4.Text = "设为否";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // selContext
            // 
            this.selContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem,
            this.反选ToolStripMenuItem});
            this.selContext.Name = "selContext";
            this.selContext.Size = new System.Drawing.Size(101, 48);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            // 
            // 反选ToolStripMenuItem
            // 
            this.反选ToolStripMenuItem.Name = "反选ToolStripMenuItem";
            this.反选ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.反选ToolStripMenuItem.Text = "反选";
            // 
            // radTable
            // 
            this.radTable.AutoSize = true;
            this.radTable.Checked = true;
            this.radTable.Font = new System.Drawing.Font("宋体", 11F);
            this.radTable.Location = new System.Drawing.Point(30, 33);
            this.radTable.Name = "radTable";
            this.radTable.Size = new System.Drawing.Size(40, 19);
            this.radTable.TabIndex = 17;
            this.radTable.TabStop = true;
            this.radTable.Text = "表";
            this.radTable.UseVisualStyleBackColor = true;
            this.radTable.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radView
            // 
            this.radView.AutoSize = true;
            this.radView.Font = new System.Drawing.Font("宋体", 11F);
            this.radView.Location = new System.Drawing.Point(104, 33);
            this.radView.Name = "radView";
            this.radView.Size = new System.Drawing.Size(55, 19);
            this.radView.TabIndex = 18;
            this.radView.Text = "视图";
            this.radView.UseVisualStyleBackColor = true;
            this.radView.Click += new System.EventHandler(this.radioButton2_Click);
            // 
            // radProc
            // 
            this.radProc.AutoSize = true;
            this.radProc.Font = new System.Drawing.Font("宋体", 11F);
            this.radProc.Location = new System.Drawing.Point(189, 33);
            this.radProc.Name = "radProc";
            this.radProc.Size = new System.Drawing.Size(85, 19);
            this.radProc.TabIndex = 19;
            this.radProc.Text = "存储过程";
            this.radProc.UseVisualStyleBackColor = true;
            this.radProc.Click += new System.EventHandler(this.radioButton3_Click);
            // 
            // radFunc
            // 
            this.radFunc.AutoSize = true;
            this.radFunc.Font = new System.Drawing.Font("宋体", 11F);
            this.radFunc.Location = new System.Drawing.Point(301, 33);
            this.radFunc.Name = "radFunc";
            this.radFunc.Size = new System.Drawing.Size(55, 19);
            this.radFunc.TabIndex = 20;
            this.radFunc.Text = "函数";
            this.radFunc.UseVisualStyleBackColor = true;
            this.radFunc.Click += new System.EventHandler(this.radioButton4_Click);
            // 
            // radTri
            // 
            this.radTri.AutoSize = true;
            this.radTri.Enabled = false;
            this.radTri.Font = new System.Drawing.Font("宋体", 11F);
            this.radTri.Location = new System.Drawing.Point(407, 33);
            this.radTri.Name = "radTri";
            this.radTri.Size = new System.Drawing.Size(70, 19);
            this.radTri.TabIndex = 21;
            this.radTri.Text = "触发器";
            this.radTri.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.richTextBox1.Font = new System.Drawing.Font("宋体", 14F);
            this.richTextBox1.Location = new System.Drawing.Point(596, 57);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(728, 259);
            this.richTextBox1.TabIndex = 22;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // btnSQL
            // 
            this.btnSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSQL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSQL.Font = new System.Drawing.Font("宋体", 14F);
            this.btnSQL.Location = new System.Drawing.Point(165, 395);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new System.Drawing.Size(131, 43);
            this.btnSQL.TabIndex = 23;
            this.btnSQL.Text = "生成SQL";
            this.btnSQL.UseVisualStyleBackColor = true;
            this.btnSQL.Click += new System.EventHandler(this.btnSQL_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radIndex);
            this.panel1.Controls.Add(this.radTritbl);
            this.panel1.Controls.Add(this.radConstraint);
            this.panel1.Controls.Add(this.radData);
            this.panel1.Controls.Add(this.radStruct);
            this.panel1.Location = new System.Drawing.Point(596, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 20);
            this.panel1.TabIndex = 24;
            // 
            // radIndex
            // 
            this.radIndex.AutoSize = true;
            this.radIndex.Font = new System.Drawing.Font("宋体", 11F);
            this.radIndex.Location = new System.Drawing.Point(354, 1);
            this.radIndex.Name = "radIndex";
            this.radIndex.Size = new System.Drawing.Size(55, 19);
            this.radIndex.TabIndex = 26;
            this.radIndex.Text = "索引";
            this.radIndex.UseVisualStyleBackColor = true;
            this.radIndex.Click += new System.EventHandler(this.radioButton1_Click_1);
            // 
            // radTritbl
            // 
            this.radTritbl.AutoSize = true;
            this.radTritbl.Font = new System.Drawing.Font("宋体", 11F);
            this.radTritbl.Location = new System.Drawing.Point(255, 0);
            this.radTritbl.Name = "radTritbl";
            this.radTritbl.Size = new System.Drawing.Size(70, 19);
            this.radTritbl.TabIndex = 25;
            this.radTritbl.Text = "触发器";
            this.radTritbl.UseVisualStyleBackColor = true;
            this.radTritbl.Click += new System.EventHandler(this.radTritbl_Click);
            // 
            // radConstraint
            // 
            this.radConstraint.AutoSize = true;
            this.radConstraint.Font = new System.Drawing.Font("宋体", 11F);
            this.radConstraint.Location = new System.Drawing.Point(172, 1);
            this.radConstraint.Name = "radConstraint";
            this.radConstraint.Size = new System.Drawing.Size(55, 19);
            this.radConstraint.TabIndex = 24;
            this.radConstraint.Text = "约束";
            this.radConstraint.UseVisualStyleBackColor = true;
            this.radConstraint.Click += new System.EventHandler(this.radConstraint_Click);
            // 
            // radData
            // 
            this.radData.AutoSize = true;
            this.radData.Font = new System.Drawing.Font("宋体", 11F);
            this.radData.Location = new System.Drawing.Point(96, 0);
            this.radData.Name = "radData";
            this.radData.Size = new System.Drawing.Size(55, 19);
            this.radData.TabIndex = 23;
            this.radData.Text = "数据";
            this.radData.UseVisualStyleBackColor = true;
            this.radData.Click += new System.EventHandler(this.radData_Click);
            // 
            // radStruct
            // 
            this.radStruct.AutoSize = true;
            this.radStruct.Font = new System.Drawing.Font("宋体", 11F);
            this.radStruct.Location = new System.Drawing.Point(3, 0);
            this.radStruct.Name = "radStruct";
            this.radStruct.Size = new System.Drawing.Size(70, 19);
            this.radStruct.TabIndex = 22;
            this.radStruct.Text = "表结构";
            this.radStruct.UseVisualStyleBackColor = true;
            this.radStruct.Click += new System.EventHandler(this.radStruct_Click);
            // 
            // picLoad
            // 
            this.picLoad.Image = global::CodeCreator.Properties.Resources.loading;
            this.picLoad.Location = new System.Drawing.Point(716, 112);
            this.picLoad.Name = "picLoad";
            this.picLoad.Size = new System.Drawing.Size(74, 63);
            this.picLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoad.TabIndex = 25;
            this.picLoad.TabStop = false;
            this.picLoad.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btnCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.btnTMP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Italic);
            this.groupBox1.Location = new System.Drawing.Point(482, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(842, 144);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "代码生成";
            // 
            // btnXML
            // 
            this.btnXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXML.Font = new System.Drawing.Font("宋体", 14F);
            this.btnXML.Location = new System.Drawing.Point(5, 393);
            this.btnXML.Name = "btnXML";
            this.btnXML.Size = new System.Drawing.Size(154, 43);
            this.btnXML.TabIndex = 27;
            this.btnXML.Text = "导出表结构XML";
            this.btnXML.UseVisualStyleBackColor = true;
            this.btnXML.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnDoc
            // 
            this.btnDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoc.Font = new System.Drawing.Font("宋体", 14F);
            this.btnDoc.Location = new System.Drawing.Point(5, 338);
            this.btnDoc.Name = "btnDoc";
            this.btnDoc.Size = new System.Drawing.Size(154, 40);
            this.btnDoc.TabIndex = 28;
            this.btnDoc.Text = "生成表结构说明";
            this.btnDoc.UseVisualStyleBackColor = true;
            this.btnDoc.Click += new System.EventHandler(this.button5_Click);
            // 
            // MSMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1336, 472);
            this.Controls.Add(this.btnDoc);
            this.Controls.Add(this.btnXML);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picLoad);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSQL);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.radTri);
            this.Controls.Add(this.radFunc);
            this.Controls.Add(this.radProc);
            this.Controls.Add(this.radView);
            this.Controls.Add(this.radTable);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MSMain";
            this.Text = "MSSqlServer主窗口";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tblContext.ResumeLayout(false);
            this.colContext.ResumeLayout(false);
            this.alterContext.ResumeLayout(false);
            this.selContext.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoad)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnTMP;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCode;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 连接管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加连接ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ContextMenuStrip tblContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip colContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem 删除表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑连接ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除列ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip alterContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem 添加列ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip selContext;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 反选ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.RadioButton radTable;
        private System.Windows.Forms.RadioButton radView;
        private System.Windows.Forms.RadioButton radProc;
        private System.Windows.Forms.RadioButton radFunc;
        private System.Windows.Forms.RadioButton radTri;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnSQL;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 反选ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 取消选择ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radStruct;
        private System.Windows.Forms.RadioButton radData;
        private System.Windows.Forms.RadioButton radConstraint;
        private System.Windows.Forms.RadioButton radTritbl;
        private System.Windows.Forms.RadioButton radIndex;
        private System.Windows.Forms.PictureBox picLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnXML;
        private System.Windows.Forms.Button btnDoc;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
    }
}