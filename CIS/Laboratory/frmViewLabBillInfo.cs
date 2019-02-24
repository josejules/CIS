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

namespace CIS.Laboratory
{
    public partial class frmViewLabBillInfo : Form
    {
        DataTable dtLabInfo = new DataTable();
        DataView dvFilter = new DataView();
        public frmViewLabBillInfo()
        {
            InitializeComponent();
        }

        private void frmViewLabBillInfo_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                ComArugments args = new ComArugments();
                args.ParamList[clsLabCommon.Laboratory.LabBillInfo.FromBillDate] = dtpFromDate.Value;
                args.ParamList[clsLabCommon.Laboratory.LabBillInfo.ToBillDate] = dtpToDate.Value;
                dtLabInfo = new clsLabBusinessFacade().LoadLabBillInfo(args);
                dgvViewLabBills.DataSource = dtLabInfo;
                dgvViewLabBills.Columns["bill_id"].Visible = false;
                dgvViewLabBills.Columns["result_entry_id"].Visible = false;

                //Bind Search ComboBox
                DataTable dt = new DataTable();
                dt.Columns.Add("HeaderType", typeof(string));
                dt.Columns.Add("HeaderText", typeof(string));

                foreach (DataGridViewColumn column in dgvViewLabBills.Columns)
                {
                    if (column.Visible)
                        dt.Rows.Add(column.HeaderText, column.ValueType.Name);
                }
                cboGridHeaders.DataSource = dt;
                cboGridHeaders.DisplayMember = "HeaderType";
                cboGridHeaders.ValueMember = "HeaderText";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchField()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtSearchText.Text))
                {
                    dvFilter = new DataView(dtLabInfo);
                    if (cboGridHeaders.SelectedValue.ToString() == "Int32")
                        dvFilter.RowFilter = string.Format(cboGridHeaders.Text + " = {0}", txtSearchText.Text);
                    else
                        dvFilter.RowFilter = string.Format("[" + cboGridHeaders.Text + "] LIKE '%{0}%'", txtSearchText.Text);
                    dgvViewLabBills.DataSource = dvFilter;
                }
                else
                    dgvViewLabBills.DataSource = dtLabInfo;
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            SearchField();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            LoadData();
            SearchField();
        }

        private void dgvViewLabBills_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvViewLabBills.Rows.Count > 0 && dgvViewLabBills.Columns.Count > 0)
            {
                foreach (DataGridViewRow row in dgvViewLabBills.Rows)
                {
                    if (!string.IsNullOrEmpty(row.Cells["result_entry_id"].Value.ToString()))
                        row.DefaultCellStyle.BackColor = Color.White;
                    else
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))); ;
                }
            }
        }

        private void dgvViewLabBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmResultEntry frmObj = new frmResultEntry();
            frmObj.LoadPatientDetails(SelectID());
            frmObj.ShowDialog();
        }

        public int SelectID()
        {
            int selCustomerId = 0;
            foreach (DataGridViewRow dgvRow in dgvViewLabBills.SelectedRows)
            {
                selCustomerId = int.Parse(dgvRow.Cells["bill_id"].Value.ToString());
            }
            return selCustomerId;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvViewLabBills.SelectedRows.Count > 0)
                {
                    string resultId = dgvViewLabBills.SelectedRows[0].Cells["result_entry_id"].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(resultId))
                    {
                        int billId = Convert.ToInt32(dgvViewLabBills.SelectedRows[0].Cells["bill_id"].Value);
                        frmResultEntryReport frmObj = new frmResultEntryReport(billId);
                        frmObj.ShowDialog();
                    }
                    else
                        MessageBox.Show("Result Entry is not found!", "View Result Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Please select any Bill!", "View Result Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message, "View Result Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBillPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvViewLabBills.SelectedRows.Count > 0)
                {
                    CIS.BillTemplates.frmPrintReceipt frmPhaRececipt = new BillTemplates.frmPrintReceipt("InvestigationBill", dgvViewLabBills.SelectedRows[0].Cells["Bill Number"].Value.ToString(),
                        dgvViewLabBills.SelectedRows[0].Cells["Patient Id"].Value.ToString());
                    frmPhaRececipt.ShowDialog();
                }
                else
                    MessageBox.Show("Please select any Bill!", "View Result Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message, "View Result Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
