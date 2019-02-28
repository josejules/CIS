using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Configuration;

using CIS.BusinessFacade;
using CIS.Common;
using CIS;

namespace CIS.Reports
{

    public partial class frmReportViewer : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        public static DataTable dtHosSource = new DataTable();
        public static DataTable dtCorptSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public frmReportViewer()
        {
            InitializeComponent();
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            this.rptCISViewer.RefreshReport();
            this.dtpDateFrom.Value = DateTime.Now;
            this.dtpDateTo.Value = DateTime.Now;
            this.cboDepartment.SelectedIndex = 1;
            //loadCorporate();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            Common.Common.startDate = objBusinessFacade.dateFromValue(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd"));
            Common.Common.endDate = objBusinessFacade.dateToValue(dtpDateTo.Value.Date.ToString("yyyy-MM-dd"));
            if (tvReportList.SelectedNode != null)
            {
                Common.Common.reportName = tvReportList.SelectedNode.Name;
            }
            else
            {
                MessageBox.Show("Report is not selected...!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Common.Common.cis_department.departmentName = cboDepartment.Text.ToString();

            if (Convert.ToInt32(cboCorporate.SelectedIndex.ToString()) > 0 && cboCorporate.Items.Count > 0)
            {
                Common.Common.cis_Corporate.corporateId = Convert.ToInt32(cboCorporate.SelectedValue.ToString());
                Common.Common.cis_Corporate.corporateName = cboCorporate.Text.ToString();
            }
            else
            {
                Common.Common.cis_Corporate.corporateId = 0;
                Common.Common.cis_Corporate.corporateName = string.Empty;
            }

            switch (Common.Common.cis_department.departmentName)
            {
                case "OP Registration":
                    Common.Common.cis_department.visitMode = 1;
                    Common.Common.cis_department.departmentId = 1;
                    break;

                case "IP Registration":
                    Common.Common.cis_department.visitMode = 2;
                    Common.Common.cis_department.departmentId = 2;
                    break;

                case "Investigation":
                    Common.Common.cis_department.departmentId = 3;
                    break;

                case "Pharmacy":
                    Common.Common.cis_department.departmentId = 4;
                    break;

                case "General":
                    Common.Common.cis_department.departmentId = 7;
                    break;

                case "IP Billing":
                    Common.Common.cis_department.departmentId = 8;
                    break;

                case "All":
                    Common.Common.cis_department.departmentId = 10;
                    break;

                default:
                    break;
            }

            string hosName = Convert.ToString(ConfigurationSettings.AppSettings["HospitalName"]);
            string place = Convert.ToString(ConfigurationSettings.AppSettings["Place"]);
            string PinCode = Convert.ToString(ConfigurationSettings.AppSettings["PinCode"]);

            objArg.ParamList["startDate"] = Common.Common.startDate;
            objArg.ParamList["endDate"] = Common.Common.endDate;
            objArg.ParamList["visitMode"] = Common.Common.cis_department.visitMode;
            objArg.ParamList["departmentId"] = Common.Common.cis_department.departmentId;
            objArg.ParamList["corporateId"] = Common.Common.cis_Corporate.corporateId;
            objArg.ParamList["corporate_name"] = Common.Common.cis_Corporate.corporateName;

            dtSource.Clear();
            dtHosSource.Clear();
            dtHosSource.Columns.Clear();

            switch (Common.Common.reportName)
            {
                case "childCensusMDeptGender":
                    dtSource = objBusinessFacade.viewRegCensusMedDepartmentByGender(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\CensusMDepartmentByGender.rdlc";
                    break;

                case "CensusMonthVisitModeByGender":
                    dtSource = objBusinessFacade.viewRegCensusMonthVisitModeByGender(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\CensusMonthVisitModeByGender.rdlc";
                    break;

                case "OPRegistationList":
                    dtSource = objBusinessFacade.viewOPRegistationList(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\OPRegistationList.rdlc";
                    break;

                case "IPAdmissionList":
                    dtSource = objBusinessFacade.viewIPAdmissionList(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\IPAdmissionList.rdlc";
                    break;

                case "IPDischargeList":
                    dtSource = objBusinessFacade.viewIPDischargeList(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\IPDischargeList.rdlc";
                    break;

                case "IPCurrentList":
                    dtSource = objBusinessFacade.viewCurrentIPList();
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\IPCurrentList.rdlc";
                    break;

                case "DailyCollection":
                    dtSource = objBusinessFacade.DailyCollection(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\DailyCollection.rdlc";
                    break;

                case "DailyCollectionSummary":
                    dtSource = objBusinessFacade.DailyCollectionSummary(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\DailyCollectionSummary.rdlc";
                    break;

                case "DueList":
                    dtSource = objBusinessFacade.DueList(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\DueList.rdlc";
                    break;

                case "CorporateDueList":
                    if (Common.Common.cis_Corporate.corporateId == 0)
                    {
                        MessageBox.Show("Corporate is not selected...!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    else
                    {
                        dtSource = objBusinessFacade.CorporateDueList(objArg);
                        rptCISViewer.LocalReport.DataSources.Clear();
                        rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\CorporateDueList.rdlc";
                    }
                    break;

                case "medicineList":
                    dtSource = objBusinessFacade.medicineList(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\MedicineList.rdlc";
                    break;

                case "InvestigationList":
                    dtSource = objBusinessFacade.InvestigationList(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\InvestigationList.rdlc";
                    break;

                case "InvestigationShareList":
                    dtSource = objBusinessFacade.InvestigationShareList(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\InvestigationShareList.rdlc";
                    break;

                case "currentStockReport":
                    dtSource = objBusinessFacade.currentStockReport(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\CurrentStockReport.rdlc";
                    break;

                case "expiryMedicineList":
                    dtSource = objBusinessFacade.expiryMedicineList(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\expiryMedicineList.rdlc";
                    break;

                case "purchaseMedicineReport":
                    dtSource = objBusinessFacade.purchaseMedicineReport(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\purchaseMedicineReport.rdlc";
                    break;

                case "refundedMedicineList":
                    dtSource = objBusinessFacade.refundedMedicineList(objArg);
                    rptCISViewer.LocalReport.DataSources.Clear();
                    rptCISViewer.LocalReport.ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\Reports\\refundedMedicineList.rdlc";
                    break;

                default:
                    break;
            }

            /*dtSource = objBusinessFacade.getPatientDetails();
            rptCISViewer.LocalReport.DataSources.Clear();
            rptCISViewer.LocalReport.ReportPath = @"E:\DotNet Project\CIS\CIS\Reports\Patient.rdlc";*/
            dtHosSource.Columns.Add("hosName", typeof(string));
            dtHosSource.Columns.Add("place", typeof(string));
            dtHosSource.Columns.Add("PinCode", typeof(string));
            dtHosSource.Columns.Add("department", typeof(string));
            dtHosSource.Columns.Add("startDate", typeof(string));
            dtHosSource.Columns.Add("endDate", typeof(string));
            dtHosSource.Columns.Add("corporate_name", typeof(string));

            DataRow row = dtHosSource.NewRow();
            //row[0] = 0;
            row[0] = hosName;
            row[1] = place;
            row[2] = PinCode;
            row[3] = Common.Common.cis_department.departmentName;
            row[4] = Common.Common.startDate;
            row[5] = Common.Common.endDate;
            row[6] = Common.Common.cis_Corporate.corporateName;
            dtHosSource.Rows.InsertAt(row, 0);
            ReportDataSource rptHosdtSource = new ReportDataSource("HOS", dtHosSource);
            rptCISViewer.LocalReport.DataSources.Add(rptHosdtSource);

            ReportDataSource CISDataSource = new ReportDataSource("CIS", dtSource);
            rptCISViewer.LocalReport.DataSources.Add(CISDataSource);

            ReportDataSource reportsRSource = new ReportDataSource("ReportsR", dtSource);
            rptCISViewer.LocalReport.DataSources.Add(reportsRSource);

            ReportDataSource collectionSource = new ReportDataSource("Collection", dtSource);
            rptCISViewer.LocalReport.DataSources.Add(collectionSource);

            ReportDataSource PharmacyRSource = new ReportDataSource("PharmacyR", dtSource);
            rptCISViewer.LocalReport.DataSources.Add(PharmacyRSource);

            ReportDataSource InvestigationRSource = new ReportDataSource("InvestigationR", dtSource);
            rptCISViewer.LocalReport.DataSources.Add(InvestigationRSource);

            rptCISViewer.LocalReport.Refresh();
            rptCISViewer.RefreshReport();
            //loadCorporate();
        }

        private void tvReportList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Common.Common.reportName = tvReportList.SelectedNode.Name;

            switch (Common.Common.reportName)
            {
                case "childCensusMDeptGender":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 1;
                    cboDepartment.Enabled = true;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "CensusMonthVisitModeByGender":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 1;
                    cboDepartment.Enabled = true;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "OPRegistationList":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 1;
                    cboDepartment.Enabled = true;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "IPAdmissionList":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 2;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "IPDischargeList":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 2;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "IPCurrentList":
                    dtpDateFrom.Enabled = false;
                    dtpDateTo.Enabled = false;
                    cboDepartment.SelectedIndex = 2;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "DailyCollection":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 1;
                    cboDepartment.Enabled = true;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "DueList":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 1;
                    cboDepartment.Enabled = true;
                    cboCorporate.Visible = true;
                    lblCorporate.Visible = true;
                    break;

                case "CorporateDueList":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 0;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = true;
                    lblCorporate.Visible = true;
                    break;

                case "refundedMedicineList":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 4;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "medicineList":
                    dtpDateFrom.Enabled = false;
                    dtpDateTo.Enabled = false;
                    cboDepartment.SelectedIndex = 4;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "InvestigationList":
                    dtpDateFrom.Enabled = false;
                    dtpDateTo.Enabled = false;
                    cboDepartment.SelectedIndex = 3;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = true;
                    lblCorporate.Visible = true;
                    break;

                case "InvestigationShareList":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 3;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = true;
                    lblCorporate.Visible = true;
                    break;

                case "currentStockReport":
                    dtpDateFrom.Enabled = false;
                    dtpDateTo.Enabled = false;
                    cboDepartment.SelectedIndex = 4;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "expiryMedicineList":
                    dtpDateFrom.Enabled = false;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 4;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                case "purchaseMedicineReport":
                    dtpDateFrom.Enabled = true;
                    dtpDateTo.Enabled = true;
                    cboDepartment.SelectedIndex = 4;
                    cboDepartment.Enabled = false;
                    cboCorporate.Visible = false;
                    lblCorporate.Visible = false;
                    break;

                default:
                    break;
            }
        }

        private void loadCorporate()
        {
            try
            {
                dtCorptSource = objBusinessFacade.loadCorporate();
                if (dtCorptSource != null && dtCorptSource.Rows.Count > 0)
                {
                    DataRow row = dtCorptSource.NewRow();
                    row[0] = 0;
                    row[1] = "";
                    dtCorptSource.Rows.InsertAt(row, 0);
                    cboCorporate.ValueMember = "CORPORATE_ID";
                    cboCorporate.DisplayMember = "CORPORATE_NAME";
                    cboCorporate.DataSource = dtCorptSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void loadInvestigationDepartment()
        {
            try
            {
                dtCorptSource = objBusinessFacade.loadInvestigationDepartment();
                if (dtCorptSource != null && dtCorptSource.Rows.Count > 0)
                {
                    DataRow row = dtCorptSource.NewRow();
                    row[0] = 0;
                    row[1] = "";
                    dtCorptSource.Rows.InsertAt(row, 0);
                    cboCorporate.ValueMember = "DEPARTMENT_ID";
                    cboCorporate.DisplayMember = "DEPARTMENT_NAME";
                    cboCorporate.DataSource = dtCorptSource;
                }
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCorporate.DataSource = null;
            if (cboDepartment.Text.ToString() == "Investigation")
            {
                lblCorporate.Text = "Sub Depart";
                loadInvestigationDepartment();
            }

            else
            {
                lblCorporate.Text = "Corporate";
                loadCorporate();
            }
        }
    }
}
