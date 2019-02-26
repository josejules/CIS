using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CIS.DataAccess;
using CIS.Common;

namespace CIS.BusinessFacade
{
    public class clsLabBusinessFacade
    {
        clsLabDataAccess objDataAccess = new clsLabDataAccess();
        public int getLastInsertedId()
        {
            return objDataAccess.getLastInsertedId();
        }
        public void commitTransction()
        {
            objDataAccess.commitTransction();
        }
        public void rollBackTransation()
        {
            objDataAccess.rollBackTransation();
        }

        #region Category
        public DataTable LoadCategory()
        {
            return new clsLabDataAccess().LoadCategory();
        }
        public int AddCategory(ComArugments args)
        {
            return new clsLabDataAccess().AddCategory(args);
        }
        public DataTable FetchCategoryInfoById(int categoryId)
        {
            return new clsLabDataAccess().FetchCategoryInfoById(categoryId);
        }
        public int DeleteCategory(int categoryId)
        {
            return new clsLabDataAccess().DeleteCategory(categoryId);
        }
        #endregion

        #region TestField
        public DataTable LoadTestFields()
        {
            return new clsLabDataAccess().LoadTestFields();
        }
        public DataTable LoadTestFieldTypes()
        {
            return new clsLabDataAccess().LoadTestFieldTypes();
        }
        public DataTable LoadTestItem(int fieldId)
        {
            return new clsLabDataAccess().LoadTestItems(fieldId);
        }
        public int AddEditTestField(ComArugments args)
        {
            return objDataAccess.AddEditTestField(args);
        }
        public int AddEditTestItems(ComArugments args)
        {
            return objDataAccess.AddEditTestItems(args);
        }
        public DataTable FetchTestFieldInfoById(int fieldId)
        {
            return new clsLabDataAccess().FetchTestFieldInfoById(fieldId);
        }
        public int DeleteTestField(int fieldId)
        {
            return new clsLabDataAccess().DeleteTestField(fieldId);
        }
        public int DeleteTestItem(int itemId)
        {
            return new clsLabDataAccess().DeleteTestItem(itemId);
        }
        #endregion

        #region TestFieldMapping
        public DataTable LoadTestClass()
        {
            return new clsLabDataAccess().LoadTestClass();
        }
        public DataTable LoadUnMappedTestFields(int investigationId)
        {
            return new clsLabDataAccess().LoadUnMappedTestFields(investigationId);
        }
        public DataTable LoadMappedTestFields(int investigationId)
        {
            return new clsLabDataAccess().LoadMappedTestFields(investigationId);
        }
        public int DeleteMappedTestFields(int investigationId)
        {
            return new clsLabDataAccess().DeleteMappedTestFields(investigationId);
        }
        public int SaveMappedTestFields(ComArugments comArgs)
        {
            return new clsLabDataAccess().SaveMappedTestFields(comArgs);
        }
        #endregion

        #region ResultEntryView
        public DataTable LoadPatientDetails(string criteria)
        {
            return new clsLabDataAccess().LoadPatientDetails(criteria);
        }
        public DataTable LoadTestDetails(int billId)
        {
            return new clsLabDataAccess().LoadTestDetails(billId);
        }
        #endregion

        #region ResultEntry
        public DataTable LoadResultEntryInfo(int billId)
        {
            return new clsLabDataAccess().LoadResultEntryInfo(billId);
        }
        public int SaveResultEntryInfo(ComArugments args)
        {
            return objDataAccess.SaveResultEntryInfo(args);
        }
        public int SaveResultEntryDetailInfo(ComArugments args)
        {
            return objDataAccess.SaveResultEntryDetailInfo(args);
        }
        public DataTable EditResultEntryInfo(int resultEntryId)
        {
            return new clsLabDataAccess().EditResultEntryInfo(resultEntryId);
        }
        public int DeletePatientResultEntry(int billId)
        {
            return objDataAccess.DeletePatientResultEntry(billId);
        }
        public DataTable LoadResultEntryInfoForLabPrintout(int billId)
        {
            return objDataAccess.LoadResultEntryInfoForLabPrintout(billId);
        }
        #endregion
        #region ViewLabBillInfo
        public DataTable LoadLabBillInfo(ComArugments comArgs)
        {
            return new clsLabDataAccess().LoadLabBillInfo(comArgs);
        }
        #endregion

        #region UserRole
        public DataTable LoadUserRole()
        {
            return new clsLabDataAccess().LoadUserRoles();
        }
        public int AddUserRole(ComArugments args)
        {
            return new clsLabDataAccess().AddUserRole(args);
        }
        public DataTable FetchUserRoleInfoById(int roleId)
        {
            return new clsLabDataAccess().FetchUserRoleInfoById(roleId);
        }
        public int DeleteUserRole(int roleId)
        {
            return new clsLabDataAccess().DeleteUserRole(roleId);
        }
        #endregion

        #region User
        public DataTable LoadUserTypes()
        {
            return new clsLabDataAccess().LoadUserTypes();
        }
        public DataTable LoadUsers()
        {
            return new clsLabDataAccess().LoadUsers();
        }
        public int AddEditUser(ComArugments args)
        {
            return new clsLabDataAccess().AddEditUser(args);
        }
        public DataTable FetchUserInfoById(int roleId)
        {
            return new clsLabDataAccess().FetchUserInfoById(roleId);
        }
        public int DeleteUser(int roleId)
        {
            return new clsLabDataAccess().DeleteUser(roleId);
        }
        #endregion

        #region UserMapping
        public DataTable LoadUserActivities(int roleId)
        {
            return new clsLabDataAccess().LoadUserActivities(roleId);
        }
        public int DeleteUserRoleActities(int roleId)
        {
            return objDataAccess.DeleteUserRoleActities(roleId);
        }
        public int SaveUserRoleActivity(ComArugments args)
        {
            return objDataAccess.SaveUserRoleActivity(args);
        }
        #endregion

        #region PrinterSetting
        public DataTable LoadPrinterSetting()
        {
            return new clsLabDataAccess().LoadPrinterSettings();
        }
        public int AddPrinterSetting(ComArugments args)
        {
            return new clsLabDataAccess().AddPrinterSetting(args);
        }
        public DataTable FetchPrinterSettingInfoById(int settingId)
        {
            return new clsLabDataAccess().FetchPrinterSettingInfoById(settingId);
        }
        public int DeletePrinterSetting(int settingId)
        {
            return new clsLabDataAccess().DeletePrinterSetting(settingId);
        }
        #endregion

        #region DischargeSummary
        public DataTable GetDischargeSummary()
        {
            return new clsLabDataAccess().GetDischargeSummary();
        }
        public int SaveDischargeSummary(ComArugments args)
        {
            return new clsLabDataAccess().SaveDischargeSummary(args);
        }
        #endregion
    }
}
