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
    public partial class cisViewAdvanceCollection : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public cisViewAdvanceCollection()
        {
            InitializeComponent();
            cboFilter.SelectedIndex = 0;
        }
        #endregion

        #region Events
        private void btnAdd_Click(object sender, EventArgs e)
        {
            cisAdvanceCollection ObjAdd = new cisAdvanceCollection();
            ObjAdd.ShowDialog();
        }
        #endregion

        private void cisViewAdvanceCollection_Load(object sender, EventArgs e)
        {
            this.dtpDateFrom.Value = DateTime.Now;
            this.dtpDateTo.Value = DateTime.Now;
            loadAdvanceTransactionDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGoDate_Click(object sender, EventArgs e)
        {
            loadAdvanceTransactionDetails();
        }

        private void loadAdvanceTransactionDetails()
        {
            try
            {
                //Common.Common.startDate = objBusinessFacade.dateFromValue(dtpDateFrom.Value.Date.ToString("dd-MM-yyyy"));
                //Common.Common.endDate =  objBusinessFacade.dateToValue(dtpDateTo.Value.Date.ToString("dd-MM-yyyy"));

                Common.Common.startDate = dtpDateFrom.Value.ToString("yyyyMMdd" + "000000"); //Changed by Jules Inorder to avoid datetime exception in diff system
                Common.Common.endDate = dtpDateTo.Value.ToString("yyyyMMdd" + "235959");

                dgvViewAdvanceCollection.DataSource = null;
                dgvViewAdvanceCollection.Rows.Clear();

                dtSource = objBusinessFacade.fetchAdvanceTransactionDetails(Common.Common.startDate, Common.Common.endDate);
                if (dtSource.Rows.Count > 0)
                {
                    dgvViewAdvanceCollection.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                    dgvViewAdvanceCollection.Columns[0].Width = 50;
                    dgvViewAdvanceCollection.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvViewAdvanceCollection.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvViewAdvanceCollection.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnGoFilter_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtSearch.Text)))
            {
                string filterField = cboFilter.SelectedItem.ToString();
                dtSource.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", filterField, txtSearch.Text);
                dgvViewAdvanceCollection.DataSource = dtSource;
            }
        }

        private void dgvViewAdvanceCollection_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvViewAdvanceCollection.Rows.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip m = new ContextMenuStrip();
                    m.Items.Add("PrintBill").Name = "PrintBill";

                    m.Show(dgvViewAdvanceCollection, new Point(e.X, e.Y));

                    m.ItemClicked += new ToolStripItemClickedEventHandler(cellMenuItem_Clicked);
                }
            }
        }

        private void cellMenuItem_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int rowIndex = dgvViewAdvanceCollection.CurrentRow.Index;
            Common.Common.billNo = dgvViewAdvanceCollection.Rows[rowIndex].Cells["Receipt No"].Value.ToString();

            switch (e.ClickedItem.Name.ToString())
            {
                case "PrintBill":
                    if (!string.IsNullOrEmpty(Common.Common.billNo))
                    {
                        CIS.BillTemplates.frmPrintReceipt frmAdvanceRececipt = new BillTemplates.frmPrintReceipt("AdvanceBill", Common.Common.billNo, "PatientId");
                        frmAdvanceRececipt.ShowDialog();
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
