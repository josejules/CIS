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
using CIS;

namespace CIS.Modules
{
    public partial class cisSearchPatient : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        CISRegAndInvoice frmReg = null;
        Laboratory.frmResultEntryView frmLabView = null;
        private string Activity = string.Empty;
        #endregion

        public cisSearchPatient()
        {
            InitializeComponent();
        }
        public cisSearchPatient(CISRegAndInvoice frmObj)
        {
            frmReg = frmObj;
            InitializeComponent();
        }
        public cisSearchPatient(Form frmObj, string activity)
        {
            Activity = activity;
            switch (Activity)
            {
                case "RegAndInvoice":
                    frmReg = (CISRegAndInvoice)frmObj;
                    break;
                case "ResultEntryView":
                    frmLabView = (Laboratory.frmResultEntryView)frmObj;
                    break;
            }
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query= string.Empty;
            query = (txtPatientId.Text == string.Empty) ? "" : "patient_id like '%" + txtPatientId.Text + "%' and ";
            query += "gender = " + Convert.ToInt32(cboGender.SelectedIndex) + " and ";
            query += (txtPatientName.Text == string.Empty) ? "" : "patient_name like '%" + Convert.ToString(txtPatientName.Text.ToString()) + "%' and ";
            query += (txtGuardainName.Text == string.Empty) ? "" : "guardian_name like '%" + Convert.ToString(txtGuardainName.Text.ToString()) + "%' and ";
            query += (txtAddress.Text == string.Empty) ? "" : "address like '%" + Convert.ToString(txtAddress.Text.ToString()) + "%' and ";
            query += (txtPhoneNo.Text == string.Empty) ? "" : "phone_no like '%" + Convert.ToString(txtPhoneNo.Text.ToString()) + "%' and ";

            query = query.Remove(query.Length-5,  5) + " limit 100";

            try
            {
                dtSource = objBusinessFacade.searchPatientInfo(query);
                if (dtSource.Rows.Count > 0)
                {
                    dgvPatientDetails.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearSearchRecords();
        }

        private void clearSearchRecords()
        {
            txtPatientId.Text = string.Empty;
            txtPatientName.Text = string.Empty;
            cboGender.SelectedIndex = 0;
            txtGuardainName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
        }

        private void cisSearchPatient_Load(object sender, EventArgs e)
        {
            cboGender.SelectedIndex = 0;
        }

        private void dgvPatientDetails_DoubleClick(object sender, EventArgs e)
        {
            switch (Activity)
            {
                case "ResultEntryView":
                    frmLabView.txtRegNo.Text = dgvPatientDetails.SelectedRows[0].Cells[1].Value.ToString();
                    break;
                case "RegAndInvoice":
                    this.frmReg.txtPatientId.Text = dgvPatientDetails.SelectedRows[0].Cells[1].Value.ToString();
                    this.frmReg.txtPatientId_function();
                    //this.frmReg.txtPatientId.Focus();
                    break;
            }
            this.Hide();
        }
    }
}
