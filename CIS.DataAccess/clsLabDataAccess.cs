using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CIS.DBHandler;
using CIS.Common;
using MySql.Data.MySqlClient;

namespace CIS.DataAccess
{
    public class clsLabDataAccess
    {
        #region Variables
        string query = string.Empty;
        DataTable dtSource = null;
        DBHandler.DBHandler objDBHandler = new DBHandler.DBHandler();
        int flag = 0;
        //ComArugments objArg = new ComArugments();
        #endregion

        #region Common
        public int getLastInsertedId()
        {
            string last_inserted_id = "select last_insert_id()";
            string rowId = objDBHandler.ExecuteTransactionQuery(last_inserted_id); // Get Last Inserted Transaction Id
            int bill_id = Convert.ToInt32(rowId);
            //objDBHandler.commitTransaction();
            return bill_id;
        }

        public void commitTransction()
        {
            objDBHandler.commitTransaction(TransactionType.Commit);
        }
        public void rollBackTransation()
        {
            objDBHandler.commitTransaction(TransactionType.Rollback);
        }
        #endregion

        #region Laboratory

        #region Category
        public DataTable LoadCategory()
        {
            query = @"SELECT CATEGORY_ID,CATEGORY_NAME FROM LAB_CATEGORY";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int AddCategory(ComArugments objArg)
        {
            int categoryId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.Category.CategoryId].ToString());
            string category = objArg.ParamList[clsLabCommon.Laboratory.Category.CategoryName].ToString();
            if (categoryId == 0)
                query = "INSERT INTO LAB_CATEGORY(category_name) VALUES ('" + category + "')";
            else
                query = "UPDATE LAB_CATEGORY SET category_name = '" + category + "' WHERE category_id =" + categoryId;
            flag = objDBHandler.ExecuteCommand(query);
            return flag;
        }

