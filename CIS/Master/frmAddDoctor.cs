using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CIS;
using CIS.Common;
using CIS.BusinessFacade;

namespace CIS.Master
{
    public partial class frmAddDoctor : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        public frmAddDoctor()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }

        public frmAddDoctor(int id)
        {
            InitializeComponent();
            Common.Common.cis_Doctor.doctorId = id;
            this.Text = "Edit Doctor";
        }

        private void frmAddDoctor_Load(object sender, EventArgs e)
        {
            if (Common.Common.cis_Doctor.doctorId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getDoctorRecord(Common.Common.cis_Doctor.doctorId);
                txtDoctorCode.Text = dtSource.Rows[0]["doctor_code"].ToString();
                txtDoctorName.Text = dtSource.Rows[0]["doctor_name"].ToString();
                cboGender.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["gender"].ToString());
                cboDoctorType.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["doctor_type"].ToString());
                txtQualification.Text = dtSource.Rows[0]["Qualification"].ToString();
                txtSpecialization.Text = dtSource.Rows[0]["Specialization"].ToString();
                txtRoomNumber.Text = dtSource.Rows[0]["room_number"].ToString();
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["STATUS"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrWhiteSpace(txtDoctorCode.Text.Trim())) && !(string.IsNullOrWhiteSpace(txtDoctorName.Text.Trim())))
                {
                    Common.Common.cis_Doctor.doctorCode = txtDoctorCode.Text.ToString().Trim();
                    Common.Common.cis_Doctor.doctorName = txtDoctorName.Text.ToString().Trim();
                    Common.Common.gender = Convert.ToInt32(cboGender.SelectedIndex.ToString());
                    Common.Common.cis_Doctor.doctorType = Convert.ToInt32(cboDoctorType.SelectedIndex.ToString());
                    Common.Common.cis_Doctor.qualification = txtQualification.Text.ToString().Trim();
                    Common.Common.cis_Doctor.specialization = txtSpecialization.Text.ToString().Trim();
                    Common.Common.cis_Doctor.roomNumber = txtRoomNumber.Text.ToString().Trim();
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    objArg.ParamList["DOCTOR_ID"] = Common.Common.cis_Doctor.doctorId;
                    objArg.ParamList["doctor_code"] = Common.Common.cis_Doctor.doctorCode;
                    objArg.ParamList["doctor_name"] = Common.Common.cis_Doctor.doctorName;
                    objArg.ParamList["doctor_type"] = Common.Common.cis_Doctor.doctorType;
                    objArg.ParamList["Gender"] = Common.Common.gender;
                    objArg.ParamList["Qualification"] = Common.Common.cis_Doctor.qualification;
                    objArg.ParamList["Specialization"] = Common.Common.cis_Doctor.specialization;
                    objArg.ParamList["room_number"] = Common.Common.cis_Doctor.roomNumber;
                    objArg.ParamList["STATUS"] = Common.Common.status;

                    if (Common.Common.cis_Doctor.doctorId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateDoctor(objArg);
                    }
                    else
                    {
                        Common.Common.flag = objBusinessFacade.insertDoctor(objArg);
                    }

                    if (Common.Common.flag == 1)
                    {
                        clear_data();
                        MessageBox.Show("Saved Sucessfully....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Fields are required....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (Common.Common.cis_department.departmentId > 0)
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Record exists already....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear_data();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Functions
        private void load_data()
        {
            cboGender.SelectedIndex = 0;
            cboDoctorType.SelectedIndex = 1;
            cboStatus.SelectedIndex = 1;
        }

        private void clear_data()
        {
            txtDoctorCode.Text = string.Empty;
            txtDoctorName.Text = string.Empty;
            txtQualification.Text = string.Empty;
            txtRoomNumber.Text = string.Empty;
            txtSpecialization.Text = string.Empty;
            Common.Common.cis_Doctor.doctorId = 0;
        }
        #endregion
    }
}
