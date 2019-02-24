using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CIS.DataAccess;
using CIS.Common;
using CIS;

namespace CIS.BusinessFacade
{
    public class BusinessFacade
    {
        #region Declaration
        DataAccess.DataAccess objDataAccess = new DataAccess.DataAccess();
        ComArugments objArg = new ComArugments();
        string subStr = null;

        DataTable dtSource = null;
        int flag = 0;
        int billId;


        #endregion
         
        #region Assign Row No to DataGridView
        public DataTable AssignRowNo(DataTable dtSource)
        {
            // If the datasource contains Row No, Remove the Row No
            if (dtSource.Columns.Contains("#") == true)
            {
                dtSource.Columns.Remove("#");
            }
            // Assign Row no for each row
            DataTable serialNoTable = new DataTable();
            serialNoTable.Columns.Add("#");
            foreach (DataColumn sCol in dtSource.Columns)
                serialNoTable.Columns.Add(sCol.ColumnName);

            int i = 1;
            foreach (DataRow sRow in dtSource.Rows)
            {
                DataRow serialRow = serialNoTable.NewRow();
                serialRow["#"] = i.ToString();
                foreach (DataColumn sCol in dtSource.Columns)
                    serialRow[sCol.ColumnName] = dtSource.Rows[i - 1][sCol].ToString();
                serialNoTable.Rows.Add(serialRow);
                i++;
            }
            //return (serialNoTable as DataView);
            return serialNoTable;
        }

        //Add new discount column
        public DataTable newDiscountColumn(DataTable dtSource)
        {
            // If the datasource contains Row No, Remove the Row No
            if (dtSource.Columns.Contains("New Discount") == true)
            {
                dtSource.Columns.Remove("New Discount");
            }
            // Assign Row no for each row
            DataTable serialNoTable = new DataTable();
            serialNoTable.Columns.Add("New Discount");
            foreach (DataColumn sCol in dtSource.Columns)
                serialNoTable.Columns.Add(sCol.ColumnName);

            int i = 1;
            foreach (DataRow sRow in dtSource.Rows)
            {
                DataRow serialRow = serialNoTable.NewRow();
                foreach (DataColumn sCol in dtSource.Columns)
                    serialRow[sCol.ColumnName] = dtSource.Rows[i - 1][sCol].ToString();
                serialNoTable.Rows.Add(serialRow);
                i++;
            }
            return serialNoTable;
        }
        #endregion
   
        #region Login and User rights details
        public DataTable getUserDetails(string CIS_User_Id)
        {
            dtSource = objDataAccess.getLoginUserDetails(CIS_User_Id);
            return dtSource;
        }

        public DataTable getUserActivities(int role_id, int partent_id)
        {
            dtSource = objDataAccess.getUserActivities(role_id, partent_id);
            return dtSource;
        }

        public DataTable getUserRoleId(int user_id)
        {
            dtSource = objDataAccess.getUserRoleId(user_id);
            return dtSource;
        }
        #endregion

        #region CIS_Department details
        public DataTable getDepartmentDetails()
        {
            dtSource = objDataAccess.getDepartmentDetails();
            return dtSource;
        }

        public int insertDepartment(ComArugments objArg)
        {
            flag = objDataAccess.insertDepartmnet(objArg);
            return flag;
        }

        public int deleteDepartment(int cisDepartmentId)
        {
            flag = objDataAccess.deleteDepartment(cisDepartmentId);
            return flag;
        }

        public DataTable getDepartmentCategoryId(int cisDepartmentId)
        {
            dtSource = objDataAccess.getDepartmentCategoryId(cisDepartmentId);
            return dtSource;
        }

        public int updateDepartment(ComArugments objArg)
        {
            flag = objDataAccess.updateDepartment(objArg);
            return flag;
        }

        public DataTable getDepartmentRecord(int departmentId)
        {
            dtSource = objDataAccess.getDepartmentRecord(departmentId);
            return dtSource;
        }

        public DataTable loadDepartment()
        {
            dtSource = objDataAccess.loadDepartment();
            return dtSource;
        }

        public DataTable loadClinicalType()
        {
            dtSource = objDataAccess.loadClinicalType();
            return dtSource;
        }

        public DataTable loadWard()
        {
            dtSource = objDataAccess.loadWard();
            return dtSource;
        }

        public DataTable loadWardReg()
        {
            dtSource = objDataAccess.loadWardReg();
            return dtSource;
        }

        public DataTable loadRoom(int wardId)
        {
            dtSource = objDataAccess.loadRoom(wardId);
            return dtSource;
        }

        public DataTable loadRoomReg(int wardId)
        {
            dtSource = objDataAccess.loadRoomReg(wardId);
            return dtSource;
        }

        public DataTable loadBed(int roomId)
        {
            dtSource = objDataAccess.loadBed(roomId);
            return dtSource;
        }

        public DataTable loadBedReg(int roomId)
        {
            dtSource = objDataAccess.loadBedReg(roomId);
            return dtSource;
        }

        public DataTable loadRegistrationDepartment()
        {
            dtSource = objDataAccess.loadRegistrationDepartment();
            return dtSource;
        }
        #endregion

        #region CIS_Patient Type details
        public DataTable getPatientTypeDetails()
        {
            dtSource = objDataAccess.getPatientTypeDetails();
            return dtSource;
        }

        public int insertPatientType(ComArugments objArg)
        {
            flag = objDataAccess.insertPatientType(objArg);
            return flag;
        }

        public int deletePatientType(int cisPatientTypeId)
        {
            flag = objDataAccess.deletePatientType(cisPatientTypeId);
            return flag;
        }

        public int updatePatientType(ComArugments objArg)
        {
            flag = objDataAccess.updatePatientType(objArg);
            return flag;
        }

        public DataTable getPatientTypeRecord(int PatientTypeId)
        {
            dtSource = objDataAccess.getPatientTypeRecord(PatientTypeId);
            return dtSource;
        }

        public DataTable loadPatientType()
        {
            dtSource = objDataAccess.loadPatientType();
            return dtSource;
        }

        public DataTable loadReferralBy()
        {
            dtSource = objDataAccess.loadReferralBy();
            return dtSource;
        }
        #endregion

        #region CIS_Doctor details
        public DataTable getDoctorDetails()
        {
            dtSource = objDataAccess.getDoctorDetails();
            return dtSource;
        }

        public DataTable getReferralDetails()
        {
            dtSource = objDataAccess.getReferralDetails();
            return dtSource;
        }

        public int insertDoctor(ComArugments objArg)
        {
            flag = objDataAccess.insertDoctor(objArg);
            return flag;
        }

        public int deleteDoctor(int DoctorId)
        {
            flag = objDataAccess.deleteDoctor(DoctorId);
            return flag;
        }

        public int deleteReferral(int DoctorId)
        {
            flag = objDataAccess.deleteReferral(DoctorId);
            return flag;
        }

        public int updateDoctor(ComArugments objArg)
        {
            flag = objDataAccess.updateDoctor(objArg);
            return flag;
        }

        public DataTable getDoctorRecord(int DoctorId)
        {
            dtSource = objDataAccess.getDoctorRecord(DoctorId);
            return dtSource;
        }

        public DataTable loadDoctor()
        {
            dtSource = objDataAccess.loadDoctor();
            return dtSource;
        }
        #endregion

        #region CIS_Address Info details
        public DataTable getAddressInfoDetails()
        {
            dtSource = objDataAccess.getAddressInfoDetails();
            return dtSource;
        }

