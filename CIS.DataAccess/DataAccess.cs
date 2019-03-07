using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using CIS.DBHandler;
using CIS.Common;


namespace CIS.DataAccess
{
    public class DataAccess
    {
        #region Declaration
        string qry = string.Empty;
        DBHandler.DBHandler objDBHandler = new DBHandler.DBHandler();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        int flag = 0;
        string rowId;
        #endregion

        #region Common
        public int lastInsertRecord()
        {
            string last_inserted_id = "select last_insert_id()";
            rowId = objDBHandler.ExecuteTransactionQuery(last_inserted_id); // Get Last Inserted Transaction Id
            int bill_id = Convert.ToInt32(rowId);
            objDBHandler.commitTransaction(TransactionType.Commit);
            return bill_id;
        }
        #endregion

        #region Login and User_Roles details
        public DataTable getLoginUserDetails(string CIS_User_Id)
        {
            qry = @"SELECT 
                        USER_ID, USER_NAME, PASSWORD, FULL_NAME, USER_TYPE, STATUS,USER_ROLE_ID
                    FROM
                        cis_user
                    WHERE
                        USER_NAME ='" + CIS_User_Id + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        public DataTable getUserActivities(int role_id, int partent_id)
        {
            qry = @"select 
                        a.action_id, a.action
                    from
                        cis_map_role_action r
                            join
                        cis_actions a ON r.action_id = a.action_id
                    where
                        role_id =" + role_id + "  and a.parent = " + partent_id + " order by action_order asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getUserRoleId(int user_id)
        {
            qry = @"SELECT 
                        user_role_id
                    FROM
                        cis_user
                    where
                        user_id = " + user_id + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getRegInvoiceTabControls()
        {
            qry = @"SELECT 
                        action_id, action
                    FROM
                        cis_actions
                    where
                        parent = 2";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        #endregion

        #region CIS_Department details
        public DataTable getDepartmentDetails()
        {
            qry = @"SELECT 
                        DEPARTMENT_ID,
                        DEPARTMENT_CATEGORY_ID,
                        DEPARTMENT_CODE AS 'Department Code',
                        DEPARTMENT_NAME AS 'Department Name',
                        IF(DEPARTMENT_TYPE = 1,
                            'Medical',
                            'Non-Medical') AS 'Department Type',
                        CASE
                            WHEN DEPARTMENT_CATEGORY_ID = 1 THEN 'OP Registraion'
                            WHEN DEPARTMENT_CATEGORY_ID = 2 THEN 'IP Registration'
                            WHEN DEPARTMENT_CATEGORY_ID = 3 THEN 'Investigation'
                            WHEN DEPARTMENT_CATEGORY_ID = 4 THEN 'Pharmacy'
                            WHEN DEPARTMENT_CATEGORY_ID = 5 THEN 'Billing'
                            WHEN DEPARTMENT_CATEGORY_ID = 6 THEN 'Ward'
                            ELSE 'Non'
                        END AS 'Department Category',
                        IF(STATUS = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_department
                    order by DEPARTMENT_CATEGORY_ID";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertDepartmnet(ComArugments objArg)
        {
            qry = "INSERT INTO cis_department(department_code, department_name, department_type, DEPARTMENT_CATEGORY_ID, STATUS) VALUES ('" + objArg.ParamList["department_code"].ToString() + "','" + objArg.ParamList["department_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["department_type"]) + ", " + Convert.ToInt32(objArg.ParamList["DEPARTMENT_CATEGORY_ID"]) + ", " + Convert.ToInt32(objArg.ParamList["STATUS"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteDepartment(int cisDepartmentId)
        {
            qry = "delete from cis_department where department_id=" + cisDepartmentId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getDepartmentRecord(int departmentId)
        {
            qry = @"SELECT 
                        DEPARTMENT_ID,
                        department_code,
                        department_name,
                        department_type,
                        DEPARTMENT_CATEGORY_ID,
                        STATUS
                    FROM
                        cis_department
                    where
                        DEPARTMENT_ID =" + departmentId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateDepartment(ComArugments objArg)
        {
            qry = "UPDATE cis_department SET DEPARTMENT_CODE='" + objArg.ParamList["department_code"].ToString() + "', DEPARTMENT_NAME='" + objArg.ParamList["department_name"].ToString() + "', DEPARTMENT_TYPE=" + Convert.ToInt32(objArg.ParamList["department_type"]) + ", DEPARTMENT_CATEGORY_ID=" + Convert.ToInt32(objArg.ParamList["DEPARTMENT_CATEGORY_ID"]) + ", STATUS=" + Convert.ToInt32(objArg.ParamList["STATUS"]) + " WHERE DEPARTMENT_ID=" + Convert.ToInt32(objArg.ParamList["departmentId"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable loadRegistrationDepartment()
        {
            qry = @"SELECT 
                        DEPARTMENT_ID, DEPARTMENT_NAME
                    FROM
                        cis_department
                    where
                        DEPARTMENT_CATEGORY_ID IN (1 , 2)
                            AND DEPARTMENT_TYPE = 2
                            and status = 1
                    ORDER BY DEPARTMENT_CATEGORY_ID ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadClinicalType()
        {
            qry = @"SELECT 
                        DEPARTMENT_ID, DEPARTMENT_NAME
                    FROM
                        cis_department
                    where
                        DEPARTMENT_TYPE = 1 and status = 1
                    ORDER BY DEPARTMENT_NAME ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadWard()
        {
            qry = @"SELECT 
                        DEPARTMENT_ID as WARD_DEPARTMENT_ID, DEPARTMENT_NAME AS WARD_DEPARTMENT_NAME
                    FROM
                        cis_department
                    where
                        DEPARTMENT_CATEGORY_ID = 6
                            and status = 1
                    ORDER BY DEPARTMENT_NAME ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadWardReg()//For Registration Page, Filtering only available beds
        {
            qry = @"SELECT 
                        d.DEPARTMENT_ID as WARD_DEPARTMENT_ID,
                        d.DEPARTMENT_NAME AS WARD_DEPARTMENT_NAME
                    FROM
                        cis_department d
                            JOIN
                        cis_bed b ON d.DEPARTMENT_ID = b.ward_id
                    where
                        d.DEPARTMENT_CATEGORY_ID = 6
                            and d.status = 1
                            and b.status = 1
                            and b.patient_id is null
                    group by d.DEPARTMENT_ID
                    ORDER BY d.DEPARTMENT_NAME ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getDepartmentCategoryId(int cisDepartmentId)
        {
            qry = @"SELECT 
                        DEPARTMENT_CATEGORY_ID
                    FROM
                        cis_department
                    where
                        department_id=" + cisDepartmentId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        #endregion

        #region CIS_Patient Type details
        public DataTable getPatientTypeDetails()
        {
            qry = @"SELECT 
                        patient_type_id,
                        patient_type AS 'Patient Type',
                        IF(STATUS = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_patient_type";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertPatientType(ComArugments objArg)
        {
            qry = "INSERT INTO cis_patient_type(patient_type, STATUS) VALUES ('" + objArg.ParamList["patient_type"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["STATUS"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deletePatientType(int cisPatientTypeId)
        {
            qry = "delete from cis_patient_type where patient_type_id=" + cisPatientTypeId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getPatientTypeRecord(int patientTypeId)
        {
            qry = @"SELECT 
                        patient_type_id,
                        patient_type,
                        STATUS
                    FROM
                        cis_patient_type
                    WHERE
                        patient_type_id =" + patientTypeId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updatePatientType(ComArugments objArg)
        {
            qry = "UPDATE cis_patient_type SET patient_type='" + objArg.ParamList["patient_type"].ToString() + "', STATUS=" + Convert.ToInt32(objArg.ParamList["STATUS"]) + "  WHERE patient_type_id=" + Convert.ToInt32(objArg.ParamList["patient_type_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable loadPatientType()
        {
            qry = @"SELECT 
                        patient_type_id, patient_type
                    FROM
                        cis_patient_type
                    WHERE
                        status = 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadReferralBy()
        {
            qry = @"SELECT 
                        cis_referral_id, referral_name
                    FROM
                        cis_referral
                    where
                        status = 1
                    order by referral_name asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region CIS_Doctor details
        public DataTable getDoctorDetails()
        {
            qry = @"SELECT 
                        doctor_id,
                        doctor_code AS 'Doctor Code',
                        doctor_name AS 'Doctor Name',
                        IF(doctor_type = 1,
                            'Residential',
                            'Consultant') AS 'Doctor Type',
                        IF(Gender = 0, 'Male', 'Female') AS Gender,
                        Qualification,
                        Specialization,
                        room_number AS 'Room Number',
                        IF(STATUS = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_doctor
                    ORDER BY doctor_name ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getReferralDetails()
        {
            qry = @"SELECT 
                        cis_referral_id as referral_id,
                        referral_name as 'Referral Name',
                        contact_no as 'Contact No',
                        contact_address as 'Contact Address',
                        IF(status = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_referral
                    order by referral_name asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertDoctor(ComArugments objArg)
        {
            qry = "INSERT INTO cis_doctor(DOCTOR_CODE, DOCTOR_NAME, DOCTOR_TYPE, GENDER, QUALIFICATION, SPECIALIZATION, ROOM_NUMBER, STATUS) VALUES ('" + objArg.ParamList["doctor_code"].ToString() + "', '" + objArg.ParamList["doctor_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["doctor_type"]) + ", " + Convert.ToInt32(objArg.ParamList["Gender"]) + ", '" + objArg.ParamList["Qualification"].ToString() + "', '" + objArg.ParamList["Specialization"].ToString() + "', '" + objArg.ParamList["room_number"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["STATUS"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteDoctor(int doctor_id)
        {
            qry = "delete from cis_doctor where doctor_id=" + doctor_id + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteReferral(int referral_id)
        {
            qry = "delete from cis_referral where cis_referral_id=" + referral_id + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getDoctorRecord(int doctor_id)
        {
            qry = @"SELECT 
                        doctor_id,
                        doctor_code,
                        doctor_name,
                        Gender,
                        doctor_type,
                        Qualification,
                        Specialization,
                        room_number,
                        STATUS
                    FROM
                        cis_doctor
                    where
                        doctor_id =" + doctor_id + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateDoctor(ComArugments objArg)
        {
            qry = "UPDATE cis_doctor SET DOCTOR_CODE='" + objArg.ParamList["doctor_code"].ToString() + "', DOCTOR_NAME='" + objArg.ParamList["doctor_name"].ToString() + "', GENDER=" + Convert.ToInt32(objArg.ParamList["Gender"]) + ", DOCTOR_TYPE=" + Convert.ToInt32(objArg.ParamList["doctor_type"]) + ", Qualification='" + objArg.ParamList["Qualification"].ToString() + "', SPECIALIZATION='" + objArg.ParamList["Specialization"].ToString() + "', room_number='" + objArg.ParamList["room_number"].ToString() + "', STATUS=" + Convert.ToInt32(objArg.ParamList["STATUS"]) + " WHERE DOCTOR_ID=" + Convert.ToInt32(objArg.ParamList["DOCTOR_ID"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }


        #endregion

        #region CIS_Address_Info details
        public DataTable getAddressInfoDetails()
        {
            qry = @"SELECT
                        address_id, 
                        Place,
                        District,
                        State,
                        postal_code as 'Postal Code',
                        IF(STATUS = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_address_info";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertAddressInfo(ComArugments objArg)
        {
            qry = "INSERT INTO cis_address_info(Place, District, State, postal_code, Status) VALUES ( '" + objArg.ParamList["place"].ToString() + "', '" + objArg.ParamList["district"].ToString() + "', '" + objArg.ParamList["state"].ToString() + "', '" + objArg.ParamList["postal_code"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["STATUS"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteAddressInfo(int addressId)
        {
            qry = "delete from cis_address_info where address_id=" + addressId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getAddressInfoRecord(int addressId)
        {
            qry = @"SELECT 
                        address_id, Place, District, State, postal_code, Status
                    FROM
                        cis_address_info
                    WHERE
                        address_id =" + addressId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable fetchAddressInfobyPlace(int address_id)
        {
            qry = @"SELECT 
                            concat_ws('\r\n',
                            place,
                            district,
                            state,
                            postal_code) as address
                    FROM
                        cis_address_info
                    WHERE
                        status = 1 and address_id= " + address_id + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadAddress()
        {
            qry = @"SELECT 
                        address_id, place
                    FROM
                        cis_address_info
                    where
                        status = 1
                    order by place asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateAddressInfo(ComArugments objArg)
        {
            qry = "UPDATE cis_address_info SET Place='" + objArg.ParamList["place"].ToString() + "', district='" + objArg.ParamList["district"].ToString() + "', state='" + objArg.ParamList["state"].ToString() + "', postal_code='" + objArg.ParamList["postal_code"].ToString() + "', STATUS=" + Convert.ToInt32(objArg.ParamList["STATUS"]) + "  WHERE address_id=" + Convert.ToInt32(objArg.ParamList["address_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        #endregion

        #region CIS_Corporate details
        public DataTable getCorporateDetails()
        {
            qry = @"SELECT 
                        corporate_id,
                        corporate_name AS 'Corporate Name',
                        Address,
                        IF(STATUS = 1, 'Active', 'Inactive') AS Status,
                        IF(is_charges_applicable = 1, 'Yes', 'No') AS 'Is Fee Applicable'
                    FROM
                        cis_corporate";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertCorporate(ComArugments objArg)
        {
            qry = "INSERT INTO cis_corporate(corporate_name, Address, STATUS, is_charges_applicable) VALUES ('" + objArg.ParamList["corporate_name"].ToString() + "', '" + objArg.ParamList["address"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["STATUS"]) + ", " + Convert.ToInt32(objArg.ParamList["is_fee_applicable"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteCorporate(int CorporateId)
        {
            qry = "delete from cis_corporate where corporate_id=" + CorporateId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getCorporateRecord(int CorporateId)
        {
            qry = @"SELECT 
                        corporate_id,
                        corporate_name,
                        address,
                        STATUS,
                        is_charges_applicable
                    FROM
                        cis_corporate
                    where
                        corporate_id =" + CorporateId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateCorporate(ComArugments objArg)
        {
            qry = "UPDATE cis_corporate SET corporate_name='" + objArg.ParamList["corporate_name"].ToString() + "', address='" + objArg.ParamList["address"].ToString() + "', STATUS=" + Convert.ToInt32(objArg.ParamList["STATUS"]) + ", is_charges_applicable=" + Convert.ToInt32(objArg.ParamList["is_fee_applicable"]) + "  WHERE corporate_id=" + Convert.ToInt32(objArg.ParamList["corporate_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable loadCorporate()
        {
            qry = @"SELECT 
                        corporate_id, corporate_name
                    FROM
                        cis_corporate
                    where
                        status = 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region CIS_Room details
        public DataTable getRoomDetails(int wardId)
        {
            qry = @"SELECT 
                        room_id,
                        room_no,
                        IF(status = 1, 'Active', 'Inactive') AS status
                    FROM
                        cis_room
                    WHERE
                        ward_id = " + wardId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getBedDetails(int wardId, int roomId)
        {
            qry = @"SELECT 
                        bed_id,
                        bed_number,
                        IF(status = 1, 'Active', 'Inactive') AS status
                    FROM
                        cis_bed
                    where
                        ward_id = " + wardId + " and room_id = " + roomId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertRoom(ComArugments objArg)
        {
            if (Convert.ToInt32(objArg.ParamList["room_id"]) == 0)
            {
                qry = "INSERT INTO cis_room(ward_id, room_no, status) VALUES (" + objArg.ParamList["ward_id"].ToString() + ", '" + objArg.ParamList["room_no"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
                flag = objDBHandler.ExecuteCommand(qry);
            }
            else
            {
                qry = "UPDATE cis_room SET room_no ='" + objArg.ParamList["room_no"].ToString() + "', status =" + Convert.ToInt32(objArg.ParamList["status"]) + " WHERE room_id= " + Convert.ToInt32(objArg.ParamList["room_id"]) + "";
                flag = objDBHandler.ExecuteCommand(qry);
            }
            return flag;
        }

        public int insertBed(ComArugments objArg)
        {
            if (Convert.ToInt32(objArg.ParamList["bed_id"]) == 0)
            {
                qry = "INSERT INTO cis_bed(bed_number, room_id, ward_id, status) VALUES ('" + objArg.ParamList["bed_no"].ToString() + "', " + objArg.ParamList["room_id"].ToString() + ", " + objArg.ParamList["ward_id"].ToString() + ",  " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
                flag = objDBHandler.ExecuteCommand(qry);
            }
            else
            {
                qry = "UPDATE cis_bed SET bed_number ='" + objArg.ParamList["bed_no"].ToString() + "', status =" + Convert.ToInt32(objArg.ParamList["status"]) + " WHERE bed_id= " + Convert.ToInt32(objArg.ParamList["bed_id"]) + "";
                flag = objDBHandler.ExecuteCommand(qry);
            }
            return flag;
        }

        public DataTable loadRoom(int wardId)
        {
            qry = @"SELECT 
                        room_id, room_no
                    FROM
                        cis_room
                    where
                        ward_id =" + wardId + " and status = 1 order by room_no asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadRoomReg(int wardId) //For Registration Page, Filter only Available Beds
        {
            qry = @"SELECT 
                        r.room_id, r.room_no
                    FROM
                        cis_room r
                            JOIN
                        cis_bed b ON r.room_id = b.room_id
                    where
                        r.ward_id =" + wardId + " and r.status = 1 and b.status = 1 and b.patient_id is NULL group by r.room_id order by r.room_no asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }


        public DataTable loadBed(int roomId)
        {
            qry = @"SELECT 
                        bed_id, bed_number
                    from
                        cis_bed
                    where
                        room_id =" + roomId + " and status = 1 order by bed_number asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadBedReg(int roomId) //For Registration Page, Filter only Available Beds
        {
            qry = @"SELECT 
                        bed_id, bed_number
                    from
                        cis_bed
                    where
                        room_id =" + roomId + " and status = 1 and patient_id is null order by bed_number asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region CIS_Define Reg Fee
        public DataTable loadDoctor()
        {
            qry = @"SELECT 
                        DOCTOR_ID, DOCTOR_NAME
                    FROM
                        cis_doctor
                    WHERE
                        status = 1
                    ORDER BY DOCTOR_NAME ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadDepartment()
        {
            qry = @"SELECT 
                        DEPARTMENT_ID, DEPARTMENT_NAME
                    FROM
                        cis_department
                    where
                        DEPARTMENT_CATEGORY_ID in (1 , 2)
                        AND DEPARTMENT_TYPE = 2
                            and status = 1
                    ORDER BY DEPARTMENT_CATEGORY_ID ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadSocialTitle()
        {
            qry = @"SELECT 
                        social_title_id, social_title
                    FROM
                        cis_social_title";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getGenderIdBySocialTitleId(int social_title_id)
        {
            qry = @"SELECT 
                        gender_id
                    FROM
                        cis_social_title
                    where
                        social_title_id =" + social_title_id + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getDefineRegFeeDetails()
        {
            qry = @"SELECT 
                        define_reg_fee_id,
                        DEPARTMENT_NAME AS 'Department Name',
                        DOCTOR_NAME AS 'Doctor Name',
                        new_reg_fee as 'New Reg Fee',
                        Validity,
                        CASE
                            WHEN validity_period = 0 THEN 'Every Visit'
                            WHEN validity_period = 1 THEN 'Day'
                            WHEN validity_period = 2 THEN 'Month'
                            WHEN validity_period = 3 THEN 'Year'
                            ELSE 'Non'
                        END AS 'Validity Period',
                        revisit_reg_fee AS 'Revisit Reg Fee'
                    FROM
                        cis_define_reg_fee f
                            left join
                        cis_department d ON f.department_id = d.DEPARTMENT_ID
                            left join
                        cis_doctor dr ON f.doctor_id = dr.DOCTOR_ID
                    where
                        d.status = 1 and dr.status = 1
                    ORDER BY DOCTOR_NAME ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertDefineRegFee(ComArugments objArg)
        {
            qry = "INSERT INTO cis_define_reg_fee(department_id, doctor_id, new_reg_fee, validity, validity_period, revisit_reg_fee) VALUES (" + Convert.ToInt32(objArg.ParamList["department_id"]) + ", " + Convert.ToInt32(objArg.ParamList["doctor_id"]) + ", " + Convert.ToDouble(objArg.ParamList["new_reg_fee"]) + ", " + Convert.ToInt32(objArg.ParamList["validity"]) + ", " + Convert.ToInt32(objArg.ParamList["validity_period"]) + ", " + Convert.ToDouble(objArg.ParamList["revisit_reg_fee"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteDefineRegFee(int DefineRegFeeId)
        {
            qry = "delete from cis_define_reg_fee where define_reg_fee_id=" + DefineRegFeeId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getDefineRegFeeRecord(int DefineRegFeeId)
        {
            qry = @"SELECT 
                        define_reg_fee_id,
                        department_id,
                        doctor_id,
                        new_reg_fee,
                        validity,
                        validity_period,
                        revisit_reg_fee
                    FROM
                        cis_define_reg_fee
                    where
                        define_reg_fee_id =" + DefineRegFeeId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable checkDefineRegFeeRecord(ComArugments objArg)
        {
            qry = @"SELECT 
                        define_reg_fee_id,
                        new_reg_fee,
                        validity,
                        validity_period,
                        revisit_reg_fee
                    FROM
                        cis_define_reg_fee
                    where
                        department_id = " + Convert.ToInt32(objArg.ParamList["department_id"]) + " and doctor_id =" + Convert.ToInt32(objArg.ParamList["doctor_id"]) + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }


        public int updateDefineRegFee(ComArugments objArg)
        {
            qry = "UPDATE cis_define_reg_fee SET department_id=" + Convert.ToInt32(objArg.ParamList["department_id"]) + ", doctor_id=" + Convert.ToInt32(objArg.ParamList["doctor_id"]) + ", new_reg_fee=" + Convert.ToDouble(objArg.ParamList["new_reg_fee"]) + ", validity=" + Convert.ToInt32(objArg.ParamList["validity"]) + ", validity_period=" + Convert.ToInt32(objArg.ParamList["validity_period"]) + ", revisit_reg_fee=" + Convert.ToDouble(objArg.ParamList["revisit_reg_fee"]) + "  WHERE define_reg_fee_id=" + Convert.ToInt32(objArg.ParamList["define_reg_fee_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        #endregion

        #region CIS_Number_Format details
        public DataTable getNumberFormat()
        {
            qry = @"SELECT 
                        number_format_id,
                        field_name,
                        number_format,
                        prefix_date,
                        prefix_text,
                        last_bill_number,
                        running_number
                    FROM
                        cis_number_format";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getPurchaseReceiptNumberFormat()
        {
            qry = @"SELECT 
                        number_format_id,
                        field_name,
                        number_format,
                        prefix_date,
                        prefix_text,
                        last_bill_number,
                        running_number
                    FROM
                        cis_number_format where number_format_id=8";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getInventoryMovementsNumberFormat()
        {
            qry = @"SELECT 
                        number_format_id,
                        field_name,
                        number_format,
                        prefix_date,
                        prefix_text,
                        last_bill_number,
                        running_number
                    FROM
                        cis_number_format where number_format_id=12";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getAdvanceTransNumberFormat()
        {
            qry = @"SELECT 
                        number_format_id,
                        field_name,
                        number_format,
                        prefix_date,
                        prefix_text,
                        last_bill_number,
                        running_number
                    FROM
                        cis_number_format
                    where
                        number_format_id in (9 , 10)";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getWardNumberFormat()
        {
            qry = @"SELECT 
                        number_format_id,
                        field_name,
                        number_format,
                        prefix_date,
                        prefix_text,
                        last_bill_number,
                        running_number
                    FROM
                        cis_number_format
                    where
                        number_format_id in (11)";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region Registration details
        public int insertRegistration(ComArugments objArg)
        {
            if (Convert.ToInt32(objArg.ParamList["visitMode"]) == 0)
            {
                qry = "INSERT INTO pat_reg_info (patient_id, social_title_id, social_title, patient_name, patient_type_id, gender, dob, age_year, age_month, age_day, guardian_name, address, phone_no, last_visit_number, last_visit_type, last_visit_date) VALUES (" + Convert.ToInt32(objArg.ParamList["patient_id"]) + ", " + Convert.ToInt32(objArg.ParamList["social_title_id"]) + ", '" + objArg.ParamList["social_title"].ToString() + "', '" + objArg.ParamList["patient_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["patientTypeId"]) + ", " + Convert.ToInt32(objArg.ParamList["gender"]) + ", '" + Convert.ToDateTime(objArg.ParamList["dob"]).ToString("yyyy-MM-dd") + "', " + Convert.ToInt32(objArg.ParamList["age_year"]) + ", " + Convert.ToInt32(objArg.ParamList["age_month"]) + ", " + Convert.ToInt32(objArg.ParamList["age_day"]) + ", '" + objArg.ParamList["guardian_name"].ToString() + "', '" + objArg.ParamList["address"].ToString() + "', '" + objArg.ParamList["phone_no"].ToString() + "', " + Convert.ToInt64(objArg.ParamList["visit_number"]) + ", " + Convert.ToInt32(objArg.ParamList["visit_type"]) + ", '" + Convert.ToDateTime(objArg.ParamList["visit_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "')";
                flag = objDBHandler.ExecuteCommand(qry);
            }
            else
            {
                qry = "UPDATE pat_reg_info SET social_title_id =" + Convert.ToInt32(objArg.ParamList["social_title_id"]) + ", social_title = '" + objArg.ParamList["social_title"].ToString() + "', patient_name = '" + objArg.ParamList["patient_name"].ToString() + "', patient_type_id = " + Convert.ToInt32(objArg.ParamList["patientTypeId"]) + ",   gender = " + Convert.ToInt32(objArg.ParamList["gender"]) + ",  dob = '" + Convert.ToDateTime(objArg.ParamList["dob"]).ToString("yyyy-MM-dd") + "',  age_year = " + Convert.ToInt32(objArg.ParamList["age_year"]) + ",  age_month =  " + Convert.ToInt32(objArg.ParamList["age_month"]) + ",  age_day = " + Convert.ToInt32(objArg.ParamList["age_day"]) + ",  guardian_name = '" + objArg.ParamList["guardian_name"].ToString() + "',  address = '" + objArg.ParamList["address"].ToString() + "', phone_no = '" + objArg.ParamList["phone_no"].ToString() + "',  last_visit_number = " + Convert.ToInt64(objArg.ParamList["visit_number"]) + ",  last_visit_type = " + Convert.ToInt32(objArg.ParamList["visit_type"]) + ", last_visit_date = '" + Convert.ToDateTime(objArg.ParamList["visit_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE patient_id = '" + objArg.ParamList["patient_id"].ToString() + "'";
                flag = objDBHandler.ExecuteCommand(qry);
            }
            return flag;
        }

        public int insertVisitInfo(ComArugments objArg)
        {
            qry = "INSERT INTO pat_visit_info (visit_number, visit_date, patient_id, visit_type, visit_mode, medical_department_id, doctor_id, doctor_name, token_number, corporate_id, ward_id, room_id, bed_id, user_id, trans_dept_id, diagnosis, employee_id, referral_id) VALUES (" + Convert.ToInt64(objArg.ParamList["visit_number"]) + ",  '" + Convert.ToDateTime(objArg.ParamList["visit_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + objArg.ParamList["patient_id"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["visit_type"]) + ",  " + Convert.ToInt32(objArg.ParamList["visitMode"]) + ",  " + Convert.ToInt32(objArg.ParamList["medicalDepartmentId"]) + ", " + Convert.ToInt32(objArg.ParamList["doctorId"]) + ", '" + objArg.ParamList["doctor_name"].ToString() + "', '" + objArg.ParamList["token_number"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["corporateId"]) + ", " + Convert.ToInt32(objArg.ParamList["wardId"]) + ", " + Convert.ToInt32(objArg.ParamList["roomId"]) + ", " + Convert.ToInt32(objArg.ParamList["bedId"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["departmentId"]) + ", '" + objArg.ParamList["diagnosis"].ToString() + "','" + objArg.ParamList["employee_id"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["referral_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertPatBedInfo(ComArugments objArg)
        {
            qry = "INSERT INTO pat_bed_info (patient_id, visit_number, ward_id, room_id, bed_id, start_date, bed_status, trans_user_id, transaction_date) VALUES ( '" + objArg.ParamList["patient_id"].ToString() + "', " + Convert.ToInt64(objArg.ParamList["visit_number"]) + ", " + Convert.ToInt32(objArg.ParamList["wardId"]) + ",  " + Convert.ToInt32(objArg.ParamList["roomId"]) + ",  " + Convert.ToInt32(objArg.ParamList["bedId"]) + ", '" + Convert.ToDateTime(objArg.ParamList["start_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "', " + Convert.ToInt32(objArg.ParamList["bed_status"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", '" + Convert.ToDateTime(objArg.ParamList["transaction_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "')";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updatePatBed(ComArugments objArg)
        {
            qry = "UPDATE cis_bed SET patient_id='" + objArg.ParamList["patient_id"].ToString() + "', visit_number=" + Convert.ToInt64(objArg.ParamList["visit_number"]) + ", trans_user_id = " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + " where bed_id=" + Convert.ToInt32(objArg.ParamList["bedId"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertRegBillInfo(ComArugments objArg)
        {
            qry = "INSERT INTO reg_bill_info (bill_number, bill_date, patient_id, visit_number, bill_amount, discount, net_total, amount_paid, due, due_collection, trans_dept_id, trans_user_id, status) VALUES ('" + objArg.ParamList["reg_bill_number"].ToString() + "', '" + Convert.ToDateTime(objArg.ParamList["regBillDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + objArg.ParamList["patient_id"].ToString() + "',  " + Convert.ToInt64(objArg.ParamList["visit_number"]) + ", " + Convert.ToDecimal(objArg.ParamList["consultationFee"]) + ", " + Convert.ToDecimal(objArg.ParamList["discountAmount"]) + ", " + Convert.ToDecimal(objArg.ParamList["netTotal"]) + ", " + Convert.ToDecimal(objArg.ParamList["amountPaid"]) + ", " + Convert.ToDecimal(objArg.ParamList["balanceAmount"]) + ", " + Convert.ToDecimal(objArg.ParamList["dueCollection"]) + ", " + Convert.ToInt32(objArg.ParamList["departmentId"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["regPaymentStatus"]) + ")";
            flag = objDBHandler.ExecuteCommandTransaction(qry); // //Excute Insert Qry and Begin the Transaction
            return flag;
        }

        public int insertRegBillDetailInfo(ComArugments objArg)
        {
            string last_inserted_id = "select last_insert_id()";
            rowId = objDBHandler.ExecuteTransactionQuery(last_inserted_id); // Get Last Inserted Transaction Id
            int bill_id = Convert.ToInt32(rowId);
            objDBHandler.commitTransaction(TransactionType.Commit);
            qry = "INSERT INTO reg_bill_detail_info (bill_id, transaction_date, account_head_id, account_head_name, account_type, bill_amount, trans_dept_id, trans_user_id, status) VALUES (" + bill_id + ", '" + Convert.ToDateTime(objArg.ParamList["regBillDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "', " + Convert.ToInt32(objArg.ParamList["account_head_id"]) + ", '" + objArg.ParamList["account_head_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["account_type"]) + ", " + Convert.ToDecimal(objArg.ParamList["consultationFee"]) + ", " + Convert.ToInt32(objArg.ParamList["departmentId"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["regPaymentStatus"]) + " )";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "INSERT INTO reg_bill_detail_info (bill_id, transaction_date, account_head_id, account_head_name, account_type, bill_amount, discount, net_total, amount_paid, due, trans_dept_id, trans_user_id, payment_mode_id, card_number, bank_name, holder_name, status) VALUES (" + bill_id + ", '" + Convert.ToDateTime(objArg.ParamList["regBillDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "', 2, 'Payment', 2, " + Convert.ToDecimal(objArg.ParamList["consultationFee"]) + ", " + Convert.ToDecimal(objArg.ParamList["discountAmount"]) + ", " + Convert.ToDecimal(objArg.ParamList["netTotal"]) + ", " + Convert.ToDecimal(objArg.ParamList["amountPaid"]) + ", " + Convert.ToDecimal(objArg.ParamList["balanceAmount"]) + ", " + Convert.ToInt32(objArg.ParamList["departmentId"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["regPaymentStatus"]) + " )";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int modifyPatientInfo(ComArugments objArg)
        {
            qry = "UPDATE pat_reg_info SET social_title_id =" + Convert.ToInt32(objArg.ParamList["social_title_id"]) + ", social_title = '" + objArg.ParamList["social_title"].ToString() + "', patient_name = '" + objArg.ParamList["patient_name"].ToString() + "', patient_type_id = " + Convert.ToInt32(objArg.ParamList["patientTypeId"]) + ",   gender = " + Convert.ToInt32(objArg.ParamList["gender"]) + ",  dob = '" + Convert.ToDateTime(objArg.ParamList["dob"]).ToString("yyyy-MM-dd") + "',  age_year = " + Convert.ToInt32(objArg.ParamList["age_year"]) + ",  age_month =  " + Convert.ToInt32(objArg.ParamList["age_month"]) + ",  age_day = " + Convert.ToInt32(objArg.ParamList["age_day"]) + ",  guardian_name = '" + objArg.ParamList["guardian_name"].ToString() + "',  address = '" + objArg.ParamList["address"].ToString() + "', phone_no = '" + objArg.ParamList["phone_no"].ToString() + "' WHERE patient_id = '" + objArg.ParamList["patient_id"].ToString() + "'";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateRegRunningNumber(ComArugments objArg)
        {
            if (Convert.ToInt32(objArg.ParamList["visitMode"]) == 0)
            {
                qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["patient_id"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_patient_id"]) + " where number_format_id=1";
                flag = objDBHandler.ExecuteCommand(qry);
            }

            qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["visit_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_visit_number"]) + " where number_format_id=2";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["token_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_token_number"]) + " where number_format_id=3";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["reg_bill_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_reg_bill_number"]) + " where number_format_id=4";
            flag = objDBHandler.ExecuteCommand(qry);

            return flag;
        }

        public DataTable getPatientDetailsByPatientId(string patient_id)
        {
            qry = @"SELECT 
                        pat_reg_id,
                        pri.patient_id as patient_id,
                        pri.social_title_id,
                        patient_name,
                        patient_type_id,
                        (select 
                                t.patient_type
                            from
                                cis_patient_type t
                            where
                                t.patient_type_id = patient_type_id
                            limit 1) as patient_type,
                        gender,
                        if(pri.gender = 1, 'Female', 'Male') as gender_name,
                        dob,
                        age_year,
                        age_month,
                        age_day,
                        guardian_name,
                        (select 
                                c.corporate_name
                            from
                                cis_corporate c
                            where
                                c.corporate_id = pvi.corporate_id
                            limit 1) as corporate_name,
                        pvi.corporate_id, pvi.employee_id,
                        address,
                        phone_no,
                        last_visit_number,
                        pvi.doctor_id,pvi.doctor_name,
                        last_visit_type,
                        if(last_visit_type = 1, 'OP', 'IP') AS visit_type,
                        date_format(pvi.visit_date, '%d/%m/%Y %h:%i %p') as visit_date,
                        patient_death_date,
                        pri.status as status,
                        drf.validity as validity,
                        drf.validity_period as validity_period,
                        drf.revisit_reg_fee as revisit_reg_fee,
                        cb.patient_id as 'patAdmissionStatus',
                        (select 
                                DEPARTMENT_NAME
                            from
                                cis_department
                            where
                                department_id = cb.ward_id
                            limit 1) as ward_name,
                        (select 
                                room_no
                            from
                                cis_room
                            where
                                room_id = cb.room_id
                            limit 1) as room_no,
                        cb.bed_number,pvi.visit_date as visit_date1
                    FROM
                        pat_reg_info pri
                            left join
                        pat_visit_info pvi ON pri.last_visit_number = pvi.visit_number
                            left join
                        cis_define_reg_fee drf ON pvi.doctor_id = drf.doctor_id
                            left join
                        cis_bed cb ON pri.patient_id = cb.patient_id
                    where
                            pri.patient_id ='" + patient_id + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getRegFeeByDoctorId(int doctorId)
        {
            qry = @"SELECT 
                        new_reg_fee, revisit_reg_fee
                    FROM
                        cis_define_reg_fee
                    where
                        doctor_id=" + doctorId + " limit 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getChargesApplicableCorp(int corporateId)
        {
            qry = @"SELECT 
                        is_charges_applicable
                    FROM
                        cis_corporate
                    where
                        corporate_id =" + corporateId + " limit 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getRegFeeByDoctorIdAndPaientId(int doctorId, string patientId)
        {
            qry = @"SELECT 
                        revisit_reg_fee,
                        pri.last_visit_date,
                        drf.validity,
                        drf.validity_period
                    FROM
                        cis_define_reg_fee drf
                            join
                        pat_reg_info pri
                    where 
                        drf.doctor_id = " + doctorId + " and pri.patient_id = '" + patientId + "' limit 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getOPRegistrationInfo(string startDate, string endDate)
        {
            qry = @"SELECT 
                        rbi.bill_number as 'Bill No',
                        date_format(pvi.visit_date, '%d/%m/%Y %h:%i %p') as 'Visit Date',
                        pvi.patient_id as 'Patient Id',
                        convert(pvi.visit_number, CHAR(50)) as 'Visit Id',
                        pri.patient_name as 'Patient Name',
                        concat(if(pri.gender = 1, 'F', 'M'),
                                '/',
                                concat(pri.age_year,
                                        'Y ',
                                        pri.age_month,
                                        'M ',
                                        pri.age_day,
                                        'D')) as 'Gender/Age',
                        if(pvi.status = 1,
                            if(pvi.visit_mode = 0,
                                'New Registation',
                                'Revisit'),
                            'Cancelled') as 'Visit Mode',
                        rbi.bill_amount as 'Bill Amount',
                        (case
                            when rbi.status = 1 then 'Paid'
                            when rbi.status = 2 then 'Not Paid'
                            when rbi.status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        pat_visit_info pvi
                            JOIN
                        pat_reg_info pri ON pvi.patient_id = pri.patient_id
                            JOIN
                        reg_bill_info rbi ON pvi.visit_number = rbi.visit_number
                    where
                        pvi.trans_dept_id = 1 and pvi.visit_date between '" + Convert.ToDateTime(startDate).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss") + "' order by pvi.visit_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getIPRegistrationInfo(string startDate, string endDate)
        {
            qry = @"SELECT 
                        rbi.bill_number as 'Bill No',
                        date_format(pvi.visit_date, '%d/%m/%Y %h:%i %p') as 'Visit Date',
                        pvi.patient_id as 'Patient Id',
                        convert(pvi.visit_number, CHAR(50)) as 'Visit Id',
                        pri.patient_name as 'Patient Name',
                        concat(if(pri.gender = 1, 'F', 'M'),
                                '/',
                                concat(pri.age_year,
                                        'Y ',
                                        pri.age_month,
                                        'M ',
                                        pri.age_day,
                                        'D')) as 'Gender/Age',
                        if(pvi.status = 1,
                            if(pvi.visit_mode = 0,
                                'New Registation',
                                'Revisit'),
                            'Cancelled') as 'Visit Mode',
                        concat((select 
                                        DEPARTMENT_NAME
                                    from
                                        cis_department
                                    where
                                        department_id = pvi.ward_id
                                    limit 1),
                                ', ',
                                (select 
                                        room_no
                                    from
                                        cis_room
                                    where
                                        room_id = pvi.room_id
                                    limit 1),
                                ', ',
                                (select 
                                        bed_number
                                    from
                                        cis_bed
                                    where
                                        bed_id = pvi.bed_id
                                    limit 1)) as 'Ward Details',
                        rbi.bill_amount as 'Bill Amount',
                        (case
                            when rbi.status = 1 then 'Paid'
                            when rbi.status = 2 then 'Not Paid'
                            when rbi.status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        pat_visit_info pvi
                            JOIN
                        pat_reg_info pri ON pvi.patient_id = pri.patient_id
                            JOIN
                        reg_bill_info rbi ON pvi.visit_number = rbi.visit_number
                    where
                        pvi.trans_dept_id = 2 and pvi.visit_date between '" + Convert.ToDateTime(startDate).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss") + "' order by pvi.visit_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadDischargeType()
        {
            qry = @"SELECT 
                        cis_discharge_type_id, cis_discharge_type
                    FROM
                        cis_discharge_type
                    where
                        status = 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateDischargeInfo(ComArugments objArg)
        {
            if (objArg.ParamList["dischargeType"] == "Expired")
            {
                qry = "update pat_reg_info set patient_death_date='" + Convert.ToDateTime(objArg.ParamList["expiryDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' where patient_id='" + objArg.ParamList["patient_id"].ToString() + "'";
                flag = objDBHandler.ExecuteCommand(qry);
            }

            qry = "update pat_visit_info set discharge_type=" + Convert.ToInt32(objArg.ParamList["dichargeTypeId"]) + ", discharge_date='" + Convert.ToDateTime(objArg.ParamList["dischargeDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' where patient_id='" + objArg.ParamList["patient_id"].ToString() + "' and visit_number='" + objArg.ParamList["visit_number"].ToString() + "'";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "update pat_bed_info set end_date='" + Convert.ToDateTime(objArg.ParamList["dischargeDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "', bed_status=4, transaction_date=now() where patient_id='" + objArg.ParamList["patient_id"].ToString() + "' and visit_number='" + objArg.ParamList["visit_number"].ToString() + "' order by id_pat_bed_info desc limit 1";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "update cis_bed set patient_id=null, visit_number=null where patient_id='" + objArg.ParamList["patient_id"].ToString() + "' and visit_number='" + objArg.ParamList["visit_number"].ToString() + "'";
            flag = objDBHandler.ExecuteCommand(qry);

            return flag;
        }

        public int updateTransferInfo(ComArugments objArg)
        {
            qry = "update cis_bed set patient_id=null, visit_number=null where patient_id='" + objArg.ParamList["patient_id"].ToString() + "' and visit_number='" + objArg.ParamList["visit_number"].ToString() + "'";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "update pat_bed_info set end_date='" + Convert.ToDateTime(objArg.ParamList["transferDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "', transaction_date=now() where patient_id='" + objArg.ParamList["patient_id"].ToString() + "' and visit_number='" + objArg.ParamList["visit_number"].ToString() + "' order by id_pat_bed_info desc limit 1";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "UPDATE cis_bed SET patient_id='" + objArg.ParamList["patient_id"].ToString() + "', visit_number=" + Convert.ToInt64(objArg.ParamList["visit_number"]) + " where bed_id=" + Convert.ToInt32(objArg.ParamList["bedId"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "INSERT INTO pat_bed_info (patient_id, visit_number, ward_id, room_id, bed_id, start_date, bed_status, transaction_date) VALUES ( '" + objArg.ParamList["patient_id"].ToString() + "', '" + Convert.ToInt64(objArg.ParamList["visit_number"]) + "', " + Convert.ToInt32(objArg.ParamList["ward_id"]) + ",  " + Convert.ToInt32(objArg.ParamList["roomId"]) + ",  " + Convert.ToInt32(objArg.ParamList["bedId"]) + ", '" + Convert.ToDateTime(objArg.ParamList["transferDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "', 3, now())";
            flag = objDBHandler.ExecuteCommand(qry);

            return flag;
        }

        public DataTable getVisitDetailsByPatientId(string patient_id)
        {
            qry = @"SELECT 
                        pri.patient_id, pri.patient_name, CONVERT( pvi.visit_number,CHAR(50)) AS visit_number
                    FROM
                        pat_reg_info pri
                            join
                        pat_visit_info pvi ON pri.patient_id = pvi.patient_id
                    where
                        pri.patient_id = '" + patient_id + "' order by pvi.visit_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable searchPatientInfo(string query)
        {
            qry = @"SELECT patient_id as 'Patient Id',
                        patient_name as 'Patient Name',
                        if(gender = 0, 'Male', 'Female') as Gender,
                        IF(age_year > 0,
                            CONCAT(age_year, ' Y'),
                            IF(age_month > 0,
                                CONCAT(age_month, ' M'),
                                IF(age_day > 0,
                                    CONCAT(age_day, ' D'),
                                    CONCAT(0, ' D')))) AS Age,
                        guardian_name as 'Guardian Name',
                        Address,
                        phone_no as 'Phone No' FROM pat_reg_info WHERE  " + query;
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        
        public DataTable getVisitandBillDetailsByPatientId(string patient_id)//For Cancel Visit and Bill
        {
            qry = @"SELECT 
                        pri.patient_id,
                        pri.patient_name,
                        pvi.visit_number,
                        date_format(pvi.visit_date, '%d/%m/%Y %h:%i %p') as visit_date,
                        if(pvi.visit_type = 1, 'OP', 'IP') AS visit_type,
                        pvi.status as visit_status,
                        rbi.bill_number,
                        rbi.reg_bill_id,
                        date_format(rbi.bill_date, '%d/%m/%Y %h:%i %p') as bill_date,
                        bill_amount,
                        discount,
                        (amount_paid + due_collection) as amount_paid,
                        due,
                        rbi.status as bill_status,
                        concat((select 
                                        DEPARTMENT_NAME
                                    from
                                        cis_department
                                    where
                                        department_id = cb.ward_id
                                    limit 1),
                                ', ',
                                (select 
                                        room_no
                                    from
                                        cis_room
                                    where
                                        room_id = cb.room_id
                                    limit 1),
                                ', ',
                                (cb.bed_number)) as ward_details
                    from
                        pat_reg_info pri
                            left join
                        pat_visit_info pvi ON pri.last_visit_number = pvi.visit_number
                            left JOIN
                        reg_bill_info rbi ON pvi.visit_number = rbi.visit_number
                            left join
                        cis_bed cb ON pvi.visit_number = cb.visit_number
                    where
                        pri.patient_id = '" + patient_id + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }


        public int cancelVisitInfo(ComArugments objArg)
        {
            qry = "update pat_visit_info set status=0 where patient_id='" + objArg.ParamList["patient_id"].ToString() + "' and visit_number='" + objArg.ParamList["visit_number"].ToString() + "'";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "update pat_bed_info set end_date=now(), bed_status=0, transaction_date=now() where patient_id='" + objArg.ParamList["patient_id"].ToString() + "' and visit_number='" + objArg.ParamList["visit_number"].ToString() + "' order by id_pat_bed_info desc limit 1";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "update cis_bed set patient_id=null, visit_number=null where patient_id='" + objArg.ParamList["patient_id"].ToString() + "' and visit_number='" + objArg.ParamList["visit_number"].ToString() + "'";
            flag = objDBHandler.ExecuteCommand(qry);

            return flag;
        }

        public int cancelRegBillInfo(ComArugments objArg)
        {
            qry = "update reg_bill_info set refund_to_patient=" + Convert.ToInt32(objArg.ParamList["refund_to_patient"]) + ", status=4 where bill_number='" + objArg.ParamList["bill_number"].ToString() + "'";
            flag = objDBHandler.ExecuteCommand(qry);

            qry = "INSERT INTO reg_bill_detail_info (bill_id, transaction_date, account_head_id, account_head_name, account_type, refund_to_patient, status) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", now(), 3, 'Cancelled', 2, " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", 4)";
            flag = objDBHandler.ExecuteCommand(qry);

            return flag;
        }

        #endregion

        #region Investigation details
        public DataTable loadInvestigationDetails()
        {
            qry = @"SELECT 
                        investigation_id, investigation_code
                    FROM
                        cis_investigation_list
                    where
                        status = 1
                    order by investigation_code ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getInvestigationById(int investigationId)
        {
            qry = @"SELECT 
                        investigation_code,
                        investigation_name,
                        l.department_id,
                        d.department_name,
                        unit_price,
                        share_type,
                        share_amt
                    FROM
                        cis_investigation_list l
                            left join
                        cis_department d ON l.department_id = d.DEPARTMENT_ID
                    where
                        l.investigation_id =" + investigationId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }


        public DataTable getInvestigationInfo(string startDate, string endDate)
        {
            qry = @"SELECT 
                        bill_number as 'Bill No',
                        date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',
                        patient_id as 'Patient Id',
                        convert(visit_number, CHAR(50)) as 'Visit Id',
                        patient_name as 'Patient Name',
                        bill_amount as 'Bill Amount',
                        Discount,
                        (amount_paid + pay_from_advance + due_collection) as 'Amount Paid',
                        Due,
                        (case
                            when status = 1 then 'Paid'
                            when status = 2 then 'Not Paid'
                            when status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        inv_bill_info
                    where
                        bill_date between '" + Convert.ToDateTime(startDate).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss") + "' order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getWardBillInfo(string startDate, string endDate)
        {
            qry = @"SELECT 
                        bill_id,
                        bill_number as 'Bill No',
                        if(bill_type = 1,
                            'Final Bill',
                            'Intermediate Bill') as 'Bill Type',
                        bill_date as 'Bill Date',
                        patient_id as 'Patient Id',
                        visit_number as 'Visit Number',
                        bill_amount as 'Bill Amt',
                        discount as 'Concession',
                        amount_paid as 'Amt Paid',
                        pay_from_advance as 'Adv Adj',
                        Due,
                        (case
                            when status = 1 then 'Paid'
                            when status = 2 then 'Not Paid'
                            when status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        ward_bill_info
                    where
                        bill_date between '" + startDate + "' and '" + endDate + "' order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion
        
        #region Pharmacy details
        public DataTable loadPhaItem()
        {
            qry = @"SELECT 
                      item_id, item_name
                    FROM
                        cis_pha_item
                    where
                        status = 1
                    order by item_name ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getPhaByItemId(int phaItemId)
        {
            qry = @"SELECT 
                        pis.inventory_stock_id,
                        pis.department_id,
                        pit.item_type_id,
                        item_type,
                        pis.item_id,
                        pi.item_name,
                        pis.lot_id,
                        pis.exp_date,
                        pis.avail_qty,
                        pis.mrp,
                        pis.vendor_price,
                        pis.default_discount,
                        pis.tax_perc as sales_tax_perc
                    FROM
                        pha_inventory_stock pis
                            LEFT JOIN
                        cis_pha_item pi ON pis.item_id = pi.item_id
                            LEFT JOIN
                        cis_pha_item_type pit ON pi.item_type_id = pit.item_type_id
                    WHERE
                        pis.item_id = " + phaItemId + " and pis.avail_qty > 0 and date_format(exp_date, '%Y-%m-%d') >= date_format(now(), '%Y-%m-%d') order by exp_date ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getPhaDepartmentId()
        {
            qry = @"SELECT 
                        department_id
                    FROM
                        cis_department
                    where
                        department_category_id = 4
                            and status = 1 limit 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getPharmacyInfo(string startDate, string endDate)
        {
            qry = @"SELECT 
                        bill_number as 'Bill No',
                        date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',
                        patient_id as 'Patient Id',
                        convert(visit_number, CHAR(50)) as 'Visit Id',
                        patient_name as 'Patient Name',
                        bill_amount as 'Bill Amount',
                        (Discount + total_free_care) as Discount,
                        (amount_paid + pay_from_advance + due_collection) as 'Amount Paid',
                        Due,
                        (case
                            when status = 1 then 'Paid'
                            when status = 2 then 'Not Paid'
                            when status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        pha_bill_info
                    where
                        bill_date between '" + Convert.ToDateTime(startDate).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss") + "' order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region CIS Pharmacy Item Type Master details
        public DataTable getItemTypeDetails()
        {
            qry = @"SELECT 
                        item_type_id,
                        item_type AS 'Item Type',
                        IF(status = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_pha_item_type
                    ORDER BY item_type ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertItemType(ComArugments objArg)
        {
            qry = "INSERT INTO cis_pha_item_type(item_type, status) VALUES ('" + objArg.ParamList["item_type"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteItemType(int cisItemTypeId)
        {
            qry = "delete from cis_pha_item_type where item_type_id=" + cisItemTypeId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getItemTypeRecord(int cisItemTypeId)
        {
            qry = @"SELECT 
                        item_type_id,
                        item_type,
                        status
                    FROM
                        cis_pha_item_type
                    WHERE
                        item_type_id =" + cisItemTypeId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateItemType(ComArugments objArg)
        {
            qry = "UPDATE cis_pha_item_type SET item_type='" + objArg.ParamList["item_type"].ToString() + "', status=" + Convert.ToInt32(objArg.ParamList["status"]) + "  WHERE item_type_id=" + Convert.ToInt32(objArg.ParamList["item_type_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable loadItemType()
        {
            qry = @"SELECT 
                        item_type_id, item_type
                    FROM
                        cis_pha_item_type
                    WHERE
                        status = 1 ORDER BY item_type ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region CIS Pharmacy Item Master details
        public DataTable getItemDetails()
        {
            qry = @"SELECT 
                        pi.item_id,
                        pi.item_name as 'Item Name',
                        pit.Item_type as 'Item Type',
                        convert(tx.tax_rate, CHAR(50)) as 'Tax Rate',
                        pi.pack_y as 'Pack Y',
                        pi.hsn_code as 'HSN Code',
                        IF(pi.status = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_pha_item pi
                            LEFT JOIN
                        cis_pha_item_type pit ON pi.item_type_id = pit.item_type_id
                            LEFT JOIN
                        cis_pha_tax tx ON pi.tax_id = tx.tax_id
                    ORDER BY item_name ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertItem(ComArugments objArg)
        {
            qry = "INSERT INTO cis_pha_item(item_name, item_type_id, tax_id, pack_y, hsn_code, status) VALUES ('" + objArg.ParamList["item_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["item_type_id"]) + ", " + Convert.ToInt32(objArg.ParamList["tax_id"]) + ", " + Convert.ToInt32(objArg.ParamList["pack_y"]) + ", '" + objArg.ParamList["hsn_code"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteItem(int cisItemId)
        {
            qry = "delete from cis_pha_item where item_id=" + cisItemId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getItemRecord(int ItemId)
        {
            qry = @"SELECT 
                        item_id,
                        item_name,
                        Item_type_id,
                        tax_id,
                        pack_y,
                        hsn_code,
                        status
                    FROM
                        cis_pha_item
                    WHERE
                        item_id =" + ItemId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        
        public int updateItem(ComArugments objArg)
        {
            qry = "UPDATE cis_pha_item SET item_name='" + objArg.ParamList["item_name"].ToString() + "', item_type_id=" + Convert.ToInt32(objArg.ParamList["item_type_id"]) + ", tax_id=" + Convert.ToInt32(objArg.ParamList["tax_id"]) + ", pack_y=" + Convert.ToInt32(objArg.ParamList["pack_y"]) + ", hsn_code = '" + objArg.ParamList["hsn_code"].ToString() + "', status=" + Convert.ToInt32(objArg.ParamList["status"]) + "  WHERE item_id=" + Convert.ToInt32(objArg.ParamList["item_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        #endregion

        #region CIS Pharmacy Vendor Master details
        public DataTable getVendorDetails()
        {
            qry = @"SELECT 
                        vendor_id,
                        vendor_name AS 'Vendor Name',
                        tin_number AS 'TIN Number',
                        contact_info AS 'Contact Info',
                        contact_address AS 'Contact Address',
                        IF(status = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_pha_vender
                    ORDER BY vendor_name ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertVendor(ComArugments objArg)
        {
            qry = "INSERT INTO cis_pha_vender(vendor_name, tin_number, contact_info, contact_address, status) VALUES ('" + objArg.ParamList["vendor_name"].ToString() + "', '" + objArg.ParamList["tin_number"].ToString() + "', '" + objArg.ParamList["contact_info"].ToString() + "', '" + objArg.ParamList["contact_address"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertReferral(ComArugments objArg)
        {
            qry = "INSERT INTO cis_referral(referral_name, contact_no, contact_address, status) VALUES ('" + objArg.ParamList["referral_name"].ToString() + "', '" + objArg.ParamList["contact_no"].ToString() + "', '" + objArg.ParamList["contact_address"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteVendor(int cisVendorId)
        {
            qry = "delete from cis_pha_vender where vendor_id=" + cisVendorId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getVendorRecord(int cisVendorId)
        {
            qry = @"SELECT 
                        vendor_id,
                        vendor_name,
                        tin_number, 
                        contact_info, 
                        contact_address,
                        status
                    FROM
                        cis_pha_vender
                    WHERE
                        vendor_id =" + cisVendorId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getReferralRecord(int cisReferralId)
        {
            qry = @"SELECT 
                        cis_referral_id,
                        referral_name,
                        contact_no,
                        contact_address,
                        status
                    FROM
                        cis_referral
                    where
                        cis_referral_id = " + cisReferralId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateVendor(ComArugments objArg)
        {
            qry = "UPDATE cis_pha_vender SET vendor_name='" + objArg.ParamList["vendor_name"].ToString() + "', tin_number='" + objArg.ParamList["tin_number"].ToString() + "', contact_info='" + objArg.ParamList["contact_info"].ToString() + "', contact_address='" + objArg.ParamList["contact_address"].ToString() + "', status=" + Convert.ToInt32(objArg.ParamList["status"]) + "  WHERE vendor_id=" + Convert.ToInt32(objArg.ParamList["vendor_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateReferral(ComArugments objArg)
        {
            qry = "UPDATE cis_referral SET referral_name='" + objArg.ParamList["referral_name"].ToString() + "', contact_no='" + objArg.ParamList["contact_no"].ToString() + "', contact_address='" + objArg.ParamList["contact_address"].ToString() + "', status=" + Convert.ToInt32(objArg.ParamList["status"]) + "  WHERE cis_referral_id=" + Convert.ToInt32(objArg.ParamList["cis_referral_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable loadVendor()
        {
            qry = @"SELECT 
                        vendor_id, vendor_name
                    FROM
                        cis_pha_vender
                    where
                        status = 1
                    order by vendor_name asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getVendorTinNo(int vendorId)
        {
            qry = @"SELECT 
                        tin_number
                    FROM
                        cis_pha_vender
                    where
                        vendor_id = " + vendorId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region CIS Pharmacy Tax Master details
        public DataTable getTaxDetails()
        {
            qry = @"SELECT 
                        tax_id,
                        convert(tax_rate, CHAR(50)) as 'Tax Rate',
                        IF(status = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_pha_tax";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertTax(ComArugments objArg)
        {
            qry = "INSERT INTO cis_pha_tax(tax_rate, status) VALUES (" + objArg.ParamList["tax_rate"].ToString() + ", " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteTax(int cisTaxId)
        {
            qry = "delete from cis_pha_tax where tax_id=" + cisTaxId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getTaxRecord(int cisTaxId)
        {
            qry = @"SELECT 
                        tax_id,
                        tax_rate,
                        status
                    FROM
                        cis_pha_tax
                    WHERE
                        tax_id =" + cisTaxId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateTax(ComArugments objArg)
        {
            qry = "UPDATE cis_pha_tax SET tax_rate=" + objArg.ParamList["tax_rate"].ToString() + ", status=" + Convert.ToInt32(objArg.ParamList["status"]) + "  WHERE tax_id=" + Convert.ToInt32(objArg.ParamList["tax_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable loadTaxPerc()
        {
            qry = @"SELECT 
                        tax_id, tax_rate
                    FROM
                        cis_pha_tax
                    WHERE
                        status = 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region CIS Investigation Category Master details
        public DataTable getInvestigationCategoryDetails()
        {
            qry = @"SELECT 
                        inv_category_id,
                        inv_category AS 'Investigation Category',
                        IF(status = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_investigation_category
                    ORDER BY inv_category ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertInvCategory(ComArugments objArg)
        {
            qry = "INSERT INTO cis_investigation_category(inv_category, status) VALUES ('" + objArg.ParamList["inv_category"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteInvCategory(int cisInvCategoryId)
        {
            qry = "delete from cis_investigation_category where inv_category_id=" + cisInvCategoryId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getInvCategoryRecord(int cisInvCategoryId)
        {
            qry = @"SELECT 
                        inv_category_id,
                        inv_category,
                        status
                    FROM
                        cis_investigation_category
                    WHERE
                        inv_category_id =" + cisInvCategoryId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateInvCategory(ComArugments objArg)
        {
            qry = "UPDATE cis_investigation_category SET inv_category='" + objArg.ParamList["inv_category"].ToString() + "', status=" + Convert.ToInt32(objArg.ParamList["status"]) + "  WHERE inv_category_id=" + Convert.ToInt32(objArg.ParamList["inv_category_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable loadInvCategory()
        {
            qry = @"SELECT 
                        inv_category_id, inv_category
                    FROM
                        cis_investigation_category
                    WHERE
                        status = 1 ORDER BY inv_category ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region CIS Investigation List Master details
        public DataTable getInvestigationDetails()
        {
            qry = @"SELECT 
                        il.investigation_id,
                        il.investigation_code AS 'Investigation Code',
                        il.investigation_name AS 'Investigation Name',
                        ic.inv_category AS 'Investigation Category',
                        d.department_name AS 'Department',
                        il.unit_price AS 'Unit Price',
                        il.share_amt as 'Share Amt',
                        IF(il.status = 1, 'Active', 'Inactive') AS Status
                    FROM
                        cis_investigation_list il
                            LEFT JOIN
                        cis_investigation_category ic ON il.investigation_category_id = ic.inv_category_id
                            LEFT JOIN
                        cis_department d ON il.department_id = d.DEPARTMENT_ID
                    ORDER BY investigation_name ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertInvestigation(ComArugments objArg)
        {
            qry = "INSERT INTO cis_investigation_list(investigation_code, investigation_name, investigation_category_id, department_id, unit_price, status, share_type, amt_type, share_per, share_amt) VALUES ('" + objArg.ParamList["investigation_code"].ToString() + "', '" + objArg.ParamList["investigation_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["investigation_category_id"]) + ", " + Convert.ToInt32(objArg.ParamList["department_id"]) + ", " + Convert.ToDecimal(objArg.ParamList["unit_price"]) + ", " + Convert.ToInt32(objArg.ParamList["status"]) + ", " + Convert.ToInt32(objArg.ParamList["share_type"]) + ", " + Convert.ToInt32(objArg.ParamList["amt_type"]) + ", " + Convert.ToDecimal(objArg.ParamList["share_per"]) + ", " + Convert.ToDecimal(objArg.ParamList["share_amt"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int deleteInvestigation(int cisInvestigationId)
        {
            qry = "delete from cis_investigation_list where investigation_id=" + cisInvestigationId + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getInvestigationRecord(int InvestigationId)
        {
            qry = @"SELECT 
                        investigation_id,
                        investigation_code,
                        investigation_name,
                        investigation_category_id,
                        department_id,
                        unit_price,
                        status,
                        share_type,
                        amt_type,
                        share_per,
                        share_amt
                    FROM
                        cis_investigation_list
                    WHERE
                        investigation_id =" + InvestigationId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int updateInvestigation(ComArugments objArg)
        {
            qry = "UPDATE cis_investigation_list SET investigation_code='" + objArg.ParamList["investigation_code"].ToString() + "', investigation_name='" + objArg.ParamList["investigation_name"].ToString() + "', investigation_category_id=" + Convert.ToInt32(objArg.ParamList["investigation_category_id"]) + ", department_id=" + Convert.ToInt32(objArg.ParamList["department_id"]) + ", unit_price=" + Convert.ToDecimal(objArg.ParamList["unit_price"]) + ", status=" + Convert.ToInt32(objArg.ParamList["status"]) + ", share_type=" + Convert.ToInt32(objArg.ParamList["share_type"]) + ", amt_type=" + Convert.ToInt32(objArg.ParamList["amt_type"]) + ", share_per=" + Convert.ToDecimal(objArg.ParamList["share_per"]) + ", share_amt=" + Convert.ToDecimal(objArg.ParamList["share_amt"]) + "  WHERE investigation_id=" + Convert.ToInt32(objArg.ParamList["investigation_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable loadInvestigationDepartment()
        {
            qry = @"SELECT 
                        DEPARTMENT_ID, DEPARTMENT_NAME
                    FROM
                        cis_department
                    where
                        DEPARTMENT_CATEGORY_ID = 3
                            AND DEPARTMENT_TYPE = 2
                            and status = 1
                    ORDER BY DEPARTMENT_NAME ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadShareType()
        {
            qry = @"SELECT 
                        master_id, master_value
                    from
                        cis_master
                    where
                        group_id = 3 and status = 1
                    order by master_value asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region CIS Pharmacy Inventory Management
        public DataTable getPhaTypeAndAvailQty(int ItemId)
        {
            qry = @"SELECT 
                        pit.item_type,
                        pi.item_type_id,
                        sum(pis.avail_qty) AS total_avail_qty,
                        tx.tax_rate
                    FROM
                        cis_pha_item pi
                            LEFT JOIN
                        cis_pha_item_type pit ON pi.item_type_id = pit.item_type_id
                            LEFT JOIN
                        pha_inventory_stock pis ON pi.item_id = pis.item_id
                            LEFT JOIN
                        cis_pha_tax tx ON pi.item_id = tx.tax_id
                    where
                        pi.item_id =" + ItemId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getPhaViewStockByItemId(int ItemId)
        {
            qry = @"SELECT 
                        pis.inventory_stock_id,
                        pit.item_type,
                        pi.item_type_id,
                        pi.item_name,
                        pi.item_id,
                        pis.lot_id,
                        pis.exp_date,
                        date_format(pis.exp_date, '%m/%Y') as 'expiry_date',
                        pis.vendor_price,
                        pis.mrp,
                        pis.default_discount,
                        pis.tax_perc,
                        pis.avail_qty
                    FROM
                        cis_pha_item pi
                            JOIN
                        cis_pha_item_type pit ON pi.item_type_id = pit.item_type_id
                            JOIN
                        pha_inventory_stock pis ON pi.item_id = pis.item_id
                    where
                        pi.item_id = " + ItemId + " and pis.avail_qty > 0";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertOpeningStock(ComArugments objArg)
        {
            qry = "INSERT INTO pha_inventory_stock (department_id, item_type_id, item_id, lot_id, exp_date, avail_qty, mrp, tax_perc) VALUES(" + Convert.ToInt32(objArg.ParamList["department_id"]) + ", " + Convert.ToInt32(objArg.ParamList["item_type_id"]) + ", " + Convert.ToInt32(objArg.ParamList["item_id"]) + ", '" + objArg.ParamList["lot_id"].ToString() + "', '" + Convert.ToDateTime(objArg.ParamList["exp_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "', " + Convert.ToInt32(objArg.ParamList["avail_qty"]) + ", " + Convert.ToDecimal(objArg.ParamList["mrp"]) + ", " + Convert.ToDecimal(objArg.ParamList["tax_perc"]) + ")";
            flag = objDBHandler.ExecuteCommandTransaction(qry); // //Excute Insert Qry and Begin the Transaction
            return flag;
        }

        public int insertOpeningStockdDetails(ComArugments objArg)
        {
            qry = "INSERT INTO pha_inventory_stock_details (inventory_stock_id, inventory_transaction_type, department_id, item_type_id, item_id, lot_id, exp_date, transaction_qty, avail_qty, mrp, transaction_date, tax_perc, transaction_user_id) VALUES(" + Convert.ToInt32(objArg.ParamList["inv_trans_id"]) + ", 1, " + Convert.ToInt32(objArg.ParamList["department_id"]) + ", " + Convert.ToInt32(objArg.ParamList["item_type_id"]) + ", " + Convert.ToInt32(objArg.ParamList["item_id"]) + ", '" + objArg.ParamList["lot_id"].ToString() + "', '" + Convert.ToDateTime(objArg.ParamList["exp_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "', " + Convert.ToInt32(objArg.ParamList["avail_qty"]) + ", " + Convert.ToInt32(objArg.ParamList["avail_qty"]) + ", " + Convert.ToDecimal(objArg.ParamList["mrp"]) + ", now(), " + Convert.ToDecimal(objArg.ParamList["tax_perc"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateStockAdjustment(ComArugments objArg)
        {
            qry = "UPDATE pha_inventory_stock SET avail_qty=" + Convert.ToInt32(objArg.ParamList["physical_qty"]) + ", mrp = " + Convert.ToDecimal(objArg.ParamList["mrp"]) + " where inventory_stock_id=" + Convert.ToInt32(objArg.ParamList["inv_trans_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertStockAdjustmentdDetails(ComArugments objArg)
        {
            qry = "INSERT INTO pha_inventory_stock_details (inventory_stock_id, inventory_transaction_type, department_id, item_type_id, item_id, lot_id, exp_date, transaction_qty, avail_qty, mrp, transaction_date, tax_perc, transaction_user_id) VALUES(" + Convert.ToInt32(objArg.ParamList["inv_trans_id"]) + ", 3, " + Convert.ToInt32(objArg.ParamList["department_id"]) + ", " + Convert.ToInt32(objArg.ParamList["item_type_id"]) + ", " + Convert.ToInt32(objArg.ParamList["item_id"]) + ", '" + objArg.ParamList["lot_id"].ToString() + "', '" + Convert.ToDateTime(objArg.ParamList["exp_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "', " + Convert.ToInt32(objArg.ParamList["trans_qty"]) + ", " + Convert.ToInt32(objArg.ParamList["physical_qty"]) + ", " + Convert.ToDecimal(objArg.ParamList["mrp"]) + ", now(), " + Convert.ToDecimal(objArg.ParamList["tax_perc"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        #endregion

        #region Inventory Movements
        public DataTable loadInternalMoveDepartment()
        {
            qry = @"SELECT 
                        department_id, DEPARTMENT_NAME
                    FROM
                        cis_department
                    where
                        DEPARTMENT_TYPE = 2
                            and DEPARTMENT_CATEGORY_ID not in (1 , 2, 5)
                            and status = 1
                    order by DEPARTMENT_NAME";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable consumeType()
        {
            qry = @"SELECT 
                       master_id as consume_type_id, master_value as consume_type
                    FROM
                        cis_master
                    where
                        group_id = 2 and status = 1
                    order by master_value";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getPhaByItemIdAll(int phaItemId)
        {
            qry = @"SELECT 
                        pis.inventory_stock_id,
                        pis.department_id,
                        pit.item_type_id,
                        item_type,
                        pis.item_id,
                        pi.item_name,
                        pis.lot_id,
                        pis.exp_date,
                        pis.avail_qty,
                        pis.mrp,
                        pis.vendor_price,
                        pis.default_discount,
                        pis.tax_perc as sales_tax_perc
                    FROM
                        pha_inventory_stock pis
                            LEFT JOIN
                        cis_pha_item pi ON pis.item_id = pi.item_id
                            LEFT JOIN
                        cis_pha_item_type pit ON pi.item_type_id = pit.item_type_id
                    WHERE
                        pis.item_id = " + phaItemId + " and pis.avail_qty > 0 order by exp_date ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int insertInventoryMovements(ComArugments objArg)
        {
            qry = "INSERT INTO pha_internal_movements (movement_type, im_number, department_id, transaction_date, entry_date, consume_type_id, vendor_id, return_no, total_amount) VALUES (" + Convert.ToInt32(objArg.ParamList["movement_type"]) + ", '" + objArg.ParamList["inventory_movements_number"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["department_id"]) + ", '" + Convert.ToDateTime(objArg.ParamList["transaction_date"]).ToString("yyyy-MM-dd") + "', now(), " + Convert.ToInt32(objArg.ParamList["consume_type_id"]) + ", " + Convert.ToInt32(objArg.ParamList["vendor_id"]) + ", '" + objArg.ParamList["return_no"].ToString() + "', " + Convert.ToDecimal(objArg.ParamList["total_amount"]) + ")";
            flag = objDBHandler.ExecuteCommandTransaction(qry); // //Excute Insert Qry and Begin the Transaction
            return flag;
        }

        public int insertInventoryMovementsDetails(ComArugments objArg)
        {
            qry = "INSERT INTO pha_internal_movements_details (pha_in_mo_id, transaction_date, item_type_id, item_id, trans_qty, lot_id, expiry_date, vendor_price, total_amount, tax_perc, tax_amt, net_total_amount, inventory_stock_id) VALUES (" + Convert.ToInt32(objArg.ParamList["pha_bill_id"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["pha_item_type_id"]) + ",   " + Convert.ToInt32(objArg.ParamList["pha_item_id"]) + ", " + Convert.ToInt32(objArg.ParamList["pha_qty"]) + ",   '" + objArg.ParamList["pha_lot_id"].ToString() + "',  '" + objArg.ParamList["pha_expiry_date"].ToString() + "', " + Convert.ToDecimal(objArg.ParamList["VendorPricePha"]) + ",   " + Convert.ToDecimal(objArg.ParamList["pha_total_amt"]) + ",  " + Convert.ToDecimal(objArg.ParamList["tax_perc"]) + ", " + Convert.ToDecimal(objArg.ParamList["taxAmt"]) + ", " + Convert.ToDecimal(objArg.ParamList["net_total_amount"]) + ", " + Convert.ToInt32(objArg.ParamList["inventory_stock_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateInventoryMovementsRunningNumber(ComArugments objArg)
        {
            qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["inventory_movements_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_inventory_movements_number"]) + " where number_format_id=12";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable loadInventoryMovementsDetails(string startDate, string endDate, int inventoryMoveTypeId)
        {
            if (inventoryMoveTypeId == 0)
            {
                qry = @"SELECT 
                        (case
                            when pim.movement_type = 0 then 'Internal Movements'
                            when pim.movement_type = 1 then 'Return Vendor'
                            when pim.movement_type = 2 then 'Consume Items'
                        end) as 'Movement Type',
                        pim.im_number as 'IM Number',
                        pim.return_no as 'Return No',
                        (select 
                                department_name
                            from
                                cis_department d
                            where
                                d.department_id = pim.department_id
                            limit 1) as 'Department Name',
                        date_format(pim.entry_date, '%d/%m/%Y %h:%i %p') as 'Date',
                        (select 
                                m.master_value
                            from
                                cis_master m
                            where
                                m.master_id = pim.consume_type_id
                                    and group_id = 2
                            limit 1) as 'Consume Type',
                        (select 
                                v.vendor_name
                            from
                                cis_pha_vender v
                            where
                                v.vendor_id = pim.vendor_id
                            limit 1) as 'Vendor Name',
                        pim.total_amount as 'Total Amt'
                    FROM
                        pha_internal_movements pim
                    where
                        pim.entry_date between '" + startDate + "' and '" + endDate + "' order by pim.entry_date desc";
            }
            else
            {
                qry = @"SELECT 
                        (case
                            when pim.movement_type = 0 then 'Internal Movements'
                            when pim.movement_type = 1 then 'Return Vendor'
                            when pim.movement_type = 2 then 'Consume Items'
                        end) as 'Movement Type',
                        pim.im_number as 'IM Number',
                        pim.return_no as 'Return No',
                        (select 
                                department_name
                            from
                                cis_department d
                            where
                                d.department_id = pim.department_id
                            limit 1) as 'Department Name',
                        date_format(pim.entry_date, '%d/%m/%Y %h:%i %p') as 'Date',
                        (select 
                                m.master_value
                            from
                                cis_master m
                            where
                                m.master_id = pim.consume_type_id
                                    and group_id = 2
                            limit 1) as 'Consume Type',
                        (select 
                                v.vendor_name
                            from
                                cis_pha_vender v
                            where
                                v.vendor_id = pim.vendor_id
                            limit 1) as 'Vendor Name',
                        pim.total_amount as 'Total Amt'
                    FROM
                        pha_internal_movements pim
                    where
                        pim.entry_date between '" + startDate + "' and '" + endDate + "' and pim.movement_type = " + inventoryMoveTypeId  + " - 1 order by pim.entry_date desc";
            }
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region Save Investigation Bill
        public int insertInvestigationBill(ComArugments objArg)
        {
            qry = "INSERT INTO inv_bill_info (bill_number, bill_date, patient_id, visit_number, bill_amount, discount, total_amount, amount_paid, due, status, trans_user_id, transaction_date, patient_name, doctor_id, doctor_name, gender, age_year, age_month, age_day, address) VALUES ( '" + objArg.ParamList["investigation_bill_number"].ToString() + "', now(), '" + objArg.ParamList["invoice_patient_id"].ToString() + "', '" + objArg.ParamList["invoice_visit_number"].ToString() + "', " + Convert.ToDecimal(objArg.ParamList["inv_bill_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_due"]) + ", " + Convert.ToInt32(objArg.ParamList["inv_status"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", now(), '" + objArg.ParamList["invoice_patient_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["invoice_doctor_id"]) + ", '" + objArg.ParamList["invoice_doctor_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["invoice_gender"]) + ", " + Convert.ToInt32(objArg.ParamList["invoice_age_year"]) + ", " + Convert.ToInt32(objArg.ParamList["invoice_age_month"]) + ", " + Convert.ToInt32(objArg.ParamList["invoice_age_day"]) + ", '" + objArg.ParamList["invoice_address"].ToString() + "')";
            flag = objDBHandler.ExecuteCommandTransaction(qry); // //Excute Insert Qry and Begin the Transaction
            return flag;
        }

        public int insertInvestigationBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO inv_bill_datail_info (bill_id, investigation_id, department_id, qty, unit_price, amount, account_type, transaction_date, transaction_user_id, share_type, share_amt) VALUES (" + Convert.ToInt32(objArg.ParamList["inv_bill_id"]) + ", " + Convert.ToInt32(objArg.ParamList["investigation_id"]) + ", " + Convert.ToInt32(objArg.ParamList["inv_department_id"]) + ", " + Convert.ToInt32(objArg.ParamList["inv_qty"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_unit_price"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_amount"]) + ", 1, now(), " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["inv_share_type"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_share_amt"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertInvestigationBillDetailsSummary(ComArugments objArg)
        {
            qry = "INSERT INTO inv_bill_datail_info (bill_id, investigation_id, department_id, qty, unit_price, amount, discount, account_type, total, amount_paid, due, payment_mode_id, card_number, bank_name, holder_name, transaction_date, transaction_user_id, status) VALUES (" + Convert.ToInt32(objArg.ParamList["inv_bill_id"]) + ", 0, 0, 0, 0, " + Convert.ToDecimal(objArg.ParamList["inv_bill_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_discount"]) + ", 2, " + Convert.ToDecimal(objArg.ParamList["inv_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["inv_due"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "',  now(), " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["inv_status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateInvestigationRunningNumber(ComArugments objArg)
        {
            qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["investigation_bill_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_investigation_bill_number"]) + " where number_format_id=5";
            flag = objDBHandler.ExecuteCommand(qry);

            return flag;
        }
        #endregion

        #region Save Pharmacy Bill
        public int insertPharmacyBill(ComArugments objArg)
        {
            qry = "INSERT INTO pha_bill_info(bill_number, bill_date, patient_id, visit_number, bill_amount, discount, total_free_care, total_amount, amount_paid, due, trans_user_id, patient_name, gender, age_year, age_month, age_day, address, doctor_id, doctor_name, status) VALUES ('" + objArg.ParamList["pharmacy_bill_number"].ToString() + "', now(), '" + objArg.ParamList["invoice_patient_id"].ToString() + "',  '" + objArg.ParamList["invoice_visit_number"].ToString() + "',  " + Convert.ToDecimal(objArg.ParamList["pha_bill_amount"]) + ",  " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_total_free_care"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_total_amount"]) + ",  " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + ",  " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", '" + objArg.ParamList["invoice_patient_name"].ToString() + "',  " + Convert.ToInt32(objArg.ParamList["invoice_gender"]) + ",  " + Convert.ToInt32(objArg.ParamList["invoice_age_year"]) + ",  " + Convert.ToInt32(objArg.ParamList["invoice_age_month"]) + ",  " + Convert.ToInt32(objArg.ParamList["invoice_age_day"]) + ",  '" + objArg.ParamList["invoice_address"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["invoice_doctor_id"]) + ", '" + objArg.ParamList["invoice_doctor_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["pha_status"]) + ")";
            flag = objDBHandler.ExecuteCommandTransaction(qry); // //Excute Insert Qry and Begin the Transaction
            return flag;
        }

        public int insertPharmacyBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO pha_bill_detail_info (bill_id, transaction_date, trans_type, item_type_id, item_id, trans_qty, lot_id, expiry_date, unit_price, total_amount, tax_perc, free_care, net_total_amount, inventory_stock_id, trans_user_id) VALUES ( " + Convert.ToInt32(objArg.ParamList["pha_bill_id"]) + ",  now(), 1, " + Convert.ToInt32(objArg.ParamList["pha_item_type_id"]) + ",  " + Convert.ToInt32(objArg.ParamList["pha_item_id"]) + ", " + Convert.ToInt32(objArg.ParamList["pha_qty"]) + ",  '" + objArg.ParamList["pha_lot_id"].ToString() + "', '" + objArg.ParamList["pha_expiry_date"].ToString() + "', " + Convert.ToDecimal(objArg.ParamList["pha_unit_price"]) + ",  " + Convert.ToDecimal(objArg.ParamList["pha_total_amt"]) + ",  " + Convert.ToDecimal(objArg.ParamList["tax_perc"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_free_care"]) + ",  " + Convert.ToDecimal(objArg.ParamList["pha_net_total_amount"]) + ", " + Convert.ToInt32(objArg.ParamList["inventory_stock_id"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertPharmacyBillDetailsSummary(ComArugments objArg)
        {
            qry = "INSERT INTO pha_bill_detail_info (bill_id, transaction_date, trans_type, total_amount, free_care, discount, net_total_amount, amount_paid, due, payment_mode_id, trans_user_id, card_number, bank_name, holder_name, status) VALUES ( " + Convert.ToInt32(objArg.ParamList["pha_bill_id"]) + ",  now(), 2, " + Convert.ToDecimal(objArg.ParamList["pha_bill_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_total_free_care"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["pha_status"]) + " )";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateStockPharmacyBill(ComArugments objArg)
        {
            qry = "UPDATE pha_inventory_stock SET avail_qty = avail_qty - " + Convert.ToInt32(objArg.ParamList["pha_qty"]) + " where inventory_stock_id=" + Convert.ToInt32(objArg.ParamList["inventory_stock_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updatePharmacyRunningNumber(ComArugments objArg)
        {
            qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["pharmacy_bill_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_pharmacy_bill_number"]) + " where number_format_id=6";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        #endregion

        #region CIS Billing
        public int insertAccountHead(ComArugments objArg)
        {
            if (Convert.ToInt32(objArg.ParamList["id_cis_account_head"]) == 0)
            {
                qry = "INSERT INTO cis_account_head( account_head_name, is_account_group, account_group_id, bill_type, unit_price, status) VALUES ( '" + objArg.ParamList["account_head_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["is_account_group"]) + ", " + Convert.ToInt32(objArg.ParamList["account_group_id"]) + ", " + Convert.ToInt32(objArg.ParamList["bill_type"]) + ", " + Convert.ToDouble(objArg.ParamList["unit_price"]) + ", " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
                flag = objDBHandler.ExecuteCommand(qry);
            }
            else
            {
                qry = "UPDATE cis_account_head SET account_head_name= '" + objArg.ParamList["account_head_name"].ToString() + "', is_account_group=" + Convert.ToInt32(objArg.ParamList["is_account_group"]) + ", account_group_id=" + Convert.ToInt32(objArg.ParamList["account_group_id"]) + ", bill_type=" + Convert.ToInt32(objArg.ParamList["bill_type"]) + ", unit_price=" + Convert.ToDouble(objArg.ParamList["unit_price"]) + ", status=" + Convert.ToInt32(objArg.ParamList["status"]) + " WHERE id_cis_account_head =" + Convert.ToInt32(objArg.ParamList["id_cis_account_head"]) + "";
                flag = objDBHandler.ExecuteCommand(qry);
            }
            return flag;
        }

        public DataTable getAccountHeadDetails()
        {
            qry = @"SELECT 
                        ah1.id_cis_account_head,
                        ah1.account_head_name as 'Account Name',
                        if(ah1.is_account_group = 0,
                            'No',
                            'Yes') AS 'Is Account Group',
                        (select 
                                account_head_name
                            from
                                cis_account_head
                            where
                                id_cis_account_head = ah1.account_group_id
                            limit 1) as 'Account Group',
                        if(ah1.bill_type = 1,
                            'Ward Bill',
                            'General Bill') AS 'Bill Type',
                        ah1.unit_price as 'Unit Price',
                        if(ah1.status = 1,
                            'Active',
                            'In Active') AS 'Status'
                    FROM
                        cis_account_head ah1
                    order by ah1.bill_type ASC , ah1.is_account_group DESC , ah1.account_head_name ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getAccountGroup()
        {
            qry = @"SELECT 
                        id_cis_account_head as id_cis_account_group, account_head_name as account_group_name
                    from
                        cis_account_head
                    WHERE
                        is_account_group = 1 and status = 1 order by account_head_name asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadGeneralCharges()
        {
            qry = @"SELECT 
                        id_cis_account_head, account_head_name
                    FROM
                        cis_account_head
                    where
                        bill_type = 2 and is_account_group = 0
                            and status = 1
                    order by account_head_name ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable loadWardCharges()
        {
            qry = @"SELECT 
                        id_cis_account_head, account_head_name
                    FROM
                        cis_account_head
                    where
                        bill_type = 1 and is_account_group = 0
                            and status = 1
                    order by account_head_name ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int deleteAccountHead(int account_head_id)
        {
            qry = "delete from cis_account_head where id_cis_account_head=" + account_head_id + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getAccoutHeadById(int accountHeadId)
        {
            qry = @"SELECT 
                        id_cis_account_head,
                        account_head_name,
                        is_account_group,
                        account_group_id,
                        bill_type,
                        unit_price,
                        status
                    FROM
                        cis_account_head
                    where
                        id_cis_account_head = " + accountHeadId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getBillChargesById(int generalChargesId)
        {
            qry = @"SELECT 
                        (select 
                                ah1.account_head_name
                            from
                                cis_account_head ah1
                            where
                                ah1.id_cis_account_head = ah2.account_group_id
                            limit 1) as account_head_name,
                        ah2.account_group_id,
                        ah2.unit_price
                    from
                        cis_account_head ah2
                    where
                        ah2.id_cis_account_head =" + generalChargesId + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getGeneralInfo(string startDate, string endDate)
        {
            qry = @"SELECT 
                        bill_number as 'Bill No',
                        date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',
                        patient_id as 'Patient Id',
                        visit_number as 'Visit Id',
                        patient_name as 'Patient Name',
                        bill_amount as 'Bill Amount',
                        Discount,
                        (amount_paid + pay_from_advance + due_collection) as 'Amount Paid',
                        Due,
                        (case
                            when status = 1 then 'Paid'
                            when status = 2 then 'Not Paid'
                            when status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        gen_bill_info
                    where
                        bill_date between '" + Convert.ToDateTime(startDate).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(endDate).ToString("yyyy-MM-dd HH:mm:ss") + "' order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public int advanceTransaction(ComArugments objArg)
        {
            if (Convert.ToDecimal(objArg.ParamList["advance_collection"]) > 0)
            {
                qry = "INSERT INTO cis_advance (patient_id, visit_no, patient_name, account_type, transaction_amount, net_amount, receipt_number, trans_user_id, trans_date, payment_mode_id, card_number, bank_name, holder_name) VALUES ('" + objArg.ParamList["patient_id"].ToString() + "', '" + objArg.ParamList["visit_number"].ToString() + "', '" + objArg.ParamList["patient_name"].ToString() + "', 1, " + Convert.ToDecimal(objArg.ParamList["advance_collection"]) + ", " + Convert.ToDecimal(objArg.ParamList["total_adv_net_coll"]) + ", '" + objArg.ParamList["adv_collection_number"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "')";
                flag = objDBHandler.ExecuteCommand(qry);

                qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["adv_collection_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_adv_collection_number"]) + " where number_format_id=9";
                flag = objDBHandler.ExecuteCommand(qry);
            }

            else
            {
                qry = "INSERT INTO cis_advance (patient_id, visit_no, patient_name, account_type, transaction_amount, net_amount, receipt_number, trans_user_id, trans_date, payment_mode_id, card_number, bank_name, holder_name) VALUES ('" + objArg.ParamList["patient_id"].ToString() + "', '" + objArg.ParamList["visit_number"].ToString() + "', '" + objArg.ParamList["patient_name"].ToString() + "', 3, " + Convert.ToDecimal(objArg.ParamList["advance_refund"]) + ", " + Convert.ToDecimal(objArg.ParamList["total_adv_net_coll"]) + ", '" + objArg.ParamList["adv_refund_number"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "')";
                flag = objDBHandler.ExecuteCommand(qry);

                qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["adv_refund_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_adv_refund_number"]) + " where number_format_id=10";
                flag = objDBHandler.ExecuteCommand(qry);
            }
            return flag;
        }

        public DataTable getNetAdvAvailbyPatientId(string patient_id, string visit_number)
        {
            qry = @"SELECT 
                        net_amount
                    FROM
                        cis_advance
                    where
                        patient_id = '" + patient_id + "' and visit_no = '" + visit_number + "' order by advance_id desc limit 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getFinalBillStatusbyPatientId(string patient_id, string visit_number)
        {
            qry = @"SELECT 
                        bill_number
                    FROM
                        ward_bill_info
                    where
                        bill_type = 1 and
                        patient_id = '" + patient_id + "' and visit_number = '" + visit_number + "' limit 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable fetchAdvanceTransactionDetails(string startDate, string endDate)
        {
            qry = @"SELECT
                        advance_id, 
                        patient_id as 'Patient Id',
                        patient_name as 'Patient Name',
                        date_format(trans_date, '%d/%m/%Y %h:%i %p') as 'Transaction Date',
                        visit_no as 'Visit Id',
                        receipt_number as 'Receipt No',
                        case
                            when account_type = 1 then 'Deposit Collection'
                            when account_type = 2 then 'Deposit Adjustment'
                            when account_type = 3 then 'Deposit Refund'
                            when account_type = 4 then 'Deposit In'
                        END AS 'Transaction Type',
                        transaction_amount as 'Transaction Amt',
                        net_amount as 'Net Amt'
                    FROM
                        cis_advance
                    where
                        trans_date between '" + startDate + "' and '" + endDate + "' order by trans_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region Save General Bill
        public int insertGeneralBill(ComArugments objArg)
        {
            qry = "INSERT INTO gen_bill_info (bill_number, bill_date, patient_id, visit_number, patient_name, bill_amount, discount, total_amount, amount_paid, due, trans_user_id, status) VALUES ( '" + objArg.ParamList["general_bill_number"].ToString() + "', now(), '" + objArg.ParamList["invoice_patient_id"].ToString() + "', '" + objArg.ParamList["invoice_visit_number"].ToString() + "','" + objArg.ParamList["invoice_patient_name"].ToString() + "', " + Convert.ToDecimal(objArg.ParamList["gen_bill_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["gen_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["gen_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["gen_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["gen_due"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["gen_status"]) + ")";
            flag = objDBHandler.ExecuteCommandTransaction(qry); // //Excute Insert Qry and Begin the Transaction
            return flag;
        }

        public int insertGeneralBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO gen_bill_detail_info(bill_id, account_head_id, account_name, account_group_id, account_type_id, transaction_date, qty, unit_price, amount, trans_user_id) VALUES ( " + Convert.ToInt32(objArg.ParamList["gen_bill_id"]) + ",  " + Convert.ToInt32(objArg.ParamList["gen_account_head_id"]) + ",  '" + objArg.ParamList["gen_account_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["gen_account_group_id"]) + ",  1, now(), " + Convert.ToInt32(objArg.ParamList["gen_qty"]) + ",  " + Convert.ToDecimal(objArg.ParamList["gen_unit_price"]) + ",  " + Convert.ToDecimal(objArg.ParamList["gen_amount"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertGeneralBillDetailsSummary(ComArugments objArg)
        {
            qry = "INSERT INTO gen_bill_detail_info (bill_id, account_type_id, transaction_date, amount, discount, total_amount, amount_paid, due, payment_mode_id, card_number, bank_name, holder_name, trans_user_id, status) VALUES ( " + Convert.ToInt32(objArg.ParamList["gen_bill_id"]) + ", 2, now(), " + Convert.ToDecimal(objArg.ParamList["gen_bill_amount"]) + ",  " + Convert.ToDecimal(objArg.ParamList["gen_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["gen_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["gen_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["gen_due"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["gen_status"]) + " )";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateGeneralRunningNumber(ComArugments objArg)
        {
            qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["general_bill_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_general_bill_number"]) + " where number_format_id=7";
            flag = objDBHandler.ExecuteCommand(qry);

            return flag;
        }
        #endregion

        #region Save Ward Bill
        public int insertWardBill(ComArugments objArg)
        {
            qry = "INSERT INTO ward_bill_info (bill_number, bill_date, patient_id, visit_number, bill_amount, discount, total_amount, amount_paid, pay_from_advance, due, status, transaction_date, transaction_user_id, bill_type, from_date, to_date, discount_perc_amt, discount_type) VALUES ('" + objArg.ParamList["ward_bill_number"].ToString() + "', now(), '" + objArg.ParamList["patient_id"].ToString() + "', '" + objArg.ParamList["visit_number"].ToString() + "', " + Convert.ToDecimal(objArg.ParamList["ward_bill_amount"]) + ",  " + Convert.ToDecimal(objArg.ParamList["ward_discount"]) + ",  " + Convert.ToDecimal(objArg.ParamList["ward_total_amount"]) + ",  " + Convert.ToDecimal(objArg.ParamList["ward_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_Adv_Adj"]) + ",  " + Convert.ToDecimal(objArg.ParamList["ward_due"]) + ", " + Convert.ToInt32(objArg.ParamList["ward_status"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["bill_type"]) + ", '" + Convert.ToDateTime(objArg.ParamList["from_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Convert.ToDateTime(objArg.ParamList["to_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "', " + Convert.ToDecimal(objArg.ParamList["ward_discountPercOrAmt"]) + ", " + Convert.ToInt32(objArg.ParamList["ward_discount_type"]) + ")";
            flag = objDBHandler.ExecuteCommandTransaction(qry); // //Excute Insert Qry and Begin the Transaction
            return flag;
        }

        public int insertWardBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO ward_bill_detail_info (bill_id, account_id, account_code, account_group_id, account_group_name, quantity, unit_price, amount, transaction_user_id, transaction_date, account_transaction_type) VALUES (" + Convert.ToInt32(objArg.ParamList["ward_bill_id"]) + ", " + Convert.ToInt32(objArg.ParamList["ward_account_head_id"]) + ",   '" + objArg.ParamList["ward_account_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["ward_account_group_id"]) + ",   '" + objArg.ParamList["ward_account_group_name"].ToString() + "', " + Convert.ToDecimal(objArg.ParamList["ward_qty"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_unit_price"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_amount"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", now(), 1)";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertWardBillDetailsSummary(ComArugments objArg)
        {
            qry = "INSERT INTO ward_bill_detail_info (bill_id, amount, discount, total_amount, amount_paid, pay_from_advance, due, transaction_user_id, transaction_date, payment_mode_id, card_number, bank_name, holder_name, account_transaction_type, status) VALUES (" + Convert.ToInt32(objArg.ParamList["ward_bill_id"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_bill_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_Adv_Adj"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_due"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ",  '" + objArg.ParamList["card_number"].ToString() + "',  '" + objArg.ParamList["bank_name"].ToString() + "',  '" + objArg.ParamList["holder_name"].ToString() + "', 2, " + Convert.ToInt32(objArg.ParamList["ward_status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertWardAdvAdjTrans(ComArugments objArg)
        {
            qry = "INSERT INTO cis_advance (patient_id, patient_name, visit_no, account_type, transaction_amount, net_amount, trans_user_id, trans_date, bill_id) VALUES ('" + objArg.ParamList["patient_id"].ToString() + "', '" + objArg.ParamList["patient_name"].ToString() + "', '" + objArg.ParamList["visit_number"].ToString() + "',  2, " + Convert.ToDecimal(objArg.ParamList["ward_Adv_Adj"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_Adv_Net_Coll"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["ward_bill_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateWardRunningNumber(ComArugments objArg)
        {
            qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["ward_bill_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_ward_bill_number"]) + " where number_format_id=11";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        #endregion

        #region CIS Pharmacy Inventory Management
        public DataTable getPhaDetailsForPurchase(string ItemNmae)
        {
            qry = @"SELECT 
                        pi.item_id, pit.item_type, pi.item_type_id, cpt.tax_rate, pi.pack_y
                    FROM
                        cis_pha_item pi
                            LEFT JOIN
                        cis_pha_item_type pit ON pi.item_type_id = pit.item_type_id
                            LEFT JOIN
                        cis_pha_tax cpt ON pi.tax_id = cpt.tax_id
                    where
                        pi.item_name = '" + ItemNmae + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region Save Purchase Receipt
        public int insertPurchaseReceipt(ComArugments objArg)
        {
            qry = "INSERT INTO pha_purchase_receipt (pur_invoice_number, pur_invoice_date, grn_number, vendor_id, invoice_total_amount, cash_discount, item_discount, tax_amount, returned_amount, net_invoice_amount, amount_paid, balance_amount, round_off, pur_receive_date) VALUES ( '" + objArg.ParamList["pur_invoice_number"].ToString() + "',  '" + Convert.ToDateTime(objArg.ParamList["pur_invoice_date"]).ToString("yyyy-MM-dd") + "', '" + objArg.ParamList["purchase_receipt_number"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["vendor_id"]) + ", " + Convert.ToDecimal(objArg.ParamList["invoice_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["cash_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["item_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["tax_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["returned_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["net_invoice_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["balance_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["round_off"]) + ", '" + Convert.ToDateTime(objArg.ParamList["pur_receive_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "' )";
            flag = objDBHandler.ExecuteCommandTransaction(qry); // //Excute Insert Qry and Begin the Transaction
            return flag;
        }

        public int insertPurchaseReceiptDetails(ComArugments objArg)
        {
            qry = "INSERT INTO pha_purchase_receipt_details (pur_invoice_id, item_type_id, item_id, received_qty, offer_qty, batch_no, expiry_date, pack_size_x, pack_size_y, vendor_price, mrp, total_amount, discount_perc, discount_amount, tax_formula, tax_perc, tax_amount, is_free_tax, free_tax_amount, net_total_amount) VALUES (  " + Convert.ToInt32(objArg.ParamList["pur_invoice_id"]) + ",  " + Convert.ToInt32(objArg.ParamList["item_type_id"]) + ",  " + Convert.ToInt32(objArg.ParamList["item_id"]) + ",  " + Convert.ToInt32(objArg.ParamList["received_qty"]) + ",  " + Convert.ToInt32(objArg.ParamList["offer_qty"]) + ", '" + objArg.ParamList["batch_no"].ToString() + "',  '" + Convert.ToDateTime(objArg.ParamList["expiry_date"]).ToString("yyyy-MM-dd") + "',  " + Convert.ToInt32(objArg.ParamList["pack_size_x"]) + ",  " + Convert.ToInt32(objArg.ParamList["pack_size_y"]) + ", " + Convert.ToDecimal(objArg.ParamList["vendor_price"]) + ", " + Convert.ToDecimal(objArg.ParamList["mrp"]) + ", " + Convert.ToDecimal(objArg.ParamList["total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["discount_perc"]) + ", " + Convert.ToDecimal(objArg.ParamList["discount_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["tax_formula"]) + ", " + Convert.ToDecimal(objArg.ParamList["tax_perc"]) + ", " + Convert.ToDecimal(objArg.ParamList["tax_amount"]) + ", " + Convert.ToInt32(objArg.ParamList["is_free_tax"]) + ", " + Convert.ToDecimal(objArg.ParamList["free_tax_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["net_total_amount"]) + " )";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updatePurRunningNumber(ComArugments objArg)
        {
            qry = "UPDATE cis_number_format SET last_bill_number='" + objArg.ParamList["purchase_receipt_number"].ToString() + "', running_number= " + Convert.ToInt32(objArg.ParamList["running_purchase_receipt_number"]) + " where number_format_id=8";
            flag = objDBHandler.ExecuteCommand(qry);

            return flag;
        }

        public int insertPurchaseStock(ComArugments objArg)
        {
            qry = "INSERT INTO pha_inventory_stock (department_id, item_type_id, item_id, lot_id, exp_date, avail_qty, mrp, tax_perc) VALUES(7, " + Convert.ToInt32(objArg.ParamList["item_type_id"]) + ", " + Convert.ToInt32(objArg.ParamList["item_id"]) + ", '" + objArg.ParamList["batch_no"].ToString() + "', '" + Convert.ToDateTime(objArg.ParamList["expiry_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "', " + Convert.ToInt32(objArg.ParamList["received_qty"]) + " + "+ Convert.ToInt32(objArg.ParamList["offer_qty"]) + ", " + Convert.ToDecimal(objArg.ParamList["mrp"]) + ", " + Convert.ToDecimal(objArg.ParamList["tax_perc"]) + ")";
            flag = objDBHandler.ExecuteCommandTransaction(qry); // //Excute Insert Qry and Begin the Transaction
            return flag;
        }

        public int insertPurchaseStockDetails(ComArugments objArg)
        {
            qry = "INSERT INTO pha_inventory_stock_details (inventory_stock_id, inventory_transaction_type, department_id, item_type_id, item_id, lot_id, exp_date, transaction_qty, avail_qty, mrp, transaction_date, tax_perc) VALUES(" + Convert.ToInt32(objArg.ParamList["inv_trans_id"]) + ", 2, 7, " + Convert.ToInt32(objArg.ParamList["item_type_id"]) + ", " + Convert.ToInt32(objArg.ParamList["item_id"]) + ", '" + objArg.ParamList["batch_no"].ToString() + "', '" + Convert.ToDateTime(objArg.ParamList["expiry_date"]).ToString("yyyy-MM-dd HH:mm:ss") + "', " + Convert.ToInt32(objArg.ParamList["received_qty"]) + " + " + Convert.ToInt32(objArg.ParamList["offer_qty"]) + ", " + Convert.ToInt32(objArg.ParamList["received_qty"]) + " + " + Convert.ToInt32(objArg.ParamList["offer_qty"]) + ", " + Convert.ToDecimal(objArg.ParamList["mrp"]) + ", now(), " + Convert.ToDecimal(objArg.ParamList["tax_perc"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public DataTable getPurchaseReceiptInfo(string dateBy, string startDate, string endDate)
        {
            qry = string.Format(@"SELECT ppr.pur_invoice_id,
                        ppr.pur_invoice_number as 'Invoice No',
                        date_format(ppr.pur_invoice_date, '%d/%m/%Y') as 'Invoice Date',
                        ppr.grn_number as 'GRN No',
                        date_format(ppr.pur_receive_date,
                                '%d/%m/%Y %h:%i %p') as 'Receive Date',
                        pv.vendor_name as 'Vendor Name',
                        round(ppr.net_invoice_amount, 0) as 'Net Amount'
                    FROM
                        pha_purchase_receipt ppr
                            LEFT JOIN
                        cis_pha_vender pv ON ppr.vendor_id = pv.vendor_id
                    where
                        ppr.{0} between '" + startDate + "' and '" +  endDate + "' order by ppr.{0} desc", dateBy);
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getPurchaseReceiptLoad(int invoice_id)
        {
            qry =@"SELECT 
                        ppr.pur_invoice_id,
                        ppr.pur_invoice_number,
                        ppr.pur_invoice_date,
                        ppr.grn_number,
                        ppr.vendor_id,
                        ppr.invoice_total_amount,
                        ppr.cash_discount,
                        ppr.item_discount,
                        ppr.tax_amount,
                        ppr.returned_amount,
                        ppr.net_invoice_amount,
                        ppr.amount_paid,
                        ppr.balance_amount,
                        ppr.pur_receive_date,
                        pprd.pur_invoice_detail_id,
                        pprd.item_type_id,
                        pprd.item_id,
                        pprd.received_qty,
                        pprd.offer_qty,
                        pprd.batch_no,
                        pprd.expiry_date,
                        pprd.pack_size_x,
                        pprd.pack_size_y,
                        pprd.vendor_price,
                        pprd.mrp,
                        pprd.total_amount,
                        pprd.discount_perc,
                        pprd.discount_amount,
                        pprd.tax_formula,
                        pprd.tax_perc,
                        pprd.tax_amount,
                        pprd.is_free_tax,
                        pprd.free_tax_amount,
                        pprd.net_total_amount
                    FROM
                        pha_purchase_receipt ppr
                            join
                        pha_purchase_receipt_details pprd ON ppr.pur_invoice_id = pprd.pur_invoice_id
                    where ppr.pur_invoice_id = " + Convert.ToInt32(objArg.ParamList["invoice_id"]) + "";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region Cancel or Refund
        public DataTable getBillTypeId(string prefix_text)
        {
            qry = @"SELECT 
                        number_format_id
                    FROM
                        cis_number_format
                    where
                        prefix_text = '" + prefix_text + "' limit 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getInvestigationBillInfo(string billNo)
        {
            qry = @"SELECT 
                        bill_id,
                        bill_number,
                        bill_date,
                        patient_id,
                        visit_number,
                        bill_amount,
                        discount,
                        total_amount,
                        (amount_paid + due_collection) as amount_paid,
                        pay_from_advance,
                        due,
                        status,
                        patient_name
                    FROM
                        inv_bill_info
                    where
                        bill_number = '" + billNo + "' and status <> 4";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getInvestigationBillDetailInfo(int billId)
        {
            qry = @"SELECT 
                        bdi.transaction_id,
                        bdi.bill_id,
                        bdi.investigation_id,
                        il.investigation_code,
                        il.investigation_name,
                        bdi.department_id,
                        d.department_name,
                        bdi.qty,
                        bdi.unit_price,
                        bdi.amount,
                        bdi.status
                    FROM
                        inv_bill_datail_info bdi
                            JOIN
                        cis_investigation_list il ON bdi.investigation_id = il.investigation_id
                            JOIN
                        cis_department d ON bdi.department_id = d.DEPARTMENT_ID
                    where
                        bill_id = " + billId + " and account_type = 1 and bdi.status <> 4";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getPharmacyBillInfo(string billNo)
        {
            qry = @"SELECT 
                        bill_id,
                        bill_number,
                        bill_date,
                        patient_id,
                        visit_number,
                        bill_amount,
                        discount,
                        total_free_care,
                        total_amount,
                        (amount_paid + due_collection) as amount_paid,
                        pay_from_advance,
                        due,
                        trans_department_id,
                        patient_name,
                        status
                    FROM
                        pha_bill_info
                    where
                        bill_number = '" + billNo + "' and status <> 4";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getPharmacyBillDetailInfo(int billId)
        {
            qry = @"SELECT 
                        pdi.bill_detail_id,
                        pdi.bill_id,
                        pdi.transaction_date,
                        pdi.trans_type,
                        pdi.item_type_id,
                        pit.item_type,
                        pdi.item_id,
                        pi.item_name,
                        pdi.trans_qty,
                        pdi.lot_id,
                        pdi.expiry_date as exp_date,
                        pdi.unit_price,
                        pdi.total_amount,
                        pdi.tax_perc,
                        pdi.free_care,
                        pdi.net_total_amount,
                        pdi.trans_department_id,
                        pdi.inventory_stock_id,
                        pdi.status
                    FROM
                        pha_bill_detail_info pdi
                            JOIN
                        cis_pha_item pi ON pdi.item_id = pi.item_id
                            join
                        cis_pha_item_type pit ON pi.item_type_id = pit.item_type_id
                    where
                        pdi.bill_id = " + billId + " and pdi.trans_type = 1 and pdi.status <> 4";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getGeneralBillInfo(string billNo)
        {
            qry = @"SELECT 
                        gen_bill_id as bill_id,
                        bill_number,
                        bill_date,
                        patient_id,
                        visit_number,
                        bill_amount,
                        discount,
                        total_amount,
                        (amount_paid + due_collection) as amount_paid,
                        pay_from_advance,
                        due,
                        status,
                        patient_name
                    FROM
                        gen_bill_info
                    where
                        bill_number = '" + billNo + "' and status <> 4";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable getGeneralBillDetailInfo(int billId)
        {
            qry = @"SELECT 
                        gbdi.bill_detail_id,
                        gbdi.bill_id,
                        gbdi.account_group_id,
                        gbdi.account_head_id,
                        ah.account_head_name,
                        gbdi.qty,
                        gbdi.unit_price,
                        gbdi.amount
                    FROM
                        gen_bill_detail_info gbdi
                            JOIN
                        cis_account_head ah ON gbdi.account_head_id = ah.id_cis_account_head
                    where
                        bill_id = " + billId + " and account_type_id = 1 and gbdi.status <> 4";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        //Pharmacy Begin
        public int updateRefundPharmacyBill(ComArugments objArg)
        {
            qry = "UPDATE pha_bill_info SET bill_amount = " + Convert.ToDecimal(objArg.ParamList["pha_bill_amount"]) + ", discount = " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", total_free_care = " + Convert.ToDecimal(objArg.ParamList["pha_total_free_care"]) + ", total_amount = " + Convert.ToDecimal(objArg.ParamList["pha_total_amount"]) + ", amount_paid = " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + " + " + Convert.ToDecimal(objArg.ParamList["already_paid"]) + ", due = " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", due_collection = " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", refund_amount = " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ",  refund_to_patient = " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", status = " + Convert.ToInt32(objArg.ParamList["pha_status"]) + " WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateRefundPharmacyPartialBillDetails(ComArugments objArg)
        {
            qry = "UPDATE pha_bill_detail_info SET status = 4, cancelled_date = now() WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + " and item_id = " + Convert.ToInt32(objArg.ParamList["cItemId"]) + " and lot_id = '" + objArg.ParamList["cLotId"].ToString() + "' and trans_type = 1 and status <> 4";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertRefundPharmacyFullBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO pha_bill_detail_info (bill_id, transaction_date, trans_type, item_type_id, item_id, trans_qty, refund_qty, lot_id, expiry_date, unit_price, total_amount, tax_perc, free_care, ref_free_care, net_total_amount, refund_amt, inventory_stock_id, status) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", now(), 1, " + Convert.ToInt32(objArg.ParamList["cTypeId"]) + ", " + Convert.ToInt32(objArg.ParamList["cItemId"]) + ", " + Convert.ToInt32(objArg.ParamList["cTransPhaQty"]) + ", " + Convert.ToInt32(objArg.ParamList["cQty"]) + ", '" + objArg.ParamList["cLotId"].ToString() + "', '" + objArg.ParamList["cExpDate"].ToString() + "', " + Convert.ToDecimal(objArg.ParamList["cUnitPrice"]) + ", " + Convert.ToDecimal(objArg.ParamList["cTotalAmt"]) + ", " + Convert.ToDecimal(objArg.ParamList["cSalesTaxPerc"]) + ", " + Convert.ToDecimal(objArg.ParamList["cFreeCare"]) + ", " + Convert.ToDecimal(objArg.ParamList["cRefundFCare"]) + ", " + Convert.ToDecimal(objArg.ParamList["net_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["cRefundAmt"]) + ", " + Convert.ToInt32(objArg.ParamList["cInventoryStockId"]) + ", 4)";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateRefundPharmacyBillDetailSummary(ComArugments objArg)
        {
            qry = "UPDATE pha_bill_detail_info set cancelled_date = now(), status = 4 WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + " and trans_type = 2 and status <> 4";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int InsertRefundPharmacyBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO pha_bill_detail_info (bill_id, transaction_date, trans_type, item_type_id, item_id, trans_qty, refund_qty, lot_id, expiry_date, unit_price, total_amount, tax_perc, free_care, ref_free_care, net_total_amount, refund_amt, inventory_stock_id, trans_user_id) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", now(), 1, " + Convert.ToInt32(objArg.ParamList["cTypeId"]) + ", " + Convert.ToInt32(objArg.ParamList["cItemId"]) + ", " + Convert.ToInt32(objArg.ParamList["cTransPhaQty"]) + ", " + Convert.ToInt32(objArg.ParamList["cQty"]) + ", '" + objArg.ParamList["cLotId"].ToString() + "', '" + objArg.ParamList["cExpDate"].ToString() + "', " + Convert.ToDecimal(objArg.ParamList["cUnitPrice"]) + ", " + Convert.ToDecimal(objArg.ParamList["cTotalAmt"]) + ", " + Convert.ToDecimal(objArg.ParamList["cSalesTaxPerc"]) + ", " + Convert.ToDecimal(objArg.ParamList["cFreeCare"]) + ", " + Convert.ToDecimal(objArg.ParamList["cRefundFCare"]) + ", " + Convert.ToDecimal(objArg.ParamList["net_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["cRefundAmt"]) + ", " + Convert.ToInt32(objArg.ParamList["cInventoryStockId"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int InsertRefundPharmacyPartialBillDetailSummary(ComArugments objArg)
        {
            //qry = "INSERT INTO pha_bill_detail_info (bill_id, transaction_date, trans_type, total_amount, free_care, discount, net_total_amount, amount_paid, refund_amt, refund_to_patient, due, due_collection, cancelled_date, inventory_stock_id, trans_user_id, payment_mode_id, card_number, bank_name, holder_name) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", now(), 2, " + Convert.ToDecimal(objArg.ParamList["pha_bill_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_total_free_care"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_total_amount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["cInventoryStockId"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "')";
            qry = "INSERT INTO pha_bill_detail_info (bill_id, transaction_date, trans_type, ref_free_care, discount, amount_paid, refund_amt, refund_to_patient, due, due_collection, cancelled_date, inventory_stock_id, trans_user_id, payment_mode_id, card_number, bank_name, holder_name) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", now(), 2, " + Convert.ToDecimal(objArg.ParamList["ref_freecare"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["cInventoryStockId"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "')";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int InsertRefundPharmacyFullBillDetailSummary(ComArugments objArg)
        {
            qry = "INSERT INTO pha_bill_detail_info (bill_id, transaction_date, trans_type, discount, amount_paid, refund_amt, refund_to_patient, due, due_collection, cancelled_date, inventory_stock_id, trans_user_id, payment_mode_id, card_number, bank_name, holder_name, status) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", now(), 2, " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["cInventoryStockId"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "', 4)";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateStockRefundPharmacyBill(ComArugments objArg)
        {
            qry = "UPDATE pha_inventory_stock SET avail_qty = avail_qty + " + Convert.ToInt32(objArg.ParamList["cQty"]) + " where inventory_stock_id=" + Convert.ToInt32(objArg.ParamList["cInventoryStockId"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        //Pharmacy End


        //Investigation Begin
        public int updateRefundInvestigationBill(ComArugments objArg)
        {
            qry = "UPDATE inv_bill_info SET bill_amount = " + Convert.ToDecimal(objArg.ParamList["pha_bill_amount"]) + ", discount = " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", total_amount =  " + Convert.ToDecimal(objArg.ParamList["pha_total_amount"]) + ", amount_paid = " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"])+" + "+Convert.ToDecimal(objArg.ParamList["already_paid"]) +", due = " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", due_collection = " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", refund_amount = " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ", refund_to_patient = " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ",status = " + Convert.ToInt32(objArg.ParamList["pha_status"]) + " WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateRefundInvestigationPartialBillDetails(ComArugments objArg)
        {
            qry = "UPDATE inv_bill_datail_info SET status = 4, cancelled_date = now() WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + " and investigation_id= " + Convert.ToInt32(objArg.ParamList["cItemId"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertRefundInvestigationFullBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO inv_bill_datail_info(bill_id, investigation_id, department_id, qty, cqty, unit_price, amount, refund_amt, cancelled_date, account_type, transaction_date, status) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", " + Convert.ToInt32(objArg.ParamList["cItemId"]) + ", " + Convert.ToInt32(objArg.ParamList["cDepartmentId"]) + ", " + Convert.ToInt32(objArg.ParamList["cTransPhaQty"]) + ", " + Convert.ToInt32(objArg.ParamList["cQty"]) + ", " + Convert.ToDecimal(objArg.ParamList["cUnitPrice"]) + ", " + Convert.ToDecimal(objArg.ParamList["cTotalAmt"]) + ", " + Convert.ToDecimal(objArg.ParamList["cRefundAmt"]) + ", now(), 1, now(), 4)";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateRefundInvestigationBillDetailSummary(ComArugments objArg)
        {
            qry = "UPDATE inv_bill_datail_info SET cancelled_date = now(), status = 4 WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + " and account_type = 2 and status <> 4";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int InsertRefundInvestigationBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO inv_bill_datail_info(bill_id, investigation_id, department_id, qty, cqty, unit_price, amount, refund_amt, cancelled_date, account_type, transaction_date) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", " + Convert.ToInt32(objArg.ParamList["cItemId"]) + ", " + Convert.ToInt32(objArg.ParamList["cDepartmentId"]) + ", " + Convert.ToInt32(objArg.ParamList["cTransPhaQty"]) + ", " + Convert.ToInt32(objArg.ParamList["cQty"]) + ", " + Convert.ToDecimal(objArg.ParamList["cUnitPrice"]) + ", " + Convert.ToDecimal(objArg.ParamList["cTotalAmt"]) + ", " + Convert.ToDecimal(objArg.ParamList["cRefundAmt"]) + ", now(), 1, now())";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int InsertRefundInvestigationPartialBillDetailSummary(ComArugments objArg)
        {
            qry = "INSERT INTO inv_bill_datail_info(bill_id, discount, account_type, amount_paid, due, due_collection, refund_amt, refund_to_patient, cancelled_date, payment_mode_id, card_number, bank_name, holder_name, transaction_date, transaction_user_id) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", 2, " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ",  '" + objArg.ParamList["card_number"].ToString() + "',  '" + objArg.ParamList["bank_name"].ToString() + "',  '" + objArg.ParamList["holder_name"].ToString() + "', now(), " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int InsertRefundInvestigationFullBillDetailSummary(ComArugments objArg)
        {
            qry = "INSERT INTO inv_bill_datail_info(bill_id, discount, account_type, amount_paid, due, due_collection, refund_amt, refund_to_patient, cancelled_date, payment_mode_id, card_number, bank_name, holder_name, transaction_date, transaction_user_id, status) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", 2, " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ",  '" + objArg.ParamList["card_number"].ToString() + "',  '" + objArg.ParamList["bank_name"].ToString() + "',  '" + objArg.ParamList["holder_name"].ToString() + "', now(), " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", 4)";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        #endregion
        //Investigation End

        //General Begin
        public int updateRefundGeneralBill(ComArugments objArg)
        {
            qry = "UPDATE gen_bill_info SET bill_amount = " + Convert.ToDecimal(objArg.ParamList["pha_bill_amount"]) + ", discount = " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", total_amount = " + Convert.ToDecimal(objArg.ParamList["pha_total_amount"]) + ", amount_paid = " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + " + " + Convert.ToDecimal(objArg.ParamList["already_paid"]) + ", due = " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ", due_collection = " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", refund_amount = " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ", refund_to_patient = " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", status = " + Convert.ToInt32(objArg.ParamList["pha_status"]) + " WHERE gen_bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateRefundGeneralPartialBillDetails(ComArugments objArg)
        {
            qry = "UPDATE gen_bill_detail_info SET status = 4, cancelled_date = now() WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + " and account_head_id= " + Convert.ToInt32(objArg.ParamList["cItemId"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int InsertRefundGeneralBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO gen_bill_detail_info (bill_id, account_head_id, account_name, account_group_id, account_type_id, transaction_date, qty, refund_qty, unit_price, amount, refund_amt) VALUES ( " + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", " + Convert.ToInt32(objArg.ParamList["cItemId"]) + ", " + objArg.ParamList["cName"] + ", " + Convert.ToInt32(objArg.ParamList["cTypeId"]) + ", 1, now(), " + Convert.ToInt32(objArg.ParamList["cTransPhaQty"]) + ", " + Convert.ToInt32(objArg.ParamList["cQty"]) + ", " + Convert.ToDecimal(objArg.ParamList["cUnitPrice"]) + ", " + Convert.ToDecimal(objArg.ParamList["cTotalAmt"]) + ", " + Convert.ToDecimal(objArg.ParamList["cRefundAmt"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateRefundGeneralBillDetailSummary(ComArugments objArg)
        {
            qry = "UPDATE gen_bill_detail_info SET cancelled_date = now(), status = 4 WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + " and account_type_id = 2 and status <> 4";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int InsertRefundGeneralPartialBillDetailSummary(ComArugments objArg)
        {
            qry = "INSERT INTO gen_bill_detail_info (bill_id, transaction_date, discount,  amount_paid, due, due_collection, payment_mode_id, card_number, bank_name, holder_name, trans_user_id, refund_amt, refund_to_patient, cancelled_date) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", now(),  " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ",  " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ",   '" + objArg.ParamList["card_number"].ToString() + "',   '" + objArg.ParamList["bank_name"].ToString() + "',   '" + objArg.ParamList["holder_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ",  " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", now())";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertRefundGeneralFullBillDetails(ComArugments objArg)
        {
            qry = "INSERT INTO gen_bill_detail_info (bill_id, account_head_id, account_name, account_group_id, account_type_id, transaction_date, qty, unit_price, amount) VALUES ( " + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", " + Convert.ToInt32(objArg.ParamList["cItemId"]) + ", '" + objArg.ParamList["cName"] + "', " + Convert.ToInt32(objArg.ParamList["cTypeId"]) + ", 1, now(), " + Convert.ToInt32(objArg.ParamList["cTransPhaQty"]) + ", " + Convert.ToDecimal(objArg.ParamList["cUnitPrice"]) + ", " + Convert.ToDecimal(objArg.ParamList["cTotalAmt"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int InsertRefundGeneralFullBillDetailSummary(ComArugments objArg)
        {
            qry = "INSERT INTO gen_bill_detail_info (bill_id, account_type_id, transaction_date, discount, amount_paid, due, due_collection, payment_mode_id, card_number, bank_name, holder_name, trans_user_id, refund_amt, refund_to_patient, cancelled_date, status) VALUES (" + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", 2, now(), " + Convert.ToDecimal(objArg.ParamList["pha_discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["pha_due"]) + ",  " + Convert.ToDecimal(objArg.ParamList["pha_due_collection"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ",   '" + objArg.ParamList["card_number"].ToString() + "',   '" + objArg.ParamList["bank_name"].ToString() + "',   '" + objArg.ParamList["holder_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToDecimal(objArg.ParamList["refund_amt"]) + ",  " + Convert.ToDecimal(objArg.ParamList["refund_to_patient"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["pha_status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        //General End

        public DataTable getPatientDetails()
        {
            qry = @"SELECT 
                        patient_id, gender, patient_name
                    FROM
                        pat_reg_info";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        #region Printing Info
        public DataTable printRegistationSlip(string visit_number)
        {
            qry = @"SELECT 
                        pri.patient_id,
                        pri.patient_name,
                        if(pri.gender = 1, 'Female', 'Male') as gender_name,
                        concat(pri.age_year,
                                'Y ',
                                pri.age_month,
                                'M ',
                                pri.age_day,
                                'D') as age,
                        pri.guardian_name,
                        pri.address,
                        pri.phone_no,
                        cc.corporate_name
                    FROM
                        pat_visit_info pvi
                            join
                        pat_reg_info pri ON pvi.patient_id = pri.patient_id
                            left join
                        cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                    where
                        pvi.visit_number = '" + visit_number + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printRegistationBill(string reg_bill_number)
        {
            qry = @"SELECT 
                        rbi.bill_number,
                        date_format(rbi.bill_date, '%d/%m/%Y %h:%i %p') as 'bill_date',
                        rbi.patient_id,
                        pri.patient_name,
                        concat(pri.age_year,
                                'Y ',
                                pri.age_month,
                                'M ',
                                pri.age_day,
                                'D') as age,
                        IF(pri.gender = 0, 'Male', 'Female') AS gender,
                        pvi.token_number,
                        pvi.doctor_name,
                        cc.corporate_name,
                        rbi.bill_amount,
                        rbi.discount,
                        rbi.due,
                        (rbi.amount_paid + rbi.due_collection) as amount_paid,
                        rbdi.account_head_name,
                        rbdi.bill_amount as reg_charges
                    FROM
                        reg_bill_info rbi
                            join
                        reg_bill_detail_info rbdi ON rbi.reg_bill_id = rbdi.bill_id
                            left join
                        pat_reg_info pri ON rbi.patient_id = pri.patient_id
                            left join
                        pat_visit_info pvi ON rbi.visit_number = pvi.visit_number
                            left join
                        cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                    where
                        rbdi.account_type = 1
                            and bill_number = '" + reg_bill_number + "' and rbi.status<>4";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printInvestigationBill(string inv_bill_number)
        {
            qry = @"SELECT 
                        ibi.bill_number,
                        date_format(ibi.bill_date, '%d/%m/%Y %h:%i %p') as 'bill_date',
                        ibi.patient_id,
                        ibi.patient_name,
                        IF(ibi.age_year > 0,
                            CONCAT(ibi.age_year, ' Y'),
                            IF(ibi.age_month > 0,
                                CONCAT(ibi.age_month, ' M'),
                                IF(ibi.age_day > 0,
                                    CONCAT(ibi.age_day, ' D'),
                                    CONCAT(0, ' D')))) AS age,
                        IF(ibi.gender = 0, 'Male', 'Female') AS gender,
                        ibi.doctor_name,
                        cc.corporate_name,
                        cil.investigation_name,
                        cd.department_name,
                        ibdi.qty,
                        ibdi.unit_price,
                        ibdi.amount,
                        ibi.bill_amount,
                        ibi.discount,
                        ibi.due,
                        (ibi.amount_paid + ibi.due_collection) as amount_paid
                    FROM
                        inv_bill_info ibi
                            join
                        inv_bill_datail_info ibdi ON ibi.bill_id = ibdi.bill_id
                            JOIN
                        cis_investigation_list cil ON ibdi.investigation_id = cil.investigation_id
                            JOIN
                        cis_department cd ON ibdi.department_id = cd.department_id
                            left join
                        pat_visit_info pvi ON ibi.visit_number = pvi.visit_number
                            left join
                        cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                    where
                        ibdi.status <> 4
                            and ibdi.account_type = 1
                            and ibi.bill_number = '" + inv_bill_number + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printPharmacyBill(string pha_bill_number)
        {
            qry = @"SELECT 
                        pbi.bill_number,
                        date_format(pbi.bill_date, '%d/%m/%Y %h:%i %p') as 'bill_date',
                        pbi.patient_id,
                        pbi.patient_name,
                        concat(pbi.age_year,
                                'Y ',
                                pbi.age_month,
                                'M ',
                                pbi.age_day,
                                'D') as age,
                        IF(pbi.gender = 0, 'Male', 'Female') AS gender,
                        pbi.doctor_name,
                        cc.corporate_name,
                        pit.item_type,
                        pi.item_name,
                        pi.hsn_code,
                        pbdi.tax_perc,
                        pbdi.lot_id,
                        pbdi.expiry_date,
                        pbdi.trans_qty,
                        pbdi.unit_price,
                        pbdi.free_care,
                        pbdi.net_total_amount,
                        pbi.bill_amount,
                        pbi.discount,
                        pbi.total_free_care,
                        (pbi.amount_paid + pbi.due_collection) as amount_paid,
                        pbi.due
                    FROM
                        pha_bill_info pbi
                            join
                        pha_bill_detail_info pbdi ON pbi.bill_id = pbdi.bill_id
                            join
                        cis_pha_item pi ON pbdi.item_id = pi.item_id
                            join
                        cis_pha_item_type pit ON pbdi.item_type_id = pit.item_type_id
                            left join
                        pat_visit_info pvi ON pbi.visit_number = pvi.visit_number
                            left join
                        cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                    where
                        pbdi.trans_type = 1 and pbdi.status = 0
                            and pbi.status <> 4
                            and pbi.bill_number = '" + pha_bill_number + "' order by pbdi.bill_detail_id";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printRefundPharmacyBill(string pha_bill_number)
        {
            qry = @"SELECT 
                        pit.item_type,
                        pi.item_name,
                        pi.hsn_code,
                        pbdi.tax_perc,
                        pbdi.lot_id,
                        pbdi.expiry_date,
                        pbdi.refund_qty,
                        pbdi.unit_price,
                        pbdi.ref_free_care,
                        cast(pbdi.refund_amt - pbdi.ref_free_care as decimal(10,2)) as refund_amt
                    FROM
                        pha_bill_info pbi
                            join
                        pha_bill_detail_info pbdi ON pbi.bill_id = pbdi.bill_id
                            join
                        cis_pha_item pi ON pbdi.item_id = pi.item_id
                            join
                        cis_pha_item_type pit ON pbdi.item_type_id = pit.item_type_id
                    where
                        pbdi.trans_type = 1 
                            and pbi.status <> 4
                            and pbdi.refund_qty > 0
                            and pbi.bill_number = '" + pha_bill_number + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printGeneralBill(string gen_bill_number)
        {
            qry = @"SELECT 
                        gbi.bill_number,
                        date_format(gbi.bill_date, '%d/%m/%Y %h:%i %p') as 'bill_date',
                        gbi.patient_id,
                        gbi.patient_name,
                        cah.account_head_name,
                        gbdi.qty,
                        gbdi.unit_price,
                        gbdi.amount,
                        gbi.bill_amount,
                        gbi.discount,
                        gbi.due,
                        (gbi.amount_paid + gbi.due_collection) as amount_paid
                    FROM
                        gen_bill_info gbi
                            join
                        gen_bill_detail_info gbdi ON gbi.gen_bill_id = gbdi.bill_id
                            JOIN
                        cis_account_head cah ON gbdi.account_head_id = cah.id_cis_account_head
                    where
                        gbdi.status <> 4
                            AND gbdi.account_type_id = 1
                            and gbi.bill_number = '" + gen_bill_number + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printRefundGeneralBill(string gen_bill_number)
        {
            qry = @"SELECT 
                        cah.account_head_name,
                        gbdi.refund_qty,
                        gbdi.unit_price,
                        gbdi.refund_amt
                    FROM
                        gen_bill_info gbi
                            join
                        gen_bill_detail_info gbdi ON gbi.gen_bill_id = gbdi.bill_id
                            JOIN
                        cis_account_head cah ON gbdi.account_head_id = cah.id_cis_account_head
                    where
                        gbdi.status = 4
                            AND gbdi.account_type_id = 1
                            and gbi.bill_number = '" + gen_bill_number + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printWardBill(string ward_bill_number)
        {
            qry = @"select 
                        pri.patient_id as patient_id,
                        pri.patient_name,
                        if(pri.gender = 1, 'Female', 'Male') as gender_name,
                        concat(pri.age_year,
                                'Y ',
                                pri.age_month,
                                'M ',
                                pri.age_day,
                                'D') as age,
                        guardian_name,
                        (select 
                                c.corporate_name
                            from
                                cis_corporate c
                            where
                                c.corporate_id = pvi.corporate_id
                            limit 1) as corporate_name,
                        date_format(pvi.visit_date, '%d/%m/%Y %h:%i %p') as visit_date,
                        date_format(wbi.from_date, '%d/%m/%Y %h:%i %p') as from_date,
                        date_format(wbi.to_date, '%d/%m/%Y %h:%i %p') as to_date,
                        concat((select 
                                        DEPARTMENT_NAME
                                    from
                                        cis_department
                                    where
                                        department_id = pbi.ward_id
                                    limit 1),
                                ', ',
                                (select 
                                        room_no
                                    from
                                        cis_room
                                    where
                                        room_id = pbi.room_id
                                    limit 1),
                                ', ',
                                (select 
                                        bed_number
                                    from
                                        cis_bed
                                    where
                                        bed_id = pbi.bed_id
                                    limit 1)) as 'ward_details',
                        wbi.bill_number,
                        date_format(wbi.bill_date, '%d/%m/%Y %h:%i %p') as bill_date,
                        wbi.bill_type,
                        if(wbi.bill_type = 1,
                            'Final Ward Bill',
                            'Intermediate Ward Bill') as 'bill_type_name',
                        wbdi.account_code,
                        wbdi.quantity,
                        wbdi.unit_price,
                        wbdi.amount,
                        wbi.bill_amount,
                        wbi.discount,
                        wbi.total_amount,
                        (wbi.amount_paid + wbi.due_collection) as 'amount_paid',
                        wbi.pay_from_advance,
                        wbi.due
                    from
                        ward_bill_info wbi
                            join
                        ward_bill_detail_info wbdi ON wbi.bill_id = wbdi.bill_id
                            join
                        pat_visit_info pvi ON wbi.visit_number = pvi.visit_number
                            join
                        pat_reg_info pri ON pvi.patient_id = pri.patient_id
                            left join
                        pat_bed_info pbi ON wbi.visit_number = pbi.visit_number
                    where
                        wbi.status <> 4
                            and wbdi.account_transaction_type = 1
                            and wbi.bill_number = '" + ward_bill_number + "' order by id_pat_bed_info desc limit 1";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printAdvanceBill(string adv_bill_number)
        {
            qry = @"SELECT 
                        pri.patient_id,
                        pri.patient_name,
                        if(pri.gender = 1, 'Female', 'Male') as gender_name,
                        concat(pri.age_year,
                                'Y ',
                                pri.age_month,
                                'M ',
                                pri.age_day,
                                'D') as age,
                        concat((select 
                                        DEPARTMENT_NAME
                                    from
                                        cis_department
                                    where
                                        department_id = cb.ward_id
                                    limit 1),
                                ', ',
                                (select 
                                        room_no
                                    from
                                        cis_room
                                    where
                                        room_id = cb.room_id
                                    limit 1),
                                ', ',
                                cb.bed_number) as 'ward_details',
                        account_type,
                        case
                            when account_type = 1 then 'Advance Collection'
                            when account_type = 2 then 'Advance Adjustment'
                            when account_type = 3 then 'Advance Refund'
                            when account_type = 4 then 'Advance In'
                        END AS 'transaction_type',
                        receipt_number,
                        date_format(ca.trans_date, '%d/%m/%Y %h:%i %p') as bill_date,
                        ca.transaction_amount
                    FROM
                        cis_advance ca
                            join
                        pat_reg_info pri ON ca.patient_id = pri.patient_id
                            left join
                        cis_bed cb ON pri.patient_id = cb.patient_id
                    where
                        receipt_number = '" + adv_bill_number + "'";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        //Consolidated Bill Details Start
        public DataTable printConsolidatedInvBill(string patient_id, string visit_number)
        {
            qry = @"SELECT 
                        bill_number,
                        date_format(bill_date, '%d/%m/%Y') as 'bill_date',
                        bill_amount,
                        discount,
                        (amount_paid + due_collection + pay_from_advance) as amount_paid,
                        due
                    FROM
                        inv_bill_info
                    where
                        patient_id = '" + patient_id + "' and visit_number = '" + visit_number + "'  and status <> 4 order by bill_date asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printConsolidatedPhaBill(string patient_id, string visit_number)
        {
            qry = @"SELECT 
                        bill_number,
                        date_format(bill_date, '%d/%m/%Y') as 'bill_date',
                        bill_amount,
                        (total_free_care + discount) as disount,
                        (amount_paid + due_collection + pay_from_advance) as amount_paid,
                        due
                    FROM
                        pha_bill_info
                    where
                        patient_id = '" + patient_id + "' and visit_number = '" + visit_number + "'  and status <> 4 order by bill_date asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printConsolidatedGenBill(string patient_id, string visit_number)
        {
            qry = @"SELECT 
                        bill_number,
                        date_format(bill_date, '%d/%m/%Y') as 'bill_date',
                        bill_amount,
                        discount,
                        (amount_paid + due_collection + pay_from_advance) as amount_paid,
                        due
                    FROM
                        gen_bill_info
                    where
                        patient_id = '" + patient_id + "' and visit_number = '" + visit_number + "'  and status <> 4 order by bill_date asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printConsolidatedInterWardBill(string patient_id, string visit_number)
        {
            qry = @"SELECT 
                        bill_number,
                        date_format(bill_date, '%d/%m/%Y') as 'bill_date',
                        bill_amount,
                        discount,
                        (amount_paid + due_collection + pay_from_advance) as amount_paid,
                        due
                    FROM
                        ward_bill_info
                    where
                        patient_id = '" + patient_id + "' and visit_number = '" + visit_number + "' and bill_type = 2 and status <> 4 order by bill_date asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        //Consolidated Bill Details End
        #endregion

        #region Report Info
        public DataTable viewRegCensusMedDepartmentByGender(ComArugments objArg) // Total Medical Department and Gender Patient Census
        {
            qry = @"SELECT 
                        T.visit_date AS Visit_Date,
                        T.Department_Name,
                        SUM(T.Male) AS Total_Male,
                        SUM(T.Female) AS Total_Female,
                        SUM(T.Child) AS Total_Child,
                        (SUM(T.Male) + SUM(T.Female) + SUM(T.Child)) AS Total_Patients
                    FROM
                        (SELECT 
                            DATE_FORMAT(visit_date, '%d/%m/%Y') AS visit_date,
                                cd.department_name,
                                CASE
                                    WHEN pri.gender = 0 AND pri.age_year > 12 THEN COUNT(*)
                                    ELSE 0
                                END AS 'Male',
                                CASE
                                    WHEN pri.gender = 1 AND pri.age_year > 12 THEN COUNT(*)
                                    ELSE 0
                                END AS 'Female',
                                CASE
                                    WHEN age_year <= 12 THEN COUNT(*)
                                    ELSE 0
                                END AS 'Child'
                        FROM
                            pat_visit_info pvi
                        LEFT JOIN cis_department cd ON pvi.medical_department_id = cd.DEPARTMENT_ID
                        JOIN pat_reg_info pri ON pvi.patient_id = pri.patient_id
                        WHERE
                            pvi.visit_date BETWEEN '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' AND pvi.visit_type = " + Convert.ToInt32(objArg.ParamList["visitMode"]) + " AND pvi.STATUS = 1 GROUP BY DATE_FORMAT(visit_date, '%d/%m/%Y') , cd.department_name , pri.gender) AS T GROUP BY T.visit_date , T.department_name ORDER BY STR_TO_DATE(Visit_Date, '%d/%m/%Y') ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewRegCensusMonthVisitModeByGender(ComArugments objArg) // Total M.Department, Gender and Visit Mode Patient Census - Montly
        {
            qry = @"SELECT 
                        Event_Year AS Year,
                        MONTHNAME(STR_TO_DATE(Event_Month, '%m')) AS Month,
                        T.Department_Name,
                        SUM(T.Male_New) AS Total_Male_New,
                        SUM(T.Male_Revisit) AS Total_Male_Revisit,
                        (SUM(T.Male_New) + SUM(T.Male_Revisit)) AS Net_Total_Male,
                        SUM(T.Female_New) AS Total_Female_New,
                        SUM(T.Female_Revisit) AS Total_Female_Revisit,
                        (SUM(T.Female_New) + SUM(T.Female_Revisit)) AS Net_Total_Female,
                        SUM(T.Child_New) AS Total_Child_New,
                        SUM(T.Child_Revisit) AS Total_Child_Revisit,
                        (SUM(T.Child_New) + SUM(T.Child_Revisit)) AS Net_Total_Child,
                        (SUM(T.Male_New) + SUM(T.Male_Revisit) + SUM(T.Female_New) + SUM(T.Female_Revisit) + SUM(T.Child_New) + SUM(T.Child_Revisit)) AS Grand_Total
                    FROM
                        (SELECT 
                            YEAR(visit_date) AS Event_Year,
                                MONTH(visit_date) AS Event_Month,
                                cd.department_name,
                                CASE
                                    WHEN
                                        pri.gender = 0 AND pri.age_year > 12
                                            AND pvi.visit_mode = 0
                                    THEN
                                        COUNT(*)
                                    ELSE 0
                                END AS 'Male_New',
                                CASE
                                    WHEN
                                        pri.gender = 0 AND pri.age_year > 12
                                            AND pvi.visit_mode = 1
                                    THEN
                                        COUNT(*)
                                    ELSE 0
                                END AS 'Male_Revisit',
                                CASE
                                    WHEN
                                        pri.gender = 1 AND pri.age_year > 12
                                            AND pvi.visit_mode = 0
                                    THEN
                                        COUNT(*)
                                    ELSE 0
                                END AS 'Female_New',
                                CASE
                                    WHEN
                                        pri.gender = 1 AND pri.age_year > 12
                                            AND pvi.visit_mode = 1
                                    THEN
                                        COUNT(*)
                                    ELSE 0
                                END AS 'Female_Revisit',
                                CASE
                                    WHEN age_year <= 12 AND pvi.visit_mode = 0 THEN COUNT(*)
                                    ELSE 0
                                END AS 'Child_New',
                                CASE
                                    WHEN age_year <= 12 AND pvi.visit_mode = 1 THEN COUNT(*)
                                    ELSE 0
                                END AS 'Child_Revisit'
                        FROM
                            pat_visit_info pvi
                        LEFT JOIN cis_department cd ON pvi.medical_department_id = cd.DEPARTMENT_ID
                        JOIN pat_reg_info pri ON pri.patient_id = pvi.patient_id
                        WHERE
                            pvi.visit_date BETWEEN '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' AND pvi.visit_type = " + Convert.ToInt32(objArg.ParamList["visitMode"]) + " AND pvi.STATUS = 1 GROUP BY YEAR(visit_date) , MONTH(visit_date) , cd.department_name , pri.gender) AS T GROUP BY Event_Year , Event_Month , T.department_name";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewOPRegistationList(ComArugments objArg) 
        {
            qry = @"SELECT 
                        pvi.patient_id,
                        pri.patient_name,
                        date_format(pvi.visit_date, '%d/%m/%Y %h:%i %p') as visit_date,
                        if(pri.gender = 1, 'Female', 'Male') as gender,
                        IF(age_year > 0,
                            CONCAT(age_year, ' Y'),
                            IF(age_month > 0,
                                CONCAT(age_month, ' M'),
                                IF(age_day > 0,
                                    CONCAT(age_day, ' D'),
                                    CONCAT(0, ' D')))) AS age,
                        if(pvi.status = 1,
                            if(pvi.visit_mode = 0, 'New', 'Revisit'),
                            'Cancelled') as visit_mode,
                        pvi.doctor_name as doctor
                    FROM
                        pat_visit_info pvi
                            JOIN
                        pat_reg_info pri ON pvi.patient_id = pri.patient_id
                    where
                            pvi.visit_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and pvi.visit_type = " + Convert.ToInt32(objArg.ParamList["visitMode"]) + " AND pvi.STATUS = 1 order by pvi.visit_date asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewIPAdmissionList(ComArugments objArg)
        {
            qry = @"SELECT 
                        pvi.patient_id,
                        pri.patient_name,
                        date_format(pvi.visit_date, '%d/%m/%Y %h:%i %p') as visit_date,
                        if(pri.gender = 1, 'Female', 'Male') as gender,
                        IF(age_year > 0,
                            CONCAT(age_year, ' Y'),
                            IF(age_month > 0,
                                CONCAT(age_month, ' M'),
                                IF(age_day > 0,
                                    CONCAT(age_day, ' D'),
                                    CONCAT(0, ' D')))) AS age,
                        if(pvi.status = 1,
                            if(pvi.visit_mode = 0, 'New', 'Revisit'),
                            'Cancelled') as visit_mode,
                        pvi.doctor_name as doctor,
                        concat(cd.DEPARTMENT_NAME,
                                ', ',
                                cr.room_no,
                                ', ',
                                cb.bed_number) as ward_details
                    FROM
                        pat_visit_info pvi
                            JOIN
                        pat_reg_info pri ON pvi.patient_id = pri.patient_id
                            LEFT JOIN
                        cis_department cd ON pvi.ward_id = cd.department_id
                            left join
                        cis_room cr ON pvi.room_id = cr.room_id
                            LEFT JOIN
                        cis_bed cb ON pvi.bed_id = cb.bed_id
                    where
                        pvi.visit_type = 2 AND pvi.STATUS = 1
                           and pvi.visit_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by pvi.visit_date asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewIPDischargeList(ComArugments objArg)
        {
            qry = @"SELECT 
                        pri.patient_id,
                        pri.patient_name,
                        if(pri.gender = 1, 'Female', 'Male') as gender,
                        IF(age_year > 0,
                            CONCAT(age_year, ' Y'),
                            IF(age_month > 0,
                                CONCAT(age_month, ' M'),
                                IF(age_day > 0,
                                    CONCAT(age_day, ' D'),
                                    CONCAT(0, ' D')))) AS age,
                        date_format(pvi.visit_date, '%d/%m/%Y %h:%i %p') as visit_date,
                        pbi.end_date,
                        concat(cd.DEPARTMENT_NAME,
                                ', ',
                                cr.room_no,
                                ', ',
                                cb.bed_number) as ward_details
                    FROM
                        pat_bed_info pbi
                            left join
                        pat_visit_info pvi ON pbi.visit_number = pvi.visit_number
                            left join
                        pat_reg_info pri ON pvi.patient_id = pri.patient_id
                            left JOIN
                        cis_department cd ON pbi.ward_id = cd.department_id
                            left join
                        cis_room cr ON pbi.room_id = cr.room_id
                            LEFT JOIN
                        cis_bed cb ON pbi.bed_id = cb.bed_id
                    where
                        pbi.bed_status = 4
                           and pbi.end_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by pbi.end_date asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewCurrentIPList()
        {
            qry = @"SELECT 
                        pri.patient_id,
                        pri.patient_name,
                        if(pri.gender = 1, 'Female', 'Male') as gender,
                        IF(age_year > 0,
                            CONCAT(age_year, ' Y'),
                            IF(age_month > 0,
                                CONCAT(age_month, ' M'),
                                IF(age_day > 0,
                                    CONCAT(age_day, ' D'),
                                    CONCAT(0, ' D')))) AS age,
                        date_format(pvi.visit_date, '%d/%m/%Y %h:%i %p') as visit_date,
                        concat(cd.DEPARTMENT_NAME,
                                ', ',
                                cr.room_no,
                                ', ',
                                cb.bed_number) as ward_details
                    FROM
                        cis_bed cb
                            left join
                        pat_visit_info pvi ON cb.visit_number = pvi.visit_number
                            left join
                        pat_reg_info pri ON pvi.patient_id = pri.patient_id
                            left JOIN
                        cis_department cd ON cb.ward_id = cd.department_id
                            left join
                        cis_room cr ON cb.room_id = cr.room_id
                    where
                        cb.patient_id is not null
                            and cb.patient_id != ''
                            and cb.visit_number is not null
                            and cb.visit_number != ''
                            and cb.status = 1
                    order by cb.ward_id , cr.room_no , cb.bed_number";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable DailyCollection(ComArugments objArg)
        {
            switch (Convert.ToInt32(objArg.ParamList["departmentId"]))
            {
                case 1:
                case 2:
                    qry = @"SELECT 
                                T.patient_id,
                                T.bill_number,
                                DATE_FORMAT(T.transaction_date, '%d/%m/%Y') as transaction_date,
                                T.bill_amount,
                                T.discount,
                                T.amount_paid,
                                T.due_collection,
                                T.due,
                                T.refund_to_patient,
                                T.net_collection
                            from
                                (SELECT 
                                    rbi.patient_id,
                                        rbi.bill_number,
                                        rbdi.transaction_date,
                                        rbdi.bill_amount,
                                        sum(rbdi.discount) as discount,
                                        sum(rbdi.amount_paid) as amount_paid,
                                        sum(rbdi.due_collection) as due_collection,
                                        min(rbdi.due) as due,
                                        sum(rbdi.refund_to_patient) as refund_to_patient,
                                        (sum(rbdi.amount_paid) + sum(rbdi.due_collection) - sum(rbdi.refund_to_patient)) as net_collection
                                FROM
                                    reg_bill_detail_info rbdi
                                join reg_bill_info rbi ON rbdi.bill_id = rbi.reg_bill_id
                                where
                                    rbdi.account_type = 2
                                and rbi.trans_dept_id = " + Convert.ToInt32(objArg.ParamList["departmentId"]) + " and rbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') , rbdi.bill_id) as T HAVING (T.bill_amount + T.discount + T.due + T.net_collection) <> 0 ORDER BY T.transaction_date ASC";
                    break;

                case 3:
                    qry = @"SELECT 
                                *
                            from
                                (SELECT 
                                    ibi.patient_id,
                                        ibi.patient_name,
                                        ibi.bill_number,
                                        DATE_FORMAT(ibdi.transaction_date, '%d/%m/%Y') as transaction_date,
                                        ibdi.total as bill_amount,
                                        sum(ibdi.discount) as discount,
                                        sum(ibdi.amount_paid) as amount_paid,
                                        sum(ibdi.due_collection) as due_collection,
                                        min(ibdi.due) as due,
                                        sum(ibdi.refund_to_patient) as refund_to_patient,
                                        (sum(ibdi.amount_paid) + sum(ibdi.due_collection) - sum(ibdi.refund_to_patient)) as net_collection
                                FROM
                                    inv_bill_datail_info ibdi
                                join inv_bill_info ibi ON ibdi.bill_id = ibi.bill_id
                                where
                                    ibdi.account_type = 2
                                        and ibdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(ibdi.transaction_date, '%d/%m/%Y') , ibdi.bill_id) as T HAVING (T.bill_amount + T.discount + T.due + T.net_collection) <> 0 ORDER BY STR_TO_DATE(transaction_date, '%d/%m/%Y') ASC";
                    break;

                case 4:
                    qry = @"SELECT 
                                *
                            from
                                (SELECT 
                                    pbi.patient_id,
                                        pbi.patient_name,
                                        pbi.bill_number,
                                        DATE_FORMAT(pbdi.transaction_date, '%d/%m/%Y') as transaction_date,
                                        pbdi.total_amount as bill_amount,
                                        sum(pbdi.discount) as discount,
                                        sum(pbdi.amount_paid) as amount_paid,
                                        sum(pbdi.due_collection) as due_collection,
                                        min(pbdi.due) as due,
                                        sum(pbdi.refund_to_patient) as refund_to_patient,
                                        (sum(pbdi.amount_paid) + sum(pbdi.due_collection) - sum(pbdi.refund_to_patient)) as net_collection
                                FROM
                                    pha_bill_detail_info pbdi
                                join pha_bill_info pbi ON pbdi.bill_id = pbi.bill_id
                                where
                                    pbdi.trans_type = 2
                                        and pbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(pbdi.transaction_date, '%d/%m/%Y') , pbdi.bill_id) as T HAVING (T.bill_amount + T.discount + T.due + T.net_collection) <> 0 ORDER BY STR_TO_DATE(transaction_date, '%d/%m/%Y') ASC";
                    break;

                case 7:
                    qry = @"SELECT 
                                *
                            from
                                (SELECT 
                                    gbi.patient_id,
                                        gbi.patient_name,
                                        gbi.bill_number,
                                        DATE_FORMAT(gbdi.transaction_date, '%d/%m/%Y') as transaction_date,
                                        gbdi.total_amount as bill_amount,
                                        sum(gbdi.discount) as discount,
                                        sum(gbdi.amount_paid) as amount_paid,
                                        sum(gbdi.due_collection) as due_collection,
                                        min(gbdi.due) as due,
                                        sum(gbdi.refund_to_patient) as refund_to_patient,
                                        (sum(gbdi.amount_paid) + sum(gbdi.due_collection) - sum(gbdi.refund_to_patient)) as net_collection
                                FROM
                                    gen_bill_detail_info gbdi
                                join gen_bill_info gbi ON gbdi.bill_id = gbi.gen_bill_id
                                where
                                    gbdi.account_type_id = 2
                                        and gbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(gbdi.transaction_date, '%d/%m/%Y') , gbdi.bill_id) as T HAVING (T.bill_amount + T.discount + T.due + T.net_collection) <> 0 ORDER BY STR_TO_DATE(transaction_date, '%d/%m/%Y') ASC";
                    break;

                case 10:
                    qry = @"SELECT * FROM (SELECT T.patient_id, T.bill_number, T.transaction_date, T.bill_amount, T.discount, T.amount_paid, T.due_collection, T.due, T.refund_to_patient, T.net_collection from (SELECT     rbi.patient_id,     rbi.bill_number,     DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') as transaction_date,     rbdi.bill_amount,     sum(rbdi.discount) as discount,     sum(rbdi.amount_paid) as amount_paid,     sum(rbdi.due_collection) as due_collection,     min(rbdi.due) as due,     sum(rbdi.refund_to_patient) as refund_to_patient,     (sum(rbdi.amount_paid) + sum(rbdi.due_collection) - sum(rbdi.refund_to_patient)) as net_collection FROM     reg_bill_detail_info rbdi join reg_bill_info rbi ON rbdi.bill_id = rbi.reg_bill_id where     rbdi.account_type = 2     and rbi.trans_dept_id = 1     and rbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') , rbdi.bill_id) as T HAVING (T.bill_amount + T.discount + T.due + T.net_collection) <> 0 UNION SELECT T.patient_id, T.bill_number, T.transaction_date, T.bill_amount, T.discount, T.amount_paid, T.due_collection, T.due, T.refund_to_patient, T.net_collection from (SELECT     rbi.patient_id,     rbi.bill_number,     DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') as transaction_date,     rbdi.bill_amount,     sum(rbdi.discount) as discount,     sum(rbdi.amount_paid) as amount_paid,     sum(rbdi.due_collection) as due_collection,     min(rbdi.due) as due,     sum(rbdi.refund_to_patient) as refund_to_patient,     (sum(rbdi.amount_paid) + sum(rbdi.due_collection) - sum(rbdi.refund_to_patient)) as net_collection FROM     reg_bill_detail_info rbdi join reg_bill_info rbi ON rbdi.bill_id = rbi.reg_bill_id where     rbdi.account_type = 2     and rbi.trans_dept_id = 2     and rbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') , rbdi.bill_id) as T HAVING (T.bill_amount + T.discount + T.due + T.net_collection) <> 0 UNION SELECT T.patient_id, T.bill_number, T.transaction_date, T.bill_amount, T.discount, T.amount_paid, T.due_collection, T.due, T.refund_to_patient, T.net_collection from (SELECT     ibi.patient_id,     ibi.patient_name,     ibi.bill_number,     DATE_FORMAT(ibdi.transaction_date, '%d/%m/%Y') as transaction_date,     ibdi.total as bill_amount,     sum(ibdi.discount) as discount,     sum(ibdi.amount_paid) as amount_paid,     sum(ibdi.due_collection) as due_collection,     min(ibdi.due) as due,     sum(ibdi.refund_to_patient) as refund_to_patient,     (sum(ibdi.amount_paid) + sum(ibdi.due_collection) - sum(ibdi.refund_to_patient)) as net_collection FROM     inv_bill_datail_info ibdi join inv_bill_info ibi ON ibdi.bill_id = ibi.bill_id where     ibdi.account_type = 2     and ibdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(ibdi.transaction_date, '%d/%m/%Y') , ibdi.bill_id) as T HAVING (T.bill_amount + T.discount + T.due + T.net_collection) <> 0 UNION SELECT T.patient_id, T.bill_number, T.transaction_date, T.bill_amount, T.discount, T.amount_paid, T.due_collection, T.due, T.refund_to_patient, T.net_collection from (SELECT     pbi.patient_id,     pbi.patient_name,     pbi.bill_number,     DATE_FORMAT(pbdi.transaction_date, '%d/%m/%Y') as transaction_date,     pbdi.total_amount as bill_amount,     sum(pbdi.discount) as discount,     sum(pbdi.amount_paid) as amount_paid,     sum(pbdi.due_collection) as due_collection,     min(pbdi.due) as due,     sum(pbdi.refund_to_patient) as refund_to_patient,     (sum(pbdi.amount_paid) + sum(pbdi.due_collection) - sum(pbdi.refund_to_patient)) as net_collection FROM     pha_bill_detail_info pbdi join pha_bill_info pbi ON pbdi.bill_id = pbi.bill_id where     pbdi.trans_type = 2     and pbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(pbdi.transaction_date, '%d/%m/%Y') , pbdi.bill_id) as T HAVING (T.bill_amount + T.discount + T.due + T.net_collection) <> 0 UNION SELECT T.patient_id, T.bill_number, T.transaction_date, T.bill_amount, T.discount, T.amount_paid, T.due_collection, T.due, T.refund_to_patient, T.net_collection from (SELECT     gbi.patient_id,     gbi.patient_name,     gbi.bill_number,     DATE_FORMAT(gbdi.transaction_date, '%d/%m/%Y') as transaction_date,     gbdi.total_amount as bill_amount,     sum(gbdi.discount) as discount,     sum(gbdi.amount_paid) as amount_paid,     sum(gbdi.due_collection) as due_collection,     min(gbdi.due) as due,     sum(gbdi.refund_to_patient) as refund_to_patient,     (sum(gbdi.amount_paid) + sum(gbdi.due_collection) - sum(gbdi.refund_to_patient)) as net_collection FROM     gen_bill_detail_info gbdi join gen_bill_info gbi ON gbdi.bill_id = gbi.gen_bill_id where     gbdi.account_type_id = 2     and gbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(gbdi.transaction_date, '%d/%m/%Y') , gbdi.bill_id) as T HAVING (T.bill_amount + T.discount + T.due + T.net_collection) <> 0 ) AS T2 ORDER BY STR_TO_DATE(T2.transaction_date, '%d/%m/%Y') ASC";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(qry))
            {
                dtSource = objDBHandler.ExecuteTable(qry); 
            }
            return dtSource;
        }

        public DataTable DueList(ComArugments objArg)
        {
            switch (Convert.ToInt32(objArg.ParamList["departmentId"]))
            {
                case 1:
                case 2:
                    if (Convert.ToInt32(objArg.ParamList["corporateId"]) > 0)
                    {
                        qry = @"select 
                                'Registration' as department,
                                rbi.bill_number,
                                date_format(rbi.bill_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',
                                rbi.patient_id,
                                pri.patient_name,
                                rbi.bill_amount,
                                rbi.discount,
                                (rbi.amount_paid + rbi.due_collection) as 'amount_paid',
                                rbi.due
                            FROM
                                reg_bill_info rbi
                                    left join
                                pat_reg_info pri ON rbi.patient_id = pri.patient_id
                                    join
                                pat_visit_info pvi ON rbi.visit_number = pvi.visit_number
                                    join
                                cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                            where
                                due > 0 
                                and rbi.trans_dept_id = " + Convert.ToInt32(objArg.ParamList["departmentId"]) + " and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " order by rbi.bill_date ASC";
                    }
                    else
                    {
                        qry = @"SELECT 
                                'Registration' as department,
                                rbi.bill_number,
                                date_format(rbi.bill_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',
                                rbi.patient_id,
                                pri.patient_name,
                                rbi.bill_amount,
                                rbi.discount,
                                (rbi.amount_paid + rbi.due_collection) as 'amount_paid',
                                rbi.due
                            FROM
                                reg_bill_info rbi
                                    left join
                                pat_reg_info pri ON rbi.patient_id = pri.patient_id
                            where
                                due > 0 
                                and rbi.trans_dept_id = " + Convert.ToInt32(objArg.ParamList["departmentId"]) + " and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by rbi.bill_date ASC";
                    }
                    break;

                case 3:
                    if (Convert.ToInt32(objArg.ParamList["corporateId"]) > 0)
                    {
                        qry = @"SELECT 
                                    'Investigation' as department,
                                    ibi.bill_number,
                                    date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',
                                    ibi.patient_id,
                                    ibi.patient_name,
                                    ibi.bill_amount,
                                    ibi.discount,
                                    (ibi.amount_paid + ibi.due_collection + ibi.pay_from_advance) as 'amount_paid',
                                    ibi.due
                                FROM
                                    inv_bill_info ibi
                                        join
                                    pat_visit_info pvi ON ibi.visit_number = pvi.visit_number
                                        join
                                    cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                                where
                                    ibi.due > 0
                                and ibi.bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " order by ibi.bill_date ASC";
                    }
                    else
                    {
                        qry = @"SELECT 
                                'Investigation' as department,
                                bill_number,
                                date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',
                                patient_id,
                                patient_name,
                                bill_amount,
                                discount,
                                (amount_paid + due_collection + pay_from_advance) as 'amount_paid',
                                due
                            FROM
                                inv_bill_info
                            where
                                due > 0
                                and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by bill_date ASC";

                    }
                    break;

                case 4:
                    if (Convert.ToInt32(objArg.ParamList["corporateId"]) > 0)
                    {
                        qry = @"SELECT 
                                    'Pharmacy' as department,
                                    pbi.bill_number,
                                    pbi.bill_date as 'transaction_date',
                                    pbi.patient_id,
                                    pbi.patient_name,
                                    pbi.bill_amount,
                                    pbi.discount,
                                    (pbi.amount_paid + pbi.due_collection + pbi.pay_from_advance) as 'amount_paid',
                                    due
                                FROM
                                    pha_bill_info pbi
                                        join
                                    pat_visit_info pvi ON pbi.visit_number = pvi.visit_number
                                        join
                                    cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                                where
                                    pbi.due > 0
                                and pbi.bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " order by pbi.bill_date ASC";
                    }

                    else
                    {
                        qry = @"SELECT 
                                'Pharmacy' as department,
                                bill_number,
                                date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',
                                patient_id,
                                patient_name,
                                bill_amount,
                                discount,
                                (amount_paid + due_collection + pay_from_advance) as 'amount_paid',
                                due
                            FROM
                                pha_bill_info
                            where
                                due > 0
                                and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by bill_date ASC";
                    }
                    break;

                case 7:
                    if (Convert.ToInt32(objArg.ParamList["corporateId"]) > 0)
                    {
                        qry = @"SELECT 
                                    'General' as department,
                                    gbi.bill_number,
                                                                date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',
                                    gbi.patient_id,
                                    gbi.patient_name,
                                    gbi.bill_amount,
                                    gbi.discount,
                                    (gbi.amount_paid + gbi.due_collection + gbi.pay_from_advance) as 'amount_paid',
                                    due
                                FROM
                                    gen_bill_info gbi
                                        join
                                    pat_visit_info pvi ON gbi.visit_number = pvi.visit_number
                                        join
                                    cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                                where
                                    gbi.due > 0
                                and gbi.bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " order by gbi.bill_date ASC";

                    }
                    else
                    {
                        qry = @"SELECT 
                                'General' as department,
                                bill_number,
                                date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',
                                patient_id,
                                patient_name,
                                bill_amount,
                                discount,
                                (amount_paid + due_collection + pay_from_advance) as 'amount_paid',
                                due
                            FROM
                                gen_bill_info
                            where
                                due > 0
                                and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by bill_date ASC";
                    }
                    break;

                case 8:
                    if (Convert.ToInt32(objArg.ParamList["corporateId"]) > 0)
                    {
                        qry = @"SELECT 
                                    'IP Billing' as department,
                                    wbi.bill_number,
                                                                date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',
                                    wbi.patient_id,
                                    pri.patient_name,
                                    wbi.bill_amount,
                                    wbi.discount,
                                    (wbi.amount_paid + wbi.due_collection + wbi.pay_from_advance) as 'amount_paid',
                                    wbi.due
                                FROM
                                    ward_bill_info wbi
                                        left join
                                    pat_reg_info pri ON wbi.patient_id = pri.patient_id
                                        join
                                    pat_visit_info pvi ON wbi.visit_number = pvi.visit_number
                                        join
                                    cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                                where
                                    wbi.due > 0
                                and wbi.bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'  and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " order by wbi.bill_date ASC";

                    }

                    else
                    {
                        qry = @"SELECT 
                                'IP Billing' as department,
                                bill_number,
                                date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',
                                wbi.patient_id,
                                pri.patient_name,
                                bill_amount,
                                discount,
                                (amount_paid + due_collection + pay_from_advance) as 'amount_paid',
                                due
                            FROM
                                ward_bill_info wbi
                                    left join
                                pat_reg_info pri ON wbi.patient_id = pri.patient_id
                            where
                                due > 0
                                and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by bill_date ASC";
                    }

                    break;

                case 10:
                    if (Convert.ToInt32(objArg.ParamList["corporateId"]) > 0)
                    {
                        qry = @"select    t.department,     t.bill_number,     date_format(t.transaction_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',     t.patient_id,     t.patient_name,     t.bill_amount,     t.discount,     t.amount_paid,     t.due from     (select        'Registration' as department,             rbi.bill_number,             rbi.bill_date as 'transaction_date',             rbi.patient_id,             pri.patient_name,             rbi.bill_amount,             rbi.discount,             (rbi.amount_paid + rbi.due_collection) as 'amount_paid',             rbi.due     FROM         reg_bill_info rbi     left join pat_reg_info pri ON rbi.patient_id = pri.patient_id     join pat_visit_info pvi ON rbi.visit_number = pvi.visit_number     join cis_corporate cc ON pvi.corporate_id = cc.corporate_id     where         due > 0 and rbi.trans_dept_id = 1             and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'             and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " union SELECT        'Investigation' as department,             ibi.bill_number,             ibi.bill_date as 'transaction_date',             ibi.patient_id,             ibi.patient_name,             ibi.bill_amount,             ibi.discount,             (ibi.amount_paid + ibi.due_collection + ibi.pay_from_advance) as 'amount_paid',             ibi.due     FROM         inv_bill_info ibi     join pat_visit_info pvi ON ibi.visit_number = pvi.visit_number     join cis_corporate cc ON pvi.corporate_id = cc.corporate_id     where         ibi.due > 0             and ibi.bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'             and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " union SELECT        'Pharmacy' as department,             pbi.bill_number,             pbi.bill_date as 'transaction_date',             pbi.patient_id,             pbi.patient_name,             pbi.bill_amount,             pbi.discount,             (pbi.amount_paid + pbi.due_collection + pbi.pay_from_advance) as 'amount_paid',             due     FROM         pha_bill_info pbi     join pat_visit_info pvi ON pbi.visit_number = pvi.visit_number     join cis_corporate cc ON pvi.corporate_id = cc.corporate_id     where         pbi.due > 0             and pbi.bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'             and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " union SELECT        'General' as department,             gbi.bill_number,             gbi.bill_date as 'transaction_date',             gbi.patient_id,             gbi.patient_name,             gbi.bill_amount,             gbi.discount,             (gbi.amount_paid + gbi.due_collection + gbi.pay_from_advance) as 'amount_paid',             due     FROM         gen_bill_info gbi     join pat_visit_info pvi ON gbi.visit_number = pvi.visit_number     join cis_corporate cc ON pvi.corporate_id = cc.corporate_id     where         gbi.due > 0             and gbi.bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'             and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " union SELECT        'IP Billing' as department,             wbi.bill_number,             wbi.bill_date as 'transaction_date',             wbi.patient_id,             pri.patient_name,             wbi.bill_amount,             wbi.discount,             (wbi.amount_paid + wbi.due_collection + wbi.pay_from_advance) as 'amount_paid',             wbi.due     FROM         ward_bill_info wbi     left join pat_reg_info pri ON wbi.patient_id = pri.patient_id     join pat_visit_info pvi ON wbi.visit_number = pvi.visit_number     join cis_corporate cc ON pvi.corporate_id = cc.corporate_id     where         wbi.due > 0             and wbi.bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'             and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + ") as t order by t.transaction_date asc ";
                    }
                    else
                    {
                        qry = @"select     t.department,     t.bill_number,     date_format(t.transaction_date, '%d/%m/%Y %h:%i %p') as 'transaction_date',     t.patient_id,     t.patient_name,     t.bill_amount,     t.discount,     t.amount_paid,     t.due FROM     (select         'Registration' as department,             rbi.bill_number,             rbi.bill_date as 'transaction_date',             rbi.patient_id,             pri.patient_name,             rbi.bill_amount,             rbi.discount,             (rbi.amount_paid + rbi.due_collection) as 'amount_paid',             rbi.due     FROM         reg_bill_info rbi     left join pat_reg_info pri ON rbi.patient_id = pri.patient_id     where         due > 0 and rbi.trans_dept_id = 1             and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' UNION SELECT         'Registration' as department,             rbi.bill_number,             rbi.bill_date as 'transaction_date',             rbi.patient_id,             pri.patient_name,             rbi.bill_amount,             rbi.discount,             (rbi.amount_paid + rbi.due_collection) as 'amount_paid',             rbi.due     FROM         reg_bill_info rbi     left join pat_reg_info pri ON rbi.patient_id = pri.patient_id     where         due > 0 and rbi.trans_dept_id = 2             and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' UNION SELECT         'Investigation' as department,             bill_number,             bill_date as 'transaction_date',             patient_id,             patient_name,             bill_amount,             discount,             (amount_paid + due_collection + pay_from_advance) as 'amount_paid',             due     FROM         inv_bill_info     where         due > 0             and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' UNION SELECT         'General' as department,             bill_number,             bill_date as 'transaction_date',             patient_id,             patient_name,             bill_amount,             discount,             (amount_paid + due_collection + pay_from_advance) as 'amount_paid',             due     FROM         gen_bill_info     where         due > 0             and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' UNION SELECT         'IP Billing' as department,             bill_number,             bill_date as 'transaction_date',             wbi.patient_id,             pri.patient_name,             bill_amount,             discount,             (amount_paid + due_collection + pay_from_advance) as 'amount_paid',             due     FROM         ward_bill_info wbi     left join pat_reg_info pri ON wbi.patient_id = pri.patient_id     where         due > 0             and bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "') as t order by t.transaction_date asc";
                    }
                    break;

                default:
                    break;
            }

            if (!string.IsNullOrEmpty(qry))
            {
                dtSource = objDBHandler.ExecuteTable(qry);
            }
            return dtSource;
        }


        public DataTable DailyCollectionSummary(ComArugments objArg)
        {
            switch (Convert.ToInt32(objArg.ParamList["departmentId"]))
            {
                case 1:
                     qry = @"SELECT 
                                T.transaction_date,
                                T.department,
                                SUM(T.bill_amount) as bill_amount,
                                SUM(T.discount) as discount,
                                SUM(T.amount_paid) as amount_paid,
                                SUM(T.due_collection) as due_collection,
                                SUM(T.due) as due,
                                SUM(T.refund_to_patient) as refund_to_patient,
                                SUM(T.net_collection) as net_collection
                            from
                                (SELECT 
                                    DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') as transaction_date,
                                        'OP Registration' as department,
                                        rbdi.bill_amount as bill_amount,
                                        sum(rbdi.discount) as discount,
                                        sum(rbdi.amount_paid) as amount_paid,
                                        sum(rbdi.due_collection) as due_collection,
                                        min(rbdi.due) as due,
                                        sum(rbdi.refund_to_patient) as refund_to_patient,
                                        (sum(rbdi.amount_paid) + sum(rbdi.due_collection) - sum(rbdi.refund_to_patient)) as net_collection
                                FROM
                                    reg_bill_detail_info rbdi
                                join reg_bill_info rbi ON rbdi.bill_id = rbi.reg_bill_id
                                where
                                    rbdi.account_type = 2
                                        and rbi.trans_dept_id = " + Convert.ToInt32(objArg.ParamList["departmentId"]) + " and rbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') , rbdi.bill_id) as T  group by STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ORDER BY STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ASC";
                    break;

                case 2:
                    qry = @"SELECT 
                                T.transaction_date,
                                T.department,
                                SUM(T.bill_amount) as bill_amount,
                                SUM(T.discount) as discount,
                                SUM(T.amount_paid) as amount_paid,
                                SUM(T.due_collection) as due_collection,
                                SUM(T.due) as due,
                                SUM(T.refund_to_patient) as refund_to_patient,
                                SUM(T.net_collection) as net_collection
                            from
                                (SELECT 
                                    DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') as transaction_date,
                                        'IP Registration' as department,
                                        rbdi.bill_amount as bill_amount,
                                        sum(rbdi.discount) as discount,
                                        sum(rbdi.amount_paid) as amount_paid,
                                        sum(rbdi.due_collection) as due_collection,
                                        min(rbdi.due) as due,
                                        sum(rbdi.refund_to_patient) as refund_to_patient,
                                        (sum(rbdi.amount_paid) + sum(rbdi.due_collection) - sum(rbdi.refund_to_patient)) as net_collection
                                FROM
                                    reg_bill_detail_info rbdi
                                join reg_bill_info rbi ON rbdi.bill_id = rbi.reg_bill_id
                                where
                                    rbdi.account_type = 2
                                        and rbi.trans_dept_id = " + Convert.ToInt32(objArg.ParamList["departmentId"]) + " and rbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') , rbdi.bill_id) as T  group by STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ORDER BY STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ASC";
                    break;

                case 3:
                    qry = @"SELECT 
                                T.transaction_date,
                                T.department,
                                SUM(T.bill_amount) as bill_amount,
                                SUM(T.discount) as discount,
                                SUM(T.amount_paid) as amount_paid,
                                SUM(T.due_collection) as due_collection,
                                SUM(T.due) as due,
                                SUM(T.refund_to_patient) as refund_to_patient,
                                SUM(T.net_collection) as net_collection
                            from
                                (SELECT 
                                        DATE_FORMAT(ibdi.transaction_date, '%d/%m/%Y') as transaction_date,
                                        'Investigation' as department,
                                        ibdi.total as bill_amount,
                                        sum(ibdi.discount) as discount,
                                        sum(ibdi.amount_paid) as amount_paid,
                                        sum(ibdi.due_collection) as due_collection,
                                        min(ibdi.due) as due,
                                        sum(ibdi.refund_to_patient) as refund_to_patient,
                                        (sum(ibdi.amount_paid) + sum(ibdi.due_collection) - sum(ibdi.refund_to_patient)) as net_collection
                                FROM
                                    inv_bill_datail_info ibdi
                                join inv_bill_info ibi ON ibdi.bill_id = ibi.bill_id
                                where
                                    ibdi.account_type = 2
                                        and ibdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(ibdi.transaction_date, '%d/%m/%Y') , ibdi.bill_id) as T group by STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ORDER BY STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ASC";
                    break;

                case 4:
                    qry = @"SELECT 
                                T.transaction_date,
                                T.department,
                                SUM(T.bill_amount) as bill_amount,
                                SUM(T.discount) as discount,
                                SUM(T.amount_paid) as amount_paid,
                                SUM(T.due_collection) as due_collection,
                                SUM(T.due) as due,
                                SUM(T.refund_to_patient) as refund_to_patient,
                                SUM(T.net_collection) as net_collection
                            from
                                (SELECT 
                                        DATE_FORMAT(pbdi.transaction_date, '%d/%m/%Y') as transaction_date,
                                        'Pharmacy' as department,
                                        pbdi.total_amount as bill_amount,
                                        sum(pbdi.discount) as discount,
                                        sum(pbdi.amount_paid) as amount_paid,
                                        sum(pbdi.due_collection) as due_collection,
                                        min(pbdi.due) as due,
                                        sum(pbdi.refund_to_patient) as refund_to_patient,
                                        (sum(pbdi.amount_paid) + sum(pbdi.due_collection) - sum(pbdi.refund_to_patient)) as net_collection
                                FROM
                                    pha_bill_detail_info pbdi
                                join pha_bill_info pbi ON pbdi.bill_id = pbi.bill_id
                                where
                                    pbdi.trans_type = 2
                                        and pbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(pbdi.transaction_date, '%d/%m/%Y') , pbdi.bill_id) as T group by STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ORDER BY STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ASC";
                    break;

                case 7:
                    qry = @"SELECT 
                                T.transaction_date,
                                T.department,
                                SUM(T.bill_amount) as bill_amount,
                                SUM(T.discount) as discount,
                                SUM(T.amount_paid) as amount_paid,
                                SUM(T.due_collection) as due_collection,
                                SUM(T.due) as due,
                                SUM(T.refund_to_patient) as refund_to_patient,
                                SUM(T.net_collection) as net_collection
                            from
                                (SELECT 
                                    DATE_FORMAT(gbdi.transaction_date, '%d/%m/%Y') as transaction_date,
                                        'General' as department,
                                        gbdi.total_amount as bill_amount,
                                        sum(gbdi.discount) as discount,
                                        sum(gbdi.amount_paid) as amount_paid,
                                        sum(gbdi.due_collection) as due_collection,
                                        min(gbdi.due) as due,
                                        sum(gbdi.refund_to_patient) as refund_to_patient,
                                        (sum(gbdi.amount_paid) + sum(gbdi.due_collection) - sum(gbdi.refund_to_patient)) as net_collection
                                FROM
                                    gen_bill_detail_info gbdi
                                join gen_bill_info gbi ON gbdi.bill_id = gbi.gen_bill_id
                                where
                                    gbdi.account_type_id = 2
                                        and gbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(gbdi.transaction_date, '%d/%m/%Y') , gbdi.bill_id) as T group by STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ORDER BY STR_TO_DATE(T.transaction_date, '%d/%m/%Y') ASC";
                    break;

                case 10:
                    qry = @"SELECT * FROM (SELECT     T.transaction_date, T.department, SUM(T.bill_amount) as bill_amount, SUM(T.discount) as discount, SUM(T.amount_paid) as amount_paid, SUM(T.due_collection) as due_collection, SUM(T.due) as due, SUM(T.refund_to_patient) as refund_to_patient, SUM(T.net_collection) as net_collection from     (SELECT     DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') as transaction_date, 'OP Registration' as department, rbdi.bill_amount as bill_amount, sum(rbdi.discount) as discount, sum(rbdi.amount_paid) as amount_paid, sum(rbdi.due_collection) as due_collection, min(rbdi.due) as due, sum(rbdi.refund_to_patient) as refund_to_patient, (sum(rbdi.amount_paid) + sum(rbdi.due_collection) - sum(rbdi.refund_to_patient)) as net_collection FROM     reg_bill_detail_info rbdi join reg_bill_info rbi ON rbdi.bill_id = rbi.reg_bill_id where     rbdi.account_type = 2 and rbi.trans_dept_id = 1 and rbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') , rbdi.bill_id) as T group by STR_TO_DATE(T.transaction_date, '%d/%m/%Y') UNION SELECT     T.transaction_date, T.department, SUM(T.bill_amount) as bill_amount, SUM(T.discount) as discount, SUM(T.amount_paid) as amount_paid, SUM(T.due_collection) as due_collection, SUM(T.due) as due, SUM(T.refund_to_patient) as refund_to_patient, SUM(T.net_collection) as net_collection from     (SELECT     DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') as transaction_date, 'IP Registration' as department, rbdi.bill_amount as bill_amount, sum(rbdi.discount) as discount, sum(rbdi.amount_paid) as amount_paid, sum(rbdi.due_collection) as due_collection, min(rbdi.due) as due, sum(rbdi.refund_to_patient) as refund_to_patient, (sum(rbdi.amount_paid) + sum(rbdi.due_collection) - sum(rbdi.refund_to_patient)) as net_collection FROM     reg_bill_detail_info rbdi join reg_bill_info rbi ON rbdi.bill_id = rbi.reg_bill_id where     rbdi.account_type = 2 and rbi.trans_dept_id = 2 and rbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(rbdi.transaction_date, '%d/%m/%Y') , rbdi.bill_id) as T group by STR_TO_DATE(T.transaction_date, '%d/%m/%Y') UNION SELECT     T.transaction_date, T.department, SUM(T.bill_amount) as bill_amount, SUM(T.discount) as discount, SUM(T.amount_paid) as amount_paid, SUM(T.due_collection) as due_collection, SUM(T.due) as due, SUM(T.refund_to_patient) as refund_to_patient, SUM(T.net_collection) as net_collection from     (SELECT     DATE_FORMAT(ibdi.transaction_date, '%d/%m/%Y') as transaction_date, 'Investigation' as department, ibdi.total as bill_amount, sum(ibdi.discount) as discount, sum(ibdi.amount_paid) as amount_paid, sum(ibdi.due_collection) as due_collection, min(ibdi.due) as due, sum(ibdi.refund_to_patient) as refund_to_patient, (sum(ibdi.amount_paid) + sum(ibdi.due_collection) - sum(ibdi.refund_to_patient)) as net_collection FROM     inv_bill_datail_info ibdi join inv_bill_info ibi ON ibdi.bill_id = ibi.bill_id where     ibdi.account_type = 2 and ibdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(ibdi.transaction_date, '%d/%m/%Y') , ibdi.bill_id) as T group by STR_TO_DATE(T.transaction_date, '%d/%m/%Y') UNION SELECT     T.transaction_date, T.department, SUM(T.bill_amount) as bill_amount, SUM(T.discount) as discount, SUM(T.amount_paid) as amount_paid, SUM(T.due_collection) as due_collection, SUM(T.due) as due, SUM(T.refund_to_patient) as refund_to_patient, SUM(T.net_collection) as net_collection from     (SELECT     DATE_FORMAT(gbdi.transaction_date, '%d/%m/%Y') as transaction_date, 'General' as department, gbdi.total_amount as bill_amount, sum(gbdi.discount) as discount, sum(gbdi.amount_paid) as amount_paid, sum(gbdi.due_collection) as due_collection, min(gbdi.due) as due, sum(gbdi.refund_to_patient) as refund_to_patient, (sum(gbdi.amount_paid) + sum(gbdi.due_collection) - sum(gbdi.refund_to_patient)) as net_collection FROM     gen_bill_detail_info gbdi join gen_bill_info gbi ON gbdi.bill_id = gbi.gen_bill_id where     gbdi.account_type_id = 2 and gbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY DATE_FORMAT(gbdi.transaction_date, '%d/%m/%Y') , gbdi.bill_id) as T group by STR_TO_DATE(T.transaction_date, '%d/%m/%Y')) AS T2 ORDER BY STR_TO_DATE(T2.transaction_date, '%d/%m/%Y') ASC";
                    break;

                default:
                    break;
            }
            if (!string.IsNullOrEmpty(qry))
            {
                dtSource = objDBHandler.ExecuteTable(qry);
            }
            return dtSource;
        }

        public DataTable CorporateDueList(ComArugments objArg)
        {
            qry = @"select     t.patient_id,    t.patient_name,    t.visit_number, t.employee_id,   date_format(t.visit_date, '%d/%m/%Y') as visit_date,    sum(t.bill_amount) as bill_amount,    sum(t.discount) as discount,    sum(t.amount_paid) as amount_paid,    sum(t.due) as due from    (select         'Registration' as department,            rbi.bill_number,            pvi.visit_date as 'visit_date',            rbi.patient_id,            pri.patient_name,            rbi.visit_number,   pvi.employee_id,         rbi.bill_amount,            rbi.discount,            (rbi.amount_paid + rbi.due_collection) as 'amount_paid',            rbi.due    FROM        reg_bill_info rbi    left join pat_reg_info pri ON rbi.patient_id = pri.patient_id    join pat_visit_info pvi ON rbi.visit_number = pvi.visit_number    join cis_corporate cc ON pvi.corporate_id = cc.corporate_id    where        due > 0 and rbi.trans_dept_id = 1            and pvi.visit_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'            and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " union SELECT         'Investigation' as department,            ibi.bill_number,            pvi.visit_date as 'visit_date',            ibi.patient_id,            ibi.patient_name,            ibi.visit_number,    pvi.employee_id,        ibi.bill_amount,            ibi.discount,            (ibi.amount_paid + ibi.due_collection + ibi.pay_from_advance) as 'amount_paid',            ibi.due    FROM        inv_bill_info ibi    join pat_visit_info pvi ON ibi.visit_number = pvi.visit_number    join cis_corporate cc ON pvi.corporate_id = cc.corporate_id    where        ibi.due > 0            and pvi.visit_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'            and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " union SELECT         'Pharmacy' as department,            pbi.bill_number,            pvi.visit_date as 'visit_date',            pbi.patient_id,            pbi.patient_name,            pbi.visit_number,     pvi.employee_id,       pbi.bill_amount,            pbi.discount,            (pbi.amount_paid + pbi.due_collection + pbi.pay_from_advance) as 'amount_paid',            due    FROM        pha_bill_info pbi    join pat_visit_info pvi ON pbi.visit_number = pvi.visit_number    join cis_corporate cc ON pvi.corporate_id = cc.corporate_id    where        pbi.due > 0            and pvi.visit_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'            and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " union SELECT         'General' as department,            gbi.bill_number,            pvi.visit_date as 'visit_date',            gbi.patient_id,            gbi.patient_name,            gbi.visit_number,    pvi.employee_id,        gbi.bill_amount,            gbi.discount,            (gbi.amount_paid + gbi.due_collection + gbi.pay_from_advance) as 'amount_paid',            due    FROM        gen_bill_info gbi    join pat_visit_info pvi ON gbi.visit_number = pvi.visit_number    join cis_corporate cc ON pvi.corporate_id = cc.corporate_id    where        gbi.due > 0            and pvi.visit_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'            and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " union SELECT         'IP Billing' as department,            wbi.bill_number,            pvi.visit_date as 'visit_date',            wbi.patient_id,            pri.patient_name,            wbi.visit_number,  pvi.employee_id,          wbi.bill_amount,            wbi.discount,            (wbi.amount_paid + wbi.due_collection + wbi.pay_from_advance) as 'amount_paid',            wbi.due    FROM        ward_bill_info wbi    left join pat_reg_info pri ON wbi.patient_id = pri.patient_id    join pat_visit_info pvi ON wbi.visit_number = pvi.visit_number    join cis_corporate cc ON pvi.corporate_id = cc.corporate_id    where        wbi.due > 0            and pvi.visit_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "'            and cc.corporate_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + ") as t group by t.visit_number";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable InvestigationList(ComArugments objArg)
        {
            qry = @"SELECT 
                        d.department_name AS 'Department',
                        il.investigation_code AS 'investigationCode',
                        il.investigation_name AS 'investigation_name',
                        ic.inv_category AS 'InvestigationCategory',
                        il.unit_price AS 'UnitPrice',
                        il.share_amt as 'share_amt'
                    FROM
                        cis_investigation_list il
                            LEFT JOIN
                        cis_investigation_category ic ON il.investigation_category_id = ic.inv_category_id
                            LEFT JOIN
                        cis_department d ON il.department_id = d.DEPARTMENT_ID
                    where
                        il.department_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " and il.status = 1  order by investigation_name asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable InvestigationShareList(ComArugments objArg)
        {
            qry = @"SELECT 
                        cm.master_value as share_to,
                        ibi.patient_id as patient_id,
                        ibi.patient_name as patient_name,
                        ibi.bill_number as bill_number,
                        DATE_FORMAT(ibi.bill_date, '%d/%m/%Y') as bill_date,
                        cil.investigation_name as investigation_name,
                        ivdi.amount as amount,
                        ivdi.share_amt as share_amt
                    FROM
                        inv_bill_datail_info ivdi
                            join
                        inv_bill_info ibi ON ivdi.bill_id = ibi.bill_id
                            join
                        cis_investigation_list cil ON ivdi.investigation_id = cil.investigation_id
                            left join
                        cis_master cm ON ivdi.share_type = cm.master_id
                    where
                        ivdi.share_amt > 0 and ibi.status <> 4
                            and cm.group_id = 3
                            and ivdi.department_id = " + Convert.ToInt32(objArg.ParamList["corporateId"]) + " and ibi.bill_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by ibi.bill_date asc ";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable printdueCollection(ComArugments billIds)
        {
            //string phabillId = Convert.ToString(billIds.ParamList["phaBillIds"]);
            qry = @"SELECT 
                        rbi.patient_id,
                        pri.patient_name,
                        if(gender = 0, 'Male', 'Female') as gender,
                        date_format(now(), '%d/%m/%Y') as 'collection_date',
                        rbi.bill_number,
                        date_format(rbi.bill_date, '%d/%m/%Y') as 'bill_date',
                        rbi.bill_amount,
                        rbi.discount,
                        (rbi.amount_paid + rbi.due_collection) as amount_paid,
                        due
                    FROM
                        reg_bill_info rbi
                            join
                        pat_reg_info pri ON rbi.patient_id = pri.patient_id
                    where
                        rbi.reg_bill_id in (" + Convert.ToString(billIds.ParamList["regBillIds"]) + ") union SELECT  patient_id,  patient_name,  if(gender = 0, 'Male', 'Female') as gender,  date_format(now(), '%d/%m/%Y') as 'collection_date',  bill_number,  date_format(bill_date, '%d/%m/%Y') as 'bill_date',  bill_amount,  discount,  (amount_paid + pay_from_advance + due_collection) as amount_paid,  due FROM  inv_bill_info where  bill_id in (" + Convert.ToString(billIds.ParamList["invBillIds"]) + ") union SELECT  patient_id,  patient_name,  if(gender = 0, 'Male', 'Female') as gender,  date_format(now(), '%d/%m/%Y') as 'collection_date',  bill_number,  date_format(bill_date, '%d/%m/%Y') as 'bill_date',  bill_amount,  discount,  (amount_paid + pay_from_advance + due_collection) as amount_paid,  due FROM  pha_bill_info where  bill_id in (" + Convert.ToString(billIds.ParamList["phaBillIds"]) + ") union SELECT  patient_id,  patient_name,  '' as gender,  date_format(now(), '%d/%m/%Y') as 'collection_date',  bill_number,  date_format(bill_date, '%d/%m/%Y') as 'bill_date',  bill_amount,  discount,  (amount_paid + pay_from_advance + due_collection) as amount_paid,  due  FROM  gen_bill_info where  gen_bill_id in (" + Convert.ToString(billIds.ParamList["genBillIds"]) + ") union SELECT  wbi.patient_id,  pri.patient_name,  if(gender = 0, 'Male', 'Female') as gender,  date_format(now(), '%d/%m/%Y') as 'collection_date',  wbi.bill_number,  date_format(wbi.bill_date, '%d/%m/%Y') as 'bill_date',  wbi.bill_amount,  wbi.discount,  (wbi.amount_paid + pay_from_advance + wbi.due_collection) as amount_paid,  due FROM  ward_bill_info wbi join  pat_reg_info pri ON wbi.patient_id = pri.patient_id where  wbi.bill_id in (" + Convert.ToString(billIds.ParamList["wardBillIds"]) + ")";
            billIds = null;
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }


        public DataTable CorporateDueListDetails(ComArugments objArg)
        {
            string startDate = objArg.ParamList["startDate"].ToString();
            string endDate = objArg.ParamList["endDate"].ToString();
            int corporateId = Convert.ToInt32(objArg.ParamList["corporateId"].ToString());
            qry = @"select bill_id,department,bill_number,visit_date,patient_id,patient_name,visit_number,bill_amount,discount,
                            amount_paid,due,due as 'ActualDue',employee_id from (select reg_bill_id as 'bill_id',
                                    'Registration' as department,
                                        rbi.bill_number,
                                        pvi.visit_date as 'visit_date',
                                        rbi.patient_id,
                                        pri.patient_name,
                                        rbi.visit_number,
                                        rbi.bill_amount,
                                        rbi.discount,
                                        (rbi.amount_paid + rbi.due_collection) as 'amount_paid',
                                        rbi.due, pvi.employee_id
                                FROM
                                    reg_bill_info rbi
                                left join pat_reg_info pri ON rbi.patient_id = pri.patient_id
                                join pat_visit_info pvi ON rbi.visit_number = pvi.visit_number
                                join cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                                where
                                    due > 0 and rbi.trans_dept_id = 1
                                        and pvi.visit_date between " + startDate + "  and " + endDate+ 
                                       @" and cc.corporate_id = " + corporateId +
                               " union SELECT ibi.bill_id," +
                                    @"'Laboratory' as department,
                                        ibi.bill_number,
                                        pvi.visit_date as 'visit_date',
                                        ibi.patient_id,
                                        ibi.patient_name,
                                        ibi.visit_number,
                                        ibi.bill_amount,
                                        ibi.discount,
                                        (ibi.amount_paid + ibi.due_collection + ibi.pay_from_advance) as 'amount_paid',
                                        ibi.due, pvi.employee_id
                                FROM
                                    inv_bill_info ibi
                                join pat_visit_info pvi ON ibi.visit_number = pvi.visit_number
                                join cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                                where
                                    ibi.due > 0
                                        and pvi.visit_date between " + startDate + "  and " + endDate+
                                      @" and cc.corporate_id = " + corporateId +
                            @" union SELECT pbi.bill_id,
                                    'Pharmacy' as department,
                                        pbi.bill_number,
                                        pvi.visit_date as 'visit_date',
                                        pbi.patient_id,
                                        pbi.patient_name,
                                        pbi.visit_number,
                                        pbi.bill_amount,
                                        pbi.discount,
                                        (pbi.amount_paid + pbi.due_collection + pbi.pay_from_advance) as 'amount_paid',
                                        due, pvi.employee_id
                                FROM
                                    pha_bill_info pbi
                                join pat_visit_info pvi ON pbi.visit_number = pvi.visit_number
                                join cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                                where
                                    pbi.due > 0
                                        and pvi.visit_date between " + startDate + "  and " + endDate+
                                      @" and cc.corporate_id = " + corporateId +
                            @" union SELECT gen_bill_id as 'bill_id',
                                    'General' as department,
                                        gbi.bill_number,
                                        pvi.visit_date as 'visit_date',
                                        gbi.patient_id,
                                        gbi.patient_name,
                                        gbi.visit_number,
                                        gbi.bill_amount,
                                        gbi.discount,
                                        (gbi.amount_paid + gbi.due_collection + gbi.pay_from_advance) as 'amount_paid',
                                        due, pvi.employee_id
                                FROM
                                    gen_bill_info gbi
                                join pat_visit_info pvi ON gbi.visit_number = pvi.visit_number
                                join cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                                where
                                    gbi.due > 0
                                        and pvi.visit_date between " + startDate + "  and " + endDate+ 
                                      @" and cc.corporate_id = "+ corporateId+
                            @" union SELECT bill_id,
                                    'IP Billing' as department,
                                        wbi.bill_number,
                                        pvi.visit_date as 'visit_date',
                                        wbi.patient_id,
                                        pri.patient_name,
                                        wbi.visit_number,
                                        wbi.bill_amount,
                                        wbi.discount,
                                        (wbi.amount_paid + wbi.due_collection + wbi.pay_from_advance) as 'amount_paid',
                                        wbi.due, pvi.employee_id
                                FROM
                                    ward_bill_info wbi
                                left join pat_reg_info pri ON wbi.patient_id = pri.patient_id
                                join pat_visit_info pvi ON wbi.visit_number = pvi.visit_number
                                join cis_corporate cc ON pvi.corporate_id = cc.corporate_id
                                where
                                    wbi.due > 0
                                        and pvi.visit_date between " + startDate + "  and " + endDate+ 
                                        @" and cc.corporate_id = "+corporateId + @") as t
                            order by patient_id";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable MedicineList(ComArugments objArg)
        {
            qry = @"SELECT 
                        it.item_type, pi.item_name, pi.hsn_code, pt.tax_rate
                    FROM
                        cis_pha_item pi
                            join
                        cis_pha_item_type it ON pi.item_type_id = it.item_type_id
                            left join
                        cis_pha_tax pt ON pi.tax_id = pt.tax_id
                    where
                        pi.status = 1
                    order by it.item_type , pi.item_name asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable currentStockReport(ComArugments objArg)
        {
            qry = @"SELECT 
                        it.item_type,
                        pi.item_name,
                        pis.lot_id,
                        date_format(pis.exp_date, '%m/%Y') AS exp_date,
                        pis.avail_qty,
                        pis.vendor_price,
                        pis.mrp
                    FROM
                        pha_inventory_stock pis
                            left join
                        cis_pha_item pi ON pis.item_id = pi.item_id
                            join
                        cis_pha_item_type it ON pi.item_type_id = it.item_type_id
                    where
                        pis.avail_qty > 0 and pi.status = 1
                    order by it.item_type , pi.item_name asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable expiryMedicineList(ComArugments objArg)
        {
            qry = @"SELECT 
                        it.item_type,
                        pi.item_name,
                        pis.lot_id,
                        date_format(pis.exp_date, '%m/%Y') AS exp_date,
                        pis.avail_qty,
                        pis.vendor_price,
                        pis.mrp,
                        datediff(pis.exp_date, '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "') as no_of_days FROM pha_inventory_stock pis   left join cis_pha_item pi ON pis.item_id = pi.item_id  join cis_pha_item_type it ON pi.item_type_id = it.item_type_id  where pis.avail_qty > 0 and pi.status = 1 and pis.exp_date <= '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by it.item_type , pi.item_name asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable purchaseMedicineReport(ComArugments objArg)
        {
            qry = @"SELECT 
                        ppr.pur_invoice_number,
                        date_format(ppr.pur_invoice_date, '%d/%m/%Y') as pur_invoice_date,
                        ppr.grn_number as grn_number,
                        date_format(ppr.pur_receive_date, '%d/%m/%Y') as pur_receive_date,
                        pv.vendor_name as vendor_name,
                        ppr.invoice_total_amount,
                        (ppr.cash_discount + item_discount) as pur_discount,
                        ppr.tax_amount,
                        ppr.returned_amount,
                        round(ppr.net_invoice_amount, 0) as net_invoice_amount
                    FROM
                        pha_purchase_receipt ppr
                            LEFT JOIN
                        cis_pha_vender pv ON ppr.vendor_id = pv.vendor_id
                    where
                        ppr.pur_invoice_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by ppr.pur_invoice_date ASC";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable refundedMedicineList(ComArugments objArg)
        {
            qry = @"SELECT 
                        pbi.bill_number,
                        date_format(pbdi.transaction_date,
                                '%d/%m/%Y %h:%i %p') as transaction_date,
                        it.item_type,
                        pi.item_name,
                        pbdi.refund_qty,
                        pbdi.unit_price,
                        pbdi.lot_id,
                        pbdi.expiry_date,
                        cu.user_name
                    FROM
                        pha_bill_detail_info pbdi
                            join
                        pha_bill_info pbi ON pbdi.bill_id = pbi.bill_id
                            left join
                        cis_pha_item pi ON pbdi.item_id = pi.item_id
                            join
                        cis_pha_item_type it ON pbdi.item_type_id = it.item_type_id
                            left join
                        cis_user cu ON pbdi.trans_user_id = cu.user_id
                    where
                        pbdi.refund_qty > 0
                            AND pbdi.transaction_date between '" + Convert.ToDateTime(objArg.ParamList["startDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + Convert.ToDateTime(objArg.ParamList["endDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "' order by pbdi.transaction_date asc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region View Bill Info
        public DataTable viewRegBillInfoByVisit(string visitQry)
        {
            qry = @"SELECT 
                        reg_bill_id as bill_id,     
                        'Registration' as Department,
                        bill_number as 'Bill No',
                        date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',
                        visit_number as 'Visit Id',
                        bill_amount as 'Bill Amount',
                        Discount,
                        (amount_paid + due_collection) as 'Amount Paid',
                        '0.00' as 'Adv Adj',
                        Due,
                        (case
                            when status = 1 then 'Paid'
                            when status = 2 then 'Not Paid'
                            when status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        reg_bill_info
                    where
                        visit_number in (" + visitQry + ") order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewInvBillInfoByVisit(string visitQry)
        {
            qry = @"SELECT 
                        bill_id as bill_id,     
                        'Investigation' as Department,
                        bill_number as 'Bill No',
                        date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',
                        visit_number as 'Visit Id',
                        bill_amount as 'Bill Amount',
                        Discount,
                        (amount_paid + due_collection) as 'Amount Paid',
                        pay_from_advance as 'Adv Adj',
                        Due,
                        (case
                            when status = 1 then 'Paid'
                            when status = 2 then 'Not Paid'
                            when status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        inv_bill_info
                    where
                        visit_number in (" + visitQry + ") order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewPharmacyBillInfoByVisit(string visitQry)
        {
            qry = @"SELECT 
                        bill_id as bill_id,     
                        'Pharmacy' as Department,
                        bill_number as 'Bill No',
                        date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',
                        visit_number as 'Visit Id',
                        bill_amount as 'Bill Amount',
                        Discount,
                        (amount_paid + due_collection) as 'Amount Paid',
                        pay_from_advance as 'Adv Adj',
                        Due,
                        (case
                            when status = 1 then 'Paid'
                            when status = 2 then 'Not Paid'
                            when status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        pha_bill_info
                    where
                        visit_number in( " + visitQry + ") order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewGenBillInfoByVisit(string visitQry)
        {
            qry = @"SELECT 
                        gen_bill_id as bill_id,     
                        'General' as Department,
                        bill_number as 'Bill No',
                        date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',
                        visit_number as 'Visit Id',
                        bill_amount as 'Bill Amount',
                        Discount,
                        (amount_paid + due_collection) as 'Amount Paid',
                        pay_from_advance as 'Adv Adj',
                        Due,
                        (case
                            when status = 1 then 'Paid'
                            when status = 2 then 'Not Paid'
                            when status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        gen_bill_info
                    where
                        visit_number in ( " + visitQry + ") order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewWardBillInfoByVisit(string visitQry)
        {
            qry = @"SELECT 
                        bill_id as bill_id,
                        'IP Billing' as Department,
                        bill_number as 'Bill No',
                        date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',
                        visit_number as 'Visit Id',
                        bill_amount as 'Bill Amount',
                        Discount,
                        (amount_paid + due_collection) as 'Amount Paid',
                        pay_from_advance as 'Adv Adj',
                        Due,
                        (case
                            when status = 1 then 'Paid'
                            when status = 2 then 'Not Paid'
                            when status = 3 then 'Partially Paid'
                            else 'Cancelled'
                        end) as Status
                    FROM
                        ward_bill_info
                    where
                        visit_number in ( " + visitQry + ") order by bill_date desc";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }

        public DataTable viewAllBillInfoByVisit(string visitQry)
        {
            //string q = "select              visit_number         from             pat_visit_info         where             patient_id = '" + patient_id + "'";
            //qry = @"SELECT     reg_bill_id as bill_id,     'Registration' as Department,     bill_number as 'Bill No',     date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',     visit_number as 'Visit Id',     bill_amount as 'Bill Amount',     Discount,     (amount_paid + due_collection) as 'Amount Paid',     Due,     (case         when status = 1 then 'Paid'         when status = 2 then 'Not Paid'         when status = 3 then 'Partially Paid'         else 'Cancelled'     end) as Status FROM     reg_bill_info where     visit_number in (select              visit_number         from             pat_visit_info         where             patient_id = '" + patient_id + "')  UNION SELECT      bill_id,     'Investigation' as Department,     bill_number as 'Bill No',     date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',     visit_number as 'Visit Id',     bill_amount as 'Bill Amount',     Discount,     (amount_paid + pay_from_advance + due_collection) as 'Amount Paid',     Due,     (case         when status = 1 then 'Paid'         when status = 2 then 'Not Paid'         when status = 3 then 'Partially Paid'         else 'Cancelled'     end) as Status FROM     inv_bill_info where     visit_number in (select              visit_number         from             pat_visit_info         where             patient_id = '" + patient_id + "')  UNION SELECT      bill_id,     'Pharmacy' as Department,     bill_number as 'Bill No',     date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',     visit_number as 'Visit Id',     bill_amount as 'Bill Amount',     Discount,     (amount_paid + pay_from_advance + due_collection) as 'Amount Paid',     Due,     (case         when status = 1 then 'Paid'         when status = 2 then 'Not Paid'         when status = 3 then 'Partially Paid'         else 'Cancelled'     end) as Status FROM     pha_bill_info where     visit_number = '201707162'  UNION SELECT      gen_bill_id as bill_id,     'General' as Department,     bill_number as 'Bill No',     date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',     visit_number as 'Visit Id',     bill_amount as 'Bill Amount',     Discount,     (amount_paid + pay_from_advance + due_collection) as 'Amount Paid',     Due,     (case         when status = 1 then 'Paid'         when status = 2 then 'Not Paid'         when status = 3 then 'Partially Paid'         else 'Cancelled'     end) as Status FROM     gen_bill_info where     visit_number in (select              visit_number         from             pat_visit_info         where             patient_id = '" + patient_id + "')";
            qry = @"SELECT     reg_bill_id as bill_id,     'Registration' as Department,     bill_number as 'Bill No', date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',     visit_number as 'Visit Id', bill_amount as 'Bill Amount',     Discount,     (amount_paid + due_collection) as 'Amount Paid',                          '0.00' as 'Adv Adj',   Due, (case         when status = 1 then 'Paid'  when status = 2 then 'Not Paid'         when status = 3 then 'Partially Paid'    else 'Cancelled'     end) as Status FROM     reg_bill_info where     visit_number in (" + visitQry + ")  UNION SELECT      bill_id,     'Investigation' as Department,     bill_number as 'Bill No', date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',     visit_number as 'Visit Id', bill_amount as 'Bill Amount',     Discount,     (amount_paid + due_collection) as 'Amount Paid',  pay_from_advance as 'Adv Adj', Due,     (case         when status = 1 then 'Paid'         when status = 2 then 'Not Paid'     when status = 3 then 'Partially Paid'         else 'Cancelled'     end) as Status FROM     inv_bill_info where visit_number in (" + visitQry + ")  UNION SELECT      bill_id,     'Pharmacy' as Department,     bill_number as 'Bill No', date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',     visit_number as 'Visit Id', bill_amount as 'Bill Amount',   (discount + total_free_care) as Discount,     (amount_paid + due_collection) as 'Amount Paid',    pay_from_advance as 'Adv Adj',   Due, (case         when status = 1 then 'Paid'         when status = 2 then 'Not Paid'     when status = 3 then 'Partially Paid'         else 'Cancelled'     end) as Status FROM     pha_bill_info  where     visit_number in (" + visitQry + ")  UNION SELECT      gen_bill_id as bill_id,     'General' as Department, bill_number as 'Bill No',     date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date',     visit_number as 'Visit Id', bill_amount as 'Bill Amount',     Discount,     (amount_paid + due_collection) as 'Amount Paid',                         pay_from_advance as 'Adv Adj',    Due, (case         when status = 1 then 'Paid'         when status = 2 then 'Not Paid'         when status = 3 then 'Partially Paid'     else 'Cancelled'     end) as Status FROM     gen_bill_info where     visit_number in (" + visitQry + ") union SELECT bill_id as bill_id, 'IP Billing' as Department, bill_number as 'Bill No', date_format(bill_date, '%d/%m/%Y %h:%i %p') as 'Bill Date', visit_number as 'Visit Id', bill_amount as 'Bill Amount', Discount, (amount_paid + due_collection) as 'Amount Paid', pay_from_advance as 'Adv Adj', Due, (case     when status = 1 then 'Paid'     when status = 2 then 'Not Paid'     when status = 3 then 'Partially Paid'     else 'Cancelled' end) as Status FROM ward_bill_info where visit_number in (" + visitQry + ")";
            dtSource = objDBHandler.ExecuteTable(qry);
            return dtSource;
        }
        #endregion

        #region Due Collection All Bills
        public int updateRegistrationDueCollection(ComArugments objArg)
        {
            qry = "UPDATE reg_bill_info SET discount = discount + " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", net_total = net_total - " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", amount_paid = amount_paid + " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", due = " + Convert.ToDecimal(objArg.ParamList["due"]) + ", due_collection = due_collection + " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ", status = " + Convert.ToInt32(objArg.ParamList["status"]) + " WHERE reg_bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertRegistrationSummaryDueCollection(ComArugments objArg)
        {
            qry = "INSERT INTO reg_bill_detail_info ( bill_id, transaction_date, account_head_id, account_head_name, account_type, discount, amount_paid, due_collection, trans_user_id, payment_mode_id, card_number, bank_name, holder_name, status) VALUES ( " + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", now(), 2, 'Payment', 2, " + Convert.ToDecimal(objArg.ParamList["discount"]) + ",  " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ",  " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateInvestigationDueCollection(ComArugments objArg)
        {
            qry = "UPDATE inv_bill_info SET discount = discount + " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", total_amount = total_amount - " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", amount_paid = " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", due =" + Convert.ToDecimal(objArg.ParamList["due"]) + ",  due_collection = due_collection + " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ",  status = " + Convert.ToInt32(objArg.ParamList["status"]) + " WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertInvestigationSummaryDueCollection(ComArugments objArg)
        {
            qry = "INSERT INTO inv_bill_datail_info ( bill_id, discount, account_type, amount_paid, due_collection, payment_mode_id, card_number, bank_name, holder_name, transaction_date, transaction_user_id, status) VALUES ( " + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", 2, " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ",  " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "', now(), " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updatePharmacyDueCollection(ComArugments objArg)
        {
            qry = "UPDATE pha_bill_info SET discount = discount + " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", total_amount = total_amount - " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", amount_paid = amount_paid + " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", due = " + Convert.ToDecimal(objArg.ParamList["due"]) + ",  due_collection = due_collection + " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ", status = " + Convert.ToInt32(objArg.ParamList["status"]) + " WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertPharmacySummaryDueCollection(ComArugments objArg)
        {
            qry = "INSERT INTO pha_bill_detail_info ( bill_id, transaction_date, trans_type, discount, amount_paid, due_collection, trans_user_id, payment_mode_id, card_number, bank_name, holder_name, status) VALUES ( " + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", now(), 2, " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ",  " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateGeneralDueCollection(ComArugments objArg)
        {
            qry = "UPDATE gen_bill_info SET discount = discount + " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", total_amount = total_amount + " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", amount_paid = amount_paid + " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", due_collection = due_collection + " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ", due =" + Convert.ToDecimal(objArg.ParamList["due"]) + ", status = " + Convert.ToInt32(objArg.ParamList["status"]) + " WHERE gen_bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertGeneralSummaryDueCollection(ComArugments objArg)
        {
            qry = "INSERT INTO gen_bill_detail_info ( bill_id, account_type_id, transaction_date, discount, amount_paid, due_collection, payment_mode_id, card_number, bank_name, holder_name, trans_user_id, status) VALUES ( " + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", 2, now(), " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ",  " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ",  " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "', " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int updateBillingDueCollection(ComArugments objArg)
        {
            qry = "UPDATE ward_bill_info SET discount = discount + " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", total_amount = total_amount + " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", amount_paid = amount_paid + " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", due_collection = due_collection + " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ", pay_from_advance = pay_from_advance + " + Convert.ToDecimal(objArg.ParamList["pay_from_advance"]) + ", due =" + Convert.ToDecimal(objArg.ParamList["due"]) + ", status = " + Convert.ToInt32(objArg.ParamList["status"]) + " WHERE bill_id = " + Convert.ToInt32(objArg.ParamList["bill_id"]) + "";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertBillingSummaryDueCollection(ComArugments objArg)
        {
            qry = "INSERT INTO ward_bill_detail_info (bill_id, discount, amount_paid, pay_from_advance, due, due_collection, transaction_user_id, transaction_date, payment_mode_id, card_number, bank_name, holder_name, account_transaction_type, status) VALUES ( " + Convert.ToInt32(objArg.ParamList["bill_id"]) + ", " + Convert.ToDecimal(objArg.ParamList["discount"]) + ", " + Convert.ToDecimal(objArg.ParamList["amount_paid"]) + ", " + Convert.ToDecimal(objArg.ParamList["pay_from_advance"]) + ", " + Convert.ToDecimal(objArg.ParamList["due"]) + ", " + Convert.ToDecimal(objArg.ParamList["due_collection"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["payment_mode_id"]) + ", '" + objArg.ParamList["card_number"].ToString() + "', '" + objArg.ParamList["bank_name"].ToString() + "', '" + objArg.ParamList["holder_name"].ToString() + "', 2, " + Convert.ToInt32(objArg.ParamList["status"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }

        public int insertWardAdvAdjDueCollection(ComArugments objArg)
        {
            qry = "INSERT INTO cis_advance (patient_id, visit_no, account_type, transaction_amount, net_amount, trans_user_id, trans_date, bill_id) VALUES ('" + objArg.ParamList["patient_id"].ToString() + "', '" + objArg.ParamList["visit_number"].ToString() + "',  2, " + Convert.ToDecimal(objArg.ParamList["pay_from_advance"]) + ", " + Convert.ToDecimal(objArg.ParamList["ward_Adv_Net_Coll"]) + ", " + Convert.ToInt32(objArg.ParamList["transaction_user_id"]) + ", now(), " + Convert.ToInt32(objArg.ParamList["bill_id"]) + ")";
            flag = objDBHandler.ExecuteCommand(qry);
            return flag;
        }
        #endregion
    }
}
