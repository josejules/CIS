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
using System.Linq;

namespace CIS.DischargeSummary
{
    public partial class frmDischargeSummary : Form
    {
        DataTable dtPatient = new DataTable();
        DataTable dtResultEntry = new DataTable();
        clsLabBusinessFacade objBusiness = new clsLabBusinessFacade();
        Dictionary<int, int> gboxHeightStatic = new Dictionary<int, int>();
        Dictionary<int, int> gboxYvalue = new Dictionary<int, int>();
        public string criteria = string.Empty;
        public int DischargeSummaryId = 0;
        public int BillId = 0;
        int editMode = 0;
        public frmDischargeSummary()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save_data();
        }

        private void save_data()
        {
            try
            {
                int flag = 0;
                int iSaved = 0;
                //Save Lab Test Result
                ComArugments args = new ComArugments();
                args.ParamList[CIS.Common.DischargeSummary.DischargeSummaryId] = DischargeSummaryId;
                args.ParamList[CIS.Common.DischargeSummary.DischargeSummaryNumber] = "1";
                args.ParamList[CIS.Common.DischargeSummary.DischargeSummaryDate] = DateTime.Now;
                args.ParamList[CIS.Common.DischargeSummary.PatientId] = txtPatientId.Text;
                args.ParamList[CIS.Common.DischargeSummary.VisitNumber] = txtVisitNo.Text;
                args.ParamList[CIS.Common.DischargeSummary.DischargeDoctorId] = cboDischargeDoctor.SelectedValue.ToString();
                args.ParamList[CIS.Common.DischargeSummary.DischargeDate] = dtpDischargeDateTime.Value;
                args.ParamList[CIS.Common.DischargeSummary.Diagnosis] = rtxtDiagnosis.Text;
                args.ParamList[CIS.Common.DischargeSummary.OperativeFindings] = rtxtOperativeFindings.Text;
                args.ParamList[CIS.Common.DischargeSummary.Procedure] = rtxtProcedure.Text;
                args.ParamList[CIS.Common.DischargeSummary.Summary] = rtxtSummary.Text;
                args.ParamList[CIS.Common.DischargeSummary.ConditionAtAdmission] = rtxtConditionAtAdmission.Text;
                args.ParamList[CIS.Common.DischargeSummary.Investigations] = rtxtInvestigations.Text;
                args.ParamList[CIS.Common.DischargeSummary.TreatmentGiven] = rtxtTreatmentGiven.Text;
                args.ParamList[CIS.Common.DischargeSummary.ConditionAtDischarge] = rtxtConditionAtDischarge.Text;
                args.ParamList[CIS.Common.DischargeSummary.TreatmentAdviced] = rtxtTreatmentAdviced.Text;
                args.ParamList[CIS.Common.DischargeSummary.ReviewOn] = rtxtReviewOn.Text;
                args.ParamList[CIS.Common.DischargeSummary.LastUpdatedUser] = 1;
                //args.ParamList[CIS.Common.DischargeSummary.FollowupDetailId] = rtxtConditionAtDischarge.Text;

                args.ParamList[clsLabCommon.Laboratory.TestResultEntry.ReportedDateTime] = dtpDischargeDateTime.Value;
                if (DischargeSummaryId == 0)
                    iSaved = objBusiness.SaveDischargeSummary(args);
                else
                    iSaved = objBusiness.SaveResultEntryInfo(args);
                //Delete all the entries in Test_Result_Entry Table and Save the Details
                //int iDeleted = objBusiness.DeletePatientResultEntry(ResultEntryId);
                //foreach (DataRow row in dtResultEntry.Rows)
                //{
                //    LabDynamicControl obj = lstCtrl.Find(ctrl => ctrl.ctrlName.Equals("txt" + row["test_field_id"].ToString() + "_" + row["investigation_id"].ToString()));
                //    string resultValue = string.Empty;
                //    if (obj != null)
                //        resultValue = obj.ctrlValue;

                //    ComArugments argsDetail = new ComArugments();
                //    argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultEntryId] = ResultEntryId;
                //    argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.InvestigationId] = row["investigation_id"].ToString();
                //    argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.TestFieldId] = row["test_field_id"].ToString();
                //    //argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultValue] = resultValue;
                //    argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultValue] = resultValue.Replace("'", "''");
                //    flag = objBusiness.SaveResultEntryDetailInfo(argsDetail);
                //}
                //objBusiness.commitTransction();
                if (iSaved == 1)
                    MessageBox.Show("Discharge Summary is saved", "Discharge Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Discharge Summary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //objBusiness.rollBackTransation();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtPatientId.Text))
                {
                    criteria = "patient_id = '" + txtPatientId.Text.Trim() + "'";
                    LoadPatientDetails();
                }
            }
        }

        public void LoadPatientDetails()
        {
            try
            {
                BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
                DataTable dtSource = new DataTable();
                dtSource = objBusinessFacade.getPatientDetailsByPatientId(txtPatientId.Text.ToString());
                if (dtSource.Rows.Count > 0)
                {
                    txtPatientName.Text = dtSource.Rows[0]["patient_name"].ToString();
                    txtSex.Text = dtSource.Rows[0]["gender_name"].ToString();
                    txtAge.Text = dtSource.Rows[0]["age_year"].ToString();
                    //txtAgeMonthInv.Text = dtSource.Rows[0]["age_month"].ToString();
                    //txtAgeDayInv.Text = dtSource.Rows[0]["age_day"].ToString();
                    //multitxtAddressInv.Text = dtSource.Rows[0]["address"].ToString();
                    txtVisitNo.Text = dtSource.Rows[0]["last_visit_number"].ToString();
                    //lblVisitTypeInv.Text = dtSource.Rows[0]["visit_type"].ToString();
                    cboDoctor.Text = dtSource.Rows[0]["doctor_name"].ToString();
                    cboDischargeDoctor.SelectedValue = dtSource.Rows[0]["doctor_id"].ToString();
                    dtpAdmissionDateTime.Value = (dtSource.Rows[0]["visit_date"].ToString() == string.Empty) ? DateTime.Now : Convert.ToDateTime(dtSource.Rows[0]["visit_date"].ToString());
                    rtxtDiagnosis.Focus();
                }
                else
                {
                    MessageBox.Show("Please enter valid Patient Id or No bills found!", "Result Entry View", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPatientId.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Result Entry View", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            CIS.Modules.cisSearchPatient objShow = new CIS.Modules.cisSearchPatient(this, "DischargeSummary");
            objShow.ShowDialog();
        }

        private void loadDoctorInv()
        {
            try
            {
                BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
                DataTable dtSource = new DataTable();
                dtSource = objBusinessFacade.loadDoctor();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboDischargeDoctor.ValueMember = "DOCTOR_ID";
                    cboDischargeDoctor.DisplayMember = "DOCTOR_NAME";
                    cboDischargeDoctor.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void frmDischargeSummary_Load(object sender, EventArgs e)
        {
            loadDoctorInv();
        }


    }
}
