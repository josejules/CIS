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
    public partial class frmCategory : Form
    {
        #region Variables
        public int CategoryId = 0;
        #endregion

        public frmCategory()
        {
            InitializeComponent();
        }

        public frmCategory(int categoryId)
        {
            CategoryId = categoryId;
            InitializeComponent();
            DataTable dtCategory = new clsLabBusinessFacade().FetchCategoryInfoById(CategoryId);
            if (dtCategory.Rows.Count > 0)
                txtCategory.Text = dtCategory.Rows[0]["category_name"].ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCategory.Text))
            {
                ComArugments args = new ComArugments();
                args.ParamList[clsLabCommon.Laboratory.Category.CategoryId] = CategoryId;
                args.ParamList[clsLabCommon.Laboratory.Category.CategoryName] = txtCategory.Text;

                int rowsAffected = new clsLabBusinessFacade().AddCategory(args);

                if (rowsAffected > 0)
                {
                    if (CategoryId == 0)
                        MessageBox.Show("Category is saved", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Category is updated", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCategory.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Please enter Category", "Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCategory_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategory.Text))
            {
                errorProvider1.SetError(txtCategory, "Please enter Category");
                txtCategory.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

    }
}
