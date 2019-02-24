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
    public partial class frmTestField : Form
    {
        #region Variables
        public int FieldId = 0;
        public int ItemId = 0;
        clsLabBusinessFacade objBusinessFacade = new clsLabBusinessFacade();
        #endregion

        public frmTestField()
        {
            try
            {
                InitializeComponent();
                LoadTestFieldTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmTestField_Load(object sender, EventArgs e)
        {
            
        }

        //private void LoadEmptyItemDataset()
        //{
        //    DataTable dtItem = new DataTable();
        //    dtItem.Columns.Add("test_item_id", typeof(int));
        //    dtItem.Columns.Add("Item", typeof(string));
        //    dtItem.Columns.Add("test_field_id", typeof(string));

        //    dgvTestItem.DataSource = dtItem;
        //    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
        //    imgDelete.Name = "Delete";
        //    imgDelete.Image = Properties.Resources.Delete;
        //    dgvTestItem.Columns.Add(imgDelete);
        //    dgvTestItem.Columns["test_item_id"].Visible = false;
        //    dgvTestItem.Columns["test_field_id"].Visible = false;
        //    dgvTestItem.Columns["Delete"].FillWeight = 20F;
        //}

        private void LoadTestFieldTypes()
        {
            cboFieldType.DataSource = new clsLabBusinessFacade().LoadTestFieldTypes();
            cboFieldType.ValueMember = "test_field_type_id";
            cboFieldType.DisplayMember = "test_field_type_name";
        }

        public frmTestField(int fieldId)
        {
            try
            {
                FieldId = fieldId;
                InitializeComponent();
                LoadTestFieldTypes();
                DataTable dtTestField = new clsLabBusinessFacade().FetchTestFieldInfoById(fieldId);
                if (dtTestField.Rows.Count > 0)
                {
                    cboFieldType.SelectedValue = Convert.ToInt16(dtTestField.Rows[0]["test_field_type_id"].ToString());
                    txtFieldName.Text = dtTestField.Rows[0]["test_field_name"].ToString();
                    txtUnit.Text = dtTestField.Rows[0]["unit"].ToString();
                    rtxtReferenceValue.Text = dtTestField.Rows[0]["reference_value"].ToString();
                    dgvTestItem.DataSource = objBusinessFacade.LoadTestItem(FieldId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Test Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtFieldName.Text))
                {
                    ComArugments args = new ComArugments();
                    args.ParamList[clsLabCommon.Laboratory.TestField.TestFieldId] = FieldId;
                    args.ParamList[clsLabCommon.Laboratory.TestField.TestFieldName] = txtFieldName.Text.Replace("'", "''");
                    args.ParamList[clsLabCommon.Laboratory.TestField.TestFieldTypeId] = cboFieldType.SelectedValue;
                    args.ParamList[clsLabCommon.Laboratory.TestField.Unit] = txtUnit.Text.Replace("'", "''");;
                    args.ParamList[clsLabCommon.Laboratory.TestField.ReferenceValue] = rtxtReferenceValue.Text.Replace("'", "''");

                    int rowsAffected = objBusinessFacade.AddEditTestField(args);
                    if (rowsAffected > 0)
                    {
                        if (cboFieldType.Text.Equals("ComboBox"))
                        {
                            int lastInsertedFieldId = 0;
                            if (FieldId == 0)
                                lastInsertedFieldId = objBusinessFacade.getLastInsertedId();
                            
                            for (int i = 0; i < dgvTestItem.Rows.Count - 1; i++)
                            {
                                ComArugments argsTestItems = new ComArugments();
                                argsTestItems.ParamList[clsLabCommon.Laboratory.TestItem.TestItemId] = dgvTestItem.Rows[i].Cells["Id"].FormattedValue.ToString() == "" ? 0 : dgvTestItem.Rows[i].Cells["Id"].FormattedValue;
                                argsTestItems.ParamList[clsLabCommon.Laboratory.TestItem.TestItemName] = dgvTestItem.Rows[i].Cells["Item"].Value;
                                argsTestItems.ParamList[clsLabCommon.Laboratory.TestItem.TestFieldId] = FieldId == 0 ? lastInsertedFieldId : FieldId;
                                int flag = objBusinessFacade.AddEditTestItems(argsTestItems);
                            }
                        }
                        if (FieldId == 0)
                            MessageBox.Show("Test Field is saved", "Test Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Test Field is updated", "Test Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearControls();
                    }
                    objBusinessFacade.commitTransction();
                }
                else
                {
                    MessageBox.Show("Please enter Test Field", "Test Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }

        private void ClearControls()
        {
            txtFieldName.Text = string.Empty;
            txtUnit.Text = string.Empty;
            rtxtReferenceValue.Text = string.Empty;
            cboFieldType.SelectedIndex = -1;
            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("test_item_id", typeof(int));
            dtItem.Columns.Add("test_item_name", typeof(string));
            dtItem.Columns.Add("test_field_id", typeof(string));

            dgvTestItem.DataSource = dtItem;
            //dgvTestItem.DataSource = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFieldName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFieldName.Text))
            {
                errorProvider1.SetError(txtFieldName, "Please enter Test Field Name");
                txtFieldName.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void dgvTestItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if ((e.ColumnIndex == 3 && FieldId == 0) || (e.ColumnIndex == 0 && FieldId > 0))
                {
                    if (dgvTestItem.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString() == "")
                    {
                        dgvTestItem.Rows.RemoveAt(e.RowIndex);
                        MessageBox.Show("Item is Deleted!", "Test Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int iDeleted = new clsLabBusinessFacade().DeleteTestItem(Convert.ToInt32(dgvTestItem.Rows[e.RowIndex].Cells["Id"].FormattedValue));
                        if (iDeleted > 0)
                        {
                            dgvTestItem.Rows.RemoveAt(e.RowIndex);
                            MessageBox.Show("Item is deleted ", "Test Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Item is not deleted", "Test Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }
        }

        private void dgvTestItem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTestItem.Columns[e.ColumnIndex].Name == "Delete")
            {
                e.Value = Properties.Resources.Delete; //Image.FromFile(@"C:\Pictures\TestImage.jpg");
            } 
        }

        private void cboFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFieldType.Text.Equals("ComboBox"))
            {
                panel6.Visible = true;
                this.Size = new Size(this.Size.Width, 511);
            }
            else
            {
                panel6.Visible = false;
                this.Size = new Size(this.Size.Width, 300);
            }
        }

    }
}
