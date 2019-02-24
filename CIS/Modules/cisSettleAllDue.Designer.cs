namespace CIS.Modules
{
    partial class cisSettleAllDue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cisSettleAllDue));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtAmountPaid = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAdvAdj = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAdvBal = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvSettleAllDue = new System.Windows.Forms.DataGridView();
            this.lblTotalDueAmt = new System.Windows.Forms.Label();
            this.lblCollectionName = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txtHolderName = new System.Windows.Forms.TextBox();
            this.label112 = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label114 = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.cboPaymentMode = new System.Windows.Forms.ComboBox();
            this.label108 = new System.Windows.Forms.Label();
            this.lblCardNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettleAllDue)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountPaid.Location = new System.Drawing.Point(112, 16);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.Size = new System.Drawing.Size(75, 26);
            this.txtAmountPaid.TabIndex = 192;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.ForeColor = System.Drawing.Color.Black;
            this.label76.Location = new System.Drawing.Point(18, 20);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(88, 18);
            this.label76.TabIndex = 191;
            this.label76.Text = "Amount Paid";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(322, 16);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(75, 26);
            this.txtDiscount.TabIndex = 194;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(239, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 193;
            this.label1.Text = "Discount";
            // 
            // txtAdvAdj
            // 
            this.txtAdvAdj.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdvAdj.Location = new System.Drawing.Point(498, 16);
            this.txtAdvAdj.Name = "txtAdvAdj";
            this.txtAdvAdj.Size = new System.Drawing.Size(75, 26);
            this.txtAdvAdj.TabIndex = 196;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(436, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 18);
            this.label2.TabIndex = 195;
            this.label2.Text = "Adv Adj";
            // 
            // lblAdvBal
            // 
            this.lblAdvBal.AutoSize = true;
            this.lblAdvBal.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvBal.ForeColor = System.Drawing.Color.DarkRed;
            this.lblAdvBal.Location = new System.Drawing.Point(579, 20);
            this.lblAdvBal.Name = "lblAdvBal";
            this.lblAdvBal.Size = new System.Drawing.Size(33, 18);
            this.lblAdvBal.TabIndex = 197;
            this.lblAdvBal.Text = "0.00";
            // 
            // btnClear
            // 
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(709, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(38, 38);
            this.btnClear.TabIndex = 199;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(664, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(38, 38);
            this.btnAdd.TabIndex = 198;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(772, 459);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 41);
            this.btnClose.TabIndex = 201;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(598, 459);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 41);
            this.btnSave.TabIndex = 200;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvSettleAllDue
            // 
            this.dgvSettleAllDue.AllowUserToAddRows = false;
            this.dgvSettleAllDue.AllowUserToOrderColumns = true;
            this.dgvSettleAllDue.AllowUserToResizeColumns = false;
            this.dgvSettleAllDue.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.dgvSettleAllDue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSettleAllDue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSettleAllDue.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSettleAllDue.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSettleAllDue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSettleAllDue.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSettleAllDue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSettleAllDue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSettleAllDue.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSettleAllDue.Location = new System.Drawing.Point(12, 54);
            this.dgvSettleAllDue.Name = "dgvSettleAllDue";
            this.dgvSettleAllDue.RowHeadersVisible = false;
            this.dgvSettleAllDue.Size = new System.Drawing.Size(848, 301);
            this.dgvSettleAllDue.TabIndex = 202;
            this.dgvSettleAllDue.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSettleAllDue_CellValueChanged);
            this.dgvSettleAllDue.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvSettleAllDue_EditingControlShowing);
            // 
            // lblTotalDueAmt
            // 
            this.lblTotalDueAmt.AutoSize = true;
            this.lblTotalDueAmt.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDueAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTotalDueAmt.Location = new System.Drawing.Point(183, 463);
            this.lblTotalDueAmt.Name = "lblTotalDueAmt";
            this.lblTotalDueAmt.Size = new System.Drawing.Size(73, 29);
            this.lblTotalDueAmt.TabIndex = 204;
            this.lblTotalDueAmt.Text = "0.00";
            // 
            // lblCollectionName
            // 
            this.lblCollectionName.AutoSize = true;
            this.lblCollectionName.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCollectionName.ForeColor = System.Drawing.Color.Navy;
            this.lblCollectionName.Location = new System.Drawing.Point(12, 463);
            this.lblCollectionName.Name = "lblCollectionName";
            this.lblCollectionName.Size = new System.Drawing.Size(167, 29);
            this.lblCollectionName.TabIndex = 203;
            this.lblCollectionName.Text = "Total Due : ";
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(690, 459);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(78, 40);
            this.btnPrint.TabIndex = 205;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.BackColor = System.Drawing.Color.Silver;
            this.groupBox10.Controls.Add(this.txtHolderName);
            this.groupBox10.Controls.Add(this.label112);
            this.groupBox10.Controls.Add(this.txtBankName);
            this.groupBox10.Controls.Add(this.label114);
            this.groupBox10.Controls.Add(this.txtCardNumber);
            this.groupBox10.Controls.Add(this.cboPaymentMode);
            this.groupBox10.Controls.Add(this.label108);
            this.groupBox10.Controls.Add(this.lblCardNumber);
            this.groupBox10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox10.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox10.Location = new System.Drawing.Point(12, 361);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(848, 97);
            this.groupBox10.TabIndex = 206;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Payment Mode";
            // 
            // txtHolderName
            // 
            this.txtHolderName.Location = new System.Drawing.Point(411, 54);
            this.txtHolderName.Name = "txtHolderName";
            this.txtHolderName.Size = new System.Drawing.Size(166, 27);
            this.txtHolderName.TabIndex = 162;
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label112.Location = new System.Drawing.Point(310, 60);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(88, 14);
            this.label112.TabIndex = 161;
            this.label112.Text = "Holder Name";
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(411, 21);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(166, 27);
            this.txtBankName.TabIndex = 160;
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label114.Location = new System.Drawing.Point(310, 27);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(78, 14);
            this.label114.TabIndex = 159;
            this.label114.Text = "Bank Name";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(120, 55);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(166, 27);
            this.txtCardNumber.TabIndex = 158;
            // 
            // cboPaymentMode
            // 
            this.cboPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboPaymentMode.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPaymentMode.FormattingEnabled = true;
            this.cboPaymentMode.Items.AddRange(new object[] {
            "Cash",
            "Debit Card",
            "Credit Card",
            "Cheque",
            "DD"});
            this.cboPaymentMode.Location = new System.Drawing.Point(120, 24);
            this.cboPaymentMode.Name = "cboPaymentMode";
            this.cboPaymentMode.Size = new System.Drawing.Size(166, 24);
            this.cboPaymentMode.TabIndex = 156;
            this.cboPaymentMode.SelectedIndexChanged += new System.EventHandler(this.cboPaymentMode_SelectedIndexChanged);
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label108.Location = new System.Drawing.Point(10, 29);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(100, 14);
            this.label108.TabIndex = 155;
            this.label108.Text = "Payment Mode";
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardNumber.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCardNumber.Location = new System.Drawing.Point(10, 61);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(58, 14);
            this.lblCardNumber.TabIndex = 157;
            this.lblCardNumber.Text = "Card No";
            // 
            // cisSettleAllDue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 503);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblTotalDueAmt);
            this.Controls.Add(this.lblCollectionName);
            this.Controls.Add(this.dgvSettleAllDue);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblAdvBal);
            this.Controls.Add(this.txtAdvAdj);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAmountPaid);
            this.Controls.Add(this.label76);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "cisSettleAllDue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settle All Due";
            this.Load += new System.EventHandler(this.cisSettleAllDue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettleAllDue)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAmountPaid;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdvAdj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAdvBal;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvSettleAllDue;
        private System.Windows.Forms.Label lblTotalDueAmt;
        private System.Windows.Forms.Label lblCollectionName;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox txtHolderName;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.ComboBox cboPaymentMode;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label lblCardNumber;
    }
}