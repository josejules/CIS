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
    public partial class frmAddCorporate : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public frmAddCorporate()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }

        public frmAddCorporate(int id)
        {
            InitializeComponent();
            Common.Common.cis_Corporate.corporateId = id;
            this.Text = "Edit Corporate";
        }
        #endregion

        #region Events
        private void frmAddCorporate_Load(object sender, EventArgs e)
        {
            if (Common.Common.cis_Corporate.corporateId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getCorporateRecord(Common.Common.cis_Corporate.corporateId);
                txtCorporateName.Text = dtSource.Rows[0]["corporate_name"].ToString();
                mtxtAddress.Text=dtSource.Rows[0]["address"].ToString();
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["STATUS"].ToString());

                if (Convert.ToInt32(dtSource.Rows[0]["is_charges_applicable"].ToString()) == 1)
                {
                    cboxIsFeeAppliable.Checked = true;
                }
                else
                {
                    cboxIsFeeAppliable.Checked = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtCorporateName.Text.Trim())))
                {
                    Common.Common.cis_Corporate.corporateName = txtCorporateName.Text.ToString().Trim();
                    Common.Common.cis_Corporate.address = mtxtAddress.Text.ToString().Trim();
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    if (cboxIsFeeAppliable.Checked == true)
                    {
                        Common.Common.cis_Corporate.isFeeApplicable = 1;
                    }
                    else
                    {
                        Common.Common.cis_Corporate.isFeeApplicable = 0;
                    }

                    objArg.ParamList["corporate_id"] = Common.Common.cis_Corporate.corporateId;
                    objArg.ParamList["corporate_name"] = Common.Common.cis_Corporate.corporateName;
                    objArg.ParamList["address"] = Common.Common.cis_Corporate.address;
                    objArg.ParamList["STATUS"] = Common.Common.status;
                    objArg.ParamList["is_fee_applicable"] = Common.Common.cis_Corporate.isFeeApplicable;

                    if (Common.Common.cis_Corporate.corporateId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateCorporate(objArg);
                    }
                    else
                    {
                        Common.Common.flag = objBusinessFacade.insertCorporate(objArg);
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

                if (Common.Common.cis_Corporate.corporateId > 0)
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
            txtCorporateName.Text = string.Empty;
            mtxtAddress.Text = string.Empty;
            cboStatus.SelectedIndex = 1;
            Common.Common.cis_Corporate.corporateId = 0;
        }
        #endregion
    }
}
