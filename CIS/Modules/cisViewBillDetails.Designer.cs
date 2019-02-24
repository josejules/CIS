namespace CIS.Modules
{
    partial class cisViewBillDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cisViewBillDetails));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSettle = new System.Windows.Forms.Button();
            this.chkShowDueOnly = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboVisitNo = new System.Windows.Forms.ComboBox();
            this.txtPatientId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvViewBillDetails = new System.Windows.Forms.DataGridView();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblTotalAmountPaid = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotalDue = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotalDiscount = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblTotalAdvAdj = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblLastVisitNo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewBillDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.lblLastVisitNo);
            this.groupBox1.Controls.Add(this.btnSettle);
            this.groupBox1.Controls.Add(this.chkShowDueOnly);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.cboDepartment);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.lblPatientName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBillNo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboVisitNo);
            this.groupBox1.Controls.Add(this.txtPatientId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1084, 101);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // btnSettle
            // 
            this.btnSettle.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnSettle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSettle.ForeColor = System.Drawing.Color.MintCream;
            this.btnSettle.Location = new System.Drawing.Point(805, 55);
            this.btnSettle.Name = "btnSettle";
            this.btnSettle.Size = new System.Drawing.Size(70, 30);
            this.btnSettle.TabIndex = 134;
            this.btnSettle.Text = "Settle";
            this.btnSettle.UseVisualStyleBackColor = false;
            this.btnSettle.Click += new System.EventHandler(this.btnSettle_Click);
            // 
            // chkShowDueOnly
            // 
            this.chkShowDueOnly.AutoSize = true;
            this.chkShowDueOnly.Location = new System.Drawing.Point(640, 65);
            this.chkShowDueOnly.Name = "chkShowDueOnly";
            this.chkShowDueOnly.Size = new System.Drawing.Size(128, 18);
            this.chkShowDueOnly.TabIndex = 133;
            this.chkShowDueOnly.Text = "Show Due Only";
            this.chkShowDueOnly.UseVisualStyleBackColor = true;
            this.chkShowDueOnly.CheckedChanged += new System.EventHandler(this.chkShowDueOnly_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(990, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(38, 38);
            this.btnAdd.TabIndex = 132;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Items.AddRange(new object[] {
            "All",
            "Registration",
            "Investigation",
            "Pharmacy",
            "General"});
            this.cboDepartment.Location = new System.Drawing.Point(447, 59);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(169, 24);
            this.cboDepartment.TabIndex = 131;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(337, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 16);
            this.label5.TabIndex = 130;
            this.label5.Text = "Department";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1034, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 38);
            this.btnClose.TabIndex = 129;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.ForeColor = System.Drawing.Color.DarkRed;
            this.lblPatientName.Location = new System.Drawing.Point(128, 65);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(0, 16);
            this.lblPatientName.TabIndex = 128;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(11, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 127;
            this.label3.Text = "Patient Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(335, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 126;
            this.label2.Text = "Bill No";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Location = new System.Drawing.Point(447, 24);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(169, 22);
            this.txtBillNo.TabIndex = 125;
            this.txtBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNo_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(11, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 124;
            this.label4.Text = "Patient Id";
            // 
            // cboVisitNo
            // 
            this.cboVisitNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVisitNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVisitNo.FormattingEnabled = true;
            this.cboVisitNo.Location = new System.Drawing.Point(706, 23);
            this.cboVisitNo.Name = "cboVisitNo";
            this.cboVisitNo.Size = new System.Drawing.Size(169, 24);
            this.cboVisitNo.TabIndex = 30;
            this.cboVisitNo.SelectedIndexChanged += new System.EventHandler(this.cboVisitNo_SelectedIndexChanged);
            // 
            // txtPatientId
            // 
            this.txtPatientId.Location = new System.Drawing.Point(128, 27);
            this.txtPatientId.Name = "txtPatientId";
            this.txtPatientId.Size = new System.Drawing.Size(152, 22);
            this.txtPatientId.TabIndex = 31;
            this.txtPatientId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientId_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(637, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 29;
            this.label1.Text = "Visit No";
            // 
            // dgvViewBillDetails
            // 
            this.dgvViewBillDetails.AllowUserToAddRows = false;
            this.dgvViewBillDetails.AllowUserToDeleteRows = false;
            this.dgvViewBillDetails.AllowUserToOrderColumns = true;
            this.dgvViewBillDetails.AllowUserToResizeColumns = false;
            this.dgvViewBillDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvViewBillDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvViewBillDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvViewBillDetails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvViewBillDetails.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvViewBillDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvViewBillDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvViewBillDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvViewBillDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvViewBillDetails.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvViewBillDetails.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvViewBillDetails.Location = new System.Drawing.Point(5, 112);
            this.dgvViewBillDetails.MultiSelect = false;
            this.dgvViewBillDetails.Name = "dgvViewBillDetails";
            this.dgvViewBillDetails.ReadOnly = true;
            this.dgvViewBillDetails.RowHeadersVisible = false;
            this.dgvViewBillDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgvViewBillDetails.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvViewBillDetails.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvViewBillDetails.RowTemplate.ReadOnly = true;
            this.dgvViewBillDetails.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvViewBillDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvViewBillDetails.Size = new System.Drawing.Size(1084, 415);
            this.dgvViewBillDetails.TabIndex = 30;
            this.dgvViewBillDetails.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvViewBillDetails_MouseClick);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(113, 543);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(47, 18);
            this.lblTotalAmount.TabIndex = 150;
            this.lblTotalAmount.Text = "0.00";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Navy;
            this.label23.Location = new System.Drawing.Point(7, 543);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(109, 18);
            this.label23.TabIndex = 149;
            this.label23.Text = "Total Amt : ";
            // 
            // lblTotalAmountPaid
            // 
            this.lblTotalAmountPaid.AutoSize = true;
            this.lblTotalAmountPaid.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmountPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTotalAmountPaid.Location = new System.Drawing.Point(622, 543);
            this.lblTotalAmountPaid.Name = "lblTotalAmountPaid";
            this.lblTotalAmountPaid.Size = new System.Drawing.Size(47, 18);
            this.lblTotalAmountPaid.TabIndex = 152;
            this.lblTotalAmountPaid.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(523, 543);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 18);
            this.label6.TabIndex = 151;
            this.label6.Text = "Amt Paid : ";
            // 
            // lblTotalDue
            // 
            this.lblTotalDue.AutoSize = true;
            this.lblTotalDue.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTotalDue.Location = new System.Drawing.Point(976, 543);
            this.lblTotalDue.Name = "lblTotalDue";
            this.lblTotalDue.Size = new System.Drawing.Size(47, 18);
            this.lblTotalDue.TabIndex = 156;
            this.lblTotalDue.Text = "0.00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Navy;
            this.label10.Location = new System.Drawing.Point(918, 543);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 18);
            this.label10.TabIndex = 155;
            this.label10.Text = "Due : ";
            // 
            // lblTotalDiscount
            // 
            this.lblTotalDiscount.AutoSize = true;
            this.lblTotalDiscount.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTotalDiscount.Location = new System.Drawing.Point(421, 543);
            this.lblTotalDiscount.Name = "lblTotalDiscount";
            this.lblTotalDiscount.Size = new System.Drawing.Size(47, 18);
            this.lblTotalDiscount.TabIndex = 158;
            this.lblTotalDiscount.Text = "0.00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Navy;
            this.label12.Location = new System.Drawing.Point(301, 543);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(122, 18);
            this.label12.TabIndex = 157;
            this.label12.Text = "Concession : ";
            // 
            // lblTotalAdvAdj
            // 
            this.lblTotalAdvAdj.AutoSize = true;
            this.lblTotalAdvAdj.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAdvAdj.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTotalAdvAdj.Location = new System.Drawing.Point(810, 542);
            this.lblTotalAdvAdj.Name = "lblTotalAdvAdj";
            this.lblTotalAdvAdj.Size = new System.Drawing.Size(47, 18);
            this.lblTotalAdvAdj.TabIndex = 160;
            this.lblTotalAdvAdj.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(722, 542);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 18);
            this.label8.TabIndex = 159;
            this.label8.Text = "Adv Adj : ";
            // 
            // lblLastVisitNo
            // 
            this.lblLastVisitNo.AutoSize = true;
            this.lblLastVisitNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastVisitNo.ForeColor = System.Drawing.Color.Transparent;
            this.lblLastVisitNo.Location = new System.Drawing.Point(970, 69);
            this.lblLastVisitNo.Name = "lblLastVisitNo";
            this.lblLastVisitNo.Size = new System.Drawing.Size(0, 16);
            this.lblLastVisitNo.TabIndex = 135;
            // 
            // cisViewBillDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 576);
            this.Controls.Add(this.lblTotalAdvAdj);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblTotalDiscount);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblTotalDue);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblTotalAmountPaid);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.dgvViewBillDetails);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "cisViewBillDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "cisViewBillDetails";
            this.Load += new System.EventHandler(this.cisViewBillDetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewBillDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboVisitNo;
        private System.Windows.Forms.TextBox txtPatientId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvViewBillDetails;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblTotalAmountPaid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotalDue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalDiscount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkShowDueOnly;
        private System.Windows.Forms.Button btnSettle;
        private System.Windows.Forms.Label lblTotalAdvAdj;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblLastVisitNo;
    }
}