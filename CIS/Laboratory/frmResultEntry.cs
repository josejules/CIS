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
using System.Linq;

namespace CIS.Laboratory
{
    public partial class frmResultEntry : Form
    {
        DataTable dtPatient = new DataTable();
        DataTable dtResultEntry = new DataTable();
        clsLabBusinessFacade objBusiness = new clsLabBusinessFacade();
        Dictionary<int, int> gboxHeightStatic = new Dictionary<int, int>();
        Dictionary<int, int> gboxYvalue = new Dictionary<int, int>();
        public string criteria = string.Empty;
        public int ResultEntryId = 0;
        public int BillId = 0;
        int editMode = 0;
        public frmResultEntry()
        {
            InitializeComponent();
        }

        public void LoadPatientDetails(int billId)
        {
            try
            {
                BillId = billId;
                criteria = "bi.bill_id = " + billId;
                dtPatient = new clsLabBusinessFacade().LoadPatientDetails(criteria);
                if (dtPatient.Rows.Count > 0)
                {
                    txtBillNo.Text = dtPatient.Rows[0]["bill_number"].ToString();
                    txtRegNo.Text = dtPatient.Rows[0]["patient_id"].ToString();
                    txtVisitNo.Text = dtPatient.Rows[0]["visit_number"].ToString();
                    txtPatientName.Text = dtPatient.Rows[0]["patient_name"].ToString();
                    dtpBillDate.Value = Convert.ToDateTime(dtPatient.Rows[0]["bill_date"].ToString());
                    txtAge.Text = dtPatient.Rows[0]["age_year"].ToString();
                    txtSex.Text = dtPatient.Rows[0]["gender"].ToString();
                    dtpReceivedDateTime.Value = (dtPatient.Rows[0]["received_date_time"].ToString() ==string.Empty)?DateTime.Now:Convert.ToDateTime(dtPatient.Rows[0]["received_date_time"].ToString());
                    dtpReportedDateTime.Value = (dtPatient.Rows[0]["reported_date_time"].ToString() == string.Empty) ? DateTime.Now : Convert.ToDateTime(dtPatient.Rows[0]["reported_date_time"].ToString());
                    ResultEntryId = (dtPatient.Rows[0]["result_entry_id"].ToString() == string.Empty) ? 0 : Convert.ToInt32(dtPatient.Rows[0]["result_entry_id"].ToString());
                    
                    if (ResultEntryId == 0)
                        dtResultEntry = new clsLabBusinessFacade().LoadResultEntryInfo(BillId);
                    else
                        dtResultEntry = new clsLabBusinessFacade().EditResultEntryInfo(ResultEntryId);
                    ContstuctGroupBoxHeight();
                    LoadResultEntries();
                }
                else
                    MessageBox.Show("Not valid Bill Number!", "Result Entry View", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Result Entry View", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ContstuctGroupBoxHeight()
        {
            gboxHeightStatic.Add(1, 65);
            gboxHeightStatic.Add(2, 90);
            gboxHeightStatic.Add(3, 120);
            gboxHeightStatic.Add(4, 160);
            gboxHeightStatic.Add(5, 180);
            gboxHeightStatic.Add(6, 220);
            gboxHeightStatic.Add(7, 267);
            gboxHeightStatic.Add(8, 300);
            gboxHeightStatic.Add(9, 330);
            gboxHeightStatic.Add(10, 370);
            gboxHeightStatic.Add(11, 400);
            gboxHeightStatic.Add(12, 430);
            gboxHeightStatic.Add(13, 460);
            gboxHeightStatic.Add(14, 490);
            gboxHeightStatic.Add(15, 520);
            gboxHeightStatic.Add(16, 550);
            gboxHeightStatic.Add(17, 580);
            gboxHeightStatic.Add(18, 610);
            gboxHeightStatic.Add(19, 640);
            gboxHeightStatic.Add(20, 670);
        }

        private void LoadResultEntries()
        {
            GroupBox gbox = new GroupBox();
            if (dtResultEntry.Rows.Count > 0)
            {
                int x_ResultLocation = 305;
                int x_ReferenceLocation = 556;
                int x_UnitLocation = 797;
                int Y_Value = 100; //40;
                int calcHeight = 0;
                string prevTestName = string.Empty;
                string currentTestName =string.Empty;
                
                int gbox_y_location = 40;
                foreach (DataRow row in dtResultEntry.Rows)
                {
                    currentTestName = row["investigation_name"].ToString();
                    if (prevTestName != currentTestName)
                    {
                        Y_Value = 30;
                        gbox = new GroupBox();
                        int calcGboxHeight= Convert.ToInt32(dtResultEntry.Compute("COUNT(investigation_id)", "investigation_id = " + Convert.ToInt32(row["investigation_id"].ToString()) + ""));
                        int rtxtCount = Convert.ToInt32(dtResultEntry.Compute("COUNT(investigation_id)", "investigation_id = " + Convert.ToInt32(row["investigation_id"].ToString()) + " and test_field_type_name = 'RichTextBox'"));
                        int rtxtHeight = 0;
                        if (rtxtCount > 0)
                            rtxtHeight = rtxtCount * 40;
                        gbox.Size = new Size(900, gboxHeightStatic[calcGboxHeight]+rtxtHeight);
                        gbox.ForeColor = System.Drawing.Color.Blue;
                        gbox.Location = new System.Drawing.Point(10, gbox_y_location);
                        gbox_y_location = gbox_y_location + gboxHeightStatic[calcGboxHeight] + rtxtHeight + 10;
                        gbox.Text = row["investigation_name"].ToString();
                        panel4.Controls.Add(gbox);
                        prevTestName = row["investigation_name"].ToString();
                    }

                    Label lblField = new Label();
                    lblField.Name = "lblField" + row["test_field_id"].ToString().Replace(" ", "");
                    lblField.Text = row["test_field_name"].ToString();
                    gbox.Controls.Add(lblField);
                    lblField.ForeColor = System.Drawing.Color.Black;
                    lblField.Location = new System.Drawing.Point(20, Y_Value);

                    Label lblRefValue = new Label();
                    lblRefValue.Name = "lblRF" + row["test_field_id"].ToString().Replace(" ", "");
                    lblRefValue.Text = row["reference_value"].ToString();
                    using (Graphics g = CreateGraphics())
                    {
                        SizeF size = g.MeasureString(lblRefValue.Text, lblRefValue.Font, 495);
                        lblRefValue.Height = (int)Math.Ceiling(size.Height);
                        calcHeight = (lblRefValue.Height > 14) ? lblRefValue.Height-20 : 0;
                        gbox_y_location += calcHeight;
                        gbox.Size = new Size(gbox.Width,gbox.Height+calcHeight); //Increase height of Gbox based on Reference Label Size
                    }
                    gbox.Controls.Add(lblRefValue);
                    lblRefValue.ForeColor = System.Drawing.Color.Black;
                    lblRefValue.Location = new System.Drawing.Point(x_ReferenceLocation, Y_Value);

                    Label lblUnit = new Label();
                    lblUnit.Name = "lblUnit" + row["test_field_id"].ToString().Replace(" ", "");
                    lblUnit.Text = row["unit"].ToString();
                    gbox.Controls.Add(lblUnit);
                    lblUnit.ForeColor = System.Drawing.Color.Black;
                    lblUnit.Location = new System.Drawing.Point(x_UnitLocation, Y_Value);

                    switch (row["test_field_type_name"].ToString())
                    {
                        case "TextBox":
                            TextBox txt = new TextBox();
                            txt.Name = "txt" + row["test_field_id"].ToString() + "_" + row["investigation_id"].ToString();
                            txt.Size = new System.Drawing.Size(170, 21);
                            if (ResultEntryId > 0) //Edit Mode
                                txt.Text = row["result_value"].ToString();
                            gbox.Controls.Add(txt);
                            txt.Location = new System.Drawing.Point(x_ResultLocation, Y_Value);
                            break;
                        case "ComboBox":
                            ComboBox cbo = new ComboBox();
                            cbo.Name = "txt" + row["test_field_id"].ToString() + "_" + row["investigation_id"].ToString();
                            cbo.Size = new System.Drawing.Size(170, 21);
                            gbox.Controls.Add(cbo);
                            cbo.Location = new System.Drawing.Point(x_ResultLocation, Y_Value);
                            cbo.DataSource = new clsLabBusinessFacade().LoadTestItem(Convert.ToInt32(row["test_field_id"].ToString()));
                            cbo.ValueMember = "test_item_id";
                            cbo.DisplayMember = "test_item_name";
                            if (ResultEntryId > 0) //Edit Mode
                                cbo.Text = row["result_value"].ToString();
                            break;
                        case "RichTextBox":
                            RichTextBox rtxt = new RichTextBox();
                            rtxt.Name = "txt" + row["test_field_id"].ToString() + "_" + row["investigation_id"].ToString();
                            rtxt.Size = new System.Drawing.Size(248, 62);
                            if (ResultEntryId > 0) //Edit Mode
                                rtxt.Text = row["result_value"].ToString();
                            gbox.Controls.Add(rtxt);
                            rtxt.Location = new System.Drawing.Point(x_ResultLocation, Y_Value);
                            calcHeight = calcHeight + 40; //Calculate Height - Based on the RichTextBox Height
                            break;
                    }

                    Y_Value = Y_Value + 30 + calcHeight;
                    calcHeight = 0;
                }
                this.Size = new Size(this.Size.Width, (((dtResultEntry.Rows.Count * 20) + 400)>670?670:(dtResultEntry.Rows.Count * 20) + 400));
            }
            else
                MessageBox.Show("Result Entry not found!", "Result Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save_data();
        }

        private void save_data()
        {
            try
            {
                List<LabDynamicControl> lstCtrl = new List<LabDynamicControl>();
                foreach (Control c in panel4.Controls)
                {
                    foreach (Control ctrl in c.Controls)
                    {
                        LabDynamicControl obj = new LabDynamicControl();
                        obj.ctrlName = ctrl.Name;
                        obj.ctrlValue = ctrl.Text;
                        lstCtrl.Add(obj);
                    }
                }
                int flag = 0;
                int iSaved = 0;
                //Save Lab Test Result
                ComArugments args = new ComArugments();
                args.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultEntryId] = ResultEntryId;
                args.ParamList[clsLabCommon.Laboratory.TestResultEntry.BillId] = BillId;
                args.ParamList[clsLabCommon.Laboratory.TestResultEntry.ReceivedDateTime] = dtpReceivedDateTime.Value;
                args.ParamList[clsLabCommon.Laboratory.TestResultEntry.ReportedDateTime] = dtpReportedDateTime.Value;
                if (ResultEntryId == 0)
                    ResultEntryId = objBusiness.SaveResultEntryInfo(args);
                else
                    iSaved = objBusiness.SaveResultEntryInfo(args);
                //Delete all the entries in Test_Result_Entry Table and Save the Details
                int iDeleted = objBusiness.DeletePatientResultEntry(ResultEntryId);
                foreach (DataRow row in dtResultEntry.Rows)
                {
                    LabDynamicControl obj = lstCtrl.Find(ctrl => ctrl.ctrlName.Equals("txt" + row["test_field_id"].ToString() + "_" + row["investigation_id"].ToString()));
                    string resultValue = string.Empty;
                    if (obj != null)
                        resultValue = obj.ctrlValue;

                    ComArugments argsDetail = new ComArugments();
                    argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultEntryId] = ResultEntryId;
                    argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.InvestigationId] = row["investigation_id"].ToString();
                    argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.TestFieldId] = row["test_field_id"].ToString();
                    //argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultValue] = resultValue;
                    argsDetail.ParamList[clsLabCommon.Laboratory.TestResultEntry.ResultValue] = resultValue.Replace("'", "''");
                    flag = objBusiness.SaveResultEntryDetailInfo(argsDetail);
                }
                objBusiness.commitTransction();
                if (flag > 0)
                    MessageBox.Show("Result Entry is saved", "Result Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Result Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objBusiness.rollBackTransation();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmResultEntryReport frmObj = new frmResultEntryReport(BillId);
            frmObj.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Alt | Keys.S:
                    save_data();
                    return true;
                case Keys.Alt | Keys.C:
                    this.Close();
                    return true;
                case Keys.Alt | Keys.P:
                    frmResultEntryReport frmObj = new frmResultEntryReport(BillId);
                    frmObj.ShowDialog();
                    return true;
                case Keys.F2:
                    //do something
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
