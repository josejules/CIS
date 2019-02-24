namespace CIS.Modules
{
    partial class CISPurchaseReceipt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CISPurchaseReceipt));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tslGRNNo = new System.Windows.Forms.ToolStripLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTaxFormula = new System.Windows.Forms.ComboBox();
            this.lblTinNo = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cboVendor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpReceiveDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.dgvPurchaseReceipt = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNetValuePur = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblDiscountAmtPur = new System.Windows.Forms.Label();
            this.txtDiscountValuePur = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.lblTotalAmoutPur = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.txtReturnAmtPur = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalTaxPur = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAmountPaidPur = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblBalanceAmtPur = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.sNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receiveQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offerQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendorPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.disPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discountAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taxPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taxAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboCalFreeTax = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.freeTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.netAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_type_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taxFormula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.del = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseReceipt)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1225, 42);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.tslGRNNo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1225, 42);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.ForeColor = System.Drawing.Color.SteelBlue;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(147, 39);
            this.toolStripLabel1.Text = "Purchase Receipt";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(75, 39);
            this.toolStripLabel2.Text = "GRN No : ";
            // 
            // tslGRNNo
            // 
            this.tslGRNNo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslGRNNo.ForeColor = System.Drawing.Color.Purple;
            this.tslGRNNo.Name = "tslGRNNo";
            this.tslGRNNo.Size = new System.Drawing.Size(0, 39);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboTaxFormula);
            this.groupBox1.Controls.Add(this.lblTinNo);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cboVendor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpReceiveDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpInvoiceDate);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtInvoiceNo);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.Green;
            this.groupBox1.Location = new System.Drawing.Point(7, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1214, 95);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Invoice Details";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(935, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 18);
            this.label4.TabIndex = 150;
            this.label4.Text = "*";
            // 
            // cboTaxFormula
            // 
            this.cboTaxFormula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTaxFormula.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboTaxFormula.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTaxFormula.FormattingEnabled = true;
            this.cboTaxFormula.Items.AddRange(new object[] {
            "MRP Based",
            "CP Based"});
            this.cboTaxFormula.Location = new System.Drawing.Point(604, 14);
            this.cboTaxFormula.Name = "cboTaxFormula";
            this.cboTaxFormula.Size = new System.Drawing.Size(165, 24);
            this.cboTaxFormula.TabIndex = 149;
            // 
            // lblTinNo
            // 
            this.lblTinNo.AutoSize = true;
            this.lblTinNo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTinNo.ForeColor = System.Drawing.Color.Black;
            this.lblTinNo.Location = new System.Drawing.Point(960, 60);
            this.lblTinNo.Name = "lblTinNo";
            this.lblTinNo.Size = new System.Drawing.Size(0, 15);
            this.lblTinNo.TabIndex = 148;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(879, 60);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 14);
            this.label21.TabIndex = 147;
            this.label21.Text = "Tin No";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(879, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 14);
            this.label11.TabIndex = 145;
            this.label11.Text = "Supplier";
            // 
            // cboVendor
            // 
            this.cboVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVendor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboVendor.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVendor.FormattingEnabled = true;
            this.cboVendor.Location = new System.Drawing.Point(960, 18);
            this.cboVendor.Name = "cboVendor";
            this.cboVendor.Size = new System.Drawing.Size(220, 24);
            this.cboVendor.TabIndex = 146;
            this.cboVendor.SelectedIndexChanged += new System.EventHandler(this.cboVendor_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(459, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 14);
            this.label2.TabIndex = 143;
            this.label2.Text = "Receive Date";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(551, 58);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(15, 18);
            this.label20.TabIndex = 142;
            this.label20.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(459, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 14);
            this.label7.TabIndex = 140;
            this.label7.Text = "Vat Type";
            // 
            // dtpReceiveDate
            // 
            this.dtpReceiveDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpReceiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReceiveDate.Location = new System.Drawing.Point(604, 54);
            this.dtpReceiveDate.Name = "dtpReceiveDate";
            this.dtpReceiveDate.Size = new System.Drawing.Size(166, 26);
            this.dtpReceiveDate.TabIndex = 141;
            this.dtpReceiveDate.Value = new System.DateTime(2016, 10, 27, 9, 42, 3, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(99, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 18);
            this.label1.TabIndex = 139;
            this.label1.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(13, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 14);
            this.label5.TabIndex = 137;
            this.label5.Text = "Invoice Date";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(164, 54);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(166, 26);
            this.dtpInvoiceDate.TabIndex = 138;
            this.dtpInvoiceDate.Value = new System.DateTime(2016, 10, 27, 9, 42, 3, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(85, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(15, 18);
            this.label19.TabIndex = 136;
            this.label19.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(13, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 14);
            this.label6.TabIndex = 135;
            this.label6.Text = "Invoice No";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(164, 17);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(166, 26);
            this.txtInvoiceNo.TabIndex = 134;
            // 
            // dgvPurchaseReceipt
            // 
            this.dgvPurchaseReceipt.AllowUserToOrderColumns = true;
            this.dgvPurchaseReceipt.AllowUserToResizeColumns = false;
            this.dgvPurchaseReceipt.AllowUserToResizeRows = false;
            this.dgvPurchaseReceipt.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseReceipt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPurchaseReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sNo,
            this.cboItemName,
            this.itemType,
            this.batchNo,
            this.expDate,
            this.packX,
            this.packY,
            this.receiveQty,
            this.offerQty,
            this.vendorPrice,
            this.mrp,
            this.total,
            this.disPerc,
            this.discountAmt,
            this.taxPerc,
            this.taxAmt,
            this.cboCalFreeTax,
            this.freeTax,
            this.netAmt,
            this.item_type_id,
            this.item_id,
            this.taxFormula,
            this.del});
            this.dgvPurchaseReceipt.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchaseReceipt.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPurchaseReceipt.Location = new System.Drawing.Point(9, 150);
            this.dgvPurchaseReceipt.MultiSelect = false;
            this.dgvPurchaseReceipt.Name = "dgvPurchaseReceipt";
            this.dgvPurchaseReceipt.RowHeadersVisible = false;
            this.dgvPurchaseReceipt.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPurchaseReceipt.Size = new System.Drawing.Size(1212, 397);
            this.dgvPurchaseReceipt.TabIndex = 2;
            this.dgvPurchaseReceipt.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseReceipt_CellClick);
            this.dgvPurchaseReceipt.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseReceipt_CellValueChanged);
            this.dgvPurchaseReceipt.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvPurchaseReceipt_EditingControlShowing);
            this.dgvPurchaseReceipt.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvPurchaseReceipt_UserAddedRow);
            this.dgvPurchaseReceipt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvPurchaseReceipt_KeyDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.lblNetValuePur);
            this.panel2.Controls.Add(this.label61);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Location = new System.Drawing.Point(0, 622);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1230, 50);
            this.panel2.TabIndex = 3;
            // 
            // lblNetValuePur
            // 
            this.lblNetValuePur.AutoSize = true;
            this.lblNetValuePur.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetValuePur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblNetValuePur.Location = new System.Drawing.Point(198, 10);
            this.lblNetValuePur.Name = "lblNetValuePur";
            this.lblNetValuePur.Size = new System.Drawing.Size(73, 29);
            this.lblNetValuePur.TabIndex = 150;
            this.lblNetValuePur.Text = "0.00";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.ForeColor = System.Drawing.Color.Navy;
            this.label61.Location = new System.Drawing.Point(16, 10);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(169, 29);
            this.label61.TabIndex = 149;
            this.label61.Text = "Net Value : ";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1145, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 40);
            this.btnClose.TabIndex = 3;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(1061, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(78, 40);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.Location = new System.Drawing.Point(977, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(78, 40);
            this.btnNew.TabIndex = 1;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(893, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblDiscountAmtPur
            // 
            this.lblDiscountAmtPur.AutoSize = true;
            this.lblDiscountAmtPur.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscountAmtPur.ForeColor = System.Drawing.Color.Black;
            this.lblDiscountAmtPur.Location = new System.Drawing.Point(218, 595);
            this.lblDiscountAmtPur.Name = "lblDiscountAmtPur";
            this.lblDiscountAmtPur.Size = new System.Drawing.Size(31, 15);
            this.lblDiscountAmtPur.TabIndex = 155;
            this.lblDiscountAmtPur.Text = "0.00";
            // 
            // txtDiscountValuePur
            // 
            this.txtDiscountValuePur.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountValuePur.Location = new System.Drawing.Point(133, 591);
            this.txtDiscountValuePur.Name = "txtDiscountValuePur";
            this.txtDiscountValuePur.Size = new System.Drawing.Size(68, 23);
            this.txtDiscountValuePur.TabIndex = 154;
            this.txtDiscountValuePur.Text = "0.00";
            this.txtDiscountValuePur.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscountValuePur_KeyDown);
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.ForeColor = System.Drawing.Color.Black;
            this.label66.Location = new System.Drawing.Point(7, 595);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(95, 14);
            this.label66.TabIndex = 153;
            this.label66.Text = "Total Discount";
            // 
            // lblTotalAmoutPur
            // 
            this.lblTotalAmoutPur.AutoSize = true;
            this.lblTotalAmoutPur.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblTotalAmoutPur.ForeColor = System.Drawing.Color.Black;
            this.lblTotalAmoutPur.Location = new System.Drawing.Point(138, 563);
            this.lblTotalAmoutPur.Name = "lblTotalAmoutPur";
            this.lblTotalAmoutPur.Size = new System.Drawing.Size(35, 14);
            this.lblTotalAmoutPur.TabIndex = 152;
            this.lblTotalAmoutPur.Text = "0.00";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Verdana", 9F);
            this.label65.ForeColor = System.Drawing.Color.Black;
            this.label65.Location = new System.Drawing.Point(6, 563);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(89, 14);
            this.label65.TabIndex = 151;
            this.label65.Text = "Total Amount";
            // 
            // txtReturnAmtPur
            // 
            this.txtReturnAmtPur.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnAmtPur.Location = new System.Drawing.Point(655, 591);
            this.txtReturnAmtPur.Name = "txtReturnAmtPur";
            this.txtReturnAmtPur.Size = new System.Drawing.Size(68, 23);
            this.txtReturnAmtPur.TabIndex = 159;
            this.txtReturnAmtPur.Text = "0.00";
            this.txtReturnAmtPur.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReturnAmtPur_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(529, 595);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 14);
            this.label3.TabIndex = 158;
            this.label3.Text = "Return Amount";
            // 
            // lblTotalTaxPur
            // 
            this.lblTotalTaxPur.AutoSize = true;
            this.lblTotalTaxPur.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblTotalTaxPur.ForeColor = System.Drawing.Color.Black;
            this.lblTotalTaxPur.Location = new System.Drawing.Point(660, 563);
            this.lblTotalTaxPur.Name = "lblTotalTaxPur";
            this.lblTotalTaxPur.Size = new System.Drawing.Size(35, 14);
            this.lblTotalTaxPur.TabIndex = 157;
            this.lblTotalTaxPur.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(528, 563);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 14);
            this.label8.TabIndex = 156;
            this.label8.Text = "Total Tax/GST";
            // 
            // txtAmountPaidPur
            // 
            this.txtAmountPaidPur.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountPaidPur.Location = new System.Drawing.Point(1131, 559);
            this.txtAmountPaidPur.Name = "txtAmountPaidPur";
            this.txtAmountPaidPur.Size = new System.Drawing.Size(68, 23);
            this.txtAmountPaidPur.TabIndex = 163;
            this.txtAmountPaidPur.Text = "0.00";
            this.txtAmountPaidPur.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmountPaidPur_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(1002, 595);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 14);
            this.label9.TabIndex = 162;
            this.label9.Text = "Balance Amt";
            // 
            // lblBalanceAmtPur
            // 
            this.lblBalanceAmtPur.AutoSize = true;
            this.lblBalanceAmtPur.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblBalanceAmtPur.ForeColor = System.Drawing.Color.Black;
            this.lblBalanceAmtPur.Location = new System.Drawing.Point(1133, 595);
            this.lblBalanceAmtPur.Name = "lblBalanceAmtPur";
            this.lblBalanceAmtPur.Size = new System.Drawing.Size(35, 14);
            this.lblBalanceAmtPur.TabIndex = 161;
            this.lblBalanceAmtPur.Text = "0.00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(1001, 563);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 14);
            this.label12.TabIndex = 160;
            this.label12.Text = "Amount Paid";
            // 
            // sNo
            // 
            this.sNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sNo.HeaderText = "#";
            this.sNo.Name = "sNo";
            this.sNo.Width = 38;
            // 
            // cboItemName
            // 
            this.cboItemName.HeaderText = "Item Name";
            this.cboItemName.Name = "cboItemName";
            this.cboItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cboItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cboItemName.Width = 249;
            // 
            // itemType
            // 
            this.itemType.HeaderText = "Type";
            this.itemType.Name = "itemType";
            this.itemType.ReadOnly = true;
            this.itemType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.itemType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.itemType.Width = 70;
            // 
            // batchNo
            // 
            this.batchNo.HeaderText = "Batch No";
            this.batchNo.Name = "batchNo";
            this.batchNo.Width = 90;
            // 
            // expDate
            // 
            this.expDate.HeaderText = "Exp Date";
            this.expDate.MaxInputLength = 10;
            this.expDate.Name = "expDate";
            this.expDate.Width = 64;
            // 
            // packX
            // 
            this.packX.HeaderText = "Pack X";
            this.packX.Name = "packX";
            this.packX.Width = 40;
            // 
            // packY
            // 
            this.packY.HeaderText = "Pack Y";
            this.packY.Name = "packY";
            this.packY.Width = 40;
            // 
            // receiveQty
            // 
            this.receiveQty.HeaderText = "Rec Qty";
            this.receiveQty.Name = "receiveQty";
            this.receiveQty.Width = 50;
            // 
            // offerQty
            // 
            this.offerQty.HeaderText = "Off Qty";
            this.offerQty.Name = "offerQty";
            this.offerQty.Width = 40;
            // 
            // vendorPrice
            // 
            this.vendorPrice.HeaderText = "SP";
            this.vendorPrice.Name = "vendorPrice";
            this.vendorPrice.Width = 50;
            // 
            // mrp
            // 
            this.mrp.HeaderText = "MRP";
            this.mrp.Name = "mrp";
            this.mrp.Width = 50;
            // 
            // total
            // 
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.Width = 60;
            // 
            // disPerc
            // 
            this.disPerc.HeaderText = "Dis %";
            this.disPerc.Name = "disPerc";
            this.disPerc.Width = 30;
            // 
            // discountAmt
            // 
            this.discountAmt.HeaderText = "Dis";
            this.discountAmt.Name = "discountAmt";
            this.discountAmt.Width = 40;
            // 
            // taxPerc
            // 
            this.taxPerc.HeaderText = "Tax %";
            this.taxPerc.Name = "taxPerc";
            this.taxPerc.Width = 40;
            // 
            // taxAmt
            // 
            this.taxAmt.HeaderText = "Tax";
            this.taxAmt.Name = "taxAmt";
            this.taxAmt.Width = 40;
            // 
            // cboCalFreeTax
            // 
            this.cboCalFreeTax.HeaderText = "Cal Fr.Tax";
            this.cboCalFreeTax.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.cboCalFreeTax.Name = "cboCalFreeTax";
            this.cboCalFreeTax.Width = 50;
            // 
            // freeTax
            // 
            this.freeTax.HeaderText = "Fr.Tax";
            this.freeTax.Name = "freeTax";
            this.freeTax.Width = 40;
            // 
            // netAmt
            // 
            this.netAmt.HeaderText = "Net Amt";
            this.netAmt.Name = "netAmt";
            this.netAmt.Width = 70;
            // 
            // item_type_id
            // 
            this.item_type_id.HeaderText = "item_type_id";
            this.item_type_id.Name = "item_type_id";
            this.item_type_id.Visible = false;
            this.item_type_id.Width = 105;
            // 
            // item_id
            // 
            this.item_id.HeaderText = "item_id";
            this.item_id.Name = "item_id";
            this.item_id.Visible = false;
            // 
            // taxFormula
            // 
            this.taxFormula.HeaderText = "taxFormula";
            this.taxFormula.Name = "taxFormula";
            this.taxFormula.Visible = false;
            // 
            // del
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle5.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle5.NullValue")));
            this.del.DefaultCellStyle = dataGridViewCellStyle5;
            this.del.HeaderText = "Del";
            this.del.Image = ((System.Drawing.Image)(resources.GetObject("del.Image")));
            this.del.Name = "del";
            this.del.Width = 50;
            // 
            // CISPurchaseReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1230, 672);
            this.Controls.Add(this.txtAmountPaidPur);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblBalanceAmtPur);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtReturnAmtPur);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTotalTaxPur);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblDiscountAmtPur);
            this.Controls.Add(this.txtDiscountValuePur);
            this.Controls.Add(this.label66);
            this.Controls.Add(this.lblTotalAmoutPur);
            this.Controls.Add(this.label65);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvPurchaseReceipt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimizeBox = false;
            this.Name = "CISPurchaseReceipt";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CISPurchaseReceipt";
            this.Load += new System.EventHandler(this.CISPurchaseReceipt_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseReceipt)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpReceiveDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboVendor;
        private System.Windows.Forms.Label lblTinNo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DataGridView dgvPurchaseReceipt;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNetValuePur;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblDiscountAmtPur;
        private System.Windows.Forms.TextBox txtDiscountValuePur;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label lblTotalAmoutPur;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.TextBox txtReturnAmtPur;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalTaxPur;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAmountPaidPur;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBalanceAmtPur;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboTaxFormula;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel tslGRNNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn sNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cboItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn expDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn packX;
        private System.Windows.Forms.DataGridViewTextBoxColumn packY;
        private System.Windows.Forms.DataGridViewTextBoxColumn receiveQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn offerQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendorPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn mrp;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn disPerc;
        private System.Windows.Forms.DataGridViewTextBoxColumn discountAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn taxPerc;
        private System.Windows.Forms.DataGridViewTextBoxColumn taxAmt;
        private System.Windows.Forms.DataGridViewComboBoxColumn cboCalFreeTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn freeTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn netAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_type_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn taxFormula;
        private System.Windows.Forms.DataGridViewImageColumn del;
    }
}