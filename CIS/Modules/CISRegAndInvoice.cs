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
    public partial class CISRegAndInvoice : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        public static DataTable dtPhaItemDetails = new DataTable();
        ComArugments objArg = new ComArugments();
        public static int[] daysOfMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public static int[] daysOfMonthLY = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        #endregion

        #region Constructor
        public CISRegAndInvoice()
        {
            InitializeComponent();
            cboGender.SelectedIndex = 1;
            cboGenderInv.SelectedIndex = 1;
            cboRegDiscountType.SelectedIndex = 0;
            tscmbRegistrationType.ComboBox.SelectedIndex = 0;
            cboDiscountTypeInv.SelectedIndex = 0;
            //cboDoctor.SelectedIndex = 0;
            //this.tscmbDepartment.Focus();
        }
        public CISRegAndInvoice(string patient_id)
        {
            InitializeComponent();
            cboGender.SelectedIndex = 1;
            cboGenderInv.SelectedIndex = 1;
            cboRegDiscountType.SelectedIndex = 0;
            tscmbRegistrationType.ComboBox.SelectedIndex = 1;
            cboDiscountTypeInv.SelectedIndex = 0;
            txtPatientId.Text = patient_id;
            //cboDoctor.SelectedIndex = 0;
            //this.tscmbDepartment.Focus();
        }
        #endregion

        #region Events
        private void CISRegAndInvoice_Load(object sender, EventArgs e)
        {
            //multitxtAddress.AutoCompleteCustomSource =source,au
            this.dtpVisitDate.Value = DateTime.Now;
            loadRegistrationDepartment();
            loadDoctor();
            loadSocialTitle();
            loadDoctorInv();
            loadPatientType();
            loadAddress();
            loadWard();
            loadCorporate();
            loadClinicalType();
            loadInvestigation();
            loadPhaItem();
            loadGeneralCharges();
            loadReferralBy();
            //dtpDOB.Format = DateTimePickerFormat.Custom;
            //dtpDOB.CustomFormat = "";
            lblBillDateInv.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            lblcCancelledDate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            cboPaymentMode.SelectedIndex = 0;
            cboSocialTitle.SelectedIndex = -1;
            cboClinicalType.SelectedValue = 16; //General Medicine By Default

            dtSource = null;
            dtSource = objBusinessFacade.getUserRoleId(Common.Common.userId);
            if (dtSource.Rows.Count > 0)
            {
                Common.Common.userRoleId = Convert.ToInt32(dtSource.Rows[0]["user_role_id"].ToString());
                disableTabControls(Common.Common.userRoleId);
            }
        }

        private void multitxtAddress_TextChanged(object sender, EventArgs e)
        {
            //if(multitxtAddress.Text
            //dtSource = objBusinessFacade.fetchAddressInfobyPlace(multitxtAddress.Text.ToString());
            //dgvCancellationInvoice.Columns[12].ReadOnly = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Are you sure to Save the record?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            try
            {
                saveInputValues();
                btnPrint_Click(sender, e);
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearRegRecords();
            clearInvoicePatientInfo();
            clearInvestigationRecords();
            clearPharmacyRecords();
            clearGeneralInputValues();
            clearGenRecords();
            clearCancelBillRecords();
            lblTotalCollectAmt.Text = "0.00";
            Common.Common.flag = 0;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            print_bill();
        }

        private void print_bill()
        {
            if (!string.IsNullOrEmpty(lblBillNo.Text))
            {
                CIS.BillTemplates.frmPrintReceipt frmRegRececipt = new BillTemplates.frmPrintReceipt("RegistrationBill", lblBillNo.Text, txtPatientId.Text);
                frmRegRececipt.ShowDialog();
            }

            if (tscmbRegistrationType.ComboBox.SelectedIndex == 0 && !(string.IsNullOrEmpty(txtPatientId.Text)))
            {
                CIS.BillTemplates.frmPrintReceipt frmRegSlip = new BillTemplates.frmPrintReceipt("RegistrationSlip", Common.Common.billNo, txtPatientId.Text);
                frmRegSlip.ShowDialog();
            }

            if (!string.IsNullOrEmpty(lblBillNoInvestigation.Text))
            {
                CIS.BillTemplates.frmPrintReceipt frmInvRececipt = new BillTemplates.frmPrintReceipt("InvestigationBill", lblBillNoInvestigation.Text, txtPatientId.Text);
                frmInvRececipt.ShowDialog();
            }

            if (!string.IsNullOrEmpty(lblBillNoPha.Text))
            {
                CIS.BillTemplates.frmPrintReceipt frmPhaRececipt = new BillTemplates.frmPrintReceipt("PharmacyBill", lblBillNoPha.Text, txtPatientId.Text);
                frmPhaRececipt.ShowDialog();
            }

            if (!string.IsNullOrEmpty(lblGenBillNo.Text))
            {
                CIS.BillTemplates.frmPrintReceipt frmGenRececipt = new BillTemplates.frmPrintReceipt("GeneralBill", lblGenBillNo.Text, txtPatientId.Text);
                frmGenRececipt.ShowDialog();
            }

            if (!string.IsNullOrEmpty(txtcBillNo.Text))//Cancellation Bill Printout
            {
                if (objBusinessFacade.NonBlankValueOfInt(lblcBillType.Text) == 6) //Included By Jules
                {
                    CIS.BillTemplates.frmPrintReceipt frmPhaRececipt = new BillTemplates.frmPrintReceipt("PharmacyBill", txtcBillNo.Text, txtPatientId.Text);
                    frmPhaRececipt.ShowDialog();
                }
                if (objBusinessFacade.NonBlankValueOfInt(lblcBillType.Text) == 3)
                {
                    CIS.BillTemplates.frmPrintReceipt frmInvRececipt = new BillTemplates.frmPrintReceipt("InvestigationBill", lblcBillType.Text, txtPatientId.Text);
                    frmInvRececipt.ShowDialog();
                }

                else if (objBusinessFacade.NonBlankValueOfInt(lblcBillType.Text) == 4)
                {
                    CIS.BillTemplates.frmPrintReceipt frmPhaRececipt = new BillTemplates.frmPrintReceipt("PharmacyBill", lblcBillType.Text, txtPatientId.Text);
                    frmPhaRececipt.ShowDialog();
                }

                else if (objBusinessFacade.NonBlankValueOfInt(lblcBillType.Text) == 7)
                {
                    CIS.BillTemplates.frmPrintReceipt frmGenRececipt = new BillTemplates.frmPrintReceipt("GeneralBill", lblcBillType.Text, txtPatientId.Text);
                    frmGenRececipt.ShowDialog();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboWardNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWardNo.SelectedIndex > 0)
            {
                Common.Common.cis_Room.ward_id = Convert.ToInt32(cboWardNo.SelectedValue.ToString());
                loadRoom(Common.Common.cis_Room.ward_id);
            }
        }

        protected void tscmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscmbDepartment.ComboBox.Items.Count > 0)
            {
                Common.Common.cis_department.departmentId = Convert.ToInt32(tscmbDepartment.ComboBox.SelectedValue.ToString());
                try
                {
                    dtSource = objBusinessFacade.getDepartmentCategoryId(Common.Common.cis_department.departmentId);
                    Common.Common.cis_department.departmentCategoryId = Convert.ToInt32(dtSource.Rows[0]["DEPARTMENT_CATEGORY_ID"].ToString());

                    if (Common.Common.cis_department.departmentCategoryId == 1)
                    {
                        cboWardNo.Enabled = false;
                        cboRoomNo.Enabled = false;
                        cboBedNo.Enabled = false;
                    }
                    else
                    {
                        cboWardNo.Enabled = true;
                        cboRoomNo.Enabled = true;
                        cboBedNo.Enabled = true;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void cboDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDoctor.Items.Count > 0)
            {
                viewDoctorFeeByCorporate();
                //viewDoctorFee();
                calculateRegFee();
            }
        }

        private void cboDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculateRegFee();
        }

        private void txtAgeYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
            txtAgeYear.MaxLength = 3;
        }

        private void txtAgeMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
            txtAgeMonth.MaxLength = 2;
        }

        private void txtAgeDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
            txtAgeDay.MaxLength = 3;
        }

        private void dtpVisitDate_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtpVisitDate.Value.Date.ToString("yyyy/MM/dd hh:mm tt")) >= Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd hh:mm tt")))
            {
                MessageBox.Show("Visit Date can't be in Future Date....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.dtpVisitDate.Value = DateTime.Now;
            }
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDOB.Value >= DateTime.Now)
            {
                MessageBox.Show("DOB can't be in Future Date....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                /*dtpDOB.Format = DateTimePickerFormat.Custom;
                dtpDOB.CustomFormat = "";
                //dtpDOB.Value = DateTime.FromOADate(0);*/
            }
            else
            {
                //DateTime dob = Convert.ToDateTime("1988/12/20");
                //string tYMD = BusinessFacade.BusinessFacade.CalculateYourAge(dateofBirth);
                //string[] YMD = tYMD.Split(',');
                //txtAgeYear.Text = YMD[0].ToString();
                //txtAgeMonth.Text = YMD[1].ToString();
                //txtAgeDay.Text = YMD[2].ToString();
                CalculateYourAge(dtpDOB.Value);
                txtGuardainName.Focus();
                //int age = CalculateAge(dob);  

            }

        }

        public void CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;

            txtAgeYear.Text = Years.ToString();
            txtAgeMonth.Text = Months.ToString();
            txtAgeDay.Text = Days.ToString();
        }

        private void tscmbRegistrationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clearRegRecords();
            RegistrationType_function();
        }

        private void RegistrationType_function()
        {
            if (tscmbRegistrationType.ComboBox.SelectedIndex == 0) //New Reg
            {
                txtPatientId.Enabled = false;
                cboClinicalType.Enabled = true;
                cboDoctor.Enabled = true;
                txtDiagnosis.Enabled = true;
                cboRegDiscountType.Enabled = true;
                txtRegDiscount.Enabled = true;
                txtRegDiscount.Enabled = true;
                txtConsultationFee.Enabled = true;
                txtRegAmountPaid.Enabled = true;
                btnSearchPatient.Enabled = false;
                //cboDoctor.SelectedIndex = 0;
                cboSocialTitle.Focus();
            }

            else if (tscmbRegistrationType.ComboBox.SelectedIndex == 2) // Modify Reg
            {
                txtPatientId.Enabled = true;
                cboClinicalType.Enabled = false;
                cboDoctor.Enabled = false;
                txtDiagnosis.Enabled = false;
                cboRegDiscountType.Enabled = false;
                txtRegDiscount.Enabled = false;
                txtRegDiscount.Enabled = false;
                txtConsultationFee.Enabled = false;
                txtRegAmountPaid.Enabled = false;
                cboWardNo.SelectedIndex = -1;
                cboRoomNo.SelectedIndex = -1;
                cboBedNo.SelectedIndex = -1;
                btnSearchPatient.Enabled = true;
                cboDoctor.SelectedIndex = 1;
                txtPatientId.Focus();
            }

            else // Revisit
            {
                txtPatientId.Enabled = true;
                cboClinicalType.Enabled = true;
                cboDoctor.Enabled = true;
                txtDiagnosis.Enabled = true;
                cboRegDiscountType.Enabled = true;
                txtRegDiscount.Enabled = true;
                txtRegDiscount.Enabled = true;
                txtConsultationFee.Enabled = true;
                txtRegAmountPaid.Enabled = true;
                btnSearchPatient.Enabled = true;
                //cboDoctor.SelectedIndex = 0;
                txtPatientId.Focus();
            }
        }

        private void tscmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                tscmbRegistrationType.Focus();
                e.Handled = true;
            }
        }

        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPatientId_function();
                e.Handled = true;
            }
        }

        public void txtPatientId_function()
        {
            DataTable dtPatient = objBusinessFacade.getPatientDetailsByPatientId(txtPatientId.Text.ToString());
            if (dtPatient.Rows.Count > 0)
            {
                if ((string.IsNullOrEmpty(dtPatient.Rows[0]["patAdmissionStatus"].ToString())) || tscmbRegistrationType.ComboBox.SelectedIndex == 2)
                {
                    txtPatientId.Text = dtPatient.Rows[0]["patient_id"].ToString();
                    txtPatientName.Text = dtPatient.Rows[0]["patient_name"].ToString();

                    /*if (!string.IsNullOrEmpty(dtSource.Rows[0]["patient_type_id"].ToString()) && Convert.ToInt32(dtSource.Rows[0]["patient_type_id"].ToString()) > 0)
                    {
                        cboSocialTitle.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["social_title_id"].ToString());
                    }*/

                    cboPatientType.SelectedValue = Convert.ToInt32(dtPatient.Rows[0]["patient_type_id"].ToString());

                    cboGender.SelectedIndex = Convert.ToInt32(dtPatient.Rows[0]["gender"].ToString());
                    txtAgeYear.Text = dtPatient.Rows[0]["age_year"].ToString();
                    txtAgeMonth.Text = dtPatient.Rows[0]["age_month"].ToString();
                    txtAgeDay.Text = dtPatient.Rows[0]["age_day"].ToString();
                    txtGuardainName.Text = dtPatient.Rows[0]["guardian_name"].ToString();
                    multitxtAddress.Text = dtPatient.Rows[0]["address"].ToString();
                    txtPhoneNo.Text = dtPatient.Rows[0]["phone_no"].ToString();
                    cboSocialTitle.SelectedValue = Convert.ToInt32(dtPatient.Rows[0]["social_title_id"].ToString());
                    cboCorporate.SelectedValue = Convert.ToInt32(dtPatient.Rows[0]["corporate_id"].ToString());
                    txtEmployeeId.Text = dtPatient.Rows[0]["employee_id"].ToString();
                    cboClinicalType.Focus();
                    if (cboDoctor.Items.Count > 0)
                    {
                        viewDoctorFee();
                        calculateRegFee();
                    }
                }
                else
                {
                    MessageBox.Show("Patient is occupied Bed....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid Patient Id....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPatientId.Text = string.Empty;
            }
        }

        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboPatientType.Select();
                e.Handled = true;
            }
        }

        private void cboPatientType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (cboGender.Enabled == false)
                {
                    txtAgeYear.Focus();
                }
                else
                {
                    cboGender.Focus();
                }
                e.Handled = true;
            }
        }

        private void cboGender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtAgeYear.Focus();
                e.Handled = true;
            }
        }

        private void txtAgeYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                CalculateYourDob();
                e.Handled = true;
            }
        }

        private void txtAgeMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                CalculateYourDob();
                e.Handled = true;
            }
        }

        private void txtAgeDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                CalculateYourDob();
                e.Handled = true;
            }
        }

        private void dtpDOB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtGuardainName.Focus();
                e.Handled = true;
            }
        }

        private void txtGuardainName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboAddress1.Focus();
                e.Handled = true;
            }
        }

        private void multitxtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab)
            //{
            //    txtPhoneNo.Focus();
            //    e.Handled = true;
            //}
        }

        private void txtPhoneNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboClinicalType.Focus();
                e.Handled = true;
            }
        }

        private void cboCorporate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboClinicalType.Focus();
                e.Handled = true;
            }
        }

        private void cboClinicalType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboDoctor.Focus();
                e.Handled = true;
            }
        }

        private void cboDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtDiagnosis.Focus();
                e.Handled = true;
            }
        }

        private void txtDiagnosis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboRegDiscountType.Focus();
                e.Handled = true;
            }
        }

        private void cboDiscountType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtRegDiscount.Focus();
                e.Handled = true;
            }
        }

        // Registration Amount Paid
        private void txtAmountPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtAmountPaid_function();
            }
        }

        private void txtAmountPaid_function()
        {
            if (Common.Common.module_visit_info.netTotalReg >= objBusinessFacade.NonBlankValueOfDecimal(txtRegAmountPaid.Text.ToString()))
            {
                if (cboWardNo.Enabled == false)
                {
                    btnSave.Focus();
                }
                else
                {
                    cboWardNo.Focus();
                }
                Common.Common.module_visit_info.amountPaidReg = objBusinessFacade.NonBlankValueOfDecimal(txtRegAmountPaid.Text.ToString());
                Common.Common.module_visit_info.balanceAmountReg = (Common.Common.module_visit_info.netTotalReg - Common.Common.module_visit_info.amountPaidReg);
                lblRegBalance.Text = Convert.ToString(Common.Common.module_visit_info.balanceAmountReg);
            }
            else
            {
                MessageBox.Show("Amount can't be greater than Net Total....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegAmountPaid.Text = Convert.ToString(Common.Common.module_visit_info.netTotalReg);
            }
        }

        private void cboWardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboRoomNo.Focus();
                e.Handled = true;
            }
        }

        private void cboRoomNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboBedNo.Focus();
                e.Handled = true;
            }
        }

        private void cboBedNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnSave.Focus();
                e.Handled = true;
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtRegAmountPaid.Select();
                e.Handled = true;
                calculateRegFee();
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtAmountPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtPatientIdInv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPatientIdInv_function();
                e.Handled = true;
            }
        }

        private void txtPatientIdInv_function()
        {
            dtSource = null;
            dtSource = objBusinessFacade.getPatientDetailsByPatientId(txtPatientIdInv.Text.ToString());
            if (dtSource.Rows.Count > 0)
            {
                txtPatientIdInv.Text = dtSource.Rows[0]["patient_id"].ToString();
                txtPatientNameInv.Text = dtSource.Rows[0]["patient_name"].ToString();
                cboGenderInv.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["gender"].ToString());
                txtAgeYearInv.Text = dtSource.Rows[0]["age_year"].ToString();
                txtAgeMonthInv.Text = dtSource.Rows[0]["age_month"].ToString();
                txtAgeDayInv.Text = dtSource.Rows[0]["age_day"].ToString();
                multitxtAddressInv.Text = dtSource.Rows[0]["address"].ToString();
                lblVisitNumberInv.Text = dtSource.Rows[0]["last_visit_number"].ToString();
                lblVisitTypeInv.Text = dtSource.Rows[0]["visit_type"].ToString();
                lblCorporateName.Text = dtSource.Rows[0]["corporate_name"].ToString();
                cboDoctorInv.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["doctor_id"].ToString());
                txtPatientNameInv.Enabled = false;
                cboGenderInv.Enabled = false;
                multitxtAddressInv.Enabled = false;
                cboInvCodeInvestigation.Focus();
            }
            else
            {
                MessageBox.Show("Invalid Patient Id....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPatientIdInv.Text = string.Empty;
            }
        }

        private void cboInvCodeInvestigation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInvCodeInvestigation.Items.Count > 1 && cboInvCodeInvestigation.SelectedIndex > 0)
            {
                Common.Common.cis_investigation_info.investigationId = Convert.ToInt32(cboInvCodeInvestigation.SelectedValue.ToString());

                try
                {
                    dtSource = null;
                    dtSource = objBusinessFacade.getInvestigationById(Common.Common.cis_investigation_info.investigationId);
                    if (dtSource.Rows.Count > 0)
                    {
                        Common.Common.cis_investigation_info.investigationCode = Convert.ToString(dtSource.Rows[0]["investigation_code"].ToString());
                        Common.Common.cis_investigation_info.investigationName = Convert.ToString(dtSource.Rows[0]["investigation_name"].ToString());
                        Common.Common.cis_investigation_info.investigationDeptId = Convert.ToInt32(dtSource.Rows[0]["department_id"].ToString());
                        Common.Common.cis_investigation_info.investigationDeptName = Convert.ToString(dtSource.Rows[0]["department_name"].ToString());
                        txtUnitPriceInvestigation.Text = Convert.ToString(dtSource.Rows[0]["unit_price"].ToString());
                        lblShareType.Text = Convert.ToString(dtSource.Rows[0]["share_type"].ToString());
                        lblShareAmt.Text = Convert.ToString(dtSource.Rows[0]["share_amt"].ToString());

                        lblInvNameInvestigation.Text = Common.Common.cis_investigation_info.investigationName;
                        txtQtyInvestigation.Text = "1";
                        txtQtyInvestigation.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void txtQtyInvestigation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
        }

        private void txtUnitPriceInvestigation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtQtyInvestigation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (!string.IsNullOrEmpty(Common.Common.cis_investigation_info.investigationCode) && Common.Common.cis_investigation_info.investigationId > 0 && objBusinessFacade.NonBlankValueOfInt(txtQtyInvestigation.Text.ToString()) > 0)
                {
                    Common.Common.cis_investigation_info.investigationQty = Convert.ToInt32(txtQtyInvestigation.Text.ToString());
                    Common.Common.cis_investigation_info.investigationUnitPrice = Convert.ToDecimal(txtUnitPriceInvestigation.Text.ToString());
                    Common.Common.cis_investigation_info.ShareType = objBusinessFacade.NonBlankValueOfInt(lblShareType.Text.ToString());
                    Common.Common.cis_investigation_info.shareAmt = (Common.Common.cis_investigation_info.investigationQty * objBusinessFacade.NonBlankValueOfDecimal(lblShareAmt.Text.ToString()));
                    Common.Common.cis_investigation_info.investigationTotal = (Common.Common.cis_investigation_info.investigationQty * Common.Common.cis_investigation_info.investigationUnitPrice);
                    lblTotalAmtInv.Text = Convert.ToString(Common.Common.cis_investigation_info.investigationTotal);

                    if (Common.Common.cis_investigation_info.investigationQty > 0)
                    {
                        if (string.IsNullOrEmpty(lblCheckEditModeInv.Text.ToString()))//Add Item
                        {
                            bool entryFound = false;
                            foreach (DataGridViewRow row in dgvInvestigationBill.Rows)//Check Item exits aleady
                            {
                                int invId = Convert.ToInt32(row.Cells["InvestigationId"].Value);
                                if (invId == Common.Common.cis_investigation_info.investigationId)
                                {
                                    MessageBox.Show("Already Exists....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    entryFound = true;
                                    cboInvCodeInvestigation.Focus();
                                }
                            }
                            if (!entryFound)//If not add the item
                            {
                                dgvInvestigationBill.Rows.Add(dgvInvestigationBill.Rows.Count + 1, Common.Common.cis_investigation_info.investigationId, Common.Common.cis_investigation_info.investigationDeptId, Common.Common.cis_investigation_info.investigationCode, Common.Common.cis_investigation_info.investigationName, Common.Common.cis_investigation_info.investigationDeptName, Common.Common.cis_investigation_info.investigationQty, Common.Common.cis_investigation_info.investigationUnitPrice, Common.Common.cis_investigation_info.investigationTotal, Common.Common.cis_investigation_info.ShareType, Common.Common.cis_investigation_info.shareAmt);
                                clearInvestigationInputValues();
                                cboInvCodeInvestigation.Focus();
                                calculateInvSum();
                            }
                        }

                        else //Edit Item
                        {
                            int rowNo = Convert.ToInt32(lblCheckEditModeInv.Text.ToString());
                            dgvInvestigationBill.Rows[rowNo].Cells["InvestigationId"].Value = Common.Common.cis_investigation_info.investigationId;
                            dgvInvestigationBill.Rows[rowNo].Cells["departmentId"].Value = Common.Common.cis_investigation_info.investigationDeptId;
                            dgvInvestigationBill.Rows[rowNo].Cells["Code"].Value = Common.Common.cis_investigation_info.investigationCode;
                            dgvInvestigationBill.Rows[rowNo].Cells["InvestigationName"].Value = Common.Common.cis_investigation_info.investigationName;
                            dgvInvestigationBill.Rows[rowNo].Cells["InvestigationDepartment"].Value = Common.Common.cis_investigation_info.investigationDeptName;
                            dgvInvestigationBill.Rows[rowNo].Cells["Qty"].Value = txtQtyInvestigation.Text.ToString();
                            dgvInvestigationBill.Rows[rowNo].Cells["UnitPrice"].Value = txtUnitPriceInvestigation.Text.ToString();
                            dgvInvestigationBill.Rows[rowNo].Cells["shareAmt"].Value = Common.Common.cis_investigation_info.shareAmt;
                            dgvInvestigationBill.Rows[rowNo].Cells["shareType"].Value = Common.Common.cis_investigation_info.ShareType;
                            dgvInvestigationBill.Rows[rowNo].Cells["TotalAmount"].Value = Common.Common.cis_investigation_info.investigationTotal;
                            clearInvestigationInputValues();
                            cboInvCodeInvestigation.Focus();
                            calculateInvSum();
                        }
                    }

                    else
                    {
                        MessageBox.Show("Fields are required....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnInvClear_Click(object sender, EventArgs e)
        {
            clearInvestigationRecords();
        }

        private void cboItemPha_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboItemPha.Items.Count > 1 && cboItemPha.SelectedIndex > 0)
            {
                Common.Common.cis_pharmacy_info.phaItemId = Convert.ToInt32(cboItemPha.SelectedValue.ToString());

                try
                {
                    Common.Common.cis_pharmacy_info.phaTotalQty = 0;
                    dtSource = null;
                    dtSource = objBusinessFacade.getPhaByItemId(Common.Common.cis_pharmacy_info.phaItemId);
                    DataTable dtSourceC = dtSource;
                    dtPhaItemDetails = dtSource;
                    if (dtSourceC != null && dtSourceC.Rows.Count > 0)
                    {
                        cboLotIdPha.ValueMember = "inventory_stock_id";
                        cboLotIdPha.DisplayMember = "lot_id";
                        cboLotIdPha.DataSource = dtSourceC;

                        Common.Common.cis_pharmacy_info.inventoryStockId = Convert.ToInt32(lblinventoryStockId.Text.ToString());
                    }

                    else
                    {
                        MessageBox.Show("Found no stock for this Item....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Common.Common.cis_pharmacy_info.inventoryStockId = 0;
                    }

                    if (Common.Common.cis_pharmacy_info.inventoryStockId > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dtSource.Rows)
                        {
                            Common.Common.cis_pharmacy_info.phaTotalQty += Convert.ToInt32(dtSource.Rows[i]["avail_qty"].ToString());
                            i++;
                        }
                        //string filterField = Common.Common.cis_pharmacy_info.inventoryStockId.ToString();
                        //dtSourceC.DefaultView.RowFilter = string.Format("[{0}] ='{1}'", "inventory_stock_id", Common.Common.cis_pharmacy_info.inventoryStockId);
                        //dgvDoctor.DataSource = dtSource;
                        //dtSource.Select("inventory_stock_id = InvStockId").Any();
                        Common.Common.cis_pharmacy_info.inventoryStockId = Convert.ToInt32(dtSource.Rows[0]["inventory_stock_id"].ToString());
                        Common.Common.cis_pharmacy_info.phaItemName = Convert.ToString(dtSource.Rows[0]["item_name"].ToString());
                        Common.Common.cis_pharmacy_info.phaDeptId = Convert.ToInt32(dtSource.Rows[0]["department_id"].ToString());
                        Common.Common.cis_pharmacy_info.phaItemTypeId = Convert.ToInt32(dtSource.Rows[0]["item_type_id"].ToString());
                        Common.Common.cis_pharmacy_info.phaItemType = Convert.ToString(dtSource.Rows[0]["item_type"].ToString());
                        Common.Common.cis_pharmacy_info.lotId = Convert.ToString(dtSource.Rows[0]["lot_id"].ToString());
                        Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToDateTime(dtSource.Rows[0]["exp_date"].ToString()).ToString("MM/yyyy");
                        Common.Common.cis_pharmacy_info.phaQty = Convert.ToInt32(dtSource.Rows[0]["avail_qty"].ToString());
                        Common.Common.cis_pharmacy_info.phaUnitPrice = Convert.ToDecimal(dtSource.Rows[0]["mrp"].ToString());
                        Common.Common.cis_pharmacy_info.phaFreeCare = Convert.ToDecimal(dtSource.Rows[0]["default_discount"].ToString());
                        Common.Common.cis_pharmacy_info.salesTaxPerc = Convert.ToDecimal(dtSource.Rows[0]["sales_tax_perc"].ToString());
                        lblItemTypePha.Text = Common.Common.cis_pharmacy_info.phaItemType; ;
                        txtUnitPricePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaUnitPrice);
                        lblFreeCarePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaFreeCare);
                        lblExpDatePha.Text = Common.Common.cis_pharmacy_info.phaExpDate;
                        lblExpDateFull.Text = dtSource.Rows[0]["exp_date"].ToString();
                        lblAvailQtyPha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaQty);
                        lblSaleTaxPerc.Text = Convert.ToString(Common.Common.cis_pharmacy_info.salesTaxPerc);
                        lblTotalAvailQtyPha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaTotalQty);
                        txtQtyPha.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void txtQtyPha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
        }

        private void txtQtyPha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (!string.IsNullOrEmpty(Common.Common.cis_pharmacy_info.phaItemName) && Common.Common.cis_pharmacy_info.phaItemId > 0 && !string.IsNullOrEmpty(txtQtyPha.Text.ToString()) && Convert.ToInt32(txtQtyPha.Text.ToString()) != 0)
                {
                    Common.Common.cis_pharmacy_info.phaQty = Convert.ToInt32(txtQtyPha.Text.ToString());
                    Common.Common.cis_pharmacy_info.phaUnitPrice = Convert.ToDecimal(txtUnitPricePha.Text.ToString());
                    Common.Common.cis_pharmacy_info.phaFreeCareTotal = (Common.Common.cis_pharmacy_info.phaQty * Common.Common.cis_pharmacy_info.phaFreeCare);
                    Common.Common.cis_pharmacy_info.phaTotalAmt = (Common.Common.cis_pharmacy_info.phaQty * Common.Common.cis_pharmacy_info.phaUnitPrice);
                    lblTotalAmtPha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaTotalAmt);

                    if (Common.Common.cis_pharmacy_info.phaQty > 0)
                    {
                        if (Common.Common.cis_pharmacy_info.phaQty <= objBusinessFacade.NonBlankValueOfInt(lblTotalAvailQtyPha.Text.ToString()))
                        {
                            /*if (string.IsNullOrEmpty(lblCheckEditModePha.Text.ToString()))//Add Item
                            {
                                bool entryFound = false;
                                foreach (DataGridViewRow row in dgvPharmacyBillDetails.Rows)
                                {
                                    int phaId = Convert.ToInt32(row.Cells["inventoryStockId"].Value);
                                    if (phaId == Common.Common.cis_pharmacy_info.inventoryStockId)
                                    {
                                        MessageBox.Show("Already Exists....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        entryFound = true;
                                        cboItemPha.Focus();
                                    }
                                }
                                if (!entryFound)
                                {
                                    dgvPharmacyBillDetails.Rows.Add(dgvPharmacyBillDetails.Rows.Count + 1, Common.Common.cis_pharmacy_info.phaItemId, Common.Common.cis_pharmacy_info.phaItemName, Common.Common.cis_pharmacy_info.phaItemTypeId, Common.Common.cis_pharmacy_info.phaItemType, Common.Common.cis_pharmacy_info.lotId, Common.Common.cis_pharmacy_info.phaExpDate, Common.Common.cis_pharmacy_info.phaQty, Common.Common.cis_pharmacy_info.phaUnitPrice, Common.Common.cis_pharmacy_info.phaFreeCareTotal, Common.Common.cis_pharmacy_info.phaTotalAmt, Common.Common.cis_pharmacy_info.phaDeptId, Common.Common.cis_pharmacy_info.inventoryStockId, Common.Common.cis_pharmacy_info.salesTaxPerc);
                                    clearPhaInputValues();
                                    cboItemPha.Focus();
                                    calculatePhaSum();
                                }
                            }
                            else //Edit Item
                            {
                                int rowNo = Convert.ToInt32(lblCheckEditModePha.Text.ToString());
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["ItemIdPha"].Value = Common.Common.cis_pharmacy_info.phaItemId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["ItemNamePha"].Value = Common.Common.cis_pharmacy_info.phaItemName;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["itemTypeId"].Value = Common.Common.cis_pharmacy_info.phaItemTypeId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["ItemTypePha"].Value = Common.Common.cis_pharmacy_info.phaItemType;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["LotIdPha"].Value = Common.Common.cis_pharmacy_info.lotId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["ExpDatePha"].Value = Common.Common.cis_pharmacy_info.phaExpDate;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["QtyPha"].Value = Common.Common.cis_pharmacy_info.phaQty;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["UnitPricePha"].Value = Common.Common.cis_pharmacy_info.phaUnitPrice;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["FreeCarePha"].Value = Common.Common.cis_pharmacy_info.phaFreeCareTotal;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["TotalAmtPha"].Value = Common.Common.cis_pharmacy_info.phaTotalAmt;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["phaDeptId"].Value = Common.Common.cis_pharmacy_info.phaDeptId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["inventoryStockId"].Value = Common.Common.cis_pharmacy_info.inventoryStockId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["sales_tax_perc"].Value = Common.Common.cis_pharmacy_info.salesTaxPerc;
                                clearPhaInputValues();
                                cboItemPha.Focus();
                                calculatePhaSum();
                            }*/

                            if (string.IsNullOrEmpty(lblCheckEditModePha.Text.ToString()))//Add Item
                            {
                                bool entryFound = false;
                                foreach (DataGridViewRow row in dgvPharmacyBillDetails.Rows)
                                {
                                    int phaId = Convert.ToInt32(row.Cells["inventoryStockId"].Value);
                                    if (phaId == Common.Common.cis_pharmacy_info.inventoryStockId)
                                    {
                                        MessageBox.Show("Already Exists....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        entryFound = true;
                                        cboItemPha.Focus();
                                        return;
                                    }
                                }
                                //Logic -> Get Next availble batches if issue qty more than current batch //Implemented By : Jules
                                if (Common.Common.cis_pharmacy_info.phaQty >= Convert.ToDecimal(lblAvailQtyPha.Text))
                                {
                                    decimal issuedQty = 0;
                                    decimal issuedQtyBatch = 0;
                                    foreach (DataRow row in dtPhaItemDetails.Rows)
                                    {
                                        if (issuedQty < Common.Common.cis_pharmacy_info.phaQty)
                                        {
                                            if (issuedQty + Convert.ToDecimal(row["avail_qty"].ToString()) > Common.Common.cis_pharmacy_info.phaQty)
                                                issuedQtyBatch = Common.Common.cis_pharmacy_info.phaQty - issuedQty;
                                            else
                                                issuedQtyBatch = Convert.ToDecimal(row["avail_qty"].ToString());
                                            Common.Common.cis_pharmacy_info.phaTotalAmt = issuedQtyBatch * Convert.ToDecimal(row["mrp"].ToString());
                                            Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToDateTime(row["exp_date"].ToString()).ToString("MM/yyyy");
                                            dgvPharmacyBillDetails.Rows.Add(dgvPharmacyBillDetails.Rows.Count + 1, Common.Common.cis_pharmacy_info.phaItemId, Common.Common.cis_pharmacy_info.phaItemName, Common.Common.cis_pharmacy_info.phaItemTypeId, Common.Common.cis_pharmacy_info.phaItemType, row["lot_id"].ToString(),
                                                Common.Common.cis_pharmacy_info.phaExpDate, issuedQtyBatch, Convert.ToDecimal(row["mrp"].ToString()), Convert.ToDecimal(row["default_discount"].ToString()), Common.Common.cis_pharmacy_info.phaTotalAmt, Common.Common.cis_pharmacy_info.phaDeptId, Common.Common.cis_pharmacy_info.inventoryStockId, Convert.ToDouble(row["sales_tax_perc"].ToString()));
                                        }
                                        issuedQty += issuedQtyBatch;
                                    }
                                }
                                else
                                {
                                    if ((!entryFound))
                                        dgvPharmacyBillDetails.Rows.Add(dgvPharmacyBillDetails.Rows.Count + 1, Common.Common.cis_pharmacy_info.phaItemId, Common.Common.cis_pharmacy_info.phaItemName, Common.Common.cis_pharmacy_info.phaItemTypeId, Common.Common.cis_pharmacy_info.phaItemType, Common.Common.cis_pharmacy_info.lotId, Common.Common.cis_pharmacy_info.phaExpDate, Common.Common.cis_pharmacy_info.phaQty, Common.Common.cis_pharmacy_info.phaUnitPrice, Common.Common.cis_pharmacy_info.phaFreeCareTotal, Common.Common.cis_pharmacy_info.phaTotalAmt, Common.Common.cis_pharmacy_info.phaDeptId, Common.Common.cis_pharmacy_info.inventoryStockId, Common.Common.cis_pharmacy_info.salesTaxPerc);
                                }
                                clearPhaInputValues();
                                cboItemPha.Focus();
                                calculatePhaSum();
                            }

                            else //Edit Item
                            {
                                int rowNo = Convert.ToInt32(lblCheckEditModePha.Text.ToString());
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["ItemIdPha"].Value = Common.Common.cis_pharmacy_info.phaItemId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["ItemNamePha"].Value = Common.Common.cis_pharmacy_info.phaItemName;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["itemTypeId"].Value = Common.Common.cis_pharmacy_info.phaItemTypeId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["ItemTypePha"].Value = Common.Common.cis_pharmacy_info.phaItemType;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["LotIdPha"].Value = Common.Common.cis_pharmacy_info.lotId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["ExpDatePha"].Value = Common.Common.cis_pharmacy_info.phaExpDate;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["QtyPha"].Value = Common.Common.cis_pharmacy_info.phaQty;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["UnitPricePha"].Value = Common.Common.cis_pharmacy_info.phaUnitPrice;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["FreeCarePha"].Value = Common.Common.cis_pharmacy_info.phaFreeCareTotal;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["TotalAmtPha"].Value = Common.Common.cis_pharmacy_info.phaTotalAmt;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["phaDeptId"].Value = Common.Common.cis_pharmacy_info.phaDeptId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["inventoryStockId"].Value = Common.Common.cis_pharmacy_info.inventoryStockId;
                                dgvPharmacyBillDetails.Rows[rowNo].Cells["sales_tax_perc"].Value = Common.Common.cis_pharmacy_info.salesTaxPerc;
                                clearPhaInputValues();
                                cboItemPha.Focus();
                                calculatePhaSum();
                            }
                        }

                        else
                        {
                            MessageBox.Show("Issued qty can't be greater than Total Avail Qty....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    else
                    {
                        MessageBox.Show("Fields are required....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnPhaClear_Click(object sender, EventArgs e)
        {
            clearPharmacyRecords();
        }

        private void cboLotIdPha_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.Common.cis_pharmacy_info.inventoryStockId = Convert.ToInt32(cboLotIdPha.SelectedValue);
            lblinventoryStockId.Text = Common.Common.cis_pharmacy_info.inventoryStockId.ToString();

            if (dtSource.Rows.Count > 0 && cboLotIdPha.SelectedValue != null)
            {
                DataView view = dtSource.Copy().DefaultView;
                view.RowFilter = "inventory_stock_id = " + cboLotIdPha.SelectedValue;

                DataTable dtSelectedBatch = view.ToTable();
                if (dtSelectedBatch.Rows.Count > 0)
                {
                    Common.Common.cis_pharmacy_info.phaItemType = Convert.ToString(dtSelectedBatch.Rows[0]["item_type"].ToString());
                    Common.Common.cis_pharmacy_info.lotId = Convert.ToString(dtSelectedBatch.Rows[0]["lot_id"].ToString());
                    Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToDateTime(dtSelectedBatch.Rows[0]["exp_date"].ToString()).ToString("MM/yyyy");
                    Common.Common.cis_pharmacy_info.phaQty = Convert.ToInt32(dtSelectedBatch.Rows[0]["avail_qty"].ToString());
                    Common.Common.cis_pharmacy_info.phaUnitPrice = Convert.ToDecimal(dtSelectedBatch.Rows[0]["mrp"].ToString());
                    Common.Common.cis_pharmacy_info.phaFreeCare = Convert.ToDecimal(dtSelectedBatch.Rows[0]["default_discount"].ToString());
                    Common.Common.cis_pharmacy_info.salesTaxPerc = Convert.ToDecimal(dtSelectedBatch.Rows[0]["sales_tax_perc"].ToString());

                    lblItemTypePha.Text = Common.Common.cis_pharmacy_info.phaItemType; ;
                    txtUnitPricePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaUnitPrice);
                    lblFreeCarePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaFreeCare);
                    lblExpDatePha.Text = Common.Common.cis_pharmacy_info.phaExpDate;
                    lblAvailQtyPha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaQty);
                    lblSaleTaxPerc.Text = Convert.ToString(Common.Common.cis_pharmacy_info.salesTaxPerc);
                    lblTotalAvailQtyPha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaTotalQty);
                    txtQtyPha.Focus();
                }
            }
        }

        private void txtAmountPaidInv_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tpRegInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tpRegInvoice.SelectedIndex == 0)
            {
                lblTotalCollectAmt.Text = Common.Common.module_visit_info.netTotalReg.ToString("0.00");
            }
            else if (string.IsNullOrWhiteSpace(txtcBillNo.Text) && tpRegInvoice.SelectedIndex == 2)
            {
                lblTotalCollectAmt.Text = Common.Common.module_visit_info.netTotalReg.ToString("0.00");
            }
            else if (string.IsNullOrWhiteSpace(txtPatientIdInv.Text) && tpRegInvoice.SelectedIndex == 1)
            {
                lblTotalCollectAmt.Text = Common.Common.module_visit_info.netTotalReg.ToString("0.00");
            }
            else
            {
                lblTotalCollectAmt.Text = Common.Common.totalInvoice.ToString("0.00");
            }
        }

        private void lblDiscountAmtInv_TextChanged(object sender, EventArgs e)
        {
            Common.Common.totalDiscount = Convert.ToDecimal(lblDiscountAmtInv.Text.ToString());

            if (Convert.ToDecimal(lblGrandTotalInv.Text.ToString()) >= Common.Common.totalAmountPaid)
            {

            }
            else
            {
                MessageBox.Show("Discount can't be greater than Net Total....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtDiscountInvestigation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtDiscountInvestigation_Leave(sender, e);
                #region CommentedLinesofJesu
                //txtAmtPaidInvestigation.Select();
                //e.Handled = true;

                //Common.Common.cis_investigation_info.investigationTotal = Convert.ToDecimal(lblInvestigationTotalAmt.Text.ToString());
                //Common.Common.cis_investigation_info.investigationDiscountAmt = Convert.ToDecimal(txtDiscountInvestigation.Text.ToString());
                //Common.Common.cis_investigation_info.amountPaidInvestigation = Convert.ToDecimal(txtAmtPaidInvestigation.Text.ToString());
                //Common.Common.cis_investigation_info.balanceAmountInvestigation = (Common.Common.cis_investigation_info.investigationTotal - Common.Common.cis_investigation_info.amountPaidInvestigation);

                //if (!string.IsNullOrEmpty(txtDiscountInvestigation.Text))
                //{
                //    if (Common.Common.cis_investigation_info.balanceAmountInvestigation >= Common.Common.cis_investigation_info.investigationDiscountAmt)
                //    {
                //        Common.Common.cis_investigation_info.balanceAmountInvestigation = (Common.Common.cis_investigation_info.investigationTotal - Common.Common.cis_investigation_info.investigationDiscountAmt - Common.Common.cis_investigation_info.amountPaidInvestigation);
                //        lblDueInvestigation.Text = Convert.ToString(Common.Common.cis_investigation_info.balanceAmountInvestigation);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Discount Amount can't be greater than Bill Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txtDiscountInvestigation.Text = "0.00";
                //        Common.Common.cis_investigation_info.balanceAmountInvestigation = (Common.Common.cis_investigation_info.investigationTotal - Common.Common.cis_investigation_info.amountPaidInvestigation);
                //        lblDueInvestigation.Text = Convert.ToString(Common.Common.cis_investigation_info.balanceAmountInvestigation);
                //        txtDiscountInvestigation.Select();
                //    }
                //}
                //else
                //{
                //    Common.Common.cis_investigation_info.investigationDiscountAmt = 0;
                //}
                #endregion

            }
        }

        private void txtDiscountPha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtDiscountPha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtDiscountPha_Leave(sender, e);
                #region CommentedLinesofJesu
                //txtAmountPaidPha.Select();
                //e.Handled = true;

                //Common.Common.cis_pharmacy_info.phaTotalAmt = Convert.ToDecimal(lblPharmacyTotalAmt.Text.ToString());
                //Common.Common.cis_pharmacy_info.phaDiscountAmt = Convert.ToDecimal(string.IsNullOrEmpty(txtDiscountPha.Text) ? "0.00" : txtDiscountPha.Text);
                //Common.Common.cis_pharmacy_info.phaFreeCareTotal = Convert.ToDecimal(lblTotalFreeCare.Text.ToString());
                //Common.Common.cis_pharmacy_info.phaAmtPaid = Convert.ToDecimal(txtAmountPaidPha.Text.ToString());
                //Common.Common.cis_pharmacy_info.balanceAmountPha = (Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaAmtPaid - Common.Common.cis_pharmacy_info.phaFreeCareTotal);

                //if (!string.IsNullOrEmpty(txtDiscountPha.Text))
                //{
                //    if (Common.Common.cis_pharmacy_info.balanceAmountPha >= Common.Common.cis_pharmacy_info.phaDiscountAmt)
                //    {
                //        Common.Common.cis_pharmacy_info.balanceAmountPha = (Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaDiscountAmt - Common.Common.cis_pharmacy_info.phaFreeCareTotal - Common.Common.cis_pharmacy_info.phaAmtPaid);
                //        lblDuePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.balanceAmountPha);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Discount Amount can't be greater than Bill Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txtDiscountPha.Text = "0.00";
                //        Common.Common.cis_pharmacy_info.balanceAmountPha = (Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaFreeCareTotal - Common.Common.cis_pharmacy_info.phaAmtPaid);
                //        lblDuePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.balanceAmountPha);
                //        txtDiscountPha.Select();
                //    }
                //}
                //else
                //{
                //    Common.Common.cis_pharmacy_info.phaDiscountAmt = 0;
                //}
                #endregion
            }
        }

        private void txtDiscountPha_Leave(object sender, EventArgs e)
        {
            txtDiscountPha.Text = AutoFormatToDecimalValue(txtDiscountPha.Text);
            if (Convert.ToDouble(txtDiscountPha.Text) > Convert.ToDouble(lblPharmacyTotalAmt.Text))
            {
                MessageBox.Show("Discount Amount should not be greater than Bill Amount!", "Lab Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDiscountPha.Focus();
                txtDiscountPha.Text = "0.00";
                CalculatePharmacyDue();
                return;
            }
            if (Convert.ToDouble(txtDiscountPha.Text) + Convert.ToDouble(txtAmountPaidPha.Text) + Convert.ToDouble(txtAdvAdjPha.Text) >
                Convert.ToDouble(lblPharmacyTotalAmt.Text))
            {
                double NetTotal = Convert.ToDouble(lblPharmacyTotalAmt.Text) - Convert.ToDouble(txtDiscountPha.Text);
                if (Convert.ToDouble(txtAmountPaidPha.Text) > 0)
                {
                    txtAmountPaidPha.Text = NetTotal.ToString("############0.00");
                    txtAmountPaidPha.Focus();
                }
            }
            CalculatePharmacyDue();
            txtAmountPaidPha.Focus();
        }

        private void txtAmountPaidPha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtAmountPaidPha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                amountPaidPha_function();
                #region CommentedLinesofJesu
                /*if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //txtAmountPaidPha_Leave(sender, e);
                
                //txtAdvAdjPha.Select();
                //e.Handled = true;

                //Common.Common.cis_pharmacy_info.phaTotalAmt = Convert.ToDecimal(lblPharmacyTotalAmt.Text.ToString());
                //Common.Common.cis_pharmacy_info.phaDiscountAmt = Convert.ToDecimal(txtDiscountPha.Text.ToString());
                //Common.Common.cis_pharmacy_info.phaFreeCareTotal = Convert.ToDecimal(lblTotalFreeCare.Text.ToString());
                //Common.Common.cis_pharmacy_info.phaAmtPaid = Convert.ToDecimal(string.IsNullOrEmpty(txtAmountPaidPha.Text) ? "0.00" : txtAmountPaidPha.Text);
                //Common.Common.cis_pharmacy_info.balanceAmountPha = (Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaDiscountAmt - Common.Common.cis_pharmacy_info.phaFreeCareTotal);

                //if (!string.IsNullOrEmpty(txtAmountPaidPha.Text))
                //{
                //    if (Common.Common.cis_pharmacy_info.balanceAmountPha >= Common.Common.cis_pharmacy_info.phaAmtPaid)
                //    {
                //        Common.Common.cis_pharmacy_info.balanceAmountPha = (Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaDiscountAmt - Common.Common.cis_pharmacy_info.phaFreeCareTotal - Common.Common.cis_pharmacy_info.phaAmtPaid);
                //        lblDuePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.balanceAmountPha);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Amount can't be greater than Net Total....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txtAmountPaidPha.Text = "0.00";
                //        Common.Common.cis_pharmacy_info.balanceAmountPha = (Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaDiscountAmt - Common.Common.cis_pharmacy_info.phaFreeCareTotal);
                //        lblDuePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.balanceAmountPha);
                //        txtAmountPaidPha.Select();
                //    }
                //}
                //else
                //{
                //    Common.Common.cis_pharmacy_info.phaAmtPaid = 0;
                //}
            }*/
                #endregion
            }
        }

        private void txtAmountPaidPha_Leave(object sender, EventArgs e)
        {
            amountPaidPha_function();
        }

        private void amountPaidPha_function()
        {
            txtAmountPaidPha.Text = AutoFormatToDecimalValue(txtAmountPaidPha.Text);
            if (Convert.ToDouble(txtAmountPaidPha.Text) + Convert.ToDouble(txtAdvAdjPha.Text) >
                Convert.ToDouble(lblPharmacyTotalAmt.Text) - Convert.ToDouble(txtDiscountPha.Text))
            {
                MessageBox.Show("Amount Paid should not be greater than Bill Amount!", "Pharmacy Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAmountPaidPha.Focus();
                txtAmountPaidPha.Text = "0.00";

                CalculatePharmacyDue();
                return;
            }
            CalculatePharmacyDue();
            btnSave.Focus();
        }

        private void CalculatePharmacyDue()
        {
            //Calculate Due
            Double labPhar = Convert.ToDouble(lblPharmacyTotalAmt.Text.ToString()) - (Convert.ToDouble(txtAmountPaidPha.Text.ToString()) +
                Convert.ToDouble(txtAdvAdjPha.Text.ToString()) + Convert.ToDouble(txtDiscountPha.Text.ToString()));
            lblDuePha.Text = labPhar.ToString("############0.00");
            lblTotalCollectAmt.Text = (Convert.ToDouble(lblDueInvestigation.Text) + Convert.ToDouble(lblGenDueAmt.Text) + labPhar).ToString("############0.00");
        }

        private void txtAgeYearInv_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
            txtAgeYearInv.MaxLength = 3;
        }

        private void txtAgeMonthInv_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
            txtAgeMonthInv.MaxLength = 2;
        }

        private void txtAgeDayInv_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
            txtAgeDayInv.MaxLength = 3;
        }

        private void dgvInvestigationBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInvestigationBill.Rows.Count > 0)
            {
                if (dgvInvestigationBill.Columns[e.ColumnIndex].Name.Equals("invEdit"))//Edit Button Click
                {
                    cboInvCodeInvestigation.SelectedValue = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvInvestigationBill.Rows[e.RowIndex].Cells["InvestigationId"].Value));
                    txtQtyInvestigation.Text = dgvInvestigationBill.Rows[e.RowIndex].Cells["Qty"].Value.ToString();
                    txtUnitPriceInvestigation.Text = dgvInvestigationBill.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString();
                    lblTotalAmtInv.Text = dgvInvestigationBill.Rows[e.RowIndex].Cells["TotalAmount"].Value.ToString();
                    lblShareType.Text = dgvInvestigationBill.Rows[e.RowIndex].Cells["shareType"].Value.ToString();
                    lblShareAmt.Text = dgvInvestigationBill.Rows[e.RowIndex].Cells["shareAmt"].Value.ToString();
                    lblCheckEditModeInv.Text = e.RowIndex.ToString();
                }

                if (dgvInvestigationBill.Columns[e.ColumnIndex].Name.Equals("invDelete"))//Delete Button Click
                {
                    if (MessageBox.Show("Are you sure to Delete the record?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvInvestigationBill.Rows.Remove(dgvInvestigationBill.Rows[e.RowIndex]);
                        calculateInvSum();
                    }
                }
            }
        }

        private void lblDueInvestigation_TextChanged(object sender, EventArgs e)
        {
            calculateGrandTotal();
        }

        private void lblDuePha_TextChanged(object sender, EventArgs e)
        {
            calculateGrandTotal();
        }

        private void dgvPharmacyBillDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPharmacyBillDetails.Rows.Count > 0)
            {
                if (dgvPharmacyBillDetails.Columns[e.ColumnIndex].Name.Equals("EditPha"))//Edit Button Click
                {
                    cboItemPha.SelectedValue = Convert.ToInt32(dgvPharmacyBillDetails.Rows[e.RowIndex].Cells["ItemIdPha"].Value.ToString());
                    cboLotIdPha.Text = dgvPharmacyBillDetails.Rows[e.RowIndex].Cells["LotIdPha"].Value.ToString();
                    lblExpDatePha.Text = dgvPharmacyBillDetails.Rows[e.RowIndex].Cells["ExpDatePha"].Value.ToString();
                    txtQtyPha.Text = dgvPharmacyBillDetails.Rows[e.RowIndex].Cells["QtyPha"].Value.ToString();
                    txtUnitPricePha.Text = dgvPharmacyBillDetails.Rows[e.RowIndex].Cells["UnitPricePha"].Value.ToString();
                    lblFreeCarePha.Text = dgvPharmacyBillDetails.Rows[e.RowIndex].Cells["FreeCarePha"].Value.ToString();
                    lblTotalAmtPha.Text = dgvPharmacyBillDetails.Rows[e.RowIndex].Cells["TotalAmtPha"].Value.ToString();
                    lblinventoryStockId.Text = dgvPharmacyBillDetails.Rows[e.RowIndex].Cells["inventoryStockId"].Value.ToString();
                    lblCheckEditModePha.Text = e.RowIndex.ToString();
                }

                if (dgvPharmacyBillDetails.Columns[e.ColumnIndex].Name.Equals("DeletePha"))//Delete Button Click
                {
                    if (MessageBox.Show("Are you sure to Delete the record?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvPharmacyBillDetails.Rows.Remove(dgvPharmacyBillDetails.Rows[e.RowIndex]);
                        calculatePhaSum();
                    }
                }
            }
        }

        private void cboRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRoomNo.SelectedIndex > 0)
            {
                loadBed(Convert.ToInt32(cboRoomNo.SelectedValue.ToString()));
            }
        }

        private void btnGenClear_Click(object sender, EventArgs e)
        {
            clearGeneralInputValues();
            clearGenRecords();
        }

        private void cboGenAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGenAccountName.Items.Count > 1 && cboGenAccountName.SelectedIndex > 0)
            {
                Common.Common.cis_billing.accountHeadId = Convert.ToInt32(cboGenAccountName.SelectedValue.ToString());

                try
                {
                    dtSource = objBusinessFacade.getBillChargesById(Common.Common.cis_billing.accountHeadId);
                    if (dtSource.Rows.Count > 0)
                    {
                        Common.Common.cis_billing.accountGroupName = Convert.ToString(dtSource.Rows[0]["account_head_name"].ToString());
                        Common.Common.cis_billing.accountGroupId = Convert.ToInt32(dtSource.Rows[0]["account_group_id"].ToString());

                        txtUnitpriceGen.Text = Convert.ToString(dtSource.Rows[0]["unit_price"].ToString());
                        lblAccountGroup.Text = Common.Common.cis_billing.accountGroupName;
                        lblAccountGroupId.Text = Common.Common.cis_billing.accountGroupId.ToString();

                        txtGenQty.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void txtGenQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (!string.IsNullOrEmpty(Common.Common.cis_billing.accountGroupName) && Common.Common.cis_billing.accountHeadId > 0)
                {
                    Common.Common.cis_billing.accountName = cboGenAccountName.SelectedText.ToString();
                    Common.Common.cis_billing.qtyGen = Convert.ToInt32(txtGenQty.Text.ToString());
                    Common.Common.cis_billing.genUnitPrice = Convert.ToDecimal(txtUnitpriceGen.Text.ToString());
                    Common.Common.cis_billing.accountGroupId = Convert.ToInt32(lblAccountGroupId.Text.ToString());
                    Common.Common.cis_billing.accountGroupName = lblAccountGroup.Text.ToString();
                    Common.Common.cis_billing.genTotal = (Common.Common.cis_billing.qtyGen * Common.Common.cis_billing.genUnitPrice);
                    lblTotalAmtInv.Text = Convert.ToString(Common.Common.cis_billing.genTotal);

                    if (Common.Common.cis_billing.qtyGen > 0)
                    {
                        if (string.IsNullOrEmpty(lblCheckEditModeGen.Text.ToString()))//Add Item
                        {
                            bool entryFound = false;
                            foreach (DataGridViewRow row in dgvGenBill.Rows)//Check Item exits aleady
                            {
                                int genId = Convert.ToInt32(row.Cells["genAccountId"].Value);
                                if (genId == Common.Common.cis_billing.accountHeadId)
                                {
                                    MessageBox.Show("Already Exists....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    entryFound = true;
                                    cboGenAccountName.Focus();
                                }
                            }
                            if (!entryFound)//If not add the item
                            {
                                dgvGenBill.Rows.Add(dgvGenBill.Rows.Count + 1, Common.Common.cis_billing.accountHeadId, Common.Common.cis_billing.accountName, Common.Common.cis_billing.accountGroupId, Common.Common.cis_billing.accountGroupName, Common.Common.cis_billing.qtyGen, Common.Common.cis_billing.genUnitPrice, Common.Common.cis_billing.genTotal);
                                clearGeneralInputValues();
                                cboGenAccountName.Focus();
                                calculateGenSum();
                            }
                        }

                        else //Edit Item
                        {
                            int rowNo = Convert.ToInt32(lblCheckEditModeGen.Text.ToString());
                            dgvGenBill.Rows[rowNo].Cells["genAccountId"].Value = Common.Common.cis_billing.accountHeadId;
                            dgvGenBill.Rows[rowNo].Cells["genAccountName"].Value = Common.Common.cis_billing.accountName;
                            dgvGenBill.Rows[rowNo].Cells["accountGroupId"].Value = lblAccountGroupId.Text.ToString();
                            dgvGenBill.Rows[rowNo].Cells["genCategory"].Value = lblAccountGroup.Text.ToString();
                            dgvGenBill.Rows[rowNo].Cells["genQty"].Value = txtGenQty.Text.ToString();
                            dgvGenBill.Rows[rowNo].Cells["genUnitPrice"].Value = txtUnitpriceGen.Text.ToString();
                            dgvGenBill.Rows[rowNo].Cells["genTotalAmt"].Value = lblGenTotalAmt.Text.ToString();
                            clearGeneralInputValues();
                            cboGenAccountName.Focus();
                            calculateInvSum();
                        }
                    }

                    else
                    {
                        MessageBox.Show("Fields are required....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void txtGenDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtGenDiscount_Leave(sender, e);
                #region CommentedLinesofJesu
                //txtGenAmtPaid.Select();
                //e.Handled = true;

                //Common.Common.cis_billing.genTotal = Convert.ToDecimal(lblGenTotalNetAmt.Text.ToString());
                //Common.Common.cis_billing.genDiscountAmt = Convert.ToDecimal(txtGenDiscount.Text.ToString());
                //Common.Common.cis_billing.genAmtPaid = Convert.ToDecimal(txtGenAmtPaid.Text.ToString());
                //Common.Common.cis_billing.balanceAmountGen = (Common.Common.cis_billing.genTotal - Common.Common.cis_billing.genAmtPaid);

                //if (!string.IsNullOrEmpty(txtGenDiscount.Text))
                //{
                //    if (Common.Common.cis_billing.balanceAmountGen >= Common.Common.cis_billing.genDiscountAmt)
                //    {
                //        Common.Common.cis_billing.balanceAmountGen = (Common.Common.cis_billing.genTotal - Common.Common.cis_billing.genDiscountAmt - Common.Common.cis_billing.genAmtPaid);
                //        lblGenDueAmt.Text = Convert.ToString(Common.Common.cis_billing.balanceAmountGen);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Discount Amount can't be greater than Bill Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txtGenDiscount.Text = "0.00";
                //        Common.Common.cis_billing.balanceAmountGen = (Common.Common.cis_billing.genTotal - Common.Common.cis_billing.genAmtPaid);
                //        lblGenDueAmt.Text = Convert.ToString(Common.Common.cis_billing.balanceAmountGen);
                //        txtGenDiscount.Select();
                //    }
                //}
                //else
                //{
                //    Common.Common.cis_billing.genDiscountAmt = 0;
                //}
                #endregion
            }
        }

        private void txtGenAmtPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtGenAmtPaid_Leave(sender, e);
                #region CommentedLinesofJesuLogic
                //txtGenAdvAdj.Select();
                //e.Handled = true;

                //Common.Common.cis_billing.genTotal = Convert.ToDecimal(lblGenTotalNetAmt.Text.ToString());
                //Common.Common.cis_billing.genDiscountAmt = Convert.ToDecimal(txtGenDiscount.Text.ToString());
                //Common.Common.cis_billing.genAmtPaid = Convert.ToDecimal(txtGenAmtPaid.Text.ToString());
                //Common.Common.cis_billing.balanceAmountGen = (Common.Common.cis_billing.genTotal - Common.Common.cis_billing.genDiscountAmt);

                //if (!string.IsNullOrEmpty(txtGenAmtPaid.Text))
                //{
                //    if (Common.Common.cis_billing.balanceAmountGen >= Common.Common.cis_billing.genAmtPaid)
                //    {
                //        Common.Common.cis_billing.balanceAmountGen = (Common.Common.cis_billing.genTotal - Common.Common.cis_billing.genDiscountAmt - Common.Common.cis_billing.genAmtPaid);
                //        lblGenDueAmt.Text = Convert.ToString(Common.Common.cis_billing.balanceAmountGen);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Amount can't be greater than Net Total....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txtGenAmtPaid.Text = "0.00";
                //        Common.Common.cis_billing.balanceAmountGen = (Common.Common.cis_billing.genTotal - Common.Common.cis_billing.genDiscountAmt);
                //        lblGenDueAmt.Text = Convert.ToString(Common.Common.cis_billing.balanceAmountGen);
                //        txtGenAmtPaid.Select();
                //    }
                //}
                //else
                //{
                //    Common.Common.cis_billing.genAmtPaid = 0;
                //}
                #endregion
            }
        }

        private void dgvGenBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGenBill.Rows.Count > 0)
            {
                //dgvPharmacyBillDetails.Columns[e.ColumnIndex].Name.Equals("EditPha")
                if (dgvGenBill.Columns[e.ColumnIndex].Name.Equals("genEdit"))//Edit Button Click
                {
                    cboGenAccountName.SelectedValue = Convert.ToInt32(dgvGenBill.Rows[e.RowIndex].Cells["genAccountId"].Value.ToString());
                    txtGenQty.Text = dgvGenBill.Rows[e.RowIndex].Cells["genQty"].Value.ToString();
                    txtUnitpriceGen.Text = dgvGenBill.Rows[e.RowIndex].Cells["genUnitPrice"].Value.ToString();
                    lblGenTotalAmt.Text = dgvGenBill.Rows[e.RowIndex].Cells["genTotalAmt"].Value.ToString();
                    lblCheckEditModeGen.Text = e.RowIndex.ToString();
                }

                if (dgvGenBill.Columns[e.ColumnIndex].Name.Equals("genDelete"))//Delete Button Click
                {
                    if (MessageBox.Show("Are you sure to Delete the record?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvGenBill.Rows.Remove(dgvGenBill.Rows[e.RowIndex]);
                        calculateGenSum();
                    }
                }
            }
        }

        private void lblGenDueAmt_TextChanged(object sender, EventArgs e)
        {
            calculateGrandTotal();
        }

        private void txtGenDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtGenAmtPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtGenAdvAdj_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtDiscountInvestigation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtAmtPaidInvestigation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtAdvAdjInvestigation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtcBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                dgvCancellationInvoice.Rows.Clear();
                dtSource.Rows.Clear();
                string subStrBillType;
                Common.Common.billNo = txtcBillNo.Text.ToString();
                subStrBillType = Common.Common.billNo.Substring(0, 2);
                dtSource = null;
                dtSource = objBusinessFacade.getBillTypeId(subStrBillType);

                if (dtSource.Rows.Count > 0)
                {
                    lblcBillType.Text = dtSource.Rows[0]["number_format_id"].ToString();
                    Common.Common.billTypeId = objBusinessFacade.NonBlankValueOfInt(lblcBillType.Text.ToString());

                    if (Common.Common.billTypeId > 0)
                    {
                        switch (Common.Common.billTypeId)
                        {
                            //Investigation Bill
                            case 5:
                                dtSource = null;
                                dtSource = objBusinessFacade.getInvestigationBillInfo(Common.Common.billNo);

                                if (dtSource.Rows.Count > 0)
                                {
                                    lblcBillId.Text = dtSource.Rows[0]["bill_id"].ToString();
                                    lblcPatientName.Text = dtSource.Rows[0]["patient_name"].ToString();
                                    lblcBillDate.Text = dtSource.Rows[0]["bill_date"].ToString();
                                    lblcPatientId.Text = dtSource.Rows[0]["patient_id"].ToString();
                                    lblcVisitNo.Text = dtSource.Rows[0]["visit_number"].ToString();

                                    lbloGrandTotal.Text = dtSource.Rows[0]["bill_amount"].ToString();
                                    lbloDiscount.Text = dtSource.Rows[0]["discount"].ToString();
                                    lbloAmountPaid.Text = dtSource.Rows[0]["amount_paid"].ToString();
                                    lbloAdvAdj.Text = dtSource.Rows[0]["pay_from_advance"].ToString();
                                    lbloDue.Text = dtSource.Rows[0]["due"].ToString();

                                    Common.Common.billId = objBusinessFacade.NonBlankValueOfInt(lblcBillId.Text.ToString());
                                    dtSource = objBusinessFacade.getInvestigationBillDetailInfo(Common.Common.billId);

                                    if (dtSource.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtSource.Rows.Count; i++)
                                        {
                                            dgvCancellationInvoice.Rows.Add();
                                            dgvCancellationInvoice.Rows[i].Cells["csNo"].Value = dgvCancellationInvoice.Rows.Count;
                                            dgvCancellationInvoice.Rows[i].Cells["ctransactionId"].Value = dtSource.Rows[i]["transaction_id"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cItemId"].Value = dtSource.Rows[i]["investigation_id"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cCode"].Value = dtSource.Rows[i]["investigation_code"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cName"].Value = dtSource.Rows[i]["investigation_name"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cDepartmentId"].Value = dtSource.Rows[i]["department_id"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cDepartmentName"].Value = dtSource.Rows[i]["department_name"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cIssuedQty"].Value = dtSource.Rows[i]["qty"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cUnitPrice"].Value = dtSource.Rows[i]["unit_price"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cTotalAmt"].Value = dtSource.Rows[i]["amount"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cNetTotal"].Value = dtSource.Rows[i]["amount"].ToString();
                                        }
                                    }

                                    dgvCancellationInvoice.Columns["cType"].Visible = false;
                                    dgvCancellationInvoice.Columns["cLotId"].Visible = false;
                                    dgvCancellationInvoice.Columns["cExpDate"].Visible = false;
                                    dgvCancellationInvoice.Columns["cFreeCare"].Visible = false;

                                    dgvCancellationInvoice.Columns["cCode"].Visible = true;
                                    dgvCancellationInvoice.Columns["cDepartmentName"].Visible = true;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Bill Number or Bill Cancelled....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;

                            // Pharmacy Bill
                            case 6:
                                dtSource = null;
                                dtSource = objBusinessFacade.getPharmacyBillInfo(Common.Common.billNo);

                                if (dtSource.Rows.Count > 0)
                                {
                                    lblcBillId.Text = dtSource.Rows[0]["bill_id"].ToString();
                                    lblcPatientName.Text = dtSource.Rows[0]["patient_name"].ToString();
                                    lblcBillDate.Text = dtSource.Rows[0]["bill_date"].ToString();
                                    lblcPatientId.Text = dtSource.Rows[0]["patient_id"].ToString();
                                    lblcVisitNo.Text = dtSource.Rows[0]["visit_number"].ToString();

                                    lbloGrandTotal.Text = dtSource.Rows[0]["bill_amount"].ToString();
                                    lbloDiscount.Text = dtSource.Rows[0]["discount"].ToString();
                                    lbloFreeCare.Text = dtSource.Rows[0]["total_free_care"].ToString();
                                    lbloAmountPaid.Text = dtSource.Rows[0]["amount_paid"].ToString();
                                    lbloAdvAdj.Text = dtSource.Rows[0]["pay_from_advance"].ToString();
                                    lbloDue.Text = dtSource.Rows[0]["due"].ToString();

                                    Common.Common.billId = objBusinessFacade.NonBlankValueOfInt(lblcBillId.Text.ToString());
                                    dtSource = objBusinessFacade.getPharmacyBillDetailInfo(Common.Common.billId);

                                    if (dtSource.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtSource.Rows.Count; i++)
                                        {
                                            dgvCancellationInvoice.Rows.Add();
                                            dgvCancellationInvoice.Rows[i].Cells["csNo"].Value = dgvCancellationInvoice.Rows.Count;
                                            dgvCancellationInvoice.Rows[i].Cells["ctransactionId"].Value = dtSource.Rows[i]["bill_detail_id"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cItemId"].Value = dtSource.Rows[i]["item_id"].ToString();
                                            //dgvCancellationInvoice.Rows[i].Cells["cCode"].Value = dtSource.Rows[i]["investigation_code"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cName"].Value = dtSource.Rows[i]["item_name"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cTypeId"].Value = dtSource.Rows[i]["item_type_id"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cType"].Value = dtSource.Rows[i]["item_type"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cLotId"].Value = dtSource.Rows[i]["lot_id"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cExpDate"].Value = dtSource.Rows[i]["exp_date"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cIssuedQty"].Value = dtSource.Rows[i]["trans_qty"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cUnitPrice"].Value = dtSource.Rows[i]["unit_price"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cTotalAmt"].Value = dtSource.Rows[i]["total_amount"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cSalesTaxPerc"].Value = dtSource.Rows[i]["tax_perc"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cFreeCare"].Value = dtSource.Rows[i]["free_care"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cNetTotal"].Value = dtSource.Rows[i]["net_total_amount"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cInventoryStockId"].Value = dtSource.Rows[i]["inventory_stock_id"].ToString();
                                        }
                                        dtSource.Rows.Clear();
                                        dgvCancellationInvoice.Columns["cType"].Visible = true;
                                        dgvCancellationInvoice.Columns["cLotId"].Visible = true;
                                        dgvCancellationInvoice.Columns["cExpDate"].Visible = true;
                                        dgvCancellationInvoice.Columns["cFreeCare"].Visible = true;

                                        dgvCancellationInvoice.Columns["cCode"].Visible = false;
                                        dgvCancellationInvoice.Columns["cDepartmentName"].Visible = false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Bill Number or Bill Cancelled....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;

                            // General Bill
                            case 7:
                                dtSource = null;
                                dtSource = objBusinessFacade.getGeneralBillInfo(Common.Common.billNo);

                                if (dtSource.Rows.Count > 0)
                                {
                                    lblcBillId.Text = dtSource.Rows[0]["bill_id"].ToString();
                                    lblcPatientName.Text = dtSource.Rows[0]["patient_name"].ToString();
                                    lblcBillDate.Text = dtSource.Rows[0]["bill_date"].ToString();
                                    lblcPatientId.Text = dtSource.Rows[0]["patient_id"].ToString();
                                    lblcVisitNo.Text = dtSource.Rows[0]["visit_number"].ToString();

                                    lbloGrandTotal.Text = dtSource.Rows[0]["bill_amount"].ToString();
                                    lbloDiscount.Text = dtSource.Rows[0]["discount"].ToString();
                                    lbloAmountPaid.Text = dtSource.Rows[0]["amount_paid"].ToString();
                                    lbloAdvAdj.Text = dtSource.Rows[0]["pay_from_advance"].ToString();
                                    lbloDue.Text = dtSource.Rows[0]["due"].ToString();

                                    Common.Common.billId = objBusinessFacade.NonBlankValueOfInt(lblcBillId.Text.ToString());
                                    dtSource = objBusinessFacade.getGeneralBillDetailInfo(Common.Common.billId);

                                    if (dtSource.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtSource.Rows.Count; i++)
                                        {
                                            dgvCancellationInvoice.Rows.Add();
                                            dgvCancellationInvoice.Rows[i].Cells["csNo"].Value = dgvCancellationInvoice.Rows.Count;
                                            dgvCancellationInvoice.Rows[i].Cells["ctransactionId"].Value = dtSource.Rows[i]["bill_detail_id"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cTypeId"].Value = dtSource.Rows[i]["account_group_id"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cItemId"].Value = dtSource.Rows[i]["account_head_id"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cName"].Value = dtSource.Rows[i]["account_head_name"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cIssuedQty"].Value = dtSource.Rows[i]["qty"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cUnitPrice"].Value = dtSource.Rows[i]["unit_price"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cTotalAmt"].Value = dtSource.Rows[i]["amount"].ToString();
                                            dgvCancellationInvoice.Rows[i].Cells["cNetTotal"].Value = dtSource.Rows[i]["amount"].ToString();
                                        }
                                    }

                                    dgvCancellationInvoice.Columns["cType"].Visible = false;
                                    dgvCancellationInvoice.Columns["cLotId"].Visible = false;
                                    dgvCancellationInvoice.Columns["cExpDate"].Visible = false;
                                    dgvCancellationInvoice.Columns["cFreeCare"].Visible = false;
                                    dgvCancellationInvoice.Columns["cCode"].Visible = false;
                                    dgvCancellationInvoice.Columns["cDepartmentName"].Visible = false;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Bill Number or Bill Cancelled....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Bill Number....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    e.Handled = true;
                }
                else
                {
                    MessageBox.Show("Invalid Bill Number....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCancellationInvoice_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCancellationInvoice.Rows.Count > 0)
            {
                int rowIndex = dgvCancellationInvoice.CurrentRow.Index;
                if (cbCancelAll.Checked == false)
                {
                    if (dgvCancellationInvoice.Columns[e.ColumnIndex].Name.Equals("cQty") && (!string.IsNullOrEmpty(dgvCancellationInvoice.Rows[rowIndex].Cells["cQty"].Value as string)) && objBusinessFacade.NonBlankValueOfInt(dgvCancellationInvoice.Rows[rowIndex].Cells["cQty"].Value.ToString()) != 0)//Cancel Qty
                    {
                        if (objBusinessFacade.NonBlankValueOfInt(dgvCancellationInvoice.Rows[rowIndex].Cells["cQty"].Value.ToString()) <= objBusinessFacade.NonBlankValueOfInt(dgvCancellationInvoice.Rows[rowIndex].Cells["cIssuedQty"].Value.ToString()))
                        {
                            Common.Common.cis_pharmacy_info.phaQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvCancellationInvoice.Rows[rowIndex].Cells["cIssuedQty"].Value));
                            Common.Common.cis_pharmacy_info.cancelledQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvCancellationInvoice.Rows[rowIndex].Cells["cQty"].Value));
                            Common.Common.cis_pharmacy_info.phaUnitPrice = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvCancellationInvoice.Rows[rowIndex].Cells["cUnitPrice"].Value));
                            Common.Common.cis_pharmacy_info.phaFreeCare = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvCancellationInvoice.Rows[rowIndex].Cells["cFreeCare"].Value));
                            if (Common.Common.cis_pharmacy_info.phaFreeCare != 0)
                            {
                                Common.Common.cis_pharmacy_info.refundFreeCare = Common.Common.cis_pharmacy_info.cancelledQty * (Common.Common.cis_pharmacy_info.phaFreeCare / Common.Common.cis_pharmacy_info.phaQty);
                            }
                            else
                            {
                                Common.Common.cis_pharmacy_info.refundFreeCare = 0;
                            }
                            Common.Common.cis_pharmacy_info.phaTotalAmt = Common.Common.cis_pharmacy_info.cancelledQty * Common.Common.cis_pharmacy_info.phaUnitPrice;
                            dgvCancellationInvoice.Rows[rowIndex].Cells["cRefundAmt"].Value = Common.Common.cis_pharmacy_info.phaTotalAmt;
                            dgvCancellationInvoice.Rows[rowIndex].Cells["cRefundFCare"].Value = Common.Common.cis_pharmacy_info.refundFreeCare;
                            calculateInvCancelSum();
                        }
                        else
                        {
                            MessageBox.Show("Refund Qty can't be greater than Issued Qty....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvCancellationInvoice.Rows[rowIndex].Cells["cQty"].Value = "0";
                        }
                    }

                    else if (dgvCancellationInvoice.Columns[e.ColumnIndex].Name.Equals("cQty") && ((string.IsNullOrEmpty(dgvCancellationInvoice.Rows[rowIndex].Cells["cQty"].Value as string)) || objBusinessFacade.NonBlankValueOfInt(dgvCancellationInvoice.Rows[rowIndex].Cells["cQty"].Value.ToString()) == 0))//Cancel Qty
                    {
                        Common.Common.cis_pharmacy_info.phaQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvCancellationInvoice.Rows[rowIndex].Cells["cIssuedQty"].Value));
                        Common.Common.cis_pharmacy_info.cancelledQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvCancellationInvoice.Rows[rowIndex].Cells["cQty"].Value));
                        Common.Common.cis_pharmacy_info.phaUnitPrice = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvCancellationInvoice.Rows[rowIndex].Cells["cUnitPrice"].Value));
                        Common.Common.cis_pharmacy_info.phaFreeCare = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvCancellationInvoice.Rows[rowIndex].Cells["cFreeCare"].Value));
                        Common.Common.cis_pharmacy_info.refundFreeCare = Common.Common.cis_pharmacy_info.cancelledQty * (Common.Common.cis_pharmacy_info.phaFreeCare / Common.Common.cis_pharmacy_info.phaQty);
                        Common.Common.cis_pharmacy_info.phaTotalAmt = Common.Common.cis_pharmacy_info.cancelledQty * Common.Common.cis_pharmacy_info.phaUnitPrice;
                        dgvCancellationInvoice.Rows[rowIndex].Cells["cRefundAmt"].Value = Common.Common.cis_pharmacy_info.phaTotalAmt;
                        dgvCancellationInvoice.Rows[rowIndex].Cells["cRefundFCare"].Value = Common.Common.cis_pharmacy_info.refundFreeCare;
                        calculateInvCancelSum();
                    }

                    /*else if (dgvCancellationInvoice.Columns[e.ColumnIndex].Name.Equals("cRefundAmt") && (!string.IsNullOrEmpty(dgvCancellationInvoice.Rows[rowIndex].Cells["cRefundAmt"].Value as string)) && objBusinessFacade.NonBlankValueOfDecimal(dgvCancellationInvoice.Rows[rowIndex].Cells["cRefundAmt"].Value.ToString()) != 0)//Cancel Qty
                    {
                        calculateInvRefundSum();
                    }
                    else if (dgvCancellationInvoice.Columns[e.ColumnIndex].Name.Equals("cRefundAmt") && ((string.IsNullOrEmpty(dgvCancellationInvoice.Rows[rowIndex].Cells["cRefundAmt"].Value as string)) || objBusinessFacade.NonBlankValueOfDecimal(dgvCancellationInvoice.Rows[rowIndex].Cells["cRefundAmt"].Value.ToString()) == 0))//Cancel Qty
                    {
                        calculateInvRefundSum();
                    }*/
                }
            }
        }

        private void Control_KeyPress_int(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
        }

        private void cbCancelAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCancelAll.Checked == true)
            {
                foreach (DataGridViewRow row in dgvCancellationInvoice.Rows) //Refund all items
                {
                    dgvCancellationInvoice.Rows[row.Index].Cells["cQty"].Value = dgvCancellationInvoice.Rows[row.Index].Cells["cIssuedQty"].Value;
                    dgvCancellationInvoice.Rows[row.Index].Cells["cRefundAmt"].Value = dgvCancellationInvoice.Rows[row.Index].Cells["cTotalAmt"].Value;
                    dgvCancellationInvoice.Rows[row.Index].Cells["cRefundFCare"].Value = dgvCancellationInvoice.Rows[row.Index].Cells["cFreeCare"].Value;
                }
                lblnGrandTotal.Text = lbloGrandTotal.Text.ToString();
                lblnRefundAmt.Text = lbloGrandTotal.Text.ToString();
                lblNRefundToPatient.Text = lbloAmountPaid.Text.ToString();
                txtnDiscount.Text = "0.00";
                lblnFreeCare.Text = "0.00";
                txtnAdvAdj.Text = "0.00";
                lblnDue.Text = "0.00";
                txtnAmountPaid.Text = "0.00";
                lblAlreadyPaid.Text = "0.00";
                lblRefundFreeCare.Text = "0.00";
                txtnDiscount.ReadOnly = true;
                txtnAmountPaid.ReadOnly = true;
                dgvCancellationInvoice.Columns["cQty"].ReadOnly = true;
            }
            else
            {
                foreach (DataGridViewRow row in dgvCancellationInvoice.Rows) //No Refund all
                {
                    dgvCancellationInvoice.Rows[row.Index].Cells["cQty"].Value = 0;
                    dgvCancellationInvoice.Rows[row.Index].Cells["cRefundAmt"].Value = 0;
                    dgvCancellationInvoice.Rows[row.Index].Cells["cRefundFCare"].Value = 0;
                }
                lblnGrandTotal.Text = "0.00";
                lblnRefundAmt.Text = "0.00";
                lblNRefundToPatient.Text = "0.00";
                txtnDiscount.Text = "0.00";
                lblnFreeCare.Text = "0.00";
                txtnAmountPaid.Text = "0.00";
                txtnAdvAdj.Text = "0.00";
                lblnDue.Text = "0.00";
                lblAlreadyPaid.Text = "0.00";
                lblRefundFreeCare.Text = "0.00";
                txtnDiscount.ReadOnly = false;
                txtnAmountPaid.ReadOnly = false;
                dgvCancellationInvoice.Columns["cQty"].ReadOnly = false;
            }
        }

        private void dgvCancellationInvoice_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvCancellationInvoice.Columns.Contains("cQty"))
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress_int);
            }
        }

        private void cboSocialTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSocialTitle.SelectedIndex > 0)
            {
                dtSource = null;
                dtSource = objBusinessFacade.getGenderIdBySocialTitleId(Convert.ToInt32(cboSocialTitle.SelectedValue.ToString()));
                if (dtSource.Rows.Count > 0)
                {
                    cboGender.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["gender_id"].ToString());
                }
                //txtPatientName.Focus();
                cboGender.Enabled = false;
            }
            else
            {
                cboGender.Enabled = true;
            }
        }

        private void btnSearchPatient_Click(object sender, EventArgs e)
        {
            CIS.Modules.cisSearchPatient objShow = new CIS.Modules.cisSearchPatient(this, "RegAndInvoice");
            objShow.ShowDialog();
        }

        private void cboPaymentMode_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void txtnDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if ((objBusinessFacade.NonBlankValueOfDecimal(lblnGrandTotal.Text.ToString()) + objBusinessFacade.NonBlankValueOfDecimal(lblnFreeCare.Text.ToString())) >= objBusinessFacade.NonBlankValueOfDecimal(txtnDiscount.Text.ToString()))
                {
                    calculateInvCanBillDetails();
                }
                else
                {
                    MessageBox.Show("Discount Amt can't be greater than Grand Total....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtnDiscount.Text = "0.00";
                }
                e.Handled = true;
                txtnAmountPaid.Focus();
            }
        }

        private void txtnAmountPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (objBusinessFacade.NonBlankValueOfDecimal(lblnDue.Text.ToString()) > 0 && objBusinessFacade.NonBlankValueOfDecimal(lblnDue.Text.ToString()) >= objBusinessFacade.NonBlankValueOfDecimal(txtnAmountPaid.Text.ToString()))
                {
                    calculateInvCanBillDetails();
                }
                else
                {
                    txtnAmountPaid.Text = "0.00";
                }
                e.Handled = true;
                cboPaymentMode.Focus();
            }
        }

        private void txtPatientName_TextChanged(object sender, EventArgs e)
        {
            if (txtPatientName.Text == string.Empty)
            {
                tpInvoice.Enabled = true;
            }
            else
            {
                tpInvoice.Enabled = false;
            }
        }
        #endregion

        #region Functions
        private void loadDoctor()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadDoctor();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);

                    cboDoctor.ValueMember = "DOCTOR_ID";
                    cboDoctor.DisplayMember = "DOCTOR_NAME";
                    cboDoctor.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadSocialTitle()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadSocialTitle();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);

                    cboSocialTitle.ValueMember = "social_title_id";
                    cboSocialTitle.DisplayMember = "social_title";
                    cboSocialTitle.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        private void loadPhaItem()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadPhaItem();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboItemPha.ValueMember = "item_id";
                    cboItemPha.DisplayMember = "item_name";
                    cboItemPha.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadDoctorInv()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadDoctor();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboDoctorInv.ValueMember = "DOCTOR_ID";
                    cboDoctorInv.DisplayMember = "DOCTOR_NAME";
                    cboDoctorInv.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadRegistrationDepartment()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadRegistrationDepartment();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    tscmbDepartment.ComboBox.ValueMember = "DEPARTMENT_ID";
                    tscmbDepartment.ComboBox.DisplayMember = "DEPARTMENT_NAME";
                    tscmbDepartment.ComboBox.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadClinicalType()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadClinicalType();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboClinicalType.ValueMember = "DEPARTMENT_ID";
                    cboClinicalType.DisplayMember = "DEPARTMENT_NAME";
                    cboClinicalType.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadCorporate()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadCorporate();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = 0;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboCorporate.ValueMember = "CORPORATE_ID";
                    cboCorporate.DisplayMember = "CORPORATE_NAME";
                    cboCorporate.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadPatientType()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadPatientType();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboPatientType.ValueMember = "PATIENT_TYPE_ID";
                    cboPatientType.DisplayMember = "PATIENT_TYPE";
                    cboPatientType.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadReferralBy()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadReferralBy();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = 0;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboReferredBy.ValueMember = "cis_referral_id";
                    cboReferredBy.DisplayMember = "referral_name";
                    cboReferredBy.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadAddress()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadAddress();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboAddress1.ValueMember = "address_id";
                    cboAddress1.DisplayMember = "place";
                    cboAddress1.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadWard()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadWardReg();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboWardNo.ValueMember = "WARD_DEPARTMENT_ID";
                    cboWardNo.DisplayMember = "WARD_DEPARTMENT_NAME";
                    cboWardNo.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadInvestigation()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadInvestigationDetails();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboInvCodeInvestigation.ValueMember = "investigation_id";
                    cboInvCodeInvestigation.DisplayMember = "investigation_code";
                    cboInvCodeInvestigation.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadGeneralCharges()
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadGeneralCharges();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboGenAccountName.ValueMember = "id_cis_account_head";
                    cboGenAccountName.DisplayMember = "account_head_name";
                    cboGenAccountName.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        public void calculateAge()
        {
            DateTime dob = Convert.ToDateTime(dtpDOB.Value.Date.ToString("MM/dd/yyyy"));
            DateTime curDate = Convert.ToDateTime(DateTime.Now.ToString()).Date;

            int noOfDays = Convert.ToInt32((curDate - dob).TotalDays);

            int yr = Convert.ToInt32(Math.Floor(Convert.ToDecimal(noOfDays / 365)));

            int m = (noOfDays % 12);

            int d = (curDate.Day - dob.Day);
        }

        public void viewDoctorFee()
        {
            if (cboDoctor.SelectedIndex > 0)
            {
                Common.Common.cis_Doctor.doctorId = Convert.ToInt32(cboDoctor.SelectedValue.ToString());
            }

            else
            {
                lblRegistrationAmount.Text = "0.00";
                txtRegDiscount.Text = "0.00";
                txtConsultationFee.Text = "0.00";
                lblRegNetTotal.Text = "0.00";
                txtRegAmountPaid.Text = "0.00";
                Common.Common.cis_Doctor.doctorId = 0;
            }

            if (tscmbRegistrationType.ComboBox.SelectedIndex == 0)
            {
                try
                {
                    dtSource = null;
                    //Common.Common.cis_Doctor.doctorId = Convert.ToInt32(cboDoctor.SelectedValue.ToString());
                    dtSource = objBusinessFacade.getRegFeeByDoctorId(Common.Common.cis_Doctor.doctorId);
                    if (dtSource.Rows.Count > 0)
                        Common.Common.cis_Define_Reg_Fee.newRegFee = Convert.ToDecimal(dtSource.Rows[0]["new_reg_fee"].ToString());
                    else
                        Common.Common.cis_Define_Reg_Fee.newRegFee = 0;
                    txtConsultationFee.Text = Convert.ToString(Common.Common.cis_Define_Reg_Fee.newRegFee);
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            else
            {
                if (!(string.IsNullOrEmpty(txtPatientId.Text)))
                {
                    try
                    {
                        dtSource = null;
                        dtSource = objBusinessFacade.getRegFeeByDoctorIdAndPaientId(Common.Common.cis_Doctor.doctorId, txtPatientId.Text.ToString());
                        if (dtSource.Rows.Count > 0)
                        {
                            Common.Common.cis_Define_Reg_Fee.validity = Convert.ToInt32(dtSource.Rows[0]["validity"].ToString());
                            Common.Common.cis_Define_Reg_Fee.validityPeriod = Convert.ToInt32(dtSource.Rows[0]["validity_period"].ToString());
                            Common.Common.cis_patient_info.last_visit_date = Convert.ToDateTime(dtSource.Rows[0]["last_visit_date"].ToString());
                            Common.Common.cis_patient_info.visit_date = Convert.ToDateTime(dtpVisitDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                            switch (Common.Common.cis_Define_Reg_Fee.validityPeriod)
                            {
                                case 0:
                                    Common.Common.cis_Define_Reg_Fee.revisitRegFee = Convert.ToDecimal(dtSource.Rows[0]["revisit_reg_fee"].ToString());
                                    break;

                                case 1:
                                    if ((Common.Common.cis_patient_info.visit_date.Date - Common.Common.cis_patient_info.last_visit_date.Date).TotalDays >= Common.Common.cis_Define_Reg_Fee.validity)
                                    {
                                        Common.Common.cis_Define_Reg_Fee.revisitRegFee = Convert.ToDecimal(dtSource.Rows[0]["revisit_reg_fee"].ToString());
                                    }
                                    else
                                    {
                                        Common.Common.cis_Define_Reg_Fee.revisitRegFee = 0;
                                    }
                                    break;

                                case 2:
                                    int compMonth = (Common.Common.cis_patient_info.visit_date.Month + Common.Common.cis_patient_info.visit_date.Year * 12) - (Common.Common.cis_patient_info.last_visit_date.Month + Common.Common.cis_patient_info.last_visit_date.Year * 12);
                                    decimal daysInEndMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) * -1;
                                    decimal noOfMonths = decimal.Round((compMonth + (Common.Common.cis_patient_info.last_visit_date.Day - DateTime.Now.Day) / daysInEndMonth), 2, MidpointRounding.AwayFromZero);

                                    if (noOfMonths >= Common.Common.cis_Define_Reg_Fee.validity)
                                    {
                                        Common.Common.cis_Define_Reg_Fee.revisitRegFee = Convert.ToDecimal(dtSource.Rows[0]["revisit_reg_fee"].ToString());
                                    }
                                    else
                                    {
                                        Common.Common.cis_Define_Reg_Fee.revisitRegFee = 0;
                                    }
                                    break;

                                case 3:
                                    if (Convert.ToInt32(((Common.Common.cis_patient_info.visit_date.Date - Common.Common.cis_patient_info.last_visit_date.Date).TotalDays) / 365) >= Common.Common.cis_Define_Reg_Fee.validity)
                                    {
                                        Common.Common.cis_Define_Reg_Fee.revisitRegFee = Convert.ToDecimal(dtSource.Rows[0]["revisit_reg_fee"].ToString());
                                    }
                                    else
                                    {
                                        Common.Common.cis_Define_Reg_Fee.revisitRegFee = 0;
                                    }
                                    break;

                                default:
                                    break;
                            }
                            txtConsultationFee.Text = Convert.ToString(Common.Common.cis_Define_Reg_Fee.revisitRegFee);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.Common.ExceptionHandler.ExceptionWriter(ex);
                        MessageBox.Show(ex.Message + ex.StackTrace);
                    }
                }
            }
        }

        private void viewDoctorFeeByCorporate()
        {
            Common.Common.cis_Doctor.doctorId = Convert.ToInt32(cboDoctor.SelectedValue.ToString());

            if (tscmbRegistrationType.ComboBox.SelectedIndex == 1 && cboCorporate.SelectedIndex > 0)
            {
                try
                {
                    dtSource = null;
                    dtSource = objBusinessFacade.getRegFeeByDoctorId(Common.Common.cis_Doctor.doctorId);
                    if (dtSource.Rows.Count > 0)
                        Common.Common.cis_Define_Reg_Fee.newRegFee = Convert.ToDecimal(dtSource.Rows[0]["revisit_reg_fee"].ToString());
                    else
                        Common.Common.cis_Define_Reg_Fee.newRegFee = 0;
                    txtConsultationFee.Text = Convert.ToString(Common.Common.cis_Define_Reg_Fee.newRegFee);
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            else
            {
                viewDoctorFee();
            }
        }

        private void calculateRegFee()
        {
            Common.Common.module_visit_info.consultationFee = Convert.ToDecimal(txtConsultationFee.Text.ToString());
            Common.Common.module_visit_info.regDiscountType = cboRegDiscountType.SelectedIndex;

            if (!(string.IsNullOrEmpty(txtRegDiscount.Text)))
            {
                Common.Common.module_visit_info.discountPercOrAmtReg = Convert.ToDecimal(txtRegDiscount.Text.ToString());
            }
            else
            {
                Common.Common.module_visit_info.discountPercOrAmtReg = 0;
            }

            if (Common.Common.module_visit_info.regDiscountType == 1 && Common.Common.module_visit_info.discountPercOrAmtReg > 0)
            {
                if (Common.Common.module_visit_info.discountPercOrAmtReg <= 100)
                {
                    Common.Common.module_visit_info.discountAmountReg = Math.Round(Common.Common.module_visit_info.consultationFee * (Common.Common.module_visit_info.discountPercOrAmtReg / 100), 2);
                    lblRegDiscount.Text = Convert.ToString(Common.Common.module_visit_info.discountAmountReg);
                }
                else
                {
                    MessageBox.Show("Discount Amount can't be greater than Bill Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRegDiscount.Text = "0.00";
                }
            }
            else
            {
                if (Common.Common.module_visit_info.discountPercOrAmtReg <= Common.Common.module_visit_info.consultationFee)
                {
                    Common.Common.module_visit_info.discountAmountReg = Math.Round(objBusinessFacade.NonBlankValueOfDecimal(txtRegDiscount.Text.ToString()), 2);
                    lblRegDiscount.Text = Convert.ToString(Common.Common.module_visit_info.discountAmountReg);
                }
                else
                {
                    MessageBox.Show("Discount Amount can't be greater than Bill Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRegDiscount.Text = "0.00";
                }
            }

            Common.Common.module_visit_info.netTotalReg = Common.Common.module_visit_info.consultationFee - Common.Common.module_visit_info.discountAmountReg;

            lblRegNetTotal.Text = Convert.ToString(Common.Common.module_visit_info.netTotalReg);
            lblRegistrationAmount.Text = Convert.ToString(Common.Common.module_visit_info.netTotalReg);
            //txtRegAmountPaid.Text = Convert.ToString(Common.Common.module_visit_info.netTotalReg);
            //Common.Common.module_visit_info.amountPaidReg = Convert.ToDecimal(txtRegAmountPaid.Text.ToString());
            //Common.Common.module_visit_info.balanceAmountReg = (Common.Common.module_visit_info.netTotalReg - Common.Common.module_visit_info.amountPaidReg);
            //lblRegBalance.Text = Convert.ToString(Common.Common.module_visit_info.balanceAmountReg);

            if (cboCorporate.SelectedIndex > 0)//By default corporate should be in due
            {
                txtRegAmountPaid.Text = "0.00";
                Common.Common.module_visit_info.amountPaidReg = Convert.ToDecimal(txtRegAmountPaid.Text.ToString());
                Common.Common.module_visit_info.balanceAmountReg = (Common.Common.module_visit_info.netTotalReg - Common.Common.module_visit_info.amountPaidReg);
                lblRegBalance.Text = Convert.ToString(Common.Common.module_visit_info.balanceAmountReg);
            }
            else
            {
                txtRegAmountPaid.Text = Convert.ToString(Common.Common.module_visit_info.netTotalReg);
                Common.Common.module_visit_info.amountPaidReg = Convert.ToDecimal(txtRegAmountPaid.Text.ToString());
                Common.Common.module_visit_info.balanceAmountReg = (Common.Common.module_visit_info.netTotalReg - Common.Common.module_visit_info.amountPaidReg);
                lblRegBalance.Text = Convert.ToString(Common.Common.module_visit_info.balanceAmountReg);
            }

            calculateGrandTotal();
        }

        private void saveInputValues()
        {
            //Begin Generate Bill Numbers
            ComArugments objArg = objBusinessFacade.generateNumber();
            Common.Common.cis_number_generation.running_patient_id = Convert.ToInt32(objArg.ParamList["running_patient_id"]);

            if (tscmbRegistrationType.ComboBox.SelectedIndex == 0)
            {
                Common.Common.cis_number_generation.patient_id = Convert.ToString(objArg.ParamList["patient_id"]);
            }
            else
            {
                Common.Common.cis_number_generation.patient_id = Convert.ToString(txtPatientId.Text.ToString());
            }
            Common.Common.cis_number_generation.running_visit_number = Convert.ToInt32(objArg.ParamList["running_visit_number"]);
            Common.Common.cis_number_generation.visit_number = Convert.ToString(objArg.ParamList["visit_number"]);
            Common.Common.cis_number_generation.running_token_number = Convert.ToInt32(objArg.ParamList["running_token_number"]);
            Common.Common.cis_number_generation.token_number = Convert.ToString(objArg.ParamList["token_number"]);
            Common.Common.cis_number_generation.running_reg_bill_number = Convert.ToInt32(objArg.ParamList["running_reg_bill_number"]);
            Common.Common.cis_number_generation.reg_bill_number = Convert.ToString(objArg.ParamList["reg_bill_number"]);
            Common.Common.cis_number_generation.running_investigation_bill_number = Convert.ToInt32(objArg.ParamList["running_investigation_bill_number"]);
            Common.Common.cis_number_generation.investigation_bill_number = Convert.ToString(objArg.ParamList["investigation_bill_number"]);
            Common.Common.cis_number_generation.running_pharmacy_bill_number = Convert.ToInt32(objArg.ParamList["running_pharmacy_bill_number"]);
            Common.Common.cis_number_generation.pharmacy_bill_number = Convert.ToString(objArg.ParamList["pharmacy_bill_number"]);
            Common.Common.cis_number_generation.running_general_bill_number = Convert.ToInt32(objArg.ParamList["running_general_bill_number"]);
            Common.Common.cis_number_generation.general_bill_number = Convert.ToString(objArg.ParamList["general_bill_number"]);
            //End Generate Bill Numbers

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

            if (!(string.IsNullOrEmpty(txtPatientName.Text)) || (!(string.IsNullOrEmpty(txtAgeYear.Text)) && !(string.IsNullOrEmpty(txtAgeMonth.Text)) && !(string.IsNullOrEmpty(txtAgeDay.Text))) || dgvInvestigationBill.Rows.Count > 0 || dgvPharmacyBillDetails.Rows.Count > 0 || dgvGenBill.Rows.Count > 0 || !(string.IsNullOrEmpty(txtcBillNo.Text)) && !(string.IsNullOrEmpty(lblcBillId.Text)) && dgvCancellationInvoice.Rows.Count > 0)
            {
                //Begin Save Registration Details
                if ((!(string.IsNullOrEmpty(txtPatientName.Text)) || (!(string.IsNullOrEmpty(txtAgeYear.Text)) && !(string.IsNullOrEmpty(txtAgeMonth.Text)) && !(string.IsNullOrEmpty(txtAgeDay.Text)))) )
                {
                    if ((!(string.IsNullOrEmpty(txtPatientName.Text.Trim())) && (!(string.IsNullOrEmpty(txtAgeYear.Text)) || !(string.IsNullOrEmpty(txtAgeMonth.Text)) || !(string.IsNullOrEmpty(txtAgeDay.Text)))) && cboDoctor.SelectedIndex > 0)
                    {
                        Common.Common.cis_department.departmentId = Convert.ToInt32(tscmbDepartment.ComboBox.SelectedValue);
                        Common.Common.cis_department.visitMode = Convert.ToInt32(tscmbRegistrationType.ComboBox.SelectedIndex);
                        Common.Common.cis_patient_info.social_title_id = Convert.ToInt32(cboSocialTitle.SelectedValue);
                        Common.Common.cis_patient_info.social_title = cboSocialTitle.Text;
                        Common.Common.cis_patient_info.patient_name = Convert.ToString(txtPatientName.Text.ToString());
                        Common.Common.cis_patient_info.visit_date = Convert.ToDateTime(dtpVisitDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        Common.Common.cis_patientType.patientTypeId = Convert.ToInt32(cboPatientType.SelectedValue);
                        Common.Common.gender = Convert.ToInt32(cboGender.SelectedIndex);
                        Common.Common.cis_patient_info.age_year = Convert.ToInt32(txtAgeYear.Text.Equals(string.Empty) ? "0" : txtAgeYear.Text);

                        if (string.IsNullOrEmpty(txtAgeYear.Text))
                        {
                            txtAgeYear.Text = "0";
                        }

                        if (string.IsNullOrEmpty(txtAgeMonth.Text))
                        {
                            txtAgeMonth.Text = "0";
                        }

                        if (string.IsNullOrEmpty(txtAgeDay.Text))
                        {
                            txtAgeDay.Text = "0";
                        }
                        Common.Common.cis_patient_info.age_month = Convert.ToInt32(txtAgeMonth.Text.ToString());
                        Common.Common.cis_patient_info.age_day = Convert.ToInt32(txtAgeDay.Text.ToString());
                        Common.Common.cis_patient_info.dob = Convert.ToDateTime(dtpDOB.Value.Date.ToString());
                        Common.Common.cis_patient_info.guardian_name = Convert.ToString(txtGuardainName.Text.ToString());
                        Common.Common.cis_patient_info.address = Convert.ToString(multitxtAddress.Text.ToString());
                        Common.Common.cis_patient_info.phone_no = Convert.ToString(txtPhoneNo.Text.ToString());
                        Common.Common.cis_Corporate.corporateId = Convert.ToInt32(cboCorporate.SelectedValue);
                        Common.Common.module_visit_info.referralId = Convert.ToInt32(cboReferredBy.SelectedValue);
                        //Common.Common.cis_Corporate.employeeID = Convert.ToString(txtEmployeeId.Text.ToString());
                        Common.Common.cis_department.medicalDepartmentId = Convert.ToInt32(cboClinicalType.SelectedValue);

                        if (Common.Common.cis_department.departmentCategoryId == 2 && Common.Common.cis_Room.ward_id > 0)
                        {
                            Common.Common.cis_Room.ward_id = Convert.ToInt32(cboWardNo.SelectedValue.ToString());
                            Common.Common.cis_Room.roomId = Convert.ToInt32(cboRoomNo.SelectedValue.ToString());
                            Common.Common.cis_bed.bedId = Convert.ToInt32(cboBedNo.SelectedValue.ToString());
                        }

                        Common.Common.cis_Doctor.doctorId = Convert.ToInt32(cboDoctor.SelectedValue);
                        Common.Common.cis_Doctor.doctorName = cboDoctor.Text.ToString();
                        Common.Common.module_visit_info.diagnosis = Convert.ToString(txtDiagnosis.Text.ToString());
                        Common.Common.module_visit_info.regBillDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        objArg.ParamList["running_patient_id"] = Common.Common.cis_number_generation.running_patient_id;
                        objArg.ParamList["patient_id"] = Common.Common.cis_number_generation.patient_id;
                        objArg.ParamList["running_visit_number"] = Common.Common.cis_number_generation.running_visit_number;
                        objArg.ParamList["visit_number"] = Common.Common.cis_number_generation.visit_number;
                        objArg.ParamList["running_token_number"] = Common.Common.cis_number_generation.running_token_number;
                        objArg.ParamList["token_number"] = Common.Common.cis_number_generation.token_number;
                        objArg.ParamList["running_reg_bill_number"] = Common.Common.cis_number_generation.running_reg_bill_number;
                        objArg.ParamList["reg_bill_number"] = Common.Common.cis_number_generation.reg_bill_number;
                        objArg.ParamList["wardId"] = Common.Common.cis_Room.ward_id;
                        objArg.ParamList["roomId"] = Common.Common.cis_Room.roomId;
                        objArg.ParamList["bedId"] = Common.Common.cis_bed.bedId;
                        objArg.ParamList["departmentId"] = Common.Common.cis_department.departmentId;
                        objArg.ParamList["visitMode"] = Common.Common.cis_department.visitMode;
                        objArg.ParamList["visit_type"] = Common.Common.cis_department.departmentCategoryId;
                        objArg.ParamList["patient_name"] = Common.Common.cis_patient_info.patient_name;
                        objArg.ParamList["social_title_id"] = Common.Common.cis_patient_info.social_title_id;
                        objArg.ParamList["social_title"] = Common.Common.cis_patient_info.social_title;
                        objArg.ParamList["visit_date"] = Common.Common.cis_patient_info.visit_date;
                        objArg.ParamList["patientTypeId"] = Common.Common.cis_patientType.patientTypeId;
                        objArg.ParamList["gender"] = Common.Common.gender;
                        objArg.ParamList["age_year"] = Common.Common.cis_patient_info.age_year;
                        objArg.ParamList["age_month"] = Common.Common.cis_patient_info.age_month;
                        objArg.ParamList["age_day"] = Common.Common.cis_patient_info.age_day;
                        objArg.ParamList["dob"] = Common.Common.cis_patient_info.dob;
                        objArg.ParamList["guardian_name"] = Common.Common.cis_patient_info.guardian_name;
                        objArg.ParamList["address"] = Common.Common.cis_patient_info.address;
                        objArg.ParamList["phone_no"] = Common.Common.cis_patient_info.phone_no;
                        objArg.ParamList["corporateId"] = Common.Common.cis_Corporate.corporateId;
                        objArg.ParamList["medicalDepartmentId"] = Common.Common.cis_department.medicalDepartmentId;

                        objArg.ParamList["doctorId"] = Common.Common.cis_Doctor.doctorId;
                        objArg.ParamList["doctor_name"] = Common.Common.cis_Doctor.doctorName;
                        objArg.ParamList["diagnosis"] = Common.Common.module_visit_info.diagnosis;

                        objArg.ParamList["account_head_id"] = Common.Common.module_visit_info.account_head_id;
                        objArg.ParamList["account_head_name"] = Common.Common.module_visit_info.account_head_name;
                        objArg.ParamList["account_type"] = Common.Common.module_visit_info.account_type;
                        objArg.ParamList["regBillDate"] = Common.Common.module_visit_info.regBillDate;
                        objArg.ParamList["consultationFee"] = Common.Common.module_visit_info.consultationFee;
                        objArg.ParamList["regDiscountType"] = Common.Common.module_visit_info.regDiscountType;
                        objArg.ParamList["discountPercOrAmt"] = Common.Common.module_visit_info.discountPercOrAmtReg;
                        objArg.ParamList["discountAmount"] = Common.Common.module_visit_info.discountAmountReg;
                        objArg.ParamList["netTotal"] = Common.Common.module_visit_info.netTotalReg;
                        objArg.ParamList["amountPaid"] = Common.Common.module_visit_info.amountPaidReg;
                        objArg.ParamList["balanceAmount"] = Common.Common.module_visit_info.balanceAmountReg;
                        objArg.ParamList["dueCollection"] = Common.Common.module_visit_info.dueCollectionReg;
                        objArg.ParamList["employee_id"] = txtEmployeeId.Text.ToString();
                        objArg.ParamList["referral_id"] = Common.Common.module_visit_info.referralId;


                        if (Common.Common.module_visit_info.balanceAmountReg == 0)
                        {
                            Common.Common.module_visit_info.regPaymentStatus = 1;//Full Payment
                        }
                        else if (Common.Common.module_visit_info.consultationFee == Common.Common.module_visit_info.balanceAmountReg)
                        {
                            Common.Common.module_visit_info.regPaymentStatus = 2;//Not Paid
                        }
                        else
                        {
                            Common.Common.module_visit_info.regPaymentStatus = 3;//Partially Paid
                        }

                        objArg.ParamList["regPaymentStatus"] = Common.Common.module_visit_info.regPaymentStatus;

                        if (tscmbRegistrationType.ComboBox.SelectedIndex == 1)//Save Revisit for OP & IP
                        {
                            if (!(string.IsNullOrEmpty(txtPatientId.Text)))
                            {
                                Common.Common.flag = objBusinessFacade.insertRegistration(objArg);
                                Common.Common.flag = objBusinessFacade.insertVisitInfo(objArg);
                                Common.Common.flag = objBusinessFacade.insertRegBillInfo(objArg);
                                Common.Common.flag = objBusinessFacade.insertRegBillDetailInfo(objArg);
                                Common.Common.flag = objBusinessFacade.updateRegRunningNumber(objArg);

                                lblVisitNo.Text = Common.Common.cis_number_generation.visit_number;
                                lblTokenNo.Text = Common.Common.cis_number_generation.token_number;
                                lblBillNo.Text = Common.Common.cis_number_generation.reg_bill_number;
                            }
                            else
                            {
                                MessageBox.Show("Patient Id is required....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        else if (tscmbRegistrationType.ComboBox.SelectedIndex == 2)//Save Modify 
                        {
                            Common.Common.flag = objBusinessFacade.modifyPatientInfo(objArg);
                        }

                        else//Save New Registration for OP & IP
                        {
                            Common.Common.flag = objBusinessFacade.insertRegistration(objArg);
                            Common.Common.flag = objBusinessFacade.insertVisitInfo(objArg);
                            Common.Common.flag = objBusinessFacade.insertRegBillInfo(objArg);
                            Common.Common.flag = objBusinessFacade.insertRegBillDetailInfo(objArg);
                            Common.Common.flag = objBusinessFacade.updateRegRunningNumber(objArg);

                            txtPatientId.Text = Common.Common.cis_number_generation.patient_id;
                            lblVisitNo.Text = Common.Common.cis_number_generation.visit_number;
                            lblTokenNo.Text = Common.Common.cis_number_generation.token_number;
                            lblBillNo.Text = Common.Common.cis_number_generation.reg_bill_number;
                        }

                        if (Common.Common.cis_department.departmentCategoryId == 2 && Common.Common.cis_Room.ward_id > 0)//Save Bed Details for IP
                        {
                            Common.Common.cis_bed.startDate = DateTime.Now;
                            //Common.Common.cis_bed.endDate = DateTime.Now;
                            Common.Common.transDate = DateTime.Now;
                            Common.Common.cis_bed.bedStatus = 2;

                            objArg.ParamList["start_date"] = Common.Common.cis_bed.startDate;
                            objArg.ParamList["transaction_date"] = Common.Common.transDate;
                            objArg.ParamList["bed_status"] = Common.Common.cis_bed.bedStatus;

                            Common.Common.flag = objBusinessFacade.insertPatBedInfo(objArg);
                            Common.Common.flag = objBusinessFacade.updatePatBed(objArg);
                        }

                        if (Common.Common.flag == 0)
                        {
                            MessageBox.Show("Registration Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        /*else
                        {
                            MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnSave.Enabled = false;
                        }*/
                    }
                    else
                    {
                        MessageBox.Show("Please enter manditory fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                //End Save Registration Details


                //Begin Invoice Patient Details
                if (dgvInvestigationBill.Rows.Count > 0 || dgvPharmacyBillDetails.Rows.Count > 0 || dgvGenBill.Rows.Count > 0)
                {
                    if (!(string.IsNullOrEmpty(txtPatientNameInv.Text.Trim())))
                    {
                        if (!(string.IsNullOrEmpty(txtAgeYearInv.Text)) || !(string.IsNullOrEmpty(txtAgeMonthInv.Text)) || !(string.IsNullOrEmpty(txtAgeDayInv.Text)))
                        {
                            Common.Common.cis_number_generation.patient_id = Convert.ToString(txtPatientIdInv.Text.ToString());
                            Common.Common.cis_patient_info.patient_name = Convert.ToString(txtPatientNameInv.Text.ToString());
                            Common.Common.gender = Convert.ToInt32(cboGenderInv.SelectedIndex);

                            if (string.IsNullOrEmpty(txtAgeYearInv.Text))
                            {
                                txtAgeYearInv.Text = "0";
                            }

                            if (string.IsNullOrEmpty(txtAgeMonthInv.Text))
                            {
                                txtAgeMonthInv.Text = "0";
                            }

                            if (string.IsNullOrEmpty(txtAgeDayInv.Text))
                            {
                                txtAgeDayInv.Text = "0";
                            }
                            Common.Common.cis_patient_info.age_year = Convert.ToInt32(txtAgeYearInv.Text.ToString());
                            Common.Common.cis_patient_info.age_month = Convert.ToInt32(txtAgeMonthInv.Text.ToString());
                            Common.Common.cis_patient_info.age_day = Convert.ToInt32(txtAgeDayInv.Text.ToString());
                            Common.Common.cis_number_generation.visit_number = Convert.ToString(lblVisitNumberInv.Text.ToString());
                            Common.Common.cis_Doctor.doctorId = Convert.ToInt32(cboDoctorInv.SelectedValue);
                            Common.Common.cis_Doctor.doctorName = cboDoctorInv.Text.ToString();
                            Common.Common.cis_patient_info.address = Convert.ToString(multitxtAddressInv.Text.ToString());

                            objArg.ParamList["invoice_patient_id"] = Common.Common.cis_number_generation.patient_id;
                            objArg.ParamList["invoice_patient_name"] = Common.Common.cis_patient_info.patient_name;
                            objArg.ParamList["invoice_gender"] = Common.Common.gender;
                            objArg.ParamList["invoice_age_year"] = Common.Common.cis_patient_info.age_year;
                            objArg.ParamList["invoice_age_month"] = Common.Common.cis_patient_info.age_month;
                            objArg.ParamList["invoice_age_day"] = Common.Common.cis_patient_info.age_day;
                            objArg.ParamList["invoice_visit_number"] = Common.Common.cis_number_generation.visit_number;
                            objArg.ParamList["invoice_doctor_id"] = Common.Common.cis_Doctor.doctorId;
                            objArg.ParamList["invoice_doctor_name"] = Common.Common.cis_Doctor.doctorName;
                            objArg.ParamList["invoice_address"] = Common.Common.cis_patient_info.address;

                            //End Invoice Patient Details

                            //Begin Save Investigation Details
                            if (dgvInvestigationBill.Rows.Count > 0)
                            {
                                Common.Common.cis_investigation_info.investigationTotal = Convert.ToDecimal(lblInvestigationTotalAmt.Text.ToString());
                                Common.Common.cis_investigation_info.investigationDiscountAmt = Convert.ToDecimal(txtDiscountInvestigation.Text.ToString());
                                Common.Common.cis_investigation_info.netTotalInvestigation = Common.Common.cis_investigation_info.investigationTotal - Common.Common.cis_investigation_info.investigationDiscountAmt;
                                Common.Common.cis_investigation_info.amountPaidInvestigation = Convert.ToDecimal(txtAmtPaidInvestigation.Text.ToString());
                                Common.Common.cis_investigation_info.balanceAmountInvestigation = Convert.ToDecimal(lblDueInvestigation.Text.ToString());

                                if (Common.Common.cis_investigation_info.balanceAmountInvestigation == 0)
                                {
                                    Common.Common.cis_investigation_info.invPaymentStatus = 1;//Full Payment
                                }
                                else if (Common.Common.cis_investigation_info.investigationTotal == Common.Common.cis_investigation_info.balanceAmountInvestigation)
                                {
                                    Common.Common.cis_investigation_info.invPaymentStatus = 2;//Not Paid
                                }
                                else
                                {
                                    Common.Common.cis_investigation_info.invPaymentStatus = 3;//Partially Paid
                                }

                                objArg.ParamList["running_investigation_bill_number"] = Common.Common.cis_number_generation.running_investigation_bill_number;
                                objArg.ParamList["investigation_bill_number"] = Common.Common.cis_number_generation.investigation_bill_number;
                                objArg.ParamList["inv_bill_amount"] = Common.Common.cis_investigation_info.investigationTotal;
                                objArg.ParamList["inv_discount"] = Common.Common.cis_investigation_info.investigationDiscountAmt;
                                objArg.ParamList["inv_total_amount"] = Common.Common.cis_investigation_info.netTotalInvestigation;
                                objArg.ParamList["inv_amount_paid"] = Common.Common.cis_investigation_info.amountPaidInvestigation;
                                objArg.ParamList["inv_due"] = Common.Common.cis_investigation_info.balanceAmountInvestigation;
                                objArg.ParamList["inv_status"] = Common.Common.cis_investigation_info.invPaymentStatus;

                                Common.Common.flag = objBusinessFacade.insertInvestigationBill(objArg);
                                Common.Common.billId = objBusinessFacade.lastInsertRecord();

                                foreach (DataGridViewRow row in dgvInvestigationBill.Rows)
                                {
                                    if (Convert.ToInt32(row.Cells["InvestigationId"].Value) != 0 && !(string.IsNullOrEmpty(row.Cells["InvestigationId"].ToString())))
                                    {

                                        Common.Common.cis_investigation_info.investigationId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["InvestigationId"].Value));
                                        Common.Common.cis_investigation_info.investigationDeptId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["departmentId"].Value));
                                        Common.Common.cis_investigation_info.investigationQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["Qty"].Value));
                                        Common.Common.cis_investigation_info.investigationUnitPrice = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["UnitPrice"].Value));
                                        Common.Common.cis_investigation_info.investigationTotal = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["TotalAmount"].Value));
                                        Common.Common.cis_investigation_info.ShareType = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["shareType"].Value));
                                        Common.Common.cis_investigation_info.shareAmt = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["shareAmt"].Value));

                                        objArg.ParamList["inv_bill_id"] = Common.Common.billId;
                                        objArg.ParamList["investigation_id"] = Common.Common.cis_investigation_info.investigationId;
                                        objArg.ParamList["inv_department_id"] = Common.Common.cis_investigation_info.investigationDeptId;
                                        objArg.ParamList["inv_qty"] = Common.Common.cis_investigation_info.investigationQty;
                                        objArg.ParamList["inv_unit_price"] = Common.Common.cis_investigation_info.investigationUnitPrice;
                                        objArg.ParamList["inv_amount"] = Common.Common.cis_investigation_info.investigationTotal;
                                        objArg.ParamList["inv_share_type"] = Common.Common.cis_investigation_info.ShareType;
                                        objArg.ParamList["inv_share_amt"] = Common.Common.cis_investigation_info.shareAmt;

                                        Common.Common.flag = objBusinessFacade.insertInvestigationBillDetails(objArg);
                                        lblBillNoInvestigation.Text = Common.Common.cis_number_generation.investigation_bill_number;
                                    }
                                }
                                Common.Common.flag = objBusinessFacade.insertInvestigationBillDetailsSummary(objArg);
                                Common.Common.flag = objBusinessFacade.updateInvestigationRunningNumber(objArg);

                                if (Common.Common.flag == 0)
                                {
                                    MessageBox.Show("Investigation Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                /*else
                                {
                                    MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnSave.Enabled = false;
                                }*/
                            }
                            //End Save Investigation Details

                            //Begin Save Pharmacy Bill Details
                            if (dgvPharmacyBillDetails.Rows.Count > 0)
                            {
                                Common.Common.cis_pharmacy_info.phaTotalAmt = Convert.ToDecimal(lblPharmacyTotalAmt.Text.ToString());
                                Common.Common.cis_pharmacy_info.phaDiscountAmt = Convert.ToDecimal(string.IsNullOrEmpty(txtDiscountPha.Text) ? "0.00" : txtDiscountPha.Text);
                                Common.Common.cis_pharmacy_info.phaFreeCareTotal = Convert.ToDecimal(lblTotalFreeCare.Text.ToString());
                                Common.Common.cis_pharmacy_info.netTotalPha = Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaDiscountAmt - Common.Common.cis_pharmacy_info.phaFreeCareTotal;
                                Common.Common.cis_pharmacy_info.phaAmtPaid = Convert.ToDecimal(txtAmountPaidPha.Text.ToString());
                                Common.Common.cis_pharmacy_info.balanceAmountPha = Convert.ToDecimal(lblDuePha.Text.ToString());

                                if (Common.Common.cis_pharmacy_info.balanceAmountPha == 0)
                                {
                                    Common.Common.cis_pharmacy_info.phaPaymentStatus = 1;//Full Payment
                                }
                                else if (Common.Common.cis_pharmacy_info.phaTotalAmt == Common.Common.cis_pharmacy_info.balanceAmountPha)
                                {
                                    Common.Common.cis_pharmacy_info.phaPaymentStatus = 2;//Not Paid
                                }
                                else
                                {
                                    Common.Common.cis_pharmacy_info.phaPaymentStatus = 3;//Partially Paid
                                }

                                objArg.ParamList["running_pharmacy_bill_number"] = Common.Common.cis_number_generation.running_pharmacy_bill_number;
                                objArg.ParamList["pharmacy_bill_number"] = Common.Common.cis_number_generation.pharmacy_bill_number;
                                objArg.ParamList["pha_bill_amount"] = Common.Common.cis_pharmacy_info.phaTotalAmt;
                                objArg.ParamList["pha_discount"] = Common.Common.cis_pharmacy_info.phaDiscountAmt;
                                objArg.ParamList["pha_total_free_care"] = Common.Common.cis_pharmacy_info.phaFreeCareTotal;
                                objArg.ParamList["pha_total_amount"] = Common.Common.cis_pharmacy_info.netTotalPha;
                                objArg.ParamList["pha_amount_paid"] = Common.Common.cis_pharmacy_info.phaAmtPaid;
                                objArg.ParamList["pha_due"] = Common.Common.cis_pharmacy_info.balanceAmountPha;
                                objArg.ParamList["pha_status"] = Common.Common.cis_pharmacy_info.phaPaymentStatus;

                                Common.Common.flag = objBusinessFacade.insertPharmacyBill(objArg);
                                Common.Common.billId = objBusinessFacade.lastInsertRecord();

                                foreach (DataGridViewRow row in dgvPharmacyBillDetails.Rows)
                                {
                                    //objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cItemId"].Value));
                                    //objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cItemId"].Value));
                                    if (objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["ItemIdPha"].Value)) != 0 && !(string.IsNullOrEmpty(row.Cells["ItemIdPha"].ToString())))
                                    {
                                        Common.Common.cis_pharmacy_info.phaItemId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["ItemIdPha"].Value));
                                        Common.Common.cis_pharmacy_info.phaItemTypeId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["itemTypeId"].Value));
                                        Common.Common.cis_pharmacy_info.lotId = Convert.ToString(row.Cells["LotIdPha"].Value);
                                        Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToString(row.Cells["ExpDatePha"].Value);
                                        Common.Common.cis_pharmacy_info.phaQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["QtyPha"].Value));
                                        Common.Common.cis_pharmacy_info.phaUnitPrice = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["UnitPricePha"].Value));
                                        Common.Common.cis_pharmacy_info.phaFreeCare = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["FreeCarePha"].Value));
                                        Common.Common.cis_pharmacy_info.phaTotalAmt = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["TotalAmtPha"].Value));
                                        Common.Common.cis_pharmacy_info.inventoryStockId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["inventoryStockId"].Value));
                                        Common.Common.cis_pharmacy_info.salesTaxPerc = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["sales_tax_perc"].Value));

                                        objArg.ParamList["pha_bill_id"] = Common.Common.billId;
                                        objArg.ParamList["pha_item_id"] = Common.Common.cis_pharmacy_info.phaItemId;
                                        objArg.ParamList["pha_item_type_id"] = Common.Common.cis_pharmacy_info.phaItemTypeId;
                                        objArg.ParamList["pha_lot_id"] = Common.Common.cis_pharmacy_info.lotId;
                                        objArg.ParamList["pha_expiry_date"] = Common.Common.cis_pharmacy_info.phaExpDate;
                                        objArg.ParamList["pha_qty"] = Common.Common.cis_pharmacy_info.phaQty;
                                        objArg.ParamList["pha_unit_price"] = Common.Common.cis_pharmacy_info.phaUnitPrice;
                                        objArg.ParamList["pha_free_care"] = Common.Common.cis_pharmacy_info.phaFreeCare;
                                        objArg.ParamList["pha_total_amt"] = Common.Common.cis_pharmacy_info.phaTotalAmt;
                                        objArg.ParamList["pha_net_total_amount"] = Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaFreeCare;
                                        objArg.ParamList["inventory_stock_id"] = Common.Common.cis_pharmacy_info.inventoryStockId;
                                        objArg.ParamList["tax_perc"] = Common.Common.cis_pharmacy_info.salesTaxPerc;

                                        Common.Common.flag = objBusinessFacade.insertPharmacyBillDetails(objArg);
                                        Common.Common.flag = objBusinessFacade.updateStockPharmacyBill(objArg);
                                        lblBillNoPha.Text = Common.Common.cis_number_generation.pharmacy_bill_number;
                                    }
                                }
                                Common.Common.flag = objBusinessFacade.insertPharmacyBillDetailsSummary(objArg);
                                Common.Common.flag = objBusinessFacade.updatePharmacyRunningNumber(objArg);

                                if (Common.Common.flag == 0)
                                {
                                    MessageBox.Show("Pharmacy Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                /*else
                                {
                                    MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnSave.Enabled = false;
                                }*/
                            }
                            //End Save Pharmacy 

                            //Begin Save General Details
                            if (dgvGenBill.Rows.Count > 0)
                            {
                                Common.Common.cis_billing.genTotal = Convert.ToDecimal(lblGenTotalNetAmt.Text.ToString());
                                Common.Common.cis_billing.genDiscountAmt = Convert.ToDecimal(txtGenDiscount.Text.ToString());
                                Common.Common.cis_billing.netTotalGen = Common.Common.cis_billing.genTotal - Common.Common.cis_billing.genDiscountAmt;
                                Common.Common.cis_billing.genAmtPaid = Convert.ToDecimal(txtGenAmtPaid.Text.ToString());
                                Common.Common.cis_billing.balanceAmountGen = Convert.ToDecimal(lblGenDueAmt.Text.ToString());

                                if (Common.Common.cis_billing.balanceAmountGen == 0)
                                {
                                    Common.Common.cis_billing.genPaymentStatus = 1;//Full Payment
                                }
                                else if (Common.Common.cis_billing.genTotal == Common.Common.cis_billing.balanceAmountGen)
                                {
                                    Common.Common.cis_billing.genPaymentStatus = 2;//Not Paid
                                }
                                else
                                {
                                    Common.Common.cis_billing.genPaymentStatus = 3;//Partially Paid
                                }

                                objArg.ParamList["running_general_bill_number"] = Common.Common.cis_number_generation.running_general_bill_number;
                                objArg.ParamList["general_bill_number"] = Common.Common.cis_number_generation.general_bill_number;
                                objArg.ParamList["gen_bill_amount"] = Common.Common.cis_billing.genTotal;
                                objArg.ParamList["gen_discount"] = Common.Common.cis_billing.genDiscountAmt;
                                objArg.ParamList["gen_total_amount"] = Common.Common.cis_billing.netTotalGen;
                                objArg.ParamList["gen_amount_paid"] = Common.Common.cis_billing.genAmtPaid;
                                objArg.ParamList["gen_due"] = Common.Common.cis_billing.balanceAmountGen;
                                objArg.ParamList["gen_status"] = Common.Common.cis_billing.genPaymentStatus;

                                Common.Common.flag = objBusinessFacade.insertGeneralBill(objArg);
                                Common.Common.billId = objBusinessFacade.lastInsertRecord();

                                foreach (DataGridViewRow row in dgvGenBill.Rows)
                                {
                                    if (!row.IsNewRow)
                                    {
                                        if (Convert.ToInt32(row.Cells["genAccountId"].Value) != 0 && !(string.IsNullOrEmpty(row.Cells["genAccountId"].ToString())))
                                        {
                                            Common.Common.cis_billing.accountHeadId = Convert.ToInt32(row.Cells["genAccountId"].Value);
                                            Common.Common.cis_billing.accountName = Convert.ToString(row.Cells["genAccountName"].Value);
                                            Common.Common.cis_billing.accountGroupId = Convert.ToInt32(row.Cells["accountGroupId"].Value);
                                            Common.Common.cis_billing.qtyGen = Convert.ToInt32(row.Cells["genQty"].Value);
                                            Common.Common.cis_billing.genUnitPrice = Convert.ToDecimal(row.Cells["genUnitPrice"].Value);
                                            Common.Common.cis_billing.genTotal = Convert.ToDecimal(row.Cells["genTotalAmt"].Value);

                                            objArg.ParamList["gen_bill_id"] = Common.Common.billId;
                                            objArg.ParamList["gen_account_head_id"] = Common.Common.cis_billing.accountHeadId;
                                            objArg.ParamList["gen_account_name"] = Common.Common.cis_billing.accountName;
                                            objArg.ParamList["gen_account_group_id"] = Common.Common.cis_billing.accountGroupId;
                                            objArg.ParamList["gen_qty"] = Common.Common.cis_billing.qtyGen;
                                            objArg.ParamList["gen_unit_price"] = Common.Common.cis_billing.genUnitPrice;
                                            objArg.ParamList["gen_amount"] = Common.Common.cis_billing.genTotal;

                                            Common.Common.flag = objBusinessFacade.insertGeneralBillDetails(objArg);
                                            lblGenBillNo.Text = Common.Common.cis_number_generation.general_bill_number;
                                        }
                                    }
                                }
                                Common.Common.flag = objBusinessFacade.insertGeneralBillDetailsSummary(objArg);
                                Common.Common.flag = objBusinessFacade.updateGeneralRunningNumber(objArg);

                                if (Common.Common.flag == 0)
                                {
                                    MessageBox.Show("General Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                /*else
                                {
                                    MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnSave.Enabled = false;
                                }*/
                            }
                            //End Save General Details
                        }
                        else
                        {
                            MessageBox.Show("Age is required....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Patient Name is required....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPatientNameInv.Text = string.Empty;
                    }
                }

                //Begin Save Cancellation Details Investigation, Pharmacy and General
                if (!(string.IsNullOrEmpty(txtcBillNo.Text)) && !(string.IsNullOrEmpty(lblcBillId.Text)) && dgvCancellationInvoice.Rows.Count > 0)
                {
                    int qty = 0;
                    int sumQty = 0;
                    int iQty = 0;
                    int iSumQty = 0;

                    foreach (DataGridViewRow row in dgvCancellationInvoice.Rows)
                    {
                        qty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cQty"].Value));
                        sumQty = sumQty + qty; //Calculate Refund Sum Qty
                    }

                    foreach (DataGridViewRow row in dgvCancellationInvoice.Rows)
                    {
                        iQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cIssuedQty"].Value));
                        iSumQty = iSumQty + iQty; //Calculate Sum Issued Qty
                    }

                    if (sumQty > 0)
                    {
                        Common.Common.billNo = txtcBillNo.Text.ToString();
                        Common.Common.billTypeId = objBusinessFacade.NonBlankValueOfInt(lblcBillType.Text.ToString());
                        Common.Common.billId = objBusinessFacade.NonBlankValueOfInt(lblcBillId.Text.ToString());

                        Common.Common.cis_pharmacy_info.phaTotalAmt = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblnGrandTotal.Text));
                        Common.Common.cis_pharmacy_info.phaDiscountAmt = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(txtnDiscount.Text));
                        Common.Common.cis_pharmacy_info.phaFreeCareTotal = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblnFreeCare.Text));
                        Common.Common.cis_pharmacy_info.netTotalPha = Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaDiscountAmt - Common.Common.cis_pharmacy_info.phaFreeCareTotal;

                        if (DateTime.Parse(lblcBillDate.Text).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            Common.Common.cis_pharmacy_info.phaAmtPaid = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(txtnAmountPaid.Text));
                            Common.Common.cis_pharmacy_info.dueCollection = 0;
                        }
                        else
                        {
                            Common.Common.cis_pharmacy_info.phaAmtPaid = 0;
                            Common.Common.cis_pharmacy_info.dueCollection = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(txtnAmountPaid.Text));
                        }

                        Common.Common.alreadyPaid = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblAlreadyPaid.Text));
                        Common.Common.refundFreeCare = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblRefundFreeCare.Text));
                        Common.Common.cis_pharmacy_info.balanceAmountPha = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblnDue.Text));
                        Common.Common.cis_pharmacy_info.refundAmt = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblnRefundAmt.Text));
                        Common.Common.cis_pharmacy_info.refundToPatient = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblNRefundToPatient.Text));

                        if (Common.Common.cis_pharmacy_info.balanceAmountPha == 0 && Common.Common.cis_pharmacy_info.phaTotalAmt == Common.Common.cis_pharmacy_info.refundAmt)
                        {
                            Common.Common.cis_pharmacy_info.phaPaymentStatus = 4;//Full Bill Cancelled
                        }
                        else if (Common.Common.cis_pharmacy_info.balanceAmountPha == 0)
                        {
                            Common.Common.cis_pharmacy_info.phaPaymentStatus = 1;//Fully Paid
                        }
                        else if (Common.Common.cis_pharmacy_info.phaTotalAmt == Common.Common.cis_pharmacy_info.balanceAmountPha)
                        {
                            Common.Common.cis_pharmacy_info.phaPaymentStatus = 2;//Not Paid
                        }
                        else
                        {
                            Common.Common.cis_pharmacy_info.phaPaymentStatus = 3;//Partially Paid
                        }

                        objArg.ParamList["bill_id"] = Common.Common.billId;
                        objArg.ParamList["pha_bill_amount"] = Common.Common.cis_pharmacy_info.phaTotalAmt;
                        objArg.ParamList["pha_discount"] = Common.Common.cis_pharmacy_info.phaDiscountAmt;
                        objArg.ParamList["pha_total_free_care"] = Common.Common.cis_pharmacy_info.phaFreeCareTotal;
                        objArg.ParamList["pha_total_amount"] = Common.Common.cis_pharmacy_info.netTotalPha;
                        objArg.ParamList["pha_amount_paid"] = Common.Common.cis_pharmacy_info.phaAmtPaid;
                        objArg.ParamList["pha_due_collection"] = Common.Common.cis_pharmacy_info.dueCollection;
                        objArg.ParamList["pha_due"] = Common.Common.cis_pharmacy_info.balanceAmountPha;
                        objArg.ParamList["refund_amt"] = Common.Common.cis_pharmacy_info.refundAmt;
                        objArg.ParamList["refund_to_patient"] = Common.Common.cis_pharmacy_info.refundToPatient;
                        objArg.ParamList["pha_status"] = Common.Common.cis_pharmacy_info.phaPaymentStatus;
                        objArg.ParamList["already_paid"] = Common.Common.alreadyPaid;
                        objArg.ParamList["ref_freecare"] = Common.Common.refundFreeCare;

                        if (Common.Common.billTypeId == 6) // Update Pharmacy
                        {
                            Common.Common.flag = objBusinessFacade.updateRefundPharmacyBill(objArg);
                        }

                        else if (Common.Common.billTypeId == 5) // Update Investigation
                        {
                            Common.Common.flag = objBusinessFacade.updateRefundInvestigationBill(objArg);
                        }

                        else if (Common.Common.billTypeId == 7) // Update General
                        {
                            Common.Common.flag = objBusinessFacade.updateRefundGeneralBill(objArg);
                        }

                        foreach (DataGridViewRow row in dgvCancellationInvoice.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["ctransactionId"].Value) != 0 && !(string.IsNullOrEmpty(Convert.ToString(row.Cells["ctransactionId"].Value))) && objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cQty"].Value)) > 0)
                            {
                                Common.Common.trans_id = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["ctransactionId"].Value));
                                Common.Common.cis_pharmacy_info.phaItemId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cItemId"].Value));
                                Common.Common.cis_investigation_info.investigationCode = Convert.ToString(row.Cells["cCode"].Value);
                                Common.Common.cis_investigation_info.investigationName = Convert.ToString(row.Cells["cName"].Value);
                                Common.Common.cis_investigation_info.investigationDeptId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cDepartmentId"].Value));
                                Common.Common.cis_investigation_info.investigationDeptName = Convert.ToString(row.Cells["cDepartmentName"].Value);
                                Common.Common.cis_pharmacy_info.phaItemTypeId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cTypeId"].Value));
                                Common.Common.cis_pharmacy_info.phaItemType = Convert.ToString(row.Cells["cType"].Value);
                                Common.Common.cis_pharmacy_info.lotId = Convert.ToString(row.Cells["cLotId"].Value);
                                Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToString(row.Cells["cExpDate"].Value);
                                Common.Common.cis_pharmacy_info.phaQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cIssuedQty"].Value));
                                Common.Common.cis_pharmacy_info.cancelledQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cQty"].Value));
                                Common.Common.cis_pharmacy_info.phaUnitPrice = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cUnitPrice"].Value));
                                Common.Common.cis_pharmacy_info.transPhaQty = Common.Common.cis_pharmacy_info.phaQty - Common.Common.cis_pharmacy_info.cancelledQty;
                                Common.Common.cis_pharmacy_info.refundAmt = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cRefundAmt"].Value)); Common.Common.cis_pharmacy_info.refundAmt = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cRefundAmt"].Value));
                                Common.Common.cis_pharmacy_info.phaTotalAmt = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cTotalAmt"].Value)) - Common.Common.cis_pharmacy_info.refundAmt;
                                Common.Common.cis_pharmacy_info.salesTaxPerc = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cSalesTaxPerc"].Value));
                                Common.Common.cis_pharmacy_info.refundFreeCare = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cRefundFCare"].Value));
                                Common.Common.cis_pharmacy_info.phaFreeCare = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cFreeCare"].Value)) - Common.Common.cis_pharmacy_info.refundFreeCare;
                                Common.Common.cis_pharmacy_info.netTotalPha = Common.Common.cis_pharmacy_info.phaTotalAmt - Common.Common.cis_pharmacy_info.phaFreeCare;
                                Common.Common.cis_pharmacy_info.inventoryStockId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["cInventoryStockId"].Value));

                                objArg.ParamList["ctransactionId"] = Common.Common.trans_id;
                                objArg.ParamList["cItemId"] = Common.Common.cis_pharmacy_info.phaItemId;
                                objArg.ParamList["cCode"] = Common.Common.cis_investigation_info.investigationCode;
                                objArg.ParamList["cName"] = Common.Common.cis_investigation_info.investigationName;
                                objArg.ParamList["cDepartmentId"] = Common.Common.cis_investigation_info.investigationDeptId;
                                objArg.ParamList["cDepartmentName"] = Common.Common.cis_investigation_info.investigationDeptName;
                                objArg.ParamList["cTypeId"] = Common.Common.cis_pharmacy_info.phaItemTypeId;
                                objArg.ParamList["cType"] = Common.Common.cis_pharmacy_info.phaItemType;
                                objArg.ParamList["cLotId"] = Common.Common.cis_pharmacy_info.lotId;
                                objArg.ParamList["cExpDate"] = Common.Common.cis_pharmacy_info.phaExpDate;
                                objArg.ParamList["cIssuedQty"] = Common.Common.cis_pharmacy_info.phaQty;
                                objArg.ParamList["cQty"] = Common.Common.cis_pharmacy_info.cancelledQty;
                                objArg.ParamList["cTransPhaQty"] = Common.Common.cis_pharmacy_info.transPhaQty;
                                objArg.ParamList["cUnitPrice"] = Common.Common.cis_pharmacy_info.phaUnitPrice;
                                objArg.ParamList["cTotalAmt"] = Common.Common.cis_pharmacy_info.phaTotalAmt;
                                objArg.ParamList["cSalesTaxPerc"] = Common.Common.cis_pharmacy_info.salesTaxPerc;
                                objArg.ParamList["cFreeCare"] = Common.Common.cis_pharmacy_info.phaFreeCare;
                                objArg.ParamList["cRefundFCare"] = Common.Common.cis_pharmacy_info.refundFreeCare;
                                objArg.ParamList["net_total_amount"] = Common.Common.cis_pharmacy_info.netTotalPha;
                                objArg.ParamList["cRefundAmt"] = Common.Common.cis_pharmacy_info.refundAmt;
                                objArg.ParamList["cInventoryStockId"] = Common.Common.cis_pharmacy_info.inventoryStockId;

                                if (Common.Common.billTypeId == 6)// Pharmacy
                                {
                                    if (Common.Common.cis_pharmacy_info.phaQty != Common.Common.cis_pharmacy_info.cancelledQty) // Update status for Item Details and Insert Item Details for Partial Cancellation
                                    {
                                        Common.Common.flag = objBusinessFacade.updateRefundPharmacyPartialBillDetails(objArg);
                                        Common.Common.flag = objBusinessFacade.InsertRefundPharmacyBillDetails(objArg);
                                    }
                                    else // Update status for Item Details and Insert Item Details for Full Cancellation
                                    {
                                        Common.Common.flag = objBusinessFacade.updateRefundPharmacyPartialBillDetails(objArg);
                                        Common.Common.flag = objBusinessFacade.insertRefundPharmacyFullBillDetails(objArg);
                                    }

                                    Common.Common.flag = objBusinessFacade.updateStockRefundPharmacyBill(objArg);
                                }

                                else if (Common.Common.billTypeId == 5)// Investigation
                                {
                                    if (Common.Common.cis_pharmacy_info.phaQty != Common.Common.cis_pharmacy_info.cancelledQty) // // Update status for Item Details and Insert Item Details for Partial Cancellation
                                    {
                                        Common.Common.flag = objBusinessFacade.updateRefundInvestigationPartialBillDetails(objArg);
                                        Common.Common.flag = objBusinessFacade.InsertRefundInvestigationBillDetails(objArg);
                                    }
                                    else  // Update status for Item Details and Insert Item Details for Full Cancellation
                                    {
                                        Common.Common.flag = objBusinessFacade.updateRefundInvestigationPartialBillDetails(objArg);
                                        Common.Common.flag = objBusinessFacade.insertRefundInvestigationFullBillDetails(objArg);
                                    }

                                }

                                else if (Common.Common.billTypeId == 7)// General
                                {
                                    if (Common.Common.cis_pharmacy_info.phaQty != Common.Common.cis_pharmacy_info.cancelledQty) // // Update status for Item Details and Insert Item Details for Partial Cancellation
                                    {
                                        Common.Common.flag = objBusinessFacade.updateRefundGeneralPartialBillDetails(objArg);
                                        Common.Common.flag = objBusinessFacade.InsertRefundGeneralBillDetails(objArg);
                                    }
                                    else  // Update status for Item Details and Insert Item Details for Full Cancellation
                                    {
                                        Common.Common.flag = objBusinessFacade.updateRefundGeneralPartialBillDetails(objArg);
                                        Common.Common.flag = objBusinessFacade.insertRefundGeneralFullBillDetails(objArg);
                                    }

                                }
                            }
                        }

                        if (Common.Common.billTypeId == 6)//Pharmacy
                        {
                            Common.Common.flag = objBusinessFacade.updateRefundPharmacyBillDetailSummary(objArg); // Update Old Summary Status

                            if (sumQty != iSumQty)// Insert Summary Partial Cancellation
                            {
                                Common.Common.flag = objBusinessFacade.InsertRefundPharmacyPartialBillDetailSummary(objArg);
                            }
                            else // Insert Summary Full Callation
                            {
                                Common.Common.flag = objBusinessFacade.InsertRefundPharmacyFullBillDetailSummary(objArg);
                            }
                        }

                        else if (Common.Common.billTypeId == 5) //Investigation
                        {
                            Common.Common.flag = objBusinessFacade.updateRefundInvestigationBillDetailSummary(objArg);  // Update Old Summary Status

                            if (sumQty != iSumQty)// Insert Summary Partial Cancellation
                            {
                                Common.Common.flag = objBusinessFacade.InsertRefundInvestigationPartialBillDetailSummary(objArg);
                            }
                            else // Insert Summary Full Callation
                            {
                                Common.Common.flag = objBusinessFacade.InsertRefundInvestigationFullBillDetailSummary(objArg);
                            }
                        }

                        else if (Common.Common.billTypeId == 7) //General
                        {
                            Common.Common.flag = objBusinessFacade.updateRefundGeneralBillDetailSummary(objArg);  // Update Old Summary Status

                            if (sumQty != iSumQty)// Insert Summary Partial Cancellation
                            {
                                Common.Common.flag = objBusinessFacade.InsertRefundGeneralPartialBillDetailSummary(objArg);
                            }
                            else // Insert Summary Full Callation
                            {
                                Common.Common.flag = objBusinessFacade.InsertRefundGeneralFullBillDetailSummary(objArg);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Found no refund qty to save the cancellation....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                /*else
                {
                    MessageBox.Show("Mandatory Fields are required....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/
                //End Save  Cancellation Details Investigation, Pharmacy and General

                if (Common.Common.flag == 1)
                {
                    btnSave.Enabled = false;
                    MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else
            {
                MessageBox.Show("Please enter manditory fields", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void clearRegRecords()
        {
            tscmbDepartment.ComboBox.SelectedIndex = 0;
            tscmbRegistrationType.ComboBox.SelectedIndex = 0;
            txtPatientId.Text = string.Empty;
            this.dtpVisitDate.Value = DateTime.Now;
            txtPatientName.Text = string.Empty;
            cboPatientType.SelectedIndex = 0;
            cboGender.SelectedIndex = 1;
            txtAgeYear.Text = string.Empty;
            txtAgeMonth.Text = string.Empty;
            txtAgeDay.Text = string.Empty;
            txtGuardainName.Text = string.Empty;
            multitxtAddress.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            cboCorporate.SelectedIndex = -1;
            cboClinicalType.SelectedIndex = 0;
            cboDoctor.SelectedIndex = 0;
            txtDiagnosis.Text = string.Empty;
            cboRegDiscountType.SelectedIndex = 0;
            lblRegDiscount.Text = "0.00";
            lblVisitNo.Text = string.Empty;
            lblTokenNo.Text = string.Empty;
            lblBillNo.Text = string.Empty;
            txtRegDiscount.Text = "0.00";
            btnSave.Enabled = true;
            lblRegistrationAmount.Text = "0.00";
            txtRegDiscount.Text = "0.00";
            txtConsultationFee.Text = "0.00";
            lblRegNetTotal.Text = "0.00";
            txtRegAmountPaid.Text = "0.00";
            cboWardNo.SelectedIndex = -1;
            cboRoomNo.SelectedIndex = -1;
            cboBedNo.SelectedIndex = -1;
            cboPaymentMode.SelectedIndex = 0;
            txtCardNumber.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtHolderName.Text = string.Empty;
            cboSocialTitle.SelectedIndex = -1;
            cboAddress1.SelectedIndex = -1;
            cboGender.Enabled = true;
            cboReferredBy.SelectedIndex = - 1;
            cboClinicalType.SelectedValue = 16; //General Medicine By Default

            Common.Common.cis_patient_info.social_title_id = 0;
            Common.Common.cis_patient_info.social_title = string.Empty;
            Common.Common.cis_patient_info.patient_name = string.Empty;
            Common.Common.cis_patient_info.age_year = 0;
            Common.Common.cis_patient_info.age_month = 0;
            Common.Common.cis_patient_info.age_day = 0;
            Common.Common.cis_patient_info.guardian_name = string.Empty;
            Common.Common.cis_patient_info.address = string.Empty;
            Common.Common.cis_patient_info.phone_no = string.Empty;

            Common.Common.module_visit_info.account_head_id = 1;
            Common.Common.module_visit_info.account_head_name = "Registration Fee";
            Common.Common.module_visit_info.account_type = 1;
            Common.Common.module_visit_info.consultationFee = 0;
            Common.Common.module_visit_info.regDiscountType = 0;
            Common.Common.module_visit_info.discountPercOrAmtReg = 0;
            Common.Common.module_visit_info.discountAmountReg = 0;
            Common.Common.module_visit_info.netTotalReg = 0;
            Common.Common.module_visit_info.amountPaidReg = 0;
            Common.Common.module_visit_info.balanceAmountReg = 0;
            Common.Common.module_visit_info.dueCollectionReg = 0;
            Common.Common.module_visit_info.refundToPatientReg = 0;
            Common.Common.module_visit_info.diagnosis = string.Empty;

            Common.Common.module_visit_info.regPaymentStatus = 0;
            Common.Common.module_visit_info.dichargeTypeId = 0;
            Common.Common.module_visit_info.referralId = 0;
        }

        private void calculateInvSum()
        {
            decimal sum = 0;
            Common.Common.cis_investigation_info.investigationTotalSum = 0;
            foreach (DataGridViewRow row in dgvInvestigationBill.Rows) //Calculate Amount Columns
            {
                sum = Convert.ToDecimal(row.Cells[8].Value);
                Common.Common.cis_investigation_info.investigationTotalSum = Common.Common.cis_investigation_info.investigationTotalSum + sum;
            }

            lblInvestigationTotalAmt.Text = Common.Common.cis_investigation_info.investigationTotalSum.ToString("0.00"); //Display Total Sum

            if (!string.IsNullOrEmpty(lblCorporateName.Text.ToString()))//By default corporate should be in due
            {
                txtAmtPaidInvestigation.Text = "0.00";
                lblDueInvestigation.Text = Common.Common.cis_investigation_info.investigationTotalSum.ToString("0.00");
            }
            else
            {
                txtAmtPaidInvestigation.Text = Common.Common.cis_investigation_info.investigationTotalSum.ToString("0.00");
                lblDueInvestigation.Text = "0.00";
            }

            txtDiscountInvestigation.Text = "0.00";
            calculateGrandTotal();
        }

        private void calculateGenSum()
        {
            decimal sum = 0;
            Common.Common.cis_billing.genTotalSum = 0;
            foreach (DataGridViewRow row in dgvGenBill.Rows) //Calculate Amount Columns
            {
                sum = Convert.ToDecimal(row.Cells[7].Value);
                Common.Common.cis_billing.genTotalSum = Common.Common.cis_billing.genTotalSum + sum;
            }

            if (!string.IsNullOrEmpty(lblCorporateName.Text.ToString()))//By default corporate should be in due
            {
                txtGenAmtPaid.Text = "0.00";
                lblGenDueAmt.Text = Common.Common.cis_billing.genTotalSum.ToString("0.00");
            }
            else
            {
                txtGenAmtPaid.Text = Common.Common.cis_billing.genTotalSum.ToString("0.00");
                lblGenDueAmt.Text = "0.00";
            }

            lblGenTotalNetAmt.Text = Common.Common.cis_billing.genTotalSum.ToString("0.00"); //Display Total Sum
            //txtGenAmtPaid.Text = Common.Common.cis_billing.genTotalSum.ToString("0.00");
            txtGenDiscount.Text = "0.00";
            //lblGenDueAmt.Text = "0.00";
            calculateGrandTotal();
        }

        private void clearInvestigationInputValues()
        {
            cboInvCodeInvestigation.SelectedIndex = -1;
            lblInvNameInvestigation.Text = string.Empty;
            txtQtyInvestigation.Text = string.Empty;
            txtUnitPriceInvestigation.Text = "0.00";
            lblTotalAmtInv.Text = "0.00";
            lblShareAmt.Text = "0.00";
            lblShareType.Text = "0";
            Common.Common.cis_investigation_info.investigationId = 0;
            Common.Common.cis_investigation_info.investigationCode = string.Empty;
            Common.Common.cis_investigation_info.investigationName = string.Empty;
            Common.Common.cis_investigation_info.investigationDeptId = 0;
            Common.Common.cis_investigation_info.investigationDeptName = string.Empty;
            lblBillNoInvestigation.Text = string.Empty;
            lblCheckEditModeInv.Text = string.Empty;
        }

        private void clearInvestigationRecords()
        {
            clearInvestigationInputValues();
            dgvInvestigationBill.Rows.Clear();
            dgvInvestigationBill.DataSource = null;
            lblInvestigationTotalAmt.Text = "0.00";
            txtDiscountInvestigation.Text = "0.00";
            txtAmtPaidInvestigation.Text = "0.00";
            lblDueInvestigation.Text = "0.00";
            Common.Common.cis_investigation_info.investigationTotalSum = 0;
            calculateGrandTotal();
        }

        private void clearInvoicePatientInfo()
        {
            txtPatientIdInv.Text = string.Empty;
            txtPatientNameInv.Text = string.Empty;
            lblBillDateInv.Text = string.Empty;
            cboGenderInv.SelectedIndex = 1;
            txtAgeYearInv.Text = string.Empty;
            txtAgeMonthInv.Text = string.Empty;
            txtAgeDayInv.Text = string.Empty;
            lblVisitTypeInv.Text = string.Empty;
            lblVisitNumberInv.Text = string.Empty;
            cboDoctorInv.SelectedIndex = 0;
            lblBedDetailsInv.Text = string.Empty;
            multitxtAddressInv.Text = string.Empty;
            txtPatientNameInv.Enabled = true;
            cboGenderInv.Enabled = true;
            multitxtAddressInv.Enabled = true;
            lblGrandTotalInv.Text = "0.00";
            lblCorporateName.Text = string.Empty;
            lblBillDateInv.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        }

        private void clearPhaInputValues()
        {
            cboItemPha.SelectedIndex = -1;
            lblItemTypePha.Text = string.Empty;
            lblExpDatePha.Text = string.Empty;
            cboLotIdPha.DataSource = null;
            txtQtyPha.Text = string.Empty;
            txtUnitPricePha.Text = string.Empty;
            lblAvailQtyPha.Text = "0.00";
            lblTotalAvailQtyPha.Text = "0.00";
            lblFreeCarePha.Text = "0.00";
            lblTotalAmtPha.Text = "0.00";
            Common.Common.cis_pharmacy_info.phaItemId = 0;
            Common.Common.cis_pharmacy_info.phaItemName = string.Empty;
            Common.Common.cis_pharmacy_info.phaItemTypeId = 0;
            Common.Common.cis_pharmacy_info.phaItemType = string.Empty;
            Common.Common.cis_pharmacy_info.lotId = string.Empty;
            Common.Common.cis_pharmacy_info.phaExpDate = string.Empty;
            Common.Common.cis_pharmacy_info.phaQty = 0;
            Common.Common.cis_pharmacy_info.phaUnitPrice = 0;
            Common.Common.cis_pharmacy_info.phaQty = 0;
            Common.Common.cis_pharmacy_info.phaFreeCareTotal = 0;
            Common.Common.cis_pharmacy_info.phaTotalAmt = 0;
            Common.Common.cis_pharmacy_info.phaDeptId = 0;
            Common.Common.cis_pharmacy_info.inventoryStockId = 0;
            Common.Common.cis_pharmacy_info.phaTotalSum = 0;
            lblPharmacyTotalAmt.Text = "0.00";
            txtDiscountPha.Text = "0.00";
            lblTotalFreeCare.Text = "0.00";
            txtAmountPaidPha.Text = "0.00";
            txtAdvAdjPha.Text = "0.00";
            lblDuePha.Text = "0.00";
            lblBillNoPha.Text = string.Empty;
            lblSaleTaxPerc.Text = string.Empty;
            lblinventoryStockId.Text = "0";
            lblCheckEditModePha.Text = string.Empty;
        }

        private void clearGeneralInputValues()
        {
            cboGenAccountName.SelectedIndex = -1;
            lblAccountGroupId.Text = string.Empty;
            lblAccountGroup.Text = string.Empty;
            txtGenQty.Text = string.Empty;
            txtUnitpriceGen.Text = "0.00";
            lblGenTotalAmt.Text = "0.00";
            Common.Common.cis_billing.accountHeadId = 0;
            Common.Common.cis_billing.accountName = string.Empty;
            Common.Common.cis_billing.accountHeadId = 0;
            Common.Common.cis_billing.accountGroupName = string.Empty;
            Common.Common.cis_billing.qtyGen = 0;
            Common.Common.cis_billing.genUnitPrice = 0;
            Common.Common.cis_billing.genTotal = 0;
            lblCheckEditModeGen.Text = string.Empty;
            txtGenAdvAdj.Text = "0.00";
            txtGenAmtPaid.Text = "0.00";
            txtGenDiscount.Text = "0.00";
            lblGenDueAmt.Text = "0.00";
            lblGenBillNo.Text = string.Empty;
        }

        private void calculatePhaSum()
        {
            decimal sum = 0;
            decimal totalFC = 0;
            Common.Common.cis_pharmacy_info.phaTotalSum = 0;
            Common.Common.cis_pharmacy_info.phaFreeCareTotal = 0;
            foreach (DataGridViewRow row in dgvPharmacyBillDetails.Rows) //Calculate Amount Columns
            {
                sum = Convert.ToDecimal(row.Cells[10].Value);
                Common.Common.cis_pharmacy_info.phaTotalSum = Common.Common.cis_pharmacy_info.phaTotalSum + sum;
                totalFC = Convert.ToDecimal(row.Cells[9].Value);
                Common.Common.cis_pharmacy_info.phaFreeCareTotal = Common.Common.cis_pharmacy_info.phaFreeCareTotal + totalFC;
            }

            lblPharmacyTotalAmt.Text = Math.Round(Common.Common.cis_pharmacy_info.phaTotalSum).ToString("0.00"); //Display Total Sum
            lblTotalFreeCare.Text = Math.Round(Common.Common.cis_pharmacy_info.phaFreeCareTotal).ToString("0.00"); //Display Total Free care

            if (!string.IsNullOrEmpty(lblCorporateName.Text.ToString()))//By default corporate should be in due
            {
                txtAmountPaidPha.Text = "0.00";
                lblDuePha.Text = (Math.Round(Common.Common.cis_pharmacy_info.phaTotalSum) - Math.Round(Common.Common.cis_pharmacy_info.phaFreeCareTotal)).ToString("0.00"); //Display Due
           
            }
            else
            {
                txtAmountPaidPha.Text = (Math.Round(Common.Common.cis_pharmacy_info.phaTotalSum) - Math.Round(Common.Common.cis_pharmacy_info.phaFreeCareTotal)).ToString("0.00"); //Display Due
                lblDuePha.Text = "0.00";
            }

            //txtAmountPaidPha.Text = (Math.Round(Common.Common.cis_pharmacy_info.phaTotalSum) - Math.Round(Common.Common.cis_pharmacy_info.phaFreeCareTotal)).ToString("0.00"); //Display Due
            //lblDuePha.Text = "0.00";
            txtDiscountPha.Text = "0.00";
            calculateGrandTotal();
        }

        private void calculateGrandTotal()
        {
            Common.Common.totalInvoice = ((objBusinessFacade.NonBlankValueOfDecimal(lblInvestigationTotalAmt.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtDiscountInvestigation.Text.ToString())) + (objBusinessFacade.NonBlankValueOfDecimal(lblPharmacyTotalAmt.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtDiscountPha.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(lblTotalFreeCare.Text.ToString())) + (objBusinessFacade.NonBlankValueOfDecimal(lblGenTotalNetAmt.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtGenDiscount.Text.ToString())));
            lblGrandTotalInv.Text = Common.Common.totalInvoice.ToString("0.00");
            lblTotalCollectAmt.Text = (Common.Common.totalInvoice + Common.Common.module_visit_info.netTotalReg).ToString("0.00");
        }

        private void clearPharmacyRecords()
        {
            clearPhaInputValues();
            dgvPharmacyBillDetails.Rows.Clear();
            dgvPharmacyBillDetails.DataSource = null;
            lblPharmacyTotalAmt.Text = "0.00";
            Common.Common.cis_pharmacy_info.phaTotalSum = 0;
            calculateGrandTotal();
        }

        private void clearGenRecords()
        {
            clearGeneralInputValues();
            dgvGenBill.Rows.Clear();
            dgvGenBill.DataSource = null;
            lblGenTotalNetAmt.Text = "0.00";
            lblGenDueAmt.Text = "0.00";
            Common.Common.cis_billing.genTotalSum = 0;
            calculateGrandTotal();
        }

        private void clearCancelBillRecords()
        {
            dgvCancellationInvoice.Rows.Clear();
            dgvCancellationInvoice.DataSource = null;

            txtcBillNo.Text = string.Empty;
            lblcPatientName.Text = string.Empty;
            lblcBillDate.Text = string.Empty;
            lblcCancelledDate.Text = string.Empty;
            lblcPatientId.Text = string.Empty;
            lblcVisitNo.Text = string.Empty;
            lblcBillId.Text = string.Empty;
            lblcBillType.Text = string.Empty;
            cbCancelAll.Checked = false;
            lbloGrandTotal.Text = "0.00";
            lbloDiscount.Text = "0.00";
            lbloFreeCare.Text = "0.00";
            lbloAdvAdj.Text = "0.00";
            lbloDue.Text = "0.00";
            lbloAmountPaid.Text = "0.00";

            lblnGrandTotal.Text = "0.00";
            txtnDiscount.Text = "0.00";
            lblnFreeCare.Text = "0.00";
            txtnAdvAdj.Text = "0.00";
            lblnDue.Text = "0.00";
            txtnAmountPaid.Text = "0.00";
            lblnRefundAmt.Text = "0.00";
            lblNRefundToPatient.Text = "0.00";
            lblAlreadyPaid.Text = "0.00";
            lblRefundFreeCare.Text = "0.00";
            lblcCancelledDate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        }

        private void loadRoom(int wardId)
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadRoomReg(wardId);
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboRoomNo.ValueMember = "room_id";
                    cboRoomNo.DisplayMember = "room_no";
                    cboRoomNo.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadBed(int roomId)
        {
            try
            {
                dtSource = null;
                dtSource = objBusinessFacade.loadBedReg(roomId);
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboBedNo.ValueMember = "bed_id";
                    cboBedNo.DisplayMember = "bed_number";
                    cboBedNo.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void calculateInvCancelSum()//Calculate for Grand Total, Refund Amt and Free Care
        {
            decimal sum = 0;
            decimal refundSum = 0;
            decimal freeCaresum = 0;
            decimal freeCareRefundSum = 0;

            Common.Common.totalSum = 0;
            Common.Common.totalRefundSum = 0;
            Common.Common.freeCareTotalSum = 0;
            Common.Common.freeCareTotalRefundSum = 0;

            foreach (DataGridViewRow row in dgvCancellationInvoice.Rows)
            {
                sum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cTotalAmt"].Value));
                Common.Common.totalSum = Common.Common.totalSum + sum; //Calculate Total Amt
                refundSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cRefundAmt"].Value));
                Common.Common.totalRefundSum = Common.Common.totalRefundSum + refundSum; //Calculate Refund Amt

                freeCaresum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cFreeCare"].Value));
                Common.Common.freeCareTotalSum = Common.Common.freeCareTotalSum + freeCaresum;
                freeCareRefundSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["cRefundFCare"].Value));
                Common.Common.freeCareTotalRefundSum = Common.Common.freeCareTotalRefundSum + freeCareRefundSum;
            }

            lblnGrandTotal.Text = Math.Round(Common.Common.totalSum - Common.Common.totalRefundSum).ToString("0.00"); //Display Grand Total
            lblnRefundAmt.Text = Math.Round(Common.Common.totalRefundSum).ToString("0.00"); //Display Refund Amt

            lblnFreeCare.Text = Math.Round((Common.Common.freeCareTotalSum - Common.Common.freeCareTotalRefundSum)).ToString("0.00"); //Display Free Care
            lblRefundFreeCare.Text = Math.Round(Common.Common.freeCareTotalRefundSum).ToString("0.00"); //Display Fefund Free Care
            calculateInvCanBillDetails();
        }

        private void calculateInvCanBillDetails()//Calculate for Amount Paid, Refund to Patient and Due
        {
            Common.Common.cis_investigation_info.oldInvAmtPaid = objBusinessFacade.NonBlankValueOfDecimal(lbloAmountPaid.Text.ToString());
            Common.Common.cis_investigation_info.investigationDiscountAmt = objBusinessFacade.NonBlankValueOfDecimal(txtnDiscount.Text.ToString()); //Invoice Discount Amt
            Common.Common.totalSum = objBusinessFacade.NonBlankValueOfDecimal(lblnGrandTotal.Text.ToString());
            Common.Common.freeCareTotalSum = objBusinessFacade.NonBlankValueOfDecimal(lblnFreeCare.Text.ToString());
            Common.Common.totalRefundSum = objBusinessFacade.NonBlankValueOfDecimal(lblnRefundAmt.Text.ToString());
            Common.Common.alreadyPaid = objBusinessFacade.NonBlankValueOfDecimal(lblAlreadyPaid.Text.ToString());

            if (Common.Common.cis_investigation_info.oldInvAmtPaid >= (Common.Common.totalSum - Common.Common.freeCareTotalSum - Common.Common.cis_investigation_info.investigationDiscountAmt))
            {
                lblAlreadyPaid.Text = (Common.Common.totalSum - Common.Common.freeCareTotalSum - Common.Common.cis_investigation_info.investigationDiscountAmt).ToString("0.00");
                //txtnAmountPaid.Text = (Common.Common.totalSum - Common.Common.freeCareTotalSum - Common.Common.cis_investigation_info.investigationDiscountAmt).ToString("0.00");
                lblnDue.Text = "0.00";
                lblNRefundToPatient.Text = (Common.Common.cis_investigation_info.oldInvAmtPaid - objBusinessFacade.NonBlankValueOfDecimal(txtnAmountPaid.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(lblAlreadyPaid.Text.ToString())).ToString();
            }

            else if (objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(txtnAmountPaid.Text)) > 0 && objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblAlreadyPaid.Text)) <= (Common.Common.totalSum - Common.Common.freeCareTotalSum - Common.Common.cis_investigation_info.investigationDiscountAmt))
            {
                //txtnAmountPaid.Text = Convert.ToString(txtnAmountPaid.Text);
                lblnDue.Text = (Common.Common.totalSum - Common.Common.freeCareTotalSum - Common.Common.cis_investigation_info.investigationDiscountAmt - objBusinessFacade.NonBlankValueOfDecimal(lblAlreadyPaid.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtnAmountPaid.Text.ToString())).ToString();
                lblNRefundToPatient.Text = "0.00";
            }

            else
            {
                lblAlreadyPaid.Text = Common.Common.cis_investigation_info.oldInvAmtPaid.ToString("0.00");
                //txtnAmountPaid.Text = Common.Common.cis_investigation_info.oldInvAmtPaid.ToString("0.00");
                lblnDue.Text = (Common.Common.totalSum - Common.Common.freeCareTotalSum - Common.Common.cis_investigation_info.investigationDiscountAmt - objBusinessFacade.NonBlankValueOfDecimal(lblAlreadyPaid.Text.ToString())).ToString();
                lblNRefundToPatient.Text = "0.00";
            }

            if (Common.Common.totalRefundSum == 0)
            {
                lblnGrandTotal.Text = "0.00";
                lblnFreeCare.Text = "0.00";
            }
        }
        #endregion

        private void cboCorporate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDoctor.Items.Count > 0 && cboCorporate.Items.Count > 0 && cboCorporate.SelectedIndex > 0)
            {
                try
                {
                    Common.Common.cis_Corporate.corporateId = Convert.ToInt32(cboCorporate.SelectedValue.ToString());
                    dtSource = null;
                    dtSource = objBusinessFacade.getChargesApplicableCorp(Common.Common.cis_Corporate.corporateId);
                    if ((dtSource.Rows.Count > 0) && (Convert.ToInt32(dtSource.Rows[0]["is_charges_applicable"].ToString()) == 1))
                    {
                        viewDoctorFeeByCorporate();
                        calculateRegFee();
                    }
                    else
                    {
                        viewDoctorFee();
                        calculateRegFee();
                    }
                    lblEmployeeId.Visible = true;
                    txtEmployeeId.Visible = true;
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }

            else
            {
                if (cboDoctor.Items.Count > 0)
                {
                    viewDoctorFee();
                    calculateRegFee();
                }
            }
        }

        private void cboSocialTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPatientName.Focus();
                e.Handled = true;
            }
        }

        private void txtAgeYear_Leave(object sender, EventArgs e)
        {
            //CalculateYourDob();
        }

        //Calculate Date Of Birth based on Given Age, Month and Year
        private void CalculateYourDob()
        {
            int givenYear = (String.IsNullOrEmpty(txtAgeYear.Text) ? 0 : Convert.ToInt32(txtAgeYear.Text));
            int givenMonth = (String.IsNullOrEmpty(txtAgeMonth.Text) ? 0 : Convert.ToInt32(txtAgeMonth.Text));
            int givenDay = (String.IsNullOrEmpty(txtAgeDay.Text) ? 0 : Convert.ToInt32(txtAgeDay.Text));

            if (givenYear > 0 || givenMonth > 0 || givenDay > 0)
            {
                //DateTime currentDate = DateTime.Now;
                //string[] currentDate = DateTime.Now.Date.ToShortDateString().Split('/');
                int currentDay = Convert.ToInt32(DateTime.Now.Day.ToString());
                int currentMonth = Convert.ToInt32(DateTime.Now.Month.ToString());
                int currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());


                int dobDate = currentDay - givenDay;
                int dobMonth = currentMonth - givenMonth;
                int dobYear = currentYear - givenYear;

                if ((currentDay - givenDay) <= 0)
                    dobMonth = dobMonth - 1;

                if (dobMonth <= 0)
                {
                    dobYear = dobYear - 1;
                    dobMonth = dobMonth + 12;
                }

                if (givenMonth > 11)
                {
                    MessageBox.Show("The months should not exceed 11");
                    txtAgeMonth.Text = "11";
                    txtAgeMonth.Focus();
                    return;
                }

                if (leapYear(dobYear))
                {
                    if (givenDay > (daysOfMonthLY[dobMonth - 1] - 1))
                    {
                        MessageBox.Show("The days should not exceed " + (daysOfMonthLY[dobMonth - 1] - 1).ToString());
                        txtAgeDay.Text = (daysOfMonthLY[dobMonth - 1] - 1).ToString();
                        txtAgeDay.Focus();

                        return;
                    }
                }

                else
                {
                    if (givenDay > (daysOfMonth[dobMonth - 1] - 1))
                    {
                        MessageBox.Show("The days should not exceed " + (daysOfMonth[dobMonth - 1] - 1).ToString());
                        txtAgeDay.Text = (daysOfMonth[dobMonth - 1] - 1).ToString();
                        txtAgeDay.Focus();

                        return;
                    }
                }

                if (dobDate <= 0)
                {
                    if (leapYear(dobYear))
                        dobDate = dobDate + daysOfMonthLY[dobMonth - 1];
                    else
                        dobDate = dobDate + daysOfMonth[dobMonth - 1];
                }

                DateTime dateOfBirth = Convert.ToDateTime((dobYear.ToString() + "/" + dobMonth.ToString() + "/" + dobDate.ToString()));

                dtpDOB.Value = dateOfBirth;
            }
        }

        private bool leapYear(int year)
        {
            bool flag = false;
            if (year % 400 == 0)
                flag = true;
            else if (year % 100 == 0)
                flag = true;
            else if (year % 4 == 0)
                flag = true;
            else
                flag = false;
            return flag;
        }

        private void txtAgeMonth_Leave(object sender, EventArgs e)
        {
            //CalculateYourDob();
        }

        private void txtAgeDay_Leave(object sender, EventArgs e)
        {
            //CalculateYourDob();
        }

        private void cboAddress1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cboAddress1.Items.Count > 1 && cboAddress1.SelectedIndex > 0)
            {
                Common.Common.cis_Address_Info.addressId = Convert.ToInt32(cboAddress1.SelectedValue.ToString());

                try
                {
                    dtSource = null;
                    dtSource = objBusinessFacade.fetchAddressInfobyPlace(Common.Common.cis_Address_Info.addressId);
                    if (dtSource.Rows.Count > 0)
                    {
                        multitxtAddress.Text = Convert.ToString(dtSource.Rows[0]["address"].ToString());
                        txtPhoneNo.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void txtDiscountInvestigation_Leave(object sender, EventArgs e)
        {
            txtDiscountInvestigation.Text = AutoFormatToDecimalValue(txtDiscountInvestigation.Text);
            if (Convert.ToDouble(txtDiscountInvestigation.Text) > Convert.ToDouble(lblInvestigationTotalAmt.Text))
            {
                MessageBox.Show("Discount Amount should not be greater than Bill Amount!", "Lab Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDiscountInvestigation.Focus();
                txtDiscountInvestigation.Text = "0.00";
                CalculateDue();
                return;
            }
            if (Convert.ToDouble(txtDiscountInvestigation.Text) + Convert.ToDouble(txtAmtPaidInvestigation.Text) + Convert.ToDouble(txtAdvAdjInvestigation.Text) >
                Convert.ToDouble(lblInvestigationTotalAmt.Text))
            {
                double NetTotal = Convert.ToDouble(lblInvestigationTotalAmt.Text) - Convert.ToDouble(txtDiscountInvestigation.Text);
                if (Convert.ToDouble(txtAmtPaidInvestigation.Text) > 0)
                {
                    txtAmtPaidInvestigation.Text = NetTotal.ToString("############0.00");
                    txtAmtPaidInvestigation.Focus();
                }
            }
            CalculateLabBill();
            txtAmtPaidInvestigation.Focus();
        }

        private void txtAdvAdjInvestigation_Leave(object sender, EventArgs e)
        {
            txtAdvAdjInvestigation.Text = AutoFormatToDecimalValue(txtAdvAdjInvestigation.Text);
        }

        private void CalculateLabBill()
        {
            double labBillAmount = Convert.ToDouble(lblInvestigationTotalAmt.Text.ToString());
            double labDiscountAmount = Convert.ToDouble(txtDiscountInvestigation.Text.ToString());
            double labAmountPaid = Convert.ToDouble(txtAmtPaidInvestigation.Text.ToString());
            double labAdvanceAdjusted = Convert.ToDouble(txtAdvAdjInvestigation.Text.ToString());

            if (labAmountPaid > labBillAmount)
            {
                MessageBox.Show("Amount Paid should not be greater than Bill Amount!", "Issue Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labAmountPaid = 0.00;
                txtAmtPaidInvestigation.Focus();
                txtAmtPaidInvestigation.Text = labAmountPaid.ToString();
            }
            if (labDiscountAmount > labBillAmount)
            {
                MessageBox.Show("Discount should not be greater than Bill Amount!", "Issue Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labDiscountAmount = 0.00;
                txtDiscountInvestigation.Focus();
                txtDiscountInvestigation.Text = labDiscountAmount.ToString();
            }
            if (labAmountPaid + labAdvanceAdjusted > labBillAmount - labDiscountAmount)
            {
                MessageBox.Show("Amount Paid should not be greater than Payment Amount!", "Issue Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labAmountPaid = 0.00;
                txtAmtPaidInvestigation.Focus();
                txtAmtPaidInvestigation.Text = labAmountPaid.ToString();
            }
            if (labDiscountAmount + labAmountPaid > labBillAmount)
            {
                MessageBox.Show("Discount and Amount Paid should not be greater than Bill Amount!", "Issue Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labDiscountAmount = 0.00;
                txtDiscountInvestigation.Text = labAmountPaid.ToString();
            }
            if (labDiscountAmount + labAmountPaid + labAdvanceAdjusted > labBillAmount)
            {
                MessageBox.Show("Amount Paid should not be greater than Payment Amount!", "Issue Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labAmountPaid = 0.00;
                txtAmtPaidInvestigation.Focus();
                txtAmtPaidInvestigation.Text = labAmountPaid.ToString();
            }
            CalculateDue();
            calculateGrandTotal();
            //calculateInvSum();
        }

        private void CalculateDue()
        {
            //Calculate Due
            Double labDue = Convert.ToDouble(lblInvestigationTotalAmt.Text.ToString()) - (Convert.ToDouble(txtAmtPaidInvestigation.Text.ToString()) +
                Convert.ToDouble(txtAdvAdjInvestigation.Text.ToString()) + Convert.ToDouble(txtDiscountInvestigation.Text.ToString()));
            lblDueInvestigation.Text = labDue.ToString("############0.00");
            lblTotalCollectAmt.Text = (Convert.ToDouble(lblDuePha.Text) + Convert.ToDouble(lblGenDueAmt.Text) + labDue).ToString("############0.00");
        }

        private void txtAmtPaidInvestigation_Leave(object sender, EventArgs e)
        {
            txtAmtPaidInvestigation.Text = AutoFormatToDecimalValue(txtAmtPaidInvestigation.Text);
            if (Convert.ToDouble(txtAmtPaidInvestigation.Text) + Convert.ToDouble(txtAdvAdjInvestigation.Text) >
                Convert.ToDouble(lblInvestigationTotalAmt.Text) - Convert.ToDouble(txtDiscountInvestigation.Text))
            {
                MessageBox.Show("Amount Paid should not be greater than Bill Amount!", "Issue Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAmtPaidInvestigation.Focus();
                txtAmtPaidInvestigation.Text = "0.00";

                CalculateDue();
                return;
            }
            CalculateLabBill();
            btnSave.Focus();
        }

        private void txtAmountPaidInv_Leave(object sender, EventArgs e)
        {
            txtAmountPaidInv.Text = AutoFormatToDecimalValue(txtAmountPaidInv.Text);
        }

        private void txtDiscountValueInv_Leave(object sender, EventArgs e)
        {
            txtDiscountValueInv.Text = AutoFormatToDecimalValue(txtDiscountValueInv.Text);
        }

        public string AutoFormatToDecimalValue(string value)
        {
            if (!(string.IsNullOrWhiteSpace(value)))
            {
                double amount = Convert.ToDouble(value);
                value = amount.ToString("#0.00"); //Amount.ToString("#,0.00");
            }
            else
                value = "0.00";
            return value;
        }

        private void txtAmtPaidInvestigation_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtAmtPaidInvestigation_Leave(sender, e);
                #region CommenetedLinesofJesu
                //txtAdvAdjInvestigation.Select();
                //e.Handled = true;

                //Common.Common.cis_investigation_info.investigationTotal = Convert.ToDecimal(lblInvestigationTotalAmt.Text.ToString());
                //Common.Common.cis_investigation_info.investigationDiscountAmt = Convert.ToDecimal(txtDiscountInvestigation.Text.ToString());
                //Common.Common.cis_investigation_info.amountPaidInvestigation = Convert.ToDecimal(txtAmtPaidInvestigation.Text.ToString());
                //Common.Common.cis_investigation_info.balanceAmountInvestigation = (Common.Common.cis_investigation_info.investigationTotal - Common.Common.cis_investigation_info.investigationDiscountAmt);

                //if (!string.IsNullOrEmpty(txtAmtPaidInvestigation.Text))
                //{
                //    if (Common.Common.cis_investigation_info.balanceAmountInvestigation >= Common.Common.cis_investigation_info.amountPaidInvestigation)
                //    {
                //        Common.Common.cis_investigation_info.balanceAmountInvestigation = (Common.Common.cis_investigation_info.investigationTotal - Common.Common.cis_investigation_info.investigationDiscountAmt - Common.Common.cis_investigation_info.amountPaidInvestigation);
                //        lblDueInvestigation.Text = Convert.ToString(Common.Common.cis_investigation_info.balanceAmountInvestigation);
                //    }
                //    //else
                //    {
                //        MessageBox.Show("Amount can't be greater than Net Total....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txtAmtPaidInvestigation.Text = "0.00";
                //        Common.Common.cis_investigation_info.balanceAmountInvestigation = (Common.Common.cis_investigation_info.investigationTotal - Common.Common.cis_investigation_info.investigationDiscountAmt);
                //        lblDueInvestigation.Text = Convert.ToString(Common.Common.cis_investigation_info.balanceAmountInvestigation);
                //        txtAmtPaidInvestigation.Select();
                //    }
                //}
                //else
                //{
                //    Common.Common.cis_investigation_info.amountPaidInvestigation = 0;
                //}
                #endregion
            }
        }

        private void disableTabControls(int role_id)
        {
            dtSource = null;
            dtSource = objBusinessFacade.getUserActivities(role_id, 2);

            this.tpRegistration.Parent = null;
            this.tpInvoice.Parent = null;
            this.tpCancelOrInvoice.Parent = null;
            this.tpInvestigation.Parent = null;
            this.tpPharmacy.Parent = null;
            this.tpGeneralBill.Parent = null;

            foreach (DataRow sRow in dtSource.Rows)
            {
                switch (Convert.ToInt32(sRow["action_id"].ToString()))
                {
                    case 35: //Enable -> Registration
                        this.tpRegistration.Parent = this.tpRegInvoice;
                        break;

                    case 36: //Enable -> Invoice
                        this.tpInvoice.Parent = this.tpRegInvoice;
                        break;

                    case 37: //Enable -> Cancel/Refund
                        this.tpCancelOrInvoice.Parent = this.tpRegInvoice;
                        break;

                    case 38: //Enable -> Investigation
                        this.tpInvestigation.Parent = this.tcInvoiceBill;
                        break;

                    case 39: //Enable -> Pharmacy
                        this.tpPharmacy.Parent = this.tcInvoiceBill;
                        break;

                    case 40: //Enable -> General
                        this.tpGeneralBill.Parent = this.tcInvoiceBill;
                        break;

                    default:
                        break;
                }
            }
        }

        private void cboAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                multitxtAddress.Focus();
                e.Handled = true;
            }*/
        }

        private void txtAdvAdjPha_Leave(object sender, EventArgs e)
        {
            txtAdvAdjPha.Text = AutoFormatToDecimalValue(txtAdvAdjPha.Text);
        }

        private void txtGenDiscount_Leave(object sender, EventArgs e)
        {
            txtGenDiscount.Text = AutoFormatToDecimalValue(txtGenDiscount.Text);
            if (Convert.ToDouble(txtGenDiscount.Text) > Convert.ToDouble(lblGenTotalNetAmt.Text))
            {
                MessageBox.Show("Discount Amount should not be greater than Bill Amount!", "General Service Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGenDiscount.Focus();
                txtGenDiscount.Text = "0.00";
                CalculateGeneralDue();
                return;
            }
            if (Convert.ToDouble(txtGenDiscount.Text) + Convert.ToDouble(txtGenAmtPaid.Text) + Convert.ToDouble(txtGenAdvAdj.Text) >
                Convert.ToDouble(lblGenTotalNetAmt.Text))
            {
                double NetTotal = Convert.ToDouble(lblGenTotalNetAmt.Text) - Convert.ToDouble(txtGenDiscount.Text);
                if (Convert.ToDouble(txtGenAmtPaid.Text) > 0)
                {
                    txtGenAmtPaid.Text = NetTotal.ToString("############0.00");
                    txtGenAmtPaid.Focus();
                }
            }
            CalculateGeneralDue();
            txtGenAmtPaid.Focus();
        }

        private void CalculateGeneralDue()
        {
            //Calculate Due
            Double genDue = Convert.ToDouble(lblGenTotalNetAmt.Text.ToString()) - (Convert.ToDouble(txtGenAmtPaid.Text.ToString()) +
                Convert.ToDouble(txtGenAdvAdj.Text.ToString()) + Convert.ToDouble(txtGenDiscount.Text.ToString()));
            lblGenDueAmt.Text = genDue.ToString("############0.00");
            lblTotalCollectAmt.Text = (Convert.ToDouble(lblDuePha.Text) + Convert.ToDouble(lblDueInvestigation.Text) + genDue).ToString("############0.00");
        }

        private void txtGenAmtPaid_Leave(object sender, EventArgs e)
        {
            txtGenAmtPaid.Text = AutoFormatToDecimalValue(txtGenAmtPaid.Text);
            if (Convert.ToDouble(txtGenAmtPaid.Text) + Convert.ToDouble(txtGenAdvAdj.Text) >
                Convert.ToDouble(lblGenTotalNetAmt.Text) - Convert.ToDouble(txtGenDiscount.Text))
            {
                MessageBox.Show("Amount Paid should not be greater than Bill Amount!", "General Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGenAmtPaid.Focus();
                txtGenAmtPaid.Text = "0.00";

                CalculateGeneralDue();
                return;
            }
            CalculateGeneralDue();
            btnSave.Focus();
        }

        private void txtGenAdvAdj_Leave(object sender, EventArgs e)
        {
            txtGenAdvAdj.Text = AutoFormatToDecimalValue(txtGenAdvAdj.Text);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Alt | Keys.S:
                    saveInputValues();
                    return true;
                case Keys.Alt | Keys.N:
                    clearRegRecords();
                    clearInvoicePatientInfo();
                    clearInvestigationRecords();
                    clearPharmacyRecords();
                    clearGeneralInputValues();
                    clearGenRecords();
                    clearCancelBillRecords();
                    lblTotalCollectAmt.Text = "0.00";
                    Common.Common.flag = 0;
                    return true;
                case Keys.Alt | Keys.C:
                    this.Close();
                    return true;
                case Keys.Alt | Keys.P:
                    print_bill();
                    return true;
                case Keys.F2:
                    //do something
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void tscmbDepartment_Leave(object sender, EventArgs e)
        {
            //tscmbRegistrationType.Focus();
        }

        private void tscmbRegistrationType_Leave(object sender, EventArgs e)
        {
            //RegistrationType_function();
        }

        private void tscmbRegistrationType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RegistrationType_function();
                e.Handled = true;
            }
        }

        private void txtPatientId_Leave(object sender, EventArgs e)
        {
            //txtPatientId_function();
        }

        private void cboSocialTitle_Leave(object sender, EventArgs e)
        {
            //txtPatientName.Focus();
        }

        private void txtPatientName_Leave(object sender, EventArgs e)
        {
            //var keyCode = e.k
            //cboPatientType.Select();
        }

        private void cboPatientType_Leave(object sender, EventArgs e)
        {
            //if (cboGender.Enabled == false)
            //{
            //    txtAgeYear.Focus();
            //}
            //else
            //{
            //    cboGender.Focus();
            //}
        }

        private void txtGuardainName_Leave(object sender, EventArgs e)
        {
            //cboAddress1.Focus();
        }

        private void cboAddress1_Leave(object sender, EventArgs e)
        {
            //multitxtAddress.Focus();
        }

        private void txtPhoneNo_Leave(object sender, EventArgs e)
        {
            //cboCorporate.Focus();
        }

        private void cboClinicalType_Leave(object sender, EventArgs e)
        {
            //cboDoctor.Focus();
        }

        private void cboDoctor_Leave(object sender, EventArgs e)
        {
            //txtDiagnosis.Focus();
        }

        private void txtDiagnosis_Leave(object sender, EventArgs e)
        {
            //cboRegDiscountType.Focus();
        }

        private void cboRegDiscountType_Leave(object sender, EventArgs e)
        {
            //txtRegDiscount.Focus();
        }

        private void txtRegDiscount_Leave(object sender, EventArgs e)
        {
            //txtRegAmountPaid.Select();
            //calculateRegFee();
        }

        private void txtRegAmountPaid_Leave(object sender, EventArgs e)
        {
            //txtAmountPaid_function();
        }

        private void cboWardNo_Leave(object sender, EventArgs e)
        {
            //cboRoomNo.Focus();
        }

        private void cboRoomNo_Leave(object sender, EventArgs e)
        {
            //cboRoomNo.Focus();
        }

        private void cboBedNo_Leave(object sender, EventArgs e)
        {
            //btnSave.Focus();
        }

        private void cboCorporate_Leave(object sender, EventArgs e)
        {
            //cboClinicalType.Focus();
        }

        private void txtPatientIdInv_Leave(object sender, EventArgs e)
        {
            //txtPatientIdInv_function();
        }

        private void txtPatientNameInv_Leave(object sender, EventArgs e)
        {
            //cboGenderInv.Focus();
        }

        private void cboGenderInv_Leave(object sender, EventArgs e)
        {
            //txtAgeYearInv.Focus();
        }

        private void txtAgeYearInv_Leave(object sender, EventArgs e)
        {
            //cboDoctorInv.Focus();
        }

        private void txtPatientName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboPatientType.Select();
                e.IsInputKey = true;
            }
        }

        private void txtPatientId_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPatientId_function();
                e.IsInputKey = true;
            }
        }

        private void cboSocialTitle_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPatientName.Focus();
                e.IsInputKey = true;
            }
        }

        private void cboPatientType_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (cboGender.Enabled == false)
                {
                    txtAgeYear.Focus();
                }
                else
                {
                    cboGender.Focus();
                }
                e.IsInputKey = true;
            }
        }

        private void cboGender_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtAgeYear.Focus();
            }
            e.IsInputKey = true;
        }

        private void txtAgeYear_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                CalculateYourDob();
            }
            e.IsInputKey = true;
        }

        private void txtAgeMonth_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                CalculateYourDob();
            }
            e.IsInputKey = true;
        }

        private void txtAgeDay_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                CalculateYourDob();
            }
            e.IsInputKey = true;
        }

        private void txtGuardainName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboAddress1.Focus();
            }
            e.IsInputKey = true;
        }

        private void cboAddress1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                multitxtAddress.Focus();
            }
            e.IsInputKey = true;
        }

        private void multitxtAddress_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPhoneNo.Focus();
            }
            e.IsInputKey = true;
        }

        private void txtPhoneNo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboCorporate.Focus();
            }
            e.IsInputKey = true;
        }

        private void cboCorporate_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboClinicalType.Focus();
            }
            e.IsInputKey = true;
        }

        private void cboClinicalType_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboDoctor.Focus();
            }
            e.IsInputKey = true;
        }

        private void cboDoctor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtDiagnosis.Focus();
            }
            e.IsInputKey = true;
        }

        private void txtDiagnosis_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboRegDiscountType.Focus();
            } e.IsInputKey = true;
        }

        private void cboRegDiscountType_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtRegDiscount.Focus();
            } e.IsInputKey = true;
        }

        private void txtRegDiscount_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtRegAmountPaid.Select();
                calculateRegFee();
            } e.IsInputKey = true;
        }

        private void txtRegAmountPaid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtAmountPaid_function();
            } e.IsInputKey = true;
        }

        private void cboWardNo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboRoomNo.Focus();
            } e.IsInputKey = true;
        }

        private void cboRoomNo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboRoomNo.Focus();
            } e.IsInputKey = true;
        }

        private void cboBedNo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btnSave.Focus();
            } e.IsInputKey = true;
        }

        private void txtPatientIdInv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPatientIdInv_function();
            } e.IsInputKey = true;
        }

        private void txtPatientNameInv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboGenderInv.Focus();
            } e.IsInputKey = true;
        }

        private void cboGenderInv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtAgeYearInv.Focus();
            } e.IsInputKey = true;
        }

        private void txtAgeYearInv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboDoctorInv.Focus();
            } e.IsInputKey = true;
        }
    }
}