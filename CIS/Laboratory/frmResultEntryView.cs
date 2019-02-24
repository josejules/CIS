using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CIS.BusinessFacade;

namespace CIS.Laboratory
{
    public partial class frmResultEntryView : Form
    {
        public string criteria = string.Empty;
        public int BillId = 0;
        DataTable dtPatient = new DataTable();
        public frmResultEntryView()
        {
            InitializeComponent();
            //dtpBillDate.MaxDate = DateTime.Now.Date;
        }

        private void LoadPatientDetails()
        {
            try
            {
                dtPatient = new clsLabBusinessFacade().LoadPatientDetails(criteria);
                if (dtPatient.Rows.Count > 0)
                {
                    BillId = Convert.ToInt32(dtPatient.Rows[0]["bill_id"].ToString());
                    cboBillNo.DataSource = dtPatient;
                    cboBillNo.ValueMember = "bill_id";
                    cboBillNo.DisplayMember = "bill_number";
                    //txtRegNo.Text = dtPatient.Rows[0]["patient_id"].ToString();
                    txtVisitNo.Text = dtPatient.Rows[0]["visit_number"].ToString();
                    txtPatientName.Text = dtPatient.Rows[0]["patient_name"].ToString();
                    dtpBillDate.Value = Convert.ToDateTime(dtPatient.Rows[0]["bill_date"].ToString());
                    txtAge.Text = dtPatient.Rows[0]["age_year"].ToString();
                    txtSex.Text = dtPatient.Rows[0]["gender"].ToString();
                    int billId = Convert.ToInt32(dtPatient.Rows[0]["bill_id"].ToString());
                    dgvResultEntryView.DataSource = new clsLabBusinessFacade().LoadTestDetails(billId);
                    dgvResultEntryView.Columns[0].Visible = false;
                }
                else
                    MessageBox.Show("Please enter valid Patient Id or No bills found!", "Result Entry View", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Result Entry View", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRegNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtRegNo.Text))
            {
                criteria = "patient_id = '" + txtRegNo.Text.Trim() + "'";
                LoadPatientDetails();
            }
        }

        private void txtRegNo_Enter(object sender, EventArgs e)
        {
            
        }

        private void cboBillNo_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtRegNo.Text))
            {
                criteria = "patient_id = '" + txtRegNo.Text.Trim() + "'";
                LoadPatientDetails();
            }
        }

        private void cboBillNo_Leave(object sender, EventArgs e)
        {
            LoadPatientDetails_BillNumber();
        }

        public void LoadPatientDetails_BillNumber()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(cboBillNo.Text))
                {
                    criteria = "bill_number = '" + cboBillNo.Text.Trim() + "'";
                    dtPatient = new clsLabBusinessFacade().LoadPatientDetails(criteria);
                    if (dtPatient.Rows.Count > 0)
                    {
                        BillId = Convert.ToInt32(dtPatient.Rows[0]["bill_id"].ToString());
                        txtRegNo.Text = dtPatient.Rows[0]["patient_id"].ToString();
                        txtVisitNo.Text = dtPatient.Rows[0]["visit_number"].ToString();
                        txtPatientName.Text = dtPatient.Rows[0]["patient_name"].ToString();
                        dtpBillDate.Value = Convert.ToDateTime(dtPatient.Rows[0]["bill_date"].ToString());
                        txtAge.Text = dtPatient.Rows[0]["age_year"].ToString();
                        txtSex.Text = dtPatient.Rows[0]["gender"].ToString();
                        int billId = Convert.ToInt32(dtPatient.Rows[0]["bill_id"].ToString());
                        dgvResultEntryView.DataSource = new clsLabBusinessFacade().LoadTestDetails(billId);
                        dgvResultEntryView.Columns[0].Visible = false;
                    }
                    else
                        MessageBox.Show("Please enter valid Bill Number", "Result Entry View", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Result Entry View", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResultEntry_Click(object sender, EventArgs e)
        {
            frmResultEntry frmObj = new frmResultEntry();
            frmObj.LoadPatientDetails(BillId);
            frmObj.ShowDialog();
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            CIS.Modules.cisSearchPatient objShow = new CIS.Modules.cisSearchPatient(this,"ResultEntryView");
            objShow.ShowDialog();
        }

        private void cboBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadPatientDetails_BillNumber();
            }
        }

        private void txtRegNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtRegNo.Text))
                {
                    criteria = "patient_id = '" + txtRegNo.Text.Trim() + "'";
                    LoadPatientDetails();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRegNo.Text = string.Empty;
            txtVisitNo.Text = string.Empty;
            txtPatientName.Text = string.Empty;
            txtSex.Text = string.Empty;
            txtAge.Text = string.Empty;
            dtpBillDate.Value = DateTime.Now.Date;
            cboBillNo.DataSource = null;
            dgvResultEntryView.DataSource = null;
        }
    }
}
