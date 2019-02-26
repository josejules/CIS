using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIS.Common
{
    public class clsLabCommon
    {
        public class Laboratory
        {
            public class LabBillInfo
            {
                public const string FromBillDate = "FromBillDate";
                public const string ToBillDate = "ToBillDate";
            }
            public class Category
            {
                public const string CategoryId = "CategoryId";
                public const string CategoryName = "CategoryName";
            }

            public class TestField
            {
                public const string TestFieldId = "Test_Field_Id";
                public const string TestFieldName = "Test_Field_Name";
                public const string TestFieldTypeId = "Test_Field_Type_Id";
                public const string TestFieldTypeName = "Test_Field_Type_Name";
                public const string Unit = "Unit";
                public const string ReferenceValue = "Reference_Value";
            }

            public class TestItem
            {
                public const string TestItemId = "Test_Item_Id";
                public const string TestItemName = "Test_Item_Name";
                public const string TestFieldId = "Test_Field_Id";
            }

            public class TestFieldMapping
            {
                public const string InvestigationId = "investigation_id";
                public const string TestFieldId = "test_field_id";
                public const string VisibleOrder = "visible_order";
            }

            public class TestResultEntry
            {
                public const string ResultEntryId = "result_entry_id";
                public const string ResultEntryDetailId = "result_entry_detail_id";
                public const string BillId = "bill_id";
                public const string ReceivedDateTime = "received_date_time";
                public const string ReportedDateTime = "reported_date_time";
                public const string InvestigationId = "investigation_id";
                public const string TestFieldId = "test_field_id";
                public const string ResultValue = "result_value";
            }
        }
    }

    public class User
    {
        public const string UserId = "user_id";
        public const string FullName = "full_name";
        public const string Username = "user_name";
        public const string Password = "password";
        public const string UserType = "user_type";
        public const string Status = "status";
        public class UserRole
        {
            public const string RoleId = "role_id";
            public const string RoleName = "role_name";
        }
        public class UserMapRoleAction
        {
            public const string RoleId = "role_id";
            public const string ActionId = "action_id";
        }
    }

    public class DischargeSummary
    {
        public const string DischargeSummaryId = "discharge_summary_id";
        public const string DischargeSummaryNumber = "discharge_summary_number";
        public const string DischargeSummaryDate = "discharge_summary_date";
        public const string PatientId = "patient_id";
        public const string VisitNumber = "visit_number";
        public const string DischargeDoctorId = "discharge_doctor_id";
        public const string DischargeDate = "discharge_date";
        public const string Diagnosis = "diagnosis";
        public const string OperativeFindings = "operative_findings";

        public const string Procedure = "procedure";
        public const string Summary = "summary";
        public const string ConditionAtAdmission = "condition_at_admission";
        public const string Investigations = "investigations";
        public const string TreatmentGiven = "treatment_given";
        public const string ConditionAtDischarge = "condition_at_discharge";
        public const string TreatmentAdviced = "treatment_adviced";
        public const string ReviewOn = "review_on";
        public const string FollowupDetailId = "followup_detail_id";
        public const string DoctorId = "doctor_id";
        public const string VisitingDate = "visiting_date";
        public const string Remarks = "remarks";

        public const string LastUpdatedUser = "last_updated_user";
        public const string LastUpdated = "last_updated";
        public const string IsDeleted = "is_deleted";
    }

    public class Master
    {
        public class PrinterSettings
        {
            public const string SettingId = "setting_id";
            public const string ReportName = "report_name";
            public const string PrinterName = "printer_name";
            public const string IsLazerPrinter = "is_lazer_printer";
        }
    }
    public class Messages
    {
        public const string Msg_Title = "CIS ";
    }

    public class LabDynamicControl
    {
        public string ctrlName;
        public string ctrlValue;
    }

    public enum PrinterSetting
    {
        RegistrationSlip,
        RegistrationBill,
        InvestigationBill,
        PharmacyBill,
        GeneralBill,
        WardBill,
        AdvanceBill
    }
}
