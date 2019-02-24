namespace CIS.Master
{
    partial class cisAddInvList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cisAddInvList));
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboInvCategory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInvestigationName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtInvUnitPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInvestigationCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboInvDepartmentId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cboAmtType = new System.Windows.Forms.ComboBox();
            this.txtDiscountPer = new System.Windows.Forms.TextBox();
            this.cboShareType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Inactive",
            "Active"});
            this.cboStatus.Location = new System.Drawing.Point(183, 228);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(166, 24);
            this.cboStatus.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 14);
            this.label5.TabIndex = 109;
            this.label5.Text = "Status";
            // 
            // cboInvCategory
            // 
            this.cboInvCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInvCategory.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboInvCategory.FormattingEnabled = true;
            this.cboInvCategory.Location = new System.Drawing.Point(187, 90);
            this.cboInvCategory.Name = "cboInvCategory";
            this.cboInvCategory.Size = new System.Drawing.Size(166, 24);
            this.cboInvCategory.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 14);
            this.label3.TabIndex = 107;
            this.label3.Text = "Category";
            // 
            // txtInvestigationName
            // 
            this.txtInvestigationName.Location = new System.Drawing.Point(187, 52);
            this.txtInvestigationName.Name = "txtInvestigationName";
            this.txtInvestigationName.Size = new System.Drawing.Size(166, 20);
            this.txtInvestigationName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 14);
            this.label1.TabIndex = 106;
            this.label1.Text = "Investigation Name";
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.Location = new System.Drawing.Point(270, 264);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 35);
            this.btnClose.TabIndex = 11;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNew.BackgroundImage")));
            this.btnNew.Location = new System.Drawing.Point(179, 264);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(85, 35);
            this.btnNew.TabIndex = 10;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.Location = new System.Drawing.Point(88, 264);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 35);
            this.btnSave.TabIndex = 9;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtInvUnitPrice
            // 
            this.txtInvUnitPrice.Location = new System.Drawing.Point(187, 166);
            this.txtInvUnitPrice.Name = "txtInvUnitPrice";
            this.txtInvUnitPrice.Size = new System.Drawing.Size(166, 20);
            this.txtInvUnitPrice.TabIndex = 4;
            this.txtInvUnitPrice.Text = "0.00";
            this.txtInvUnitPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInvUnitPrice_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(9, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 14);
            this.label2.TabIndex = 112;
            this.label2.Text = "Unit Price";
            // 
            // txtInvestigationCode
            // 
            this.txtInvestigationCode.Location = new System.Drawing.Point(187, 16);
            this.txtInvestigationCode.Name = "txtInvestigationCode";
            this.txtInvestigationCode.Size = new System.Drawing.Size(166, 20);
            this.txtInvestigationCode.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(9, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 14);
            this.label4.TabIndex = 114;
            this.label4.Text = "Investigation Code";
            // 
            // cboInvDepartmentId
            // 
            this.cboInvDepartmentId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInvDepartmentId.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboInvDepartmentId.FormattingEnabled = true;
            this.cboInvDepartmentId.Location = new System.Drawing.Point(187, 126);
            this.cboInvDepartmentId.Name = "cboInvDepartmentId";
            this.cboInvDepartmentId.Size = new System.Drawing.Size(166, 24);
            this.cboInvDepartmentId.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 14);
            this.label6.TabIndex = 115;
            this.label6.Text = "Department";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.ForeColor = System.Drawing.Color.Black;
            this.lblDiscount.Location = new System.Drawing.Point(319, 199);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(31, 15);
            this.lblDiscount.TabIndex = 149;
            this.lblDiscount.Text = "0.00";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(11, 199);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 14);
            this.label16.TabIndex = 148;
            this.label16.Text = "Share";
            // 
            // cboAmtType
            // 
            this.cboAmtType.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAmtType.FormattingEnabled = true;
            this.cboAmtType.Items.AddRange(new object[] {
            "Rs",
            "%"});
            this.cboAmtType.Location = new System.Drawing.Point(188, 194);
            this.cboAmtType.Name = "cboAmtType";
            this.cboAmtType.Size = new System.Drawing.Size(54, 24);
            this.cboAmtType.TabIndex = 6;
            // 
            // txtDiscountPer
            // 
            this.txtDiscountPer.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountPer.Location = new System.Drawing.Point(248, 195);
            this.txtDiscountPer.Name = "txtDiscountPer";
            this.txtDiscountPer.Size = new System.Drawing.Size(47, 23);
            this.txtDiscountPer.TabIndex = 7;
            this.txtDiscountPer.Text = "0.00";
            this.txtDiscountPer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiscountPer_KeyDown);
            this.txtDiscountPer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscountPer_KeyPress);
            this.txtDiscountPer.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtDiscountPer_PreviewKeyDown);
            // 
            // cboShareType
            // 
            this.cboShareType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShareType.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboShareType.FormattingEnabled = true;
            this.cboShareType.Location = new System.Drawing.Point(61, 194);
            this.cboShareType.Name = "cboShareType";
            this.cboShareType.Size = new System.Drawing.Size(121, 24);
            this.cboShareType.TabIndex = 5;
            // 
            // cisAddInvList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 310);
            this.Controls.Add(this.cboShareType);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cboAmtType);
            this.Controls.Add(this.txtDiscountPer);
            this.Controls.Add(this.cboInvDepartmentId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtInvestigationCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInvUnitPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboInvCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtInvestigationName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "cisAddInvList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Investigation List";
            this.Load += new System.EventHandler(this.cisAddInvList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboInvCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInvestigationName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtInvUnitPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInvestigationCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboInvDepartmentId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboAmtType;
        private System.Windows.Forms.TextBox txtDiscountPer;
        private System.Windows.Forms.ComboBox cboShareType;
    }
}