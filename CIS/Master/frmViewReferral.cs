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
    public partial class frmViewReferral : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public frmViewReferral()
        {
            InitializeComponent();
            cboFilter.SelectedIndex = 0;
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cisAddReferral ObjAdd = new cisAddReferral();
            ObjAdd.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Common.Common.id = getReferralId();

            cisAddReferral ObjAdd = new cisAddReferral(Common.Common.id);
            ObjAdd.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvReferral.Rows.Count > 0)
            {
                if (MessageBox.Show("Are you sure to delete the record?", "Doctor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        int getId = getReferralId();
                        Common.Common.flag = objBusinessFacade.deleteReferral(getId);

                        if (Common.Common.flag == 1)
                        {
                            MessageBox.Show("Record Deleted.....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Doctor Can't be Deleted. It has association", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewReferral();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtSearch.Text)))
            {
                string filterField = cboFilter.SelectedItem.ToString();
                dtSource.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", filterField, txtSearch.Text);
                dgvReferral.DataSource = dtSource;
            }
        }

        private void frmViewReferral_Load(object sender, EventArgs e)
        {
            ViewReferral();
            dgvReferral.Columns[1].Visible = false;
            dgvReferral.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReferral.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvReferral.Columns[0].Width = 50;
            dgvReferral.Columns[2].Width = 75;
            dgvReferral.Columns[3].Width = 135;
        }

        #region Functions
        public void ViewReferral()
        {
            try
            {
                dtSource = objBusinessFacade.getReferralDetails();
                dgvReferral.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }

            if (dgvReferral.Rows.Count <= 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
        }

        public int getReferralId()
        {
            if (dgvReferral.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select Row to Delete .....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Common.Common.id = int.Parse(dgvReferral.SelectedRows[0].Cells["REFERRAL_ID"].Value.ToString());
            }
            return Common.Common.id;
        }
        #endregion
    }
}
