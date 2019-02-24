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
    public partial class cisAddItem : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public cisAddItem()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }
        public cisAddItem(int id)
        {
            InitializeComponent();
            Common.Common.cis_pharmacy_info.phaItemId = id;
            this.Text = "Edit Item";
        }
        #endregion

        #region Events
        private void cisAddItem_Load(object sender, EventArgs e)
        {
            loadItemType();
            loadTaxPerc();
            if (Common.Common.cis_pharmacy_info.phaItemId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getItemRecord(Common.Common.cis_pharmacy_info.phaItemId);
                txtItemName.Text = dtSource.Rows[0]["item_name"].ToString();
                txtHSNCode.Text = dtSource.Rows[0]["hsn_code"].ToString();
                txtPackY.Text = dtSource.Rows[0]["pack_y"].ToString();
                cboItemType.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["item_type_id"].ToString());
                cboTaxPerc.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["tax_id"].ToString());
                cboStatus.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["status"].ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save_data();
        }

        private void save_data()
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtItemName.Text.Trim())))
                {
                    Common.Common.cis_pharmacy_info.phaItemName = txtItemName.Text.ToString().Trim();
                    Common.Common.cis_pharmacy_info.HSNCode = txtHSNCode.Text.ToString().Trim();
                    Common.Common.cis_pharmacy_info.packY = Convert.ToInt32((string.IsNullOrWhiteSpace(txtPackY.Text.ToString()) ? "0" : txtPackY.Text));
                    Common.Common.cis_pharmacy_info.phaItemTypeId = Convert.ToInt32(cboItemType.SelectedValue.ToString());
                    Common.Common.cis_pharmacy_info.salesTaxPerc = Convert.ToInt32(cboTaxPerc.SelectedValue.ToString());
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    objArg.ParamList["item_id"] = Common.Common.cis_pharmacy_info.phaItemId;
                    objArg.ParamList["hsn_code"] = Common.Common.cis_pharmacy_info.HSNCode;
                    objArg.ParamList["pack_y"] = Common.Common.cis_pharmacy_info.packY;
                    objArg.ParamList["item_name"] = Common.Common.cis_pharmacy_info.phaItemName;
                    objArg.ParamList["item_type_id"] = Common.Common.cis_pharmacy_info.phaItemTypeId;
                    objArg.ParamList["tax_id"] = Common.Common.cis_pharmacy_info.salesTaxPerc;
                    objArg.ParamList["status"] = Common.Common.status;

                    if (Common.Common.cis_pharmacy_info.phaItemId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateItem(objArg);
                    }
                    else
                    {
                        clear_data();
                        Common.Common.flag = objBusinessFacade.insertItem(objArg);
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

                if (Common.Common.cis_pharmacy_info.phaItemId > 0)
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
        private void loadItemType()
        {
            try
            {
                dtSource = objBusinessFacade.loadItemType();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboItemType.ValueMember = "item_type_id";
                    cboItemType.DisplayMember = "item_type";
                    cboItemType.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadTaxPerc()
        {
            try
            {
                dtSource = objBusinessFacade.loadTaxPerc();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    cboTaxPerc.ValueMember = "tax_id";
                    cboTaxPerc.DisplayMember = "tax_rate";
                    cboTaxPerc.DataSource = dtSource;
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
        }

        private void clear_data()
        {
            txtItemName.Text = string.Empty;
            //cboItemType.SelectedIndex = 0;
            cboStatus.SelectedIndex = 1;
            Common.Common.cis_pharmacy_info.phaItemId = 0;
        }
        #endregion

        private void txtPackY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Alt | Keys.S:
                    save_data();
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
    }
}
