using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Printing;

using CIS.BusinessFacade;
using CIS.Common;
using CIS;


namespace CIS.BillTemplates
{
    public partial class frmPrintReceipt : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        private Font verdana10Font;
        #endregion


        public frmPrintReceipt()
        {
            /*InitializeComponent();
            string Source = LoadReceipt(printRegistationBill(""), GetHeadDetails(), "Registration_Bill.txt", 8, 20);
            lblPreview.Text = Source;*/
        }

        public frmPrintReceipt(string printSlip, ComArugments billIds)
        {
            InitializeComponent();
            string dueCollSource = LoadDueCollection(printdueCollection(billIds), GetHeadDetails(), "Due_Collection_Patient.txt", 8, 20);
            lblPreview.Text = dueCollSource;

        }

        public frmPrintReceipt(string printSlip, string billNumber, string patientId)
        {
            InitializeComponent();
            switch (printSlip)
            {
                case "RegistrationSlip":
                    string regSlipSource = LoadRegSlip(printRegistationSlip(patientId), GetHeadDetails(), "Registration_Slip.txt", 8, 20);
                    lblPreview.Text = regSlipSource;
                    break;

                case "RegistrationBill":
                    string regSource = LoadRegReceipt(printRegistationBill(billNumber), GetHeadDetails(), "Registration_Bill.txt", 8, 20);
                    lblPreview.Text = regSource;
                    break;

                case "InvestigationBill":
                    string invSource = LoadInvReceipt(printInvestigationBill(billNumber), GetHeadDetails(), "Investigation_Bill.txt", 8, 20);
                    lblPreview.Text = invSource;
                    break;

                case "PharmacyBill":
                    string phaSource = LoadPhaReceipt(printPharmacyBill(billNumber), printRefundPharmacyBill(billNumber), "Pharmacy_Bill.txt", 8, 20);
                    lblPreview.Text = phaSource;
                    break;

                case "GeneralBill":
                    string genSource = LoadGenReceipt(printGeneralBill(billNumber), printRefundGeneralBill(billNumber), "General_Bill.txt", 8, 20);
                    lblPreview.Text = genSource;
                    break;

                case "WardBill":
                    string wardSource = LoadWardReceipt(printWardBill(billNumber), GetHeadDetails(), "Ward_Bill.txt", 8, 20);
                    lblPreview.Text = wardSource;
                    break;

                case "AdvanceBill":
                    string advSource = LoadAdvanceReceipt(printAdvBill(billNumber), GetHeadDetails(), "Advance_Bill.txt", 8, 20);
                    lblPreview.Text = advSource;
                    break;
            }
        }

        private DataTable printRegistationSlip(string patient_id)
        {
            dtSource = objBusinessFacade.printRegistationSlip(patient_id);
            return dtSource;
        }

        private DataTable printRegistationBill(string reg_bill_number)
        {
            dtSource = objBusinessFacade.printRegistationBill(reg_bill_number);
            return dtSource;
        }

        private DataTable printInvestigationBill(string inv_bill_number)
        {
            dtSource = objBusinessFacade.printInvestigationBill(inv_bill_number);
            return objBusinessFacade.AssignRowNo(dtSource);
        }

        private DataTable printPharmacyBill(string pha_bill_number)
        {
            dtSource = objBusinessFacade.printPharmacyBill(pha_bill_number);
            return objBusinessFacade.AssignRowNo(dtSource);
        }

        private DataTable printRefundPharmacyBill(string pha_bill_number)
        {
            dtSource = objBusinessFacade.printRefundPharmacyBill(pha_bill_number);
            return objBusinessFacade.AssignRowNo(dtSource);
        }

        private DataTable printGeneralBill(string gen_bill_number)
        {
            dtSource = objBusinessFacade.printGeneralBill(gen_bill_number);
            return objBusinessFacade.AssignRowNo(dtSource);
        }

        private DataTable printRefundGeneralBill(string gen_bill_number)
        {
            dtSource = objBusinessFacade.printRefundGeneralBill(gen_bill_number);
            return objBusinessFacade.AssignRowNo(dtSource);
        }

        private DataTable printWardBill(string ward_bill_number)
        {
            dtSource = objBusinessFacade.printWardBill(ward_bill_number);
            return objBusinessFacade.AssignRowNo(dtSource);
        }

        private DataTable printAdvBill(string adv_bill_number)
        {
            dtSource = objBusinessFacade.printAdvanceBill(adv_bill_number);
            return objBusinessFacade.AssignRowNo(dtSource);
        }

        private DataTable printdueCollection(ComArugments billIds)
        {
            dtSource = objBusinessFacade.printdueCollection(billIds);
            return dtSource;
        }

        private DataTable GetHeadDetails()
        {
            DataTable dtDetails = new DataTable();
            dtDetails.Columns.Add("Head Id", typeof(int));
            dtDetails.Columns.Add("Head", typeof(string));
            dtDetails.Columns.Add("Amount", typeof(string));
            dtDetails.Columns.Add("ReceiptNumber", typeof(string));
            dtDetails.Columns.Add("DDCHEQUENO", typeof(string));
            dtDetails.Columns.Add("ISSUEDATE", typeof(DateTime));
            dtDetails.Columns.Add("GROUPID", typeof(string));
            dtDetails.Columns.Add("Fee", typeof(string));
            dtDetails.Rows.Add(1, "Bus Fee", "1300", "ABE123", "", DateTime.Now, "GR9", "Bus Fee");
            dtDetails.Rows.Add(2, "Tution Fee", "7000", "ABE124", "", DateTime.Now, "GR9", "Tution Fee");
            dtDetails.Rows.Add(3, "Special Fee", "1300", "ABE125", "", DateTime.Now, "GR9", "Special Fee");
            return dtDetails;
        }

