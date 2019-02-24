using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using CIS.Laboratory.Report;
using CIS.BusinessFacade;

namespace CIS.Laboratory
{
    public partial class frmResultEntryReport : Form
    {
        int BillId = 0;
        public frmResultEntryReport()
        {
            InitializeComponent();
        }
        public frmResultEntryReport(int billId)
        {
            BillId = billId;
            InitializeComponent();
        }

        private void frmResultEntryReport_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtResultEntry = new clsLabBusinessFacade().LoadResultEntryInfoForLabPrintout(BillId);
                if (dtResultEntry.Rows.Count > 0)
                {
                    if (ConfigurationManager.AppSettings["HospitalName"] == "ST.MARY'S HOSPITAL")
                    {
                        StMaryHosurResultView rptObj = new StMaryHosurResultView();
                        rptObj.SetDataSource(dtResultEntry);
                        crystalReportViewer1.ReportSource = rptObj;
                        crystalReportViewer1.EnableDrillDown = false;
                    }
                    else
                    {
                        ResultView rptObj = new ResultView();
                        rptObj.SetDataSource(dtResultEntry);
                        crystalReportViewer1.ReportSource = rptObj;
                    }

                    //Add Parameter Fields
                    ParameterDiscreteValue objDiscreteValue;
                    ParameterField objParameterField;
                    objDiscreteValue = new ParameterDiscreteValue();
                    objDiscreteValue.Value = ConfigurationManager.AppSettings["LabName"];
                    objParameterField = crystalReportViewer1.ParameterFieldInfo["LabName"];
                    objParameterField.CurrentValues.Add(objDiscreteValue);
                    crystalReportViewer1.ParameterFieldInfo.Add(objParameterField);

                    ParameterDiscreteValue objDiscreteValue1;
                    ParameterField objParameterField1;
                    objDiscreteValue1 = new ParameterDiscreteValue();
                    objDiscreteValue1.Value = ConfigurationManager.AppSettings["Address1"];
                    objParameterField1 = crystalReportViewer1.ParameterFieldInfo["Address1"];
                    objParameterField1.CurrentValues.Add(objDiscreteValue1);
                    crystalReportViewer1.ParameterFieldInfo.Add(objParameterField1);

                    ParameterDiscreteValue objDiscreteValue2 = new ParameterDiscreteValue(); ;
                    ParameterField objParameterField2;
                    objDiscreteValue2.Value = ConfigurationManager.AppSettings["Address2"];
                    objParameterField2 = crystalReportViewer1.ParameterFieldInfo["Address2"];
                    objParameterField2.CurrentValues.Add(objDiscreteValue2);
                    crystalReportViewer1.ParameterFieldInfo.Add(objParameterField2);

                    ParameterDiscreteValue objDiscreteValue3 = new ParameterDiscreteValue(); ;
                    ParameterField objParameterField3;
                    objDiscreteValue3.Value = ConfigurationManager.AppSettings["Address3"];
                    objParameterField3 = crystalReportViewer1.ParameterFieldInfo["Address3"];
                    objParameterField3.CurrentValues.Add(objDiscreteValue3);
                    crystalReportViewer1.ParameterFieldInfo.Add(objParameterField3);

                    crystalReportViewer1.ParameterFieldInfo.Add(objParameterField);
                }
                else
                {
                    MessageBox.Show("Result Entry is not found!", "View Lab Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
