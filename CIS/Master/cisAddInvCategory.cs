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
    public partial class cisAddInvCategory : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public cisAddInvCategory()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }

        public cisAddInvCategory(int id)
        {
            InitializeComponent();
            Common.Common.cis_investigation_info.invCategoryId = id;
            this.Text = "Edit Investigation Category";
        }
        #endregion

        #region Events
        private void cisAddInvCategory_Load(object sender, EventArgs e)
        {
            if (Common.Common.cis_investigation_info.invCategoryId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getInvCategoryRecord(Common.Common.cis_investigation_info.invCategoryId);
                txtInvCategory.Text = dtSource.Rows[0]["inv_category"].ToString();
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["status"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtInvCategory.Text.Trim())))
                {
                    Common.Common.cis_investigation_info.invCategory = txtInvCategory.Text.ToString();
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    objArg.ParamList["inv_category_id"] = Common.Common.cis_investigation_info.invCategoryId;
                    objArg.ParamList["inv_category"] = Common.Common.cis_investigation_info.invCategory;
                    objArg.ParamList["status"] = Common.Common.status;

                    if (Common.Common.cis_investigation_info.invCategoryId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateInvCategory(objArg);
                    }
                    else
                    {
                        Common.Common.flag = objBusinessFacade.insertInvCategory(objArg);
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

                if (Common.Common.cis_investigation_info.invCategoryId > 0)
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
        #endregion

        #region Functions
        private void load_data()
        {
            cboStatus.SelectedIndex = 1;
        }

        private void clear_data()
        {
            txtInvCategory.Text = string.Empty;
            cboStatus.SelectedIndex = 1;
            Common.Common.cis_investigation_info.invCategoryId = 0;
        }
        #endregion
    }
}
