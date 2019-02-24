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
    public partial class frmViewCorporate : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public frmViewCorporate()
        {
            InitializeComponent();
            cboFilter.SelectedIndex = 0;
        }
        #endregion

        #region Events
        private void frmViewCorporate_Load(object sender, EventArgs e)
        {
            ViewCorporate();
            dgvCorporate.Columns[1].Visible = false;
            dgvCorporate.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCorporate.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCorporate.Columns[0].Width = 60;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddCorporate ObjAdd = new frmAddCorporate();
            ObjAdd.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Common.Common.id = getCorporateId();

            frmAddCorporate ObjAdd = new frmAddCorporate(Common.Common.id);
            ObjAdd.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCorporate.Rows.Count > 0)
            {
                if (MessageBox.Show("Are you sure to delete the record?", "Corporate", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        int getId = getCorporateId();
                        Common.Common.flag = objBusinessFacade.deleteCorporate(getId);

                        if (Common.Common.flag == 1)
                        {
                            MessageBox.Show("Record Deleted.....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Corporate Can't be Deleted. It has association", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dgvCorporate.DataSource = dtSource;
            }
        }
        #endregion

        #region Functions
        public void ViewCorporate()
        {
            try
            {
                dtSource = objBusinessFacade.getCorporateDetails();
                dgvCorporate.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }

            if (dgvCorporate.Rows.Count <= 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
        }

        public int getCorporateId()
        {
            if (dgvCorporate.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select Row to Delete .....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Common.Common.id = int.Parse(dgvCorporate.SelectedRows[0].Cells["corporate_id"].Value.ToString());
            }
            return Common.Common.id;
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewCorporate();
        }
    }
}
