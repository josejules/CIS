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
    public partial class cisAddVendor : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public cisAddVendor()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }

        public cisAddVendor(int id)
        {
            InitializeComponent();
            Common.Common.cis_pharmacy_info.vendorId = id;
            this.Text = "Edit Vendor";
        }
        #endregion

        #region Events
        private void cisAddVendor_Load(object sender, EventArgs e)
        {
            if (Common.Common.cis_pharmacy_info.vendorId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getVendorRecord(Common.Common.cis_pharmacy_info.vendorId);
                txtVendorName.Text = dtSource.Rows[0]["vendor_name"].ToString();
                txtTinNo.Text = dtSource.Rows[0]["tin_number"].ToString();
                mtxtVenorContactInfo.Text = dtSource.Rows[0]["contact_info"].ToString();
                mtxtVendorContactAddress.Text = dtSource.Rows[0]["contact_address"].ToString();
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["status"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtVendorName.Text.Trim())))
                {
                    Common.Common.cis_pharmacy_info.vendorName = txtVendorName.Text.ToString().Trim();
                    Common.Common.cis_pharmacy_info.TINNo = txtTinNo.Text.ToString().Trim();
                    Common.Common.cis_pharmacy_info.vendorContactInfo = mtxtVenorContactInfo.Text.ToString().Trim();
                    Common.Common.cis_pharmacy_info.vendorContactAddress = mtxtVendorContactAddress.Text.ToString().Trim();
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    objArg.ParamList["vendor_id"] = Common.Common.cis_pharmacy_info.vendorId;
                    objArg.ParamList["vendor_name"] = Common.Common.cis_pharmacy_info.vendorName;
                    objArg.ParamList["tin_number"] = Common.Common.cis_pharmacy_info.TINNo;
                    objArg.ParamList["contact_info"] = Common.Common.cis_pharmacy_info.vendorContactInfo;
                    objArg.ParamList["contact_address"] = Common.Common.cis_pharmacy_info.vendorContactAddress;
                    objArg.ParamList["status"] = Common.Common.status;

                    if (Common.Common.cis_pharmacy_info.vendorId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateVendor(objArg);
                    }
                    else
                    {
                        Common.Common.flag = objBusinessFacade.insertVendor(objArg);
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

                if (Common.Common.cis_pharmacy_info.vendorId > 0)
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
            txtVendorName.Text = string.Empty;
            txtTinNo.Text = string.Empty;
            mtxtVenorContactInfo.Text = string.Empty;
            mtxtVendorContactAddress.Text = string.Empty;
            cboStatus.SelectedIndex = 1;
            Common.Common.cis_pharmacy_info.vendorId = 0;
        }
        #endregion
    }
}
