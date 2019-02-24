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
    public partial class frmLoadLabData : Form
    {
        #region Variables
        clsLabBusinessFacade objLabBusiness = new clsLabBusinessFacade();
        DataTable dtSource = null;
        DataView dv = new DataView();
        public string activity = string.Empty;
        //ComArugments objArg = new ComArugments();
        #endregion
        public frmLoadLabData()
        {
            InitializeComponent();
        }

        public frmLoadLabData(string action)
        {
            try
            {
                InitializeComponent();
                activity = action;
                LoadData();
            }
            catch (Exception ex)
            {
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        public void LoadData()
        {
            switch (activity)
            {
                case "Category":
                    dtSource = objLabBusiness.LoadCategory();
                    break;
                case "TestField":
                    dtSource = objLabBusiness.LoadTestFields();
                    break;
                case "UserRole":
                    dtSource = objLabBusiness.LoadUserRole();
                    break;
                case "User":
                    dtSource = objLabBusiness.LoadUsers();
                    break;
                case "PrinterSetting":
                    dtSource = objLabBusiness.LoadPrinterSetting();
                    break;
            }

            //DataColumn column1 = new DataColumn();
            //column1.DataType = System.Type.GetType("System.Int32");
            //column1.AutoIncrement = true;
            //column1.AutoIncrementSeed = 1;
            //column1.AutoIncrementStep = 1;
            //dtSource.Columns.Add(column1);
            dtSource = new BusinessFacade.BusinessFacade().AssignRowNo(dtSource);
            dgvLoadLabData.DataSource = dtSource;
            bindingSource1.DataSource = dtSource;
            dgvLoadLabData.Columns[1].Visible = false;

            DataTable dt = new DataTable();
            dt.Columns.Add("HeaderType", typeof(string));
            dt.Columns.Add("HeaderText", typeof(string));

            foreach (DataGridViewColumn column in dgvLoadLabData.Columns)
            {
                if(column.Visible)
                    dt.Rows.Add(column.HeaderText, column.ValueType.Name);
            }
            cboGridHeaders.DataSource = dt;
            cboGridHeaders.DisplayMember = "HeaderType";
            cboGridHeaders.ValueMember = "HeaderText";
            
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            switch (activity)
            {
                case "Category":
                    new Master.frmCategory().ShowDialog();
                    break;
                case "TestField":
                    new Master.frmTestField().ShowDialog();
                    break;
                case "UserRole":
                    new User.frmUserRole().ShowDialog();
                    break;
                case "User":
                    new User.frmUser().ShowDialog();
                    break;
                case "PrinterSetting":
                    new CIS.Master.frmPrinterSettings().ShowDialog();
                    break;
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            switch (activity)
            {
                case "Category":
                    new Master.frmCategory(SelectID()).Show();
                    break;
                case "TestField":
                    new Master.frmTestField(SelectID()).Show();
                    break;
                case "UserRole":
                    new User.frmUserRole(SelectID()).Show();
                    break;
                case "User":
                    new User.frmUser(SelectID()).Show();
                    break;
                case "PrinterSetting":
                    new CIS.Master.frmPrinterSettings(SelectID()).Show();
                    break;
            }
        }

        /// <summary>
        /// This is to select the Id in the Grid View
        /// </summary>
        /// <returns></returns>
        public int SelectID()
        {
            int selCustomerId = 0;
            foreach (DataGridViewRow dgvRow in dgvLoadLabData.SelectedRows)
            {
                selCustomerId = int.Parse(dgvRow.Cells[1].Value.ToString());
            }
            return selCustomerId;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            int iDeleted = 0;
            if (MessageBox.Show("Are you sure delete selected  Info ?", Messages.Msg_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    switch (activity)
                    {
                        case "Category":
                            iDeleted = new clsLabBusinessFacade().DeleteCategory(SelectID());
                            break;
                        case "TestField":
                            iDeleted = new clsLabBusinessFacade().DeleteTestField(SelectID());
                            break;
                        case "UserRole":
                            iDeleted = new clsLabBusinessFacade().DeleteUserRole(SelectID());
                            break;
                        case "User":
                            iDeleted = new clsLabBusinessFacade().DeleteUser(SelectID());
                            break;
                        case "PrinterSetting":
                            iDeleted = new clsLabBusinessFacade().DeletePrinterSetting(SelectID());
                            break;
                    }
                }

                catch (Exception)
                {
                    MessageBox.Show(activity + " Can't be deleted. It has an association", Messages.Msg_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //throw;
                }

                if (iDeleted > 0)
                    MessageBox.Show(activity + " is deleted ", Messages.Msg_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                /*else
                    MessageBox.Show(activity + " is not deleted", Messages.Msg_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);*/
                LoadData();
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            SearchField();
        }

        private void SearchField()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtSearchText.Text))
                {
                    dv = new DataView(dtSource);
                    if (cboGridHeaders.SelectedValue.ToString() == "Int32")
                        dv.RowFilter = string.Format(cboGridHeaders.Text + " = {0}", txtSearchText.Text);
                    else
                        dv.RowFilter = string.Format("[" +cboGridHeaders.Text + "] LIKE '%{0}%'", txtSearchText.Text);
                    dgvLoadLabData.DataSource = dv;
                }
                else
                    dgvLoadLabData.DataSource = dtSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchField();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvLoadLabData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Edit();
        }
    }
}
