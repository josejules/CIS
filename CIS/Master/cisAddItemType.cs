﻿using System;
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
    public partial class cisAddItemType : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public cisAddItemType()
        {
            InitializeComponent();
            load_data();
            clear_data();
        }

        public cisAddItemType(int id)
        {
            InitializeComponent();
            Common.Common.cis_pharmacy_info.phaItemTypeId = id;
            this.Text = "Edit Item Type";
        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(string.IsNullOrEmpty(txtItemType.Text.Trim())))
                {
                    Common.Common.cis_pharmacy_info.phaItemType = txtItemType.Text.ToString().Trim();
                    Common.Common.status = Convert.ToInt32(cboStatus.SelectedIndex.ToString());

                    objArg.ParamList["item_type_id"] = Common.Common.cis_pharmacy_info.phaItemTypeId;
                    objArg.ParamList["item_type"] = Common.Common.cis_pharmacy_info.phaItemType;
                    objArg.ParamList["status"] = Common.Common.status;

                    if (Common.Common.cis_pharmacy_info.phaItemTypeId > 0)
                    {
                        Common.Common.flag = objBusinessFacade.updateItemType(objArg);
                    }
                    else
                    {
                        Common.Common.flag = objBusinessFacade.insertItemType(objArg);
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

                if (Common.Common.cis_pharmacy_info.phaItemTypeId > 0)
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

        private void cisAddItemType_Load(object sender, EventArgs e)
        {
            if (Common.Common.cis_pharmacy_info.phaItemTypeId > 0) //For Editing
            {
                dtSource = objBusinessFacade.getItemTypeRecord(Common.Common.cis_pharmacy_info.phaItemTypeId);
                txtItemType.Text = dtSource.Rows[0]["item_type"].ToString();
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
            txtItemType.Text = string.Empty;
            cboStatus.SelectedIndex = 1;
            Common.Common.cis_pharmacy_info.phaItemTypeId = 0;
        }
        #endregion
    }
}
