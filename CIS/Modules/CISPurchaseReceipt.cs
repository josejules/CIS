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
    public partial class CISPurchaseReceipt : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public CISPurchaseReceipt()
        {
            InitializeComponent();
            dgvPurchaseReceipt.Rows[0].Cells[16].Value = "No";
            dgvPurchaseReceipt.Columns[0].ReadOnly = true;
            dgvPurchaseReceipt.Columns[11].ReadOnly = true;
            dgvPurchaseReceipt.Columns[15].ReadOnly = true;
            dgvPurchaseReceipt.Columns[17].ReadOnly = true;
            dgvPurchaseReceipt.Columns[18].ReadOnly = true;
            dgvPurchaseReceipt.Rows[dgvPurchaseReceipt.RowCount - 1].Cells[0].Value = dgvPurchaseReceipt.RowCount;
            cboTaxFormula.SelectedIndex = 1;
        }

        private void dgvPurchaseReceipt_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dgvPurchaseReceipt.Rows[dgvPurchaseReceipt.RowCount - 1].Cells[0].Value = dgvPurchaseReceipt.RowCount;
            dgvPurchaseReceipt.Rows[dgvPurchaseReceipt.RowCount - 1].Cells[16].Value = "No";
        }

        public void addItems(AutoCompleteStringCollection col)
        {
            dtSource = objBusinessFacade.loadPhaItem();
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                for(int i=0; i < dtSource.Rows.Count; i++)
                {
                    col.Add(dtSource.Rows[i]["item_name"].ToString());
                }
            }
            
        }
        private void dgvPurchaseReceipt_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvPurchaseReceipt.Columns.Contains("cboItemName"))
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.Suggest;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    addItems(DataCollection);
                    autoText.AutoCompleteCustomSource = DataCollection;
                }
            }

            if (dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(5) || dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(6) || dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(7) || dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(8))
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress_int);
            }
            else if (dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(9) || dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(10) || dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(12) || dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(13) || dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(14))
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress_digit);
            }
            else if (dgvPurchaseReceipt.CurrentCell.ColumnIndex.Equals(4))
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress_ExpDate);
            }

            else
            {
                e.Control.KeyPress += new KeyPressEventHandler(AllowAll_KeyPress);
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

        private void Control_KeyPress_ExpDate(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '/') ? false : true;
        }

        private void AllowAll_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.' || char.IsLetterOrDigit(e.KeyChar) || char.IsSymbol(e.KeyChar)) ? false : true;
        }

        private void calculatePurchase()
        {
            int rowIndex = dgvPurchaseReceipt.CurrentRow.Index;

            Common.Common.cis_pharmacy_info.packX = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["packX"].Value));
            Common.Common.cis_pharmacy_info.packY = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["packY"].Value));

            if (Common.Common.cis_pharmacy_info.packX != 0 && Common.Common.cis_pharmacy_info.packY != 0)
            {
                Common.Common.cis_pharmacy_info.receiveQty = Common.Common.cis_pharmacy_info.packX * Common.Common.cis_pharmacy_info.packY;
                dgvPurchaseReceipt.Rows[rowIndex].Cells["receiveQty"].Value = Common.Common.cis_pharmacy_info.receiveQty;
            }
            else
            {
                //Common.Common.cis_pharmacy_info.receiveQty = 0;
                //dgvPurchaseReceipt.Rows[rowIndex].Cells["receiveQty"].Value = Common.Common.cis_pharmacy_info.receiveQty;
            }

            Common.Common.cis_pharmacy_info.receiveQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["receiveQty"].Value));
            Common.Common.cis_pharmacy_info.offerQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["offerQty"].Value));

            if (Common.Common.cis_pharmacy_info.receiveQty != 0 && Common.Common.cis_pharmacy_info.packY != 0)
            {
                Common.Common.cis_pharmacy_info.vendorPrice = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["vendorPrice"].Value));
                if (Common.Common.cis_pharmacy_info.vpActive == false && Common.Common.cis_pharmacy_info.vendorPrice != 0)
                {
                    Common.Common.cis_pharmacy_info.vendorPrice = Common.Common.cis_pharmacy_info.vendorPrice / Common.Common.cis_pharmacy_info.packY;
                    Common.Common.cis_pharmacy_info.vpActive = true;
                    dgvPurchaseReceipt.Rows[rowIndex].Cells["vendorPrice"].Value = Common.Common.cis_pharmacy_info.vendorPrice.ToString("#.##");
                }
                else if (Common.Common.cis_pharmacy_info.vendorPrice == 0)
                {
                    Common.Common.cis_pharmacy_info.vpActive = false;
                }
                else
                {
                    //Common.Common.cis_pharmacy_info.vendorPrice = 0;
                    //Common.Common.cis_pharmacy_info.vpActive = false;
                    //dgvPurchaseReceipt.Rows[rowIndex].Cells["vendorPrice"].Value = Common.Common.cis_pharmacy_info.vendorPrice.ToString("#.##");
                }

                Common.Common.cis_pharmacy_info.MRP = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["mrp"].Value));
                if (Common.Common.cis_pharmacy_info.mrpActive == false && Common.Common.cis_pharmacy_info.MRP != 0)
                {
                    Common.Common.cis_pharmacy_info.MRP = Common.Common.cis_pharmacy_info.MRP / Common.Common.cis_pharmacy_info.packY;
                    Common.Common.cis_pharmacy_info.mrpActive = true;
                    dgvPurchaseReceipt.Rows[rowIndex].Cells["mrp"].Value = Common.Common.cis_pharmacy_info.MRP.ToString("#.##");
                }
                else if (Common.Common.cis_pharmacy_info.MRP == 0)
                {
                    Common.Common.cis_pharmacy_info.mrpActive = false;
                }
                else
                {
                    //Common.Common.cis_pharmacy_info.MRP = 0;
                    //Common.Common.cis_pharmacy_info.mrpActive = false;
                    //dgvPurchaseReceipt.Rows[rowIndex].Cells["mrp"].Value = Common.Common.cis_pharmacy_info.MRP.ToString("#.##");
                }
            }

            Common.Common.cis_pharmacy_info.vendorPrice = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["vendorPrice"].Value));
            Common.Common.cis_pharmacy_info.MRP = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["mrp"].Value));

            if (Common.Common.cis_pharmacy_info.receiveQty != 0 && Common.Common.cis_pharmacy_info.vendorPrice != 0)
            {
                Common.Common.cis_pharmacy_info.totalAmtPur = Common.Common.cis_pharmacy_info.receiveQty * Common.Common.cis_pharmacy_info.vendorPrice;
                dgvPurchaseReceipt.Rows[rowIndex].Cells["total"].Value = Common.Common.cis_pharmacy_info.totalAmtPur.ToString("#.##");
            }
            else
            {
                Common.Common.cis_pharmacy_info.totalAmtPur = 0;
                dgvPurchaseReceipt.Rows[rowIndex].Cells["total"].Value = Common.Common.cis_pharmacy_info.totalAmtPur.ToString("#.##");
            }

            Common.Common.cis_pharmacy_info.discountPercPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["disPerc"].Value));
            if (Common.Common.cis_pharmacy_info.discountPercPur != 0 && Common.Common.cis_pharmacy_info.totalAmtPur != 0)
            {
                Common.Common.cis_pharmacy_info.discountAmtPur = Common.Common.cis_pharmacy_info.totalAmtPur * Common.Common.cis_pharmacy_info.discountPercPur / 100;
                dgvPurchaseReceipt.Rows[rowIndex].Cells["discountAmt"].Value = Common.Common.cis_pharmacy_info.discountAmtPur.ToString("#.##");
            }
            else
            {
                Common.Common.cis_pharmacy_info.discountAmtPur = 0;
                dgvPurchaseReceipt.Rows[rowIndex].Cells["discountAmt"].Value = Common.Common.cis_pharmacy_info.discountAmtPur.ToString("#.##");
            }

            Common.Common.cis_pharmacy_info.discountAmtPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["discountAmt"].Value));
            Common.Common.cis_pharmacy_info.taxPercPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["taxPerc"].Value));

            if (Common.Common.cis_pharmacy_info.taxPercPur != 0 && Common.Common.cis_pharmacy_info.totalAmtPur != 0)
            {
                if (cboTaxFormula.SelectedIndex == 1)
                {
                    Common.Common.cis_pharmacy_info.taxAmtPur = ((Common.Common.cis_pharmacy_info.receiveQty * Common.Common.cis_pharmacy_info.vendorPrice) - Common.Common.cis_pharmacy_info.discountAmtPur) * Common.Common.cis_pharmacy_info.taxPercPur / 100;
                    dgvPurchaseReceipt.Rows[rowIndex].Cells["taxAmt"].Value = Common.Common.cis_pharmacy_info.taxAmtPur.ToString("#.##");
                }
                else
                {
                    Common.Common.cis_pharmacy_info.taxAmtPur = ((Common.Common.cis_pharmacy_info.receiveQty * Common.Common.cis_pharmacy_info.MRP * 100 / (100 + Common.Common.cis_pharmacy_info.taxPercPur)) - Common.Common.cis_pharmacy_info.discountAmtPur) * Common.Common.cis_pharmacy_info.taxPercPur / 100;
                    dgvPurchaseReceipt.Rows[rowIndex].Cells["taxAmt"].Value = Common.Common.cis_pharmacy_info.taxAmtPur.ToString("#.##");
                }
            }
            else
            {
                Common.Common.cis_pharmacy_info.taxAmtPur = 0;
                dgvPurchaseReceipt.Rows[rowIndex].Cells["taxAmt"].Value = Common.Common.cis_pharmacy_info.taxAmtPur.ToString("#.##");
            }

            if (Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["cboCalFreeTax"].Value) == "Yes" && Common.Common.cis_pharmacy_info.offerQty != 0 && Common.Common.cis_pharmacy_info.taxPercPur != 0)
            {
                if (cboTaxFormula.SelectedIndex == 1)
                {
                    Common.Common.cis_pharmacy_info.freeTaxAmtPur = (Common.Common.cis_pharmacy_info.offerQty * Common.Common.cis_pharmacy_info.vendorPrice) * Common.Common.cis_pharmacy_info.taxPercPur / 100;
                    dgvPurchaseReceipt.Rows[rowIndex].Cells["freeTax"].Value = Common.Common.cis_pharmacy_info.freeTaxAmtPur.ToString("#.##");
                }
                else
                {
                    Common.Common.cis_pharmacy_info.freeTaxAmtPur = (Common.Common.cis_pharmacy_info.offerQty * Common.Common.cis_pharmacy_info.MRP * 100 / (100 + Common.Common.cis_pharmacy_info.taxPercPur)) * Common.Common.cis_pharmacy_info.taxPercPur / 100;
                    dgvPurchaseReceipt.Rows[rowIndex].Cells["freeTax"].Value = Common.Common.cis_pharmacy_info.freeTaxAmtPur.ToString("#.##");
                }
            }
            else
            {
                Common.Common.cis_pharmacy_info.freeTaxAmtPur = 0;
                dgvPurchaseReceipt.Rows[rowIndex].Cells["freeTax"].Value = Common.Common.cis_pharmacy_info.freeTaxAmtPur.ToString("#.##");
            }

            Common.Common.cis_pharmacy_info.taxAmtPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["taxAmt"].Value));
            Common.Common.cis_pharmacy_info.freeTaxAmtPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells["freeTax"].Value));

            Common.Common.cis_pharmacy_info.netAmtPur = Common.Common.cis_pharmacy_info.totalAmtPur - Common.Common.cis_pharmacy_info.discountAmtPur + Common.Common.cis_pharmacy_info.taxAmtPur + Common.Common.cis_pharmacy_info.freeTaxAmtPur;
            dgvPurchaseReceipt.Rows[rowIndex].Cells[18].Value = Common.Common.cis_pharmacy_info.netAmtPur.ToString("#.##");

            if (Common.Common.cis_pharmacy_info.netAmtPur != 0)
            {
                calculatePurSum();
            }
        }

        private void dgvPurchaseReceipt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPurchaseReceipt.Rows.Count > 1)
            {
                int rowIndex = dgvPurchaseReceipt.CurrentRow.Index;

                if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("cboItemName"))
                {
                    Common.Common.cis_pharmacy_info.phaItemName = dgvPurchaseReceipt.Rows[rowIndex].Cells["cboItemName"].Value.ToString();
                    dtSource = objBusinessFacade.getPhaDetailsForPurchase(Common.Common.cis_pharmacy_info.phaItemName);
                    if (dtSource.Rows.Count > 0)
                    {
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["itemType"].Value = dtSource.Rows[0]["item_type"].ToString();
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["item_id"].Value = dtSource.Rows[0]["item_id"].ToString();
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["item_type_id"].Value = dtSource.Rows[0]["item_type_id"].ToString();
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["taxPerc"].Value = dtSource.Rows[0]["tax_rate"].ToString();
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["packY"].Value = dtSource.Rows[0]["pack_y"].ToString();
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["taxFormula"].Value = Convert.ToInt32(cboTaxFormula.SelectedIndex);
                    }

                    else
                    {
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["itemType"].Value = string.Empty;
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["item_id"].Value = string.Empty;
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["item_type_id"].Value = string.Empty;
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["taxPerc"].Value = string.Empty;
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["packY"].Value = string.Empty;
                        dgvPurchaseReceipt.Rows[rowIndex].Cells["item_id"].Value = string.Empty;
                    }
                }
                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("expDate") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["expDate"].Value as string)))//Exp Date 
                {
                    string exp_str = Convert.ToString(dgvPurchaseReceipt.Rows[rowIndex].Cells[4].Value);

                    DateTime dt;
                    if (DateTime.TryParse(exp_str, out dt))
                    {
                        if (Convert.ToDateTime(dt.ToString("MM/yyyy")) <= Convert.ToDateTime(DateTime.Now.ToString("MM/yyyy")))
                        {
                            MessageBox.Show("Expiry Date should be in Future Date....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvPurchaseReceipt.Rows[rowIndex].Cells[4].Value = DBNull.Value;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Expiry Date Format....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvPurchaseReceipt.Rows[rowIndex].Cells[4].Value = DBNull.Value;
                    }
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("packX") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["packX"].Value as string)))//Pack X
                {
                    calculatePurchase();
                }
                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("packY") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["packY"].Value as string)))//Pack Y
                {
                    calculatePurchase();
                }
                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("receiveQty") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["receiveQty"].Value as string)))//Receive Qty
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("offerQty") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["offerQty"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("vendorPrice") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["vendorPrice"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("mrp") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["mrp"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("total") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["total"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("disPerc") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["disPerc"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("discountAmt") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["discountAmt"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("taxPerc") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["taxPerc"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("taxAmt") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["taxAmt"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("cboCalFreeTax") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["cboCalFreeTax"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("taxAmt") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["taxAmt"].Value as string)))
                {
                    calculatePurchase();
                }

                else if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("freeTax") && (!string.IsNullOrEmpty(dgvPurchaseReceipt.Rows[rowIndex].Cells["freeTax"].Value as string)))
                {
                    calculatePurchase();
                }
            }
        }

        private void loadPhaItem()
        {
            try
            {
                dtSource = objBusinessFacade.loadPhaItem();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    //cboItemName.a
                    //DataRow row = dtSource.NewRow();
                    //row[0] = -1;
                    //row[1] = "";
                    //dtSource.Rows.InsertAt(row, 0);
                    //cboItemName
                   // cboItemName.ValueMember = "item_id";
                    //cboItemName.DisplayMember = "item_name";
                    //cboItemName.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void CISPurchaseReceipt_Load(object sender, EventArgs e)
        {
            this.dtpInvoiceDate.Value = DateTime.Now;
            this.dtpReceiveDate.Value = DateTime.Now;
            loadVendor();
            loadPhaItem();
        }

        private void dgvPurchaseReceipt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPurchaseReceipt.Columns[e.ColumnIndex].Name.Equals("del"))//Delete Button Click
            {
                int rowIndex = dgvPurchaseReceipt.CurrentRow.Index;
                
                if (Convert.ToInt32(dgvPurchaseReceipt.Rows[rowIndex].Cells[1].Value) != 0)
                {
                    if (MessageBox.Show("Are you sure to Delete the record?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvPurchaseReceipt.Rows.Remove(dgvPurchaseReceipt.Rows[e.RowIndex]);
                        calculatePurSum();
                    }
                }
            }
        }

        private void calculatePurSum()
        {
            decimal totalAmtSum = 0;
            decimal discountSum = 0;
            decimal taxSum = 0;
            decimal freeTaxSum = 0;
            decimal netTotalSum = 0;

            foreach (DataGridViewRow row in dgvPurchaseReceipt.Rows) //Calculate Amount Columns
            {
                totalAmtSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["total"].Value));
                Common.Common.cis_pharmacy_info.totalAmtPurSum = Common.Common.cis_pharmacy_info.totalAmtPurSum + totalAmtSum;
                discountSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["discountAmt"].Value));
                Common.Common.cis_pharmacy_info.discountPurSum = Common.Common.cis_pharmacy_info.discountPurSum + discountSum;
                taxSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["taxAmt"].Value));
                Common.Common.cis_pharmacy_info.taxPurSum = Common.Common.cis_pharmacy_info.taxPurSum + taxSum;
                freeTaxSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["freeTax"].Value));
                Common.Common.cis_pharmacy_info.freeTaxPurSum = Common.Common.cis_pharmacy_info.freeTaxPurSum + freeTaxSum;
                netTotalSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["netAmt"].Value));
                Common.Common.cis_pharmacy_info.netTotalPurSum = Common.Common.cis_pharmacy_info.netTotalPurSum + netTotalSum;
            }

            lblTotalAmoutPur.Text = Common.Common.cis_pharmacy_info.totalAmtPurSum.ToString("0.00"); //Display Total Amount
            lblDiscountAmtPur.Text = Common.Common.cis_pharmacy_info.discountPurSum.ToString("0.00"); //Display Total Discount
            lblTotalTaxPur.Text = (Common.Common.cis_pharmacy_info.taxPurSum + Common.Common.cis_pharmacy_info.freeTaxPurSum).ToString("0.00"); //Display Total Tax
            //lblNetValuePur.Text = Common.Common.cis_pharmacy_info.netTotalPurSum.ToString("0.00"); //Display Net Total
            lblNetValuePur.Text = Math.Round(Common.Common.cis_pharmacy_info.netTotalPurSum).ToString("0.00"); //Display Net Total
            lblBalanceAmtPur.Text = Math.Round(Common.Common.cis_pharmacy_info.netTotalPurSum, 0).ToString("0.00"); //Display Balance Amt

            Common.Common.cis_pharmacy_info.totalAmtPurSum = 0;
            Common.Common.cis_pharmacy_info.discountPurSum = 0;
            Common.Common.cis_pharmacy_info.taxPurSum = 0;
            Common.Common.cis_pharmacy_info.freeTaxPurSum = 0;
            Common.Common.cis_pharmacy_info.netTotalPurSum = 0;
        }

        private void calculatePurGrandSum()
        {
            lblNetValuePur.Text = (objBusinessFacade.NonBlankValueOfDecimal(lblTotalAmoutPur.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtDiscountValuePur.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtReturnAmtPur.Text.ToString())).ToString("0.00");
            lblBalanceAmtPur.Text = Math.Round((objBusinessFacade.NonBlankValueOfDecimal(lblTotalAmoutPur.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtDiscountValuePur.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtReturnAmtPur.Text.ToString())), 0).ToString("0.00");
        }

        private void txtDiscountValuePur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtReturnAmtPur.Select();
                e.Handled = true;

                if (objBusinessFacade.NonBlankValueOfDecimal(txtDiscountValuePur.Text.ToString()) <= (objBusinessFacade.NonBlankValueOfDecimal(lblTotalAmoutPur.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(lblDiscountAmtPur.Text.ToString())))
                {
                    calculatePurGrandSum();
                }
                else
                {
                    MessageBox.Show("Discount Amount can't be greater than Total Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiscountValuePur.Text = "0.00";
                    txtDiscountValuePur.Select();
                }
            }
        }

        private void txtReturnAmtPur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAmountPaidPur.Select();
                e.Handled = true;

                calculatePurGrandSum();
            }
        }

        private void txtAmountPaidPur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
                e.Handled = true;

                if (objBusinessFacade.NonBlankValueOfDecimal(txtAmountPaidPur.Text.ToString()) <= objBusinessFacade.NonBlankValueOfDecimal(lblNetValuePur.Text.ToString()))
                {
                    lblBalanceAmtPur.Text = Math.Round((objBusinessFacade.NonBlankValueOfDecimal(lblNetValuePur.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtAmountPaidPur.Text.ToString())), 0).ToString("0.00");
                }
                else
                {
                    MessageBox.Show("Amount Paid can't be greater than Balance Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAmountPaidPur.Text = "0.00";
                    lblBalanceAmtPur.Text = Math.Round((objBusinessFacade.NonBlankValueOfDecimal(lblNetValuePur.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtAmountPaidPur.Text.ToString())), 0).ToString("0.00");
                    txtAmountPaidPur.Select();
                }
            }
        }

        private void loadVendor()
        {
            try
            {
                dtSource = objBusinessFacade.loadVendor();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
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

        private void cboVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVendor.Items.Count > 1 && cboVendor.SelectedIndex > 0)
            {
                Common.Common.cis_pharmacy_info.vendorId = Convert.ToInt32(cboVendor.SelectedValue.ToString());

                try
                {
                    dtSource = objBusinessFacade.getVendorTinNo(Common.Common.cis_pharmacy_info.vendorId);
                    if (dtSource.Rows.Count > 0)
                    {
                        lblTinNo.Text = Convert.ToString(dtSource.Rows[0]["tin_number"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            else
            {
                lblTinNo.Text = string.Empty;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPurchaseReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            int rowIndex = dgvPurchaseReceipt.CurrentRow.Index;

            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Right)
            {
                /*if (dgvPurchaseReceipt.CurrentCell.ColumnIndex == dgvPurchaseReceipt.ColumnCount - 4)
                {
                    dgvPurchaseReceipt.CurrentCell = dgvPurchaseReceipt.Rows[dgvPurchaseReceipt.CurrentCell.RowIndex + 1].Cells[dgvPurchaseReceipt.CurrentCell.ColumnIndex + 2];
                }*/
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to Save the record?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dgvPurchaseReceipt.Rows.Count > 1)
                {
                    if (!string.IsNullOrEmpty(txtInvoiceNo.Text) && Convert.ToInt32(cboVendor.SelectedIndex) > 0)
                    {
                        ComArugments objArg = objBusinessFacade.getPurchaseReceiptNumberFormat();
                        Common.Common.cis_number_generation.running_purchase_receipt_number = Convert.ToInt32(objArg.ParamList["running_purchase_receipt_number"]);
                        Common.Common.cis_number_generation.purchase_receipt_number = Convert.ToString(objArg.ParamList["purchase_receipt_number"]);

                        Common.Common.cis_pharmacy_info.invoiceNumber = Convert.ToString(txtInvoiceNo.Text.ToString());
                        Common.Common.cis_pharmacy_info.invoiceDate = Convert.ToDateTime(dtpInvoiceDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        Common.Common.cis_pharmacy_info.receiveDate = Convert.ToDateTime(dtpReceiveDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        Common.Common.cis_pharmacy_info.vendorId = Convert.ToInt32(cboVendor.SelectedValue);

                        Common.Common.cis_pharmacy_info.totalAmtPurSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblTotalAmoutPur.Text)); // Total Amount
                        Common.Common.cis_pharmacy_info.discountCashPurSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(txtDiscountValuePur.Text)); // Total Cash Discount
                        Common.Common.cis_pharmacy_info.discountItemPurSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblDiscountAmtPur.Text)); // Total Item Discount
                        Common.Common.cis_pharmacy_info.taxPurSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblTotalTaxPur.Text)); // Total Tax
                        Common.Common.cis_pharmacy_info.retunedAmtPurSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(txtReturnAmtPur.Text)); // Total return amount
                        Common.Common.cis_pharmacy_info.AmtPaidPurSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(txtAmountPaidPur.Text)); // Total amount Paid
                        Common.Common.cis_pharmacy_info.balancePurSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblBalanceAmtPur.Text)); // Total Balance Paid
                        Common.Common.cis_pharmacy_info.netTotalPurSum = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(lblNetValuePur.Text)); // Net Total
                        Common.Common.cis_pharmacy_info.roundOffPurSum = Common.Common.cis_pharmacy_info.netTotalPurSum - Math.Round(Common.Common.cis_pharmacy_info.netTotalPurSum, 0); // Total Round Off

                        objArg.ParamList["running_purchase_receipt_number"] = Common.Common.cis_number_generation.running_purchase_receipt_number;
                        objArg.ParamList["purchase_receipt_number"] = Common.Common.cis_number_generation.purchase_receipt_number;
                        objArg.ParamList["pur_invoice_number"] = Common.Common.cis_pharmacy_info.invoiceNumber;
                        objArg.ParamList["pur_invoice_date"] = Common.Common.cis_pharmacy_info.invoiceDate;
                        objArg.ParamList["pur_receive_date"] = Common.Common.cis_pharmacy_info.receiveDate;
                        objArg.ParamList["vendor_id"] = Common.Common.cis_pharmacy_info.vendorId;

                        objArg.ParamList["invoice_total_amount"] = Common.Common.cis_pharmacy_info.totalAmtPurSum;
                        objArg.ParamList["cash_discount"] = Common.Common.cis_pharmacy_info.discountCashPurSum;
                        objArg.ParamList["item_discount"] = Common.Common.cis_pharmacy_info.discountItemPurSum;
                        objArg.ParamList["tax_amount"] = Common.Common.cis_pharmacy_info.taxPurSum;
                        objArg.ParamList["returned_amount"] = Common.Common.cis_pharmacy_info.retunedAmtPurSum;
                        objArg.ParamList["amount_paid"] = Common.Common.cis_pharmacy_info.AmtPaidPurSum;
                        objArg.ParamList["balance_amount"] = Common.Common.cis_pharmacy_info.balancePurSum;
                        objArg.ParamList["net_invoice_amount"] = Common.Common.cis_pharmacy_info.netTotalPurSum;
                        objArg.ParamList["round_off"] = Common.Common.cis_pharmacy_info.roundOffPurSum;

                        Common.Common.flag = objBusinessFacade.insertPurchaseReceipt(objArg);
                        Common.Common.billId = objBusinessFacade.lastInsertRecord();
                        Common.Common.flag = objBusinessFacade.updatePurRunningNumber(objArg);

                        foreach (DataGridViewRow row in dgvPurchaseReceipt.Rows)
                        {
                            int rowIndex = dgvPurchaseReceipt.CurrentRow.Index;

                            if (!(string.IsNullOrEmpty(Convert.ToString(row.Cells["item_id"].Value))) && !(string.IsNullOrEmpty(Convert.ToString(row.Cells["netAmt"].Value))) && objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["netAmt"].Value)) != 0)
                            {
                                Common.Common.cis_pharmacy_info.phaItemId = Convert.ToInt32(row.Cells["item_id"].Value);
                                Common.Common.cis_pharmacy_info.lotId = Convert.ToString(row.Cells["batchNo"].Value);
                                Common.Common.cis_pharmacy_info.purExpDate = new DateTime(Convert.ToDateTime(row.Cells["expDate"].Value).Year, Convert.ToDateTime(row.Cells["expDate"].Value).Month, DateTime.DaysInMonth(Convert.ToDateTime(row.Cells["expDate"].Value).Year, Convert.ToDateTime(row.Cells["expDate"].Value).Month));
                                Common.Common.cis_pharmacy_info.packX = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["packX"].Value));
                                Common.Common.cis_pharmacy_info.packY = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["packY"].Value));
                                Common.Common.cis_pharmacy_info.receiveQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["receiveQty"].Value));
                                Common.Common.cis_pharmacy_info.offerQty = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["offerQty"].Value));
                                Common.Common.cis_pharmacy_info.vendorPrice = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["vendorPrice"].Value));
                                Common.Common.cis_pharmacy_info.MRP = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["mrp"].Value));
                                Common.Common.cis_pharmacy_info.totalAmtPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["total"].Value));
                                Common.Common.cis_pharmacy_info.discountPercPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["disPerc"].Value));
                                Common.Common.cis_pharmacy_info.discountAmtPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["discountAmt"].Value));
                                Common.Common.cis_pharmacy_info.taxPercPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["taxPerc"].Value));
                                Common.Common.cis_pharmacy_info.taxAmtPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["taxAmt"].Value));
                                if (Convert.ToString(row.Cells["cboCalFreeTax"].Value) == "Yes")
                                {
                                    Common.Common.cis_pharmacy_info.cboFreeTaxStatus = 1;
                                }
                                else
                                {
                                    Common.Common.cis_pharmacy_info.cboFreeTaxStatus = 0;
                                }
                                Common.Common.cis_pharmacy_info.freeTaxAmtPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["freeTax"].Value));
                                Common.Common.cis_pharmacy_info.netAmtPur = objBusinessFacade.NonBlankValueOfDecimal(Convert.ToString(row.Cells["netAmt"].Value));
                                Common.Common.cis_pharmacy_info.phaItemTypeId = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["item_type_id"].Value));
                                Common.Common.cis_pharmacy_info.taxFormula = objBusinessFacade.NonBlankValueOfInt(Convert.ToString(row.Cells["taxFormula"].Value));

                                objArg.ParamList["pur_invoice_id"] = Common.Common.billId;
                                objArg.ParamList["item_id"] = Common.Common.cis_pharmacy_info.phaItemId;
                                objArg.ParamList["batch_no"] = Common.Common.cis_pharmacy_info.lotId;
                                objArg.ParamList["expiry_date"] = Common.Common.cis_pharmacy_info.purExpDate;
                                objArg.ParamList["pack_size_x"] = Common.Common.cis_pharmacy_info.packX;
                                objArg.ParamList["pack_size_y"] = Common.Common.cis_pharmacy_info.packY;
                                objArg.ParamList["received_qty"] = Common.Common.cis_pharmacy_info.receiveQty;
                                objArg.ParamList["offer_qty"] = Common.Common.cis_pharmacy_info.offerQty;
                                objArg.ParamList["vendor_price"] = Common.Common.cis_pharmacy_info.vendorPrice;
                                objArg.ParamList["mrp"] = Common.Common.cis_pharmacy_info.MRP;
                                objArg.ParamList["total_amount"] = Common.Common.cis_pharmacy_info.totalAmtPur;
                                objArg.ParamList["discount_perc"] = Common.Common.cis_pharmacy_info.discountPercPur;
                                objArg.ParamList["discount_amount"] = Common.Common.cis_pharmacy_info.discountAmtPur;
                                objArg.ParamList["tax_formula"] = Common.Common.cis_pharmacy_info.taxFormula;
                                objArg.ParamList["tax_perc"] = Common.Common.cis_pharmacy_info.taxPercPur;
                                objArg.ParamList["tax_amount"] = Common.Common.cis_pharmacy_info.taxAmtPur;
                                objArg.ParamList["is_free_tax"] = Common.Common.cis_pharmacy_info.cboFreeTaxStatus;
                                objArg.ParamList["free_tax_amount"] = Common.Common.cis_pharmacy_info.freeTaxAmtPur;
                                objArg.ParamList["net_total_amount"] = Common.Common.cis_pharmacy_info.netAmtPur;
                                objArg.ParamList["item_type_id"] = Common.Common.cis_pharmacy_info.phaItemTypeId;

                                Common.Common.flag = objBusinessFacade.insertPurchaseReceiptDetails(objArg);

                                Common.Common.flag = objBusinessFacade.insertPurchaseStock(objArg);
                                Common.Common.trans_id = objBusinessFacade.lastInsertRecord();
                                objArg.ParamList["inv_trans_id"] = Common.Common.trans_id;
                                Common.Common.flag = objBusinessFacade.insertPurchaseStockDetails(objArg);
                            }
                        }
                        if (Common.Common.flag == 1)
                        {
                            tslGRNNo.Text = Common.Common.cis_number_generation.purchase_receipt_number.ToString();
                            btnSave.Enabled = false;
                            MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mandatory Fields are required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearPurchaseReceipt();
        }

        private void clearPurchaseReceipt()
        {
            dgvPurchaseReceipt.Rows.Clear();
            dgvPurchaseReceipt.DataSource = null;
            txtInvoiceNo.Text = string.Empty;
            this.dtpInvoiceDate.Value = DateTime.Now;
            this.dtpReceiveDate.Value = DateTime.Now;
            cboTaxFormula.SelectedIndex = 1;
            cboVendor.SelectedIndex = -1;
            tslGRNNo.Text = string.Empty;
            lblTinNo.Text = string.Empty;
            lblTotalAmoutPur.Text = "0.00";
            txtDiscountValuePur.Text = "0.00";
            lblTotalTaxPur.Text = "0.00";
            txtReturnAmtPur.Text = "0.00";
            txtAmountPaidPur.Text = "0.00";
            lblBalanceAmtPur.Text = "0.00";
            lblNetValuePur.Text = "0.00";
            btnSave.Enabled = true;
        }
    }
}
