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
using CIS;

namespace CIS.Modules
{
    public partial class CISViewPurchaseReceipt : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public CISViewPurchaseReceipt()
        {
            InitializeComponent();
            cboFilter.SelectedIndex = 0;
            cboDateBy.SelectedIndex = 0;
        }

        private void btnGoDate_Click(object sender, EventArgs e)
        {
            loadPurchaseInfo();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CIS.Modules.CISPurchaseReceipt objShow = new CIS.Modules.CISPurchaseReceipt();
            objShow.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CISViewPurchaseReceipt_Load(object sender, EventArgs e)
        {
            this.dtpDateFrom.Value = DateTime.Now;
            this.dtpDateTo.Value = DateTime.Now;
            loadPurchaseInfo();
        }

        private void btnGoFilter_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtSearch.Text)))
            {
                string filterField = cboFilter.SelectedItem.ToString();
                dtSource.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filterField, txtSearch.Text);
                dgvViewPurchaseReceipt.DataSource = dtSource;
            }
        }

        private void loadPurchaseInfo()
        {
            if (cboDateBy.SelectedIndex == 0)
            {
                Common.Common.dateBy = "pur_invoice_date";
            }
            else
            {
                Common.Common.dateBy = "pur_receive_date";
            }
            //Common.Common.startDate = objBusinessFacade.dateFromValue(dtpDateFrom.Value.Date.ToString("dd-MM-yyyy"));
            //Common.Common.endDate = objBusinessFacade.dateToValue(dtpDateTo.Value.Date.ToString("dd-MM-yyyy"));

            Common.Common.startDate = dtpDateFrom.Value.ToString("yyyyMMdd" + "000000"); //Changed by Jules Inorder to avoid datetime exception in diff system
            Common.Common.endDate = dtpDateTo.Value.ToString("yyyyMMdd" + "235959");

            dgvViewPurchaseReceipt.DataSource = null;
            dgvViewPurchaseReceipt.Rows.Clear();
            try
            {
                dtSource = objBusinessFacade.getPurchaseReceiptInfo(Common.Common.dateBy, Common.Common.startDate, Common.Common.endDate);
                if (dtSource.Rows.Count > 0)
                {
                    dgvViewPurchaseReceipt.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                    dgvViewPurchaseReceipt.Columns[0].Width = 50;
                    dgvViewPurchaseReceipt.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvViewPurchaseReceipt.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvViewPurchaseReceipt.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message, "Purchase Receipt", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dgvViewPurchaseReceipt_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Double Clicked", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvViewPurchaseReceipt_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
