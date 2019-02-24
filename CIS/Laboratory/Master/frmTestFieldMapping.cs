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

namespace CIS.Laboratory.Master
{
    public partial class frmTestFieldMapping : Form
    {
        public frmTestFieldMapping()
        {
            InitializeComponent();
        }

        CheckBox chkUnMappedHeader = new CheckBox();
        CheckBox chkMappedHeader = new CheckBox();
        static DataTable dtUnMapped = null;
        static DataTable dtMapped = null;
        private void frmTestFieldMapping_Load(object sender, EventArgs e)
        {
            LoadTestClasses();
            LoadUnmappedTestFields();
            LoadMappedTestFields();
            this.cboTestItem.SelectedValueChanged += new System.EventHandler(this.cboTestItem_SelectedValueChanged);

            //Find the Location of Header Cell.
            Point headerCellLocation = this.dgvTestFields.GetCellDisplayRectangle(0, -1, true).Location;

            //Place the Header CheckBox in the Location of the Header Cell.
            chkUnMappedHeader.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
            chkUnMappedHeader.BackColor = Color.White;
            chkUnMappedHeader.Size = new Size(18, 18);

            //Assign Click event to the Header CheckBox.
            chkUnMappedHeader.Click += new EventHandler(chkUnMappedHeader_Clicked);
            dgvTestFields.Controls.Add(chkUnMappedHeader);

            //Add a CheckBox Column to the DataGridView at the first position.
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.FillWeight = 20F;
            checkBoxColumn.Name = "checkBoxColumn";
            dgvTestFields.Columns.Insert(0, checkBoxColumn);
            dgvTestFields.Columns[1].Visible=false;
            dgvTestFields.Columns[0].FillWeight = 20F;

            //Find the Location of Header Cell.
            Point headerCellLocation1 = this.dgvTestFieldsMapped.GetCellDisplayRectangle(0, -1, true).Location;

            //Place the Header CheckBox in the Location of the Header Cell.
            chkMappedHeader.Location = new Point(headerCellLocation1.X + 8, headerCellLocation1.Y + 2);
            chkMappedHeader.BackColor = Color.White;
            chkMappedHeader.Size = new Size(18, 18);

            //Assign Click event to the Header CheckBox.
            chkMappedHeader.Click += new EventHandler(chkMappedHeader_Clicked);
            dgvTestFieldsMapped.Controls.Add(chkMappedHeader);

            //Add a CheckBox Column to the DataGridView at the first position.
            DataGridViewCheckBoxColumn checkBoxColumn1 = new DataGridViewCheckBoxColumn();
            checkBoxColumn1.HeaderText = "";
            checkBoxColumn1.FillWeight = 20F;
            checkBoxColumn1.Name = "checkBoxColumn1";
            dgvTestFieldsMapped.Columns.Insert(0, checkBoxColumn1);
            dgvTestFieldsMapped.Columns[1].Visible = false;
            dgvTestFieldsMapped.Columns[0].FillWeight = 20F;

        }

