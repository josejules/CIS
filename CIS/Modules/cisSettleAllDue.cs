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
    public partial class cisSettleAllDue : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSourceDue = new DataTable();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cisSettleAllDue(DataTable dtSource, string patient_id, string visit_number)
        {
            InitializeComponent();
            dtSourceDue = dtSource;
            Common.Common.cis_number_generation.patient_id = patient_id;
            Common.Common.cis_number_generation.visit_number = visit_number;
        }

        private void cisSettleAllDue_Load(object sender, EventArgs e)
        {
            dtSourceDue.DefaultView.RowFilter = "Status = 'Partially Paid' or Status = 'Not Paid'";
            dgvSettleAllDue.DataSource = dtSourceDue;

            //Set Amount Paid and Due as 0, to get new values
            for (int i = 0; i < dgvSettleAllDue.Rows.Count; i++)
            {
                dgvSettleAllDue.Rows[i].Cells["Amount Paid"].Value = "0.00";
                dgvSettleAllDue.Rows[i].Cells["Discount"].Value = "0.00";
                dgvSettleAllDue.Rows[i].Cells["Adv Adj"].Value = "0.00";

                if (Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Department"].Value) == "IP Billing")
                {
                    dgvSettleAllDue.Rows[i].Cells["Adv Adj"].ReadOnly = false;
                }
                else
                {
                    dgvSettleAllDue.Rows[i].Cells["Adv Adj"].ReadOnly = true;
                }
            }

            decimal due = 0;
            Common.Common.totalDue = 0;

            foreach (DataGridViewRow row in dgvSettleAllDue.Rows)
            {
                due = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Due"].Value));
                Common.Common.totalDue = Common.Common.totalDue + due; //Calculate Total due
            }

            lblTotalDueAmt.Text = Common.Common.totalDue.ToString("0.00");
            cboPaymentMode.SelectedIndex = 0;

            dtSource = objBusinessFacade.getNetAdvAvailbyPatientId(Common.Common.cis_number_generation.patient_id, Common.Common.cis_number_generation.visit_number);

            if (dtSource.Rows.Count > 0)
            {
                lblAdvBal.Text = dtSource.Rows[0]["net_amount"].ToString();
            }
            else
            {
                lblAdvBal.Text = "0.00";
            }

            /*if (dtSourceDue.Rows.Count > 0)
            {
                for (int i = 0; i < dtSourceDue.Rows.Count; i++)
                {
                    dgvSettleAllDue.Rows.Add();
                    dgvSettleAllDue.Rows[i].Cells["bill_id"].Value = dtSourceDue.Rows[i]["bill_id"].ToString();
                    dgvSettleAllDue.Rows[i].Cells["department"].Value = dtSourceDue.Rows[i]["Department"].ToString();
                    dgvSettleAllDue.Rows[i].Cells["bill_no"].Value = dtSourceDue.Rows[i]["Bill No"].ToString();
                    dgvSettleAllDue.Rows[i].Cells["bill_date"].Value = dtSourceDue.Rows[i]["Bill Date"].ToString();
                    dgvSettleAllDue.Rows[i].Cells["visit_no"].Value = dtSourceDue.Rows[i]["Visit No"].ToString();
                    dgvSettleAllDue.Rows[i].Cells["bill_amt"].Value = dtSourceDue.Rows[i]["Bill Amount"].ToString();
                    dgvSettleAllDue.Rows[i].Cells["discount"].Value = dtSourceDue.Rows[i]["Discount"].ToString();
                    dgvSettleAllDue.Rows[i].Cells["amt_paid"].Value = dtSourceDue.Rows[i]["Amount Paid"].ToString();
                    dgvSettleAllDue.Rows[i].Cells["due"].Value = dtSourceDue.Rows[i]["Due"].ToString();
                }
                //dgvSettleAllDue.Columns["physical_qty"].Visible = true;

                //gvSettleAllDue.Columns["sno"].ReadOnly = true;
                //dtSourceDue.DefaultView.RowFilter = "Status = 'Partially Paid' or Status = 'Not Paid'";
                dtSourceDue.DefaultView.RowFilter = "Due > 0";
                dgvSettleAllDue.DataSource = dtSourceDue;
            }*/
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //decimal remainDue =0;
            Common.Common.totalAmountPaid = objBusinessFacade.NonBlankValueOfDecimal(txtAmountPaid.Text.ToString());
            Common.Common.totalDiscount = objBusinessFacade.NonBlankValueOfDecimal(txtDiscount.Text.ToString());
            Common.Common.totalAdvAdj = objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString());

            Common.Common.totalDue = objBusinessFacade.NonBlankValueOfDecimal(lblTotalDueAmt.Text.ToString());

            if (Common.Common.totalDue >= (Common.Common.totalAmountPaid + Common.Common.totalDiscount + Common.Common.totalAdvAdj) && Common.Common.totalAdvAdj <= objBusinessFacade.NonBlankValueOfDecimal(lblAdvBal.Text.ToString()))
            {
                if (Common.Common.totalAmountPaid > 0)
                {
                    for (int i = 0; i < dgvSettleAllDue.Rows.Count; i++)
                    {
                        if (Common.Common.totalAmountPaid >= objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value)))
                        {
                            dgvSettleAllDue.Rows[i].Cells["Amount Paid"].Value = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value)).ToString();
                            Common.Common.totalAmountPaid = Common.Common.totalAmountPaid - objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value));
                            dgvSettleAllDue.Rows[i].Cells["Due"].Value = "0.00";
                        }
                        else
                        {
                            dgvSettleAllDue.Rows[i].Cells["Amount Paid"].Value = (Common.Common.totalAmountPaid).ToString();
                            dgvSettleAllDue.Rows[i].Cells["Due"].Value = (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value)) - Common.Common.totalAmountPaid).ToString();
                            Common.Common.totalAmountPaid = 0;
                        }
                    }
                }

                if (Common.Common.totalDiscount > 0)
                {
                    for (int i = 0; i < dgvSettleAllDue.Rows.Count; i++)
                    {
                        if (Common.Common.totalDiscount >= objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value)))
                        {
                            dgvSettleAllDue.Rows[i].Cells["Discount"].Value = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value)).ToString();
                            Common.Common.totalDiscount = Common.Common.totalDiscount - objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value));
                            dgvSettleAllDue.Rows[i].Cells["Due"].Value = "0.00";
                        }
                        else
                        {
                            dgvSettleAllDue.Rows[i].Cells["Discount"].Value = (Common.Common.totalDiscount).ToString();
                            dgvSettleAllDue.Rows[i].Cells["Due"].Value = (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value)) - Common.Common.totalDiscount).ToString();
                            Common.Common.totalDiscount = 0;
                        }
                    }
                }

                if (Common.Common.totalAdvAdj > 0)
                {
                    for (int i = 0; i < dgvSettleAllDue.Rows.Count; i++)
                    {
                        if (Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Department"].Value) == "IP Billing")
                        {
                            if (Common.Common.totalAdvAdj >= objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value)))
                            {
                                dgvSettleAllDue.Rows[i].Cells["Adv Adj"].Value = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value)).ToString();
                                Common.Common.totalAdvAdj = Common.Common.totalAdvAdj - objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value));
                                dgvSettleAllDue.Rows[i].Cells["Due"].Value = "0.00";
                            }
                            else
                            {
                                dgvSettleAllDue.Rows[i].Cells["Adv Adj"].Value = (Common.Common.totalAdvAdj).ToString();
                                dgvSettleAllDue.Rows[i].Cells["Due"].Value = (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[i].Cells["Due"].Value)) - Common.Common.totalAdvAdj).ToString();
                                Common.Common.totalAdvAdj = 0;
                            }
                        }
                    }
                }
                btnAdd.Enabled = false;
            }
            else
            {
                MessageBox.Show("Amount Paid or Discount can't be greater than Total Due || Adv Adj can't be Greater than Total Advance....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmountPaid.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtAdvAdj.Text = string.Empty;
                txtAmountPaid.Focus();
                btnAdd.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAmountPaid.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtAdvAdj.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Control_KeyPress_digit(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void dgvSettleAllDue_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvSettleAllDue.Columns.Contains("Amount Paid"))
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress_digit);
            }

            if (dgvSettleAllDue.Columns.Contains("Discount"))
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress_digit);
            }

            if (dgvSettleAllDue.Columns.Contains("Adv Adj"))
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress_digit);
            }
        }

        private void dgvSettleAllDue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            /*if (dgvSettleAllDue.Rows.Count > 1)
            {
                int rowIndex = dgvSettleAllDue.CurrentRow.Index;
                decimal dueAmt = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[rowIndex].Cells["Due"].Value));
                decimal amtPaid = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[rowIndex].Cells["Amount Paid"].Value));

                if (dgvSettleAllDue.Columns[e.ColumnIndex].Name.Equals("Discount") && !string.IsNullOrEmpty(Convert.ToString(dgvSettleAllDue.Rows[rowIndex].Cells["Discount"].Value)) && objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[rowIndex].Cells["Discount"].Value)) <= (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[rowIndex].Cells["Due"].Value)) - (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[rowIndex].Cells["Amount Paid"].Value)))))
                {
                    dgvSettleAllDue.Rows[rowIndex].Cells["Due"].Value = (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[rowIndex].Cells["Due"].Value)) - objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[rowIndex].Cells["Discount"].Value)) - objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvSettleAllDue.Rows[rowIndex].Cells["Amount Paid"].Value))).ToString();
                }
                else if (dgvSettleAllDue.Columns[e.ColumnIndex].Name.Equals("Discount"))
                {
                    dgvSettleAllDue.Rows[rowIndex].Cells["Discount"].Value = "0.00";
                    dgvSettleAllDue.Rows[rowIndex].Cells["Due"].Value = (dueAmt - amtPaid).ToString();
                }
            }*/
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvSettleAllDue.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvSettleAllDue.Rows)
                {
                    if (objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["bill_id"].Value)) > 0 && (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Discount"].Value)) > 0 || objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Amount Paid"].Value)) > 0 || objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Adv Adj"].Value)) > 0))
                    {
                        Common.Common.billId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["bill_id"].Value));
                        Common.Common.cis_department.departmentName = Convert.ToString(row.Cells["Department"].Value);
                        Common.Common.totalDiscount = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Discount"].Value));
                        Common.Common.totalAmountPaid = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Amount Paid"].Value));
                        Common.Common.totalAdvAdj = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Adv Adj"].Value));
                        Common.Common.totalDue = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Due"].Value));

                        if (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Adv Adj"].Value)) > 0)
                        {
                            dtSource = objBusinessFacade.getNetAdvAvailbyPatientId(Common.Common.cis_number_generation.patient_id, Common.Common.cis_number_generation.visit_number);

                            if (dtSource.Rows.Count > 0)
                            {
                                Common.Common.cis_billing.totalAdvanceAvailable = objBusinessFacade.NonBlankValueOfDecimal(dtSource.Rows[0]["net_amount"].ToString());
                            }

                            Common.Common.cis_billing.totalAdvNetColl = Common.Common.cis_billing.totalAdvanceAvailable - Common.Common.totalAdvAdj;
                        }

                        if (DateTime.Parse(Convert.ToString(row.Cells["Bill Date"].Value)).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            Common.Common.totalAmountPaid = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Amount Paid"].Value));
                            Common.Common.totalDueCollection = 0;
                        }
                        else
                        {
                            Common.Common.totalAmountPaid = 0;
                            Common.Common.totalDueCollection = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Amount Paid"].Value));
                        }

                        if (Common.Common.totalDue == 0)
                        {
                            Common.Common.status = 1;//Fully Paid
                        }
                        else
                        {
                            Common.Common.status = 3;//Partially Paid
                        }

                        Common.Common.paymentModeId = Convert.ToInt32(cboPaymentMode.SelectedIndex.ToString());
                        Common.Common.cardNumber = txtCardNumber.Text.ToString();
                        Common.Common.bankName = txtBankName.Text.ToString();
                        Common.Common.holderName = txtHolderName.Text.ToString();

                        objArg.ParamList["bill_id"] = Common.Common.billId;
                        objArg.ParamList["discount"] = Common.Common.totalDiscount;
                        objArg.ParamList["amount_paid"] = Common.Common.totalAmountPaid;
                        objArg.ParamList["due_collection"] = Common.Common.totalDueCollection;
                        objArg.ParamList["pay_from_advance"] = Common.Common.totalAdvAdj;
                        objArg.ParamList["ward_Adv_Net_Coll"] = Common.Common.cis_billing.totalAdvNetColl;
                        objArg.ParamList["due"] = Common.Common.totalDue;
                        objArg.ParamList["status"] = Common.Common.status;
                        objArg.ParamList["transaction_user_id"] = Common.Common.userId;

                        objArg.ParamList["patient_id"] = Common.Common.cis_number_generation.patient_id;
                        objArg.ParamList["visit_number"] = Common.Common.cis_number_generation.visit_number;
                        //objArg.ParamList["patient_name"] = Common.Common.cis_patient_info.patient_name;


                        objArg.ParamList["payment_mode_id"] = Common.Common.paymentModeId;
                        objArg.ParamList["card_number"] = Common.Common.cardNumber;
                        objArg.ParamList["bank_name"] = Common.Common.bankName;
                        objArg.ParamList["holder_name"] = Common.Common.holderName;

                        switch (Common.Common.cis_department.departmentName)
                        {
                            case "Registration":
                                Common.Common.flag = objBusinessFacade.updateRegistrationDueCollection(objArg);
                                Common.Common.flag = objBusinessFacade.insertRegistrationSummaryDueCollection(objArg);
                                break;

                            case "Investigation":
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
                }
                if (Common.Common.flag == 0)
                {
                    MessageBox.Show("Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
            }
        }

        private void cboPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.Common.paymentModeId = Convert.ToInt32(cboPaymentMode.SelectedIndex.ToString());

            if (Common.Common.paymentModeId == 0)
            {
                txtCardNumber.Enabled = false;
                txtBankName.Enabled = false;
                txtHolderName.Enabled = false;
                txtCardNumber.Text = string.Empty;
                txtBankName.Text = string.Empty;
                txtHolderName.Text = string.Empty;
            }
            else
            {
                if (Common.Common.paymentModeId == 1 || Common.Common.paymentModeId == 2)
                {
                    lblCardNumber.Text = "Card No";
                }
                else if (Common.Common.paymentModeId == 3)
                {
                    lblCardNumber.Text = "Cheque No";
                }
                else
                {
                    lblCardNumber.Text = "DD No";
                }

                txtCardNumber.Enabled = true;
                txtBankName.Enabled = true;
                txtHolderName.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSettleAllDue.Rows.Count > 0)
                {
                    string regBillId = string.Empty;
                    string invBillId = string.Empty;
                    string phaBillId = string.Empty;
                    string genBillId = string.Empty;
                    string wardBillId = string.Empty;

                    StringBuilder regBillIds = new StringBuilder();
                    StringBuilder invBillIds = new StringBuilder();
                    StringBuilder phaBillIds = new StringBuilder();
                    StringBuilder genBillIds = new StringBuilder();
                    StringBuilder wardBillIds = new StringBuilder();

                    foreach (DataGridViewRow row in dgvSettleAllDue.Rows)
                    {
                        //if (objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["bill_id"].Value)) > 0 && (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Discount"].Value)) > 0 || objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Amount Paid"].Value)) > 0 || objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["Adv Adj"].Value)) > 0))
                        {
                            //Common.Common.billId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["bill_id"].Value));
                            Common.Common.cis_department.departmentName = Convert.ToString(row.Cells["Department"].Value);

                            //objArg.ParamList["bill_id"] = Common.Common.billId;

                            switch (Common.Common.cis_department.departmentName)
                            {
                                case "Registration":
                                    regBillId = Convert.ToString(row.Cells["bill_id"].Value);
                                    regBillIds.Append(regBillId).Append(",");
                                    break;

                                case "Investigation":
                                    invBillId = Convert.ToString(row.Cells["bill_id"].Value);
                                    invBillIds.Append(invBillId).Append(",");
                                    break;

                                case "Pharmacy":
                                    phaBillId = Convert.ToString(row.Cells["bill_id"].Value);
                                    phaBillIds.Append(phaBillId).Append(",");
                                    break;

                                case "General":
                                    genBillId = Convert.ToString(row.Cells["bill_id"].Value);
                                    genBillIds.Append(genBillId).Append(",");
                                    break;

                                case "IP Billing":
                                    wardBillId = Convert.ToString(row.Cells["bill_id"].Value);
                                    wardBillIds.Append(wardBillId).Append(",");
                                    break;

                                default:
                                    break;
                            }
                        }

                        /*else
                        {
                            MessageBox.Show("No settlements are done...!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }*/
                    }

                    if (!(string.IsNullOrEmpty(regBillIds.ToString())))
                    {
                        regBillIds = regBillIds.Remove(regBillIds.Length - 1, 1);
                    }

                    else
                    {
                        regBillIds.Append("0");
                    }

                    if (!(string.IsNullOrEmpty(invBillIds.ToString())))
                    {
                        invBillIds = invBillIds.Remove(invBillIds.Length - 1, 1);
                    }
                    else
                    {
                        invBillIds.Append("0");
                    }

                    if (!(string.IsNullOrEmpty(phaBillIds.ToString())))
                    {
                        phaBillIds = phaBillIds.Remove(phaBillIds.Length - 1, 1);
                    }
                    else
                    {
                        phaBillIds.Append("0");
                    }

                    if (!(string.IsNullOrEmpty(genBillIds.ToString())))
                    {
                        genBillIds = genBillIds.Remove(genBillIds.Length - 1, 1);
                    }
                    else
                    {
                        genBillIds.Append("0");
                    }

                    if (!(string.IsNullOrEmpty(wardBillIds.ToString())))
                    {
                        wardBillIds = wardBillIds.Remove(wardBillIds.Length - 1, 1);
                    }
                    else
                    {
                        wardBillIds.Append("0");
                    }

                    objArg.ParamList["regBillIds"] = regBillIds.ToString();
                    objArg.ParamList["invBillIds"] = invBillIds.ToString();
                    objArg.ParamList["phaBillIds"] = phaBillIds.ToString();
                    objArg.ParamList["genBillIds"] = genBillIds.ToString();
                    objArg.ParamList["wardBillIds"] = wardBillIds.ToString();

                    CIS.BillTemplates.frmPrintReceipt frmDueCollRececipt = new BillTemplates.frmPrintReceipt("DueCollectionBill", objArg);
                    frmDueCollRececipt.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Settle All Due", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
