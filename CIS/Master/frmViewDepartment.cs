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

namespace CIS
{
    public partial class frmViewDepartment : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public frmViewDepartment()
        {
            InitializeComponent();
            cboFilter.SelectedIndex = 0;
        }
        #endregion

        #region Events
        private void frmViewDepartment_Load(object sender, EventArgs e)
        {
            ViewDepartment();
            dgvDepartment.Columns[1].Visible = false;
            dgvDepartment.Columns[2].Visible = false;
            dgvDepartment.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDepartment.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDepartment.Columns[0].Width = 50;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddDepartment ObjAdd = new frmAddDepartment();
            ObjAdd.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDepartment.Rows.Count > 0)
            {
                if (MessageBox.Show("Are you sure to delete the record?", "Department", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        int getId = getDepartmentId();
                        Common.Common.cis_department.departmentCategoryId = getDepartmentCategoryId();
                        Common.Common.cis_department.departmentCategory = getDepartmentType();

                        if ((Common.Common.cis_department.departmentCategoryId == 1 || Common.Common.cis_department.departmentCategoryId == 2 || Common.Common.cis_department.departmentCategoryId == 4 || Common.Common.cis_department.departmentCategoryId == 5) && (Common.Common.cis_department.departmentCategory == "Non-Medical"))
                        {
                            MessageBox.Show("Department Can't be deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        else
                        {
                            Common.Common.flag = objBusinessFacade.deleteDepartment(getId);
                        }

                        if (Common.Common.flag == 1)
                        {
                            MessageBox.Show("Record Deleted.....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Department Can't be Deleted. It has association", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //throw;
                    }
                }
            }

            else
            {
                MessageBox.Show("Records are not Available to Delete.....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Common.Common.id = getDepartmentId();
            Common.Common.cis_department.departmentCategoryId = getDepartmentCategoryId();
            Common.Common.cis_department.departmentCategory = getDepartmentType();
            if ((Common.Common.cis_department.departmentCategoryId == 1 || Common.Common.cis_department.departmentCategoryId == 2 || Common.Common.cis_department.departmentCategoryId == 4 || Common.Common.cis_department.departmentCategoryId == 5) && (Common.Common.cis_department.departmentCategory == "Non-Medical"))
            {
                MessageBox.Show("Department Can't be Edited", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                frmAddDepartment ObjAdd = new frmAddDepartment(Common.Common.id);
                ObjAdd.ShowDialog();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtSearch.Text.Trim())))
            {
                string filterField = cboFilter.SelectedItem.ToString();
                dtSource.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", filterField, txtSearch.Text);
                dgvDepartment.DataSource = dtSource;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Functions
        public void ViewDepartment()
        {
            try
            {
                dtSource = objBusinessFacade.getDepartmentDetails();
                dgvDepartment.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }

            if (dgvDepartment.Rows.Count <= 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
        }

        public int getDepartmentId()
        {
            if (dgvDepartment.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select Row to Delete .....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Common.Common.id = int.Parse(dgvDepartment.SelectedRows[0].Cells["DEPARTMENT_ID"].Value.ToString());
            }
            return Common.Common.id;
        }

        public int getDepartmentCategoryId()
        {
            if (dgvDepartment.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select Row to Delete .....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Common.Common.id = int.Parse(dgvDepartment.SelectedRows[0].Cells["DEPARTMENT_CATEGORY_ID"].Value.ToString());
            }
            return Common.Common.id;
        }

        public string getDepartmentType()
        {
            if (dgvDepartment.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select Row to Delete .....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Common.Common.cis_department.departmentCategory = (dgvDepartment.SelectedRows[0].Cells["Department Type"].Value.ToString());
            }
            return Common.Common.cis_department.departmentCategory;
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewDepartment();
        }
    }
}