        public string LoadRegSlip(DataTable dtPrint, DataTable dtHeadTotal, string ReceiptName, int NoofLines, int HeadWidth)
        {
            int EmptyLines = NoofLines - dtHeadTotal.Rows.Count;
            string TemplatePath = string.Empty;
            string FileSource = string.Empty;
            string ReceiptLine = string.Empty;
            TemplatePath = Application.StartupPath + "/" + ReceiptName;
            string CharWidth = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["FrontCharWidth"].ToString() : ConfigurationSettings.AppSettings["CharecterWidth"].ToString());
            string LeftMargin = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["SpaceLeftMargin"].ToString() : ConfigurationSettings.AppSettings["LeftMargin"].ToString());
            DataTable dt = dtPrint;
            if (dt.Rows.Count > 0)
            {
                string BPerson = string.Empty; string BAddress = string.Empty;

                string FinalText = string.Empty;

                StreamReader Strem = new StreamReader(TemplatePath);

                try
                {
                    while (Strem.Peek() > 0)
                    {
                        ReceiptLine = Strem.ReadLine() + "\n";
                        ReceiptLine = ReceiptLine.Replace("<PATIENTID>", dt.Rows[0]["patient_id"].ToString());
                        //ReceiptLine = ReceiptLine.Replace("<PATIENTNAME>", "<BLD>" + dt.Rows[0]["patient_name"].ToString() + "</BLD>");
                        ReceiptLine = ReceiptLine.Replace("<GUARDIANNAME>",
                            RowColAlign(dt.Rows[0]["guardian_name"].ToString(), 15, false));
                        ReceiptLine = ReceiptLine.Replace("<PATIENTNAME>", dt.Rows[0]["patient_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<AGE>", dt.Rows[0]["age"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<GENDER>", dt.Rows[0]["gender_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<PHONENO>", dt.Rows[0]["phone_no"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<ADDRESS>", dt.Rows[0]["address"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<CORPORATENAME>", dt.Rows[0]["corporate_name"].ToString());

                        FileSource += ReceiptLine;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Strem.Close();
            }

            return FileSource;
        }

        public string LoadRegReceipt(DataTable dtPrint, DataTable dtHeadTotal, string ReceiptName, int NoofLines, int HeadWidth)
        {
            int EmptyLines = NoofLines - dtHeadTotal.Rows.Count;
            string TemplatePath = string.Empty;
            string FileSource = string.Empty;
            string ReceiptLine = string.Empty;
            TemplatePath = Application.StartupPath + "/" + ReceiptName;
            string CharWidth = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["FrontCharWidth"].ToString() : ConfigurationSettings.AppSettings["CharecterWidth"].ToString());
            string LeftMargin = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["SpaceLeftMargin"].ToString() : ConfigurationSettings.AppSettings["LeftMargin"].ToString());
            DataTable dt = dtPrint;
            if (dt.Rows.Count > 0)
            {
                string BPerson = string.Empty; string BAddress = string.Empty;

                string FinalText = string.Empty;

                StreamReader Strem = new StreamReader(TemplatePath);

                try
                {
                    while (Strem.Peek() > 0)
                    {
                        ReceiptLine = Strem.ReadLine() + "\n";
                        ReceiptLine = ReceiptLine.Replace("<PATIENTID>",
                            RowColAlign(dt.Rows[0]["patient_id"].ToString(), 10, false));
                        ReceiptLine = ReceiptLine.Replace("<REGBILLNO>",
                            RowColAlign(dt.Rows[0]["bill_number"].ToString(), 10, false));
                        ReceiptLine = ReceiptLine.Replace("<REGBILLDATE>",
                            RowColAlign(dt.Rows[0]["bill_date"].ToString(), 17, false));
                        ReceiptLine = ReceiptLine.Replace("<PATIENTNAME>", dt.Rows[0]["patient_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<AGE>", dt.Rows[0]["age"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<GENDER>", dt.Rows[0]["gender"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<TOKENNO>", dt.Rows[0]["token_number"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<DOCTORNAME>", dt.Rows[0]["doctor_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<CORPORATENAME>", dt.Rows[0]["corporate_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<REGACCOUNTHEAD>", dt.Rows[0]["account_head_name"].ToString());


                        if (ReceiptLine.Contains("<TITLE>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<TITLE>", "<BLD> Corpus Fund </BLD>");
                        }
                        if (ReceiptLine.Contains("<REGCHARGES>"))
                        {
                            string regCharges = dt.Rows[0]["reg_charges"].ToString();
                            ReceiptLine = ReceiptLine.Replace("<REGCHARGES>",
                            RowColAlign(regCharges.PadLeft((regCharges.Length + (3 - (regCharges.Length % 3))), ' '), 10, false));
                        }

                        if (ReceiptLine.Contains("<TOTAL>"))
                            ReceiptLine = ReceiptLine.Replace("<TOTAL>", RowColAlign(dt.Rows[0]["bill_amount"].ToString(), 8, true));
                        if (ReceiptLine.Contains("<REGDISCOUNT>"))
                            ReceiptLine = ReceiptLine.Replace("<REGDISCOUNT>", RowColAlign(dt.Rows[0]["discount"].ToString(), 8, true));
                        if (ReceiptLine.Contains("<REGAMOUNTPAID>"))
                            ReceiptLine = ReceiptLine.Replace("<REGAMOUNTPAID>", RowColAlign(dt.Rows[0]["amount_paid"].ToString(), 8, true));
                        if (ReceiptLine.Contains("<REGDUE>"))
                            ReceiptLine = ReceiptLine.Replace("<REGDUE>", RowColAlign(dt.Rows[0]["due"].ToString(), 8, true));

                        if (ReceiptLine.Contains("<DISCOUNT>"))
                        {
                            string TermFee = string.Empty;

                            ReceiptLine = ReceiptLine.Replace("<DISCOUNT>",
                            RowColAlign(TermFee, TermFee.Length, false));
                        }

                        if (ReceiptLine.Contains("<TIME>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<TIME>", GetCurrentTime("hh:mm tt"));
                        }

                        if (ReceiptLine.Contains("<EMPTY>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<EMPTY>", "\n");
                            for (int j = 0; j < 7 - 1; j++)
                            {
                                ReceiptLine += "\n";
                            }
                        }
                        FileSource += ReceiptLine;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Strem.Close();
            }

            return FileSource;
        }

        public string LoadInvReceipt(DataTable dtPrint, DataTable dtHeadTotal, string ReceiptName, int NoofLines, int HeadWidth)
        {
            int EmptyLines = NoofLines - dtHeadTotal.Rows.Count;
            string TemplatePath = string.Empty;
            string FileSource = string.Empty;
            string ReceiptLine = string.Empty;
            TemplatePath = Application.StartupPath + "/" + ReceiptName;
            string CharWidth = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["FrontCharWidth"].ToString() : ConfigurationSettings.AppSettings["CharecterWidth"].ToString());
            string LeftMargin = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["SpaceLeftMargin"].ToString() : ConfigurationSettings.AppSettings["LeftMargin"].ToString());
            DataTable dt = dtPrint;
            if (dt.Rows.Count > 0)
            {
                string BPerson = string.Empty; string BAddress = string.Empty;

                string FinalText = string.Empty;

                StreamReader Strem = new StreamReader(TemplatePath);

                try
                {
                    while (Strem.Peek() > 0)
                    {
                        ReceiptLine = Strem.ReadLine() + "\n";
                        ReceiptLine = ReceiptLine.Replace("<PATIENTID>",
                            RowColAlign(dt.Rows[0]["patient_id"].ToString(), 17, false));
                        ReceiptLine = ReceiptLine.Replace("<INVBILLNO>",
                            RowColAlign(dt.Rows[0]["bill_number"].ToString(), 17, false));
                        ReceiptLine = ReceiptLine.Replace("<INVBILLDATE>",
                            RowColAlign(dt.Rows[0]["bill_date"].ToString(), 19, false));
                        ReceiptLine = ReceiptLine.Replace("<PATIENTNAME>",  RowColAlign(dt.Rows[0]["patient_name"].ToString(), 25, false));
                        ReceiptLine = ReceiptLine.Replace("<AGE>", dt.Rows[0]["age"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<GENDER>", dt.Rows[0]["gender"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<DOCTORNAME>", dt.Rows[0]["doctor_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<CORPORATENAME>", dt.Rows[0]["corporate_name"].ToString());

                        if (ReceiptLine.Contains("<DESHEAD>"))
                        {
                            ReceiptLine = ReplaceInvAccountHeadDetails(ReceiptLine, dtPrint, HeadWidth);
                        }
                        if (ReceiptLine.Contains("<TITLE>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<TITLE>", "<BLD> Corpus Fund </BLD>");
                        }
                        if (ReceiptLine.Contains("<TOTAL>"))
                            ReceiptLine = ReceiptLine.Replace("<TOTAL>", RowColAlign(dt.Rows[0]["bill_amount"].ToString(), 8, true));
                        if (ReceiptLine.Contains("<INVDISCOUNT>"))
                            ReceiptLine = ReceiptLine.Replace("<INVDISCOUNT>", RowColAlign(dt.Rows[0]["discount"].ToString(), 8, true));
                        if (ReceiptLine.Contains("<INVAMOUNTPAID>"))
                            ReceiptLine = ReceiptLine.Replace("<INVAMOUNTPAID>", RowColAlign(dt.Rows[0]["amount_paid"].ToString(), 8, true));
                        if (ReceiptLine.Contains("<INVDUE>"))
                            ReceiptLine = ReceiptLine.Replace("<INVDUE>", RowColAlign(dt.Rows[0]["due"].ToString(), 8, true));
                        if (ReceiptLine.Contains("<DISCOUNT>"))
                        {
                            string TermFee = string.Empty;

                            ReceiptLine = ReceiptLine.Replace("<DISCOUNT>",
                            RowColAlign(TermFee, TermFee.Length, false));
                        }

                        if (ReceiptLine.Contains("<TIME>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<TIME>", GetCurrentTime("hh:mm tt"));
                        }

                        if (ReceiptLine.Contains("<EMPTY>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<EMPTY>", "\n");
                            for (int j = 0; j < EmptyLines - 1; j++)
                            {
                                ReceiptLine += "\n";
                            }
                        }
                        if (ReceiptLine.Contains("RPT_MLTI"))
                        {
                            string FileSource1 = string.Empty;
                            ReceiptLine = ReceiptLine.Replace("<RPT_MLTI>", "").Replace("\n", "");
                            ReceiptLine = reportMultiRow(FileSource1, ReceiptLine, dtPrint, "LabServiceBill");

                        }
                        FileSource += ReceiptLine;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Strem.Close();
            }
            return FileSource;
        }

        public string LoadPhaReceipt(DataTable dtPrint, DataTable dtRefundList, string ReceiptName, int NoofLines, int HeadWidth)
        {
            //int EmptyLines = NoofLines - dtRefundList.Rows.Count;
            string TemplatePath = string.Empty;
            string FileSource = string.Empty;
            string RefundFileSource = string.Empty;
            string ReceiptLine = string.Empty;
            string BreakLine = "------------------------------------------------------------------------------------";
            string refundHeader = "#  Type  Item Name	  Lot   Exp Date	QTY    Unit Price F.Care  Total";
            string refundHeading = "Refunded Medicines";
            string ReceiptRefundLine = string.Empty;
            TemplatePath = Application.StartupPath + "/" + ReceiptName;
            DataTable dt = dtPrint;
            DataTable dtR = dtRefundList;
            DataView dv = dtRefundList.DefaultView;

            if (dt.Rows.Count > 0)
            {
                string BPerson = string.Empty; string BAddress = string.Empty;

                string FinalText = string.Empty;

                StreamReader Strem = new StreamReader(TemplatePath);

                try
                {
                    while (Strem.Peek() > 0)
                    {
                        ReceiptLine = Strem.ReadLine() + "\n";
                        ReceiptLine = ReceiptLine.Replace("<PATIENTID>",
                            RowColAlign(dt.Rows[0]["patient_id"].ToString(), 10, false));
                        ReceiptLine = ReceiptLine.Replace("<PHABILLNO>",
                            RowColAlign(dt.Rows[0]["bill_number"].ToString(), 10, false));
                        ReceiptLine = ReceiptLine.Replace("<PHABILLDATE>",
                            RowColAlign(dt.Rows[0]["bill_date"].ToString(), 17, false));
                        ReceiptLine = ReceiptLine.Replace("<PATIENTNAME>", dt.Rows[0]["patient_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<AGE>", dt.Rows[0]["age"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<GENDER>", dt.Rows[0]["gender"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<DOCTORNAME>", dt.Rows[0]["doctor_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<CORPORATENAME>", dt.Rows[0]["corporate_name"].ToString());

                        if (ReceiptLine.Contains("<DESHEAD>"))
                        {
                            ReceiptLine = ReplacePhaAccountHeadDetails(ReceiptLine, dtPrint, HeadWidth);
                        }



                        if (ReceiptLine.Contains("<TITLE>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<TITLE>", "<BLD> Corpus Fund </BLD>");
                        }

                        if (ReceiptLine.Contains("<PHAFREECARE>"))
                        {
                            string TotalConcession = dt.Rows[0]["total_free_care"].ToString();
                            ReceiptLine = ReceiptLine.Replace("<PHAFREECARE>",
                            RowColAlign(TotalConcession.PadLeft((TotalConcession.Length + (3 - (TotalConcession.Length % 3))), ' '), 10, false));
                        }

                        if (ReceiptLine.Contains("<PHAAMOUNTPAID>"))
                            ReceiptLine = ReceiptLine.Replace("<PHAAMOUNTPAID>", RowColAlign(dt.Rows[0]["amount_paid"].ToString(), 10, true));
                        if (ReceiptLine.Contains("<PHADUE>"))
                            ReceiptLine = ReceiptLine.Replace("<PHADUE>", RowColAlign(dt.Rows[0]["due"].ToString(), 10, true));
                        if (ReceiptLine.Contains("<TOTAL>"))
                            ReceiptLine = ReceiptLine.Replace("<TOTAL>", RowColAlign(dt.Rows[0]["bill_amount"].ToString(), 10, true));
                        if (ReceiptLine.Contains("<PHADISCOUNT>"))
                            ReceiptLine = ReceiptLine.Replace("<PHADISCOUNT>", RowColAlign(dt.Rows[0]["discount"].ToString(), 10, true));
                        
                        /*if (ReceiptLine.Contains("<EMPTY>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<EMPTY>", "\n");
                            for (int j = 0; j < EmptyLines - 1; j++)
                            {
                                ReceiptLine += "\n";
                            }
                        }*/

                        if (dtR.Rows.Count > 0)
                        {
                            if (ReceiptLine.Contains("<DESREFUNDHEAD>"))
                            {
                                ReceiptRefundLine = refundHeading;
                                ReceiptRefundLine = BreakLine;
                                ReceiptRefundLine = refundHeader;
                                ReceiptRefundLine = BreakLine;
                                ReceiptRefundLine = ReplacePhaRefundAccountHeadDetails(ReceiptLine, dtRefundList, HeadWidth);
                                ReceiptRefundLine += BreakLine;
                            }
                        }

                        if (ReceiptLine.Contains("RPT_DYN"))
                        {
                            if (dtR.Rows.Count == 0)
                                ReceiptLine = ReceiptLine.Remove(0); 
                            else
                                ReceiptLine = ReceiptLine.Replace("<RPT_DYN>", "");
                        }
                        if (ReceiptLine.Contains("<REFUNDAMOUNT>"))
                            ReceiptLine = ReceiptLine.Replace("<REFUNDAMOUNT>", RowColAlign(dtRefundList.AsEnumerable().Sum(x => Convert.ToDecimal(x["refund_amt"])).ToString(), 10, true));
                        if (ReceiptLine.Contains("<RPT_MLTI>"))
                        {
                            string FileSource1 = string.Empty;
                            ReceiptLine = ReceiptLine.Replace("<RPT_MLTI>", "").Replace("\n", "");
                            ReceiptLine = reportMultiRow(FileSource1, ReceiptLine, dtPrint, "PharmacyBillPrint");
                        }
                        if (ReceiptLine.Contains("<RPT_MLTI1>"))
                        {
                            string FileSource1 = string.Empty;
                            ReceiptLine = ReceiptLine.Replace("<RPT_MLTI1>", "").Replace("\n", "");
                            if (dtR.Rows.Count > 0)
                                ReceiptLine = reportMultiRow(FileSource1, ReceiptLine, dtR, "PharmacyBillPrintCancel");
                            else
                                ReceiptLine = ReceiptLine.Remove(0);
                        }
                        FileSource += ReceiptLine;
                        RefundFileSource = ReceiptRefundLine;
                    }

                    //MessageBox.Show(RefundFileSource);
                    FileSource += ReceiptRefundLine;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Strem.Close();
            }
            return FileSource;
        }

        public string LoadGenReceipt(DataTable dtPrint, DataTable dtRefundList, string ReceiptName, int NoofLines, int HeadWidth)
        {
            //int EmptyLines = NoofLines - dtRefundList.Rows.Count;
            string TemplatePath = string.Empty;
            string FileSource = string.Empty;
            string RefundFileSource = string.Empty;
            string ReceiptLine = string.Empty;
            string ReceiptRefundLine = string.Empty;
            TemplatePath = Application.StartupPath + "/" + ReceiptName;
            string CharWidth = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["FrontCharWidth"].ToString() : ConfigurationSettings.AppSettings["CharecterWidth"].ToString());
            string LeftMargin = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["SpaceLeftMargin"].ToString() : ConfigurationSettings.AppSettings["LeftMargin"].ToString());
            DataTable dt = dtPrint;
            DataTable dtR = dtRefundList;

            if (dt.Rows.Count > 0)
            {
                string BPerson = string.Empty; string BAddress = string.Empty;

                string FinalText = string.Empty;

                StreamReader Strem = new StreamReader(TemplatePath);

                try
                {
                    while (Strem.Peek() > 0)
                    {
                        ReceiptLine = Strem.ReadLine() + "\n";
                        ReceiptLine = ReceiptLine.Replace("<PATIENTID>",
                            RowColAlign(dt.Rows[0]["patient_id"].ToString(), 10, false));
                        ReceiptLine = ReceiptLine.Replace("<GENBILLNO>",
                            RowColAlign(dt.Rows[0]["bill_number"].ToString(), 10, false));
                        ReceiptLine = ReceiptLine.Replace("<GENBILLDATE>",
                            RowColAlign(dt.Rows[0]["bill_date"].ToString(), 17,                                                                                                                                 false));
                        ReceiptLine = ReceiptLine.Replace("<PATIENTNAME>", dt.Rows[0]["patient_name"].ToString());
                        
                        if (ReceiptLine.Contains("<TITLE>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<TITLE>", "<BLD> Corpus Fund </BLD>");
                        }

                        if (ReceiptLine.Contains("<GENAMOUNTPAID>"))
                            ReceiptLine = ReceiptLine.Replace("<GENAMOUNTPAID>", RowColAlign(dt.Rows[0]["amount_paid"].ToString(), 10, true));
                        if (ReceiptLine.Contains("<GENDUE>"))
                            ReceiptLine = ReceiptLine.Replace("<GENDUE>", RowColAlign(dt.Rows[0]["due"].ToString(), 10, true));
                        if (ReceiptLine.Contains("<TOTAL>"))
                            ReceiptLine = ReceiptLine.Replace("<TOTAL>", RowColAlign(dt.Rows[0]["bill_amount"].ToString(), 10, true));
                        if (ReceiptLine.Contains("<GENDISCOUNT>"))
                            ReceiptLine = ReceiptLine.Replace("<GENDISCOUNT>", RowColAlign(dt.Rows[0]["discount"].ToString(), 10, true));

                        if (ReceiptLine.Contains("RPT_MLTI"))
                        {
                            string FileSource1 = string.Empty;
                            ReceiptLine = ReceiptLine.Replace("<RPT_MLTI>", "").Replace("\n", "");
                            ReceiptLine = reportMultiRow(FileSource1, ReceiptLine, dtPrint, "GeneralBill");
                        }
                        FileSource += ReceiptLine;
                        RefundFileSource = ReceiptRefundLine;
                    }

                    //MessageBox.Show(RefundFileSource);
                    FileSource += ReceiptRefundLine;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Strem.Close();
            }
            return FileSource;
        }

        public string LoadWardReceipt(DataTable dtPrint, DataTable dtRefundList, string ReceiptName, int NoofLines, int HeadWidth)
        {
            //int EmptyLines = NoofLines - dtRefundList.Rows.Count;
            string TemplatePath = string.Empty;
            string FileSource = string.Empty;
            string RefundFileSource = string.Empty;
            string ReceiptLine = string.Empty;
            string BreakLine = "------------------------------------------------------------------------------------";
            string refundHeader = "#    Descr		          QTY    Unit Price  Total ";
            string refundHeading = "Refunded Items";
            string ReceiptRefundLine = string.Empty;
            TemplatePath = Application.StartupPath + "/" + ReceiptName;
            string CharWidth = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["FrontCharWidth"].ToString() : ConfigurationSettings.AppSettings["CharecterWidth"].ToString());
            string LeftMargin = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["SpaceLeftMargin"].ToString() : ConfigurationSettings.AppSettings["LeftMargin"].ToString());
            DataTable dt = dtPrint;
            DataTable dtR = dtRefundList;

            if (dt.Rows.Count > 0)
            {
                string BPerson = string.Empty; string BAddress = string.Empty;

                string FinalText = string.Empty;

                StreamReader Strem = new StreamReader(TemplatePath);

                try
                {
                    while (Strem.Peek() > 0)
                    {
                        ReceiptLine = Strem.ReadLine() + "\n";
                        ReceiptLine = ReceiptLine.Replace("<BILLTYPE>",
                            RowColAlign(dt.Rows[0]["bill_type_name"].ToString(), 25, false));
                        ReceiptLine = ReceiptLine.Replace("<PATIENTID>",
                            RowColAlign(dt.Rows[0]["patient_id"].ToString(), 10, false));
                        ReceiptLine = ReceiptLine.Replace("<WARDBILLNO>",
                            RowColAlign(dt.Rows[0]["bill_number"].ToString(), 10, false));
                        ReceiptLine = ReceiptLine.Replace("<WARDBILLDATE>",
                            RowColAlign(dt.Rows[0]["bill_date"].ToString(), 20, false));
                        ReceiptLine = ReceiptLine.Replace("<PATIENTNAME>", dt.Rows[0]["patient_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<AGE>",
                            RowColAlign(dt.Rows[0]["age"].ToString(), 17, false));
                        ReceiptLine = ReceiptLine.Replace("<GENDER>",
                            RowColAlign(dt.Rows[0]["gender_name"].ToString(), 8, false));
                        ReceiptLine = ReceiptLine.Replace("<GUARDIAN>",
                            RowColAlign(dt.Rows[0]["guardian_name"].ToString(), 20, false));
                        ReceiptLine = ReceiptLine.Replace("<CORPORATENAME>",
                            RowColAlign(dt.Rows[0]["corporate_name"].ToString(), 20, false));
                        ReceiptLine = ReceiptLine.Replace("<ADMISSIONBILLDATE>",
                            RowColAlign(dt.Rows[0]["visit_date"].ToString(), 25, false));
                        ReceiptLine = ReceiptLine.Replace("<DISCHARGEDATE>",
                            RowColAlign(dt.Rows[0]["to_date"].ToString(), 25, false));
                        ReceiptLine = ReceiptLine.Replace("<FROMDATE>",
                            RowColAlign(dt.Rows[0]["from_date"].ToString(), 25, false));
                        ReceiptLine = ReceiptLine.Replace("<TODATE>",
                            RowColAlign(dt.Rows[0]["to_date"].ToString(), 25, false));
                        ReceiptLine = ReceiptLine.Replace("<WARDDETAILS>",
                            RowColAlign(dt.Rows[0]["ward_details"].ToString(), 60, false));

                        if (ReceiptLine.Contains("<DESHEAD>"))
                        {
                            ReceiptLine = ReplaceWardAccountHeadDetails(ReceiptLine, dtPrint, HeadWidth);
                        }

                        if (ReceiptLine.Contains("<TOTAL>"))
                        {
                            double douTotal = 0;
                            douTotal = GetDoubleValue(dt.Rows[0]["bill_amount"].ToString());
                            string Total = Convert.ToString(douTotal);
                            ReceiptLine = ReceiptLine.Replace("<TOTAL>",
                            RowColAlign(FormatToAmount1(douTotal.ToString().PadLeft((Total.Length + (3 - (Total.Length % 3))), ' ')), 10, false));
                        }
                        if (ReceiptLine.Contains("<TITLE>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<TITLE>", "<BLD> Corpus Fund </BLD>");
                        }

                        if (ReceiptLine.Contains("<WARDDISCOUNT>"))
                        {
                            string TotalConcession = dt.Rows[0]["discount"].ToString();
                            ReceiptLine = ReceiptLine.Replace("<WARDDISCOUNT>",
                            RowColAlign(TotalConcession.PadLeft((TotalConcession.Length + (3 - (TotalConcession.Length % 3))), ' '), 10, false));
                        }

                        if (ReceiptLine.Contains("<WARDAMOUNTPAID>"))
                        {
                            string amountPaid = dt.Rows[0]["amount_paid"].ToString();
                            ReceiptLine = ReceiptLine.Replace("<WARDAMOUNTPAID>",
                            RowColAlign(amountPaid.PadLeft((amountPaid.Length + (3 - (amountPaid.Length % 3))), ' '), 10, false));
                        }

                        if (ReceiptLine.Contains("<WARDDUE>"))
                        {
                            string Due = dt.Rows[0]["due"].ToString();
                            ReceiptLine = ReceiptLine.Replace("<WARDDUE>",
                            RowColAlign(Due.PadLeft((Due.Length + (3 - (Due.Length % 3))), ' '), 10, false));
                        }

                        if (ReceiptLine.Contains("<WARDADVADJ>"))
                        {
                            string Due = dt.Rows[0]["pay_from_advance"].ToString();
                            ReceiptLine = ReceiptLine.Replace("<WARDADVADJ>",
                            RowColAlign(Due.PadLeft((Due.Length + (3 - (Due.Length % 3))), ' '), 10, false));
                        }

                        if (dtR.Rows.Count > 0)
                        {
                            if (ReceiptLine.Contains("<DESREFUNDHEAD>"))
                            {
                                ReceiptRefundLine = refundHeading;
                                ReceiptRefundLine = BreakLine;
                                ReceiptRefundLine = refundHeader;
                                ReceiptRefundLine = BreakLine;
                                ReceiptRefundLine = ReplaceWardAccountHeadDetails(ReceiptLine, dtRefundList, HeadWidth);
                                ReceiptRefundLine += BreakLine;
                            }
                        }
                        FileSource += ReceiptLine;
                        RefundFileSource = ReceiptRefundLine;
                    }

                    //MessageBox.Show(RefundFileSource);
                    FileSource += ReceiptRefundLine;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Strem.Close();
            }
            return FileSource;
        }

        public string LoadDueCollection(DataTable dtPrint, DataTable dtRefundList, string ReceiptName, int NoofLines, int HeadWidth)
        {
            //int EmptyLines = NoofLines - dtRefundList.Rows.Count;
            string TemplatePath = string.Empty;
            string FileSource = string.Empty;
            string RefundFileSource = string.Empty;
            string ReceiptLine = string.Empty;
            string BreakLine = "------------------------------------------------------------------------------------";
            string refundHeader = "#  Type  Item Name	  Lot   Exp Date	QTY    Unit Price F.Care  Total";
            string refundHeading = "Refunded Medicines";
            string ReceiptRefundLine = string.Empty;
            TemplatePath = Application.StartupPath + "/" + ReceiptName;
            DataTable dt = dtPrint;
            DataTable dtR = dtRefundList;

            if (dt.Rows.Count > 0)
            {
                string BPerson = string.Empty; string BAddress = string.Empty;

                string FinalText = string.Empty;

                StreamReader Strem = new StreamReader(TemplatePath);

                try
                {
                    while (Strem.Peek() > 0)
                    {
                        ReceiptLine = Strem.ReadLine() + "\n";
                        ReceiptLine = ReceiptLine.Replace("<PATIENTID>",
                            RowColAlign(dt.Rows[0]["patient_id"].ToString(), 17, false));
                        //ReceiptLine = ReceiptLine.Replace("<PHABILLNO>",
                        //RowColAlign(dt.Rows[0]["bill_number"].ToString(), 17, false));
                        ReceiptLine = ReceiptLine.Replace("<COLLECTIONDATE>",
                            RowColAlign(dt.Rows[0]["collection_date"].ToString(), 17, false));
                        ReceiptLine = ReceiptLine.Replace("<PATIENTNAME>", dt.Rows[0]["patient_name"].ToString());
                        //ReceiptLine = ReceiptLine.Replace("<AGE>", dt.Rows[0]["age"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<GENDER>", dt.Rows[0]["gender"].ToString());
                        //ReceiptLine = ReceiptLine.Replace("<DOCTORNAME>", dt.Rows[0]["doctor_name"].ToString());

                        if (ReceiptLine.Contains("<DESHEAD>"))
                        {
                            ReceiptLine = ReplaceDueCollectionHeadDetails(ReceiptLine, dtPrint, HeadWidth);
                        }

                        /*if (ReceiptLine.Contains("<TITLE>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<TITLE>", "<BLD> Corpus Fund </BLD>");
                        }

                        if (ReceiptLine.Contains("<PHAFREECARE>"))
                        {
                            string TotalConcession = dt.Rows[0]["total_free_care"].ToString();
                            ReceiptLine = ReceiptLine.Replace("<PHAFREECARE>",
                            RowColAlign(TotalConcession.PadLeft((TotalConcession.Length + (3 - (TotalConcession.Length % 3))), ' '), 10, false));
                        }

                        if (ReceiptLine.Contains("<PHAAMOUNTPAID>"))
                            ReceiptLine = ReceiptLine.Replace("<PHAAMOUNTPAID>", RowColAlign(dt.Rows[0]["amount_paid"].ToString(), 10, true));
                        if (ReceiptLine.Contains("<PHADUE>"))
                            ReceiptLine = ReceiptLine.Replace("<PHADUE>", RowColAlign(dt.Rows[0]["due"].ToString(), 10, true));
                        if (ReceiptLine.Contains("<TOTAL>"))
                            ReceiptLine = ReceiptLine.Replace("<TOTAL>", RowColAlign(dt.Rows[0]["bill_amount"].ToString(), 10, true));
                        if (ReceiptLine.Contains("<PHADISCOUNT>"))
                            ReceiptLine = ReceiptLine.Replace("<PHADISCOUNT>", RowColAlign(dt.Rows[0]["discount"].ToString(), 10, true));

                        if (ReceiptLine.Contains("<EMPTY>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<EMPTY>", "\n");
                            for (int j = 0; j < EmptyLines - 1; j++)
                            {
                                ReceiptLine += "\n";
                            }
                        }*/

                        /*if (dtR.Rows.Count > 0)
                        {
                            if (ReceiptLine.Contains("<DESREFUNDHEAD>"))
                            {
                                ReceiptRefundLine = refundHeading;
                                ReceiptRefundLine = BreakLine;
                                ReceiptRefundLine = refundHeader;
                                ReceiptRefundLine = BreakLine;
                                ReceiptRefundLine = ReplacePhaRefundAccountHeadDetails(ReceiptLine, dtRefundList, HeadWidth);
                                ReceiptRefundLine += BreakLine;
                            }
                        }

                        if (ReceiptLine.Contains("RPT_DYN"))
                        {
                            if (dtR.Rows.Count == 0)
                                ReceiptLine = ReceiptLine.Remove(0);
                            else
                                ReceiptLine = ReceiptLine.Replace("<RPT_DYN>", "");
                        }*/
                        if (ReceiptLine.Contains("RPT_MLTI"))
                        {
                            string FileSource1 = string.Empty;
                            ReceiptLine = ReceiptLine.Replace("<RPT_MLTI>", "").Replace("\n", "");
                            ReceiptLine = reportMultiRow(FileSource1, ReceiptLine, dtPrint, "DueCollectionPrint");
                        }
                        /*if (ReceiptLine.Contains("RPT_MLTI1"))
                        {
                            string FileSource1 = string.Empty;
                            ReceiptLine = ReceiptLine.Replace("<RPT_MLTI1>", "").Replace("\n", "");
                            if (dtR.Rows.Count > 0)
                                ReceiptLine = reportMultiRow(FileSource1, ReceiptLine, dtR, "PharmacyBillPrintCancel");
                            else
                                ReceiptLine = ReceiptLine.Remove(0);
                        }*/
                        FileSource += ReceiptLine;
                        RefundFileSource = ReceiptRefundLine;
                    }

                    //MessageBox.Show(RefundFileSource);
                    FileSource += ReceiptRefundLine;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Strem.Close();
            }
            return FileSource;
        }

        private string ReplaceDueCollectionHeadDetails(string receiptLine, DataTable dthead, int HeadWidth)
        {
            string HeadDescription = string.Empty;
            DataTable dtReceipt = dthead;
            string ReplaceedHead = string.Empty;
            double AMOUNT0 = 0;
            //double Total = 0;
            //double Concession = 0;
            string sHead = string.Empty, sAmount = string.Empty, sConcession = string.Empty, sTotal = string.Empty;
            ReplaceedHead = receiptLine;
            string AppandHead = string.Empty;
            string FSpace = receiptLine.Substring(1, receiptLine.IndexOf('<')).Replace('<', ' ');
            for (int i = 0; i < dtReceipt.Rows.Count; i++)
            {
                //string unitPrice = Convert.ToString(dtReceipt.Rows[i]["unit_price"].ToString());
                //AMOUNT0 = GetDoubleValue(dtReceipt.Rows[i]["net_total_amount"].ToString());
                //string amount = Convert.ToString(dtReceipt.Rows[i]["net_total_amount"].ToString());

                // sAmount = (amount.Length.Equals(2) || amount.Length.Equals(1)) ?
                // RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft(6, ' ')), 10, false) :
                // RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft((amount.Length + (3 - (amount.Length % 3))), ' ')), 10, true);

                AppandHead = ((i > 0) ? FSpace : "") + string.Concat(RowColAlign(dtReceipt.Rows[i]["#"].ToString(), 3, true), "  ", RowColAlign(dtReceipt.Rows[i]["bill_date"].ToString(), 10, false), "  ", RowColAlign(dtReceipt.Rows[i]["bill_amount"].ToString(), HeadWidth, false), " ", RowColAlign(dtReceipt.Rows[i]["discount"].ToString(), HeadWidth, false), " ", RowColAlign(dtReceipt.Rows[i]["amount_paid"].ToString(), 5, true), " ", RowColAlign(dtReceipt.Rows[i]["due"].ToString(), 10, false), "\n");
                sHead += AppandHead;
            }
            ReplaceedHead = ReplaceedHead.Replace("<DESHEAD>", sHead);
            HeadDescription += ReplaceedHead;
            return HeadDescription;
        }

        public string LoadAdvanceReceipt(DataTable dtPrint, DataTable dtHeadTotal, string ReceiptName, int NoofLines, int HeadWidth)
        {
            int EmptyLines = NoofLines - dtHeadTotal.Rows.Count;
            string TemplatePath = string.Empty;
            string FileSource = string.Empty;
            string ReceiptLine = string.Empty;
            TemplatePath = Application.StartupPath + "/" + ReceiptName;
            string CharWidth = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["FrontCharWidth"].ToString() : ConfigurationSettings.AppSettings["CharecterWidth"].ToString());
            string LeftMargin = (ReceiptName.Equals("CorpusFund1.txt") ? ConfigurationSettings.AppSettings["SpaceLeftMargin"].ToString() : ConfigurationSettings.AppSettings["LeftMargin"].ToString());
            DataTable dt = dtPrint;
            if (dt.Rows.Count > 0)
            {
                string BPerson = string.Empty; string BAddress = string.Empty;

                string FinalText = string.Empty;

                StreamReader Strem = new StreamReader(TemplatePath);

                try
                {
                    while (Strem.Peek() > 0)
                    {
                        ReceiptLine = Strem.ReadLine() + "\n";
                        ReceiptLine = ReceiptLine.Replace("<PATIENTID>",
                            RowColAlign(dt.Rows[0]["patient_id"].ToString(), 17, false));
                        ReceiptLine = ReceiptLine.Replace("<ADVBILLNO>",
                            RowColAlign(dt.Rows[0]["receipt_number"].ToString(), 17, false));
                        ReceiptLine = ReceiptLine.Replace("<ADVBILLDATE>",
                            RowColAlign(dt.Rows[0]["bill_date"].ToString(), 25, false));
                        ReceiptLine = ReceiptLine.Replace("<PATIENTNAME>", "<BLD>" + dt.Rows[0]["patient_name"].ToString() + "</BLD>");
                        ReceiptLine = ReceiptLine.Replace("<AGE>", dt.Rows[0]["age"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<GENDER>", dt.Rows[0]["gender_name"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<WARDDETAILS>", dt.Rows[0]["ward_details"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<ADVANCETRANSACTION>", dt.Rows[0]["transaction_type"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<ADVACCOUNTHEAD>", dt.Rows[0]["transaction_type"].ToString());
                        ReceiptLine = ReceiptLine.Replace("<ADVCHARGES>", dt.Rows[0]["transaction_amount"].ToString());

                        if (ReceiptLine.Contains("<EMPTY>"))
                        {
                            ReceiptLine = ReceiptLine.Replace("<EMPTY>", "\n");
                            for (int j = 0; j < EmptyLines - 1; j++)
                            {
                                ReceiptLine += "\n";
                            }
                        }
                        FileSource += ReceiptLine;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Strem.Close();
            }

            return FileSource;
        }

        public string FormatDate(string date)
        {
            if (date != string.Empty)
            {
                if (date.Length > 10)
                {
                    date = date.Substring(0, 10);
                }
            }
            return date;
        }

        #region RowColAlign
        //private string RowColAlign(string rowText, int spaceWidth, bool isSpaceFront)
        //{
        //    string TempSpace = string.Empty;
        //    if (isSpaceFront)
        //    {
        //        for (int i = 1; i <= spaceWidth; i++)
        //        {
        //            TempSpace = TempSpace + " ";
        //        }
        //        rowText = TempSpace + rowText;

        //    }
        //    else
        //    {
        //        if (rowText.Length < spaceWidth)
        //        {
        //            for (int i = 1; i <= (spaceWidth - rowText.Length); i++)
        //            {
        //                TempSpace = TempSpace + " ";
        //            }
        //            rowText = rowText + TempSpace;
        //        }
        //        else if (rowText != string.Empty && rowText.Length > spaceWidth)
        //        {
        //            int TotalLines = rowText.Length / spaceWidth;
        //            int Remainder = rowText.Length % spaceWidth;

        //            string FormattedString = string.Empty;
        //            string RemainingString = string.Empty;
        //            for (int i = 0; i < TotalLines; i++)
        //            {
        //                string RowSubString = rowText.Substring(i * spaceWidth, spaceWidth);
        //                FormattedString += RowSubString + "\n";
        //            }
        //            FormattedString = FormattedString.Trim('\n');// CommonUtility.RemoveLastDelimter(FormattedString, "\n");
        //            if (Remainder > 0)
        //            {
        //                RemainingString = rowText.Substring(TotalLines * spaceWidth, Remainder);
        //                RemainingString = RowColAlign(RemainingString, spaceWidth, false);
        //                rowText = FormattedString + "\n" + RemainingString;
        //            }
        //            else
        //            {
        //                rowText = FormattedString;
        //            }
        //        }
        //    }
        //    return rowText;
        //}

        private string RowColAlign(string rowcolText, int rowcolwidth, bool isSpacefront)
        {
            string tempSpace = "";
            // string original
            if (rowcolText.Length < rowcolwidth)
            {
                for (int j = 1; j <= (rowcolwidth - rowcolText.Length); j++)
                {
                    tempSpace = tempSpace + " ";
                }
            }
            if (isSpacefront == true)
                rowcolText = tempSpace + rowcolText;
            else
                rowcolText = rowcolText + tempSpace;
            return rowcolText;
        }
        #endregion


        public string FormatToAmount(string stringAmount)
        {
            double Amount = GetDoubleValue(stringAmount);

            string FormattedAmount = Amount.ToString("F2");
            return FormattedAmount;
        }

        public string FormatToAmount1(string stringAmount)
        {
            string FormattedAmount = stringAmount + ".00";
            return FormattedAmount;
        }

        public double GetDoubleValue(string value)
        {
            double Val = 0;
            if (value != "")
            {
                try
                {
                    Val = double.Parse(value);

                }
                catch (Exception) { }
            }

            return Val;
        }

        private string ReplaceInvAccountHeadDetails(string receiptLine, DataTable dthead, int HeadWidth)
        {
            string HeadDescription = string.Empty;
            DataTable dtReceipt = dthead;
            string ReplaceedHead = string.Empty;
            double AMOUNT0 = 0;
            //double Total = 0;
            //double Concession = 0;
            string sHead = string.Empty, sAmount = string.Empty, sConcession = string.Empty, sTotal = string.Empty;
            ReplaceedHead = receiptLine;
            string AppandHead = string.Empty;
            string FSpace = receiptLine.Substring(1, receiptLine.IndexOf('<')).Replace('<', ' ');
            for (int i = 0; i < dtReceipt.Rows.Count; i++)
            {
                string unitPrice = Convert.ToString(dtReceipt.Rows[i]["unit_price"].ToString());
                AMOUNT0 = GetDoubleValue(dtReceipt.Rows[i]["amount"].ToString());
                string amount = Convert.ToString(dtReceipt.Rows[i]["amount"].ToString());

                sAmount = (amount.Length.Equals(2) || amount.Length.Equals(1)) ?
                    RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft(6, ' ')), 10, false) :
                RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft((amount.Length + (3 - (amount.Length % 3))), ' ')), 10, true);

                AppandHead = ((i > 0) ? FSpace : "") + string.Concat(RowColAlign(dtReceipt.Rows[i]["#"].ToString(), 3, true), "  ", RowColAlign(dtReceipt.Rows[i]["department_name"].ToString(), 10, false), "  ", RowColAlign(dtReceipt.Rows[i]["investigation_name"].ToString(), HeadWidth, false), " ", RowColAlign(dtReceipt.Rows[i]["qty"].ToString(), 3, true), "  ", RowColAlign(unitPrice, 5, true), "   ", sAmount, "\n");
                sHead += AppandHead;

                //sConcession += RowColAlign(FormatToAmount1(Concession.ToString().PadLeft((amount.Length + (3 - (amount.Length % 3))), ' ')), 10, false);

                //sTotal += RowColAlign(FormatToAmount1(Total.ToString().PadLeft((amount.Length + (3 - (amount.Length % 3))), ' ')), 10, false);
            }
            ReplaceedHead = ReplaceedHead.Replace("<DESHEAD>", sHead);
            HeadDescription += ReplaceedHead;
            return HeadDescription;
        }

        private string ReplacePhaAccountHeadDetails(string receiptLine, DataTable dthead, int HeadWidth)
        {
            string HeadDescription = string.Empty;
            DataTable dtReceipt = dthead;
            string ReplaceedHead = string.Empty;
            double AMOUNT0 = 0;
            //double Total = 0;
            //double Concession = 0;
            string sHead = string.Empty, sAmount = string.Empty, sConcession = string.Empty, sTotal = string.Empty;
            ReplaceedHead = receiptLine;
            string AppandHead = string.Empty;
            string FSpace = receiptLine.Substring(1, receiptLine.IndexOf('<')).Replace('<', ' ');
            for (int i = 0; i < dtReceipt.Rows.Count; i++)
            {
                string unitPrice = Convert.ToString(dtReceipt.Rows[i]["unit_price"].ToString());
                AMOUNT0 = GetDoubleValue(dtReceipt.Rows[i]["net_total_amount"].ToString());
                string amount = Convert.ToString(dtReceipt.Rows[i]["net_total_amount"].ToString());

                sAmount = (amount.Length.Equals(2) || amount.Length.Equals(1)) ?
                    RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft(6, ' ')), 10, false) :
                RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft((amount.Length + (3 - (amount.Length % 3))), ' ')), 10, true);

                AppandHead = ((i > 0) ? FSpace : "") + string.Concat(RowColAlign(dtReceipt.Rows[i]["#"].ToString(), 3, true), "  ", RowColAlign(dtReceipt.Rows[i]["item_type"].ToString(), 10, false), "  ", RowColAlign(dtReceipt.Rows[i]["item_name"].ToString(), HeadWidth, false), " ", RowColAlign(dtReceipt.Rows[i]["hsn_code"].ToString(), HeadWidth, false), " ", RowColAlign(dtReceipt.Rows[i]["tax_perc"].ToString(), 5, true), " ", RowColAlign(dtReceipt.Rows[i]["lot_id"].ToString(), 10, false), " ", RowColAlign(dtReceipt.Rows[i]["expiry_date"].ToString(), 10, false), " ", RowColAlign(dtReceipt.Rows[i]["trans_qty"].ToString(), 3, true), "  ", RowColAlign(unitPrice, 5, true), "   ", RowColAlign(dtReceipt.Rows[i]["free_care"].ToString(), 3, true), "  ", sAmount, "\n");
                sHead += AppandHead;
            }
            ReplaceedHead = ReplaceedHead.Replace("<DESHEAD>", sHead);
            HeadDescription += ReplaceedHead;
            return HeadDescription;
        }

        private string ReplacePhaRefundAccountHeadDetails(string receiptLine, DataTable dthead, int HeadWidth)
        {
            string HeadDescription = string.Empty;
            DataTable dtReceipt = dthead;
            string ReplaceedHead = string.Empty;
            double AMOUNT0 = 0;
            //double Total = 0;
            //double Concession = 0;
            string sHead = string.Empty, sAmount = string.Empty, sConcession = string.Empty, sTotal = string.Empty;
            ReplaceedHead = receiptLine;
            string AppandHead = string.Empty;
            string FSpace = receiptLine.Substring(1, receiptLine.IndexOf('<')).Replace('<', ' ');
            for (int i = 0; i < dtReceipt.Rows.Count; i++)
            {
                string unitPrice = Convert.ToString(dtReceipt.Rows[i]["unit_price"].ToString());
                AMOUNT0 = GetDoubleValue(dtReceipt.Rows[i]["refund_amt"].ToString());
                string amount = Convert.ToString(dtReceipt.Rows[i]["refund_amt"].ToString());

                sAmount = (amount.Length.Equals(2) || amount.Length.Equals(1)) ?
                    RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft(6, ' ')), 10, false) :
                RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft((amount.Length + (3 - (amount.Length % 3))), ' ')), 10, true);

                AppandHead = ((i > 0) ? FSpace : "") + string.Concat(RowColAlign(dtReceipt.Rows[i]["#"].ToString(), 3, true), "  ", RowColAlign(dtReceipt.Rows[i]["item_type"].ToString(), 10, false), "  ", RowColAlign(dtReceipt.Rows[i]["item_name"].ToString(), HeadWidth, false), " ", RowColAlign(dtReceipt.Rows[i]["hsn_code"].ToString(), HeadWidth, false), " ", RowColAlign(dtReceipt.Rows[i]["tax_perc"].ToString(), 5, true), " ", RowColAlign(dtReceipt.Rows[i]["lot_id"].ToString(), 10, false), " ", RowColAlign(dtReceipt.Rows[i]["expiry_date"].ToString(), 10, false), " ", RowColAlign(dtReceipt.Rows[i]["refund_qty"].ToString(), 3, true), "  ", RowColAlign(unitPrice, 5, true), "   ", RowColAlign(dtReceipt.Rows[i]["ref_free_care"].ToString(), 3, true), "  ", sAmount, "\n");
                sHead += AppandHead;
            }
            ReplaceedHead = ReplaceedHead.Replace("<DESREFUNDHEAD>", sHead);
            HeadDescription += ReplaceedHead;
            return HeadDescription;
        }

        private string ReplaceGenRefundAccountHeadDetails(string receiptLine, DataTable dthead, int HeadWidth)
        {
            string HeadDescription = string.Empty;
            DataTable dtReceipt = dthead;
            string ReplaceedHead = string.Empty;
            double AMOUNT0 = 0;
            //double Total = 0;
            //double Concession = 0;
            string sHead = string.Empty, sAmount = string.Empty, sConcession = string.Empty, sTotal = string.Empty;
            ReplaceedHead = receiptLine;
            string AppandHead = string.Empty;
            string FSpace = receiptLine.Substring(1, receiptLine.IndexOf('<')).Replace('<', ' ');
            for (int i = 0; i < dtReceipt.Rows.Count; i++)
            {
                string unitPrice = Convert.ToString(dtReceipt.Rows[i]["unit_price"].ToString());
                AMOUNT0 = GetDoubleValue(dtReceipt.Rows[i]["refund_amt"].ToString());
                string amount = Convert.ToString(dtReceipt.Rows[i]["refund_amt"].ToString());

                sAmount = (amount.Length.Equals(2) || amount.Length.Equals(1)) ?
                    RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft(6, ' ')), 10, false) :
                RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft((amount.Length + (3 - (amount.Length % 3))), ' ')), 10, true);

                AppandHead = ((i > 0) ? FSpace : "") + string.Concat(RowColAlign(dtReceipt.Rows[i]["#"].ToString(), 3, true), "  ", RowColAlign(dtReceipt.Rows[i]["account_head_name"].ToString(), HeadWidth, false), " ", RowColAlign(dtReceipt.Rows[i]["refund_qty"].ToString(), 3, true), "  ", RowColAlign(unitPrice, 5, true), "   ", sAmount, "\n");
                sHead += AppandHead;
            }
            ReplaceedHead = ReplaceedHead.Replace("<DESREFUNDHEAD>", sHead);
            HeadDescription += ReplaceedHead;
            return HeadDescription;
        }

        private string ReplaceWardAccountHeadDetails(string receiptLine, DataTable dthead, int HeadWidth)
        {
            string HeadDescription = string.Empty;
            DataTable dtReceipt = dthead;
            string ReplaceedHead = string.Empty;
            double AMOUNT0 = 0;
            //double Total = 0;
            //double Concession = 0;
            string sHead = string.Empty, sAmount = string.Empty, sConcession = string.Empty, sTotal = string.Empty;
            ReplaceedHead = receiptLine;
            string AppandHead = string.Empty;
            string FSpace = receiptLine.Substring(1, receiptLine.IndexOf('<')).Replace('<', ' ');
            for (int i = 0; i < dtReceipt.Rows.Count; i++)
            {
                string unitPrice = Convert.ToString(dtReceipt.Rows[i]["unit_price"].ToString());
                AMOUNT0 = GetDoubleValue(dtReceipt.Rows[i]["amount"].ToString());
                string amount = Convert.ToString(dtReceipt.Rows[i]["amount"].ToString());

                sAmount = (amount.Length.Equals(2) || amount.Length.Equals(1)) ?
                    RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft(6, ' ')), 10, false) :
                RowColAlign(FormatToAmount1(AMOUNT0.ToString().PadLeft((amount.Length + (3 - (amount.Length % 3))), ' ')), 10, true);

                AppandHead = ((i > 0) ? FSpace : "") + string.Concat(RowColAlign(dtReceipt.Rows[i]["#"].ToString(), 3, true), "  ", RowColAlign(dtReceipt.Rows[i]["account_code"].ToString(), HeadWidth, false), " ", RowColAlign(dtReceipt.Rows[i]["quantity"].ToString(), 3, true), "  ", RowColAlign(unitPrice, 5, true), "   ", sAmount, "\n");
                sHead += AppandHead;
            }
            ReplaceedHead = ReplaceedHead.Replace("<DESHEAD>", sHead);
            HeadDescription += ReplaceedHead;
            return HeadDescription;
        }

        #region Body Row Replace
        string tempString = "";
        public String reportMultiRow(String FileSource, string tempFileSource, DataTable dataSource, string reportName)
        {
            string itemName = string.Empty; //Temporarily Variables Declared By Lawrence M
            string batchNumber = string.Empty;
            switch (reportName)
            {
                case "LabServiceBill":
                    for (int i = 0; i < dataSource.Rows.Count; i++)
                    {
                        tempString = tempFileSource;

                        if (Convert.ToString(i + 1).Length < 2)
                            tempString = tempString.Replace("<SN>", "0" + Convert.ToString(i + 1));
                        else
                            tempString = tempString.Replace("<SN>", Convert.ToString(i + 1));

                        tempString = tempString.Replace("<DESCR>", RowColAlign(dataSource.Rows[i]["investigation_name"].ToString(), 30, false));
                        tempString = tempString.Replace("<QTY>", RowColAlign(dataSource.Rows[i]["qty"].ToString(), 5, true));
                        tempString = tempString.Replace("<UNITPRICE>", RowColAlign(dataSource.Rows[i]["unit_price"].ToString(), 8, true));
                        tempString = tempString.Replace("<AMOUNT>", RowColAlign(dataSource.Rows[i]["amount"].ToString(), 8, true));
                        //tempString = RowAlign(tempString, 50);
                        FileSource = FileSource + tempString;
                        FileSource = FileSource + "\r\n";
                    }
                    break;
                case "LabWardBillService":
                    for (int i = 0; i < dataSource.Rows.Count; i++)
                    {
                        tempString = tempFileSource;

                        if (Convert.ToString(i + 1).Length < 2)
                            tempString = tempString.Replace("<SN>", "0" + Convert.ToString(i + 1));
                        else
                            tempString = tempString.Replace("<SN>", Convert.ToString(i + 1));

                        tempString = tempString.Replace("<TRANSACTION_DATE>", RowColAlign(dataSource.Rows[i]["TRANSACTION_DATE"].ToString(), 10, true));
                        tempString = tempString.Replace("<TEST_NAME>", RowColAlign(dataSource.Rows[i]["TEST_REPORT_NAME"].ToString(), 30, false));
                        tempString = tempString.Replace("<AMOUNT>", RowColAlign(dataSource.Rows[i]["AMOUNT"].ToString(), 8, true));

                        FileSource = FileSource + tempString;
                        FileSource = FileSource + "\r\n";
                    }
                    break;
                case "PharmacyBillPrint":
                    for (int i = 0; i < dataSource.Rows.Count; i++)
                    {
                        tempString = tempFileSource;

                        if (Convert.ToString(i + 1).Length < 2)
                            tempString = tempString.Replace("<SN>", "0" + Convert.ToString(i + 1));
                        else
                            tempString = tempString.Replace("<SN>", Convert.ToString(i + 1));
                        itemName = dataSource.Rows[i]["ITEM_NAME"].ToString(); //Added By Lawrence M
                        batchNumber = dataSource.Rows[i]["lot_id"].ToString();
                        tempString = tempString.Replace("<ITEMTYPE>", RowColAlign(dataSource.Rows[i]["ITEM_TYPE"].ToString(), 7, false));
                        tempString = tempString.Replace("<ITEMNAME>", RowColAlign((itemName.Length > 25) ? itemName.Substring(0, 22) : itemName, 25, false));
                        tempString = tempString.Replace("<BATCHNO>", RowColAlign((batchNumber.Length > 10) ? batchNumber.Substring(0, 10) : batchNumber, 10, false));
                        //tempString = tempString.Replace("<MANU>", RowColAlign(dataSource.Rows[i]["MANUFACTURER"].ToString(), 30, false));
                        tempString = tempString.Replace("<EXPDATE>", RowColAlign(dataSource.Rows[i]["EXPIRY_DATE"].ToString(), 7, false));
                        tempString = tempString.Replace("<UNITPRIC>", RowColAlign(dataSource.Rows[i]["UNIT_PRICE"].ToString(), 7, true));
                        //tempString = tempString.Replace("<TAXAMT>", RowColAlign(dataSource.Rows[i]["TAX_AMOUNT"].ToString(), 5, true));
                        tempString = tempString.Replace("<QTY>", RowColAlign(dataSource.Rows[i]["trans_qty"].ToString(), 4, true));
                        tempString = tempString.Replace("<AMOUNT>", RowColAlign(dataSource.Rows[i]["unit_price"].ToString(), 9, true));
                        //tempString = tempString.Replace("<ACTUAL_REFUND>", RowColAlign(dataSource.Rows[i]["ACTUAL_REFUND"].ToString(), 10, true));
                        tempString = tempString.Replace("<FREE_CARE>", RowColAlign(dataSource.Rows[i]["FREE_CARE"].ToString(), 8, true));
                        FileSource = FileSource + tempString;
                        FileSource = FileSource + "\r\n";
                    }
                    break;
                case "PharmacyBillPrintCancel":
                    for (int i = 0; i < dataSource.Rows.Count; i++)
                    {
                        tempString = tempFileSource;

                        if (Convert.ToString(i + 1).Length < 2)
                            tempString = tempString.Replace("<SN>", "0" + Convert.ToString(i + 1));
                        else
                            tempString = tempString.Replace("<SN>", Convert.ToString(i + 1));
                        itemName = dataSource.Rows[i]["ITEM_NAME"].ToString(); //Added By Lawrence M
                        batchNumber = dataSource.Rows[i]["lot_id"].ToString();
                        tempString = tempString.Replace("<ITEMTYPE>", RowColAlign(dataSource.Rows[i]["ITEM_TYPE"].ToString(), 7, false));
                        tempString = tempString.Replace("<ITEMNAME>", RowColAlign((itemName.Length > 25) ? itemName.Substring(0, 22) : itemName, 25, false));
                        tempString = tempString.Replace("<BATCHNO>", RowColAlign((batchNumber.Length > 10) ? batchNumber.Substring(0, 10) : batchNumber, 10, false));
                        //tempString = tempString.Replace("<MANU>", RowColAlign(dataSource.Rows[i]["MANUFACTURER"].ToString(), 30, false));
                        tempString = tempString.Replace("<EXPDATE>", RowColAlign(dataSource.Rows[i]["EXPIRY_DATE"].ToString(), 7, false));
                        tempString = tempString.Replace("<UNITPRIC>", RowColAlign(dataSource.Rows[i]["UNIT_PRICE"].ToString(), 7, true));
                        //tempString = tempString.Replace("<TAXAMT>", RowColAlign(dataSource.Rows[i]["TAX_AMOUNT"].ToString(), 5, true));
                        tempString = tempString.Replace("<QTY>", RowColAlign(dataSource.Rows[i]["refund_qty"].ToString(), 3, true));
                        tempString = tempString.Replace("<AMOUNT>", RowColAlign(dataSource.Rows[i]["refund_amt"].ToString(), 9, true));
                        //tempString = tempString.Replace("<ACTUAL_REFUND>", RowColAlign(dataSource.Rows[i]["ACTUAL_REFUND"].ToString(), 10, true));
                        tempString = tempString.Replace("<FREE_CARE>", RowColAlign(dataSource.Rows[i]["ref_free_care"].ToString(), 8, true));
                        FileSource = FileSource + tempString;
                        FileSource = FileSource + "\r\n";
                    }
                    break;
                case "GeneralBill":
                    for (int i = 0; i < dataSource.Rows.Count; i++)
                    {
                        tempString = tempFileSource;

                        if (Convert.ToString(i + 1).Length < 2)
                            tempString = tempString.Replace("<SN>", "0" + Convert.ToString(i + 1));
                        else
                            tempString = tempString.Replace("<SN>", Convert.ToString(i + 1));

                        tempString = tempString.Replace("<DESCR>", RowColAlign(dataSource.Rows[i]["account_head_name"].ToString(), 30, false));
                        tempString = tempString.Replace("<QTY>", RowColAlign(dataSource.Rows[i]["qty"].ToString(), 5, true));
                        tempString = tempString.Replace("<UNITPRICE>", RowColAlign(dataSource.Rows[i]["unit_price"].ToString(), 8, true));
                        tempString = tempString.Replace("<AMOUNT>", RowColAlign(dataSource.Rows[i]["amount"].ToString(), 8, true));
                        //tempString = RowAlign(tempString, 50);
                        FileSource = FileSource + tempString;
                        FileSource = FileSource + "\r\n";
                    }
                    break;

                case "DueCollectionPrint":
                    decimal total_bill_amount = 0;
                    decimal total_discount = 0;
                    decimal total_amount_paid = 0;
                    decimal total_due = 0;
                    for (int i = 0; i < dataSource.Rows.Count; i++)
                    {
                        tempString = tempFileSource;

                        if (Convert.ToString(i + 1).Length < 2)
                            tempString = tempString.Replace("<SN>", "0" + Convert.ToString(i + 1));
                        else
                            tempString = tempString.Replace("<SN>", Convert.ToString(i + 1));

                        tempString = tempString.Replace("<BILLNO>", RowColAlign(dataSource.Rows[i]["bill_number"].ToString(), 10, false));
                        tempString = tempString.Replace("<BILLDATE>", RowColAlign(dataSource.Rows[i]["bill_date"].ToString(), 10, true));
                        tempString = tempString.Replace("<BILLAMT>", RowColAlign(dataSource.Rows[i]["bill_amount"].ToString(), 8, true));
                        tempString = tempString.Replace("<CONS>", RowColAlign(dataSource.Rows[i]["discount"].ToString(), 8, true));
                        tempString = tempString.Replace("<AMTPAID>", RowColAlign(dataSource.Rows[i]["amount_paid"].ToString(), 8, true));
                        tempString = tempString.Replace("<DUE>", RowColAlign(dataSource.Rows[i]["due"].ToString(), 8, true));

                        total_bill_amount = total_bill_amount + objBusinessFacade.NonBlankValueOfDecimal(dataSource.Rows[i]["bill_amount"].ToString());
                        total_discount = total_discount + objBusinessFacade.NonBlankValueOfDecimal(dataSource.Rows[i]["discount"].ToString());
                        total_amount_paid = total_amount_paid + objBusinessFacade.NonBlankValueOfDecimal(dataSource.Rows[i]["amount_paid"].ToString());
                        total_due = total_due + objBusinessFacade.NonBlankValueOfDecimal(dataSource.Rows[i]["due"].ToString());

                        //tempString = RowAlign(tempString, 50);
                        FileSource = FileSource + tempString;
                        FileSource = FileSource + "\r\n";
                    }
                    tempString = tempString.Replace("<TOTALAMT>", RowColAlign(total_bill_amount.ToString(), 10, false));
                    tempString = tempString.Replace("<TOTALAMOUNTPAID>", RowColAlign(total_discount.ToString(), 10, false));
                    tempString = tempString.Replace("<TOTALDUE>", RowColAlign(total_amount_paid.ToString(), 10, false));
                    tempString = tempString.Replace("<TOTALDISCOUNT>", RowColAlign(total_due.ToString(), 10, false));
                    break;
            }
            return FileSource;
        }

        #endregion

        public static string GetCurrentTime(string format)
        {
            string Time = "";
            try
            {
                Time = DateTime.Now.ToString(format);
            }
            catch (Exception) { }
            return Time;
        }

        private void btnPint_Click(object sender, EventArgs e)
        {
            //DosPrinting objdos = new DosPrinting();
            //string PrinterName = objdos.GetDefaultPrinter();
            //// PrinterName = ConfigurationManager.AppSettings["Printer"].ToString();
            //objdos.SendStringToPrinter(PrinterName, lblPreview.Text);
            //this.Close();

            DosPrinting objdos = new DosPrinting();
            string PrinterName = objdos.GetDefaultPrinter();
            // PrinterName = ConfigurationManager.AppSettings["Printer"].ToString();

            PrintDialog printDlg = new PrintDialog();
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "fileName";
            printDlg.Document = printDoc;
            printDlg.AllowSelection = true;
            printDlg.AllowSomePages = true;


            //Call ShowDialog
            if (printDlg.ShowDialog() == DialogResult.OK)
            {
                verdana10Font = new Font("Courier New", 10);
                printDoc.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                printDoc.Print();
            }
            this.Close();
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            //Get the Graphics object
            Graphics g = ev.Graphics;
            Font printFont = new Font("Courier New", 10, FontStyle.Bold);
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            string line = null;

            //Read margins from PrintPageEventArgs
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = 0;        //ev.MarginBounds.Top;

            //Calculate the lines per page on the basis of the height of the page and the height of the font
            linesPerPage = ev.MarginBounds.Height /
            verdana10Font.GetHeight(g);
            string measureString = string.Empty;

            StringReader reader = new StringReader(lblPreview.Text);
            while ((line = reader.ReadLine()) != null)
            {
                //Calculate the starting position
                yPos = topMargin + (count *
                verdana10Font.GetHeight(g));

                if (count < linesPerPage && line.Contains("<BLD>"))
                {
                    line = line.Replace("<BLD>", "$^").Replace("</BLD>", @"^$");
                    string[] bldletter = line.Split('$');

                    float leftMarginCal = leftMargin;

                    for (int i = 0; i < bldletter.Count(); i++)
                    {
                        if (i == 0)
                        {
                            if (bldletter[i].Contains("^"))
                            {

                                ev.Graphics.DrawString(bldletter[i].Replace("^", string.Empty), new Font("Courier New", 10, FontStyle.Bold), Brushes.Black,
                                                leftMargin, yPos, new StringFormat());
                            }
                            else
                            {
                                ev.Graphics.DrawString(bldletter[i], new Font("Courier New", 10), Brushes.Black,
                                                leftMargin, yPos, new StringFormat());
                            }
                        }
                        else
                        {
                            // Measure string.
                            SizeF stringSize = new SizeF();
                            stringSize = ev.Graphics.MeasureString(measureString, new Font("Courier New", 10, FontStyle.Bold));
                            if (bldletter[i].Contains("^"))
                            {
                                ev.Graphics.DrawString(bldletter[i].Replace("^", string.Empty), new Font("Courier New", 10, FontStyle.Bold), Brushes.Black,
                                                stringSize.Width + leftMargin, yPos, new StringFormat());
                            }
                            else
                            {
                                ev.Graphics.DrawString(bldletter[i], new Font("Courier New", 10), Brushes.Black,
                                                stringSize.Width + leftMargin, yPos, new StringFormat());
                            }
                        }
                        measureString += bldletter[i];
                    }
                    measureString = string.Empty;
                }
                else
                {
                    ev.Graphics.DrawString(line, verdana10Font, Brushes.Black,
                                    leftMargin, yPos, new StringFormat());
                }
                count++;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrintReceipt_Load(object sender, EventArgs e)
        {

        }
    }

    /*public static class SplitMethod
        {
            public static List<string> SplitOn(this string initial, int MaxCharacters)
            {

                List<string> lines = new List<string>();

                if (string.IsNullOrEmpty(initial) == false)
                {
                    string targetGroup = "Line";
                    string theRegex = string.Format(@"(?<{0}>.{{1,{1}}})(?:\W|$)", targetGroup, MaxCharacters);
                    MatchCollection matches = Regex.Matches(initial, theRegex, RegexOptions.IgnoreCase
                                                                              | RegexOptions.Multiline
                                                                              | RegexOptions.ExplicitCapture
                                                                              | RegexOptions.CultureInvariant
                                                                              | RegexOptions.Compiled);
                    if (matches != null)
                        if (matches.Count > 0)
                            foreach (Match m in matches)
                                lines.Add(m.Groups[targetGroup].Value.TrimStart().TrimEnd());
                }

                return lines;
            }
        }*/

}
