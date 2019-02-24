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

namespace CIS.Modules
{
    public partial class cisViewAndAdjustStockByItem : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();

        #endregion

        public cisViewAndAdjustStockByItem()
        {
            InitializeComponent();
        }

        private void txtQtyPha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMRPPha.Focus();
                e.Handled = true;
            }
        }

        private void txtQtyPha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
        }

        private void cisViewAndAdjustStockByItem_Load(object sender, EventArgs e)
        {
            loadPhaItem();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Common.Common.cis_pharmacy_info.selectedPhaTab = Convert.ToString(tcInventoryManagement.SelectedTab.Name);

            switch (Common.Common.cis_pharmacy_info.selectedPhaTab)
            {
                case "tpAddOpeningStock":
                    if (!string.IsNullOrEmpty(lblItemTypePha.Text.ToString()) && !string.IsNullOrEmpty(txtLotIdPha.Text.ToString()) && !string.IsNullOrEmpty(txtExpDateMonth.Text.ToString()) && !string.IsNullOrEmpty(txtExpDateYear.Text.ToString()) && !string.IsNullOrEmpty(txtMRPPha.Text.ToString()))
                    {
                        Common.Common.cis_pharmacy_info.phaItemTypeId = Convert.ToInt32(lblItemTypeId.Text.ToString());
                        Common.Common.cis_pharmacy_info.phaItemType = lblItemTypePha.Text.ToString();
                        Common.Common.cis_pharmacy_info.phaItemId = Convert.ToInt32(cboItemPha.SelectedValue.ToString());
                        Common.Common.cis_pharmacy_info.phaItemName = cboItemPha.Text;
                        Common.Common.cis_pharmacy_info.lotId = txtLotIdPha.Text.ToString();

                        Common.Common.cis_pharmacy_info.phaExpDate = string.Concat(txtExpDateMonth.Text.ToString(), '/', txtExpDateYear.Text.ToString());
                        DateTime expDate = new DateTime(Convert.ToInt32(txtExpDateYear.Text.ToString()), Convert.ToInt32(txtExpDateMonth.Text.ToString()), DateTime.DaysInMonth(Convert.ToInt32(txtExpDateYear.Text.ToString()), Convert.ToInt32(txtExpDateMonth.Text.ToString())));

                        Common.Common.cis_pharmacy_info.phaQty = Convert.ToInt32(txtQtyPha.Text.ToString());
                        Common.Common.cis_pharmacy_info.phaUnitPrice = Convert.ToDecimal(txtMRPPha.Text.ToString());
                        Common.Common.cis_pharmacy_info.salesTaxPerc = objBusinessFacade.NonBlankValueOfDecimal(lblTaxPerc.Text.ToString());

                        dgvInventoryManagement.Rows.Add(dgvInventoryManagement.Rows.Count, 0, Common.Common.cis_pharmacy_info.phaItemTypeId, Common.Common.cis_pharmacy_info.phaItemType, Common.Common.cis_pharmacy_info.phaItemId, Common.Common.cis_pharmacy_info.phaItemName, Common.Common.cis_pharmacy_info.lotId, Common.Common.cis_pharmacy_info.phaExpDate, expDate, 0, Common.Common.cis_pharmacy_info.phaUnitPrice, 0, Common.Common.cis_pharmacy_info.salesTaxPerc, Common.Common.cis_pharmacy_info.phaQty);
                        clearRecords();
                        dgvInventoryManagement.Columns["physical_qty"].Visible = false;
                        dgvInventoryManagement.Columns["adj_qty"].Visible = false;
                        dgvInventoryManagement.Columns["vendor_price"].Visible = false;

                        dgvInventoryManagement.Columns["sno"].ReadOnly = true;
                        dgvInventoryManagement.Columns["item_type"].ReadOnly = true;
                        dgvInventoryManagement.Columns["item_name"].ReadOnly = true;
                        dgvInventoryManagement.Columns["lot_id"].ReadOnly = true;
                        dgvInventoryManagement.Columns["exp_date"].ReadOnly = true;
                        dgvInventoryManagement.Columns["tax_perc"].ReadOnly = true;
                        dgvInventoryManagement.Columns["def_discount"].ReadOnly = true;
                        dgvInventoryManagement.Columns["adj_qty"].ReadOnly = true;
                        dgvInventoryManagement.Columns["vendor_price"].ReadOnly = true;

                        dgvInventoryManagement.Columns["avail_qty"].ReadOnly = false;
                        dgvInventoryManagement.Columns["mrp"].ReadOnly = false;
                        dgvInventoryManagement.Columns["physical_qty"].ReadOnly = false;
                    }

                    else
                    {
                        MessageBox.Show("Fields are required....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;

                case "tpStockByItem":
                    if (!string.IsNullOrEmpty(lblItemTypePha.Text.ToString()))
                    {
                        dgvInventoryManagement.DataSource = null;
                        dgvInventoryManagement.Rows.Clear();
                        Common.Common.cis_pharmacy_info.phaItemId = Convert.ToInt32(cboItemPha.SelectedValue.ToString());
                        try
                        {
                            dtSource = objBusinessFacade.getPhaViewStockByItemId(Common.Common.cis_pharmacy_info.phaItemId);
                            if (dtSource.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtSource.Rows.Count; i++)
                                {
                                    dgvInventoryManagement.Rows.Add();
                                    dgvInventoryManagement.Rows[i].Cells["sno"].Value = dgvInventoryManagement.Rows.Count - 1;
                                    dgvInventoryManagement.Rows[i].Cells["inventory_stock_id"].Value = dtSource.Rows[i]["inventory_stock_id"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["item_type_id"].Value = dtSource.Rows[i]["item_type_id"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["item_type"].Value = dtSource.Rows[i]["item_type"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["item_id"].Value = dtSource.Rows[i]["item_id"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["item_name"].Value = dtSource.Rows[i]["item_name"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["lot_id"].Value = dtSource.Rows[i]["lot_id"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["exp_date"].Value = dtSource.Rows[i]["expiry_date"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["expiry_date"].Value = dtSource.Rows[i]["exp_date"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["avail_qty"].Value = dtSource.Rows[i]["avail_qty"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["mrp"].Value = dtSource.Rows[i]["mrp"].ToString();
                                    dgvInventoryManagement.Rows[i].Cells["tax_perc"].Value = dtSource.Rows[i]["tax_perc"].ToString();
                                }
                                dgvInventoryManagement.Columns["physical_qty"].Visible = true;
                                dgvInventoryManagement.Columns["adj_qty"].Visible = true;
                                dgvInventoryManagement.Columns["vendor_price"].Visible = true;

                                dgvInventoryManagement.Columns["sno"].ReadOnly = true;
                                dgvInventoryManagement.Columns["item_type"].ReadOnly = true;
                                dgvInventoryManagement.Columns["item_name"].ReadOnly = true;
                                dgvInventoryManagement.Columns["lot_id"].ReadOnly = true;
                                dgvInventoryManagement.Columns["exp_date"].ReadOnly = true;
                                dgvInventoryManagement.Columns["tax_perc"].ReadOnly = true;
                                dgvInventoryManagement.Columns["def_discount"].ReadOnly = true;
                                dgvInventoryManagement.Columns["adj_qty"].ReadOnly = true;
                                dgvInventoryManagement.Columns["vendor_price"].ReadOnly = true;

                                dgvInventoryManagement.Columns["avail_qty"].ReadOnly = true;
                                dgvInventoryManagement.Columns["mrp"].ReadOnly = false;
                                dgvInventoryManagement.Columns["physical_qty"].ReadOnly = false;
                                //dgvInventoryManagement.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                            }

                        }
                        catch (Exception ex)
                        {
                            Common.Common.ExceptionHandler.ExceptionWriter(ex);
                            MessageBox.Show(ex.Message + ex.StackTrace);
                        }

                        clearRecords();
                    }
                    else
                    {
                        MessageBox.Show("Fields are required....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;

                default:
                    break;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            clearRecords();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvInventoryManagement.Rows.Count > 1)
            {
                dtSource = objBusinessFacade.getPhaDepartmentId();
                Common.Common.cis_department.departmentId = Convert.ToInt32(dtSource.Rows[0]["department_id"].ToString());

                foreach (DataGridViewRow row in dgvInventoryManagement.Rows)
                {
                    if (objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["item_id"].Value)) > 0)
                    {
                        Common.Common.cis_pharmacy_info.inventoryStockId = Convert.ToInt32(row.Cells["inventory_stock_id"].Value);
                        Common.Common.cis_pharmacy_info.phaItemTypeId = Convert.ToInt32(row.Cells["item_type_id"].Value);
                        Common.Common.cis_pharmacy_info.phaItemId = Convert.ToInt32(row.Cells["item_id"].Value);
                        Common.Common.cis_pharmacy_info.lotId = Convert.ToString(row.Cells["lot_id"].Value);
                        Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToString(row.Cells["expiry_date"].Value);
                        Common.Common.cis_pharmacy_info.phaQty = Convert.ToInt32(row.Cells["avail_qty"].Value);
                        Common.Common.cis_pharmacy_info.physicalPhaQty = Convert.ToInt32(row.Cells["physical_qty"].Value);
                        Common.Common.cis_pharmacy_info.phaUnitPrice = Convert.ToDecimal(row.Cells["mrp"].Value);
                        Common.Common.cis_pharmacy_info.salesTaxPerc = Convert.ToDecimal(row.Cells["tax_perc"].Value);
                        Common.Common.cis_pharmacy_info.transPhaQty = Convert.ToInt32(row.Cells["adj_qty"].Value);

                        objArg.ParamList["transaction_user_id"] = Common.Common.userId;
                        objArg.ParamList["inv_trans_id"] = Common.Common.cis_pharmacy_info.inventoryStockId;
                        objArg.ParamList["item_type_id"] = Common.Common.cis_pharmacy_info.phaItemTypeId;
                        objArg.ParamList["item_id"] = Common.Common.cis_pharmacy_info.phaItemId;
                        objArg.ParamList["lot_id"] = Common.Common.cis_pharmacy_info.lotId;
                        objArg.ParamList["exp_date"] = Common.Common.cis_pharmacy_info.phaExpDate;
                        objArg.ParamList["avail_qty"] = Common.Common.cis_pharmacy_info.phaQty;
                        objArg.ParamList["mrp"] = Common.Common.cis_pharmacy_info.phaUnitPrice;
                        objArg.ParamList["tax_perc"] = Common.Common.cis_pharmacy_info.salesTaxPerc;
                        objArg.ParamList["department_id"] = Common.Common.cis_department.departmentId;
                        objArg.ParamList["physical_qty"] = Common.Common.cis_pharmacy_info.physicalPhaQty;
                        objArg.ParamList["trans_qty"] = Common.Common.cis_pharmacy_info.transPhaQty;

                        if (Convert.ToString(tcInventoryManagement.SelectedTab.Name) == "tpAddOpeningStock")
                        {
                            Common.Common.flag = objBusinessFacade.insertOpeningStock(objArg);
                            Common.Common.trans_id = objBusinessFacade.lastInsertRecord();
                            objArg.ParamList["inv_trans_id"] = Common.Common.trans_id;
                            Common.Common.flag = objBusinessFacade.insertOpeningStockdDetails(objArg);
                        }
                        else if (Convert.ToString(tcInventoryManagement.SelectedTab.Name) == "tpStockByItem" && !(string.IsNullOrEmpty(Convert.ToString(row.Cells["physical_qty"].Value))))
                        {
                            Common.Common.flag = objBusinessFacade.updateStockAdjustment(objArg);
                            Common.Common.flag = objBusinessFacade.insertStockAdjustmentdDetails(objArg);
                        }
                    }
                }
                if (Common.Common.flag == 0)
                {
                    MessageBox.Show("Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtExpDateMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
            txtExpDateMonth.MaxLength = 2;
        }

        private void txtExpDateYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
            txtExpDateYear.MaxLength = 4;
        }

        private void txtMRPPha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void loadPhaItem()
        {
            try
            {
                dtSource = objBusinessFacade.loadPhaItem();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboItemPha.ValueMember = "item_id";
                    cboItemPha.DisplayMember = "item_name";
                    cboItemPha.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void cboItemPha_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboItemPha.Items.Count > 1 && cboItemPha.SelectedIndex > 0)
            {
                Common.Common.cis_pharmacy_info.phaItemId = Convert.ToInt32(cboItemPha.SelectedValue.ToString());

                try
                {
                    Common.Common.cis_pharmacy_info.phaTotalQty = 0;
                    dtSource = objBusinessFacade.getPhaTypeAndAvailQty(Common.Common.cis_pharmacy_info.phaItemId);
                    lblItemTypePha.Text = dtSource.Rows[0]["item_type"].ToString();
                    lblItemTypeId.Text = dtSource.Rows[0]["item_type_id"].ToString();
                    lblTotalAvailQty.Text = dtSource.Rows[0]["total_avail_qty"].ToString();
                    lblTaxPerc.Text = dtSource.Rows[0]["tax_rate"].ToString();
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void cboItemPha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLotIdPha.Focus();
                e.Handled = true;
            }
        }

        private void txtLotIdPha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtExpDateMonth.Focus();
                e.Handled = true;
            }
        }

        private void txtExpDateMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtExpDateMonth.Text.ToString()))
                {

                    if (Convert.ToInt32(txtExpDateMonth.Text.ToString()) <= 12 || Convert.ToInt32(txtExpDateMonth.Text.ToString()) < 0)
                    {
                        txtExpDateYear.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Month is not valid....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    e.Handled = true;
                }
            }

        }

        private void txtExpDateYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQtyPha.Focus();
                e.Handled = true;
            }
        }

        private void txtQtyPha_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMRPPha.Focus();
                e.Handled = true;
            }
        }

        private void txtMRPPha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.Focus();
                e.Handled = true;
            }
        }

        private void clearRecords()
        {
            cboItemPha.SelectedIndex = 0;
            txtLotIdPha.Text = string.Empty;
            txtExpDateMonth.Text = string.Empty;
            txtExpDateYear.Text = string.Empty;
            txtQtyPha.Text = string.Empty;
            lblItemTypePha.Text = string.Empty;
            lblItemTypeId.Text = string.Empty;
            lblTotalAvailQty.Text = string.Empty;
            txtMRPPha.Text = string.Empty;
            lblTaxPerc.Text = string.Empty;
            cboItemPha.Focus();
        }


        private void tcInventoryManagement_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.Common.cis_pharmacy_info.selectedPhaTab = Convert.ToString(tcInventoryManagement.SelectedTab.Name);

            switch (Common.Common.cis_pharmacy_info.selectedPhaTab)
            {
                case "tpAddOpeningStock":
                    dgvInventoryManagement.DataSource = null;
                    dgvInventoryManagement.Rows.Clear();
                    txtLotIdPha.Enabled = true;
                    txtExpDateMonth.Enabled = true;
                    txtExpDateYear.Enabled = true;
                    txtQtyPha.Enabled = true;
                    txtMRPPha.Enabled = true;
                    break;

                case "tpStockByItem":
                    dgvInventoryManagement.DataSource = null;
                    dgvInventoryManagement.Rows.Clear();
                    txtLotIdPha.Enabled = false;
                    txtExpDateMonth.Enabled = false;
                    txtExpDateYear.Enabled = false;
                    txtQtyPha.Enabled = false;
                    txtMRPPha.Enabled = false;
                    break;

                default:
                    break;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearRecords();
            dgvInventoryManagement.DataSource = null;
            dgvInventoryManagement.Rows.Clear();
            btnSave.Enabled = true;
        }

        private void dgvInventoryManagement_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInventoryManagement.Rows.Count > 1)
            {
                int rowIndex = dgvInventoryManagement.CurrentRow.Index;

                if (dgvInventoryManagement.Columns[e.ColumnIndex].Name.Equals("physical_qty") && !string.IsNullOrEmpty(Convert.ToString(dgvInventoryManagement.Rows[rowIndex].Cells["physical_qty"].Value)))
                {
                    dgvInventoryManagement.Rows[rowIndex].Cells["adj_qty"].Value = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvInventoryManagement.Rows[rowIndex].Cells["physical_qty"].Value)) - objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvInventoryManagement.Rows[rowIndex].Cells["avail_qty"].Value));
                }
                else if (dgvInventoryManagement.Columns[e.ColumnIndex].Name.Equals("physical_qty") && string.IsNullOrEmpty(Convert.ToString(dgvInventoryManagement.Rows[rowIndex].Cells["physical_qty"].Value)))
                {
                    dgvInventoryManagement.Rows[rowIndex].Cells["adj_qty"].Value = string.Empty;
                }
            }
        }

        private void Control_KeyPress_digit(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void Control_KeyPress_int(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
        }

        private void dgvInventoryManagement_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvInventoryManagement.Columns.Contains("physical_qty"))
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress_int);
            }

            if (dgvInventoryManagement.Columns.Contains("mrp"))
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress_digit);
            }
        }

        private void txtLotIdPha_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtExpDateMonth.Focus();
            }
            e.IsInputKey = true;
        }

        private void txtExpDateMonth_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (!string.IsNullOrEmpty(txtExpDateMonth.Text.ToString()))
                {

                    if (Convert.ToInt32(txtExpDateMonth.Text.ToString()) <= 12 || Convert.ToInt32(txtExpDateMonth.Text.ToString()) < 0)
                    {
                        txtExpDateYear.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Month is not valid....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    e.IsInputKey = true;
                }
            }
        }

        private void txtExpDateYear_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtQtyPha.Focus();
            }
            e.IsInputKey = true;
        }
    }
}