        public int insertAddressInfo(ComArugments objArg)
        {
            flag = objDataAccess.insertAddressInfo(objArg);
            return flag;
        }

        public int deleteAddressInfo(int AddressInfoId)
        {
            flag = objDataAccess.deleteAddressInfo(AddressInfoId);
            return flag;
        }

        public int updateAddressInfo(ComArugments objArg)
        {
            flag = objDataAccess.updateAddressInfo(objArg);
            return flag;
        }

        public DataTable getAddressInfoRecord(int AddressInfoId)
        {
            dtSource = objDataAccess.getAddressInfoRecord(AddressInfoId);
            return dtSource;
        }

        public DataTable fetchAddressInfobyPlace(int address_id)
        {
            dtSource = objDataAccess.fetchAddressInfobyPlace(address_id);
            return dtSource;
        }

        public DataTable loadAddress()
        {
            dtSource = objDataAccess.loadAddress();
            return dtSource;
        }
        #endregion

        #region CIS_Corporate details
        public DataTable getCorporateDetails()
        {
            dtSource = objDataAccess.getCorporateDetails();
            return dtSource;
        }

        public int insertCorporate(ComArugments objArg)
        {
            flag = objDataAccess.insertCorporate(objArg);
            return flag;
        }

        public int deleteCorporate(int CorporateId)
        {
            flag = objDataAccess.deleteCorporate(CorporateId);
            return flag;
        }

        public int updateCorporate(ComArugments objArg)
        {
            flag = objDataAccess.updateCorporate(objArg);
            return flag;
        }

        public DataTable getCorporateRecord(int CorporateId)
        {
            dtSource = objDataAccess.getCorporateRecord(CorporateId);
            return dtSource;
        }

        public DataTable loadCorporate()
        {
            dtSource = objDataAccess.loadCorporate();
            return dtSource;
        }
        #endregion

        #region CIS_Room

        public DataTable getRoomDetails(int wardId)
        {
            dtSource = objDataAccess.getRoomDetails(wardId);
            return dtSource;
        }

        public DataTable getBedDetails(int wardId, int roomId)
        {
            dtSource = objDataAccess.getBedDetails(wardId, roomId);
            return dtSource;
        }

        public int insertRoom(ComArugments objArg)
        {
            flag = objDataAccess.insertRoom(objArg);
            return flag;
        }

        public int insertBed(ComArugments objArg)
        {
            flag = objDataAccess.insertBed(objArg);
            return flag;
        }
        #endregion

        #region CIS_Define Reg Fee
        public DataTable getDefineRegFeeDetails()
        {
            dtSource = objDataAccess.getDefineRegFeeDetails();
            return dtSource;
        }

        public DataTable checkDefineRegFeeRecord(ComArugments objArg)
        {
            dtSource = objDataAccess.checkDefineRegFeeRecord(objArg);
            return dtSource;
        }

        public int insertDefineRegFee(ComArugments objArg)
        {
            flag = objDataAccess.insertDefineRegFee(objArg);
            return flag;
        }

        public int deleteDefineRegFee(int DefineRegFeeId)
        {
            flag = objDataAccess.deleteDefineRegFee(DefineRegFeeId);
            return flag;
        }

        public int updateDefineRegFee(ComArugments objArg)
        {
            flag = objDataAccess.updateDefineRegFee(objArg);
            return flag;
        }

        public DataTable getDefineRegFeeRecord(int DefineRegFeeId)
        {
            dtSource = objDataAccess.getDefineRegFeeRecord(DefineRegFeeId);
            return dtSource;
        }

        public DataTable getRegFeeByDoctorId(int doctorId)
        {
            dtSource = objDataAccess.getRegFeeByDoctorId(doctorId);
            return dtSource;
        }

        public DataTable getChargesApplicableCorp(int corporateId)
        {
            dtSource = objDataAccess.getChargesApplicableCorp(corporateId);
            return dtSource;
        }
        #endregion

        #region CIS_Common Functions

        public ComArugments getPurchaseReceiptNumberFormat()
        {
            try
            {
                dtSource = objDataAccess.getPurchaseReceiptNumberFormat();

                Common.Common.cis_number_generation.number_format_id = Convert.ToInt32(dtSource.Rows[0]["number_format_id"].ToString());
                Common.Common.cis_number_generation.field_name = dtSource.Rows[0]["field_name"].ToString();
                Common.Common.cis_number_generation.number_format = dtSource.Rows[0]["number_format"].ToString();
                Common.Common.cis_number_generation.prefix_date = dtSource.Rows[0]["prefix_date"].ToString();
                Common.Common.cis_number_generation.prefix_text = dtSource.Rows[0]["prefix_text"].ToString();
                Common.Common.cis_number_generation.last_bill_number = dtSource.Rows[0]["last_bill_number"].ToString();
                Common.Common.cis_number_generation.running_number = Convert.ToInt32(dtSource.Rows[0]["running_number"].ToString());

                if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                {
                    subStr = Common.Common.cis_number_generation.last_bill_number.Substring(3, 2);

                    if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                    {
                        Common.Common.cis_number_generation.running_purchase_receipt_number = (Common.Common.cis_number_generation.running_number + 1);
                        Common.Common.cis_number_generation.purchase_receipt_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                    }
                    else
                    {
                        Common.Common.cis_number_generation.running_purchase_receipt_number = 1;
                        Common.Common.cis_number_generation.purchase_receipt_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), Common.Common.cis_number_generation.running_purchase_receipt_number);
                    }
                }
                else
                {
                    Common.Common.cis_number_generation.running_purchase_receipt_number = (Common.Common.cis_number_generation.running_number + 1);
                    Common.Common.cis_number_generation.purchase_receipt_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                }

                objArg.ParamList["running_purchase_receipt_number"] = Common.Common.cis_number_generation.running_purchase_receipt_number;
                objArg.ParamList["purchase_receipt_number"] = Common.Common.cis_number_generation.purchase_receipt_number;

                return objArg;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ComArugments getInventoryMovementsNumberFormat()
        {
            try
            {
                dtSource = objDataAccess.getInventoryMovementsNumberFormat();

                Common.Common.cis_number_generation.number_format_id = Convert.ToInt32(dtSource.Rows[0]["number_format_id"].ToString());
                Common.Common.cis_number_generation.field_name = dtSource.Rows[0]["field_name"].ToString();
                Common.Common.cis_number_generation.number_format = dtSource.Rows[0]["number_format"].ToString();
                Common.Common.cis_number_generation.prefix_date = dtSource.Rows[0]["prefix_date"].ToString();
                Common.Common.cis_number_generation.prefix_text = dtSource.Rows[0]["prefix_text"].ToString();
                Common.Common.cis_number_generation.last_bill_number = dtSource.Rows[0]["last_bill_number"].ToString();
                Common.Common.cis_number_generation.running_number = Convert.ToInt32(dtSource.Rows[0]["running_number"].ToString());

                if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                {
                    subStr = Common.Common.cis_number_generation.last_bill_number.Substring(2, 2);

                    if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                    {
                        Common.Common.cis_number_generation.running_inventory_movements_number = (Common.Common.cis_number_generation.running_number + 1);
                        Common.Common.cis_number_generation.inventory_movements_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                    }
                    else
                    {
                        Common.Common.cis_number_generation.running_inventory_movements_number = 1;
                        Common.Common.cis_number_generation.inventory_movements_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), Common.Common.cis_number_generation.running_inventory_movements_number);
                    }
                }
                else
                {
                    Common.Common.cis_number_generation.running_inventory_movements_number = (Common.Common.cis_number_generation.running_number + 1);
                    Common.Common.cis_number_generation.inventory_movements_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                }

