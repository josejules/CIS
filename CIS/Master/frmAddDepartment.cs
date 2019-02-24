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

namespace CIS
{
    public partial class frmAddDepartment : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public frmAddDepartment()
        {
            InitializeComponent();
            load_data();
        }

        public frmAddDepartment(int id)
        {
            InitializeComponent();
            Common.Common.cis_department.departmentId = id;
            this.Text = "Edit Department";
        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtDepartmentCode.Text.Trim())) && !(string.IsNullOrEmpty(txtDepartmentName.Text.Trim())))
                {
                    Common.Common.cis_department.departmentCode = txtDepartmentCode.Text.ToString().Trim();
                    Common.Common.cis_department.departmentName = txtDepartmentName.Text.ToString().Trim();
                    Common.Common.cis_department.departmentType = Convert.ToInt32(cboDepartmentType.SelectedIndex.ToString()) + 1;
                    Common.Common.cis_department.departmentCategoryId = Convert.ToInt32(cboDepartmentCategory.SelectedIndex.ToString()) + 1;
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    objArg.ParamList["departmentId"] = Common.Common.cis_department.departmentId;
                    objArg.ParamList["department_code"] = Common.Common.cis_department.departmentCode;
                    objArg.ParamList["department_name"] = Common.Common.cis_department.departmentName;
                    objArg.ParamList["department_type"] = Common.Common.cis_department.departmentType;
                    objArg.ParamList["DEPARTMENT_CATEGORY_ID"] = Common.Common.cis_department.departmentCategoryId;
                    objArg.ParamList["STATUS"] = Common.Common.status;

                    if (Common.Common.cis_department.departmentId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateDepartment(objArg);
                    }
                    else
                    {
                        Common.Common.flag = objBusinessFacade.insertDepartment(objArg);
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
            Common.Common.cis_department.departmentId = 0;
        }

        private void frmAddDepartment_Load(object sender, EventArgs e)
        {
            if (Common.Common.cis_department.departmentId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getDepartmentRecord(Common.Common.cis_department.departmentId);
                txtDepartmentCode.Text = dtSource.Rows[0]["department_code"].ToString();
                txtDepartmentName.Text = dtSource.Rows[0]["department_name"].ToString();
                cboDepartmentType.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["department_type"].ToString()) - 1;
                cboDepartmentCategory.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["DEPARTMENT_CATEGORY_ID"].ToString()) - 1;
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["STATUS"].ToString());
            }
        }
        #endregion

        #region Functions
        private void load_data()
        {
            cboDepartmentType.SelectedIndex = 1;
            cboDepartmentCategory.SelectedIndex = 0;
            cboStatus.SelectedIndex = 1;
        }

        private void clear_data()
        {
            txtDepartmentCode.Text = string.Empty;
            txtDepartmentName.Text = string.Empty;
            cboDepartmentType.SelectedIndex = 1;
            cboDepartmentCategory.SelectedIndex = 0;
            cboStatus.SelectedIndex = 1;
            Common.Common.cis_department.departmentId = 0;
        }
        #endregion
    }
}