        private void chkUnMappedHeader_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            dgvTestFields.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgvTestFields.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = chkUnMappedHeader.Checked;
            }
        }

        private void chkMappedHeader_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Necessary to end the edit mode of the Cell.
                dgvTestFieldsMapped.EndEdit();
                //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
                foreach (DataGridViewRow row in dgvTestFieldsMapped.Rows)
                {
                    DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn1"] as DataGridViewCheckBoxCell);
                    checkBox.Value = chkMappedHeader.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }

        private void LoadUnmappedTestFields()
        {
            dgvTestFields.DataSource = null;
            dtUnMapped = new clsLabBusinessFacade().LoadUnMappedTestFields(Convert.ToInt32(cboTestItem.SelectedValue));
            dgvTestFields.DataSource = dtUnMapped;
            dgvTestFields.Columns["test_field_id"].Visible = false;
            dgvTestFields.Columns["Test Fields"].ReadOnly = true;
        }

        private void LoadMappedTestFields()
        {
            dgvTestFieldsMapped.DataSource = null;
            dtMapped = new clsLabBusinessFacade().LoadMappedTestFields(Convert.ToInt32(cboTestItem.SelectedValue));
            dgvTestFieldsMapped.DataSource = dtMapped;
            dgvTestFieldsMapped.Columns["test_field_id"].Visible = false;
            dgvTestFieldsMapped.Columns["Test Fields"].ReadOnly = true;
        }

        private void LoadTestClasses()
        {
            cboTestItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTestItem.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboTestItem.DataSource = new clsLabBusinessFacade().LoadTestClass();
            cboTestItem.ValueMember = "investigation_id";
            cboTestItem.DisplayMember = "investigation_name";
        }

        private void cboTestItem_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadUnmappedTestFields();
            LoadMappedTestFields();
        }

        private void dgvTestFields_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgvTestFields.Rows[dgvTestFields.CurrentRow.Index].Cells[0];

                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "True":
                        {
                            ch1.Value = false;
                            break;
                        }
                    case "False":
                        {
                            ch1.Value = true;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestFieldsMapped.Rows.Count > 0)
                {
                    int iDeleted = new clsLabBusinessFacade().DeleteMappedTestFields(Convert.ToInt32(cboTestItem.SelectedValue));
                    int order = 1;
                    int iSaved = 0;
                    foreach (DataGridViewRow row in dgvTestFieldsMapped.Rows)
                    {
                        ComArugments args = new ComArugments();
                        args.ParamList[clsLabCommon.Laboratory.TestFieldMapping.InvestigationId] = cboTestItem.SelectedValue;
                        args.ParamList[clsLabCommon.Laboratory.TestFieldMapping.TestFieldId] = row.Cells["test_field_id"].Value.ToString();
                        args.ParamList[clsLabCommon.Laboratory.TestFieldMapping.VisibleOrder] = order++;
                        iSaved = new clsLabBusinessFacade().SaveMappedTestFields(args);
                    }
                    if (iSaved > 0)
                        MessageBox.Show("Test Fields are mapped successfully", "Map Test Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Test Fields are not selected!", "Map Test Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvTestFieldsMapped_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgvTestFieldsMapped.Rows[dgvTestFieldsMapped.CurrentRow.Index].Cells[0];

                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "True":
                        {
                            ch1.Value = false;
                            break;
                        }
                    case "False":
                        {
                            ch1.Value = true;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTempUnMapped = ((DataTable)dgvTestFields.DataSource).Clone();
                DataTable dtTempMapped = ((DataTable)dgvTestFieldsMapped.DataSource);

                foreach (DataGridViewRow row in dgvTestFields.Rows)
                {
                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                    ch1 = (DataGridViewCheckBoxCell)row.Cells[0];

                    if (ch1.Value == null)
                        ch1.Value = false;
                    switch (ch1.Value.ToString())
                    {
                        case "True":
                            {
                                dtTempMapped.Rows.Add(Convert.ToInt32(row.Cells[1].Value), row.Cells[2].Value);
                                break;
                            }
                        case "False":
                            {
                                dtTempUnMapped.Rows.Add(Convert.ToInt32(row.Cells[1].Value), row.Cells[2].Value);
                                break;
                            }
                    }

                }

                dgvTestFields.DataSource = dtTempUnMapped;
                dgvTestFieldsMapped.DataSource = dtTempMapped;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTempUnMapped = ((DataTable)dgvTestFields.DataSource);
                DataTable dtTempMapped = ((DataTable)dgvTestFieldsMapped.DataSource).Clone();

                foreach (DataGridViewRow row in dgvTestFieldsMapped.Rows)
                {
                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                    ch1 = (DataGridViewCheckBoxCell)row.Cells[0];

                    if (ch1.Value == null)
                        ch1.Value = false;
                    switch (ch1.Value.ToString())
                    {
                        case "True":
                            {
                                dtTempUnMapped.Rows.Add(Convert.ToInt32(row.Cells[1].Value), row.Cells[2].Value);
                                break;
                            }
                        case "False":
                            {
                                dtTempMapped.Rows.Add(Convert.ToInt32(row.Cells[1].Value), row.Cells[2].Value);
                                break;
                            }
                    }

                }

                dgvTestFields.DataSource = dtTempUnMapped;
                dgvTestFieldsMapped.DataSource = dtTempMapped;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dgvTestFieldsMapped.Rows.Count > 0)
            {
                int rowIndex = dgvTestFieldsMapped.SelectedCells[0].OwningRow.Index;
                if (rowIndex > 0)
                {
                    DataTable dt = ((DataTable)dgvTestFieldsMapped.DataSource);
                    DataRow row = dt.NewRow();
                    row[0] = Convert.ToInt32(dgvTestFieldsMapped.Rows[rowIndex].Cells[1].Value.ToString());
                    row[1] = dgvTestFieldsMapped.Rows[rowIndex].Cells[2].Value.ToString();

                    dt.Rows.RemoveAt(rowIndex);
                    dt.Rows.InsertAt(row, rowIndex - 1);

                    dgvTestFieldsMapped.Rows[rowIndex - 1].Selected = true;
                }
            }
            else
                MessageBox.Show("Test Record is not found!", "Test Field Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (dgvTestFieldsMapped.Rows.Count > 0)
            {
                int rowIndex = dgvTestFieldsMapped.SelectedCells[0].OwningRow.Index;
                if (rowIndex < dgvTestFieldsMapped.Rows.Count - 1)
                {
                    DataTable dt = ((DataTable)dgvTestFieldsMapped.DataSource);
                    DataRow row = dt.NewRow();
                    row[0] = Convert.ToInt32(dgvTestFieldsMapped.Rows[rowIndex].Cells[1].Value.ToString());
                    row[1] = dgvTestFieldsMapped.Rows[rowIndex].Cells[2].Value.ToString();

                    dt.Rows.RemoveAt(rowIndex);
                    dt.Rows.InsertAt(row, rowIndex + 1);

                    dgvTestFieldsMapped.Rows[rowIndex + 1].Selected = true;
                }
            }
            else
                MessageBox.Show("Test Record is not found!", "Test Field Mapping", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
