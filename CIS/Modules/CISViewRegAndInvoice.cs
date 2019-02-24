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
    public partial class CISViewRegAndInvoice : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public CISViewRegAndInvoice()
        {
            InitializeComponent();
            cboDepartment.SelectedIndex = 0;
            cboFilter.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CIS.Modules.CISRegAndInvoice objShow = new CIS.Modules.CISRegAndInvoice();
            objShow.ShowDialog();
        }

        private void CISViewRegAndInvoice_Load(object sender, EventArgs e)
        {
            this.dtpDateFrom.Value = DateTime.Now.Date;
            this.dtpDateTo.Value = DateTime.Now.Date;
            dtpDateFrom.MaxDate = DateTime.Now.Date;
            dtpDateTo.MaxDate = DateTime.Now.Date;
            loadRegAndBillInfo();
        }

        private void btnGoDate_Click(object sender, EventArgs e)
        {
            loadRegAndBillInfo();
        }

        private void btnGoFilter_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtSearch.Text)))
            {
                string filterField = cboFilter.SelectedItem.ToString();
                dtSource.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", filterField, txtSearch.Text);
                dgvViewRegAndInvoice.DataSource = dtSource;
            }
            else
            {
                MessageBox.Show("Filter value is empty....!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void loadRegAndBillInfo()
        {
            if (dtpDateFrom.Value.Date > dtpDateTo.Value.Date)
            {
                MessageBox.Show("From date should not be greater than To date", "View Patient Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpDateTo.Value = dtpDateFrom.Value;
            }
            Common.Common.cis_department.departmentCategory = cboDepartment.SelectedItem.ToString();
            Common.Common.startDate = objBusinessFacade.dateFromValue(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd"));
            Common.Common.endDate = objBusinessFacade.dateToValue(dtpDateTo.Value.Date.ToString("yyyy-MM-dd"));

            switch (Common.Common.cis_department.departmentCategory)
            {
                case "OP Registration":
                    dgvViewRegAndInvoice.DataSource = null;
                    dgvViewRegAndInvoice.Rows.Clear();
                    try
                    {
                        dtSource = objBusinessFacade.getOPRegistrationInfo(Common.Common.startDate, Common.Common.endDate);
                        if (dtSource.Rows.Count > 0)
                        {
                            dgvViewRegAndInvoice.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                            dgvViewRegAndInvoice.Columns[0].Width = 50;
                            dgvViewRegAndInvoice.Columns[1].Width = 110;
                            dgvViewRegAndInvoice.Columns[6].Width = 160;
                            dgvViewRegAndInvoice.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvViewRegAndInvoice.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;

                case "IP Registration":
                    dgvViewRegAndInvoice.DataSource = null;
                    dgvViewRegAndInvoice.Rows.Clear();
                    try
                    {
                        dtSource = objBusinessFacade.getIPRegistrationInfo(Common.Common.startDate, Common.Common.endDate);
                        if (dtSource.Rows.Count > 0)
                        {
                            dgvViewRegAndInvoice.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                            dgvViewRegAndInvoice.Columns[0].Width = 50;
                            dgvViewRegAndInvoice.Columns[1].Width = 80;
                            //dgvViewRegAndInvoice.Columns[6].Width = 160;
                            dgvViewRegAndInvoice.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvViewRegAndInvoice.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvViewRegAndInvoice.Columns[10].Width = 60;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;

                case "Investigation":
                    dgvViewRegAndInvoice.DataSource = null;
                    dgvViewRegAndInvoice.Rows.Clear();
                    try
                    {
                        dtSource = objBusinessFacade.getInvestigationInfo(Common.Common.startDate, Common.Common.endDate);
                        if (dtSource.Rows.Count > 0)
                        {
                            dgvViewRegAndInvoice.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                            dgvViewRegAndInvoice.Columns[0].Width = 50;
                            //dgvViewRegAndInvoice.Columns[1].Width = 110;
                            //dgvViewRegAndInvoice.Columns[6].Width = 160;
                            dgvViewRegAndInvoice.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvViewRegAndInvoice.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;

                case "Pharmacy":
                    dgvViewRegAndInvoice.DataSource = null;
                    dgvViewRegAndInvoice.Rows.Clear();
                    try
                    {
                        dtSource = objBusinessFacade.getPharmacyInfo(Common.Common.startDate, Common.Common.endDate);
                        if (dtSource.Rows.Count > 0)
                        {
                            dgvViewRegAndInvoice.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                            dgvViewRegAndInvoice.Columns[0].Width = 50;
                            //dgvViewRegAndInvoice.Columns[1].Width = 110;
                            //dgvViewRegAndInvoice.Columns[6].Width = 160;
                            dgvViewRegAndInvoice.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvViewRegAndInvoice.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;

                case "General":
                    dgvViewRegAndInvoice.DataSource = null;
                    dgvViewRegAndInvoice.Rows.Clear();
                    try
                    {
                        dtSource = objBusinessFacade.getGeneralInfo(Common.Common.startDate, Common.Common.endDate);
                        if (dtSource.Rows.Count > 0)
                        {
                            dgvViewRegAndInvoice.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                            dgvViewRegAndInvoice.Columns[0].Width = 50;
                            //dgvViewRegAndInvoice.Columns[1].Width = 110;
                            //dgvViewRegAndInvoice.Columns[6].Width = 160;
                            dgvViewRegAndInvoice.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgvViewRegAndInvoice.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    break;

                default:
                    break;
            }
        }

        private void dgvViewRegAndInvoice_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvViewRegAndInvoice.Rows.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip m = new ContextMenuStrip();
                    m.Items.Add("Print Receipt").Name = "Print Receipt";
                    m.Items.Add("Print Label").Name = "Print Label";

                    m.Show(dgvViewRegAndInvoice, new Point(e.X, e.Y));

                    m.ItemClicked += new ToolStripItemClickedEventHandler(cellMenuItem_Clicked);
                }
            }
        }

        private void cellMenuItem_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int rowIndex = dgvViewRegAndInvoice.CurrentRow.Index;
            string statusN = string.Empty;

            Common.Common.billNo = dgvViewRegAndInvoice.Rows[rowIndex].Cells["Bill No"].Value.ToString();
            Common.Common.cis_number_generation.visit_number = dgvViewRegAndInvoice.Rows[rowIndex].Cells["Visit Id"].Value.ToString();

            string subStrBillType;
            subStrBillType = Common.Common.billNo.Substring(0, 2);
            dtSource = objBusinessFacade.getBillTypeId(subStrBillType);
            Common.Common.billTypeId = objBusinessFacade.NonBlankValueOfInt(dtSource.Rows[0]["number_format_id"].ToString());
            if (Common.Common.billTypeId == 4)
            {
                statusN = dgvViewRegAndInvoice.Rows[rowIndex].Cells["Visit Mode"].Value.ToString();
            }
            else
            {
                statusN = dgvViewRegAndInvoice.Rows[rowIndex].Cells["Status"].Value.ToString();
            }

            if (Common.Common.billTypeId > 0 && statusN != "Cancelled")
            {
                switch (e.ClickedItem.Name.ToString())
                {
                    case "Print Receipt":
                        switch (Common.Common.billTypeId)
                        {
                            //Registration Bill
                            case 4:
                                CIS.BillTemplates.frmPrintReceipt frmRegRececipt = new BillTemplates.frmPrintReceipt("RegistrationBill", Common.Common.billNo, Common.Common.cis_number_generation.patient_id);
                                frmRegRececipt.ShowDialog();
                                break;

                            //Investigation Bill
                            case 5:
                                CIS.BillTemplates.frmPrintReceipt frmInvRececipt = new BillTemplates.frmPrintReceipt("InvestigationBill", Common.Common.billNo, Common.Common.cis_number_generation.patient_id);
                                frmInvRececipt.ShowDialog();
                                break;

                            //Pharmacy Bill
                            case 6:
                                CIS.BillTemplates.frmPrintReceipt frmPhaRececipt = new BillTemplates.frmPrintReceipt("PharmacyBill", Common.Common.billNo, Common.Common.cis_number_generation.patient_id);
                                frmPhaRececipt.ShowDialog();
                                break;

                            //General Bill
                            case 7:
                                CIS.BillTemplates.frmPrintReceipt frmGenRececipt = new BillTemplates.frmPrintReceipt("GeneralBill", Common.Common.billNo, Common.Common.cis_number_generation.patient_id);
                                frmGenRececipt.ShowDialog();
                                break;

                            default:
                                break;
                        }
                        break;

                    case "Print Label":
                        //Registration Slip
                        if (!string.IsNullOrEmpty(Common.Common.cis_number_generation.visit_number))
                        {
                            CIS.BillTemplates.frmPrintReceipt frmRegSlip = new BillTemplates.frmPrintReceipt("RegistrationSlip", Common.Common.billNo, Common.Common.cis_number_generation.visit_number);
                            frmRegSlip.ShowDialog();
                        }
                        break;

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Bill is Cancelled....!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtpDateTo_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpDateFrom.Value.Date < dtpDateTo.Value.Date)
            //{
            //    MessageBox.Show("To date should not be less than From date");
            //}
        }

        private void dtpDateFrom_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpDateFrom.Value.Date > dtpDateTo.Value.Date)
            //{
            //    MessageBox.Show("From date should not be greater than To date");
            //    dtpDateFrom.Value = dtpDateTo.Value;
            //}
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Alt | Keys.A:
                 CIS.Modules.CISRegAndInvoice objShow = new CIS.Modules.CISRegAndInvoice();
                  objShow.ShowDialog();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadRegAndBillInfo();
            txtSearch.Text = string.Empty;
        }
    }
}
