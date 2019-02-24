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
    public partial class cisAddReferral : Form
    {

        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion


        #region Constructor
        public cisAddReferral()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }

        public cisAddReferral(int id)
        {
            InitializeComponent();
            Common.Common.module_visit_info.referralId = id;
            this.Text = "Edit Referral";
        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtReferralName.Text.Trim())))
                {
                    Common.Common.module_visit_info.referralName= txtReferralName.Text.ToString().Trim();
                    Common.Common.module_visit_info.referralContactNo = txtContactNo.Text.ToString().Trim();
                    Common.Common.module_visit_info.referralContactAddress = mtxtContactAddress.Text.ToString().Trim();
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    objArg.ParamList["cis_referral_id"] = Common.Common.module_visit_info.referralId;
                    objArg.ParamList["referral_name"] = Common.Common.module_visit_info.referralName;
                    objArg.ParamList["contact_no"] = Common.Common.module_visit_info.referralContactNo;
                    objArg.ParamList["contact_address"] = Common.Common.module_visit_info.referralContactAddress;
                    objArg.ParamList["status"] = Common.Common.status;

                    if (Common.Common.module_visit_info.referralId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateReferral(objArg);
                    }
                    else
                    {
                        Common.Common.flag = objBusinessFacade.insertReferral(objArg);
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

                if (Common.Common.module_visit_info.referralId > 0)
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

        private void cisAddReferral_Load(object sender, EventArgs e)
        {
            if (Common.Common.module_visit_info.referralId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getReferralRecord(Common.Common.module_visit_info.referralId);
                txtReferralName.Text = dtSource.Rows[0]["referral_name"].ToString();
                txtContactNo.Text = dtSource.Rows[0]["contact_no"].ToString();
                mtxtContactAddress.Text = dtSource.Rows[0]["contact_address"].ToString();
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["status"].ToString());
            }
        }
        #endregion

        #region Functions
        private void load_data()
        {
            cboStatus.SelectedIndex = 1;
        }

        private void clear_data()
        {
            txtReferralName.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            mtxtContactAddress.Text = string.Empty;
            cboStatus.SelectedIndex = 1;
            Common.Common.module_visit_info.referralId = 0;
        }
        #endregion
    }
}
