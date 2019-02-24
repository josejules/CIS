using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CIS.BusinessFacade;
using CIS.Common;

namespace CIS.Master
{
    public partial class frmPrinterSettings : Form
    {
        #region Variables
        public int SettingId = 0;
        #endregion

        public frmPrinterSettings()
        {
            InitializeComponent();
            cboReportName.DataSource = Enum.GetValues(typeof(CIS.Common.PrinterSetting));
        }

        public frmPrinterSettings(int settingId)
        {
            try
            {
                SettingId = settingId;
                InitializeComponent();
                cboReportName.DataSource = Enum.GetValues(typeof(CIS.Common.PrinterSetting));
                DataTable dtPrinterSetting = new clsLabBusinessFacade().FetchPrinterSettingInfoById(SettingId);
                if (dtPrinterSetting.Rows.Count > 0)
                {
                    cboReportName.Text = dtPrinterSetting.Rows[0]["report_name"].ToString();
                    txtPrinterName.Text = dtPrinterSetting.Rows[0]["printer_name"].ToString();
                    chkIsLazer.Checked = dtPrinterSetting.Rows[0]["is_lazer_printer"].ToString() == "1" ? true : false ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPrinterSettings_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrinterName.Text))
            {
                ComArugments args = new ComArugments();
                args.ParamList[CIS.Common.Master.PrinterSettings.SettingId] = SettingId;
                args.ParamList[CIS.Common.Master.PrinterSettings.ReportName] = cboReportName.Text;
                args.ParamList[CIS.Common.Master.PrinterSettings.PrinterName] = txtPrinterName.Text;
                args.ParamList[CIS.Common.Master.PrinterSettings.IsLazerPrinter] = chkIsLazer.Checked;

                int rowsAffected = new clsLabBusinessFacade().AddPrinterSetting(args);

                if (rowsAffected > 0)
                {
                    if (SettingId == 0)
                        MessageBox.Show("Printer Setting is saved", "Printer Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Printer Setting is updated", "Printer Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrinterName.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Please enter Printer Name", "Printer Setting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrinterSettings_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrinterName.Text))
            {
                errorProvider1.SetError(txtPrinterName, "Please enter Printer Name");
                txtPrinterName.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

    }
}
