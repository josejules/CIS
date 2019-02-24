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
using System.Security.Cryptography;

namespace CIS.Laboratory.User
{
    public partial class frmUser : Form
    {
        public int UserId = 0;
        public frmUser()
        {
            InitializeComponent();
            LoadUserRoles();
            LoadUserTypes();
        }
        public frmUser(int userId)
        {
            UserId = userId;
            InitializeComponent();
            LoadUserRoles();
            LoadUserTypes();
            DataTable dtUser = new clsLabBusinessFacade().FetchUserInfoById(UserId);
            if (dtUser.Rows.Count > 0)
            {
                txtFullName.Text = dtUser.Rows[0]["full_name"].ToString();
                txtUsername.Text = dtUser.Rows[0]["user_name"].ToString();
                txtPassword.Text = new Encryption().Decrypt(dtUser.Rows[0]["password"].ToString(),true);
                cboUserType.SelectedValue = Convert.ToInt32(dtUser.Rows[0]["user_type"].ToString());
                cboUserRole.SelectedValue = Convert.ToInt32(dtUser.Rows[0]["user_role_id"].ToString());
            }
        }

        private void LoadUserRoles()
        {
            cboUserRole.DataSource = new clsLabBusinessFacade().LoadUserRole();
            cboUserRole.ValueMember = "role_id";
            cboUserRole.DisplayMember = "User Role";
        }

        private void LoadUserTypes()
        {
            cboUserType.DataSource = new clsLabBusinessFacade().LoadUserTypes();
            cboUserType.ValueMember = "user_type_id";
            cboUserType.DisplayMember = "user_type";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateControls())
                {
                    ComArugments args = new ComArugments();
                    args.ParamList[CIS.Common.User.UserId] = UserId;
                    args.ParamList[CIS.Common.User.FullName] = txtFullName.Text;
                    args.ParamList[CIS.Common.User.Username] = txtUsername.Text;
                    args.ParamList[CIS.Common.User.Password] = new Encryption().Encrypt(txtPassword.Text,true);
                    args.ParamList[CIS.Common.User.UserRole.RoleId] = cboUserRole.SelectedValue;
                    args.ParamList[CIS.Common.User.UserType] = cboUserType.SelectedValue;

                    int rowsAffected = new clsLabBusinessFacade().AddEditUser(args);
                    if (rowsAffected > 0)
                    {
                        if (UserId == 0)
                            MessageBox.Show("User is saved", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("User is updated", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearControls();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }

        private void ClearControls()
        {
            txtFullName.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            cboUserType.SelectedIndex = 0;
            cboUserRole.SelectedIndex = 0;
        }

        private bool ValidateControls()
        {
            bool flag = false;
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter Full Name", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName.Focus();
                return flag;
            }
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter Username", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
                return flag;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter Password", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return flag;
            }
            if (cboUserRole.Items.Count == 0)
            {
                MessageBox.Show("Please Add User Roles in Role Activity!", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboUserRole.Focus();
                return flag;
            }
            if (cboUserRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please select User Role!", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboUserRole.Focus();
                return flag;
            }
            return true;
        }

        /// <summary>
        /// It encrypts the password into MD5 Algorithm(One way Encryption)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFullName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFullName.Text))
            {
                errorProvider1.SetError(txtFullName, "Please enter Full Name");
                txtFullName.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }

        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Please enter Username");
                txtUsername.Focus();
            }
            else
                errorProvider1.Clear();
        }

        private void txtPassword_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Please enter Password");
                txtPassword.Focus();
            }
            else
                errorProvider1.Clear();
        }
    }
}
