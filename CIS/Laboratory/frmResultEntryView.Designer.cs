namespace CIS.Laboratory
{
    partial class frmResultEntryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResultEntryView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvResultEntryView = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtVisitNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnLookup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRegNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBillNo = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnResultEntry = new System.Windows.Forms.Button();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSex = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultEntryView)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(870, 418);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvResultEntryView);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(870, 318);
            this.panel3.TabIndex = 1;
            // 
            // dgvResultEntryView
            // 
            this.dgvResultEntryView.AllowUserToAddRows = false;
            this.dgvResultEntryView.AllowUserToDeleteRows = false;
            this.dgvResultEntryView.AllowUserToResizeColumns = false;
            this.dgvResultEntryView.AllowUserToResizeRows = false;
            this.dgvResultEntryView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultEntryView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvResultEntryView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultEntryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultEntryView.Location = new System.Drawing.Point(0, 0);
            this.dgvResultEntryView.Name = "dgvResultEntryView";
            this.dgvResultEntryView.ReadOnly = true;
            this.dgvResultEntryView.RowHeadersVisible = false;
            this.dgvResultEntryView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResultEntryView.Size = new System.Drawing.Size(870, 318);
            this.dgvResultEntryView.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.dtpBillDate);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnResultEntry);
            this.panel2.Controls.Add(this.txtAge);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtSex);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(870, 100);
            this.panel2.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtVisitNo);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.txtPatientName);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(247, 24);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(257, 74);
            this.panel6.TabIndex = 21;
            // 
            // txtVisitNo
            // 
            this.txtVisitNo.Enabled = false;
            this.txtVisitNo.Location = new System.Drawing.Point(91, 4);
            this.txtVisitNo.Name = "txtVisitNo";
            this.txtVisitNo.Size = new System.Drawing.Size(150, 20);
            this.txtVisitNo.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Visit No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Patient Name";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Enabled = false;
            this.txtPatientName.Location = new System.Drawing.Point(91, 38);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(150, 20);
            this.txtPatientName.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnLookup);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.txtRegNo);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.cboBillNo);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 24);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(247, 74);
            this.panel5.TabIndex = 20;
            // 
            // btnLookup
            // 
            this.btnLookup.Image = ((System.Drawing.Image)(resources.GetObject("btnLookup.Image")));
            this.btnLookup.Location = new System.Drawing.Point(206, 6);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Size = new System.Drawing.Size(28, 24);
            this.btnLookup.TabIndex = 18;
            this.btnLookup.UseVisualStyleBackColor = true;
            this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Patient Id";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRegNo
            // 
            this.txtRegNo.Location = new System.Drawing.Point(74, 8);
            this.txtRegNo.Name = "txtRegNo";
            this.txtRegNo.Size = new System.Drawing.Size(127, 20);
            this.txtRegNo.TabIndex = 2;
            this.txtRegNo.Enter += new System.EventHandler(this.txtRegNo_Enter);
            this.txtRegNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRegNo_KeyDown);
            this.txtRegNo.Leave += new System.EventHandler(this.txtRegNo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bill No";
            // 
            // cboBillNo
            // 
            this.cboBillNo.FormattingEnabled = true;
            this.cboBillNo.Location = new System.Drawing.Point(74, 41);
            this.cboBillNo.Name = "cboBillNo";
            this.cboBillNo.Size = new System.Drawing.Size(160, 21);
            this.cboBillNo.TabIndex = 9;
            this.cboBillNo.Enter += new System.EventHandler(this.cboBillNo_Enter);
            this.cboBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboBillNo_KeyDown);
            this.cboBillNo.Leave += new System.EventHandler(this.cboBillNo_Leave);
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.Controls.Add(this.label8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(868, 24);
            this.panel4.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(11, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Patient Details";
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBillDate.Location = new System.Drawing.Point(690, 31);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(149, 20);
            this.dtpBillDate.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(637, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Bill Date";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(764, 61);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnResultEntry
            // 
            this.btnResultEntry.Location = new System.Drawing.Point(659, 61);
            this.btnResultEntry.Name = "btnResultEntry";
            this.btnResultEntry.Size = new System.Drawing.Size(98, 23);
            this.btnResultEntry.TabIndex = 14;
            this.btnResultEntry.Text = "Result Entry";
            this.btnResultEntry.UseVisualStyleBackColor = true;
            this.btnResultEntry.Click += new System.EventHandler(this.btnResultEntry_Click);
            // 
            // txtAge
            // 
            this.txtAge.Enabled = false;
            this.txtAge.Location = new System.Drawing.Point(553, 62);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(73, 20);
            this.txtAge.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(522, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Age";
            // 
            // txtSex
            // 
            this.txtSex.Enabled = false;
            this.txtSex.Location = new System.Drawing.Point(553, 28);
            this.txtSex.Name = "txtSex";
            this.txtSex.Size = new System.Drawing.Size(73, 20);
            this.txtSex.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(522, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Sex";
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(868, 98);
            this.panel7.TabIndex = 22;
            // 
            // frmResultEntryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 418);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResultEntryView";
            this.Text = "Result Entry View";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultEntryView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cboBillNo;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnResultEntry;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvResultEntryView;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLookup;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSex;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVisitNo;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtRegNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
    }
}