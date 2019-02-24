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
namespace CIS.Laboratory.User
{
    public partial class frmUserMapping : Form
    {
        clsLabBusinessFacade objBusiness = new clsLabBusinessFacade();
        public frmUserMapping()
        {
            InitializeComponent();
        }

        CheckBox chkHeader = new CheckBox();
        private void frmUserMapping_Load(object sender, EventArgs e)
        {
            LoadUserRoles();
            LoadUserActivities();
            this.cboUser.SelectedValueChanged += new System.EventHandler(this.cboUser_SelectedValueChanged);

            //Find the Location of Header Cell.
            Point headerCellLocation = this.dgvUserActivities.GetCellDisplayRectangle(0, -1, true).Location;

            //Place the Header CheckBox in the Location of the Header Cell.
            chkHeader.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
            chkHeader.BackColor = Color.White;
            chkHeader.Size = new Size(18, 18);

            //Assign Click event to the Header CheckBox.
            chkHeader.Click += new EventHandler(chkHeader_Clicked);
            dgvUserActivities.Controls.Add(chkHeader);
        }

        private void LoadUserRoles()
        {
            cboUser.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboUser.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboUser.DataSource = new clsLabBusinessFacade().LoadUserRole();
            cboUser.ValueMember = "role_id";
            cboUser.DisplayMember = "User Role";
        }

        private void LoadUserActivities()
        {
            dgvUserActivities.DataSource = null;
            
            //Add a CheckBox Column to the DataGridView at the first position.
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.FillWeight = 20F;
            checkBoxColumn.Name = "checkBoxColumn";
            checkBoxColumn.DataPropertyName = "Visible";
            dgvUserActivities.Columns.Insert(0, checkBoxColumn);
            dgvUserActivities.Columns[0].FillWeight = 20F;
            
            dgvUserActivities.DataSource = new clsLabBusinessFacade().LoadUserActivities(Convert.ToInt32(cboUser.SelectedValue));
            dgvUserActivities.Columns["action_id"].Visible = false;
            dgvUserActivities.Columns["Module"].ReadOnly = true;
            dgvUserActivities.Columns["Activity"].ReadOnly = true;
        }

        private void cboUser_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadUserActivities();
        }

        private void chkHeader_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            dgvUserActivities.EndEdit();

            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in dgvUserActivities.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["checkBoxColumn"] as DataGridViewCheckBoxCell);
                checkBox.Value = chkHeader.Checked;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUserActivities.Rows.Count > 0)
                {
                    int count = 0;
                    DataGridViewCheckBoxCell ch = new DataGridViewCheckBoxCell();
                    foreach (DataGridViewRow row in dgvUserActivities.Rows)
                    {
                        ch = (DataGridViewCheckBoxCell)row.Cells[3];

                        if (ch.Value == null)
                            ch.Value = false;
                        switch (ch.Value.ToString())
                        {
                            case "1": //Selected User Activity
                                {
                                    count++;
                                    break;
                                }
                            case "0":
                                {
                                    if (row.Cells["Activity"].Value.ToString() == "User Rights")
                                    {
                                        if (cboUser.Text == "Admin")
                                        {
                                            MessageBox.Show("Please select User rights Activity!", "Map User Activity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                    if (count > 0)
                    {
                        int iDeleted = objBusiness.DeleteUserRoleActities(Convert.ToInt32(cboUser.SelectedValue));
                        int iSaved = 0;

                        DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                        foreach (DataGridViewRow row in dgvUserActivities.Rows)
                        {
                            ch1 = (DataGridViewCheckBoxCell)row.Cells[3];

                            if (ch1.Value == null)
                                ch1.Value = false;
                            switch (ch1.Value.ToString())
                            {
                                case "1": //Selected User Activity
                                    {
                                        ComArugments args = new ComArugments();
                                        args.ParamList[CIS.Common.User.UserMapRoleAction.RoleId] = cboUser.SelectedValue;
                                        args.ParamList[CIS.Common.User.UserMapRoleAction.ActionId] = row.Cells["action_id"].Value.ToString();
                                        iSaved = objBusiness.SaveUserRoleActivity(args);
                                        break;
                                    }
                            }
                            //if (Convert.ToInt32(row.Cells[3].Value.ToString()) == 1)
                        }
                        if (iSaved > 0)
                            MessageBox.Show("Activities are mapped successfully", "Map User Activity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objBusiness.commitTransction();
                    }
                    else
                        MessageBox.Show("Activities are not selected!", "Map User Activity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                objBusiness.rollBackTransation();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
