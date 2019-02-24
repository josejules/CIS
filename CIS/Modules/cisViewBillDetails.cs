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
    public partial class cisViewBillDetails : Form
    {

        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        public DataTable dtPatient = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cisViewBillDetails()
        {
            InitializeComponent();
        }

        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtPatient = objBusinessFacade.getVisitDetailsByPatientId(txtPatientId.Text.ToString());
                if (dtPatient.Rows.Count > 0)
                {
                    txtPatientId.Text = dtPatient.Rows[0]["patient_id"].ToString();
                    lblPatientName.Text = dtPatient.Rows[0]["patient_name"].ToString();

                    DataRow row = dtPatient.NewRow();
                    row[2] = "All";
                    dtPatient.Rows.InsertAt(row, 0);
                    

                    cboVisitNo.ValueMember = "visit_number";
                    cboVisitNo.DisplayMember = "visit_number";
                    cboVisitNo.DataSource = dtPatient;
                    cboVisitNo.Focus();

                    cboVisitNo.SelectedIndex = 1;
                    lblLastVisitNo.Text = cboVisitNo.Text;
                }
                else
                {
                    MessageBox.Show("Invalid Patient Id....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                e.Handled = true;
            }
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cboVisitNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cisViewBillDetails_Load(object sender, EventArgs e)
        {
            cboDepartment.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboVisitNo.Items.Count > 0)
            {
                Common.Common.cis_number_generation.visit_number = Convert.ToString(cboVisitNo.SelectedValue);
                Common.Common.cis_number_generation.patient_id = txtPatientId.Text.ToString();
                Common.Common.cis_department.departmentName = Convert.ToString(cboDepartment.Text);
                string visitQry = string.Empty;

                if (cboVisitNo.SelectedIndex == 0)//Select all visit numbers by Patient
                {
                    visitQry = "select visit_number from pat_visit_info where patient_id = '" + Common.Common.cis_number_generation.patient_id + "'";
                }
                else // Select only one visit number by patient
                {
                    visitQry = "Select '" + Common.Common.cis_number_generation.visit_number + "'";
                }

                switch (Common.Common.cis_department.departmentName)
                {
                    case "All":
                        dgvViewBillDetails.DataSource = null;
                        dgvViewBillDetails.Rows.Clear();
                        try
                        {
                            dtSource = objBusinessFacade.viewAllBillInfoByVisit(visitQry);
                            if (dtSource.Rows.Count > 0)
                            {
                                dgvViewBillDetails.DataSource =dtSource;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;

                    case "Registration":
                        dgvViewBillDetails.DataSource = null;
                        dgvViewBillDetails.Rows.Clear();
                        try
                        {
                            dtSource = objBusinessFacade.viewRegBillInfoByVisit(visitQry);
                            if (dtSource.Rows.Count > 0)
                            {
                                dgvViewBillDetails.DataSource = dtSource;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;

                    case "Investigation":
                        dgvViewBillDetails.DataSource = null;
                        dgvViewBillDetails.Rows.Clear();
                        try
                        {
                            dtSource = objBusinessFacade.viewInvBillInfoByVisit(visitQry);
                            if (dtSource.Rows.Count > 0)
                            {
                                dgvViewBillDetails.DataSource = dtSource;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;

                    case "Pharmacy":
                        dgvViewBillDetails.DataSource = null;
                        dgvViewBillDetails.Rows.Clear();
                        try
                        {
                            dtSource = objBusinessFacade.viewPharmacyBillInfoByVisit(visitQry);
                            if (dtSource.Rows.Count > 0)
                            {
                                dgvViewBillDetails.DataSource = dtSource;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;

                    case "General":
                        dgvViewBillDetails.DataSource = null;
                        dgvViewBillDetails.Rows.Clear();
                        try
                        {
                            dtSource = objBusinessFacade.viewGenBillInfoByVisit(visitQry);
                            if (dtSource.Rows.Count > 0)
                            {
                                dgvViewBillDetails.DataSource = dtSource;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        break;

                    case "IP Billing":
                        dgvViewBillDetails.DataSource = null;
                        dgvViewBillDetails.Rows.Clear();
                        try
                        {
                            dtSource = objBusinessFacade.viewWardBillInfoByVisit(visitQry);
                            if (dtSource.Rows.Count > 0)
                            {
                                dgvViewBillDetails.DataSource = dtSource;
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
                calculateSum();
            }
        }

        private void chkShowDueOnly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtSource.Rows.Count > 0)
                {
                    if (chkShowDueOnly.Checked == true)
                    {
                        //string filterField = cboFilter.SelectedItem.ToString();
                        //dtSource.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", "Status", ("Due" or "Partically Paid"));
                        dtSource.DefaultView.RowFilter = "Status = 'Partially Paid' or Status = 'Not Paid'";
                        dgvViewBillDetails.DataSource = dtSource;
                        calculateSum();
                    }
                    else
                    {
                        dtSource.DefaultView.RowFilter = string.Empty;
                        calculateSum();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "View Bill Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void calculateSum()
        {
            decimal sum = 0;
            Common.Common.totalSum = 0;
            decimal discount = 0;
            Common.Common.totalDiscount = 0;
            decimal amountPaid = 0;
            Common.Common.totalAmountPaid = 0;
            decimal advAdj = 0;
            Common.Common.totalAdvAdj = 0;
            decimal due = 0;
            Common.Common.totalDue = 0;

            foreach (DataGridViewRow row in dgvViewBillDetails.Rows)
            {
                sum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Bill Amount"].Value));
                Common.Common.totalSum = Common.Common.totalSum + sum; //Calculate Total Amt

                discount = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Discount"].Value));
                Common.Common.totalDiscount = Common.Common.totalDiscount + discount; //Calculate Discount

                amountPaid = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Amount Paid"].Value));
                Common.Common.totalAmountPaid = Common.Common.totalAmountPaid + amountPaid; //Calculate Amount Amt

                advAdj = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Adv Adj"].Value));
                Common.Common.totalAdvAdj = Common.Common.totalAdvAdj + advAdj; //Calculate Amount Amt

                due = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Due"].Value));
                Common.Common.totalDue = Common.Common.totalDue + due; //Calculate Total due
            }

            lblTotalAmount.Text = Common.Common.totalSum.ToString("0.00");
            lblTotalDiscount.Text = Common.Common.totalDiscount.ToString("0.00");
            lblTotalAmountPaid.Text = Common.Common.totalAmountPaid.ToString("0.00");
            lblTotalAdvAdj.Text = Common.Common.totalAdvAdj.ToString("0.00");
            lblTotalDue.Text = Common.Common.totalDue.ToString("0.00");
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            int dueFlag = 0;
            if (dgvViewBillDetails.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvViewBillDetails.Rows)// Check if found due
                {
                    if (Convert.ToString(row.Cells["Status"].Value) == "Partially Paid" || Convert.ToString(row.Cells["Status"].Value) == "Not Paid")
                    {
                        dueFlag = 1;
                        break;
                    }
                    else
                    {
                        dueFlag = 0;
                    }

                }

                if (dueFlag == 1)//Open if found due only
                {
                    Common.Common.cis_number_generation.patient_id = txtPatientId.Text.ToString();
                    Common.Common.cis_number_generation.visit_number = lblLastVisitNo.Text.ToString();

                    CIS.Modules.cisSettleAllDue objShow = new CIS.Modules.cisSettleAllDue(dtSource, Common.Common.cis_number_generation.patient_id, Common.Common.cis_number_generation.visit_number);
                    objShow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Found No Dues to Settle....!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dgvViewBillDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvViewBillDetails.Rows.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip m = new ContextMenuStrip();
                    m.Items.Add("Print").Name = "Print";

                    m.Show(dgvViewBillDetails, new Point(e.X, e.Y));

                    m.ItemClicked += new ToolStripItemClickedEventHandler(cellMenuItem_Clicked);
                }
            }
        }

        private void cellMenuItem_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                int rowIndex = dgvViewBillDetails.CurrentRow.Index;

                Common.Common.billNo = dgvViewBillDetails.Rows[rowIndex].Cells["Bill No"].Value.ToString();
                Common.Common.cis_number_generation.patient_id = txtPatientId.Text;//dgvViewBillDetails.Rows[rowIndex].Cells["Patient_Id"].Value.ToString();
                string statusN = dgvViewBillDetails.Rows[rowIndex].Cells["Status"].Value.ToString();
                string subStrBillType;
                subStrBillType = Common.Common.billNo.Substring(0, 2);
                dtSource = objBusinessFacade.getBillTypeId(subStrBillType);
                Common.Common.billTypeId = objBusinessFacade.NonBlankValueOfInt(dtSource.Rows[0]["number_format_id"].ToString());

                if (Common.Common.billTypeId > 0 && statusN != "Cancelled")
                {
                    switch (e.ClickedItem.Name.ToString())
                    {
                        case "Print":
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

                        case "View":
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("No bill for Cancelled Bill....!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "View Bill Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
