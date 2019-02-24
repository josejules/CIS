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
    public partial class cisPharmacyMaster : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        #region Constructor
        public cisPharmacyMaster()
        {
            InitializeComponent();
            //cboFilter.SelectedIndex = 0;
        }
        #endregion

        #region Events
        private void cisPharmacyMaster_Load(object sender, EventArgs e)
        {
            ViewItemType();
            dgvPharmacyMaster.Columns[1].Visible = false;
            dgvPharmacyMaster.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPharmacyMaster.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPharmacyMaster.Columns[0].Width = 60;
            tcPharmacyMaster_SelectedIndexChanged( sender,  e);
        }

        private void tcPharmacyMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPharmacyMaster.DataSource = null;

            Common.Common.cis_pharmacy_info.selectedPhaTab = Convert.ToString(tcPharmacyMaster.SelectedTab.Name);

            switch (Common.Common.cis_pharmacy_info.selectedPhaTab)
            {
                case "tpItemType":
                    ViewItemType();
                    string[] itemType = { "Item Type", "Status" };
                    cboFilter.DataSource = itemType;
                    break;

                case "tpItem":
                    ViewItem();
                    string[] item = { "Item Name", "Item Type", "Tax Rate", "HSN Code", "Status" };
                    cboFilter.DataSource = item;
                    break;

                case "tpVendor":
                    ViewVendor();
                    string[] vendor = { "Vendor Name", "TIN Number", "Status" };
                    cboFilter.DataSource = vendor;
                    break;

                case "tpTax":
                    ViewTax();
                    string[] tax = { "Tax Rate", "Status" };
                    cboFilter.DataSource = tax;
                    break;

                default:
                    break;
            }

            if (dgvPharmacyMaster.Rows.Count <= 0)
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                dgvPharmacyMaster.Columns[1].Visible = false;
                dgvPharmacyMaster.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPharmacyMaster.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPharmacyMaster.Columns[0].Width = 60;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Common.Common.cis_pharmacy_info.selectedPhaTab = Convert.ToString(tcPharmacyMaster.SelectedTab.Name);

            switch (Common.Common.cis_pharmacy_info.selectedPhaTab)
            {
                case "tpItemType":
                    cisAddItemType ObjITAdd = new cisAddItemType();
                    ObjITAdd.ShowDialog();
                    break;

                case "tpItem":
                    cisAddItem ObjIAdd = new cisAddItem();
                    ObjIAdd.ShowDialog();
                    break;

                case "tpVendor":
                    cisAddVendor ObjVAdd = new cisAddVendor();
                    ObjVAdd.ShowDialog();
                    break;

                case "tpTax":
                    cisAddTax ObjTAdd = new cisAddTax();
                    ObjTAdd.ShowDialog();
                    break;

                default:
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Common.Common.id = getPhaId();

            Common.Common.cis_pharmacy_info.selectedPhaTab = Convert.ToString(tcPharmacyMaster.SelectedTab.Name);

            switch (Common.Common.cis_pharmacy_info.selectedPhaTab)
            {
                case "tpItemType":
                    cisAddItemType ObjITAdd = new cisAddItemType(Common.Common.id);
                    ObjITAdd.ShowDialog();
                    break;

                case "tpItem":
                    cisAddItem ObjIAdd = new cisAddItem(Common.Common.id);
                    ObjIAdd.ShowDialog();
                    break;

                case "tpVendor":
                    cisAddVendor ObjVAdd = new cisAddVendor(Common.Common.id);
                    ObjVAdd.ShowDialog();
                    break;

                case "tpTax":
                    cisAddTax ObjTAdd = new cisAddTax(Common.Common.id);
                    ObjTAdd.ShowDialog();
                    break;

                default:
                    break;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPharmacyMaster.Rows.Count > 0)
            {
                if (MessageBox.Show("Are you sure to delete the record?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Common.Common.cis_pharmacy_info.selectedPhaTab = Convert.ToString(tcPharmacyMaster.SelectedTab.Name);
                        int getId = getPhaId();

                        switch (Common.Common.cis_pharmacy_info.selectedPhaTab)
                        {
                            case "tpItemType":
                                Common.Common.flag = objBusinessFacade.deleteItemType(getId);
                                break;

                            case "tpItem":
                                Common.Common.flag = objBusinessFacade.deleteItem(getId);
                                break;

                            case "tpVendor":
                                Common.Common.flag = objBusinessFacade.deleteVendor(getId);
                                break;

                            case "tpTax":
                                Common.Common.flag = objBusinessFacade.deleteTax(getId);
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
                dgvPharmacyMaster.DataSource = dtSource;
            }
        }
        #endregion

        #region Functions
        public void ViewItemType()
        {
            try
            {
                dtSource = objBusinessFacade.getItemTypeDetails();
                dgvPharmacyMaster.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ViewItem()
        {
            try
            {
                dtSource = objBusinessFacade.getItemDetails();
                dgvPharmacyMaster.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ViewVendor()
        {
            try
            {
                dtSource = objBusinessFacade.getVendorDetails();
                dgvPharmacyMaster.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ViewTax()
        {
            try
            {
                dtSource = objBusinessFacade.getTaxDetails();
                dgvPharmacyMaster.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int getPhaId()
        {
            if (dgvPharmacyMaster.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please Select Row to Delete .....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Common.Common.cis_pharmacy_info.selectedPhaTab = Convert.ToString(tcPharmacyMaster.SelectedTab.Name);

                switch (Common.Common.cis_pharmacy_info.selectedPhaTab)
                {
                    case "tpItemType":
                        Common.Common.id = int.Parse(dgvPharmacyMaster.SelectedRows[0].Cells["item_type_id"].Value.ToString());
                        break;

                    case "tpItem":
                        Common.Common.id = int.Parse(dgvPharmacyMaster.SelectedRows[0].Cells["item_id"].Value.ToString());
                        break;

                    case "tpVendor":
                        Common.Common.id = int.Parse(dgvPharmacyMaster.SelectedRows[0].Cells["vendor_id"].Value.ToString());
                        break;

                    case "tpTax":
                        Common.Common.id = int.Parse(dgvPharmacyMaster.SelectedRows[0].Cells["tax_id"].Value.ToString());
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
            tcPharmacyMaster_SelectedIndexChanged(sender, e);
        }
    }
}
