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
    public partial class cisAdvanceCollection : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cisAdvanceCollection()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearAdvanceDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPatientId_function();
                e.Handled = true;
            }
        }

        private void txtPatientId_function()
        {
            dtSource = objBusinessFacade.getPatientDetailsByPatientId(txtPatientId.Text.ToString());
            if (dtSource.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtSource.Rows[0]["bed_number"].ToString()))
                {
                    lblPatientName.Text = dtSource.Rows[0]["patient_name"].ToString();
                    lblGender.Text = dtSource.Rows[0]["gender_name"].ToString();
                    lblAge.Text = string.Concat(dtSource.Rows[0]["age_year"].ToString(), "Y ", dtSource.Rows[0]["age_month"].ToString(), "M ", dtSource.Rows[0]["age_day"].ToString(), "D");
                    lblVisitNo.Text = dtSource.Rows[0]["last_visit_number"].ToString();
                    lblVisitDate.Text = dtSource.Rows[0]["visit_date"].ToString();
                    lblBedDetails.Text = string.Concat(dtSource.Rows[0]["ward_name"].ToString(), ", ", dtSource.Rows[0]["room_no"].ToString(), ", ", dtSource.Rows[0]["bed_number"].ToString());

                    dtSource = objBusinessFacade.getNetAdvAvailbyPatientId(txtPatientId.Text.ToString(), lblVisitNo.Text.ToString());

                    if (dtSource.Rows.Count > 0)
                    {
                        lblTotalAdvAvail.Text = dtSource.Rows[0]["net_amount"].ToString();
                    }
                    else
                    {
                        lblTotalAdvAvail.Text = "0.00";
                    }
                }

                else
                {
                    MessageBox.Show("Patient is not admitted....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clearAdvanceDetails();
                }
            }
            else
            {
                MessageBox.Show("Invalid Patient Id....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearAdvanceDetails();
            }
        }

        private void clearAdvanceDetails()
        {
            txtPatientId.Text = string.Empty;
            lblPatientName.Text = string.Empty;
            lblVisitNo.Text = string.Empty;
            lblVisitDate.Text = string.Empty;
            lblGender.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblBedDetails.Text = string.Empty;
            txtAdvanceColl.Text = string.Empty;
            txtAdvRefund.Text = string.Empty;
            rBtnAdvColl.Checked = true;
            txtAdvRefund.Enabled = false;
            btnSave.Enabled = true;
            lblTotalAdvAvail.Text = string.Empty;
        }

        private void rBtnAdvColl_CheckedChanged(object sender, EventArgs e)
        {
            txtAdvanceColl.Text = string.Empty;
            txtAdvRefund.Text = string.Empty;
            txtAdvRefund.Enabled = false;
            txtAdvanceColl.Enabled = true;
        }

        private void rbtnAdvRefund_CheckedChanged(object sender, EventArgs e)
        {
            txtAdvanceColl.Text = string.Empty;
            txtAdvRefund.Text = string.Empty;
            txtAdvRefund.Enabled = true;
            txtAdvanceColl.Enabled = false;
        }

        private void cisAdvanceCollection_Load(object sender, EventArgs e)
        {
            rBtnAdvColl.Checked = true;
            txtAdvRefund.Enabled = false;
            cboPaymentMode.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblVisitNo.Text.ToString()) && (objBusinessFacade.NonBlankValueOfDecimal(txtAdvanceColl.Text.ToString()) > 0 || objBusinessFacade.NonBlankValueOfDecimal(txtAdvRefund.Text.ToString()) > 0))
            {
                Common.Common.cis_number_generation.patient_id = Convert.ToString(txtPatientId.Text.ToString());
                Common.Common.cis_number_generation.visit_number = Convert.ToString(lblVisitNo.Text.ToString());
                Common.Common.cis_patient_info.patient_name = Convert.ToString(lblPatientName.Text.ToString());
                Common.Common.cis_billing.advanceCollection = objBusinessFacade.NonBlankValueOfDecimal(txtAdvanceColl.Text.ToString());
                Common.Common.cis_billing.advanceRefund = objBusinessFacade.NonBlankValueOfDecimal(txtAdvRefund.Text.ToString());
                Common.Common.cis_billing.totalAdvanceAvailable = objBusinessFacade.NonBlankValueOfDecimal(lblTotalAdvAvail.Text.ToString());

                ComArugments objArgN = objBusinessFacade.getAdvanceTransNumberFormat();

                if (rBtnAdvColl.Checked == true)
                {
                    Common.Common.cis_number_generation.running_adv_collection_number = Convert.ToInt32(objArgN.ParamList["running_adv_collection_number"]);
                    Common.Common.cis_number_generation.adv_collection_number = Convert.ToString(objArgN.ParamList["adv_collection_number"]);
                    Common.Common.cis_billing.totalAdvNetColl = Common.Common.cis_billing.totalAdvanceAvailable + Common.Common.cis_billing.advanceCollection;
                    lblAdvBillNo.Text = Common.Common.cis_number_generation.adv_collection_number;
                }

                else
                {
                    Common.Common.cis_number_generation.running_adv_refund_number = Convert.ToInt32(objArgN.ParamList["running_adv_refund_number"]);
                    Common.Common.cis_number_generation.adv_refund_number = Convert.ToString(objArgN.ParamList["adv_refund_number"]);
                    Common.Common.cis_billing.totalAdvNetColl = Common.Common.cis_billing.totalAdvanceAvailable - Common.Common.cis_billing.advanceRefund;
                    lblAdvBillNo.Text = Common.Common.cis_number_generation.adv_refund_number;
                }

                objArg.ParamList["patient_id"] = Common.Common.cis_number_generation.patient_id;
                objArg.ParamList["visit_number"] = Common.Common.cis_number_generation.visit_number;
                objArg.ParamList["patient_name"] = Common.Common.cis_patient_info.patient_name;
                objArg.ParamList["advance_collection"] = Common.Common.cis_billing.advanceCollection;
                objArg.ParamList["advance_refund"] = Common.Common.cis_billing.advanceRefund;
                objArg.ParamList["total_adv_net_coll"] = Common.Common.cis_billing.totalAdvNetColl;
                objArg.ParamList["running_adv_collection_number"] = Common.Common.cis_number_generation.running_adv_collection_number;
                objArg.ParamList["adv_collection_number"] = Common.Common.cis_number_generation.adv_collection_number;
                objArg.ParamList["running_adv_refund_number"] = Common.Common.cis_number_generation.running_adv_refund_number;
                objArg.ParamList["adv_refund_number"] = Common.Common.cis_number_generation.adv_refund_number;

                //Begin Payment Mode Details
                Common.Common.paymentModeId = Convert.ToInt32(cboPaymentMode.SelectedIndex.ToString());
                Common.Common.cardNumber = txtCardNumber.Text.ToString();
                Common.Common.bankName = txtBankName.Text.ToString();
                Common.Common.holderName = txtHolderName.Text.ToString();

                objArg.ParamList["transaction_user_id"] = Common.Common.userId;
                objArg.ParamList["payment_mode_id"] = Common.Common.paymentModeId;
                objArg.ParamList["card_number"] = Common.Common.cardNumber;
                objArg.ParamList["bank_name"] = Common.Common.bankName;
                objArg.ParamList["holder_name"] = Common.Common.holderName;
                //End Payment Mode Details

                Common.Common.flag = objBusinessFacade.advanceTransaction(objArg);

                if (Common.Common.flag == 0)
                {
                    MessageBox.Show("Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please Enter mandatory fields...!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtAdvanceColl_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtAdvRefund_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
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
            if (!string.IsNullOrEmpty(lblAdvBillNo.Text.ToString()))
            {
                CIS.BillTemplates.frmPrintReceipt frmAdvanceRececipt = new BillTemplates.frmPrintReceipt("AdvanceBill", lblAdvBillNo.Text.ToString(), "PatientId");
                frmAdvanceRececipt.ShowDialog();
            }
        }

        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            //txtPatientId_function();
        }

        private void txtPatientId_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPatientId_function();
            }
            e.IsInputKey = true;
        }
    }
}
