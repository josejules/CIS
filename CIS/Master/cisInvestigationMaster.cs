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
    public partial class cisInvestigationMaster : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public cisInvestigationMaster()
        {
            InitializeComponent();
            //cboFilter.SelectedIndex = 0;
        }
        #endregion

        #region Events
        private void cisInvestigationMaster_Load(object sender, EventArgs e)
        {
            ViewInvestigationCategory();
            dgvInvestigationMaster.Columns[1].Visible = false;
            dgvInvestigationMaster.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInvestigationMaster.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInvestigationMaster.Columns[0].Width = 60;
            tcInvestigationMaster_SelectedIndexChanged(sender, e);
        }

        private void tcInvestigationMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvInvestigationMaster.DataSource = null;

            Common.Common.cis_investigation_info.selectedInvTab = Convert.ToString(tcInvestigationMaster.SelectedTab.Name);

            switch (Common.Common.cis_investigation_info.selectedInvTab)
            {
                case "tpInvCategory":
                    ViewInvestigationCategory();
                    string[] invCate = { "Investigation Category", "Status" };
                    cboFilter.DataSource = invCate;
                    break;

                case "tpInvList":
                    ViewInvestigationList();
                    string[] invList = { "Investigation Code", "Investigation Name", "Investigation Category", "Department", "Status" };
                    cboFilter.DataSource = invList;
                    break;

                default:
                    break;
            }

            if (dgvInvestigationMaster.Rows.Count <= 0)
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                dgvInvestigationMaster.Columns[1].Visible = false;
                dgvInvestigationMaster.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvInvestigationMaster.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvInvestigationMaster.Columns[0].Width = 60;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Common.Common.cis_investigation_info.selectedInvTab = Convert.ToString(tcInvestigationMaster.SelectedTab.Name);

            switch (Common.Common.cis_investigation_info.selectedInvTab)
            {
                case "tpInvCategory":
                    cisAddInvCategory ObjICAdd = new cisAddInvCategory();
                    ObjICAdd.ShowDialog();
                    break;

                case "tpInvList":
                    cisAddInvList ObjILAdd = new cisAddInvList();
                    ObjILAdd.ShowDialog();
                    break;

                default:
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Common.Common.id = getInvId();

            Common.Common.cis_investigation_info.selectedInvTab = Convert.ToString(tcInvestigationMaster.SelectedTab.Name);

            switch (Common.Common.cis_investigation_info.selectedInvTab)
            {
                case "tpInvCategory":
                    cisAddInvCategory ObjICAdd = new cisAddInvCategory(Common.Common.id);
                    ObjICAdd.ShowDialog();
                    break;

                case "tpInvList":
                    cisAddInvList ObjILAdd = new cisAddInvList(Common.Common.id);
                    ObjILAdd.ShowDialog();
                    break;

                default:
                    break;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInvestigationMaster.Rows.Count > 0)
            {
                if (MessageBox.Show("Are you sure to delete the record?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Common.Common.cis_investigation_info.selectedInvTab = Convert.ToString(tcInvestigationMaster.SelectedTab.Name);
                        int getId = getInvId();

                        switch (Common.Common.cis_investigation_info.selectedInvTab)
                        {
                            case "tpInvCategory":
                                Common.Common.flag = objBusinessFacade.deleteInvCategory(getId);
                                break;

                            case "tpInvList":
                                Common.Common.flag = objBusinessFacade.deleteInvestigation(getId);
                                break;

                            default:
                                break;
                        }

                        if (Common.Common.flag == 1)
                        {
                            MessageBox.Show("Record Deleted.....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Record can't be deleted. It has association", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dgvInvestigationMaster.DataSource = dtSource;
            }
        }
        #endregion

        #region Functions
        public void ViewInvestigationCategory()
        {
            try
            {
                dtSource = objBusinessFacade.getInvestigationCategoryDetails();
                dgvInvestigationMaster.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ViewInvestigationList()
        {
            try
            {
                dtSource = objBusinessFacade.getInvestigationDetails();
                dgvInvestigationMaster.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int getInvId()
        {
            if (dgvInvestigationMaster.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select Row to Delete .....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Common.Common.cis_investigation_info.selectedInvTab = Convert.ToString(tcInvestigationMaster.SelectedTab.Name);

                switch (Common.Common.cis_investigation_info.selectedInvTab)
                {
                    case "tpInvCategory":
                        Common.Common.id = int.Parse(dgvInvestigationMaster.SelectedRows[0].Cells["inv_category_id"].Value.ToString());
                        break;

                    case "tpInvList":
                        Common.Common.id = int.Parse(dgvInvestigationMaster.SelectedRows[0].Cells["investigation_id"].Value.ToString());
                        break;

                    default:
                        break;
                }
            }
            return Common.Common.id;
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tcInvestigationMaster_SelectedIndexChanged(sender, e);
        }
    }
}
