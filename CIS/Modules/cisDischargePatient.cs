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
    public partial class cisDischargePatient : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cisDischargePatient()
        {
            InitializeComponent();
            this.dtpDischargeDate.Value = DateTime.Now;
            this.dtpExpiryDate.Value = DateTime.Now;
        }

        private void cisDischargePatient_Load(object sender, EventArgs e)
        {
            loadDischargeType();
        }

        private void loadDischargeType()
        {
            try
            {
                dtSource = objBusinessFacade.loadDischargeType();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboDischargeType.ValueMember = "cis_discharge_type_id";
                    cboDischargeType.DisplayMember = "cis_discharge_type";
                    cboDischargeType.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
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
                    lblBedDetails.Text = string.Concat(dtSource.Rows[0]["ward_name"].ToString(), ", ", dtSource.Rows[0]["room_no"].ToString(), ", ", dtSource.Rows[0]["bed_number"].ToString());
                    cboDischargeType.Focus();
                }

                else
                {
                    MessageBox.Show("Patient is not admitted....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPatientId.Text = string.Empty;
                    //clearDischargePatient();
                }
            }
            else
            {
                MessageBox.Show("Invalid Patient Id....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPatientId.Text = string.Empty;
                //clearDischargePatient();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtPatientId.Text))
            {
                MessageBox.Show("Please enter Patient Id", "Discharge Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cboDischargeType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Discharge Type", "Discharge Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(lblVisitNo.Text.ToString()))
            {
                MessageBox.Show("Please enter valid Patient Id", "Discharge Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!string.IsNullOrEmpty(lblVisitNo.Text.ToString()))
            {
                Common.Common.cis_number_generation.patient_id = Convert.ToString(txtPatientId.Text.ToString());
                Common.Common.cis_number_generation.visit_number = Convert.ToString(lblVisitNo.Text.ToString());
                Common.Common.module_visit_info.dischargeDate = Convert.ToDateTime(dtpDischargeDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                Common.Common.module_visit_info.dichargeTypeId = Convert.ToInt32(cboDischargeType.SelectedValue.ToString());
                Common.Common.module_visit_info.expiryDate = Convert.ToDateTime(dtpExpiryDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                objArg.ParamList["patient_id"] = Common.Common.cis_number_generation.patient_id;
                objArg.ParamList["visit_number"] = Common.Common.cis_number_generation.visit_number;
                objArg.ParamList["dischargeDate"] = Common.Common.module_visit_info.dischargeDate;
                objArg.ParamList["dichargeTypeId"] = Common.Common.module_visit_info.dichargeTypeId;
                objArg.ParamList["expiryDate"] = Common.Common.module_visit_info.expiryDate;
                objArg.ParamList["dischargeType"] = cboDischargeType.Text;

                Common.Common.flag = objBusinessFacade.updateDischargeInfo(objArg);

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
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearDischargePatient();
            btnSave.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboDischargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDischargeType.SelectedIndex >= 0)
            {
                //Common.Common.module_visit_info.dichargeTypeId = Convert.ToInt32(cboDischargeType.SelectedValue.ToString());
                if (cboDischargeType.Text == "Expired")
                {
                    dtpExpiryDate.Enabled = true;
                }
                else
                {
                    dtpExpiryDate.Enabled = false;
                }
            }
        }

        private void clearDischargePatient()
        {
            txtPatientId.Text = string.Empty;
            lblPatientName.Text = string.Empty;
            lblVisitNo.Text = string.Empty;
            lblGender.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblBedDetails.Text = string.Empty;
            cboDischargeType.SelectedIndex = 0;
            this.dtpDischargeDate.Value = DateTime.Now;
            this.dtpExpiryDate.Value = DateTime.Now;
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
                e.IsInputKey = true;
            }
        }
    }
}
