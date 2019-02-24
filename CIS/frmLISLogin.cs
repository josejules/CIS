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
using System.IO;


namespace CIS
{
    public partial class frmLISLogin : Form
    {
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();

        string userName = null;
        string passWord = null;
        int userId;

        public frmLISLogin()
        {
            try
            {
                InitializeComponent();
                txtUserName.Focus();
                Common.Common.ExceptionHandler.errorPath = Application.StartupPath;
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text != String.Empty && txtPassword.Text != String.Empty) //Verifying for Empty fields
                {
                    dtSource = objBusinessFacade.getUserDetails(txtUserName.Text);
                    if (dtSource != null && dtSource.Rows.Count > 0)
                    {
                        userId = Convert.ToInt32(dtSource.Rows[0]["USER_ID"].ToString());
                        Common.Common.userType = Convert.ToInt32(dtSource.Rows[0]["USER_TYPE"].ToString());
                        userName = dtSource.Rows[0]["USER_NAME"].ToString();
                        passWord = new Encryption().Decrypt(dtSource.Rows[0]["PASSWORD"].ToString(), true);
                        Common.Common.userRoleId = Convert.ToInt32(dtSource.Rows[0]["USER_ROLE_ID"].ToString());

                        if (userName == txtUserName.Text && passWord == txtPassword.Text)
                        {
                            Common.Common.sLoggedUser = userName;
                            Common.Common.userId = userId;
                            //MessageBox.Show("Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            HomePageCIS objShow = new HomePageCIS(Common.Common.sLoggedUser, Common.Common.userId, Common.Common.userRoleId);
                            objShow.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Password Try again !", "Login Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password Try again !", "Login Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
