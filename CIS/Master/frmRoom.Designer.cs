namespace CIS.Master
{
    partial class frmRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRoom));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboWard = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRoomPrefix = new System.Windows.Forms.TextBox();
            this.lblRoomPrefix = new System.Windows.Forms.Label();
            this.txtRoomTo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRoomFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddBulk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboRoom = new System.Windows.Forms.ComboBox();
            this.lblRoom = new System.Windows.Forms.Label();
            this.rbtnBed = new System.Windows.Forms.RadioButton();
            this.rbtnRoom = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboRoomStatus = new System.Windows.Forms.ComboBox();
            this.txtRoomNo = new System.Windows.Forms.TextBox();
            this.btnAddSingle = new System.Windows.Forms.Button();
            this.lblRoomNo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvRoomAndBed = new System.Windows.Forms.DataGridView();
            this.roomId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomRowNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.roomDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.lblCheckEditModeRoom = new System.Windows.Forms.Label();
            this.lblRoomId = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomAndBed)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(156, 548);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(934, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 548);
            this.panel1.TabIndex = 23;
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(35, 118);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 40);
            this.btnClose.TabIndex = 15;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // btnNew
            // 
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.Location = new System.Drawing.Point(35, 72);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(78, 40);
            this.btnNew.TabIndex = 14;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(35, 27);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 40);
            this.btnSave.TabIndex = 13;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // cboWard
            // 
            this.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWard.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWard.FormattingEnabled = true;
            this.cboWard.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cboWard.Location = new System.Drawing.Point(49, 60);
            this.cboWard.Name = "cboWard";
            this.cboWard.Size = new System.Drawing.Size(272, 24);
            this.cboWard.TabIndex = 100;
            this.cboWard.SelectedIndexChanged += new System.EventHandler(this.cboWard_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 99;
            this.label3.Text = "Ward";
            // 
            // txtRoomPrefix
            // 
            this.txtRoomPrefix.AccessibleName = "";
            this.txtRoomPrefix.Location = new System.Drawing.Point(95, 19);
            this.txtRoomPrefix.MaxLength = 5;
            this.txtRoomPrefix.Name = "txtRoomPrefix";
            this.txtRoomPrefix.Size = new System.Drawing.Size(103, 22);
            this.txtRoomPrefix.TabIndex = 104;
            // 
            // lblRoomPrefix
            // 
            this.lblRoomPrefix.AutoSize = true;
            this.lblRoomPrefix.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomPrefix.ForeColor = System.Drawing.Color.Black;
            this.lblRoomPrefix.Location = new System.Drawing.Point(3, 21);
            this.lblRoomPrefix.Name = "lblRoomPrefix";
            this.lblRoomPrefix.Size = new System.Drawing.Size(88, 14);
            this.lblRoomPrefix.TabIndex = 105;
            this.lblRoomPrefix.Text = "Room Prefix";
            // 
            // txtRoomTo
            // 
            this.txtRoomTo.Location = new System.Drawing.Point(365, 20);
            this.txtRoomTo.Name = "txtRoomTo";
            this.txtRoomTo.Size = new System.Drawing.Size(79, 22);
            this.txtRoomTo.TabIndex = 106;
            this.txtRoomTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomTo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(338, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 14);
            this.label1.TabIndex = 107;
            this.label1.Text = "To";
            // 
            // txtRoomFrom
            // 
            this.txtRoomFrom.Location = new System.Drawing.Point(257, 21);
            this.txtRoomFrom.Name = "txtRoomFrom";
            this.txtRoomFrom.Size = new System.Drawing.Size(69, 22);
            this.txtRoomFrom.TabIndex = 108;
            this.txtRoomFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomFrom_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(209, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 14);
            this.label2.TabIndex = 109;
            this.label2.Text = "From";
            // 
            // btnAddBulk
            // 
            this.btnAddBulk.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnAddBulk.FlatAppearance.BorderSize = 0;
            this.btnAddBulk.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAddBulk.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBulk.Image")));
            this.btnAddBulk.Location = new System.Drawing.Point(463, 12);
            this.btnAddBulk.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddBulk.Name = "btnAddBulk";
            this.btnAddBulk.Size = new System.Drawing.Size(40, 40);
            this.btnAddBulk.TabIndex = 110;
            this.btnAddBulk.UseVisualStyleBackColor = true;
            this.btnAddBulk.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel2.Controls.Add(this.cboRoom);
            this.panel2.Controls.Add(this.lblRoom);
            this.panel2.Controls.Add(this.rbtnBed);
            this.panel2.Controls.Add(this.rbtnRoom);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.cboWard);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(3, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(928, 155);
            this.panel2.TabIndex = 111;
            // 
            // cboRoom
            // 
            this.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoom.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRoom.FormattingEnabled = true;
            this.cboRoom.Location = new System.Drawing.Point(49, 101);
            this.cboRoom.Name = "cboRoom";
            this.cboRoom.Size = new System.Drawing.Size(272, 24);
            this.cboRoom.TabIndex = 124;
            this.cboRoom.SelectedIndexChanged += new System.EventHandler(this.cboRoom_SelectedIndexChanged);
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoom.Location = new System.Drawing.Point(2, 105);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(44, 14);
            this.lblRoom.TabIndex = 123;
            this.lblRoom.Text = "Room";
            // 
            // rbtnBed
            // 
            this.rbtnBed.AutoSize = true;
            this.rbtnBed.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnBed.Location = new System.Drawing.Point(127, 11);
            this.rbtnBed.Name = "rbtnBed";
            this.rbtnBed.Size = new System.Drawing.Size(59, 27);
            this.rbtnBed.TabIndex = 122;
            this.rbtnBed.TabStop = true;
            this.rbtnBed.Text = "Bed";
            this.rbtnBed.UseVisualStyleBackColor = true;
            this.rbtnBed.CheckedChanged += new System.EventHandler(this.rbtnBed_CheckedChanged);
            // 
            // rbtnRoom
            // 
            this.rbtnRoom.AutoSize = true;
            this.rbtnRoom.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnRoom.Location = new System.Drawing.Point(5, 12);
            this.rbtnRoom.Name = "rbtnRoom";
            this.rbtnRoom.Size = new System.Drawing.Size(74, 27);
            this.rbtnRoom.TabIndex = 121;
            this.rbtnRoom.TabStop = true;
            this.rbtnRoom.Text = "Room";
            this.rbtnRoom.UseVisualStyleBackColor = true;
            this.rbtnRoom.CheckedChanged += new System.EventHandler(this.rbtnRoom_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRoomFrom);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnAddBulk);
            this.groupBox2.Controls.Add(this.txtRoomTo);
            this.groupBox2.Controls.Add(this.txtRoomPrefix);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lblRoomPrefix);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox2.Location = new System.Drawing.Point(342, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(512, 59);
            this.groupBox2.TabIndex = 120;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bulk Entry";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboRoomStatus);
            this.groupBox1.Controls.Add(this.txtRoomNo);
            this.groupBox1.Controls.Add(this.btnAddSingle);
            this.groupBox1.Controls.Add(this.lblRoomNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(342, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(512, 57);
            this.groupBox1.TabIndex = 119;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Single Entry";
            // 
            // cboRoomStatus
            // 
            this.cboRoomStatus.FormattingEnabled = true;
            this.cboRoomStatus.Items.AddRange(new object[] {
            "Inactive",
            "Active"});
            this.cboRoomStatus.Location = new System.Drawing.Point(270, 18);
            this.cboRoomStatus.Name = "cboRoomStatus";
            this.cboRoomStatus.Size = new System.Drawing.Size(167, 22);
            this.cboRoomStatus.TabIndex = 115;
            // 
            // txtRoomNo
            // 
            this.txtRoomNo.AccessibleName = "";
            this.txtRoomNo.Location = new System.Drawing.Point(96, 19);
            this.txtRoomNo.MaxLength = 5;
            this.txtRoomNo.Name = "txtRoomNo";
            this.txtRoomNo.Size = new System.Drawing.Size(103, 22);
            this.txtRoomNo.TabIndex = 112;
            // 
            // btnAddSingle
            // 
            this.btnAddSingle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddSingle.BackgroundImage")));
            this.btnAddSingle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddSingle.Location = new System.Drawing.Point(463, 11);
            this.btnAddSingle.Name = "btnAddSingle";
            this.btnAddSingle.Size = new System.Drawing.Size(40, 40);
            this.btnAddSingle.TabIndex = 116;
            this.btnAddSingle.UseVisualStyleBackColor = true;
            this.btnAddSingle.Click += new System.EventHandler(this.btnAddSingle_Click);
            // 
            // lblRoomNo
            // 
            this.lblRoomNo.AutoSize = true;
            this.lblRoomNo.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblRoomNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNo.ForeColor = System.Drawing.Color.Black;
            this.lblRoomNo.Location = new System.Drawing.Point(4, 21);
            this.lblRoomNo.Name = "lblRoomNo";
            this.lblRoomNo.Size = new System.Drawing.Size(66, 14);
            this.lblRoomNo.TabIndex = 113;
            this.lblRoomNo.Text = "Room No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(209, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 14);
            this.label5.TabIndex = 114;
            this.label5.Text = "Status";
            // 
            // dgvRoomAndBed
            // 
            this.dgvRoomAndBed.AllowUserToAddRows = false;
            this.dgvRoomAndBed.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoomAndBed.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRoomAndBed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoomAndBed.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoomAndBed.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRoomAndBed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoomAndBed.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roomId,
            this.roomRowNo,
            this.roomNo,
            this.roomStatus,
            this.roomEdit,
            this.roomDelete});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoomAndBed.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRoomAndBed.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvRoomAndBed.Location = new System.Drawing.Point(8, 170);
            this.dgvRoomAndBed.Name = "dgvRoomAndBed";
            this.dgvRoomAndBed.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.dgvRoomAndBed.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvRoomAndBed.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvRoomAndBed.RowTemplate.ReadOnly = true;
            this.dgvRoomAndBed.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRoomAndBed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvRoomAndBed.Size = new System.Drawing.Size(928, 394);
            this.dgvRoomAndBed.TabIndex = 0;
            this.dgvRoomAndBed.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoom_CellClick);
            // 
            // roomId
            // 
            this.roomId.HeaderText = "roomId";
            this.roomId.Name = "roomId";
            this.roomId.Visible = false;
            // 
            // roomRowNo
            // 
            this.roomRowNo.HeaderText = "#";
            this.roomRowNo.Name = "roomRowNo";
            // 
            // roomNo
            // 
            this.roomNo.HeaderText = "Room No";
            this.roomNo.Name = "roomNo";
            // 
            // roomStatus
            // 
            this.roomStatus.HeaderText = "Status";
            this.roomStatus.Name = "roomStatus";
            this.roomStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.roomStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // roomEdit
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.NullValue = null;
            this.roomEdit.DefaultCellStyle = dataGridViewCellStyle3;
            this.roomEdit.HeaderText = "Edit";
            this.roomEdit.Image = ((System.Drawing.Image)(resources.GetObject("roomEdit.Image")));
            this.roomEdit.Name = "roomEdit";
            // 
            // roomDelete
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.NullValue = null;
            this.roomDelete.DefaultCellStyle = dataGridViewCellStyle4;
            this.roomDelete.HeaderText = "Delete";
            this.roomDelete.Image = ((System.Drawing.Image)(resources.GetObject("roomDelete.Image")));
            this.roomDelete.Name = "roomDelete";
            // 
            // lblCheckEditModeRoom
            // 
            this.lblCheckEditModeRoom.AutoSize = true;
            this.lblCheckEditModeRoom.Location = new System.Drawing.Point(283, 79);
            this.lblCheckEditModeRoom.Name = "lblCheckEditModeRoom";
            this.lblCheckEditModeRoom.Size = new System.Drawing.Size(0, 13);
            this.lblCheckEditModeRoom.TabIndex = 117;
            this.lblCheckEditModeRoom.Visible = false;
            // 
            // lblRoomId
            // 
            this.lblRoomId.AutoSize = true;
            this.lblRoomId.Location = new System.Drawing.Point(263, 83);
            this.lblRoomId.Name = "lblRoomId";
            this.lblRoomId.Size = new System.Drawing.Size(0, 13);
            this.lblRoomId.TabIndex = 118;
            this.lblRoomId.Visible = false;
            // 
            // frmRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 576);
            this.Controls.Add(this.lblRoomId);
            this.Controls.Add(this.lblCheckEditModeRoom);
            this.Controls.Add(this.dgvRoomAndBed);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Room";
            this.Load += new System.EventHandler(this.frmRoom_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomAndBed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboWard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRoomPrefix;
        private System.Windows.Forms.Label lblRoomPrefix;
        private System.Windows.Forms.TextBox txtRoomTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRoomFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddBulk;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvRoomAndBed;
        private System.Windows.Forms.Label lblRoomNo;
        private System.Windows.Forms.TextBox txtRoomNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboRoomStatus;
        private System.Windows.Forms.Button btnAddSingle;
        private System.Windows.Forms.Label lblCheckEditModeRoom;
        private System.Windows.Forms.Label lblRoomId;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomId;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomRowNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomStatus;
        private System.Windows.Forms.DataGridViewImageColumn roomEdit;
        private System.Windows.Forms.DataGridViewImageColumn roomDelete;
        private System.Windows.Forms.RadioButton rbtnBed;
        private System.Windows.Forms.RadioButton rbtnRoom;
        private System.Windows.Forms.ComboBox cboRoom;
        private System.Windows.Forms.Label lblRoom;
    }
}