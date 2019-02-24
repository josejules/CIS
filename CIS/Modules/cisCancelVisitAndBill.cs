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
    public partial class cisCancelVisitAndBill : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cisCancelVisitAndBill()
        {
            InitializeComponent();
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
            dtSource = objBusinessFacade.getVisitandBillDetailsByPatientId(txtPatientId.Text.ToString());
            if (dtSource.Rows.Count > 0)
            {
                lblPatientName.Text = dtSource.Rows[0]["patient_name"].ToString();
                lblVisitNo.Text = dtSource.Rows[0]["visit_number"].ToString();
                lblVisitDate.Text = dtSource.Rows[0]["visit_date"].ToString();
                lblVisitType.Text = dtSource.Rows[0]["visit_type"].ToString();
                lblBedDetails.Text = dtSource.Rows[0]["ward_details"].ToString();
                lblBillNumber.Text = dtSource.Rows[0]["bill_number"].ToString();
                lblBillDate.Text = dtSource.Rows[0]["bill_date"].ToString();
                lblBillAmount.Text = dtSource.Rows[0]["bill_amount"].ToString();
                lblDiscount.Text = dtSource.Rows[0]["discount"].ToString();
                lblAmountPaid.Text = dtSource.Rows[0]["amount_paid"].ToString();
                lblDue.Text = dtSource.Rows[0]["due"].ToString();
                lblRefundToPatient.Text = dtSource.Rows[0]["amount_paid"].ToString();
                lblBillId.Text = dtSource.Rows[0]["reg_bill_id"].ToString();
                if (dtSource.Rows[0]["visit_status"].ToString() == "0")
                {
                    lblVisitStatus.Text = "Cancelled";
                    cbCancelVisit.Checked = false;
                    cbCancelVisit.Enabled = false;
                }
                else
                {
                    lblVisitStatus.Text = "Available";
                    cbCancelVisit.Checked = true;
                    cbCancelVisit.Enabled = true;
                }

                if (dtSource.Rows[0]["bill_status"].ToString() == "4")
                {
                    lblBillStatus.Text = "Cancelled";
                    cbCancelRegBill.Checked = false;
                    cbCancelRegBill.Enabled = false;
                }
                else
                {
                    lblBillStatus.Text = "Available";
                    cbCancelRegBill.Checked = true;
                    cbCancelRegBill.Enabled = true;
                }

                if (dtSource.Rows[0]["visit_status"].ToString() == "0" && dtSource.Rows[0]["bill_status"].ToString() == "4")
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                }

                btnSave.Focus();
            }
            else
            {
                MessageBox.Show("Invalid Patient Id....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearCancelVisitAndBill();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblVisitNo.Text.ToString()) && (cbCancelVisit.Checked == true || cbCancelRegBill.Checked == true))
            {
                if (cbCancelVisit.Checked == true && !string.IsNullOrEmpty(lblVisitNo.Text.ToString()))
                {
                    Common.Common.cis_number_generation.patient_id = Convert.ToString(txtPatientId.Text.ToString());
                    Common.Common.cis_number_generation.visit_number = Convert.ToString(lblVisitNo.Text.ToString());

                    objArg.ParamList["patient_id"] = Common.Common.cis_number_generation.patient_id;
                    objArg.ParamList["visit_number"] = Common.Common.cis_number_generation.visit_number;

                    Common.Common.flag = objBusinessFacade.cancelVisitInfo(objArg);
                }

                if (cbCancelRegBill.Checked == true && !string.IsNullOrEmpty(lblVisitNo.Text.ToString()))
                {
                    Common.Common.cis_number_generation.reg_bill_number = Convert.ToString(lblBillNumber.Text.ToString());
                    Common.Common.billId = Convert.ToInt32(lblBillId.Text.ToString());
                    Common.Common.module_visit_info.refundToPatientReg = Convert.ToDecimal(lblRefundToPatient.Text.ToString());

                    objArg.ParamList["bill_number"] = Common.Common.cis_number_generation.reg_bill_number;
                    objArg.ParamList["bill_id"] = Common.Common.billId;
                    objArg.ParamList["refund_to_patient"] = Common.Common.module_visit_info.refundToPatientReg;

                    Common.Common.flag = objBusinessFacade.cancelRegBillInfo(objArg);
                }
            }

            else
            {
                MessageBox.Show("Patient Id is Mandatory or Cancellation is not enabled", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (Common.Common.flag == 0)
            {
                MessageBox.Show("Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cancelled Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                Common.Common.flag = 0;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearCancelVisitAndBill();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearCancelVisitAndBill()
        {
            txtPatientId.Text = string.Empty;
            lblPatientName.Text = string.Empty;
            lblVisitNo.Text = string.Empty;
            lblVisitDate.Text = string.Empty;
            lblVisitType.Text = string.Empty;
            lblBillNumber.Text = string.Empty;
            lblBillDate.Text = string.Empty;
            lblBillAmount.Text = string.Empty;
            lblDiscount.Text = string.Empty;
            lblAmountPaid.Text = string.Empty;
            lblDue.Text = string.Empty;
            lblVisitStatus.Text = string.Empty;
            lblBillStatus.Text = string.Empty;
            cbCancelVisit.Checked = false;
            cbCancelRegBill.Checked = false;
            cbCancelVisit.Enabled = true;
            cbCancelRegBill.Enabled = true;
            btnSave.Enabled = true;
            lblBillId.Text = string.Empty;
            lblBedDetails.Text = string.Empty;
            lblRefundToPatient.Text = string.Empty;
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
