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

namespace CIS.Master
{
    public partial class frmAddDefineRegFee : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public frmAddDefineRegFee()
        {
            InitializeComponent();
            cboDoctor.SelectedIndex = 0;
            cboValidity.SelectedIndex = 1;
        }

        public frmAddDefineRegFee(int id)
        {
            InitializeComponent();
            Common.Common.cis_Define_Reg_Fee.define_reg_fee_id = id;
            this.Text = "Edit Define Reg Fee";
        }
        #endregion

        #region Events
        private void frmDefineRegFee_Load(object sender, EventArgs e)
        {
            loadDepartment();
            loadDoctor();
            if (Common.Common.cis_Define_Reg_Fee.define_reg_fee_id > 0) //For Editing
            {
                dtSource = objBusinessFacade.getDefineRegFeeRecord(Common.Common.cis_Define_Reg_Fee.define_reg_fee_id);
                cboDepartment.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["department_id"].ToString());
                cboDoctor.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["doctor_id"].ToString());
                txtNewRegFee.Text = dtSource.Rows[0]["new_reg_fee"].ToString();
                txtValidity.Text = dtSource.Rows[0]["validity"].ToString();
                cboValidity.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["validity_period"].ToString());
                txtRevisitRegFee.Text = dtSource.Rows[0]["revisit_reg_fee"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Common.Common.cis_department.departmentId = Convert.ToInt32(cboDepartment.SelectedValue.ToString());
            Common.Common.cis_Doctor.doctorId = Convert.ToInt32(cboDoctor.SelectedValue.ToString());
            Common.Common.cis_Define_Reg_Fee.newRegFee = objBusinessFacade.NonBlankValueOfDecimal(txtNewRegFee.Text.ToString());
            Common.Common.cis_Define_Reg_Fee.validity = objBusinessFacade.NonBlankValueOfInt(txtValidity.Text.ToString());
            Common.Common.cis_Define_Reg_Fee.validityPeriod = Convert.ToInt32(cboValidity.SelectedIndex.ToString());
            Common.Common.cis_Define_Reg_Fee.revisitRegFee = objBusinessFacade.NonBlankValueOfDecimal(txtRevisitRegFee.Text.ToString());

            objArg.ParamList["define_reg_fee_id"] = Common.Common.cis_Define_Reg_Fee.define_reg_fee_id;
            objArg.ParamList["department_id"] = Common.Common.cis_department.departmentId;
            objArg.ParamList["doctor_id"] = Common.Common.cis_Doctor.doctorId;
            objArg.ParamList["new_reg_fee"] = Common.Common.cis_Define_Reg_Fee.newRegFee;
            objArg.ParamList["validity"] = Common.Common.cis_Define_Reg_Fee.validity;
            objArg.ParamList["validity_period"] = Common.Common.cis_Define_Reg_Fee.validityPeriod;
            objArg.ParamList["revisit_reg_fee"] = Common.Common.cis_Define_Reg_Fee.revisitRegFee;

            try
            {
                dtSource = objBusinessFacade.checkDefineRegFeeRecord(objArg);

                if (Common.Common.cis_Define_Reg_Fee.define_reg_fee_id > 0)
                {
                    Common.Common.flag = objBusinessFacade.updateDefineRegFee(objArg);
                }
                else
                {
                    if (dtSource == null || dtSource.Rows.Count <= 0)
                    {
                        Common.Common.flag = objBusinessFacade.insertDefineRegFee(objArg);
                    }

                    else
                    {
                        MessageBox.Show("Record is available already....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (Common.Common.flag == 1)
                {
                    MessageBox.Show("Saved Sucessfully....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_data();
                }

            }
            catch (Exception)
            {

                throw;
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

        private void txtNewRegFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtValidity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
        }

        private void txtRevisitRegFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        #endregion

        #region Functions
        private void loadDoctor()
        {
            try
            {
                dtSource = objBusinessFacade.loadDoctor();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboDoctor.DataSource = dtSource;
                    cboDoctor.DisplayMember = "DOCTOR_NAME";
                    cboDoctor.ValueMember = "DOCTOR_ID";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadDepartment()
        {
            try
            {
                dtSource = objBusinessFacade.loadDepartment();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboDepartment.DataSource = dtSource;
                    cboDepartment.DisplayMember = "DEPARTMENT_NAME";
                    cboDepartment.ValueMember = "DEPARTMENT_ID";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void clear_data()
        {
            cboDepartment.SelectedIndex = 0;
            cboDoctor.SelectedIndex = 0;
            cboValidity.SelectedIndex = 1;
            txtNewRegFee.Text = "0.00";
            txtValidity.Text = "0";
            txtRevisitRegFee.Text = "0.00";
            Common.Common.cis_Define_Reg_Fee.define_reg_fee_id = 0;
        }
        #endregion

        private void cboValidity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboValidity.SelectedIndex == 0)//Every Visit
            {
                txtValidity.Text = "0";
                txtValidity.Enabled = false;
            }
            else
            {
                //txtValidity.Text = "0";
                txtValidity.Enabled = true;
            }
        }
    }
}
