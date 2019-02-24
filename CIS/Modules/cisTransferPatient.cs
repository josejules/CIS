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
    public partial class cisTransferPatient : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cisTransferPatient()
        {
            InitializeComponent();
            this.dtpTransferDate.Value = DateTime.Now;
        }

        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtSource = objBusinessFacade.getPatientDetailsByPatientId(txtPatientId.Text.ToString());
                if (dtSource.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtSource.Rows[0]["bed_number"].ToString()))
                    {
                        lblPatientName.Text = dtSource.Rows[0]["patient_name"].ToString();
                        lblGender.Text = dtSource.Rows[0]["gender_name"].ToString();
                        lblAge.Text = string.Concat(dtSource.Rows[0]["age_year"].ToString(), "Y ", dtSource.Rows[0]["age_month"].ToString(), "M ", dtSource.Rows[0]["age_day"].ToString(),"D");
                        lblVisitNo.Text = dtSource.Rows[0]["last_visit_number"].ToString();
                        lblBedDetails.Text = string.Concat(dtSource.Rows[0]["ward_name"].ToString(), ", ", dtSource.Rows[0]["room_no"].ToString(), ", ", dtSource.Rows[0]["bed_number"].ToString());
                        cboWardNo.Focus();
                    }

                    else
                    {
                        MessageBox.Show("Patient is not admitted....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clearTransferPatient();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Patient Id....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clearTransferPatient();
                }
                e.Handled = true;
            }
        }

        private void cisTransferPatient_Load(object sender, EventArgs e)
        {
            loadWard();
        }

        private void clearTransferPatient()
        {
            txtPatientId.Text = string.Empty;
            lblPatientName.Text = string.Empty;
            lblVisitNo.Text = string.Empty;
            lblGender.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblBedDetails.Text = string.Empty;
            this.dtpTransferDate.Value = DateTime.Now;
            cboWardNo.SelectedIndex = -1;
            cboRoomNo.SelectedIndex = -1;
            cboBedNo.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateControls())
            {
                Common.Common.cis_number_generation.patient_id = Convert.ToString(txtPatientId.Text.ToString());
                Common.Common.cis_number_generation.visit_number = Convert.ToString(lblVisitNo.Text.ToString());
                Common.Common.module_visit_info.transferDate = Convert.ToDateTime(dtpTransferDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                Common.Common.cis_Room.ward_id = Convert.ToInt32(cboWardNo.SelectedValue.ToString());
                Common.Common.cis_Room.roomId = Convert.ToInt32(cboRoomNo.SelectedValue.ToString());
                Common.Common.cis_bed.bedId = Convert.ToInt32(cboBedNo.SelectedValue.ToString());

                objArg.ParamList["patient_id"] = Common.Common.cis_number_generation.patient_id;
                objArg.ParamList["visit_number"] = Common.Common.cis_number_generation.visit_number;
                objArg.ParamList["transferDate"] = Common.Common.module_visit_info.transferDate;
                objArg.ParamList["ward_id"] = Common.Common.cis_Room.ward_id;
                objArg.ParamList["roomId"] = Common.Common.cis_Room.roomId;
                objArg.ParamList["bedId"] = Common.Common.cis_bed.bedId;

                Common.Common.flag = objBusinessFacade.updateTransferInfo(objArg);

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

        private bool ValidateControls()
        {
            if (string.IsNullOrEmpty(txtPatientId.Text.ToString()))
            {
                MessageBox.Show("Please enter Patient Id", "Transfer Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(lblVisitNo.Text.ToString()))
            {
                MessageBox.Show("Please enter valid Patient Id", "Transfer Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboWardNo.SelectedIndex < 1)
            {
                MessageBox.Show("Please select Ward", "Transfer Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboRoomNo.SelectedIndex < 1)
            {
                MessageBox.Show("Please select Room Number", "Transfer Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboBedNo.SelectedIndex < 1)
            {
                MessageBox.Show("Please select Bed Number", "Transfer Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearTransferPatient();
            btnSave.Enabled = true;
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

        private void cboRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRoomNo.SelectedIndex > 0)
            {
                loadBed(Convert.ToInt32(cboRoomNo.SelectedValue.ToString()));
            }
        }

        private void loadWard()
        {
            try
            {
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
            catch (Exception)
            {
                throw;
            }
        }

        private void loadRoom(int wardId)
        {
            try
            {
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
            catch (Exception)
            {
                throw;
            }
        }

        private void loadBed(int roomId)
        {
            try
            {
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
