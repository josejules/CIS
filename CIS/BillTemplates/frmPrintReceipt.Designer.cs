namespace CIS.BillTemplates
{
    partial class frmPrintReceipt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintReceipt));
            this.lblPreview = new System.Windows.Forms.Label();
            this.btnPint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.SuspendLayout();
            // 
            // lblPreview
            // 
            this.lblPreview.Location = new System.Drawing.Point(12, 9);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(609, 436);
            this.lblPreview.TabIndex = 0;
            this.lblPreview.Text = "label1";
            this.lblPreview.UseCompatibleTextRendering = true;
            // 
            // btnPint
            // 
            this.btnPint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPint.Image = global::CIS.Properties.Resources.Print1;
            this.btnPint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPint.Location = new System.Drawing.Point(450, 459);
            this.btnPint.Name = "btnPint";
            this.btnPint.Size = new System.Drawing.Size(75, 23);
            this.btnPint.TabIndex = 1;
            this.btnPint.Text = "&Print";
            this.btnPint.UseVisualStyleBackColor = true;
            this.btnPint.Click += new System.EventHandler(this.btnPint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::CIS.Properties.Resources.Delete;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(531, 459);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // frmPrintReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 494);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPint);
            this.Controls.Add(this.lblPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPrintReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Receipt";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button btnPint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
    }
}