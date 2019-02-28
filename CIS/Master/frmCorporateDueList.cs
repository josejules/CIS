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
    public partial class frmCorporateDueList : Form
    {
        DataTable dtCorporateDue = new DataTable();
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public frmCorporateDueList()
        {
            InitializeComponent();
        }

        private void frmViewLabBillInfo_Load(object sender, EventArgs e)
        {
            LoadCorporate();
            cboPaymentMode.SelectedIndex = 0;
        }

        private void LoadCorporate()
        {
            //Bind Corporate ComboBox
            cboCorporate.DataSource = new CIS.BusinessFacade.BusinessFacade().getCorporateDetails();
            cboCorporate.DisplayMember = "Corporate Name";
            cboCorporate.ValueMember = "corporate_id";
        }

        private void LoadCorporateDueList()
        {
            try
            {
                ComArugments args = new ComArugments();
                args.ParamList["startDate"] = dtpFromDate.Value.ToString("yyyyMMdd" + "000000"); 
                args.ParamList["endDate"] = dtpToDate.Value.ToString("yyyyMMdd" + "235959");
                args.ParamList["corporateId"] = cboCorporate.SelectedValue;
                dtCorporateDue = new CIS.BusinessFacade.BusinessFacade().CorporateDueListDetails(args);
                dgvCorporateDueList.DataSource = dtCorporateDue;
                string due  = dtCorporateDue.Compute("sum(due)", string.Empty).ToString();
                lblTotalDue.Text = due;
                lblRemainingDue.Text = due;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadCorporateDueList();
        }

        private void btnSplitAmount_Click(object sender, EventArgs e)
        {
            try
            {
                decimal totalAmountPaid = Convert.ToDecimal(string.IsNullOrWhiteSpace(txtAmountPaid.Text) ? "0.00" : txtAmountPaid.Text);
                if (totalAmountPaid > 0)
                {
                    foreach (DataGridViewRow row in dgvCorporateDueList.Rows)
                    {
                        if (totalAmountPaid > Convert.ToDecimal(row.Cells["ActualDue"].Value))
                        {
                            row.Cells["AmountPaid"].Value = row.Cells["ActualDue"].Value;
                            row.Cells["Due"].Value = "0.00";
                        }
                        else
                        {
                            row.Cells["AmountPaid"].Value = totalAmountPaid;
                            row.Cells["Due"].Value = Convert.ToDecimal(row.Cells["ActualDue"].Value) - totalAmountPaid;
                        }
                        totalAmountPaid = totalAmountPaid - Convert.ToDecimal(row.Cells["AmountPaid"].Value);
                    }
                    lblRemainingDue.Text = dgvCorporateDueList.Rows.Cast<DataGridViewRow>()
                                                .Sum(x => Convert.ToDecimal(x.Cells["Due"].Value)).ToString();
                }
                else
                    MessageBox.Show("Please enter Amount Paid", "Corporate Due List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Corporate Due List", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ComArugments objArg = new ComArugments();
                foreach (DataGridViewRow row in dgvCorporateDueList.Rows)
                {
                    if (Convert.ToDecimal(row.Cells["AmountPaid"].Value) > 0)
                    {
                        objArg.ParamList["bill_id"] = row.Cells["bill_id"].Value;
                        objArg.ParamList["due_collection"] = row.Cells["AmountPaid"].Value;
                        objArg.ParamList["due"] = row.Cells["Due"].Value;
                        objArg.ParamList["status"] = (Convert.ToDecimal(row.Cells["Due"].Value) == 0) ? "1" : "3";
                        objArg.ParamList["transaction_user_id"] = Common.Common.userId;
                        objArg.ParamList["discount"] = "0.00";
                        objArg.ParamList["net_total"] = "0.00";
                        objArg.ParamList["amount_paid"] = "0.00";

                        objArg.ParamList["payment_mode_id"] = Convert.ToInt32(cboPaymentMode.SelectedIndex.ToString());
                        objArg.ParamList["card_number"] = txtChequeNo.Text.ToString();
                        objArg.ParamList["bank_name"] = txtBankName.Text.ToString();
                        objArg.ParamList["holder_name"] = string.Empty;


                        switch (row.Cells["Department"].Value.ToString())
                        {
                            case "Registration":
                                Common.Common.flag = objBusinessFacade.updateRegistrationDueCollection(objArg);
                                Common.Common.flag = objBusinessFacade.insertRegistrationSummaryDueCollection(objArg);
                                break;

                            case "Laboratory":
                                Common.Common.flag = objBusinessFacade.updateInvestigationDueCollection(objArg);
                                Common.Common.flag = objBusinessFacade.insertInvestigationSummaryDueCollection(objArg);
                                break;

                            case "Pharmacy":
                                Common.Common.flag = objBusinessFacade.updatePharmacyDueCollection(objArg);
                                Common.Common.flag = objBusinessFacade.insertPharmacySummaryDueCollection(objArg);
                                break;

                            case "General":
                                Common.Common.flag = objBusinessFacade.updateGeneralDueCollection(objArg);
                                Common.Common.flag = objBusinessFacade.insertGeneralSummaryDueCollection(objArg);
                                break;

                            case "IP Billing":
                                Common.Common.flag = objBusinessFacade.updateBillingDueCollection(objArg);
                                Common.Common.flag = objBusinessFacade.insertBillingSummaryDueCollection(objArg);
                                Common.Common.flag = objBusinessFacade.insertWardAdvAdjDueCollection(objArg);
                                break;

                            default:
                                break;
                        }
                    }
                    //ClearControls();
                }
                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Corporate Due List", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearControls()
        {
            txtAmountPaid.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtChequeNo.Text = string.Empty;
            cboPaymentMode.SelectedIndex = -1;
            if (cboCorporate.Items.Count > 0)
                cboCorporate.SelectedIndex = 0;
            dgvCorporateDueList.DataSource = dtCorporateDue.Clone();
        }
    }
}