                objArg.ParamList["running_inventory_movements_number"] = Common.Common.cis_number_generation.running_inventory_movements_number;
                objArg.ParamList["inventory_movements_number"] = Common.Common.cis_number_generation.inventory_movements_number;

                return objArg;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ComArugments getAdvanceTransNumberFormat()
        {
            try
            {
                dtSource = objDataAccess.getAdvanceTransNumberFormat();

                for (int i = 0; i < dtSource.Rows.Count; i++)
                {

                    Common.Common.cis_number_generation.number_format_id = Convert.ToInt32(dtSource.Rows[i]["number_format_id"].ToString());
                    Common.Common.cis_number_generation.field_name = dtSource.Rows[i]["field_name"].ToString();
                    Common.Common.cis_number_generation.number_format = dtSource.Rows[i]["number_format"].ToString();
                    Common.Common.cis_number_generation.prefix_date = dtSource.Rows[i]["prefix_date"].ToString();
                    Common.Common.cis_number_generation.prefix_text = dtSource.Rows[i]["prefix_text"].ToString();
                    Common.Common.cis_number_generation.last_bill_number = dtSource.Rows[i]["last_bill_number"].ToString();
                    Common.Common.cis_number_generation.running_number = Convert.ToInt32(dtSource.Rows[i]["running_number"].ToString());

                    switch (Common.Common.cis_number_generation.number_format_id)
                    {
                        case 9:
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(2, 2);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_adv_collection_number = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.adv_collection_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                                }
                                else
                                {
                                    Common.Common.cis_number_generation.running_adv_collection_number = 1;
                                    Common.Common.cis_number_generation.adv_collection_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), Common.Common.cis_number_generation.running_adv_collection_number);
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_adv_collection_number = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.adv_collection_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                            }
                            break;

                        case 10:
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(2, 2);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_adv_refund_number = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.adv_refund_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                                }
                                else
                                {
                                    Common.Common.cis_number_generation.running_adv_refund_number = 1;
                                    Common.Common.cis_number_generation.adv_refund_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), Common.Common.cis_number_generation.running_adv_refund_number);
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_adv_refund_number = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.adv_refund_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                            }
                            break;
                    }
                }

                objArg.ParamList["running_adv_collection_number"] = Common.Common.cis_number_generation.running_adv_collection_number;
                objArg.ParamList["adv_collection_number"] = Common.Common.cis_number_generation.adv_collection_number;
                objArg.ParamList["running_adv_refund_number"] = Common.Common.cis_number_generation.running_adv_refund_number;
                objArg.ParamList["adv_refund_number"] = Common.Common.cis_number_generation.adv_refund_number;

                return objArg;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public ComArugments getWardNumberFormat()
        {
            try
            {
                dtSource = objDataAccess.getWardNumberFormat();

                for (int i = 0; i < dtSource.Rows.Count; i++)
                {

                    Common.Common.cis_number_generation.number_format_id = Convert.ToInt32(dtSource.Rows[i]["number_format_id"].ToString());
                    Common.Common.cis_number_generation.field_name = dtSource.Rows[i]["field_name"].ToString();
                    Common.Common.cis_number_generation.number_format = dtSource.Rows[i]["number_format"].ToString();
                    Common.Common.cis_number_generation.prefix_date = dtSource.Rows[i]["prefix_date"].ToString();
                    Common.Common.cis_number_generation.prefix_text = dtSource.Rows[i]["prefix_text"].ToString();
                    Common.Common.cis_number_generation.last_bill_number = dtSource.Rows[i]["last_bill_number"].ToString();
                    Common.Common.cis_number_generation.running_number = Convert.ToInt32(dtSource.Rows[i]["running_number"].ToString());

                    switch (Common.Common.cis_number_generation.number_format_id)
                    {
                        case 11:
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(2, 2);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_ward_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.ward_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                                }
                                else
                                {
                                    Common.Common.cis_number_generation.running_ward_bill_number = 1;
                                    Common.Common.cis_number_generation.ward_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), Common.Common.cis_number_generation.running_ward_bill_number);
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_ward_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.ward_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                            }
                            break;
                    }
                }

                objArg.ParamList["running_ward_bill_number"] = Common.Common.cis_number_generation.running_ward_bill_number;
                objArg.ParamList["ward_bill_number"] = Common.Common.cis_number_generation.ward_bill_number;

                return objArg;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ComArugments generateNumber()
        {
            try
            {
                dtSource = objDataAccess.getNumberFormat();

                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    Common.Common.cis_number_generation.number_format_id = Convert.ToInt32(dtSource.Rows[i]["number_format_id"].ToString());
                    Common.Common.cis_number_generation.field_name = dtSource.Rows[i]["field_name"].ToString();
                    Common.Common.cis_number_generation.number_format = dtSource.Rows[i]["number_format"].ToString();
                    Common.Common.cis_number_generation.prefix_date = dtSource.Rows[i]["prefix_date"].ToString();
                    Common.Common.cis_number_generation.prefix_text = dtSource.Rows[i]["prefix_text"].ToString();
                    Common.Common.cis_number_generation.last_bill_number = dtSource.Rows[i]["last_bill_number"].ToString();
                    Common.Common.cis_number_generation.running_number = Convert.ToInt32(dtSource.Rows[i]["running_number"].ToString());

                    switch (Common.Common.cis_number_generation.number_format_id)
                    {
                        case 1://Patient Id
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(0, 2);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_patient_id = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.number_format_patient_id = Common.Common.cis_number_generation.running_patient_id.ToString().PadLeft(Common.Common.cis_number_generation.number_format.Length, '0');
                                    Common.Common.cis_number_generation.patient_id = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.number_format_patient_id));
                                }
                                else
                                {
                                    Common.Common.cis_number_generation.running_patient_id = 1;
                                    Common.Common.cis_number_generation.number_format_patient_id = Common.Common.cis_number_generation.running_patient_id.ToString().PadLeft(Common.Common.cis_number_generation.number_format.Length, '0');
                                    Common.Common.cis_number_generation.patient_id = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.number_format_patient_id));
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_patient_id = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.number_format_patient_id = Common.Common.cis_number_generation.running_patient_id.ToString().PadLeft(Common.Common.cis_number_generation.number_format.Length, '0');
                                Common.Common.cis_number_generation.patient_id = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.number_format_patient_id));
                            }
                            break;

                        case 2://Visit Number
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(0, 8);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_visit_number = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.visit_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                                }
                                else
                                {
                                    Common.Common.cis_number_generation.running_visit_number = 1;
                                    Common.Common.cis_number_generation.visit_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (1));
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_visit_number = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.visit_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                            }

                            break;

                        case 3: // Token Number
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(0, 2);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_token_number = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.token_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), "/", (Common.Common.cis_number_generation.running_number + 1));
                                }
                                else
                                {
                                    Common.Common.cis_number_generation.running_token_number = 1;
                                    Common.Common.cis_number_generation.token_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), "/", (1));
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_token_number = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.token_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), "/", (Common.Common.cis_number_generation.running_number + 1));
                            }
                            break;

                        case 4: //Reg Bill Number
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(2, 2);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_reg_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.reg_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                                }
                                else
                                {
                                    //Common.Common.cis_number_generation.running_reg_bill_number = 1;
                                    Common.Common.cis_number_generation.reg_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (1));
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_reg_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.reg_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                            }
                            break;

                        case 5://Inv Bill Number
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(2, 2);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_investigation_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.investigation_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                                }
                                else
                                {
                                    Common.Common.cis_number_generation.running_investigation_bill_number = 1;
                                    Common.Common.cis_number_generation.investigation_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), Common.Common.cis_number_generation.running_investigation_bill_number);
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_investigation_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.investigation_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                            }
                            break;

                        case 6://Pharmacy Bill Number
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(2, 2);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_pharmacy_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.pharmacy_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                                }
                                else
                                {
                                    Common.Common.cis_number_generation.running_pharmacy_bill_number = 1;
                                    Common.Common.cis_number_generation.pharmacy_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), Common.Common.cis_number_generation.running_pharmacy_bill_number);
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_pharmacy_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.pharmacy_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                            }
                            break;

                        case 7://General Bill Number
                            if (!(string.IsNullOrEmpty(Common.Common.cis_number_generation.last_bill_number)))
                            {
                                subStr = Common.Common.cis_number_generation.last_bill_number.Substring(2, 2);

                                if (subStr == DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date))
                                {
                                    Common.Common.cis_number_generation.running_general_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                    Common.Common.cis_number_generation.general_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                                }
                                else
                                {
                                    Common.Common.cis_number_generation.running_general_bill_number = 1;
                                    Common.Common.cis_number_generation.general_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), Common.Common.cis_number_generation.running_general_bill_number);
                                }
                            }
                            else
                            {
                                Common.Common.cis_number_generation.running_general_bill_number = (Common.Common.cis_number_generation.running_number + 1);
                                Common.Common.cis_number_generation.general_bill_number = string.Concat((Common.Common.cis_number_generation.prefix_text), (DateTime.Now.ToString(Common.Common.cis_number_generation.prefix_date)), (Common.Common.cis_number_generation.running_number + 1));
                            }
                            break;

                        default:
                            break;
                    }
                }

                objArg.ParamList["running_patient_id"] = Common.Common.cis_number_generation.running_patient_id;
                objArg.ParamList["patient_id"] = Common.Common.cis_number_generation.patient_id;
                objArg.ParamList["running_visit_number"] = Common.Common.cis_number_generation.running_visit_number;
                objArg.ParamList["visit_number"] = Common.Common.cis_number_generation.visit_number;
                objArg.ParamList["running_token_number"] = Common.Common.cis_number_generation.running_token_number;
                objArg.ParamList["token_number"] = Common.Common.cis_number_generation.token_number;
                objArg.ParamList["running_reg_bill_number"] = Common.Common.cis_number_generation.running_reg_bill_number;
                objArg.ParamList["reg_bill_number"] = Common.Common.cis_number_generation.reg_bill_number;
                objArg.ParamList["running_investigation_bill_number"] = Common.Common.cis_number_generation.running_investigation_bill_number;
                objArg.ParamList["investigation_bill_number"] = Common.Common.cis_number_generation.investigation_bill_number;
                objArg.ParamList["running_pharmacy_bill_number"] = Common.Common.cis_number_generation.running_pharmacy_bill_number;
                objArg.ParamList["pharmacy_bill_number"] = Common.Common.cis_number_generation.pharmacy_bill_number;
                objArg.ParamList["running_general_bill_number"] = Common.Common.cis_number_generation.running_general_bill_number;
                objArg.ParamList["general_bill_number"] = Common.Common.cis_number_generation.general_bill_number;

                return objArg;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int lastInsertRecord()
        {
            billId = objDataAccess.lastInsertRecord();
            return billId;
        }

        public int NonBlankValueOfInt(string cellValue)
        {
            if (string.IsNullOrEmpty(cellValue))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(cellValue);
            }
        }

        public decimal NonBlankValueOfDecimal(string cellValue)
        {
            if (string.IsNullOrEmpty(cellValue))
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(cellValue);
            }
        }

        public string dateFromValue(string dateFValue)
        {
            return string.Concat(dateFValue, " 00:00:00");
        }

        public string dateToValue(string dateTValue)
        {
            return string.Concat(dateTValue, " 23:59:59");
        }
        #endregion

        #region Registration

        public DataTable loadSocialTitle()
        {
            dtSource = objDataAccess.loadSocialTitle();
            return dtSource;
        }

        public DataTable getGenderIdBySocialTitleId(int social_title_id)
        {
            dtSource = objDataAccess.getGenderIdBySocialTitleId(social_title_id);
            return dtSource;
        }
        
        public int insertRegistration(ComArugments objArg)
        {
            flag = objDataAccess.insertRegistration(objArg);
            return flag;
        }

        public int insertVisitInfo(ComArugments objArg)
        {
            flag = objDataAccess.insertVisitInfo(objArg);
            return flag;
        }

        public int insertPatBedInfo(ComArugments objArg)
        {
            flag = objDataAccess.insertPatBedInfo(objArg);
            return flag;
        }

        public int updatePatBed(ComArugments objArg)
        {
            flag = objDataAccess.updatePatBed(objArg);
            return flag;
        }

        public int insertRegBillInfo(ComArugments objArg)
        {
            flag = objDataAccess.insertRegBillInfo(objArg);
            return flag;
        }

        public int insertRegBillDetailInfo(ComArugments objArg)
        {
            flag = objDataAccess.insertRegBillDetailInfo(objArg);
            return flag;
        }

        public int modifyPatientInfo(ComArugments objArg)
        {
            flag = objDataAccess.modifyPatientInfo(objArg);
            return flag;
        }

        public int updateRegRunningNumber(ComArugments objArg)
        {
            flag = objDataAccess.updateRegRunningNumber(objArg);
            return flag;
        }

        public DataTable getPatientDetailsByPatientId(string patient_id)
        {
            dtSource = objDataAccess.getPatientDetailsByPatientId(patient_id);
            return dtSource;
        }

        public DataTable getRegFeeByDoctorIdAndPaientId(int doctorId, string patientId)
        {
            dtSource = objDataAccess.getRegFeeByDoctorIdAndPaientId(doctorId, patientId);
            return dtSource;
        }

        public DataTable getOPRegistrationInfo(string startDate, string endDate)
        {
            dtSource = objDataAccess.getOPRegistrationInfo(startDate, endDate);
            return dtSource;
        }

        public DataTable getIPRegistrationInfo(string startDate, string endDate)
        {
            dtSource = objDataAccess.getIPRegistrationInfo(startDate, endDate);
            return dtSource;
        }

        public DataTable loadDischargeType()
        {
            dtSource = objDataAccess.loadDischargeType();
            return dtSource;
        }

        public int updateDischargeInfo(ComArugments objArg)
        {
            flag = objDataAccess.updateDischargeInfo(objArg);
            return flag;
        }

        public int updateTransferInfo(ComArugments objArg)
        {
            flag = objDataAccess.updateTransferInfo(objArg);
            return flag;
        }

        public DataTable getVisitDetailsByPatientId(string patient_id)
        {
            dtSource = objDataAccess.getVisitDetailsByPatientId(patient_id);
            return dtSource;
        }

        public DataTable getVisitandBillDetailsByPatientId(string patient_id)
        {
            dtSource = objDataAccess.getVisitandBillDetailsByPatientId(patient_id);
            return dtSource;
        }

        public int cancelVisitInfo(ComArugments objArg)
        {
            flag = objDataAccess.cancelVisitInfo(objArg);
            return flag;
        }

        public int cancelRegBillInfo(ComArugments objArg)
        {
            flag = objDataAccess.cancelRegBillInfo(objArg);
            return flag;
        }

        public DataTable searchPatientInfo(string query)
        {
            dtSource = objDataAccess.searchPatientInfo(query);
            return dtSource;
        }
        #endregion

        #region Investigation
        public DataTable loadInvestigationDetails()
        {
            dtSource = objDataAccess.loadInvestigationDetails();
            return dtSource;
        }

        public DataTable getInvestigationById(int investigationId)
        {
            dtSource = objDataAccess.getInvestigationById(investigationId);
            return dtSource;
        }

        public DataTable getInvestigationInfo(string startDate, string endDate)
        {
            dtSource = objDataAccess.getInvestigationInfo(startDate, endDate);
            return dtSource;
        }

        public DataTable getWardBillInfo(string startDate, string endDate)
        {
            dtSource = objDataAccess.getWardBillInfo(startDate, endDate);
            return dtSource;
        }
        #endregion

        #region Pharmacy
        public DataTable loadPhaItem()
        {
            dtSource = objDataAccess.loadPhaItem();
            return dtSource;
        }

        public DataTable getPhaByItemId(int phaItemId)
        {
            dtSource = objDataAccess.getPhaByItemId(phaItemId);
            return dtSource;
        }

        public DataTable getPhaDepartmentId()
        {
            dtSource = objDataAccess.getPhaDepartmentId();
            return dtSource;
        }

        public DataTable getPharmacyInfo(string startDate, string endDate)
        {
            dtSource = objDataAccess.getPharmacyInfo(startDate, endDate);
            return dtSource;
        }
        #endregion

        #region CIS Pharmacy Item Type Master details
        public DataTable getItemTypeDetails()
        {
            dtSource = objDataAccess.getItemTypeDetails();
            return dtSource;
        }

        public int insertItemType(ComArugments objArg)
        {
            flag = objDataAccess.insertItemType(objArg);
            return flag;
        }

        public int deleteItemType(int cisItemTypeId)
        {
            flag = objDataAccess.deleteItemType(cisItemTypeId);
            return flag;
        }

        public int updateItemType(ComArugments objArg)
        {
            flag = objDataAccess.updateItemType(objArg);
            return flag;
        }

        public DataTable getItemTypeRecord(int ItemTypeId)
        {
            dtSource = objDataAccess.getItemTypeRecord(ItemTypeId);
            return dtSource;
        }

        public DataTable loadItemType()
        {
            dtSource = objDataAccess.loadItemType();
            return dtSource;
        }

        public DataTable loadTaxPerc()
        {
            dtSource = objDataAccess.loadTaxPerc();
            return dtSource;
        }
        
        #endregion

        #region CIS Pharmacy Item Master details
        public DataTable getItemDetails()
        {
            dtSource = objDataAccess.getItemDetails();
            return dtSource;
        }

        public int insertItem(ComArugments objArg)
        {
            flag = objDataAccess.insertItem(objArg);
            return flag;
        }

        public int deleteItem(int cisItemId)
        {
            flag = objDataAccess.deleteItem(cisItemId);
            return flag;
        }

        public int updateItem(ComArugments objArg)
        {
            flag = objDataAccess.updateItem(objArg);
            return flag;
        }

        public DataTable getItemRecord(int ItemId)
        {
            dtSource = objDataAccess.getItemRecord(ItemId);
            return dtSource;
        }
        #endregion

        #region CIS Pharmacy Vendor Master details
        public DataTable getVendorDetails()
        {
            dtSource = objDataAccess.getVendorDetails();
            return dtSource;
        }

        public int insertVendor(ComArugments objArg)
        {
            flag = objDataAccess.insertVendor(objArg);
            return flag;
        }

        public int insertReferral(ComArugments objArg)
        {
            flag = objDataAccess.insertReferral(objArg);
            return flag;
        }

        public int deleteVendor(int cisVendorId)
        {
            flag = objDataAccess.deleteVendor(cisVendorId);
            return flag;
        }

        public int updateVendor(ComArugments objArg)
        {
            flag = objDataAccess.updateVendor(objArg);
            return flag;
        }

        public int updateReferral(ComArugments objArg)
        {
            flag = objDataAccess.updateReferral(objArg);
            return flag;
        }

        public DataTable getVendorRecord(int VendorId)
        {
            dtSource = objDataAccess.getVendorRecord(VendorId);
            return dtSource;
        }

        public DataTable getReferralRecord(int ReferralId)
        {
            dtSource = objDataAccess.getReferralRecord(ReferralId);
            return dtSource;
        }

        public DataTable loadVendor()
        {
            dtSource = objDataAccess.loadVendor();
            return dtSource;
        }

        public DataTable getVendorTinNo(int vendorId)
        {
            dtSource = objDataAccess.getVendorTinNo(vendorId);
            return dtSource;
        }
        #endregion

        #region CIS Pharmacy Tax Master details
        public DataTable getTaxDetails()
        {
            dtSource = objDataAccess.getTaxDetails();
            return dtSource;
        }

        public int insertTax(ComArugments objArg)
        {
            flag = objDataAccess.insertTax(objArg);
            return flag;
        }

        public int deleteTax(int cisTaxId)
        {
            flag = objDataAccess.deleteTax(cisTaxId);
            return flag;
        }

        public int updateTax(ComArugments objArg)
        {
            flag = objDataAccess.updateTax(objArg);
            return flag;
        }

        public DataTable getTaxRecord(int TaxId)
        {
            dtSource = objDataAccess.getTaxRecord(TaxId);
            return dtSource;
        }
        #endregion

        #region CIS Investigation Category Master details
        public DataTable getInvestigationCategoryDetails()
        {
            dtSource = objDataAccess.getInvestigationCategoryDetails();
            return dtSource;
        }

        public int insertInvCategory(ComArugments objArg)
        {
            flag = objDataAccess.insertInvCategory(objArg);
            return flag;
        }

        public int deleteInvCategory(int cisInvCategoryId)
        {
            flag = objDataAccess.deleteInvCategory(cisInvCategoryId);
            return flag;
        }

        public int updateInvCategory(ComArugments objArg)
        {
            flag = objDataAccess.updateInvCategory(objArg);
            return flag;
        }

        public DataTable getInvCategoryRecord(int cisInvCategoryId)
        {
            dtSource = objDataAccess.getInvCategoryRecord(cisInvCategoryId);
            return dtSource;
        }

        public DataTable loadInvCategory()
        {
            dtSource = objDataAccess.loadInvCategory();
            return dtSource;
        }
        #endregion

        #region CIS Investigation List Master details
        public DataTable getInvestigationDetails()
        {
            dtSource = objDataAccess.getInvestigationDetails();
            return dtSource;
        }

        public int insertInvestigation(ComArugments objArg)
        {
            flag = objDataAccess.insertInvestigation(objArg);
            return flag;
        }

        public int deleteInvestigation(int cisInvestigationId)
        {
            flag = objDataAccess.deleteInvestigation(cisInvestigationId);
            return flag;
        }

        public int updateInvestigation(ComArugments objArg)
        {
            flag = objDataAccess.updateInvestigation(objArg);
            return flag;
        }

        public DataTable getInvestigationRecord(int InvestigationId)
        {
            dtSource = objDataAccess.getInvestigationRecord(InvestigationId);
            return dtSource;
        }

        public DataTable loadInvestigationDepartment()
        {
            dtSource = objDataAccess.loadInvestigationDepartment();
            return dtSource;
        }

        public DataTable loadShareType()
        {
            dtSource = objDataAccess.loadShareType();
            return dtSource;
        }
        #endregion

        #region CIS Pharmacy Inventory Management
        public DataTable getPhaTypeAndAvailQty(int ItemId)
        {
            dtSource = objDataAccess.getPhaTypeAndAvailQty(ItemId);
            return dtSource;
        }

        public DataTable getPhaViewStockByItemId(int ItemId)
        {
            dtSource = objDataAccess.getPhaViewStockByItemId(ItemId);
            return dtSource;
        }

        public int insertOpeningStock(ComArugments objArg)
        {
            flag = objDataAccess.insertOpeningStock(objArg);
            return flag;
        }

        public int insertOpeningStockdDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertOpeningStockdDetails(objArg);
            return flag;
        }

        public int updateStockAdjustment(ComArugments objArg)
        {
            flag = objDataAccess.updateStockAdjustment(objArg);
            return flag;
        }

        public int insertStockAdjustmentdDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertStockAdjustmentdDetails(objArg);
            return flag;
        }
        #endregion

        #region Save Investigation Bill
        public int insertInvestigationBill(ComArugments objArg)
        {
            flag = objDataAccess.insertInvestigationBill(objArg);
            return flag;
        }

        public int insertInvestigationBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertInvestigationBillDetails(objArg);
            return flag;
        }

        public int insertInvestigationBillDetailsSummary(ComArugments objArg)
        {
            flag = objDataAccess.insertInvestigationBillDetailsSummary(objArg);
            return flag;
        }

        public int updateInvestigationRunningNumber(ComArugments objArg)
        {
            flag = objDataAccess.updateInvestigationRunningNumber(objArg);
            return flag;
        }
        #endregion

        #region Save Pharmacy Bill
        public int insertPharmacyBill(ComArugments objArg)
        {
            flag = objDataAccess.insertPharmacyBill(objArg);
            return flag;
        }

        public int insertPharmacyBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertPharmacyBillDetails(objArg);
            return flag;
        }

        public int insertPharmacyBillDetailsSummary(ComArugments objArg)
        {
            flag = objDataAccess.insertPharmacyBillDetailsSummary(objArg);
            return flag;
        }

        public int updateStockPharmacyBill(ComArugments objArg)
        {
            flag = objDataAccess.updateStockPharmacyBill(objArg);
            return flag;
        }

        public int updatePharmacyRunningNumber(ComArugments objArg)
        {
            flag = objDataAccess.updatePharmacyRunningNumber(objArg);
            return flag;
        }
        #endregion

        #region CIS Billing
        public int insertAccountHead(ComArugments objArg)
        {
            flag = objDataAccess.insertAccountHead(objArg);
            return flag;
        }

        public DataTable getAccountHeadDetails()
        {
            dtSource = objDataAccess.getAccountHeadDetails();
            return dtSource;
        }

        public DataTable getAccountGroup()
        {
            dtSource = objDataAccess.getAccountGroup();
            return dtSource;
        }

        public DataTable loadGeneralCharges()
        {
            dtSource = objDataAccess.loadGeneralCharges();
            return dtSource;
        }

        public int deleteAccountHead(int account_head_id)
        {
            flag = objDataAccess.deleteAccountHead(account_head_id);
            return flag;
        }

        public DataTable getAccoutHeadById(int accountHeadId)
        {
            dtSource = objDataAccess.getAccoutHeadById(accountHeadId);
            return dtSource;
        }

        public DataTable loadWardCharges()
        {
            dtSource = objDataAccess.loadWardCharges();
            return dtSource;
        }

        public DataTable getBillChargesById(int generalChargesId)
        {
            dtSource = objDataAccess.getBillChargesById(generalChargesId);
            return dtSource;
        }

        public DataTable getGeneralInfo(string startDate, string endDate)
        {
            dtSource = objDataAccess.getGeneralInfo(startDate, endDate);
            return dtSource;
        }

        public int advanceTransaction(ComArugments objArg)
        {
            flag = objDataAccess.advanceTransaction(objArg);
            return flag;
        }

        public DataTable getNetAdvAvailbyPatientId(string patient_id, string visit_number)
        {
            dtSource = objDataAccess.getNetAdvAvailbyPatientId(patient_id, visit_number);
            return dtSource;
        }

        public DataTable getFinalBillStatusbyPatientId(string patient_id, string visit_number)
        {
            dtSource = objDataAccess.getFinalBillStatusbyPatientId(patient_id, visit_number);
            return dtSource;
        }

        public DataTable fetchAdvanceTransactionDetails(string startDate, string endDate)
        {
            dtSource = objDataAccess.fetchAdvanceTransactionDetails(startDate, endDate);
            return dtSource;
        }

        //Consolidated Bill Details Start
        public DataTable printConsolidatedInvBill(string patient_id, string visit_number)
        {
            dtSource = objDataAccess.printConsolidatedInvBill(patient_id, visit_number);
            return dtSource;
        }

        public DataTable printConsolidatedPhaBill(string patient_id, string visit_number)
        {
            dtSource = objDataAccess.printConsolidatedPhaBill(patient_id, visit_number);
            return dtSource;
        }

        public DataTable printConsolidatedGenBill(string patient_id, string visit_number)
        {
            dtSource = objDataAccess.printConsolidatedGenBill(patient_id, visit_number);
            return dtSource;
        }

        public DataTable printConsolidatedInterWardBill(string patient_id, string visit_number)
        {
            dtSource = objDataAccess.printConsolidatedInterWardBill(patient_id, visit_number);
            return dtSource;
        }
        //Consolidated Bill Details End
        #endregion

        #region Save General Bill
        public int insertGeneralBill(ComArugments objArg)
        {
            flag = objDataAccess.insertGeneralBill(objArg);
            return flag;
        }

        public int insertGeneralBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertGeneralBillDetails(objArg);
            return flag;
        }

        public int insertGeneralBillDetailsSummary(ComArugments objArg)
        {
            flag = objDataAccess.insertGeneralBillDetailsSummary(objArg);
            return flag;
        }

        public int updateGeneralRunningNumber(ComArugments objArg)
        {
            flag = objDataAccess.updateGeneralRunningNumber(objArg);
            return flag;
        }
        #endregion

        #region Save Ward Bill
        public int insertWardBill(ComArugments objArg)
        {
            flag = objDataAccess.insertWardBill(objArg);
            return flag;
        }

        public int insertWardBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertWardBillDetails(objArg);
            return flag;
        }

        public int insertWardBillDetailsSummary(ComArugments objArg)
        {
            flag = objDataAccess.insertWardBillDetailsSummary(objArg);
            return flag;
        }

        public int insertWardAdvAdjTrans(ComArugments objArg)
        {
            flag = objDataAccess.insertWardAdvAdjTrans(objArg);
            return flag;
        }

        public int updateWardRunningNumber(ComArugments objArg)
        {
            flag = objDataAccess.updateWardRunningNumber(objArg);
            return flag;
        }
        #endregion

        #region CIS Pharmacy Inventory Management
        public DataTable getPhaDetailsForPurchase(string ItemName)
        {
            dtSource = objDataAccess.getPhaDetailsForPurchase(ItemName);
            return dtSource;
        }
        #endregion

        #region Save Purchase Receipt
        public int insertPurchaseReceipt(ComArugments objArg)
        {
            flag = objDataAccess.insertPurchaseReceipt(objArg);
            return flag;
        }

        public int insertPurchaseReceiptDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertPurchaseReceiptDetails(objArg);
            return flag;
        }

        public int updatePurRunningNumber(ComArugments objArg)
        {
            flag = objDataAccess.updatePurRunningNumber(objArg);
            return flag;
        }

        public int insertPurchaseStock(ComArugments objArg)
        {
            flag = objDataAccess.insertPurchaseStock(objArg);
            return flag;
        }

        public int insertPurchaseStockDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertPurchaseStockDetails(objArg);
            return flag;
        }

        public DataTable getPurchaseReceiptInfo(string dateBy, string startDate, string endDate)
        {
            dtSource = objDataAccess.getPurchaseReceiptInfo(dateBy, startDate, endDate);
            return dtSource;
        }

        public DataTable getPurchaseReceiptLoad(int invoice_id)
        {
            dtSource = objDataAccess.getPurchaseReceiptLoad(invoice_id);
            return dtSource;
        }
        #endregion

        #region Cancel or Refund
        public DataTable getBillTypeId(string prefix_text)
        {
            dtSource = objDataAccess.getBillTypeId(prefix_text);
            return dtSource;
        }

        public DataTable getInvestigationBillInfo(string billNo)
        {
            dtSource = objDataAccess.getInvestigationBillInfo(billNo);
            return dtSource;
        }

        public DataTable getInvestigationBillDetailInfo(int billId)
        {
            dtSource = objDataAccess.getInvestigationBillDetailInfo(billId);
            return dtSource;
        }

        public DataTable getPharmacyBillInfo(string billNo)
        {
            dtSource = objDataAccess.getPharmacyBillInfo(billNo);
            return dtSource;
        }

        public DataTable getPharmacyBillDetailInfo(int billId)
        {
            dtSource = objDataAccess.getPharmacyBillDetailInfo(billId);
            return dtSource;
        }

        public DataTable getGeneralBillInfo(string billNo)
        {
            dtSource = objDataAccess.getGeneralBillInfo(billNo);
            return dtSource;
        }

        public DataTable getGeneralBillDetailInfo(int billId)
        {
            dtSource = objDataAccess.getGeneralBillDetailInfo(billId);
            return dtSource;
        }

        public int updateStockRefundPharmacyBill(ComArugments objArg)
        {
            flag = objDataAccess.updateStockRefundPharmacyBill(objArg);
            return flag;
        }

        public int updateRefundPharmacyBill(ComArugments objArg)
        {
            flag = objDataAccess.updateRefundPharmacyBill(objArg);
            return flag;
        }

        public int updateRefundPharmacyPartialBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.updateRefundPharmacyPartialBillDetails(objArg);
            return flag;
        }

        public int insertRefundPharmacyFullBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertRefundPharmacyFullBillDetails(objArg);
            return flag;
        }

        public int updateRefundPharmacyBillDetailSummary(ComArugments objArg)
        {
            flag = objDataAccess.updateRefundPharmacyBillDetailSummary(objArg);
            return flag;
        }

        public int InsertRefundPharmacyBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.InsertRefundPharmacyBillDetails(objArg);
            return flag;
        }

        public int InsertRefundPharmacyPartialBillDetailSummary(ComArugments objArg)
        {
            flag = objDataAccess.InsertRefundPharmacyPartialBillDetailSummary(objArg);
            return flag;
        }

        public int InsertRefundPharmacyFullBillDetailSummary(ComArugments objArg)
        {
            flag = objDataAccess.InsertRefundPharmacyFullBillDetailSummary(objArg);
            return flag;
        }


        //Investigation Begin
        public int updateRefundInvestigationBill(ComArugments objArg)
        {
            flag = objDataAccess.updateRefundInvestigationBill(objArg);
            return flag;
        }

        public int updateRefundInvestigationPartialBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.updateRefundInvestigationPartialBillDetails(objArg);
            return flag;
        }

        public int insertRefundInvestigationFullBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertRefundInvestigationFullBillDetails(objArg);
            return flag;
        }

        public int updateRefundInvestigationBillDetailSummary(ComArugments objArg)
        {
            flag = objDataAccess.updateRefundInvestigationBillDetailSummary(objArg);
            return flag;
        }

        public int InsertRefundInvestigationBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.InsertRefundInvestigationBillDetails(objArg);
            return flag;
        }

        public int InsertRefundInvestigationPartialBillDetailSummary(ComArugments objArg)
        {
            flag = objDataAccess.InsertRefundInvestigationPartialBillDetailSummary(objArg);
            return flag;
        }

        public int InsertRefundInvestigationFullBillDetailSummary(ComArugments objArg)
        {
            flag = objDataAccess.InsertRefundInvestigationFullBillDetailSummary(objArg);
            return flag;
        }
        //Investigation End

        //General Begin
        public int updateRefundGeneralBill(ComArugments objArg)
        {
            flag = objDataAccess.updateRefundGeneralBill(objArg);
            return flag;
        }

        public int updateRefundGeneralPartialBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.updateRefundGeneralPartialBillDetails(objArg);
            return flag;
        }

        public int InsertRefundGeneralBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.InsertRefundGeneralBillDetails(objArg);
            return flag;
        }

        public int updateRefundGeneralBillDetailSummary(ComArugments objArg)
        {
            flag = objDataAccess.updateRefundGeneralBillDetailSummary(objArg);
            return flag;
        }

        public int InsertRefundGeneralPartialBillDetailSummary(ComArugments objArg)
        {
            flag = objDataAccess.InsertRefundGeneralPartialBillDetailSummary(objArg);
            return flag;
        }

        public int insertRefundGeneralFullBillDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertRefundGeneralFullBillDetails(objArg);
            return flag;
        }

        public int InsertRefundGeneralFullBillDetailSummary(ComArugments objArg)
        {
            flag = objDataAccess.InsertRefundGeneralFullBillDetailSummary(objArg);
            return flag;
        }
        //General End
        #endregion

        public DataTable getPatientDetails()
        {
            dtSource = objDataAccess.getPatientDetails();
            return dtSource;
        }

        #region Printing Info
        public DataTable printRegistationSlip(string visit_number)
        {
            dtSource = objDataAccess.printRegistationSlip(visit_number);
            return dtSource;
        }

        public DataTable printRegistationBill(string reg_bill_number)
        {
            dtSource = objDataAccess.printRegistationBill(reg_bill_number);
            return dtSource;
        }

        public DataTable printInvestigationBill(string inv_bill_number)
        {
            dtSource = objDataAccess.printInvestigationBill(inv_bill_number);
            return dtSource;
        }

        public DataTable printPharmacyBill(string pha_bill_number)
        {
            dtSource = objDataAccess.printPharmacyBill(pha_bill_number);
            return dtSource;
        }

        public DataTable printRefundPharmacyBill(string pha_bill_number)
        {
            dtSource = objDataAccess.printRefundPharmacyBill(pha_bill_number);
            return dtSource;
        }

        public DataTable printGeneralBill(string gen_bill_number)
        {
            dtSource = objDataAccess.printGeneralBill(gen_bill_number);
            return dtSource;
        }

        public DataTable printRefundGeneralBill(string gen_bill_number)
        {
            dtSource = objDataAccess.printRefundGeneralBill(gen_bill_number);
            return dtSource;
        }

        public DataTable printWardBill(string ward_bill_number)
        {
            dtSource = objDataAccess.printWardBill(ward_bill_number);
            return dtSource;
        }

        public DataTable printAdvanceBill(string adv_bill_number)
        {
            dtSource = objDataAccess.printAdvanceBill(adv_bill_number);
            return dtSource;
        }
        #endregion

        #region Report Info
        public DataTable viewRegCensusMedDepartmentByGender(ComArugments objArg) // Total Medical Department and Gender Patient Census
        {
            dtSource = objDataAccess.viewRegCensusMedDepartmentByGender(objArg);
            return dtSource;
        }

        public DataTable viewRegCensusMonthVisitModeByGender(ComArugments objArg) // Total M.Department, Gender and Visit Mode Patient Census - Montly
        {
            dtSource = objDataAccess.viewRegCensusMonthVisitModeByGender(objArg);
            return dtSource;
        }

        public DataTable viewOPRegistationList(ComArugments objArg)
        {
            dtSource = objDataAccess.viewOPRegistationList(objArg);
            return dtSource;
        }

        public DataTable viewIPAdmissionList(ComArugments objArg)
        {
            dtSource = objDataAccess.viewIPAdmissionList(objArg);
            return dtSource;
        }

        public DataTable viewIPDischargeList(ComArugments objArg)
        {
            dtSource = objDataAccess.viewIPDischargeList(objArg);
            return dtSource;
        }

        public DataTable viewCurrentIPList()
        {
            dtSource = objDataAccess.viewCurrentIPList();
            return dtSource;
        }

        public DataTable DailyCollection(ComArugments objArg)
        {
            dtSource = objDataAccess.DailyCollection(objArg);
            return dtSource;
        }

        public DataTable DueList(ComArugments objArg)
        {
            dtSource = objDataAccess.DueList(objArg);
            return dtSource;
        }

        public DataTable printdueCollection(ComArugments billIds)
        {
            dtSource = objDataAccess.printdueCollection(billIds);
            return dtSource;
        }

        public DataTable InvestigationList(ComArugments objArg)
        {
            dtSource = objDataAccess.InvestigationList(objArg);
            return dtSource;
        }

        public DataTable InvestigationShareList(ComArugments objArg)
        {
            dtSource = objDataAccess.InvestigationShareList(objArg);
            return dtSource;
        }

        public DataTable CorporateDueList(ComArugments objArg)
        {
            dtSource = objDataAccess.CorporateDueList(objArg);
            return dtSource;
        }

        public DataTable CorporateDueListDetails(ComArugments objArg)
        {
            dtSource = objDataAccess.CorporateDueListDetails(objArg);
            return dtSource;
        }

        public DataTable DailyCollectionSummary(ComArugments objArg)
        {
            dtSource = objDataAccess.DailyCollectionSummary(objArg);
            return dtSource;
        }

        public DataTable medicineList(ComArugments objArg)
        {
            dtSource = objDataAccess.MedicineList(objArg);
            return dtSource;
        }

        public DataTable currentStockReport(ComArugments objArg)
        {
            dtSource = objDataAccess.currentStockReport(objArg);
            return dtSource;
        }

        public DataTable expiryMedicineList(ComArugments objArg)
        {
            dtSource = objDataAccess.expiryMedicineList(objArg);
            return dtSource;
        }

        public DataTable purchaseMedicineReport(ComArugments objArg)
        {
            dtSource = objDataAccess.purchaseMedicineReport(objArg);
            return dtSource;
        }

        public DataTable refundedMedicineList(ComArugments objArg)
        {
            dtSource = objDataAccess.refundedMedicineList(objArg);
            return dtSource;
        }
        #endregion

        #region View Bill Info
        public DataTable viewRegBillInfoByVisit(string visitQry)
        {
            dtSource = objDataAccess.viewRegBillInfoByVisit(visitQry);
            return dtSource;
        }

        public DataTable viewInvBillInfoByVisit(string visitQry)
        {
            dtSource = objDataAccess.viewInvBillInfoByVisit(visitQry);
            return dtSource;
        }
        public DataTable viewPharmacyBillInfoByVisit(string visitQry)
        {
            dtSource = objDataAccess.viewPharmacyBillInfoByVisit(visitQry);
            return dtSource;
        }

        public DataTable viewGenBillInfoByVisit(string visitQry)
        {
            dtSource = objDataAccess.viewGenBillInfoByVisit(visitQry);
            return dtSource;
        }

        public DataTable viewWardBillInfoByVisit(string visitQry)
        {
            dtSource = objDataAccess.viewWardBillInfoByVisit(visitQry);
            return dtSource;
        }

        public DataTable viewAllBillInfoByVisit(string visitQry)
        {
            dtSource = objDataAccess.viewAllBillInfoByVisit(visitQry);
            return dtSource;
        }
        #endregion

        #region Due Collection All Bills
        public int updateRegistrationDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.updateRegistrationDueCollection(objArg);
            return flag;
        }

        public int insertRegistrationSummaryDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.insertRegistrationSummaryDueCollection(objArg);
            return flag;
        }

        public int updateInvestigationDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.updateInvestigationDueCollection(objArg);
            return flag;
        }

        public int insertInvestigationSummaryDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.insertInvestigationSummaryDueCollection(objArg);
            return flag;
        }

        public int updatePharmacyDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.updatePharmacyDueCollection(objArg);
            return flag;
        }

        public int insertPharmacySummaryDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.insertPharmacySummaryDueCollection(objArg);
            return flag;
        }

        public int updateGeneralDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.updateGeneralDueCollection(objArg);
            return flag;
        }

        public int insertGeneralSummaryDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.insertGeneralSummaryDueCollection(objArg);
            return flag;
        }

        public int updateBillingDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.updateBillingDueCollection(objArg);
            return flag;
        }

        public int insertBillingSummaryDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.insertBillingSummaryDueCollection(objArg);
            return flag;
        }

        public int insertWardAdvAdjDueCollection(ComArugments objArg)
        {
            flag = objDataAccess.insertWardAdvAdjDueCollection(objArg);
            return flag;
        }
        #endregion

        #region Inventory Movements
        public DataTable loadInternalMoveDepartment()
        {
            dtSource = objDataAccess.loadInternalMoveDepartment();
            return dtSource;
        }

        public DataTable consumeType()
        {
            dtSource = objDataAccess.consumeType();
            return dtSource;
        }

        public DataTable getPhaByItemIdAll(int phaItemId)
        {
            dtSource = objDataAccess.getPhaByItemIdAll(phaItemId);
            return dtSource;
        }

        public int insertInventoryMovements(ComArugments objArg)
        {
            flag = objDataAccess.insertInventoryMovements(objArg);
            return flag;
        }

        public int insertInventoryMovementsDetails(ComArugments objArg)
        {
            flag = objDataAccess.insertInventoryMovementsDetails(objArg);
            return flag;
        }

        public int updateInventoryMovementsRunningNumber(ComArugments objArg)
        {
            flag = objDataAccess.updateInventoryMovementsRunningNumber(objArg);
            return flag;
        }

        public DataTable loadInventoryMovementsDetails(string startDate, string endDate, int inventoryMoveTypeId)
        {
            dtSource = objDataAccess.loadInventoryMovementsDetails(startDate, endDate, inventoryMoveTypeId);
            return dtSource;
        }
        #endregion

        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="Dob">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
        public static string CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;
            return Years.ToString() + "," + Months.ToString() + "," + Days.ToString();
           // return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
           // Years, Months, Days, Hours, Seconds);
        }   

    }
}
