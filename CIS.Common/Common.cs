using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CIS.Common
{
    public class Common
    {
        #region Logged-in User details
        public static string sLoggedUser;
        public static int userId;
        public static int userRoleId;
        public static int userType;
        public static int billId;
        public static int billTypeId;
        public static string billNo;
        public static int trans_id;
        public static string dateBy;
        public static string reportName;
        #endregion

        public static int id = 0;
        public static int flag;
        public static int status;
        public static int gender;
        public static int paymentModeId;
        public static string cardNumber;
        public static string bankName;
        public static string holderName;
        public static DateTime transDate;
        public static string startDate;
        public static string endDate;
        public static decimal totalCollectionAmount;
        public static decimal totalAmountPaid;
        public static decimal totalDiscount;
        public static decimal totalAdvAdj;
        public static decimal totalDue;
        public static decimal totalDueCollection;
        public static decimal remainigTempAmt;
        public static decimal totalInvoice;
        public static decimal alreadyPaid;
        public static decimal refundFreeCare;

        public static decimal totalSum;
        public static decimal totalRefundSum;
        public static decimal freeCareTotalSum;
        public static decimal freeCareTotalRefundSum;

        public class cis_department
        {
            public static int departmentId;
            public static string departmentCode;
            public static string departmentName;
            public static int departmentType;
            public static int departmentCategoryId;
            public static string departmentCategory;
            public static int medicalDepartmentId;
            public static int visitMode;
        }

        public class cis_patientType
        {
            public static int patientTypeId;
            public static string patientType;
        }

        public class cis_Doctor
        {
            public static int doctorId;
            public static string doctorCode;
            public static string doctorName;
            public static int doctorType;
            public static string qualification;
            public static string specialization;
            public static string roomNumber;
        }

        public class cis_Address_Info
        {
            public static int addressId;
            public static string place;
            public static string district;
            public static string state;
            public static string postalCode;
        }

        public class cis_Corporate
        {
            public static int corporateId;
            public static int isFeeApplicable;
            public static string corporateName;
            public static string address;
        }

        public class cis_Room
        {
            public static int roomId;
            public static int roomFrom;
            public static int roomTo;
            public static string roomPrefix;
            public static int ward_id;
            public static string roomNo;
        }

        public class cis_bed
        {
            public static int bedId;
            public static string bedNo;
            public static DateTime startDate;
            public static DateTime endDate;
            public static int bedStatus;
        }

        public class cis_Define_Reg_Fee
        {
            public static int define_reg_fee_id;
            public static decimal newRegFee = 0;
            public static int validity;
            public static int validityPeriod;
            public static decimal revisitRegFee = 0;
        }

        public class module_visit_info
        {
            public static int account_head_id = 1;
            public static string account_head_name = "Registration Fee";
            public static int account_type = 1;
            public static decimal consultationFee  = 0;
            public static int regDiscountType;
            public static decimal discountPercOrAmtReg = 0;
            public static decimal discountAmountReg = 0;
            public static decimal netTotalReg = 0;
            public static decimal amountPaidReg = 0;
            public static decimal balanceAmountReg = 0;
            public static decimal dueCollectionReg = 0;
            public static decimal refundToPatientReg = 0;
            public static string diagnosis;
            public static DateTime regBillDate;
            public static int regPaymentStatus;
            public static int dichargeTypeId;
            public static string dischargeType;
            public static DateTime dischargeDate;
            public static DateTime? expiryDate;
            public static DateTime transferDate;
            public static int referralId;
            public static string referralName;
            public static string referralContactNo;
            public static string referralContactAddress;
        }

        public class cis_number_generation
        {
            public static int number_format_id;
            public static string field_name;
            public static string number_format;
            public static string prefix_date;
            public static string prefix_text;
            public static string last_bill_number;
            public static int running_number;
            public static string patient_id;
            public static int running_patient_id;
            public static string number_format_patient_id;
            public static string visit_number;
            public static int running_visit_number;
            public static string token_number;
            public static int running_token_number;
            public static string reg_bill_number;
            public static int running_reg_bill_number;
            public static string investigation_bill_number;
            public static int running_investigation_bill_number;
            public static string pharmacy_bill_number;
            public static int running_pharmacy_bill_number;
            public static string general_bill_number;
            public static int running_general_bill_number;
            public static string purchase__number;
            public static int running_purchase_receipt_number;
            public static string purchase_receipt_number;
            public static int running_adv_collection_number;
            public static string adv_collection_number;
            public static int running_adv_refund_number;
            public static string adv_refund_number;
            public static int running_ward_bill_number;
            public static string ward_bill_number;
            public static int running_inventory_movements_number;
            public static string inventory_movements_number;
        }

        public class cis_patient_info
        {
            public static DateTime visit_date;
            public static DateTime last_visit_date;
            public static int social_title_id;
            public static string social_title;
            public static string patient_name;
            public static int age_year = 0;
            public static int age_month = 0;
            public static int age_day = 0;
            public static DateTime dob;
            public static string guardian_name;
            public static string address;
            public static string phone_no;
        }

        public class cis_investigation_info
        {
            public static int investigationId;
            public static string investigationCode;
            public static string investigationName;
            public static int investigationDeptId;
            public static string investigationDeptName;
            public static int investigationQty;
            public static decimal investigationUnitPrice = 0;
            public static decimal investigationTotal = 0;
            public static decimal investigationTotalSum = 0;
            public static decimal investigationDiscountAmt = 0;
            public static decimal netTotalInvestigation = 0;
            public static decimal amountPaidInvestigation = 0;
            public static decimal balanceAmountInvestigation = 0;
            public static string selectedInvTab;
            public static int invCategoryId;
            public static string invCategory;
            public static decimal investigationAmtPaid = 0;
            public static int invPaymentStatus = 0;
            public static decimal invRefundTotalSum = 0;
            public static decimal oldInvAmtPaid = 0;
            public static int ShareType;
            public static int AmtType;
            public static decimal sharePerc = 0;
            public static decimal shareAmt = 0;
        }

        public class cis_pharmacy_info
        {
            public static int inventoryStockId;
            public static int phaDeptId;
            public static int phaItemTypeId;
            public static string phaItemType;
            public static string HSNCode;
            public static int phaItemId;
            public static string phaItemName;
            public static int investigationDeptId;
            public static string lotId;
            public static int phaQty;
            public static int transPhaQty;
            public static int physicalPhaQty;
            public static int phaTotalQty;
            public static string phaExpDate;
            public static decimal phaUnitPrice = 0;
            public static decimal phaFreeCare = 0;
            public static decimal phaFreeCareTotal = 0;
            public static decimal phaDiscountAmt = 0;
            public static decimal phaTotalAmt = 0;
            public static decimal phaRefundTotalSum = 0;
            public static decimal oldPhaTotalAmt = 0;
            public static decimal oldPhaAmtPaid = 0;
            public static decimal phaTotalSum = 0;
            public static string selectedPhaTab;
            public static decimal phaAmtPaid = 0;
            public static decimal balanceAmountPha = 0;
            public static decimal netTotalPha = 0;
            public static decimal refundAmt = 0;
            public static int phaPaymentStatus;

            public static int taxId;
            public static decimal taxRate = 0;
            public static int vendorId;
            public static string vendorName;
            public static string TINNo;
            public static string vendorContactInfo;
            public static string vendorContactAddress;
            public static decimal salesTaxPerc;

            public static string invoiceNumber;
            public static DateTime invoiceDate;
            public static int taxFormula;
            public static DateTime receiveDate;

            public static int packX;
            public static int packY;
            public static int receiveQty;
            public static int offerQty;
            public static int cancelledQty;
            public static decimal vendorPrice;
            public static decimal MRP;
            public static decimal totalAmtPur;
            public static decimal discountPercPur;
            public static decimal discountAmtPur;
            public static decimal taxPercPur;
            public static decimal taxAmtPur;
            public static decimal freeTaxAmtPur;
            public static decimal netAmtPur;
            public static DateTime purExpDate;
            public static Boolean vpActive = false;
            public static Boolean mrpActive = false;

            public static decimal totalAmtPurSum = 0;
            public static decimal discountPurSum = 0;
            public static decimal taxPurSum = 0;
            public static decimal discountCashPurSum = 0;
            public static decimal discountItemPurSum = 0;
            public static decimal freeTaxPurSum = 0;
            public static decimal retunedAmtPurSum = 0;
            public static decimal AmtPaidPurSum = 0;
            public static decimal roundOffPurSum = 0;
            public static decimal balancePurSum = 0;
            public static int cboFreeTaxStatus = 0;
            public static decimal netTotalPurSum = 0;
            public static decimal refundFreeCare= 0;
            public static decimal refundToPatient = 0;
            public static decimal dueCollection = 0;
            public static int investoryMoveTypeId = 0;
            public static int consumeId = 0;
        }

        public class cis_billing
        {
            public static int accountHeadId;
            public static string accountName;
            public static string accountGroupName;
            public static int isAccountGroup;
            public static int accountGroupId;
            public static int billTypeId;
            public static decimal billUnitPrice = 0;
            public static decimal genUnitPrice = 0;
            public static decimal genTotal = 0;
            public static int qtyGen;
            public static decimal genTotalSum;
            public static decimal genDiscountAmt = 0;
            public static decimal genAmtPaid = 0;
            public static decimal balanceAmountGen = 0;
            public static decimal netTotalGen = 0;
            public static int genPaymentStatus;

            public static decimal wardUnitPrice = 0;
            public static int qtyWard;
            public static decimal wardTotal = 0;

            /*public static decimal wardTotalSum;
            public static decimal wardDiscountAmt = 0;
            public static decimal wardAmtPaid = 0;
            public static decimal balanceAmountWard = 0;
            public static decimal netTotalWard = 0;*/


            public static decimal advanceCollection;
            public static decimal advanceRefund;
            public static decimal advanceAdjustment;
            public static decimal totalAdvanceAvailable;
            public static decimal totalAdvNetColl;

            public static decimal totalWardAmt;
            public static decimal netlWardAmt;
            public static decimal discountWardAmt;
            public static int discountType;
            public static decimal discountPercOrAmtWard;
            public static decimal amtPaidWardAmt;
            public static decimal dueWardAmt;
            public static int wardPaymentStatus;
        }

        public class ExceptionHandler
        {
            public static string errorPath;
            public static void ExceptionWriter(Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(errorPath+ @"\Error.txt"))
                {
                    writer.WriteLine(Environment.NewLine);
                    writer.WriteLine(DateTime.Now + ":::" + ex.Message + ":::" + ex.StackTrace);
                }
            }
        }
    }
    public class DateFormatInfo
    {
        public const string TimeFormat = "HH:mm:ss";
        public class MySQLFormat
        {
            //for insert/update
            public const string DateAndTimeUpdate = "yyyy-MM-dd HH:mm:ss";
            public const string DateAndTimeNoformatBegin = "yyyyMMdd" + "000000";
            public const string DateAndTimeNoformatEnd = "yyyyMMdd" + "235959";
            public const string DateTimeWithhours = "'%d-%m-%Y %h:%i %p'";
            public const string DateUpdate = "yyyy-MM-dd";
            public const string TimeStampUpdate = "0000-00-00 HH:mm:ss";
        }
    }
}
