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
    public partial class cisViewInventoryMovements : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        ComArugments objArg = new ComArugments();
        #endregion

        public cisViewInventoryMovements()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CIS.Modules.cisInventoryMovements objShow = new CIS.Modules.cisInventoryMovements();
            objShow.ShowDialog();
        }

        private void cisViewInvestoryMovements_Load(object sender, EventArgs e)
        {
            cboFilter.SelectedIndex = 0;
            cboInvestoryMoveType.SelectedIndex = 0;
            this.dtpDateFrom.Value = DateTime.Now;
            this.dtpDateTo.Value = DateTime.Now;
            loadInventoryMovementsDetails();
        }

        private void loadInventoryMovementsDetails()
        {
            //Common.Common.startDate = objBusinessFacade.dateFromValue(dtpDateFrom.Value.Date.ToString("dd-MM-yyyy"));
            //Common.Common.endDate = objBusinessFacade.dateToValue(dtpDateTo.Value.Date.ToString("dd-MM-yyyy"));

            Common.Common.startDate = dtpDateFrom.Value.ToString("yyyyMMdd" + "000000"); //Changed by Jules Inorder to avoid datetime exception in diff system
            Common.Common.endDate = dtpDateTo.Value.ToString("yyyyMMdd" + "235959");
            Common.Common.cis_pharmacy_info.investoryMoveTypeId = cboInvestoryMoveType.SelectedIndex;

            dgvViewInventoryMovements.DataSource = null;
            dgvViewInventoryMovements.Rows.Clear();
            try
            {
                dtSource = objBusinessFacade.loadInventoryMovementsDetails(Common.Common.startDate, Common.Common.endDate, Common.Common.cis_pharmacy_info.investoryMoveTypeId);
                if (dtSource.Rows.Count > 0)
                {
                    dgvViewInventoryMovements.DataSource = objBusinessFacade.AssignRowNo(dtSource);
                    dgvViewInventoryMovements.Columns[0].Width = 50;
                    dgvViewInventoryMovements.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvViewInventoryMovements.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //dgvViewInventoryMovements.Columns[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "View Inventory Movements", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.Common.ExceptionHandler.ExceptionWriter(ex);
            }
        }

        private void btnGoFilter_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtSearch.Text)))
            {
                string filterField = cboFilter.SelectedItem.ToString();
                dtSource.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", filterField, txtSearch.Text);
                dgvViewInventoryMovements.DataSource = dtSource;
            }
        }

        private void btnGoDate_Click(object sender, EventArgs e)
        {
            loadInventoryMovementsDetails();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.dtpDateFrom.Value = DateTime.Now;
            this.dtpDateTo.Value = DateTime.Now;
            txtSearch.Text = string.Empty;
            loadInventoryMovementsDetails();
        }
    }
}
