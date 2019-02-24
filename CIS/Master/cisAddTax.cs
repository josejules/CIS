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
    public partial class cisAddTax : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public cisAddTax()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }

        public cisAddTax(int id)
        {
            InitializeComponent();
            Common.Common.cis_pharmacy_info.taxId = id;
            this.Text = "Edit Tax";
        }
        #endregion

        #region Events
        private void cisAddTax_Load(object sender, EventArgs e)
        {
            if (Common.Common.cis_pharmacy_info.taxId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getTaxRecord(Common.Common.cis_pharmacy_info.taxId);
                txtTaxRate.Text = dtSource.Rows[0]["tax_rate"].ToString();
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["status"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtTaxRate.Text.Trim())))
                {
                    Common.Common.cis_pharmacy_info.taxRate = Convert.ToDecimal(txtTaxRate.Text.ToString());
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    objArg.ParamList["tax_id"] = Common.Common.cis_pharmacy_info.taxId;
                    objArg.ParamList["tax_rate"] = Common.Common.cis_pharmacy_info.taxRate;
                    objArg.ParamList["status"] = Common.Common.status;

                    if (Common.Common.cis_pharmacy_info.taxId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateTax(objArg);
                    }
                    else
                    {
                        Common.Common.flag = objBusinessFacade.insertTax(objArg);
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

                if (Common.Common.cis_pharmacy_info.taxId > 0)
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

        private void txtTaxRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }
        #endregion

        #region Functions
        private void load_data()
        {
            cboStatus.SelectedIndex = 1;
        }

        private void clear_data()
        {
            txtTaxRate.Text = string.Empty;
            cboStatus.SelectedIndex = 1;
            Common.Common.cis_pharmacy_info.taxId = 0;
        }
        #endregion
    }
}
