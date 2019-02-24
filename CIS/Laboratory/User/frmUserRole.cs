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

namespace CIS.Laboratory.User
{
    public partial class frmUserRole : Form
    {
        #region Variables
        public int RoleId = 0;
        #endregion

        public frmUserRole()
        {
            InitializeComponent();
        }

        public frmUserRole(int roleId)
        {
            RoleId = roleId;
            InitializeComponent();
            DataTable dtCategory = new clsLabBusinessFacade().FetchUserRoleInfoById(RoleId);
            if (dtCategory.Rows.Count > 0)
                txtUserRole.Text = dtCategory.Rows[0]["User Role"].ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUserRole.Text))
            {
                ComArugments args = new ComArugments();
                args.ParamList[CIS.Common.User.UserRole.RoleId] = RoleId;
                args.ParamList[CIS.Common.User.UserRole.RoleName] = txtUserRole.Text;

                int rowsAffected = new clsLabBusinessFacade().AddUserRole(args);

                if (rowsAffected > 0)
                {
                    if (RoleId == 0)
                        MessageBox.Show("User Role is saved", "User Role", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("User Role is updated", "User Role", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserRole.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Please enter User Role", "User Role", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserRole_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserRole.Text))
            {
                errorProvider1.SetError(txtUserRole, "Please enter User Role");
                txtUserRole.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

    }
}
