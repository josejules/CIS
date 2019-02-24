namespace CIS.Reports
{
    partial class frmReportViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportViewer));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Daily Collection");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Daily Collecton Summary");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Due List");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Corporate Due List");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Collection    ", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Census Medical Dept By Gender");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Census Monthly VisitMode By Gender");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("OP Registation List");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("IP Admission List");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("IP Discharge List");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("IP Current List");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Registration     ", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Investigation List");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Investgation Share List");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Investigation", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Medicine List");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Current Stock Report");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Expiry Medicine List");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Purchase Medicine Report");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Refunded Medicine List");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Pharmacy    ", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20});
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboCorporate = new System.Windows.Forms.ComboBox();
            this.lblCorporate = new System.Windows.Forms.Label();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.btnShow = new System.Windows.Forms.Button();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tvReportList = new System.Windows.Forms.TreeView();
            this.rptCISViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1220, 662);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1220, 69);
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cboCorporate);
            this.groupBox1.Controls.Add(this.lblCorporate);
            this.groupBox1.Controls.Add(this.cboDepartment);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpDateTo);
            this.groupBox1.Controls.Add(this.btnShow);
            this.groupBox1.Controls.Add(this.dtpDateFrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1204, 62);
            this.groupBox1.TabIndex = 126;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // cboCorporate
            // 
            this.cboCorporate.FormattingEnabled = true;
            this.cboCorporate.Items.AddRange(new object[] {
            "All",
            "OP Registration",
            "IP Registration",
            "Investigation",
            "Pharmacy",
            "General",
            "Billing"});
            this.cboCorporate.Location = new System.Drawing.Point(868, 19);
            this.cboCorporate.Name = "cboCorporate";
            this.cboCorporate.Size = new System.Drawing.Size(198, 22);
            this.cboCorporate.TabIndex = 142;
            // 
            // lblCorporate
            // 
            this.lblCorporate.AutoSize = true;
            this.lblCorporate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorporate.ForeColor = System.Drawing.Color.Black;
            this.lblCorporate.Location = new System.Drawing.Point(763, 22);
            this.lblCorporate.Name = "lblCorporate";
            this.lblCorporate.Size = new System.Drawing.Size(81, 16);
            this.lblCorporate.TabIndex = 141;
            this.lblCorporate.Text = "Corporate";
            // 
            // cboDepartment
            // 
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Items.AddRange(new object[] {
            "All",
            "OP Registration",
            "IP Registration",
            "Investigation",
            "Pharmacy",
            "General",
            "IP Billing"});
            this.cboDepartment.Location = new System.Drawing.Point(545, 19);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(198, 22);
            this.cboDepartment.TabIndex = 140;
            this.cboDepartment.SelectedIndexChanged += new System.EventHandler(this.cboDepartment_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(444, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 126;
            this.label1.Text = "Department";
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "dd/MM/yyyy";
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpDateTo.Location = new System.Drawing.Point(294, 19);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(117, 22);
            this.dtpDateTo.TabIndex = 125;
            this.dtpDateTo.Value = new System.DateTime(2016, 10, 27, 9, 42, 3, 0);
            // 
            // btnShow
            // 
            this.btnShow.Image = ((System.Drawing.Image)(resources.GetObject("btnShow.Image")));
            this.btnShow.Location = new System.Drawing.Point(1111, 12);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(87, 36);
            this.btnShow.TabIndex = 0;
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpDateFrom.Location = new System.Drawing.Point(98, 19);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(121, 22);
            this.dtpDateFrom.TabIndex = 123;
            this.dtpDateFrom.Value = new System.DateTime(2016, 10, 27, 9, 42, 3, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(225, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 124;
            this.label3.Text = "Date To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 122;
            this.label2.Text = "Date From";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tvReportList);
            this.panel3.Controls.Add(this.rptCISViewer);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1220, 662);
            this.panel3.TabIndex = 1;
            // 
            // tvReportList
            // 
            this.tvReportList.BackColor = System.Drawing.Color.White;
            this.tvReportList.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvReportList.FullRowSelect = true;
            this.tvReportList.Location = new System.Drawing.Point(4, 66);
            this.tvReportList.Name = "tvReportList";
            treeNode1.Name = "DailyCollection";
            treeNode1.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode1.Text = "Daily Collection";
            treeNode2.Name = "DailyCollectionSummary";
            treeNode2.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode2.Text = "Daily Collecton Summary";
            treeNode3.Name = "DueList";
            treeNode3.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode3.Text = "Due List";
            treeNode4.Name = "CorporateDueList";
            treeNode4.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode4.Text = "Corporate Due List";
            treeNode5.Name = "Collection";
            treeNode5.NodeFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            treeNode5.Text = "Collection    ";
            treeNode6.Name = "childCensusMDeptGender";
            treeNode6.NodeFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode6.Text = "Census Medical Dept By Gender";
            treeNode7.Name = "CensusMonthVisitModeByGender";
            treeNode7.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode7.Text = "Census Monthly VisitMode By Gender";
            treeNode8.Name = "OPRegistationList";
            treeNode8.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode8.Text = "OP Registation List";
            treeNode9.Name = "IPAdmissionList";
            treeNode9.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode9.Text = "IP Admission List";
            treeNode10.Name = "IPDischargeList";
            treeNode10.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode10.Text = "IP Discharge List";
            treeNode11.Name = "IPCurrentList";
            treeNode11.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode11.Text = "IP Current List";
            treeNode12.ForeColor = System.Drawing.Color.Black;
            treeNode12.Name = "rootRegistration";
            treeNode12.NodeFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode12.Text = "Registration     ";
            treeNode13.Name = "InvestigationList";
            treeNode13.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode13.Text = "Investigation List";
            treeNode14.Name = "InvestigationShareList";
            treeNode14.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode14.Text = "Investgation Share List";
            treeNode15.Name = "Investigation";
            treeNode15.NodeFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            treeNode15.Text = "Investigation";
            treeNode16.Name = "medicineList";
            treeNode16.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode16.Text = "Medicine List";
            treeNode17.Name = "currentStockReport";
            treeNode17.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode17.Text = "Current Stock Report";
            treeNode18.Name = "expiryMedicineList";
            treeNode18.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode18.Text = "Expiry Medicine List";
            treeNode19.Name = "purchaseMedicineReport";
            treeNode19.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode19.Text = "Purchase Medicine Report";
            treeNode20.Name = "refundedMedicineList";
            treeNode20.NodeFont = new System.Drawing.Font("Verdana", 9.75F);
            treeNode20.Text = "Refunded Medicine List";
            treeNode21.Name = "Pharmacy";
            treeNode21.NodeFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            treeNode21.Text = "Pharmacy    ";
            this.tvReportList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode12,
            treeNode15,
            treeNode21});
            this.tvReportList.PathSeparator = "";
            this.tvReportList.Size = new System.Drawing.Size(295, 584);
            this.tvReportList.TabIndex = 1;
            this.tvReportList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvReportList_AfterSelect);
            // 
            // rptCISViewer
            // 
            this.rptCISViewer.Location = new System.Drawing.Point(305, 74);
            this.rptCISViewer.Name = "rptCISViewer";
            this.rptCISViewer.Size = new System.Drawing.Size(919, 584);
            this.rptCISViewer.TabIndex = 0;
            // 
            // frmReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 662);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Viewer";
            this.Load += new System.EventHandler(this.frmReportViewer_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnShow;
        private Microsoft.Reporting.WinForms.ReportViewer rptCISViewer;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tvReportList;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.ComboBox cboCorporate;
        private System.Windows.Forms.Label lblCorporate;

    }
}