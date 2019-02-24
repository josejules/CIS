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
    public partial class cisWardBill : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cisWardBill()
        {
            InitializeComponent();
            rbtnFinalWardBill.Checked = true;
        }

        private void txtPatientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dtSource = objBusinessFacade.getPatientDetailsByPatientId(txtPatientId.Text.ToString());
                    if (dtSource.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtSource.Rows[0]["bed_number"].ToString()))
                        {
                            lblPatientName.Text = dtSource.Rows[0]["patient_name"].ToString();
                            lblGender.Text = dtSource.Rows[0]["gender_name"].ToString();
                            lblAge.Text = string.Concat(dtSource.Rows[0]["age_year"].ToString(), "Y ", dtSource.Rows[0]["age_month"].ToString(), "M ", dtSource.Rows[0]["age_day"].ToString(), "D");
                            lblVisitNo.Text = dtSource.Rows[0]["last_visit_number"].ToString();
                            lblVisitDate.Text = dtSource.Rows[0]["visit_date"].ToString();
                            lblBedDetails.Text = string.Concat(dtSource.Rows[0]["ward_name"].ToString(), ", ", dtSource.Rows[0]["room_no"].ToString(), ", ", dtSource.Rows[0]["bed_number"].ToString());
                            this.dtpFromDate.Value = Convert.ToDateTime(dtSource.Rows[0]["visit_date1"].ToString());
                            lblPatientType.Text = dtSource.Rows[0]["patient_type"].ToString();
                            multitxtAddress.Text = dtSource.Rows[0]["address"].ToString();

                            dtSource = objBusinessFacade.getFinalBillStatusbyPatientId(txtPatientId.Text.ToString(), lblVisitNo.Text.ToString());

                            if (dtSource.Rows.Count > 0)//Check if Final/Discharge bill is prepared already for this patient by Visit
                            {
                                if (MessageBox.Show("Final/Discharge Bill is prepared already. Do you want to prepare Intermediate Bill?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    rbtnIntermediateBill.Checked = true;
                                    rbtnFinalWardBill.Checked = false;
                                    rbtnFinalWardBill.Enabled = false;
                                    dtSource = objBusinessFacade.getNetAdvAvailbyPatientId(txtPatientId.Text.ToString(), lblVisitNo.Text.ToString());

                                    if (dtSource.Rows.Count > 0)
                                    {
                                        lblNetAvailAdv.Text = dtSource.Rows[0]["net_amount"].ToString();
                                    }
                                    else
                                    {
                                        lblNetAvailAdv.Text = "0.00";
                                    }
                                }
                                else
                                {
                                    clearWardRecords();
                                }
                            }
                            else
                            {
                                dtSource = objBusinessFacade.getNetAdvAvailbyPatientId(txtPatientId.Text.ToString(), lblVisitNo.Text.ToString());

                                if (dtSource.Rows.Count > 0)
                                {
                                    lblNetAvailAdv.Text = dtSource.Rows[0]["net_amount"].ToString();
                                }
                                else
                                {
                                    lblNetAvailAdv.Text = "0.00";
                                }
                            }
                        }

                        else
                        {
                            MessageBox.Show("Patient is not admitted....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clearWardBillDetails();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Patient Id....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clearWardBillDetails();
                    }
                    e.Handled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ward Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                }
            }
        }

        private void clearWardBillDetails()
        {
            txtPatientId.Text = string.Empty;
            lblPatientName.Text = string.Empty;
            lblVisitNo.Text = string.Empty;
            lblVisitDate.Text = string.Empty;
            lblGender.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblBedDetails.Text = string.Empty;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblVisitNo.Text.ToString()))
            {
                Common.Common.cis_number_generation.patient_id = Convert.ToString(txtPatientId.Text.ToString());
                Common.Common.cis_number_generation.visit_number = Convert.ToString(lblVisitNo.Text.ToString());
                Common.Common.cis_patient_info.patient_name = Convert.ToString(lblPatientName.Text.ToString());

                ComArugments objArgN = objBusinessFacade.getWardNumberFormat();

                Common.Common.cis_number_generation.running_ward_bill_number = Convert.ToInt32(objArgN.ParamList["running_ward_bill_number"]);
                Common.Common.cis_number_generation.ward_bill_number = Convert.ToString(objArgN.ParamList["ward_bill_number"]);

                objArg.ParamList["patient_id"] = Common.Common.cis_number_generation.patient_id;
                objArg.ParamList["visit_number"] = Common.Common.cis_number_generation.visit_number;
                objArg.ParamList["patient_name"] = Common.Common.cis_patient_info.patient_name;

                //Begin Save Ward Bill Detailst
                if (dgvWardBill.Rows.Count > 0)
                {
                    Common.Common.cis_billing.totalWardAmt = objBusinessFacade.NonBlankValueOfDecimal(lblWardTotalNetAmt.Text.ToString());
                    Common.Common.cis_billing.discountWardAmt = objBusinessFacade.NonBlankValueOfDecimal(lblDiscount.Text.ToString());
                    Common.Common.cis_billing.discountPercOrAmtWard = objBusinessFacade.NonBlankValueOfDecimal(txtDiscount.Text.ToString());
                    Common.Common.cis_billing.discountType = Convert.ToInt32(cboDiscountType.SelectedIndex.ToString());
                    Common.Common.cis_billing.netlWardAmt = Common.Common.cis_billing.totalWardAmt - Common.Common.cis_billing.discountWardAmt;
                    Common.Common.cis_billing.amtPaidWardAmt = objBusinessFacade.NonBlankValueOfDecimal(txtAmtPaid.Text.ToString());
                    Common.Common.cis_billing.dueWardAmt = objBusinessFacade.NonBlankValueOfDecimal(lblDueAmt.Text.ToString());
                    Common.Common.cis_billing.totalAdvanceAvailable = objBusinessFacade.NonBlankValueOfDecimal(lblNetAvailAdv.Text.ToString());
                    Common.Common.cis_billing.advanceAdjustment = objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString());

                    Common.Common.cis_billing.totalAdvNetColl = Common.Common.cis_billing.totalAdvanceAvailable - Common.Common.cis_billing.advanceAdjustment;

                    if (Common.Common.cis_billing.dueWardAmt == 0)
                    {
                        Common.Common.cis_billing.wardPaymentStatus = 1;//Full Payment
                    }

                    else if (Common.Common.cis_billing.netlWardAmt == Common.Common.cis_billing.dueWardAmt)
                    {
                        Common.Common.cis_billing.wardPaymentStatus = 2;//Not Paid
                    }

                    else
                    {
                        Common.Common.cis_billing.wardPaymentStatus = 3;//Partially Paid
                    }

                    Common.Common.paymentModeId = Convert.ToInt32(cboPaymentMode.SelectedIndex.ToString());
                    Common.Common.cardNumber = txtCardNumber.Text.ToString();
                    Common.Common.bankName = txtBankName.Text.ToString();
                    Common.Common.holderName = txtHolderName.Text.ToString();

                    objArg.ParamList["running_ward_bill_number"] = Common.Common.cis_number_generation.running_ward_bill_number;
                    objArg.ParamList["ward_bill_number"] = Common.Common.cis_number_generation.ward_bill_number;

                    objArg.ParamList["ward_bill_amount"] = Common.Common.cis_billing.totalWardAmt;
                    objArg.ParamList["ward_discountPercOrAmt"] = Common.Common.cis_billing.discountPercOrAmtWard;
                    objArg.ParamList["ward_discount_type"] = Common.Common.cis_billing.discountType;
                    objArg.ParamList["ward_discount"] = Common.Common.cis_billing.discountWardAmt;
                    objArg.ParamList["ward_total_amount"] = Common.Common.cis_billing.netlWardAmt;
                    objArg.ParamList["ward_amount_paid"] = Common.Common.cis_billing.amtPaidWardAmt;
                    objArg.ParamList["ward_Adv_Avail"] = Common.Common.cis_billing.totalAdvanceAvailable;
                    objArg.ParamList["ward_Adv_Adj"] = Common.Common.cis_billing.advanceAdjustment;
                    objArg.ParamList["ward_Adv_Net_Coll"] = Common.Common.cis_billing.totalAdvNetColl;
                    objArg.ParamList["ward_due"] = Common.Common.cis_billing.dueWardAmt;
                    objArg.ParamList["ward_status"] = Common.Common.cis_billing.wardPaymentStatus;
                    objArg.ParamList["from_date"] = Convert.ToDateTime(dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    objArg.ParamList["to_date"] = Convert.ToDateTime(dtpToDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                    if (rbtnIntermediateBill.Checked == true)
                    {
                        objArg.ParamList["bill_type"] = 2;//Final Bill
                    }

                    else
                    {
                        objArg.ParamList["bill_type"] = 1;//Intermediate Bill
                    }

                    //Begin Payment Mode Details

                    objArg.ParamList["transaction_user_id"] = Common.Common.userId;
                    objArg.ParamList["payment_mode_id"] = Common.Common.paymentModeId;
                    objArg.ParamList["card_number"] = Common.Common.cardNumber;
                    objArg.ParamList["bank_name"] = Common.Common.bankName;
                    objArg.ParamList["holder_name"] = Common.Common.holderName;
                    //End Payment Mode Details

                    Common.Common.flag = objBusinessFacade.insertWardBill(objArg);
                    Common.Common.billId = objBusinessFacade.lastInsertRecord();

                    foreach (DataGridViewRow row in dgvWardBill.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if (Convert.ToInt32(row.Cells["wardAccountId"].Value) != 0 && !(string.IsNullOrEmpty(row.Cells["wardAccountId"].ToString())))
                            {
                                Common.Common.cis_billing.accountHeadId = Convert.ToInt32(row.Cells["wardAccountId"].Value);
                                Common.Common.cis_billing.accountName = Convert.ToString(row.Cells["wardAccountName"].Value);
                                Common.Common.cis_billing.accountGroupId = Convert.ToInt32(row.Cells["accountGroupId"].Value);
                                Common.Common.cis_billing.accountGroupName = Convert.ToString(row.Cells["wardCategory"].Value);
                                Common.Common.cis_billing.qtyWard = Convert.ToInt32(row.Cells["wardQty"].Value);
                                Common.Common.cis_billing.wardUnitPrice = Convert.ToDecimal(row.Cells["wardUnitPrice"].Value);
                                Common.Common.cis_billing.wardTotal = Convert.ToDecimal(row.Cells["wardTotalAmt"].Value);

                                objArg.ParamList["ward_bill_id"] = Common.Common.billId;
                                objArg.ParamList["ward_account_head_id"] = Common.Common.cis_billing.accountHeadId;
                                objArg.ParamList["ward_account_name"] = Common.Common.cis_billing.accountName;
                                objArg.ParamList["ward_account_group_id"] = Common.Common.cis_billing.accountGroupId;
                                objArg.ParamList["ward_account_group_name"] = Common.Common.cis_billing.accountGroupName;
                                objArg.ParamList["ward_qty"] = Common.Common.cis_billing.qtyWard;
                                objArg.ParamList["ward_unit_price"] = Common.Common.cis_billing.wardUnitPrice;
                                objArg.ParamList["ward_amount"] = Common.Common.cis_billing.wardTotal;

                                Common.Common.flag = objBusinessFacade.insertWardBillDetails(objArg);

                            }
                        }
                    }

                    Common.Common.flag = objBusinessFacade.insertWardBillDetailsSummary(objArg);
                    Common.Common.flag = objBusinessFacade.insertWardAdvAdjTrans(objArg);
                    Common.Common.flag = objBusinessFacade.updateWardRunningNumber(objArg);

                    /*if (objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString()) > 0)
                    {
                        
                    }*/

                    if (Common.Common.flag == 0)
                    {
                        MessageBox.Show("Ward Detail Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    /*else
                    {
                        MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                    }*/
                }
                else
                {
                    MessageBox.Show("Billing details are empty....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //End Save Ward Bill Details

                if (Common.Common.flag == 0)
                {
                    MessageBox.Show("Record is not Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblWardBillNo.Text = Common.Common.cis_number_generation.ward_bill_number;
                    MessageBox.Show("Record Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    Common.Common.flag = 0;
                }
            }

            else
            {
                MessageBox.Show("Patient Id is required....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cisWardBill_Load(object sender, EventArgs e)
        {
            this.dtpToDate.Value = DateTime.Now;
            lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            cboPaymentMode.SelectedIndex = 0;
            cboDiscountType.SelectedIndex = 0;
            loadWardCharges();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.Common.paymentModeId = Convert.ToInt32(cboPaymentMode.SelectedIndex.ToString());

            if (Common.Common.paymentModeId == 0)
            {
                txtCardNumber.Enabled = false;
                txtBankName.Enabled = false;
                txtHolderName.Enabled = false;
                txtCardNumber.Text = string.Empty;
                txtBankName.Text = string.Empty;
                txtHolderName.Text = string.Empty;
            }
            else
            {
                if (Common.Common.paymentModeId == 1 || Common.Common.paymentModeId == 2)
                {
                    lblCardNumber.Text = "Card No";
                }
                else if (Common.Common.paymentModeId == 3)
                {
                    lblCardNumber.Text = "Cheque No";
                }
                else
                {
                    lblCardNumber.Text = "DD No";
                }

                txtCardNumber.Enabled = true;
                txtBankName.Enabled = true;
                txtHolderName.Enabled = true;
            }
        }

        private void cboWardAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWardAccountName.Items.Count > 1 && cboWardAccountName.SelectedIndex > 0)
            {
                Common.Common.cis_billing.accountHeadId = Convert.ToInt32(cboWardAccountName.SelectedValue.ToString());

                try
                {
                    dtSource = objBusinessFacade.getBillChargesById(Common.Common.cis_billing.accountHeadId);
                    if (dtSource.Rows.Count > 0)
                    {
                        Common.Common.cis_billing.accountGroupName = Convert.ToString(dtSource.Rows[0]["account_head_name"].ToString());
                        Common.Common.cis_billing.accountGroupId = Convert.ToInt32(dtSource.Rows[0]["account_group_id"].ToString());

                        txtUnitpriceWard.Text = Convert.ToString(dtSource.Rows[0]["unit_price"].ToString());
                        lblAccountGroup.Text = Common.Common.cis_billing.accountGroupName;
                        lblAccountGroupId.Text = Common.Common.cis_billing.accountGroupId.ToString();

                        txtWardQty.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Common.Common.ExceptionHandler.ExceptionWriter(ex);
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void loadWardCharges()
        {
            try
            {
                dtSource = objBusinessFacade.loadWardCharges();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboWardAccountName.ValueMember = "id_cis_account_head";
                    cboWardAccountName.DisplayMember = "account_head_name";
                    cboWardAccountName.DataSource = dtSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void txtWardQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(Common.Common.cis_billing.accountGroupName) && Common.Common.cis_billing.accountHeadId > 0)
                {
                    Common.Common.cis_billing.accountName = cboWardAccountName.Text.ToString();
                    Common.Common.cis_billing.qtyGen = Convert.ToInt32(txtWardQty.Text.ToString());
                    Common.Common.cis_billing.genUnitPrice = Convert.ToDecimal(txtUnitpriceWard.Text.ToString());
                    Common.Common.cis_billing.accountGroupId = Convert.ToInt32(lblAccountGroupId.Text.ToString());
                    Common.Common.cis_billing.accountGroupName = lblAccountGroup.Text.ToString();
                    Common.Common.cis_billing.genTotal = (Common.Common.cis_billing.qtyGen * Common.Common.cis_billing.genUnitPrice);
                    lblWardTotalAmt.Text = Convert.ToString(Common.Common.cis_billing.genTotal);

                    if (Common.Common.cis_billing.qtyGen > 0)
                    {
                        if (string.IsNullOrEmpty(lblCheckEditModeWard.Text.ToString()))//Add Item
                        {
                            bool entryFound = false;
                            foreach (DataGridViewRow row in dgvWardBill.Rows)//Check Item exits aleady
                            {
                                int wardId = Convert.ToInt32(row.Cells["wardAccountId"].Value);
                                if (wardId == Common.Common.cis_billing.accountHeadId)
                                {
                                    MessageBox.Show("Already Exists....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    entryFound = true;
                                    cboWardAccountName.Focus();
                                }
                            }
                            if (!entryFound)//If not add the item
                            {
                                dgvWardBill.Rows.Add(dgvWardBill.Rows.Count + 1, Common.Common.cis_billing.accountHeadId, Common.Common.cis_billing.accountName, Common.Common.cis_billing.accountGroupId, Common.Common.cis_billing.accountGroupName, Common.Common.cis_billing.qtyGen, Common.Common.cis_billing.genUnitPrice, Common.Common.cis_billing.genTotal);
                                clearWardInputValues();
                                cboWardAccountName.Focus();
                                calculateWardSum();
                            }
                        }

                        else //Edit Item
                        {
                            int rowNo = Convert.ToInt32(lblCheckEditModeWard.Text.ToString());
                            dgvWardBill.Rows[rowNo].Cells["wardAccountId"].Value = Common.Common.cis_billing.accountHeadId;
                            dgvWardBill.Rows[rowNo].Cells["wardAccountName"].Value = Common.Common.cis_billing.accountName;
                            dgvWardBill.Rows[rowNo].Cells["accountGroupId"].Value = lblAccountGroupId.Text.ToString();
                            dgvWardBill.Rows[rowNo].Cells["wardCategory"].Value = lblAccountGroup.Text.ToString();
                            dgvWardBill.Rows[rowNo].Cells["wardQty"].Value = txtWardQty.Text.ToString();
                            dgvWardBill.Rows[rowNo].Cells["wardUnitPrice"].Value = txtUnitpriceWard.Text.ToString();
                            dgvWardBill.Rows[rowNo].Cells["wardTotalAmt"].Value = lblWardTotalAmt.Text.ToString();
                            clearWardInputValues();
                            cboWardAccountName.Focus();
                            calculateWardSum();
                        }
                    }

                    else
                    {
                        MessageBox.Show("Fields are required....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void txtWardQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void clearWardInputValues()
        {
            cboWardAccountName.SelectedIndex = -1;
            lblAccountGroupId.Text = string.Empty;
            lblAccountGroup.Text = string.Empty;
            txtWardQty.Text = string.Empty;
            txtUnitpriceWard.Text = "0.00";
            lblWardTotalAmt.Text = "0.00";
            Common.Common.cis_billing.accountHeadId = 0;
            Common.Common.cis_billing.accountName = string.Empty;
            Common.Common.cis_billing.accountHeadId = 0;
            Common.Common.cis_billing.accountGroupName = string.Empty;
            Common.Common.cis_billing.qtyGen = 0;
            Common.Common.cis_billing.genUnitPrice = 0;
            Common.Common.cis_billing.genTotal = 0;
            lblCheckEditModeWard.Text = string.Empty;
            txtAdvAdj.Text = "0.00";
            txtAmtPaid.Text = "0.00";
            txtDiscount.Text = "0.00";
            //lblGenDueAmt.Text = "0.00";
            lblWardBillNo.Text = string.Empty;
            lblDiscount.Text = "0.00";
        }

        private void calculateWardSum()
        {
            decimal sum = 0;
            Common.Common.cis_billing.genTotalSum = 0;
            foreach (DataGridViewRow row in dgvWardBill.Rows) //Calculate Amount Columns
            {
                sum = Convert.ToDecimal(row.Cells[7].Value);
                Common.Common.cis_billing.genTotalSum = Common.Common.cis_billing.genTotalSum + sum;
            }

            lblWardTotalNetAmt.Text = Common.Common.cis_billing.genTotalSum.ToString("0.00"); //Display Total Sum
            txtAmtPaid.Text = "0.00";
            txtDiscount.Text = "0.00";
            lblDueAmt.Text = Common.Common.cis_billing.genTotalSum.ToString("0.00");
            calculateGrandTotal();
        }

        private void calculateGrandTotal()
        {
            Common.Common.totalInvoice = (objBusinessFacade.NonBlankValueOfDecimal(lblWardTotalNetAmt.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(lblDiscount.Text.ToString()));
            lblTotalCollectAmt.Text = Common.Common.totalInvoice.ToString("0.00");
            lblDueAmt.Text = (objBusinessFacade.NonBlankValueOfDecimal(lblWardTotalNetAmt.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(lblDiscount.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtAmtPaid.Text.ToString()) - objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString())).ToString("0.00");
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (objBusinessFacade.NonBlankValueOfDecimal(txtDiscount.Text.ToString()) > 0)
                {
                    txtAmtPaid.Select();
                    e.Handled = true;
                    calculateWardFee();
                }

                else
                {
                    MessageBox.Show("Discount Amount can't be greater than Bill Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiscount.Text = "0.00";
                    calculateGrandTotal();
                }
            }
        }

        private void calculateWardFee()
        {
            Common.Common.cis_billing.netlWardAmt = Convert.ToDecimal(lblWardTotalNetAmt.Text.ToString());
            Common.Common.cis_billing.discountType = cboDiscountType.SelectedIndex;

            if (!string.IsNullOrEmpty(txtDiscount.Text))
            {
                Common.Common.cis_billing.discountPercOrAmtWard = Convert.ToDecimal(txtDiscount.Text.ToString());
            }
            else
            {
                Common.Common.cis_billing.discountPercOrAmtWard = 0;
            }

            if (Common.Common.cis_billing.discountType == 1 && Common.Common.cis_billing.discountPercOrAmtWard > 0)
            {
                if (Common.Common.cis_billing.discountPercOrAmtWard <= 100)
                {
                    Common.Common.cis_billing.discountWardAmt = Math.Round(Common.Common.cis_billing.netlWardAmt * (Common.Common.cis_billing.discountPercOrAmtWard / 100), 2);
                    lblDiscount.Text = Convert.ToString(Common.Common.cis_billing.discountWardAmt);
                }
                else
                {
                    MessageBox.Show("Discount Amount can't be greater than Bill Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiscount.Text = "0.00";
                }
            }
            else
            {
                if (Common.Common.cis_billing.discountPercOrAmtWard <= Common.Common.cis_billing.netlWardAmt)
                {
                    Common.Common.cis_billing.discountWardAmt = Math.Round(Convert.ToDecimal(txtDiscount.Text.ToString()), 2);
                    lblDiscount.Text = Convert.ToString(Common.Common.cis_billing.discountWardAmt);
                }
                else
                {
                    MessageBox.Show("Discount Amount can't be greater than Bill Amount....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiscount.Text = "0.00";
                }
            }

            Common.Common.cis_billing.netlWardAmt = Common.Common.cis_billing.netlWardAmt - Common.Common.cis_billing.discountWardAmt;

            //lblRegNetTotal.Text = Convert.ToString(Common.Common.cis_billing.netTotalWard);
            //lblRegistrationAmount.Text = Convert.ToString(Common.Common.cis_billing.netTotalWard);
            //txtRegAmountPaid.Text = Convert.ToString(Common.Common.cis_billing.netTotalWard);
            //Common.Common.cis_billing.amountPaidReg = Convert.ToDecimal(txtAmountPaid.Text.ToString());
            //Common.Common.cis_billing.balanceAmountReg = (Common.Common.cis_billing.netTotalWard - Common.Common.cis_billing.amountPaidReg);
            //lblRegBalance.Text = Convert.ToString(Common.Common.cis_billing.balanceAmountReg);
            calculateGrandTotal();
        }

        private void dgvWardBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvWardBill.Rows.Count > 0)
            {
                //dgvPharmacyBillDetails.Columns[e.ColumnIndex].Name.Equals("EditPha")
                if (dgvWardBill.Columns[e.ColumnIndex].Name.Equals("wardEdit"))//Edit Button Click
                {
                    cboWardAccountName.SelectedValue = Convert.ToInt32(dgvWardBill.Rows[e.RowIndex].Cells["wardAccountId"].Value.ToString());
                    txtWardQty.Text = dgvWardBill.Rows[e.RowIndex].Cells["wardQty"].Value.ToString();
                    lblAccountGroup.Text = dgvWardBill.Rows[e.RowIndex].Cells["wardCategory"].Value.ToString();
                    txtUnitpriceWard.Text = dgvWardBill.Rows[e.RowIndex].Cells["wardUnitPrice"].Value.ToString();
                    lblWardTotalAmt.Text = dgvWardBill.Rows[e.RowIndex].Cells["wardTotalAmt"].Value.ToString();
                    lblCheckEditModeWard.Text = e.RowIndex.ToString();
                }

                if (dgvWardBill.Columns[e.ColumnIndex].Name.Equals("wardDelete"))//Delete Button Click
                {
                    if (MessageBox.Show("Are you sure to Delete the record?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvWardBill.Rows.Remove(dgvWardBill.Rows[e.RowIndex]);
                        calculateWardSum();
                    }
                }
            }
        }

        private void txtAmtPaid_KeyDown(object sender, KeyEventArgs e)
       {
            if (e.KeyCode == Keys.Enter)
            {
                Common.Common.cis_billing.netlWardAmt = Convert.ToDecimal(lblWardTotalNetAmt.Text.ToString());
                if (Common.Common.cis_billing.netlWardAmt >= Convert.ToDecimal(txtAmtPaid.Text.ToString()))
                {
                    txtAdvAdj.Focus();
                    e.Handled = true;
                    Common.Common.cis_billing.amtPaidWardAmt = Convert.ToDecimal(txtAmtPaid.Text.ToString());
                    Common.Common.cis_billing.dueWardAmt = (Common.Common.cis_billing.netlWardAmt - Common.Common.cis_billing.amtPaidWardAmt - objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString()));
                    lblDueAmt.Text = Convert.ToString(Common.Common.cis_billing.dueWardAmt);
                }
                else
                {
                    MessageBox.Show("Amount can't be greater than Net Total....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Common.Common.cis_billing.dueWardAmt = (Common.Common.cis_billing.netlWardAmt - Common.Common.cis_billing.amtPaidWardAmt - objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString()));
                    lblDueAmt.Text = Convert.ToString(Common.Common.cis_billing.dueWardAmt);
                    txtAmtPaid.Text = "0.00";
                }
                calculateGrandTotal();
            }
        }

        private void txtAdvAdj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Common.Common.cis_billing.netlWardAmt = Convert.ToDecimal(lblWardTotalNetAmt.Text.ToString());

                if (objBusinessFacade.NonBlankValueOfDecimal(lblNetAvailAdv.Text.ToString()) >= objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString()))
                {
                    if (Common.Common.cis_billing.netlWardAmt >= Convert.ToDecimal(txtAdvAdj.Text.ToString()))
                    {
                        txtAdvAdj.Focus();
                        e.Handled = true;
                        //Common.Common.cis_billing.wardAmtPaid = Convert.ToDecimal(txtAmtPaid.Text.ToString());
                        //Common.Common.cis_billing.balanceAmountWard = (Common.Common.cis_billing.netlWardAmt - Common.Common.cis_billing.wardAmtPaid - objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString()));
                        //lblDueAmt.Text = Convert.ToString(Common.Common.cis_billing.balanceAmountWard);
                    }
                    else
                    {
                        MessageBox.Show("Amount can't be greater than Net Total....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Common.Common.cis_billing.balanceAmountWard = (Common.Common.cis_billing.netlWardAmt - Common.Common.cis_billing.wardAmtPaid - objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString()));
                        //lblDueAmt.Text = Convert.ToString(Common.Common.cis_billing.balanceAmountWard);
                        txtAdvAdj.Text = "0.00";
                    }
                }

                else
                {
                    MessageBox.Show("Amount can't be greater than Total Advance....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Common.Common.cis_billing.balanceAmountWard = (Common.Common.cis_billing.netlWardAmt - Common.Common.cis_billing.wardAmtPaid - objBusinessFacade.NonBlankValueOfDecimal(txtAdvAdj.Text.ToString()));
                    //lblDueAmt.Text = Convert.ToString(Common.Common.cis_billing.balanceAmountWard);
                    txtAdvAdj.Text = "0.00";
                }
                calculateGrandTotal();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblWardBillNo.Text))
            {
                CIS.BillTemplates.frmPrintReceipt frmWardRececipt = new BillTemplates.frmPrintReceipt("WardBill", lblWardBillNo.Text, txtPatientId.Text);
                frmWardRececipt.ShowDialog();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearWardRecords();
        }

        private void clearWardRecords()
        {
            clearWardInputValues();
            dgvWardBill.Rows.Clear();
            dgvWardBill.DataSource = null;
            lblWardTotalNetAmt.Text = "0.00";
            lblWardTotalAmt.Text = "0.00";
            rbtnFinalWardBill.Checked = true;
            rbtnFinalWardBill.Enabled = true;
            //Common.Common.cis_billing.genTotalSum = 0;
            cboPaymentMode.SelectedIndex = 0;
            txtHolderName.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtCardNumber.Text = string.Empty;
            calculateGrandTotal();
            clearWardPatientInfo();
        }

        private void clearWardPatientInfo()
        {
            txtPatientId.Text = string.Empty;
            lblPatientName.Text = string.Empty;
            lblBillDate.Text = string.Empty;
            this.dtpToDate.Value = DateTime.Now;
            this.dtpFromDate.Value = DateTime.Now;
            lblVisitDate.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblGender.Text = string.Empty;
            lblVisitNo.Text = string.Empty;
            lblPatientType.Text = string.Empty;
            lblCorporateName.Text = string.Empty;
            lblBedDetails.Text = string.Empty;
            multitxtAddress.Text = string.Empty;
            btnSave.Enabled = true;
        }

        private void rbtnIntermediateBill_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnIntermediateBill.Checked == true)
            {
                lblDisDate.Text = "To";
            }
            else
            {
                lblDisDate.Text = "Discharge";
            }
        }

        private void rbtnFinalWardBill_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnFinalWardBill.Checked == true)
            {
                lblDisDate.Text = "Discharge";
            }
            else
            {
                lblDisDate.Text = "To";
            }
        }
    }
}
