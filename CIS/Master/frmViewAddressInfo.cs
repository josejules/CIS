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

namespace CIS.Master
{
    public partial class frmViewAddressInfo : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public frmViewAddressInfo()
        {
            InitializeComponent();
            cboFilter.SelectedIndex = 0;
        }
        #endregion

        #region Events
        private void frmViewState_Load(object sender, EventArgs e)
        {
            ViewAddressInfo();
            dgvAddressInfo.Columns[1].Visible = false;
            dgvAddressInfo.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAddressInfo.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAddressInfo.Columns[0].Width = 60;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddAddressInfo ObjAdd = new frmAddAddressInfo();
            ObjAdd.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Common.Common.id = getAddressInfoId();

            frmAddAddressInfo ObjAdd = new frmAddAddressInfo(Common.Common.id);
            ObjAdd.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAddressInfo.Rows.Count > 0)
            {
                if (MessageBox.Show("Are you sure to delete the record?", "Address Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        int getId = getAddressInfoId();
                        Common.Common.flag = objBusinessFacade.deleteAddressInfo(getId);

                        if (Common.Common.flag == 1)
                        {
                            MessageBox.Show("Record Deleted.....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Address Info Can't be Deleted. It has association", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //throw;
                    }
                }
            }

            else
            {
                MessageBox.Show("Records are not Available to Delete.....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtSearch.Text)))
            {
                string filterField = cboFilter.SelectedItem.ToString();
                dtSource.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", filterField, txtSearch.Text);
                dgvAddressInfo.DataSource = dtSource;
            }
        }
        #endregion

        #region Functions
        public void ViewAddressInfo()
        {
            try
            {
                dtSource = objBusinessFacade.getAddressInfoDetails();
                dgvAddressInfo.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }

            if (dgvAddressInfo.Rows.Count <= 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
        }

        public int getAddressInfoId()
        {
            if (dgvAddressInfo.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select Row to Delete .....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Common.Common.id = int.Parse(dgvAddressInfo.SelectedRows[0].Cells["address_id"].Value.ToString());
            }
            return Common.Common.id;
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewAddressInfo();
        }
    }
}
