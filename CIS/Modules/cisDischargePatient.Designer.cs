namespace CIS.Modules
{
    partial class cisDischargePatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cisDischargePatient));
            this.txtPatientId = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lblVisitNo = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.lblBedDetails = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.cboDischargeType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpDischargeDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPatientId
            // 
            this.txtPatientId.Location = new System.Drawing.Point(148, 12);
            this.txtPatientId.Name = "txtPatientId";
            this.txtPatientId.Size = new System.Drawing.Size(166, 20);
            this.txtPatientId.TabIndex = 136;
            this.txtPatientId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientId_KeyDown);
            this.txtPatientId.Leave += new System.EventHandler(this.txtPatientId_Leave);
            this.txtPatientId.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtPatientId_PreviewKeyDown);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.Black;
            this.label35.Location = new System.Drawing.Point(10, 15);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(69, 14);
            this.label35.TabIndex = 137;
            this.label35.Text = "Patient Id";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.Black;
            this.label41.Location = new System.Drawing.Point(10, 50);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(92, 14);
            this.label41.TabIndex = 139;
            this.label41.Text = "Patient Name";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.ForeColor = System.Drawing.Color.Black;
            this.lblPatientName.Location = new System.Drawing.Point(148, 50);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(0, 14);
            this.lblPatientName.TabIndex = 140;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.Black;
            this.label47.Location = new System.Drawing.Point(10, 84);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(86, 14);
            this.label47.TabIndex = 154;
            this.label47.Text = "Visit Number";
            // 
            // lblVisitNo
            // 
            this.lblVisitNo.AutoSize = true;
            this.lblVisitNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisitNo.ForeColor = System.Drawing.Color.Black;
            this.lblVisitNo.Location = new System.Drawing.Point(148, 84);
            this.lblVisitNo.Name = "lblVisitNo";
            this.lblVisitNo.Size = new System.Drawing.Size(0, 14);
            this.lblVisitNo.TabIndex = 155;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.Black;
            this.label43.Location = new System.Drawing.Point(10, 146);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(31, 14);
            this.label43.TabIndex = 157;
            this.label43.Text = "Age";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label42.Location = new System.Drawing.Point(10, 114);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(53, 14);
            this.label42.TabIndex = 156;
            this.label42.Text = "Gender";
            // 
            // lblBedDetails
            // 
            this.lblBedDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBedDetails.AutoSize = true;
            this.lblBedDetails.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBedDetails.ForeColor = System.Drawing.Color.Black;
            this.lblBedDetails.Location = new System.Drawing.Point(148, 176);
            this.lblBedDetails.MaximumSize = new System.Drawing.Size(400, 50);
            this.lblBedDetails.Name = "lblBedDetails";
            this.lblBedDetails.Size = new System.Drawing.Size(0, 14);
            this.lblBedDetails.TabIndex = 163;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.Black;
            this.label52.Location = new System.Drawing.Point(10, 176);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(78, 14);
            this.label52.TabIndex = 162;
            this.label52.Text = "Bed Details";
            // 
            // cboDischargeType
            // 
            this.cboDischargeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDischargeType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboDischargeType.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDischargeType.FormattingEnabled = true;
            this.cboDischargeType.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cboDischargeType.Location = new System.Drawing.Point(148, 235);
            this.cboDischargeType.Name = "cboDischargeType";
            this.cboDischargeType.Size = new System.Drawing.Size(166, 24);
            this.cboDischargeType.TabIndex = 165;
            this.cboDischargeType.SelectedIndexChanged += new System.EventHandler(this.cboDischargeType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(10, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 14);
            this.label3.TabIndex = 164;
            this.label3.Text = "Discharge Type";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGender.ForeColor = System.Drawing.Color.Black;
            this.lblGender.Location = new System.Drawing.Point(148, 114);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(0, 14);
            this.lblGender.TabIndex = 169;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAge.ForeColor = System.Drawing.Color.Black;
            this.lblAge.Location = new System.Drawing.Point(148, 146);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(0, 14);
            this.lblAge.TabIndex = 170;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(119, 206);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 13);
            this.label20.TabIndex = 173;
            this.label20.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(10, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 14);
            this.label7.TabIndex = 171;
            this.label7.Text = "Discharge Date";
            // 
            // dtpDischargeDate
            // 
            this.dtpDischargeDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpDischargeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDischargeDate.Location = new System.Drawing.Point(148, 199);
            this.dtpDischargeDate.Name = "dtpDischargeDate";
            this.dtpDischargeDate.Size = new System.Drawing.Size(166, 20);
            this.dtpDischargeDate.TabIndex = 172;
            this.dtpDischargeDate.Value = new System.DateTime(2016, 10, 27, 9, 42, 3, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 277);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 14);
            this.label1.TabIndex = 174;
            this.label1.Text = "Expiry Date";
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpiryDate.Location = new System.Drawing.Point(148, 271);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(166, 20);
            this.dtpExpiryDate.TabIndex = 175;
            this.dtpExpiryDate.Value = new System.DateTime(2016, 10, 27, 9, 42, 3, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(90, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 13);
            this.label2.TabIndex = 176;
            this.label2.Text = "*";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(237, 307);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 40);
            this.btnClose.TabIndex = 168;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.Location = new System.Drawing.Point(153, 307);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(78, 40);
            this.btnNew.TabIndex = 167;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(69, 307);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 40);
            this.btnSave.TabIndex = 166;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(118, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 177;
            this.label4.Text = "*";
            // 
            // cisDischargePatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 356);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpExpiryDate);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpDischargeDate);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cboDischargeType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblBedDetails);
            this.Controls.Add(this.label52);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.lblVisitNo);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.txtPatientId);
            this.Controls.Add(this.label35);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "cisDischargePatient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Discharge Patient";
            this.Load += new System.EventHandler(this.cisDischargePatient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPatientId;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label lblVisitNo;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label lblBedDetails;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.ComboBox cboDischargeType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpDischargeDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}