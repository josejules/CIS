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
    public partial class frmAddAddressInfo : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public frmAddAddressInfo()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }

        public frmAddAddressInfo(int id)
        {
            InitializeComponent();
            Common.Common.cis_Address_Info.addressId = id;
            this.Text = "Edit Address Info";
        }
        #endregion

        #region Events
        private void frmAddState_Load(object sender, EventArgs e)
        {
            if (Common.Common.cis_Address_Info.addressId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getAddressInfoRecord(Common.Common.cis_Address_Info.addressId);
                txtPlace.Text = dtSource.Rows[0]["place"].ToString();
                txtDistrict.Text = dtSource.Rows[0]["district"].ToString();
                txtState.Text = dtSource.Rows[0]["state"].ToString();
                txtPostalCode.Text = dtSource.Rows[0]["postal_code"].ToString();
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["STATUS"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtPlace.Text.Trim())) && !(string.IsNullOrEmpty(txtDistrict.Text.Trim())))
                {
                    Common.Common.cis_Address_Info.place = txtPlace.Text.ToString().Trim();
                    Common.Common.cis_Address_Info.district = txtDistrict.Text.ToString().Trim();
                    Common.Common.cis_Address_Info.state = txtState.Text.ToString().Trim();
                    Common.Common.cis_Address_Info.postalCode = txtPostalCode.Text.ToString().Trim();
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    objArg.ParamList["address_id"] = Common.Common.cis_Address_Info.addressId;
                    objArg.ParamList["place"] = Common.Common.cis_Address_Info.place;
                    objArg.ParamList["district"] = Common.Common.cis_Address_Info.district;
                    objArg.ParamList["state"] = Common.Common.cis_Address_Info.state;
                    objArg.ParamList["postal_code"] = Common.Common.cis_Address_Info.postalCode;
                    objArg.ParamList["STATUS"] = Common.Common.status;

                    if (Common.Common.cis_Address_Info.addressId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateAddressInfo(objArg);
                    }
                    else
                    {
                        Common.Common.flag = objBusinessFacade.insertAddressInfo(objArg);
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

                if (Common.Common.cis_Address_Info.addressId > 0)
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Record already exists....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
        }
        #endregion

        #region Functions
        private void load_data()
        {
            cboStatus.SelectedIndex = 1;
        }

        private void clear_data()
        {
            txtPlace.Text = string.Empty;
            txtDistrict.Text = string.Empty;
            txtState.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            cboStatus.SelectedIndex = 1;
            Common.Common.cis_Address_Info.addressId = 0;
        }
        #endregion
    }
}
