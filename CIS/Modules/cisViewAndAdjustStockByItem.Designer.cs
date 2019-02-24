namespace CIS.Modules
{
    partial class cisViewAndAdjustStockByItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cisViewAndAdjustStockByItem));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tcInventoryManagement = new System.Windows.Forms.TabControl();
            this.tpAddOpeningStock = new System.Windows.Forms.TabPage();
            this.tpStockByItem = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cboItemPha = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.txtQtyPha = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.txtMRPPha = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.txtLotIdPha = new System.Windows.Forms.TextBox();
            this.txtExpDateMonth = new System.Windows.Forms.TextBox();
            this.txtExpDateYear = new System.Windows.Forms.TextBox();
            this.lblItemTypePha = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblTotalAvailQty = new System.Windows.Forms.Label();
            this.lblItemTypeId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTaxPerc = new System.Windows.Forms.Label();
            this.dgvInventoryManagement = new System.Windows.Forms.DataGridView();
            this.sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventory_stock_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_type_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lot_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exp_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expiry_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendor_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.def_discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_perc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avail_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.physical_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adj_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tcInventoryManagement.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryManagement)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(36, 9);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 41);
            this.btnSave.TabIndex = 9;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // tcInventoryManagement
            // 
            this.tcInventoryManagement.Controls.Add(this.tpAddOpeningStock);
            this.tcInventoryManagement.Controls.Add(this.tpStockByItem);
            this.tcInventoryManagement.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcInventoryManagement.Location = new System.Drawing.Point(4, 85);
            this.tcInventoryManagement.Name = "tcInventoryManagement";
            this.tcInventoryManagement.SelectedIndex = 0;
            this.tcInventoryManagement.Size = new System.Drawing.Size(930, 32);
            this.tcInventoryManagement.TabIndex = 43;
            this.tcInventoryManagement.SelectedIndexChanged += new System.EventHandler(this.tcInventoryManagement_SelectedIndexChanged);
            // 
            // tpAddOpeningStock
            // 
            this.tpAddOpeningStock.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpAddOpeningStock.Location = new System.Drawing.Point(4, 27);
            this.tpAddOpeningStock.Name = "tpAddOpeningStock";
            this.tpAddOpeningStock.Padding = new System.Windows.Forms.Padding(3);
            this.tpAddOpeningStock.Size = new System.Drawing.Size(922, 1);
            this.tpAddOpeningStock.TabIndex = 0;
            this.tpAddOpeningStock.Text = "Add Opening Stock";
            this.tpAddOpeningStock.UseVisualStyleBackColor = true;
            // 
            // tpStockByItem
            // 
            this.tpStockByItem.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpStockByItem.Location = new System.Drawing.Point(4, 27);
            this.tpStockByItem.Name = "tpStockByItem";
            this.tpStockByItem.Padding = new System.Windows.Forms.Padding(3);
            this.tpStockByItem.Size = new System.Drawing.Size(922, 1);
            this.tpStockByItem.TabIndex = 1;
            this.tpStockByItem.Text = "View Stock & Adjust by Item";
            this.tpStockByItem.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(37, 104);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 41);
            this.btnClose.TabIndex = 12;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(932, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 548);
            this.panel1.TabIndex = 37;
            // 
            // btnNew
            // 
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.Location = new System.Drawing.Point(37, 56);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(88, 41);
            this.btnNew.TabIndex = 13;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(834, 43);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(38, 38);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cboItemPha
            // 
            this.cboItemPha.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboItemPha.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboItemPha.DropDownHeight = 102;
            this.cboItemPha.DropDownWidth = 170;
            this.cboItemPha.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboItemPha.FormattingEnabled = true;
            this.cboItemPha.IntegralHeight = false;
            this.cboItemPha.Location = new System.Drawing.Point(70, 16);
            this.cboItemPha.Name = "cboItemPha";
            this.cboItemPha.Size = new System.Drawing.Size(317, 26);
            this.cboItemPha.TabIndex = 1;
            this.cboItemPha.SelectedIndexChanged += new System.EventHandler(this.cboItemPha_SelectedIndexChanged);
            this.cboItemPha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboItemPha_KeyDown);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.ForeColor = System.Drawing.Color.Black;
            this.label57.Location = new System.Drawing.Point(11, 18);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(37, 18);
            this.label57.TabIndex = 176;
            this.label57.Text = "Item";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.Black;
            this.label51.Location = new System.Drawing.Point(492, 19);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(30, 18);
            this.label51.TabIndex = 179;
            this.label51.Text = "Qty";
            // 
            // txtQtyPha
            // 
            this.txtQtyPha.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtyPha.Location = new System.Drawing.Point(542, 15);
            this.txtQtyPha.Name = "txtQtyPha";
            this.txtQtyPha.Size = new System.Drawing.Size(44, 26);
            this.txtQtyPha.TabIndex = 5;
            this.txtQtyPha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQtyPha_KeyDown_1);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.ForeColor = System.Drawing.Color.Black;
            this.label76.Location = new System.Drawing.Point(11, 56);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(42, 18);
            this.label76.TabIndex = 186;
            this.label76.Text = "Lot Id";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.ForeColor = System.Drawing.Color.Black;
            this.label72.Location = new System.Drawing.Point(210, 56);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(62, 18);
            this.label72.TabIndex = 187;
            this.label72.Text = "Exp Date";
            // 
            // txtMRPPha
            // 
            this.txtMRPPha.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMRPPha.Location = new System.Drawing.Point(542, 53);
            this.txtMRPPha.Name = "txtMRPPha";
            this.txtMRPPha.Size = new System.Drawing.Size(53, 26);
            this.txtMRPPha.TabIndex = 6;
            this.txtMRPPha.Text = "0.00";
            this.txtMRPPha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMRPPha_KeyDown);
            this.txtMRPPha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMRPPha_KeyPress);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.Black;
            this.label48.Location = new System.Drawing.Point(492, 56);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(36, 18);
            this.label48.TabIndex = 188;
            this.label48.Text = "MRP";
            // 
            // txtLotIdPha
            // 
            this.txtLotIdPha.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotIdPha.Location = new System.Drawing.Point(70, 53);
            this.txtLotIdPha.Name = "txtLotIdPha";
            this.txtLotIdPha.Size = new System.Drawing.Size(112, 26);
            this.txtLotIdPha.TabIndex = 2;
            this.txtLotIdPha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLotIdPha_KeyDown);
            this.txtLotIdPha.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtLotIdPha_PreviewKeyDown);
            // 
            // txtExpDateMonth
            // 
            this.txtExpDateMonth.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpDateMonth.Location = new System.Drawing.Point(275, 52);
            this.txtExpDateMonth.Name = "txtExpDateMonth";
            this.txtExpDateMonth.Size = new System.Drawing.Size(40, 26);
            this.txtExpDateMonth.TabIndex = 3;
            this.txtExpDateMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpDateMonth_KeyDown);
            this.txtExpDateMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExpDateMonth_KeyPress);
            this.txtExpDateMonth.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtExpDateMonth_PreviewKeyDown);
            // 
            // txtExpDateYear
            // 
            this.txtExpDateYear.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpDateYear.Location = new System.Drawing.Point(343, 52);
            this.txtExpDateYear.Name = "txtExpDateYear";
            this.txtExpDateYear.Size = new System.Drawing.Size(62, 26);
            this.txtExpDateYear.TabIndex = 4;
            this.txtExpDateYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpDateYear_KeyDown);
            this.txtExpDateYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExpDateYear_KeyPress);
            this.txtExpDateYear.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtExpDateYear_PreviewKeyDown);
            // 
            // lblItemTypePha
            // 
            this.lblItemTypePha.AutoSize = true;
            this.lblItemTypePha.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemTypePha.ForeColor = System.Drawing.Color.Black;
            this.lblItemTypePha.Location = new System.Drawing.Point(398, 19);
            this.lblItemTypePha.Name = "lblItemTypePha";
            this.lblItemTypePha.Size = new System.Drawing.Size(0, 18);
            this.lblItemTypePha.TabIndex = 193;
            // 
            // btnRemove
            // 
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(879, 43);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(38, 38);
            this.btnRemove.TabIndex = 194;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblTotalAvailQty
            // 
            this.lblTotalAvailQty.AutoSize = true;
            this.lblTotalAvailQty.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAvailQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalAvailQty.Location = new System.Drawing.Point(592, 19);
            this.lblTotalAvailQty.Name = "lblTotalAvailQty";
            this.lblTotalAvailQty.Size = new System.Drawing.Size(15, 18);
            this.lblTotalAvailQty.TabIndex = 195;
            this.lblTotalAvailQty.Text = "0";
            // 
            // lblItemTypeId
            // 
            this.lblItemTypeId.AutoSize = true;
            this.lblItemTypeId.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemTypeId.ForeColor = System.Drawing.Color.Black;
            this.lblItemTypeId.Location = new System.Drawing.Point(405, 56);
            this.lblItemTypeId.Name = "lblItemTypeId";
            this.lblItemTypeId.Size = new System.Drawing.Size(0, 18);
            this.lblItemTypeId.TabIndex = 196;
            this.lblItemTypeId.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(629, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 18);
            this.label1.TabIndex = 197;
            this.label1.Text = "Tax %";
            // 
            // lblTaxPerc
            // 
            this.lblTaxPerc.AutoSize = true;
            this.lblTaxPerc.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxPerc.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTaxPerc.Location = new System.Drawing.Point(703, 57);
            this.lblTaxPerc.Name = "lblTaxPerc";
            this.lblTaxPerc.Size = new System.Drawing.Size(0, 18);
            this.lblTaxPerc.TabIndex = 198;
            // 
            // dgvInventoryManagement
            // 
            this.dgvInventoryManagement.AllowUserToOrderColumns = true;
            this.dgvInventoryManagement.AllowUserToResizeColumns = false;
            this.dgvInventoryManagement.AllowUserToResizeRows = false;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.dgvInventoryManagement.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvInventoryManagement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInventoryManagement.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvInventoryManagement.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvInventoryManagement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvInventoryManagement.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInventoryManagement.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgvInventoryManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventoryManagement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sno,
            this.inventory_stock_id,
            this.item_type_id,
            this.item_type,
            this.item_id,
            this.item_name,
            this.lot_id,
            this.exp_date,
            this.expiry_date,
            this.vendor_price,
            this.mrp,
            this.def_discount,
            this.tax_perc,
            this.avail_qty,
            this.physical_qty,
            this.adj_qty});
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInventoryManagement.DefaultCellStyle = dataGridViewCellStyle24;
            this.dgvInventoryManagement.Location = new System.Drawing.Point(5, 114);
            this.dgvInventoryManagement.Name = "dgvInventoryManagement";
            this.dgvInventoryManagement.RowHeadersVisible = false;
            this.dgvInventoryManagement.Size = new System.Drawing.Size(925, 458);
            this.dgvInventoryManagement.TabIndex = 199;
            this.dgvInventoryManagement.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventoryManagement_CellValueChanged);
            this.dgvInventoryManagement.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvInventoryManagement_EditingControlShowing);
            // 
            // sno
            // 
            this.sno.FillWeight = 42.54721F;
            this.sno.HeaderText = "#";
            this.sno.Name = "sno";
            // 
            // inventory_stock_id
            // 
            this.inventory_stock_id.HeaderText = "inventory_stock_id";
            this.inventory_stock_id.Name = "inventory_stock_id";
            this.inventory_stock_id.Visible = false;
            // 
            // item_type_id
            // 
            this.item_type_id.HeaderText = "item_type_id";
            this.item_type_id.Name = "item_type_id";
            this.item_type_id.Visible = false;
            // 
            // item_type
            // 
            this.item_type.FillWeight = 91.37979F;
            this.item_type.HeaderText = "Item Type";
            this.item_type.Name = "item_type";
            // 
            // item_id
            // 
            this.item_id.HeaderText = "item_id";
            this.item_id.Name = "item_id";
            this.item_id.Visible = false;
            // 
            // item_name
            // 
            this.item_name.FillWeight = 243.6548F;
            this.item_name.HeaderText = "Item Name";
            this.item_name.Name = "item_name";
            // 
            // lot_id
            // 
            this.lot_id.FillWeight = 91.37979F;
            this.lot_id.HeaderText = "Lot Id";
            this.lot_id.Name = "lot_id";
            // 
            // exp_date
            // 
            this.exp_date.FillWeight = 91.37979F;
            this.exp_date.HeaderText = "Exp Date";
            this.exp_date.Name = "exp_date";
            // 
            // expiry_date
            // 
            this.expiry_date.HeaderText = "expiry_date";
            this.expiry_date.Name = "expiry_date";
            this.expiry_date.Visible = false;
            // 
            // vendor_price
            // 
            this.vendor_price.FillWeight = 91.37979F;
            this.vendor_price.HeaderText = "Vendor Price";
            this.vendor_price.Name = "vendor_price";
            // 
            // mrp
            // 
            this.mrp.FillWeight = 91.37979F;
            this.mrp.HeaderText = "MRP";
            this.mrp.Name = "mrp";
            // 
            // def_discount
            // 
            this.def_discount.FillWeight = 91.37979F;
            this.def_discount.HeaderText = "Def.Discount";
            this.def_discount.Name = "def_discount";
            // 
            // tax_perc
            // 
            this.tax_perc.FillWeight = 91.37979F;
            this.tax_perc.HeaderText = "Tax Perc";
            this.tax_perc.Name = "tax_perc";
            // 
            // avail_qty
            // 
            this.avail_qty.FillWeight = 91.37979F;
            this.avail_qty.HeaderText = "Avail Qty";
            this.avail_qty.Name = "avail_qty";
            // 
            // physical_qty
            // 
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.LightBlue;
            this.physical_qty.DefaultCellStyle = dataGridViewCellStyle23;
            this.physical_qty.FillWeight = 91.37979F;
            this.physical_qty.HeaderText = "Physical Qty";
            this.physical_qty.Name = "physical_qty";
            // 
            // adj_qty
            // 
            this.adj_qty.FillWeight = 91.37979F;
            this.adj_qty.HeaderText = "Adj Qty";
            this.adj_qty.Name = "adj_qty";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(318, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 18);
            this.label2.TabIndex = 200;
            this.label2.Text = "M";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(411, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 18);
            this.label3.TabIndex = 201;
            this.label3.Text = "Y";
            // 
            // cisViewAndAdjustStockByItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 576);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvInventoryManagement);
            this.Controls.Add(this.lblTaxPerc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblItemTypeId);
            this.Controls.Add(this.lblTotalAvailQty);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.lblItemTypePha);
            this.Controls.Add(this.txtExpDateYear);
            this.Controls.Add(this.txtExpDateMonth);
            this.Controls.Add(this.txtLotIdPha);
            this.Controls.Add(this.txtMRPPha);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.label72);
            this.Controls.Add(this.label76);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.txtQtyPha);
            this.Controls.Add(this.cboItemPha);
            this.Controls.Add(this.label57);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tcInventoryManagement);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "cisViewAndAdjustStockByItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View & Adjust Stock By Item";
            this.Load += new System.EventHandler(this.cisViewAndAdjustStockByItem_Load);
            this.tcInventoryManagement.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryManagement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tcInventoryManagement;
        private System.Windows.Forms.TabPage tpAddOpeningStock;
        private System.Windows.Forms.TabPage tpStockByItem;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cboItemPha;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox txtQtyPha;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txtMRPPha;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox txtLotIdPha;
        private System.Windows.Forms.TextBox txtExpDateMonth;
        private System.Windows.Forms.TextBox txtExpDateYear;
        private System.Windows.Forms.Label lblItemTypePha;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblTotalAvailQty;
        private System.Windows.Forms.Label lblItemTypeId;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTaxPerc;
        private System.Windows.Forms.DataGridView dgvInventoryManagement;
        private System.Windows.Forms.DataGridViewTextBoxColumn sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn inventory_stock_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_type_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn lot_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn exp_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn expiry_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendor_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn mrp;
        private System.Windows.Forms.DataGridViewTextBoxColumn def_discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_perc;
        private System.Windows.Forms.DataGridViewTextBoxColumn avail_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn physical_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn adj_qty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}