namespace CIS.Master
{
    partial class frmAddDefineRegFee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddDefineRegFee));
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboValidity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRevisitRegFee = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValidity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNewRegFee = new System.Windows.Forms.TextBox();
            this.cboDoctor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(133, 11);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(250, 24);
            this.cboDepartment.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 14);
            this.label4.TabIndex = 116;
            this.label4.Text = "Department";
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.Location = new System.Drawing.Point(298, 209);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 35);
            this.btnClose.TabIndex = 115;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNew.BackgroundImage")));
            this.btnNew.Location = new System.Drawing.Point(207, 209);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(85, 35);
            this.btnNew.TabIndex = 8;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.Location = new System.Drawing.Point(116, 209);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 35);
            this.btnSave.TabIndex = 7;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cboValidity
            // 
            this.cboValidity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboValidity.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboValidity.FormattingEnabled = true;
            this.cboValidity.Items.AddRange(new object[] {
            "Every Visit",
            "Day",
            "Month",
            "Year"});
            this.cboValidity.Location = new System.Drawing.Point(242, 129);
            this.cboValidity.Name = "cboValidity";
            this.cboValidity.Size = new System.Drawing.Size(141, 24);
            this.cboValidity.TabIndex = 5;
            this.cboValidity.SelectedIndexChanged += new System.EventHandler(this.cboValidity_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(11, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 14);
            this.label2.TabIndex = 111;
            this.label2.Text = "Revisit Reg Fee";
            // 
            // txtRevisitRegFee
            // 
            this.txtRevisitRegFee.AccessibleName = "";
            this.txtRevisitRegFee.Location = new System.Drawing.Point(133, 171);
            this.txtRevisitRegFee.MaxLength = 5;
            this.txtRevisitRegFee.Name = "txtRevisitRegFee";
            this.txtRevisitRegFee.Size = new System.Drawing.Size(103, 20);
            this.txtRevisitRegFee.TabIndex = 6;
            this.txtRevisitRegFee.Text = "0.00";
            this.txtRevisitRegFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRevisitRegFee_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 14);
            this.label1.TabIndex = 109;
            this.label1.Text = "Validity";
            // 
            // txtValidity
            // 
            this.txtValidity.AccessibleName = "";
            this.txtValidity.Location = new System.Drawing.Point(133, 131);
            this.txtValidity.MaxLength = 5;
            this.txtValidity.Name = "txtValidity";
            this.txtValidity.Size = new System.Drawing.Size(103, 20);
            this.txtValidity.TabIndex = 4;
            this.txtValidity.Text = "0";
            this.txtValidity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValidity_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(11, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 14);
            this.label6.TabIndex = 107;
            this.label6.Text = "New Reg Fee";
            // 
            // txtNewRegFee
            // 
            this.txtNewRegFee.AccessibleName = "";
            this.txtNewRegFee.Location = new System.Drawing.Point(133, 92);
            this.txtNewRegFee.MaxLength = 5;
            this.txtNewRegFee.Name = "txtNewRegFee";
            this.txtNewRegFee.Size = new System.Drawing.Size(103, 20);
            this.txtNewRegFee.TabIndex = 3;
            this.txtNewRegFee.Text = "0.00";
            this.txtNewRegFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewRegFee_KeyPress);
            // 
            // cboDoctor
            // 
            this.cboDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoctor.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDoctor.FormattingEnabled = true;
            this.cboDoctor.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cboDoctor.Location = new System.Drawing.Point(133, 52);
            this.cboDoctor.Name = "cboDoctor";
            this.cboDoctor.Size = new System.Drawing.Size(250, 24);
            this.cboDoctor.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 14);
            this.label3.TabIndex = 101;
            this.label3.Text = "Doctor";
            // 
            // frmAddDefineRegFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 255);
            this.Controls.Add(this.cboDepartment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.cboDoctor);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNewRegFee);
            this.Controls.Add(this.cboValidity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtValidity);
            this.Controls.Add(this.txtRevisitRegFee);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddDefineRegFee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Define Reg Fee";
            this.Load += new System.EventHandler(this.frmDefineRegFee_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDoctor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRevisitRegFee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtValidity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNewRegFee;
        private System.Windows.Forms.ComboBox cboValidity;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Label label4;
    }
}