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
    public partial class cisAddInvList : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public cisAddInvList()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }

        public cisAddInvList(int id)
        {
            InitializeComponent();
            Common.Common.cis_investigation_info.investigationId = id;
            this.Text = "Edit Investigation";
        }
        #endregion

        #region Events
        private void cisAddInvList_Load(object sender, EventArgs e)
        {
            loadInvCategory();
            loadInvDepartment();
            loadShareType();
            if (Common.Common.cis_investigation_info.investigationId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getInvestigationRecord(Common.Common.cis_investigation_info.investigationId);
                txtInvestigationCode.Text = dtSource.Rows[0]["investigation_code"].ToString();
                txtInvestigationName.Text = dtSource.Rows[0]["investigation_name"].ToString();
                cboInvCategory.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["investigation_category_id"].ToString());
                cboInvDepartmentId.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["department_id"].ToString());
                txtInvUnitPrice.Text = dtSource.Rows[0]["unit_price"].ToString();
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["status"].ToString());
                cboShareType.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["share_type"].ToString());
                cboAmtType.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["amt_type"].ToString());
                txtDiscountPer.Text = dtSource.Rows[0]["share_per"].ToString();
                lblDiscount.Text = dtSource.Rows[0]["share_amt"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear_data();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtInvUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Alt | Keys.S:
                    SaveData();
                    return true;
                case Keys.Alt | Keys.N:
                    clear_data();
                    return true;
                case Keys.Alt | Keys.C:
                    this.Close();
                    return true;
                case Keys.Control | Keys.Alt | Keys.S:
                    // do something...
                    return true;
                case Keys.F2:
                    //do something
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        #region Functions
        private void loadInvCategory()
        {
            try
            {
                dtSource = objBusinessFacade.loadInvCategory();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboInvCategory.ValueMember = "inv_category_id";
                    cboInvCategory.DisplayMember = "inv_category";
                    cboInvCategory.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadInvDepartment()
        {
            try
            {
                dtSource = objBusinessFacade.loadInvestigationDepartment();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboInvDepartmentId.ValueMember = "DEPARTMENT_ID";
                    cboInvDepartmentId.DisplayMember = "DEPARTMENT_NAME";
                    cboInvDepartmentId.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadShareType()
        {
            try
            {
                dtSource = objBusinessFacade.loadShareType();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboShareType.ValueMember = "master_id";
                    cboShareType.DisplayMember = "master_value";
                    cboShareType.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void load_data()
        {
            cboStatus.SelectedIndex = 1;
            //cboShareType.SelectedIndex = 0;
            cboAmtType.SelectedIndex = 1;
        }

        private void SaveData()
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtInvestigationCode.Text.Trim())) && !(string.IsNullOrEmpty(txtInvestigationName.Text.Trim())))
                {
                    Common.Common.cis_investigation_info.investigationCode = txtInvestigationCode.Text.ToString().Trim();
                    Common.Common.cis_investigation_info.investigationName = txtInvestigationName.Text.ToString().Trim();
                    Common.Common.cis_investigation_info.invCategoryId = Convert.ToInt32(cboInvCategory.SelectedValue.ToString());
                    Common.Common.cis_investigation_info.investigationDeptId = Convert.ToInt32(cboInvDepartmentId.SelectedValue.ToString());
                    Common.Common.cis_investigation_info.investigationUnitPrice = Convert.ToDecimal(txtInvUnitPrice.Text.ToString());
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());
                    Common.Common.cis_investigation_info.ShareType = Convert.ToInt32(cboShareType.SelectedValue.ToString());
                    Common.Common.cis_investigation_info.AmtType = cboAmtType.SelectedIndex;
                    Common.Common.cis_investigation_info.sharePerc = objBusinessFacade.NonBlankValueOfDecimal(txtDiscountPer.Text.ToString());
                    Common.Common.cis_investigation_info.shareAmt = objBusinessFacade.NonBlankValueOfDecimal(lblDiscount.Text.ToString());

                    objArg.ParamList["investigation_id"] = Common.Common.cis_investigation_info.investigationId;
                    objArg.ParamList["investigation_code"] = Common.Common.cis_investigation_info.investigationCode;
                    objArg.ParamList["investigation_name"] = Common.Common.cis_investigation_info.investigationName;
                    objArg.ParamList["investigation_category_id"] = Common.Common.cis_investigation_info.invCategoryId;
                    objArg.ParamList["department_id"] = Common.Common.cis_investigation_info.investigationDeptId;
                    objArg.ParamList["unit_price"] = Common.Common.cis_investigation_info.investigationUnitPrice;
                    objArg.ParamList["status"] = Common.Common.status;
                    objArg.ParamList["share_type"] = Common.Common.cis_investigation_info.ShareType;
                    objArg.ParamList["amt_type"] = Common.Common.cis_investigation_info.AmtType;
                    objArg.ParamList["share_per"] = Common.Common.cis_investigation_info.sharePerc;
                    objArg.ParamList["share_amt"] = Common.Common.cis_investigation_info.shareAmt;

                    if (Common.Common.cis_investigation_info.investigationId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateInvestigation(objArg);
                    }
                    else
                    {
                        //clear_data();
                        Common.Common.flag = objBusinessFacade.insertInvestigation(objArg);
                    }

                    if (Common.Common.flag == 1)
                    {
                        clear_data();
                        MessageBox.Show("Saved Successfully....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Fields are required....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (Common.Common.cis_investigation_info.investigationId > 0)
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

        private void clear_data()
        {
            txtInvestigationCode.Text = string.Empty;
            txtInvestigationName.Text = string.Empty;
            txtInvUnitPrice.Text = "0.00";
            txtDiscountPer.Text = "0.00";
            lblDiscount.Text = "0.00";
            cboStatus.SelectedIndex = 1;
            Common.Common.cis_investigation_info.investigationId = 0;
        }

        private void calculateDiscount()
        {
            Common.Common.cis_investigation_info.investigationUnitPrice = objBusinessFacade.NonBlankValueOfDecimal(txtInvUnitPrice.Text.ToString());
            Common.Common.cis_investigation_info.AmtType = cboAmtType.SelectedIndex;
            Common.Common.cis_investigation_info.sharePerc = objBusinessFacade.NonBlankValueOfDecimal(txtDiscountPer.Text.ToString());


            if (Common.Common.cis_investigation_info.AmtType == 1 && Common.Common.cis_investigation_info.sharePerc > 0)//Perc
            {
                if (Common.Common.cis_investigation_info.sharePerc <= 100)
                {
                    Common.Common.cis_investigation_info.shareAmt = Math.Round(Common.Common.cis_investigation_info.investigationUnitPrice * (Common.Common.cis_investigation_info.sharePerc / 100), 2);
                    lblDiscount.Text = Convert.ToString(Common.Common.cis_investigation_info.shareAmt);
                }
                else
                {
                    MessageBox.Show("Discount Amount can't be greater than Unit Price....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiscountPer.Text = "0.00";
                    lblDiscount.Text = "0.00";
                }
            }
            else //Amt
            {
                if (Common.Common.cis_investigation_info.sharePerc <= Common.Common.cis_investigation_info.investigationUnitPrice)
                {
                    Common.Common.cis_investigation_info.shareAmt = Math.Round(objBusinessFacade.NonBlankValueOfDecimal(txtDiscountPer.Text.ToString()), 2);
                    lblDiscount.Text = Convert.ToString(Common.Common.cis_investigation_info.shareAmt);
                }
                else
                {
                    MessageBox.Show("Discount Amount can't be greater than Unit Price....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiscountPer.Text = "0.00";
                    lblDiscount.Text = "0.00";
                }
            }
        }

        #endregion

        private void txtDiscountPer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                cboStatus.Select();
                calculateDiscount();
            } e.IsInputKey = true;
        }

        private void txtDiscountPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboStatus.Select();
                e.Handled = true;
                calculateDiscount();
            }
        }

        private void txtDiscountPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }
    }
}