        public DataTable FetchCategoryInfoById(int categoryId)
        {
            query = @"SELECT CATEGORY_ID,CATEGORY_NAME FROM LAB_CATEGORY where category_id = " + categoryId;
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int DeleteCategory(int categoryId)
        {
            query = @"DELETE FROM LAB_CATEGORY where category_id = " + categoryId;
            return objDBHandler.ExecuteCommand(query); ;
        }
        #endregion

        #region Lab Test Field
        public DataTable LoadTestFieldTypes()
        {
            query = @"SELECT test_field_type_id, test_field_type_name FROM lab_test_field_type";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public DataTable LoadTestFields()
        {
            query = @"SELECT 
                        test_field_Id,
                        test_field_name as 'Test Field Name',
                        test_field_type_name as 'Test Field Type',
                        unit as 'Unit',
                        reference_value as 'Reference Value'
                    FROM
                        lab_test_field tf
                            left join
                        lab_test_field_type tft ON tf.test_field_type_id = tft.test_field_type_id";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public DataTable LoadTestItems(int fieldId)
        {
            query = @"SELECT test_item_id, test_item_name, test_field_id FROM lab_test_item where test_field_id = " + fieldId;
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int AddEditTestField(ComArugments objArg)
        {
            int fieldId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestField.TestFieldId].ToString());
            string fieldName = objArg.ParamList[clsLabCommon.Laboratory.TestField.TestFieldName].ToString();
            Int16 fieldTypeId = Convert.ToInt16(objArg.ParamList[clsLabCommon.Laboratory.TestField.TestFieldTypeId].ToString());
            string unit = objArg.ParamList[clsLabCommon.Laboratory.TestField.Unit].ToString();
            string referenceValue = objArg.ParamList[clsLabCommon.Laboratory.TestField.ReferenceValue].ToString();

            if (fieldId == 0)
            {
                query = "INSERT INTO lab_test_field(test_field_name, test_field_type_id, unit, reference_value) " +
                        "VALUES ('" + fieldName + "'," + fieldTypeId + ",'" + unit + "','" + referenceValue + "')";
                if (fieldTypeId == 2)
                    flag = objDBHandler.ExecuteCommandTransaction(query);
                else
                    flag = objDBHandler.ExecuteCommand(query);
            }
            else
            {
                query = "UPDATE lab_test_field SET test_field_name = '" + fieldName + "',test_field_type_id =" + fieldTypeId + ", Unit ='" + unit + "', reference_value ='" + referenceValue + "'" +
                        "WHERE test_field_id =" + fieldId;
                flag = objDBHandler.ExecuteCommand(query);
            }
            return flag;
        }

        public int AddEditTestItems(ComArugments objArg)
        {
            int itemId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestItem.TestItemId].ToString());
            string itemName = objArg.ParamList[clsLabCommon.Laboratory.TestItem.TestItemName].ToString();
            int fieldId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestField.TestFieldId].ToString());

            if (itemId == 0)
            {
                query = "INSERT INTO lab_test_item(test_item_name, test_field_id) " +
                        "VALUES ('" + itemName + "'," + fieldId + ")";
            }
            else
            {
                query = "UPDATE lab_test_item SET test_item_name = '" + itemName + "',test_field_id =" + fieldId +
                        " WHERE test_item_id =" + itemId;
            }
            flag = objDBHandler.ExecuteCommand(query);
            return flag;
        }

        public DataTable FetchTestFieldInfoById(int fieldId)
        {
            query = @"SELECT test_field_id, test_field_name, test_field_type_id, unit, reference_value FROM lab_test_field where test_field_id = " + fieldId;
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int DeleteTestField(int fieldId)
        {
            query = @"DELETE FROM lab_test_item where test_field_id = " + fieldId + "; " +
                "DELETE FROM lab_test_field where test_field_id = " + fieldId + ";";
            return objDBHandler.ExecuteCommand(query);
        }

        public int DeleteTestItem(int itemId)
        {
            query = @"DELETE FROM lab_test_item where test_item_id = " + itemId;
            return objDBHandler.ExecuteCommand(query); ;
        }
        #endregion

        #region LabTestFieldMapping
        public DataTable LoadTestClass()
        {
            query = @"select investigation_id,investigation_name from cis_investigation_list where status =1 ";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }
        public DataTable LoadUnMappedTestFields(int investigationId)
        {
            query = @"select test_field_id, test_field_name as 'Test Fields' from lab_test_field
                        where test_field_id not in 
                            (select test_field_id from lab_test_field_mapping where investigation_id = " + investigationId + ")";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }
        public DataTable LoadMappedTestFields(int investigationId)
        {
            query = @"select test_field_id, test_field_name as 'Test Fields' from lab_test_field
                        where test_field_id in 
                            (select test_field_id from lab_test_field_mapping where investigation_id = " + investigationId + ")";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }
        public int DeleteMappedTestFields(int investigationId)
        {
            query = @"delete from lab_test_field_mapping where investigation_id=" + investigationId;
            return objDBHandler.ExecuteCommand(query);
        }
        public int SaveMappedTestFields(ComArugments objArg)
        {
            int investigationId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestFieldMapping.InvestigationId].ToString());
            string testfieldId = objArg.ParamList[clsLabCommon.Laboratory.TestFieldMapping.TestFieldId].ToString();
            int visibleOrder = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestFieldMapping.VisibleOrder].ToString());

            query = "insert into lab_test_field_mapping(investigation_id,test_field_id,visible_order) " +
                        "VALUES (" + investigationId + ",'" + testfieldId + "'," + visibleOrder + ")";
            flag = objDBHandler.ExecuteCommand(query);
            return flag;
        }
        #endregion

        #region ResultEntryView
        public DataTable LoadPatientDetails(string criteria)
        {
            query = @"select bi.bill_id,bill_number,patient_id,bill_date,visit_number,patient_name,
	                            if(gender = 1, 'Female', 'Male') as gender,age_year, result_entry_id,
		                        received_date_time,
		                        reported_date_time
                        from inv_bill_info bi
                        left join lab_test_result_entry re on re.bill_id = bi.bill_id
                    where " + criteria;
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }
        public DataTable LoadTestDetails(int billId)
        {
            query = @"select 
                        il.investigation_id,il.investigation_code as 'Test Code',il.investigation_name as 'Test Name'
                    from inv_bill_datail_info cbd
	                    join cis_investigation_list il ON cbd.investigation_id = il.investigation_id
                    where bill_id = " + billId;
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }
        #endregion

        #region ResultEntry
        public DataTable LoadResultEntryInfo(int billId)
        {
            query = @"select 
	                    tf.test_field_id,inv_category,il.investigation_id,visible_order,
                        investigation_name,    test_field_name,
                        test_field_type_name,    unit,    reference_value
                    from inv_bill_datail_info ibd
                            join    lab_test_field_mapping tfm ON ibd.investigation_id = tfm.investigation_id
                            join    cis_investigation_list il ON tfm.investigation_id = il.investigation_id
                            join    lab_test_field tf ON tfm.test_field_id = tf.test_field_id
                            join    lab_test_field_type ft ON tf.test_field_type_id = ft.test_field_type_id
		                    join    cis_investigation_category cat on cat.inv_category_id = il.investigation_category_id
                    where    bill_id = " + billId +
                    " order by il.investigation_id,visible_order";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int SaveResultEntryInfo(ComArugments objArg)
        {
            int resultEntryId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultEntryId].ToString());
            int billId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestResultEntry.BillId].ToString());
            string receivedDateTime = Convert.ToDateTime(objArg.ParamList[clsLabCommon.Laboratory.TestResultEntry.ReceivedDateTime].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
            string reportedDateTime = Convert.ToDateTime(objArg.ParamList[clsLabCommon.Laboratory.TestResultEntry.ReportedDateTime].ToString()).ToString("yyyy-MM-dd hh:mm:ss");

            if (resultEntryId == 0)
                query = "insert into lab_test_result_entry(bill_id,received_date_time,reported_date_time) " +
                    "values (" + billId + ",'" + receivedDateTime + "','" + reportedDateTime + "')";
            else
                query = "update lab_test_result_entry set bill_id = " + billId + ",received_date_time = '" + receivedDateTime + "',reported_date_time='" + reportedDateTime + "' "
                            + "where result_entry_id=" + resultEntryId;
            flag = objDBHandler.ExecuteCommandTransaction(query);
            if (resultEntryId == 0)
                flag = getLastInsertedId();
            return flag;
        }

        public int SaveResultEntryDetailInfo(ComArugments objArg)
        {
            int resultEntryId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultEntryId].ToString());
            int investigationId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestResultEntry.InvestigationId].ToString());
            int fieldId = Convert.ToInt32(objArg.ParamList[clsLabCommon.Laboratory.TestResultEntry.TestFieldId].ToString());
            string resultValue = objArg.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultValue].ToString();

            query = "insert into lab_test_result_detail_entry (result_entry_id, investigation_id,test_field_id,result_value) " +
                    "values (" + resultEntryId + "," + investigationId + "," + fieldId + ",'" + resultValue + "')";
            flag = objDBHandler.ExecuteCommand(query);
            return flag;
        }

        public DataTable EditResultEntryInfo(int resultEntryId)
        {
            query = @"select re.result_entry_id,tf.test_field_id,inv_category as 'category',il.investigation_id,
                            investigation_name,    test_field_name,
                            test_field_type_name,    unit,    reference_value,result_value
                        from lab_test_result_entry re
                        join lab_test_result_detail_entry red on re.result_entry_id = red.result_entry_id
                        join    lab_test_field tf ON red.test_field_id = tf.test_field_id 
                        join    lab_test_field_type ft ON tf.test_field_type_id = ft.test_field_type_id
                        join    cis_investigation_list il ON red.investigation_id = il.investigation_id
                        join    cis_investigation_category cat on cat.inv_category_id = il.investigation_category_id
                        where re.result_entry_id = " + resultEntryId +
                        " order by result_entry_detail_id";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int DeletePatientResultEntry(int resultEntryId)
        {
            query = @"delete from lab_test_result_detail_entry where result_entry_id =" + resultEntryId;
            return objDBHandler.ExecuteCommand(query);
        }

        public DataTable LoadResultEntryInfoForLabPrintout(int billId)
        {
            query = @"select tf.test_field_id,inv_category as 'category',il.investigation_id,
                        investigation_name,    test_field_name, bill_number, 
                        test_field_type_name,    unit,    reference_value,result_value,
	                    patient_name, patient_id, visit_number, if(gender = 1, 'Female', 'Male') as gender, age_year, 
	                    doctor_name, date_format(bill_date, '%d/%m/%Y %h:%i %p') as bill_date,
						date_format(received_date_time, '%d/%m/%Y %h:%i %p') as received_date_time,
						date_format(reported_date_time, '%d/%m/%Y %h:%i %p') as reported_date_time
                    from lab_test_result_entry re
					join lab_test_result_detail_entry red on re.result_entry_id = red.result_entry_id
                    join    lab_test_field tf ON red.test_field_id = tf.test_field_id 
                    join    lab_test_field_type ft ON tf.test_field_type_id = ft.test_field_type_id
                    join    cis_investigation_list il ON red.investigation_id = il.investigation_id
                    join    cis_investigation_category cat on cat.inv_category_id = il.investigation_category_id
                    join inv_bill_info ibf on ibf.bill_id = re.bill_id
                    where    re.bill_id = " + billId +
                    " order by inv_category,red.result_entry_detail_id";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }
        #endregion

        #region LabBillView
        public DataTable LoadLabBillInfo(ComArugments objArg)
        {
            string fromBillDate = CommonUtility.GetMySQLDateTime((objArg.ParamList[clsLabCommon.Laboratory.LabBillInfo.FromBillDate].ToString()), DateDataType.DateNoFormatBegin);
            string toBillDate = Convert.ToDateTime(objArg.ParamList[clsLabCommon.Laboratory.LabBillInfo.ToBillDate].ToString()).ToString("yyyyMMdd" + "235959");
            query = @"select 
                            bi.bill_id,
                            bill_number as 'Bill Number',
                            patient_id as 'Patient Id',
                            visit_number as 'Visit Number',
                            patient_name as 'Patient Name',
                            if(gender = 1, 'Female', 'Male') as 'Gender',
                            age_year as 'Age', result_entry_id,
                            date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date'
                        from
                            inv_bill_info bi
                            left join lab_test_result_entry re ON re.bill_id = bi.bill_id
                    where    bill_date between " + fromBillDate + " and " + toBillDate +
                    " order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }
        #endregion
        #endregion

        #region User
        #region UserRole
        public DataTable LoadUserRoles()
        {
            query = @"select role_id,role_name as 'User Role' from cis_roles";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int AddUserRole(ComArugments objArg)
        {
            int roleId = Convert.ToInt32(objArg.ParamList[User.UserRole.RoleId].ToString());
            string userRole = objArg.ParamList[User.UserRole.RoleName].ToString();
            if (roleId == 0)
                query = "INSERT INTO cis_roles(role_name) VALUES ('" + userRole + "')";
            else
                query = "UPDATE cis_roles SET role_name = '" + userRole + "' WHERE role_id =" + roleId;
            flag = objDBHandler.ExecuteCommand(query);
            return flag;
        }

        public DataTable FetchUserRoleInfoById(int roleId)
        {
            query = @"select role_id,role_name as 'User Role' from cis_roles where role_id = " + roleId;
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int DeleteUserRole(int roleId)
        {
            query = @"DELETE FROM cis_roles where role_id = " + roleId;
            return objDBHandler.ExecuteCommand(query); ;
        }
        #endregion

        #region User
        public DataTable LoadUserTypes()
        {
            query = @"select user_type_id,user_type from cis_user_type";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public DataTable LoadUsers()
        {
            query = @"select user_id,full_name as 'Full Name',user_name as 'User Name',
	                            r.role_name as 'User Role',t.user_type as 'User Type'
                            from cis_user u
	                        left join cis_roles r on u.user_role_id = r.role_id
	                        left join cis_user_type t on u.user_type = t.user_type_id
                        where u.status = 1";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int AddEditUser(ComArugments objArg)
        {
            int userId = Convert.ToInt32(objArg.ParamList[User.UserId].ToString());
            string fullName = objArg.ParamList[User.FullName].ToString();
            string username = objArg.ParamList[User.Username].ToString();
            string password = objArg.ParamList[User.Password].ToString();
            int roleId = Convert.ToInt32(objArg.ParamList[User.UserRole.RoleId].ToString());
            int userTypeId = Convert.ToInt32(objArg.ParamList[User.UserType].ToString());
            int status = 1; //Active Users
            if (userId == 0)
                query = "INSERT INTO cis_user(full_name,user_name,password,user_type,status,user_role_id) " +
                            " VALUES ('" + fullName + "','" + username + "','" + password + "'," + userTypeId + "," + status + "," + roleId + ")";
            else
                query = "UPDATE cis_user SET full_name = '" + fullName + "', user_name = '" + username + "', password = '" + password
                            + "', user_role_id =" + roleId + " ,status =" + status + " ,user_type =" + userTypeId + " WHERE user_id =" + userId;
            flag = objDBHandler.ExecuteCommand(query);
            return flag;
        }

        public DataTable FetchUserInfoById(int userId)
        {
            query = @"select user_id,full_name,user_name,password,user_type,status,user_role_id from cis_user where user_id = " + userId;
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int DeleteUser(int userId)
        {
            query = @"UPDATE cis_user set status = 0 where role_id = " + userId;
            return objDBHandler.ExecuteCommand(query); ;
        }
        #endregion

        #region UserActivityMapping
        public DataTable LoadUserActivities(int roleId)
        {
            query = @"select a1.action_id,a2.action as 'Module', a1.action as 'Activity',
	                        case when a3.action_id is null then false else true end as 'Visible'
                         from cis_actions a1
                         join cis_actions a2 on a1.parent = a2.action_id
                        left join cis_map_role_action a3 on a1.action_id = a3.action_id and a3.role_id =" + roleId +
                    " order by a1.parent,a1.action_order ";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int DeleteUserRoleActities(int roleId)
        {
            query = @"delete from cis_map_role_action where role_id=" + roleId;
            return objDBHandler.ExecuteCommandTransaction(query);
        }

        public int SaveUserRoleActivity(ComArugments objArg)
        {
            int roleId = Convert.ToInt32(objArg.ParamList[User.UserMapRoleAction.RoleId].ToString());
            int actionId = Convert.ToInt32(objArg.ParamList[User.UserMapRoleAction.ActionId].ToString());

            query = "insert into cis_map_role_action(role_id,action_id) " +
                        "VALUES (" + roleId + "," + actionId + ")";
            flag = objDBHandler.ExecuteCommand(query);
            return flag;
        }
        #endregion
        #endregion

        #region PrinterSetttings
        public DataTable LoadPrinterSettings()
        {
            query = @"select setting_id, report_name as 'Report Name', printer_name as 'Printer Name', 
                        case when is_lazer_printer  then 'Yes' else 'No' end as 'Is Lazer Printer' from printer_settings";
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int AddPrinterSetting(ComArugments objArg)
        {
            int settingId = Convert.ToInt32(objArg.ParamList[Master.PrinterSettings.SettingId].ToString());
            string reportName = objArg.ParamList[Master.PrinterSettings.ReportName].ToString();
            string printerName = objArg.ParamList[Master.PrinterSettings.PrinterName].ToString();
            bool isLazerPrinter = Convert.ToBoolean(objArg.ParamList[Master.PrinterSettings.IsLazerPrinter].ToString());
            if (settingId == 0)
                query = "INSERT INTO printer_settings(report_name,printer_name,is_lazer_printer) VALUES ('" + reportName + "','" + printerName + "'," + isLazerPrinter + ")";
            else
                query = "UPDATE printer_settings SET report_name = '" + reportName + "',printer_name='" + printerName + "',is_lazer_printer =" + isLazerPrinter + " WHERE setting_id =" + settingId;
            flag = objDBHandler.ExecuteCommand(query);
            return flag;
        }

        public DataTable FetchPrinterSettingInfoById(int settingId)
        {
            query = @"select setting_id, report_name, printer_name, is_lazer_printer from printer_settings where setting_id = " + settingId;
            dtSource = objDBHandler.ExecuteTable(query);
            return dtSource;
        }

        public int DeletePrinterSetting(int settingId)
        {
            query = @"DELETE FROM printer_settings where setting_id = " + settingId;
            return objDBHandler.ExecuteCommand(query); ;
        }
        #endregion

        #region DischargeSummary
        public DataTable GetDischargeSummary()
        {
            List<MySqlParameter> listparameter = new List<MySqlParameter>();
            DataSet ds = objDBHandler.ExecuteDateSet("sp_GetDischargeSummary", listparameter);
            return ds.Tables[0];
        }
        public int SaveDischargeSummary(ComArugments args)
        {
            int dischargesummaryid = Convert.ToInt32(args.ParamList[CIS.Common.DischargeSummary.DischargeSummaryId].ToString());
            string discharegesummaryNo = args.ParamList[CIS.Common.DischargeSummary.DischargeSummaryNumber].ToString();
            string patient_id = args.ParamList[CIS.Common.DischargeSummary.PatientId].ToString();
            string visit_no = args.ParamList[CIS.Common.DischargeSummary.VisitNumber].ToString();
            int doctor_id = Convert.ToInt32(args.ParamList[CIS.Common.DischargeSummary.DischargeDoctorId].ToString());
            DateTime discharge_date = DateTime.Now;//Convert.ToDateTime( args.ParamList[CIS.Common.DischargeSummary.DischargeDate].ToString());
            string diagnosis = args.ParamList[CIS.Common.DischargeSummary.Diagnosis].ToString();
            string operative_findings = args.ParamList[CIS.Common.DischargeSummary.OperativeFindings].ToString();
            string procedure = args.ParamList[CIS.Common.DischargeSummary.Procedure].ToString();
            string summary = args.ParamList[CIS.Common.DischargeSummary.Summary].ToString();
            string conditionAtAdmisson = args.ParamList[CIS.Common.DischargeSummary.ConditionAtAdmission].ToString();
            string investigation = args.ParamList[CIS.Common.DischargeSummary.Investigations].ToString();
            string treatmentGiven = args.ParamList[CIS.Common.DischargeSummary.TreatmentGiven].ToString();
            string conditionAtDischarge = args.ParamList[CIS.Common.DischargeSummary.ConditionAtDischarge].ToString();
            string treatmentAdviced = args.ParamList[CIS.Common.DischargeSummary.TreatmentAdviced].ToString();
            string reviewon = args.ParamList[CIS.Common.DischargeSummary.ReviewOn].ToString();
            int lastUpdatedUser = Convert.ToInt32(args.ParamList[CIS.Common.DischargeSummary.LastUpdatedUser].ToString());

            List<MySqlParameter> listparameter = new List<MySqlParameter>();
            MySqlParameter param = new MySqlParameter("DischargeSummaryId", MySqlDbType.Int32);
            param.Value = dischargesummaryid;
            param.Direction = ParameterDirection.Input;
            listparameter.Add(param);

            MySqlParameter param1 = new MySqlParameter("DischargeSummaryNumber", MySqlDbType.VarChar, 45);
            param1.Value = discharegesummaryNo;
            listparameter.Add(param1);

            MySqlParameter param2 = new MySqlParameter("PatientId", MySqlDbType.VarChar, 45);
            param2.Value = patient_id;
            listparameter.Add(param2);

            MySqlParameter param3 = new MySqlParameter("VisitNumber", MySqlDbType.VarChar, 45);
            param3.Value = visit_no;
            listparameter.Add(param3);

            MySqlParameter param4 = new MySqlParameter("DischargeDoctorId", MySqlDbType.Int32);
            param4.Value = doctor_id;
            listparameter.Add(param4);

            MySqlParameter param5 = new MySqlParameter("Diagnosis", MySqlDbType.String);
            param5.Value = diagnosis;
            listparameter.Add(param5);

            MySqlParameter param6 = new MySqlParameter("OperativeFindings", MySqlDbType.String);
            param6.Value = operative_findings;
            listparameter.Add(param6);

            MySqlParameter param7 = new MySqlParameter("Procedure1", MySqlDbType.String);
            param7.Value = procedure;
            listparameter.Add(param7);

            MySqlParameter param8 = new MySqlParameter("Summary", MySqlDbType.String);
            param8.Value = procedure;
            listparameter.Add(param8);

            MySqlParameter param9 = new MySqlParameter("ConditionAtAdmission", MySqlDbType.String);
            param9.Value = conditionAtAdmisson;
            listparameter.Add(param9);

            MySqlParameter param10 = new MySqlParameter("Investigations", MySqlDbType.String);
            param10.Value = investigation;
            listparameter.Add(param10);

            MySqlParameter param11 = new MySqlParameter("TreatmentGiven", MySqlDbType.String);
            param11.Value = treatmentGiven;
            listparameter.Add(param11);

            MySqlParameter param12 = new MySqlParameter("ConditionAtDischarge", MySqlDbType.String);
            param12.Value = conditionAtDischarge;
            listparameter.Add(param12);

            MySqlParameter param13 = new MySqlParameter("TreatmentAdviced", MySqlDbType.String);
            param13.Value = treatmentAdviced;
            listparameter.Add(param13);

            MySqlParameter param14 = new MySqlParameter("ReviewOn", MySqlDbType.String);
            param14.Value = reviewon;
            listparameter.Add(param14);

            MySqlParameter param15 = new MySqlParameter("LastUpdatedUser", MySqlDbType.String);
            param15.Value = lastUpdatedUser;
            listparameter.Add(param15);

            DataSet ds = objDBHandler.ExecuteDateSet("sp_InsertUpdateDischargeSummary", listparameter);
            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }
        #endregion
    }
}
