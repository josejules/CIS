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
    public partial class frmViewDoctor : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public frmViewDoctor()
        {
            InitializeComponent();
            cboFilter.SelectedIndex = 0;
        }
        #endregion

        #region Events
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddDoctor ObjAdd = new frmAddDoctor();
            ObjAdd.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Common.Common.id = getDoctorId();

            frmAddDoctor ObjAdd = new frmAddDoctor(Common.Common.id);
            ObjAdd.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDoctor.Rows.Count > 0)
            {
                if (MessageBox.Show("Are you sure to delete the record?", "Doctor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        int getId = getDoctorId();
                        Common.Common.flag = objBusinessFacade.deleteDoctor(getId);

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

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtSearch.Text)))
            {
                string filterField = cboFilter.SelectedItem.ToString();
                dtSource.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", filterField, txtSearch.Text);
                dgvDoctor.DataSource = dtSource;
            }
        }

        private void frmViewDoctor_Load(object sender, EventArgs e)
        {
            ViewDoctor();
            dgvDoctor.Columns[1].Visible = false;
            dgvDoctor.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDoctor.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDoctor.Columns[0].Width = 50;
            dgvDoctor.Columns[2].Width = 75;
            dgvDoctor.Columns[3].Width = 135;
        }
        #endregion

        #region Functions
        public void ViewDoctor()
        {
            try
            {
                dtSource = objBusinessFacade.getDoctorDetails();
                dgvDoctor.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }

            if (dgvDoctor.Rows.Count <= 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
        }

        public int getDoctorId()
        {
            if (dgvDoctor.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select Row to Delete .....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Common.Common.id = int.Parse(dgvDoctor.SelectedRows[0].Cells["DOCTOR_ID"].Value.ToString());
            }
            return Common.Common.id;
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewDoctor();
        }
    }
}
