namespace CIS.Master
{
    partial class cisInvestigationMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cisInvestigationMaster));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tcInvestigationMaster = new System.Windows.Forms.TabControl();
            this.tpInvCategory = new System.Windows.Forms.TabPage();
            this.tpInvList = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.dgvInvestigationMaster = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tcInvestigationMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvestigationMaster)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            // tcInvestigationMaster
            // 
            this.tcInvestigationMaster.Controls.Add(this.tpInvCategory);
            this.tcInvestigationMaster.Controls.Add(this.tpInvList);
            this.tcInvestigationMaster.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcInvestigationMaster.Location = new System.Drawing.Point(12, 12);
            this.tcInvestigationMaster.Name = "tcInvestigationMaster";
            this.tcInvestigationMaster.SelectedIndex = 0;
            this.tcInvestigationMaster.Size = new System.Drawing.Size(930, 28);
            this.tcInvestigationMaster.TabIndex = 36;
            this.tcInvestigationMaster.SelectedIndexChanged += new System.EventHandler(this.tcInvestigationMaster_SelectedIndexChanged);
            // 
            // tpInvCategory
            // 
            this.tpInvCategory.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpInvCategory.Location = new System.Drawing.Point(4, 27);
            this.tpInvCategory.Name = "tpInvCategory";
            this.tpInvCategory.Padding = new System.Windows.Forms.Padding(3);
            this.tpInvCategory.Size = new System.Drawing.Size(922, 0);
            this.tpInvCategory.TabIndex = 0;
            this.tpInvCategory.Text = "Investigation Category";
            this.tpInvCategory.UseVisualStyleBackColor = true;
            // 
            // tpInvList
            // 
            this.tpInvList.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpInvList.Location = new System.Drawing.Point(4, 27);
            this.tpInvList.Name = "tpInvList";
            this.tpInvList.Padding = new System.Windows.Forms.Padding(3);
            this.tpInvList.Size = new System.Drawing.Size(922, 0);
            this.tpInvList.TabIndex = 1;
            this.tpInvList.Text = "Investigation List";
            this.tpInvList.UseVisualStyleBackColor = true;
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
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(70, 547);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(191, 24);
            this.cboFilter.TabIndex = 33;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(270, 550);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(169, 20);
            this.txtSearch.TabIndex = 34;
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
            // dgvInvestigationMaster
            // 
            this.dgvInvestigationMaster.AllowUserToAddRows = false;
            this.dgvInvestigationMaster.AllowUserToDeleteRows = false;
            this.dgvInvestigationMaster.AllowUserToOrderColumns = true;
            this.dgvInvestigationMaster.AllowUserToResizeColumns = false;
            this.dgvInvestigationMaster.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInvestigationMaster.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvInvestigationMaster.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvestigationMaster.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvInvestigationMaster.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvInvestigationMaster.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInvestigationMaster.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInvestigationMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvInvestigationMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInvestigationMaster.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvInvestigationMaster.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvInvestigationMaster.Location = new System.Drawing.Point(14, 38);
            this.dgvInvestigationMaster.MultiSelect = false;
            this.dgvInvestigationMaster.Name = "dgvInvestigationMaster";
            this.dgvInvestigationMaster.ReadOnly = true;
            this.dgvInvestigationMaster.RowHeadersVisible = false;
            this.dgvInvestigationMaster.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.dgvInvestigationMaster.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvInvestigationMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvInvestigationMaster.RowTemplate.ReadOnly = true;
            this.dgvInvestigationMaster.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInvestigationMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvestigationMaster.Size = new System.Drawing.Size(928, 483);
            this.dgvInvestigationMaster.TabIndex = 31;
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
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.ForeColor = System.Drawing.Color.Black;
            this.lblFilter.Location = new System.Drawing.Point(18, 552);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(46, 16);
            this.lblFilter.TabIndex = 32;
            this.lblFilter.Text = "Filter";
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.ForeColor = System.Drawing.Color.Black;
            this.btnGo.Image = ((System.Drawing.Image)(resources.GetObject("btnGo.Image")));
            this.btnGo.Location = new System.Drawing.Point(445, 543);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(56, 32);
            this.btnGo.TabIndex = 35;
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(940, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 548);
            this.panel1.TabIndex = 30;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRefresh.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Image = global::CIS.Properties.Resources.Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(54, 213);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(51, 42);
            this.btnRefresh.TabIndex = 125;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cisInvestigationMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 576);
            this.Controls.Add(this.tcInvestigationMaster);
            this.Controls.Add(this.cboFilter);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvInvestigationMaster);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "cisInvestigationMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Investigation Master";
            this.Load += new System.EventHandler(this.cisInvestigationMaster_Load);
            this.tcInvestigationMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvestigationMaster)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TabControl tcInvestigationMaster;
        private System.Windows.Forms.TabPage tpInvCategory;
        private System.Windows.Forms.TabPage tpInvList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.DataGridView dgvInvestigationMaster;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
    }
}