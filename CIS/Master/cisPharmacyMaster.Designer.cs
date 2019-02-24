namespace CIS.Master
{
    partial class cisPharmacyMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cisPharmacyMaster));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.dgvPharmacyMaster = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tcPharmacyMaster = new System.Windows.Forms.TabControl();
            this.tpItemType = new System.Windows.Forms.TabPage();
            this.tpItem = new System.Windows.Forms.TabPage();
            this.tpVendor = new System.Windows.Forms.TabPage();
            this.tpTax = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPharmacyMaster)).BeginInit();
            this.panel1.SuspendLayout();
            this.tcPharmacyMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(61, 542);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(191, 24);
            this.cboFilter.TabIndex = 26;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(261, 545);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(169, 20);
            this.txtSearch.TabIndex = 27;
            // 
            // btnEdit
            // 
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(17, 62);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(132, 41);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // dgvPharmacyMaster
            // 
            this.dgvPharmacyMaster.AllowUserToAddRows = false;
            this.dgvPharmacyMaster.AllowUserToDeleteRows = false;
            this.dgvPharmacyMaster.AllowUserToOrderColumns = true;
            this.dgvPharmacyMaster.AllowUserToResizeColumns = false;
            this.dgvPharmacyMaster.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPharmacyMaster.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPharmacyMaster.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPharmacyMaster.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPharmacyMaster.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPharmacyMaster.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPharmacyMaster.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPharmacyMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPharmacyMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPharmacyMaster.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPharmacyMaster.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPharmacyMaster.Location = new System.Drawing.Point(5, 33);
            this.dgvPharmacyMaster.MultiSelect = false;
            this.dgvPharmacyMaster.Name = "dgvPharmacyMaster";
            this.dgvPharmacyMaster.ReadOnly = true;
            this.dgvPharmacyMaster.RowHeadersVisible = false;
            this.dgvPharmacyMaster.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgvPharmacyMaster.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPharmacyMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvPharmacyMaster.RowTemplate.ReadOnly = true;
            this.dgvPharmacyMaster.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPharmacyMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPharmacyMaster.Size = new System.Drawing.Size(928, 483);
            this.dgvPharmacyMaster.TabIndex = 24;
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
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.ForeColor = System.Drawing.Color.Black;
            this.lblFilter.Location = new System.Drawing.Point(9, 547);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(46, 16);
            this.lblFilter.TabIndex = 25;
            this.lblFilter.Text = "Filter";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(17, 9);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(132, 41);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.ForeColor = System.Drawing.Color.Black;
            this.btnGo.Image = ((System.Drawing.Image)(resources.GetObject("btnGo.Image")));
            this.btnGo.Location = new System.Drawing.Point(436, 538);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(56, 32);
            this.btnGo.TabIndex = 28;
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(17, 166);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(132, 41);
            this.btnClose.TabIndex = 12;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(17, 114);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(132, 41);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(931, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 548);
            this.panel1.TabIndex = 23;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRefresh.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Image = global::CIS.Properties.Resources.Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(50, 213);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(51, 42);
            this.btnRefresh.TabIndex = 125;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tcPharmacyMaster
            // 
            this.tcPharmacyMaster.Controls.Add(this.tpItemType);
            this.tcPharmacyMaster.Controls.Add(this.tpItem);
            this.tcPharmacyMaster.Controls.Add(this.tpVendor);
            this.tcPharmacyMaster.Controls.Add(this.tpTax);
            this.tcPharmacyMaster.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcPharmacyMaster.Location = new System.Drawing.Point(3, 7);
            this.tcPharmacyMaster.Name = "tcPharmacyMaster";
            this.tcPharmacyMaster.SelectedIndex = 0;
            this.tcPharmacyMaster.Size = new System.Drawing.Size(930, 28);
            this.tcPharmacyMaster.TabIndex = 29;
            this.tcPharmacyMaster.SelectedIndexChanged += new System.EventHandler(this.tcPharmacyMaster_SelectedIndexChanged);
            // 
            // tpItemType
            // 
            this.tpItemType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpItemType.Location = new System.Drawing.Point(4, 27);
            this.tpItemType.Name = "tpItemType";
            this.tpItemType.Padding = new System.Windows.Forms.Padding(3);
            this.tpItemType.Size = new System.Drawing.Size(922, 0);
            this.tpItemType.TabIndex = 0;
            this.tpItemType.Text = "Item Type";
            this.tpItemType.UseVisualStyleBackColor = true;
            // 
            // tpItem
            // 
            this.tpItem.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpItem.Location = new System.Drawing.Point(4, 27);
            this.tpItem.Name = "tpItem";
            this.tpItem.Padding = new System.Windows.Forms.Padding(3);
            this.tpItem.Size = new System.Drawing.Size(922, 0);
            this.tpItem.TabIndex = 1;
            this.tpItem.Text = "Item";
            this.tpItem.UseVisualStyleBackColor = true;
            // 
            // tpVendor
            // 
            this.tpVendor.Location = new System.Drawing.Point(4, 27);
            this.tpVendor.Name = "tpVendor";
            this.tpVendor.Size = new System.Drawing.Size(922, 0);
            this.tpVendor.TabIndex = 2;
            this.tpVendor.Text = "Supplier";
            this.tpVendor.UseVisualStyleBackColor = true;
            // 
            // tpTax
            // 
            this.tpTax.Location = new System.Drawing.Point(4, 27);
            this.tpTax.Name = "tpTax";
            this.tpTax.Size = new System.Drawing.Size(922, 0);
            this.tpTax.TabIndex = 3;
            this.tpTax.Text = "Tax";
            this.tpTax.UseVisualStyleBackColor = true;
            // 
            // cisPharmacyMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 576);
            this.Controls.Add(this.tcPharmacyMaster);
            this.Controls.Add(this.cboFilter);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvPharmacyMaster);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "cisPharmacyMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pharmacy Master";
            this.Load += new System.EventHandler(this.cisPharmacyMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPharmacyMaster)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tcPharmacyMaster.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.DataGridView dgvPharmacyMaster;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tcPharmacyMaster;
        private System.Windows.Forms.TabPage tpItemType;
        private System.Windows.Forms.TabPage tpItem;
        private System.Windows.Forms.TabPage tpVendor;
        private System.Windows.Forms.TabPage tpTax;
        private System.Windows.Forms.Button btnRefresh;
    }
}