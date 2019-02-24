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

namespace CIS.Master
{
    public partial class cis_account_head : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cis_account_head()
        {
            InitializeComponent();
        }

        private void cis_account_head_Load(object sender, EventArgs e)
        {
            cboBillType.SelectedIndex = 0;
            cboFilter.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            cboAccountGroup.Enabled = true;
            loadAccountType();
            viewAccountHead();

        }

        private void viewAccountHead()
        {
            try
            {
                dtSource = objBusinessFacade.getAccountHeadDetails();

                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    dgvAccountHead.DataSource = objBusinessFacade.AssignRowNo(dtSource);

                    //cboAccountGroup.SelectedIndex = 0;
                    dgvAccountHead.Columns[1].Visible = false;
                    dgvAccountHead.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAccountHead.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAccountHead.Columns[0].Width = 50;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void loadAccountType()
        {
            try
            {
                dtSource = objBusinessFacade.getAccountGroup();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = 0;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboAccountGroup.ValueMember = "id_cis_account_group";
                    cboAccountGroup.DisplayMember = "account_group_name";
                    cboAccountGroup.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void cBoxIsAccountGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxIsAccountGroup.Checked == true)
            {
                cboAccountGroup.Enabled = false;
                txtUnitPrice.Enabled = false;
                if (cboAccountGroup.Items.Count > 0)
                {
                    cboAccountGroup.SelectedIndex = 0;
                }
            }
            else
            {
                cboAccountGroup.Enabled = true;
                txtUnitPrice.Enabled = true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void clearData()
        {
            cboBillType.SelectedIndex = 0;
            cboFilter.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            cboAccountGroup.Enabled = true;
            txtAcountName.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
            lblAccountHeadId.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if ((!(string.IsNullOrEmpty(txtAcountName.Text)) && (objBusinessFacade.NonBlankValueOfDecimal(txtUnitPrice.Text.ToString()) != 0) && cBoxIsAccountGroup.Checked == false) || (cBoxIsAccountGroup.Checked == true && !(string.IsNullOrEmpty(txtAcountName.Text))))
            {
                if ((cBoxIsAccountGroup.Checked == false && !string.IsNullOrEmpty(cboAccountGroup.Text) || (cBoxIsAccountGroup.Checked == true)))
                {
                    Common.Common.cis_billing.accountHeadId = objBusinessFacade.NonBlankValueOfInt(lblAccountHeadId.Text.ToString());
                    Common.Common.cis_billing.accountName = txtAcountName.Text.ToString();

                    if (cBoxIsAccountGroup.Checked == true)
                    {
                        Common.Common.cis_billing.isAccountGroup = 1;
                    }
                    else
                    {
                        Common.Common.cis_billing.isAccountGroup = 0;
                    }

                    Common.Common.cis_billing.accountGroupId = Convert.ToInt32(cboAccountGroup.SelectedValue);

                    Common.Common.cis_billing.billTypeId = cboBillType.SelectedIndex + 1;

                    Common.Common.cis_billing.billUnitPrice = objBusinessFacade.NonBlankValueOfDecimal(txtUnitPrice.Text.ToString());

                    if (cboStatus.SelectedIndex == 0)
                    {
                        Common.Common.status = 1;
                    }
                    else
                    {
                        Common.Common.status = 0;
                    }

                    objArg.ParamList["id_cis_account_head"] = Common.Common.cis_billing.accountHeadId;
                    objArg.ParamList["account_head_name"] = Common.Common.cis_billing.accountName;
                    objArg.ParamList["is_account_group"] = Common.Common.cis_billing.isAccountGroup;
                    objArg.ParamList["account_group_id"] = Common.Common.cis_billing.accountGroupId;
                    objArg.ParamList["bill_type"] = Common.Common.cis_billing.billTypeId;
                    objArg.ParamList["unit_price"] = Common.Common.cis_billing.billUnitPrice;
                    objArg.ParamList["status"] = Common.Common.status;

                    Common.Common.flag = objBusinessFacade.insertAccountHead(objArg);

                    if (Common.Common.flag == 0)
                    {
                        MessageBox.Show("Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearData();
                        viewAccountHead();
                        loadAccountType();
                    }
                }
                else
                {
                    MessageBox.Show("Account Group is empty...!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Mandatory Fields are Empty...!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void dgvAccountHead_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvAccountHead.Rows.Count > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip m = new ContextMenuStrip();
                    m.Items.Add("Edit").Name = "Edit";
                    m.Items.Add("Delete").Name = "Delete";

                    m.Show(dgvAccountHead, new Point(e.X, e.Y));

                    m.ItemClicked += new ToolStripItemClickedEventHandler(cellMenuItem_Clicked);
                }
            }
        }

        private void cellMenuItem_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int rowIndex = dgvAccountHead.CurrentRow.Index;
            string statusN = string.Empty;

            Common.Common.trans_id = Convert.ToInt32(dgvAccountHead.Rows[rowIndex].Cells["id_cis_account_head"].Value.ToString());

            switch (e.ClickedItem.Name.ToString())
            {
                case "Edit":
                    dtSource = objBusinessFacade.getAccoutHeadById(Common.Common.trans_id);

                    if (dtSource != null && dtSource.Rows.Count > 0)
                    {
                        lblAccountHeadId.Text = dtSource.Rows[0]["id_cis_account_head"].ToString();
                        txtAcountName.Text = dtSource.Rows[0]["account_head_name"].ToString();
                        txtUnitPrice.Text = dtSource.Rows[0]["unit_price"].ToString();
                        cboAccountGroup.SelectedValue = Convert.ToInt32(dtSource.Rows[0]["account_group_id"].ToString());
                        cboBillType.SelectedIndex = Convert.ToInt32(dtSource.Rows[0]["bill_type"].ToString()) - 1;

                        if (Convert.ToInt32(dtSource.Rows[0]["status"].ToString()) == 1)
                        {
                            cboStatus.SelectedIndex = 0;//Active
                        }
                        else
                        {
                            cboStatus.SelectedIndex = 1;//Inactive
                        }

                        if (Convert.ToInt32(dtSource.Rows[0]["is_account_group"].ToString()) == 1)
                        {
                            cBoxIsAccountGroup.Checked = true;
                        }
                        else
                        {
                            cBoxIsAccountGroup.Checked = false;
                        }
                    }
                    break;


                case "Delete":
                    if (dgvAccountHead.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Are you sure to delete the record?", "Doctor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                Common.Common.flag = objBusinessFacade.deleteAccountHead(Common.Common.trans_id);

                                if (Common.Common.flag == 1)
                                {
                                    MessageBox.Show("Record Deleted.....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    viewAccountHead();
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Record Can't be Deleted. It has association", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //throw;
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("Records are not Available to Delete.....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
