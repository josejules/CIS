namespace CIS.Modules
{
    partial class cisViewInventoryMovements
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cisViewInventoryMovements));
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvViewInventoryMovements = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cboInvestoryMoveType = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnGoDate = new System.Windows.Forms.Button();
            this.btnGoFilter = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewInventoryMovements)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpDateTo.Location = new System.Drawing.Point(628, 21);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(118, 22);
            this.dtpDateTo.TabIndex = 121;
            this.dtpDateTo.Value = new System.DateTime(2018, 4, 27, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(559, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 120;
            this.label3.Text = "To Date";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpDateFrom.Location = new System.Drawing.Point(409, 20);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(121, 22);
            this.dtpDateFrom.TabIndex = 119;
            this.dtpDateFrom.Value = new System.DateTime(2018, 4, 27, 23, 55, 6, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(319, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 33;
            this.label2.Text = "From Date";
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Items.AddRange(new object[] {
            "IM Number",
            "Department Name",
            "Vendor Name"});
            this.cboFilter.Location = new System.Drawing.Point(113, 57);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(191, 24);
            this.cboFilter.TabIndex = 30;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(322, 58);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(169, 22);
            this.txtSearch.TabIndex = 31;
            // 
            // dgvViewInventoryMovements
            // 
            this.dgvViewInventoryMovements.AllowUserToAddRows = false;
            this.dgvViewInventoryMovements.AllowUserToDeleteRows = false;
            this.dgvViewInventoryMovements.AllowUserToOrderColumns = true;
            this.dgvViewInventoryMovements.AllowUserToResizeColumns = false;
            this.dgvViewInventoryMovements.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvViewInventoryMovements.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvViewInventoryMovements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvViewInventoryMovements.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvViewInventoryMovements.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvViewInventoryMovements.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvViewInventoryMovements.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvViewInventoryMovements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvViewInventoryMovements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvViewInventoryMovements.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvViewInventoryMovements.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvViewInventoryMovements.Location = new System.Drawing.Point(3, 98);
            this.dgvViewInventoryMovements.MultiSelect = false;
            this.dgvViewInventoryMovements.Name = "dgvViewInventoryMovements";
            this.dgvViewInventoryMovements.ReadOnly = true;
            this.dgvViewInventoryMovements.RowHeadersVisible = false;
            this.dgvViewInventoryMovements.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgvViewInventoryMovements.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvViewInventoryMovements.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvViewInventoryMovements.RowTemplate.ReadOnly = true;
            this.dgvViewInventoryMovements.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvViewInventoryMovements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvViewInventoryMovements.Size = new System.Drawing.Size(1157, 476);
            this.dgvViewInventoryMovements.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(11, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 29;
            this.label1.Text = "Filter";
            // 
            // cboInvestoryMoveType
            // 
            this.cboInvestoryMoveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInvestoryMoveType.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboInvestoryMoveType.FormattingEnabled = true;
            this.cboInvestoryMoveType.Items.AddRange(new object[] {
            "All",
            "Internal Movements",
            "Return to Vendor",
            "Consume Items"});
            this.cboInvestoryMoveType.Location = new System.Drawing.Point(113, 20);
            this.cboInvestoryMoveType.Name = "cboInvestoryMoveType";
            this.cboInvestoryMoveType.Size = new System.Drawing.Size(191, 24);
            this.cboInvestoryMoveType.TabIndex = 28;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.ForeColor = System.Drawing.Color.Transparent;
            this.lblFilter.Location = new System.Drawing.Point(11, 23);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(43, 16);
            this.lblFilter.TabIndex = 27;
            this.lblFilter.Text = "Type";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.btnGoDate);
            this.groupBox1.Controls.Add(this.dtpDateTo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpDateFrom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboFilter);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnGoFilter);
            this.groupBox1.Controls.Add(this.cboInvestoryMoveType);
            this.groupBox1.Controls.Add(this.lblFilter);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1157, 89);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRefresh.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Image = global::CIS.Properties.Resources.Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(559, 43);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(51, 42);
            this.btnRefresh.TabIndex = 123;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnGoDate
            // 
            this.btnGoDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoDate.ForeColor = System.Drawing.Color.Black;
            this.btnGoDate.Image = ((System.Drawing.Image)(resources.GetObject("btnGoDate.Image")));
            this.btnGoDate.Location = new System.Drawing.Point(759, 15);
            this.btnGoDate.Name = "btnGoDate";
            this.btnGoDate.Size = new System.Drawing.Size(56, 32);
            this.btnGoDate.TabIndex = 122;
            this.btnGoDate.UseVisualStyleBackColor = true;
            this.btnGoDate.Click += new System.EventHandler(this.btnGoDate_Click);
            // 
            // btnGoFilter
            // 
            this.btnGoFilter.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoFilter.ForeColor = System.Drawing.Color.Black;
            this.btnGoFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnGoFilter.Image")));
            this.btnGoFilter.Location = new System.Drawing.Point(497, 53);
            this.btnGoFilter.Name = "btnGoFilter";
            this.btnGoFilter.Size = new System.Drawing.Size(56, 32);
            this.btnGoFilter.TabIndex = 32;
            this.btnGoFilter.UseVisualStyleBackColor = true;
            this.btnGoFilter.Click += new System.EventHandler(this.btnGoFilter_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(1059, 32);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 37);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Tag = "Alt + A";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cisViewInventoryMovements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 576);
            this.Controls.Add(this.dgvViewInventoryMovements);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "cisViewInventoryMovements";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "cisViewInvestoryMovements";
            this.Load += new System.EventHandler(this.cisViewInvestoryMovements_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewInventoryMovements)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnGoDate;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvViewInventoryMovements;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoFilter;
        private System.Windows.Forms.ComboBox cboInvestoryMoveType;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
    }
}