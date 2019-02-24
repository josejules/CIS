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
    public partial class cisInventoryMovements : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        public static DataTable dtPhaItemDetails = new DataTable();
        #endregion

        public cisInventoryMovements()
        {
            InitializeComponent();
        }

        private void cisInventoryMovements_Load(object sender, EventArgs e)
        {
            this.dtpTransDate.Value = DateTime.Now;
            loadVendor();
            loadInternalMoveDepartment();
            consumeType();
            tscmbMovementType.SelectedIndex = 0;
            loadPhaItem();
        }

        private void loadVendor()
        {
            try
            {
                dtSource = objBusinessFacade.loadVendor();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = 0;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboVendor.ValueMember = "vendor_id";
                    cboVendor.DisplayMember = "vendor_name";
                    cboVendor.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadInternalMoveDepartment()
        {
            try
            {
                dtSource = objBusinessFacade.loadInternalMoveDepartment();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = 0;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboDepartment.ValueMember = "department_id";
                    cboDepartment.DisplayMember = "DEPARTMENT_NAME";
                    cboDepartment.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void consumeType()
        {
            try
            {
                dtSource = objBusinessFacade.consumeType();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = 0;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboConsumeType.ValueMember = "consume_type_id";
                    cboConsumeType.DisplayMember = "consume_type";
                    cboConsumeType.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadPhaItem()
        {
            try
            {
                dtSource = null;
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

        private void tscmbMovementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscmbMovementType.SelectedIndex == 0)//Internal Movements
            {
                cboDepartment.Enabled = true;
                cboConsumeType.Enabled = false;
                cboConsumeType.SelectedIndex = 0;
                cboVendor.Enabled = false;
                cboVendor.SelectedIndex = 0;
                txtReturnNo.Enabled = false;
            }

            else if (tscmbMovementType.SelectedIndex == 1)//Return Vendor
            {
                cboDepartment.Enabled = false;
                cboDepartment.SelectedIndex = 0;
                cboConsumeType.Enabled = false;
                cboConsumeType.SelectedIndex = 0;
                cboVendor.Enabled = true;
                txtReturnNo.Enabled = true;
            }

            else if (tscmbMovementType.SelectedIndex == 2)//Consume Items
            {
                cboDepartment.Enabled = false;
                cboDepartment.SelectedIndex = 0;
                cboConsumeType.Enabled = true;
                cboVendor.Enabled = false;
                cboVendor.SelectedIndex = 0;
                txtReturnNo.Enabled = false;
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
                    dtSource = null;
                    dtSource = objBusinessFacade.getPhaByItemIdAll(Common.Common.cis_pharmacy_info.phaItemId);
                    DataTable dtSourceC = dtSource;
                    dtPhaItemDetails = dtSource;
                    if (dtSourceC != null && dtSourceC.Rows.Count > 0)
                    {
                        cboLotIdPha.ValueMember = "inventory_stock_id";
                        cboLotIdPha.DisplayMember = "lot_id";
                        cboLotIdPha.DataSource = dtSourceC;

                        Common.Common.cis_pharmacy_info.inventoryStockId = Convert.ToInt32(lblinventoryStockId.Text.ToString());
                    }

                    else
                    {
                        MessageBox.Show("Found no stock for this Item....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Common.Common.cis_pharmacy_info.inventoryStockId = 0;
                    }

                    if (Common.Common.cis_pharmacy_info.inventoryStockId > 0)
                    {
                        int i = 0;
                        foreach (DataRow dr in dtSource.Rows)
                        {
                            Common.Common.cis_pharmacy_info.phaTotalQty += Convert.ToInt32(dtSource.Rows[i]["avail_qty"].ToString());
                            i++;
                        }
                        //string filterField = Common.Common.cis_pharmacy_info.inventoryStockId.ToString();
                        //dtSourceC.DefaultView.RowFilter = string.Format("[{0}] ='{1}'", "inventory_stock_id", Common.Common.cis_pharmacy_info.inventoryStockId);
                        //dgvDoctor.DataSource = dtSource;
                        //dtSource.Select("inventory_stock_id = InvStockId").Any();
                        Common.Common.cis_pharmacy_info.inventoryStockId = Convert.ToInt32(dtSource.Rows[0]["inventory_stock_id"].ToString());
                        Common.Common.cis_pharmacy_info.phaItemName = Convert.ToString(dtSource.Rows[0]["item_name"].ToString());
                        Common.Common.cis_pharmacy_info.phaDeptId = Convert.ToInt32(dtSource.Rows[0]["department_id"].ToString());
                        Common.Common.cis_pharmacy_info.phaItemTypeId = Convert.ToInt32(dtSource.Rows[0]["item_type_id"].ToString());
                        Common.Common.cis_pharmacy_info.phaItemType = Convert.ToString(dtSource.Rows[0]["item_type"].ToString());
                        Common.Common.cis_pharmacy_info.lotId = Convert.ToString(dtSource.Rows[0]["lot_id"].ToString());
                        Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToDateTime(dtSource.Rows[0]["exp_date"].ToString()).ToString("MM/yyyy");
                        Common.Common.cis_pharmacy_info.phaQty = Convert.ToInt32(dtSource.Rows[0]["avail_qty"].ToString());
                        Common.Common.cis_pharmacy_info.vendorPrice = Convert.ToDecimal(dtSource.Rows[0]["vendor_price"].ToString());
                        Common.Common.cis_pharmacy_info.salesTaxPerc = Convert.ToDecimal(dtSource.Rows[0]["sales_tax_perc"].ToString());

                        lblItemTypePha.Text = Common.Common.cis_pharmacy_info.phaItemType; ;
                        txtVendorPricePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.vendorPrice);
                        lblExpDatePha.Text = Common.Common.cis_pharmacy_info.phaExpDate;
                        lblExpDateFull.Text = dtSource.Rows[0]["exp_date"].ToString();
                        lblAvailQtyPha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaQty);
                        lblTaxPerc.Text = Convert.ToString(Common.Common.cis_pharmacy_info.salesTaxPerc);
                        lblTotalAvailQtyPha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaTotalQty);
                        txtQtyPha.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void cboLotIdPha_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.Common.cis_pharmacy_info.inventoryStockId = Convert.ToInt32(cboLotIdPha.SelectedValue);
            lblinventoryStockId.Text = Common.Common.cis_pharmacy_info.inventoryStockId.ToString();

            if (dtSource.Rows.Count > 0 && cboLotIdPha.SelectedValue != null)
            {
                DataView view = dtSource.Copy().DefaultView;
                view.RowFilter = "inventory_stock_id = " + cboLotIdPha.SelectedValue;

                DataTable dtSelectedBatch = view.ToTable();
                if (dtSelectedBatch.Rows.Count > 0)
                {
                    Common.Common.cis_pharmacy_info.phaItemType = Convert.ToString(dtSelectedBatch.Rows[0]["item_type"].ToString());
                    Common.Common.cis_pharmacy_info.lotId = Convert.ToString(dtSelectedBatch.Rows[0]["lot_id"].ToString());
                    Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToDateTime(dtSelectedBatch.Rows[0]["exp_date"].ToString()).ToString("MM/yyyy");
                    Common.Common.cis_pharmacy_info.phaQty = Convert.ToInt32(dtSelectedBatch.Rows[0]["avail_qty"].ToString());
                    Common.Common.cis_pharmacy_info.vendorPrice = Convert.ToDecimal(dtSelectedBatch.Rows[0]["vendor_price"].ToString());
                    Common.Common.cis_pharmacy_info.phaFreeCare = Convert.ToDecimal(dtSelectedBatch.Rows[0]["default_discount"].ToString());
                    Common.Common.cis_pharmacy_info.salesTaxPerc = Convert.ToDecimal(dtSelectedBatch.Rows[0]["sales_tax_perc"].ToString());

                    lblItemTypePha.Text = Common.Common.cis_pharmacy_info.phaItemType; ;
                    txtVendorPricePha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.vendorPrice);
                    lblExpDatePha.Text = Common.Common.cis_pharmacy_info.phaExpDate;
                    lblAvailQtyPha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaQty);
                    lblTaxPerc.Text = Convert.ToString(Common.Common.cis_pharmacy_info.salesTaxPerc);
                    lblTotalAvailQtyPha.Text = Convert.ToString(Common.Common.cis_pharmacy_info.phaTotalQty);
                    txtQtyPha.Focus();
                }
            }
        }

        private void txtQtyPha_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b') ? false : true;
        }

        private void txtQtyPha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (!string.IsNullOrEmpty(Common.Common.cis_pharmacy_info.phaItemName) && Common.Common.cis_pharmacy_info.phaItemId > 0 && !string.IsNullOrEmpty(txtQtyPha.Text.ToString()) && Convert.ToInt32(txtQtyPha.Text.ToString()) != 0)
                {
                    Common.Common.cis_pharmacy_info.phaQty = Convert.ToInt32(txtQtyPha.Text.ToString());
                    Common.Common.cis_pharmacy_info.vendorPrice = Convert.ToDecimal(txtVendorPricePha.Text.ToString());
                    Common.Common.cis_pharmacy_info.salesTaxPerc = Convert.ToDecimal(lblTaxPerc.Text.ToString());
                    Common.Common.cis_pharmacy_info.phaTotalAmt = (Common.Common.cis_pharmacy_info.phaQty * Common.Common.cis_pharmacy_info.vendorPrice);
                    //Common.Common.cis_pharmacy_info.MRP.ToString("#.##");
                    if (Common.Common.cis_pharmacy_info.phaTotalAmt != 0)
                    {
                        Common.Common.cis_pharmacy_info.taxAmtPur = Math.Round((Common.Common.cis_pharmacy_info.phaTotalAmt * Common.Common.cis_pharmacy_info.salesTaxPerc / 100), 2);
                    }
                    else
                    {
                        Common.Common.cis_pharmacy_info.taxAmtPur = 0;
                    }

                    lblTaxAmount.Text = Common.Common.cis_pharmacy_info.taxAmtPur.ToString("#.##");
                    Common.Common.cis_pharmacy_info.netTotalPha = Math.Round((Common.Common.cis_pharmacy_info.phaTotalAmt + Common.Common.cis_pharmacy_info.taxAmtPur), 2);
                    lblTotalAmtPha.Text = Common.Common.cis_pharmacy_info.netTotalPha.ToString("#.##");

                    if (Common.Common.cis_pharmacy_info.phaQty > 0)
                    {
                        if (Common.Common.cis_pharmacy_info.phaQty <= objBusinessFacade.NonBlankValueOfInt(lblTotalAvailQtyPha.Text.ToString()))
                        {
                            if (string.IsNullOrEmpty(lblCheckEditModePha.Text.ToString()))//Add Item
                            {
                                bool entryFound = false;
                                foreach (DataGridViewRow row in dgvMovementBillDetails.Rows)
                                {
                                    int phaId = Convert.ToInt32(row.Cells["inventoryStockId"].Value);
                                    if (phaId == Common.Common.cis_pharmacy_info.inventoryStockId)
                                    {
                                        MessageBox.Show("Already Exists....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        entryFound = true;
                                        cboItemPha.Focus();
                                        return;
                                    }
                                }
                                //Logic -> Get Next availble batches if issue qty more than current batch //Implemented By : Jules
                                /*if (Common.Common.cis_pharmacy_info.phaQty >= Convert.ToDecimal(lblAvailQtyPha.Text))
                                {
                                    decimal issuedQty = 0;
                                    decimal issuedQtyBatch = 0;
                                    foreach (DataRow row in dtPhaItemDetails.Rows)
                                    {
                                        if (issuedQty < Common.Common.cis_pharmacy_info.phaQty)
                                        {
                                            if (issuedQty + Convert.ToDecimal(row["avail_qty"].ToString()) > Common.Common.cis_pharmacy_info.phaQty)
                                                issuedQtyBatch = Common.Common.cis_pharmacy_info.phaQty - issuedQty;
                                            else
                                                issuedQtyBatch = Convert.ToDecimal(row["avail_qty"].ToString());
                                            Common.Common.cis_pharmacy_info.phaTotalAmt = issuedQtyBatch * Convert.ToDecimal(row["mrp"].ToString());
                                            Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToDateTime(row["exp_date"].ToString()).ToString("MM/yyyy");
                                            dgvPharmacyBillDetails.Rows.Add(dgvPharmacyBillDetails.Rows.Count + 1, Common.Common.cis_pharmacy_info.phaItemId, Common.Common.cis_pharmacy_info.phaItemName, Common.Common.cis_pharmacy_info.phaItemTypeId, Common.Common.cis_pharmacy_info.phaItemType, row["lot_id"].ToString(),
                                                Common.Common.cis_pharmacy_info.phaExpDate, issuedQtyBatch, Convert.ToDecimal(row["mrp"].ToString()), Convert.ToDecimal(row["default_discount"].ToString()), Common.Common.cis_pharmacy_info.phaTotalAmt, Common.Common.cis_pharmacy_info.phaDeptId, Common.Common.cis_pharmacy_info.inventoryStockId, Convert.ToDouble(row["sales_tax_perc"].ToString()));
                                        }
                                        issuedQty += issuedQtyBatch;
                                    }
                                }
                                else
                                {
                                    if ((!entryFound))
                                        dgvPharmacyBillDetails.Rows.Add(dgvPharmacyBillDetails.Rows.Count + 1, Common.Common.cis_pharmacy_info.phaItemId, Common.Common.cis_pharmacy_info.phaItemName, Common.Common.cis_pharmacy_info.phaItemTypeId, Common.Common.cis_pharmacy_info.phaItemType, Common.Common.cis_pharmacy_info.lotId, Common.Common.cis_pharmacy_info.phaExpDate, Common.Common.cis_pharmacy_info.phaQty, Common.Common.cis_pharmacy_info.phaUnitPrice, Common.Common.cis_pharmacy_info.phaFreeCareTotal, Common.Common.cis_pharmacy_info.phaTotalAmt, Common.Common.cis_pharmacy_info.phaDeptId, Common.Common.cis_pharmacy_info.inventoryStockId, Common.Common.cis_pharmacy_info.salesTaxPerc);
                                }*/

                                if ((!entryFound))
                                {
                                    dgvMovementBillDetails.Rows.Add(dgvMovementBillDetails.Rows.Count + 1, Common.Common.cis_pharmacy_info.phaItemId, Common.Common.cis_pharmacy_info.phaItemName, Common.Common.cis_pharmacy_info.phaItemTypeId, Common.Common.cis_pharmacy_info.phaItemType, Common.Common.cis_pharmacy_info.lotId, Common.Common.cis_pharmacy_info.phaExpDate, Common.Common.cis_pharmacy_info.phaQty, Common.Common.cis_pharmacy_info.vendorPrice, Common.Common.cis_pharmacy_info.salesTaxPerc, Common.Common.cis_pharmacy_info.taxAmtPur, Common.Common.cis_pharmacy_info.netTotalPha, Common.Common.cis_pharmacy_info.phaDeptId, Common.Common.cis_pharmacy_info.inventoryStockId);
                                }

                                clearPhaInputValues();
                                cboItemPha.Focus();
                                calculatePhaSum();
                            }

                            else //Edit Item
                            {
                                int rowNo = Convert.ToInt32(lblCheckEditModePha.Text.ToString());
                                dgvMovementBillDetails.Rows[rowNo].Cells["ItemIdPha"].Value = Common.Common.cis_pharmacy_info.phaItemId;
                                dgvMovementBillDetails.Rows[rowNo].Cells["ItemNamePha"].Value = Common.Common.cis_pharmacy_info.phaItemName;
                                dgvMovementBillDetails.Rows[rowNo].Cells["itemTypeId"].Value = Common.Common.cis_pharmacy_info.phaItemTypeId;
                                dgvMovementBillDetails.Rows[rowNo].Cells["ItemTypePha"].Value = Common.Common.cis_pharmacy_info.phaItemType;
                                dgvMovementBillDetails.Rows[rowNo].Cells["LotIdPha"].Value = Common.Common.cis_pharmacy_info.lotId;
                                dgvMovementBillDetails.Rows[rowNo].Cells["ExpDatePha"].Value = Common.Common.cis_pharmacy_info.phaExpDate;
                                dgvMovementBillDetails.Rows[rowNo].Cells["QtyPha"].Value = Common.Common.cis_pharmacy_info.phaQty;
                                dgvMovementBillDetails.Rows[rowNo].Cells["VendorPricePha"].Value = Common.Common.cis_pharmacy_info.vendorPrice;
                                dgvMovementBillDetails.Rows[rowNo].Cells["tax_perc"].Value = Common.Common.cis_pharmacy_info.salesTaxPerc;
                                dgvMovementBillDetails.Rows[rowNo].Cells["TaxAmt"].Value = Common.Common.cis_pharmacy_info.taxAmtPur;
                                dgvMovementBillDetails.Rows[rowNo].Cells["TotalAmtPha"].Value = Common.Common.cis_pharmacy_info.netTotalPha;
                                dgvMovementBillDetails.Rows[rowNo].Cells["phaDeptId"].Value = Common.Common.cis_pharmacy_info.phaDeptId;
                                dgvMovementBillDetails.Rows[rowNo].Cells["inventoryStockId"].Value = Common.Common.cis_pharmacy_info.inventoryStockId;
                                clearPhaInputValues();
                                cboItemPha.Focus();
                                calculatePhaSum();
                            }
                        }

                        else
                        {
                            MessageBox.Show("Issued qty can't be greater than Total Avail Qty....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    else
                    {
                        MessageBox.Show("Fields are required....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void clearPhaInputValues()
        {
            cboItemPha.SelectedIndex = -1;
            lblItemTypePha.Text = string.Empty;
            lblExpDatePha.Text = string.Empty;
            cboLotIdPha.DataSource = null;
            txtQtyPha.Text = string.Empty;
            txtVendorPricePha.Text = string.Empty;
            lblAvailQtyPha.Text = "0.00";
            lblTotalAvailQtyPha.Text = "0.00";
            lblTaxPerc.Text = "0.00";
            lblTotalAmtPha.Text = "0.00";
            Common.Common.cis_pharmacy_info.phaItemId = 0;
            Common.Common.cis_pharmacy_info.phaItemName = string.Empty;
            Common.Common.cis_pharmacy_info.phaItemTypeId = 0;
            Common.Common.cis_pharmacy_info.phaItemType = string.Empty;
            Common.Common.cis_pharmacy_info.lotId = string.Empty;
            Common.Common.cis_pharmacy_info.phaExpDate = string.Empty;
            Common.Common.cis_pharmacy_info.phaQty = 0;
            Common.Common.cis_pharmacy_info.phaUnitPrice = 0;
            Common.Common.cis_pharmacy_info.phaQty = 0;
            Common.Common.cis_pharmacy_info.phaFreeCareTotal = 0;
            Common.Common.cis_pharmacy_info.phaTotalAmt = 0;
            Common.Common.cis_pharmacy_info.phaDeptId = 0;
            Common.Common.cis_pharmacy_info.inventoryStockId = 0;
            Common.Common.cis_pharmacy_info.phaTotalSum = 0;
            lblinventoryStockId.Text = "0";
            lblCheckEditModePha.Text = string.Empty;
        }

        private void calculatePhaSum()
        {
            decimal sum = 0;
            Common.Common.cis_pharmacy_info.phaTotalSum = 0;
            foreach (DataGridViewRow row in dgvMovementBillDetails.Rows) //Calculate Amount Columns
            {
                sum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["TotalAmtPha"].Value));
                //objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["taxAmt"].Value));
                Common.Common.cis_pharmacy_info.phaTotalSum = Common.Common.cis_pharmacy_info.phaTotalSum + sum;
            }

            lblNetValuePur.Text = Math.Round(Common.Common.cis_pharmacy_info.phaTotalSum).ToString("0.00"); //Display Total Sum
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvMovementBillDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMovementBillDetails.Rows.Count > 0)
            {
                if (dgvMovementBillDetails.Columns[e.ColumnIndex].Name.Equals("EditPha"))//Edit Button Click
                {
                    cboItemPha.SelectedValue = Convert.ToInt32(dgvMovementBillDetails.Rows[e.RowIndex].Cells["ItemIdPha"].Value.ToString());
                    cboLotIdPha.Text = dgvMovementBillDetails.Rows[e.RowIndex].Cells["LotIdPha"].Value.ToString();
                    lblExpDatePha.Text = dgvMovementBillDetails.Rows[e.RowIndex].Cells["ExpDatePha"].Value.ToString();
                    txtQtyPha.Text = dgvMovementBillDetails.Rows[e.RowIndex].Cells["QtyPha"].Value.ToString();
                    txtVendorPricePha.Text = dgvMovementBillDetails.Rows[e.RowIndex].Cells["VendorPricePha"].Value.ToString();
                    lblTaxAmount.Text = dgvMovementBillDetails.Rows[e.RowIndex].Cells["TaxAmt"].Value.ToString();
                    lblTotalAmtPha.Text = dgvMovementBillDetails.Rows[e.RowIndex].Cells["TotalAmtPha"].Value.ToString();
                    lblinventoryStockId.Text = dgvMovementBillDetails.Rows[e.RowIndex].Cells["inventoryStockId"].Value.ToString();
                    lblCheckEditModePha.Text = e.RowIndex.ToString();
                }

                if (dgvMovementBillDetails.Columns[e.ColumnIndex].Name.Equals("DeletePha"))//Delete Button Click
                {
                    if (MessageBox.Show("Are you sure to Delete the record?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvMovementBillDetails.Rows.Remove(dgvMovementBillDetails.Rows[e.RowIndex]);
                        calculatePhaSum();
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearPharmacyRecords();
        }

        private void clearPharmacyRecords()
        {
            clearPhaInputValues();
            dgvMovementBillDetails.Rows.Clear();
            dgvMovementBillDetails.DataSource = null;
            lblTotalAmtPha.Text = "0.00";
            Common.Common.cis_pharmacy_info.phaTotalSum = 0;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to Save the record?", "Inventory Movements", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dgvMovementBillDetails.Rows.Count > 0)
                    {
                        ComArugments objArg = objBusinessFacade.getInventoryMovementsNumberFormat();
                        Common.Common.cis_number_generation.running_inventory_movements_number = Convert.ToInt32(objArg.ParamList["running_inventory_movements_number"]);
                        Common.Common.cis_number_generation.inventory_movements_number = Convert.ToString(objArg.ParamList["inventory_movements_number"]);

                        Common.Common.cis_pharmacy_info.invoiceNumber = Convert.ToString(txtReturnNo.Text.ToString());
                        Common.Common.cis_pharmacy_info.invoiceDate = Convert.ToDateTime(dtpTransDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        Common.Common.cis_pharmacy_info.vendorId = Convert.ToInt32(cboVendor.SelectedValue);
                        Common.Common.cis_department.departmentId = Convert.ToInt32(cboDepartment.SelectedValue);
                        Common.Common.cis_pharmacy_info.consumeId = Convert.ToInt32(cboConsumeType.SelectedValue);
                        Common.Common.cis_pharmacy_info.investoryMoveTypeId = tscmbMovementType.ComboBox.SelectedIndex;
                        Common.Common.cis_pharmacy_info.totalAmtPurSum = objBusinessFacade.NonBlankValueOfDecimal(lblNetValuePur.Text.ToString());

                        objArg.ParamList["running_inventory_movements_number"] = Common.Common.cis_number_generation.running_inventory_movements_number;
                        objArg.ParamList["inventory_movements_number"] = Common.Common.cis_number_generation.inventory_movements_number;
                        objArg.ParamList["return_no"] = Common.Common.cis_pharmacy_info.invoiceNumber;
                        objArg.ParamList["transaction_date"] = Common.Common.cis_pharmacy_info.invoiceDate;
                        objArg.ParamList["vendor_id"] = Common.Common.cis_pharmacy_info.vendorId;
                        objArg.ParamList["department_id"] = Common.Common.cis_department.departmentId;
                        objArg.ParamList["consume_type_id"] = Common.Common.cis_pharmacy_info.consumeId;
                        objArg.ParamList["movement_type"] = Common.Common.cis_pharmacy_info.investoryMoveTypeId;
                        objArg.ParamList["total_amount"] = Common.Common.cis_pharmacy_info.totalAmtPurSum;

                        Common.Common.flag = objBusinessFacade.insertInventoryMovements(objArg);
                        Common.Common.billId = objBusinessFacade.lastInsertRecord();

                        foreach (DataGridViewRow row in dgvMovementBillDetails.Rows)
                        {
                            if (objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["ItemIdPha"].Value)) != 0 && !(string.IsNullOrEmpty(row.Cells["ItemIdPha"].ToString())))
                            {
                                Common.Common.cis_pharmacy_info.phaItemId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["ItemIdPha"].Value));
                                Common.Common.cis_pharmacy_info.phaItemTypeId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["itemTypeId"].Value));
                                Common.Common.cis_pharmacy_info.lotId = Convert.ToString(row.Cells["LotIdPha"].Value);
                                Common.Common.cis_pharmacy_info.phaExpDate = Convert.ToString(row.Cells["ExpDatePha"].Value);
                                Common.Common.cis_pharmacy_info.phaQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["QtyPha"].Value));
                                Common.Common.cis_pharmacy_info.vendorPrice = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["VendorPricePha"].Value));
                                Common.Common.cis_pharmacy_info.phaTotalAmt = Common.Common.cis_pharmacy_info.phaQty * Common.Common.cis_pharmacy_info.vendorPrice;
                                Common.Common.cis_pharmacy_info.taxPercPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["tax_perc"].Value));
                                Common.Common.cis_pharmacy_info.taxAmtPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["taxAmt"].Value));
                                Common.Common.cis_pharmacy_info.netTotalPha = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["TotalAmtPha"].Value));
                                Common.Common.cis_pharmacy_info.inventoryStockId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["inventoryStockId"].Value));

                                objArg.ParamList["pha_bill_id"] = Common.Common.billId;
                                objArg.ParamList["pha_item_id"] = Common.Common.cis_pharmacy_info.phaItemId;
                                objArg.ParamList["pha_item_type_id"] = Common.Common.cis_pharmacy_info.phaItemTypeId;
                                objArg.ParamList["pha_lot_id"] = Common.Common.cis_pharmacy_info.lotId;
                                objArg.ParamList["pha_expiry_date"] = Common.Common.cis_pharmacy_info.phaExpDate;
                                objArg.ParamList["pha_qty"] = Common.Common.cis_pharmacy_info.phaQty;
                                objArg.ParamList["VendorPricePha"] = Common.Common.cis_pharmacy_info.vendorPrice;
                                objArg.ParamList["pha_total_amt"] = Common.Common.cis_pharmacy_info.phaTotalAmt;
                                objArg.ParamList["tax_perc"] = Common.Common.cis_pharmacy_info.taxPercPur;
                                objArg.ParamList["taxAmt"] = Common.Common.cis_pharmacy_info.taxAmtPur;
                                objArg.ParamList["net_total_amount"] = Common.Common.cis_pharmacy_info.netTotalPha;
                                objArg.ParamList["inventory_stock_id"] = Common.Common.cis_pharmacy_info.inventoryStockId;

                                Common.Common.flag = objBusinessFacade.insertInventoryMovementsDetails(objArg);
                                Common.Common.flag = objBusinessFacade.updateStockPharmacyBill(objArg);
                            }
                        }
                        Common.Common.flag = objBusinessFacade.updateInventoryMovementsRunningNumber(objArg);
                        tslblIMNo.Text = Common.Common.cis_number_generation.inventory_movements_number;

                        if (Common.Common.flag == 1)
                        {
                            btnSave.Enabled = false;
                            MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                       /* if (tscmbMovementType.SelectedIndex == 0)//Internal Movements
                        {

                        }*/
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
