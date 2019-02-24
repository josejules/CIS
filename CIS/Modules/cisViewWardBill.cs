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
    public partial class cisViewWardBill : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cisViewWardBill()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cisWardBill ObjAdd = new cisWardBill();
            ObjAdd.ShowDialog();
        }

        private void cisViewWardBill_Load(object sender, EventArgs e)
        {
            cboFilter.SelectedIndex = 0;
            this.dtpDateFrom.Value = DateTime.Now;
            this.dtpDateTo.Value = DateTime.Now;
            loadWardBillDetails();
        }

        private void loadWardBillDetails()
        {
            //Common.Common.startDate = objBusinessFacade.dateFromValue(dtpDateFrom.Value.Date.ToString("dd-MM-yyyy"));
            //Common.Common.endDate = objBusinessFacade.dateToValue(dtpDateTo.Value.Date.ToString("dd-MM-yyyy"));

            Common.Common.startDate = dtpDateFrom.Value.ToString("yyyyMMdd" + "000000"); //Changed by Jules Inorder to avoid datetime exception in diff system
            Common.Common.endDate = dtpDateTo.Value.ToString("yyyyMMdd" + "235959");

            dgvViewWardBillDetails.DataSource = null;
            dgvViewWardBillDetails.Rows.Clear();
            try
            {
                dtSource = objBusinessFacade.getWardBillInfo(Common.Common.startDate, Common.Common.endDate);
                if (dtSource.Rows.Count > 0)
                {
                    dgvViewWardBillDetails.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                    dgvViewWardBillDetails.Columns[0].Width = 50;
                    dgvViewWardBillDetails.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvViewWardBillDetails.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvViewWardBillDetails.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "View Ward Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
            }
        }

        private void btnGoDate_Click(object sender, EventArgs e)
        {
            loadWardBillDetails();
        }

        private void btnGoFilter_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtSearch.Text)))
            {
                string filterField = cboFilter.SelectedItem.ToString();
                dtSource.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", filterField, txtSearch.Text);
                dgvViewWardBillDetails.DataSource = dtSource;
            }
        }

        private void dgvViewWardBillDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvViewWardBillDetails.Rows.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip m = new ContextMenuStrip();
                    m.Items.Add("PrintBill").Name = "PrintBill";

                    m.Show(dgvViewWardBillDetails, new Point(e.X, e.Y));

                    m.ItemClicked += new ToolStripItemClickedEventHandler(cellMenuItem_Clicked);
                }
            }
        }

        private void cellMenuItem_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int rowIndex = dgvViewWardBillDetails.CurrentRow.Index;
            Common.Common.billNo = dgvViewWardBillDetails.Rows[rowIndex].Cells["Bill No"].Value.ToString();
            string statusN = string.Empty;
            statusN = dgvViewWardBillDetails.Rows[rowIndex].Cells["Status"].Value.ToString();

            switch (e.ClickedItem.Name.ToString())
            {
                case "PrintBill":
                    if ((!string.IsNullOrEmpty(Common.Common.billNo)) && statusN != "Cancelled")
                    {
                        CIS.BillTemplates.frmPrintReceipt frmWardRececipt = new BillTemplates.frmPrintReceipt("WardBill", Common.Common.billNo, "PatientId");
                        frmWardRececipt.ShowDialog();
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
