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
    public partial class frmRoom : Form
    {
        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        public static DataTable dtSource = new DataTable();
        //BindingSource dtSource = new BindingSource();
        ComArugments objArg = new ComArugments();

        public static DataTable dts = new DataTable("wroom");
        //BindingSource dts = new BindingSource();
        DataColumn room_id = new DataColumn("room_id", typeof(System.Int32));
        DataColumn room_no = new DataColumn("Room No", typeof(System.String));
        DataColumn status = new DataColumn("Status", typeof(System.String));
      
        #endregion

        #region Constructor
        public frmRoom()
        {
            InitializeComponent();
            cboRoomStatus.SelectedIndex = 1;
        }
        #endregion

        #region Events
        private void frmRoom_Load(object sender, EventArgs e)
        {
            loadWard();
            ViewRoom();
            dgvRoomAndBed.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRoomAndBed.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRoomAndBed.Columns[0].Width = 50;
            rbtnRoom.Checked = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRoomFrom.Text.ToString()) && !string.IsNullOrEmpty(txtRoomTo.Text.ToString()))
            {
                CIS.Common.Common.cis_Room.roomPrefix = txtRoomPrefix.Text.ToString();
                CIS.Common.Common.cis_Room.roomFrom = Convert.ToInt32(txtRoomFrom.Text.ToString());
                CIS.Common.Common.cis_Room.roomTo = Convert.ToInt32(txtRoomTo.Text.ToString());

                //dts.Columns.AddRange(new DataColumn[] { room_id, room_no, status });

                //int s = dtSource.Rows.Count;
                for (int i = CIS.Common.Common.cis_Room.roomFrom; i <= CIS.Common.Common.cis_Room.roomTo; i++)
                {
                    dgvRoomAndBed.Rows.Add(0, dgvRoomAndBed.Rows.Count + 1, txtRoomPrefix.Text.ToString() + i.ToString(), "Active");
                }
                txtRoomPrefix.Text = string.Empty;
                txtRoomFrom.Text = string.Empty;
                txtRoomTo.Text = string.Empty;
                //dtSource.Merge(dts);
                //dgvRoom.DataSource = objBusinessFacade.AssignRowNo(dtSource);
            }

            else
            {
                MessageBox.Show("Fields are required....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtRoomFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void txtRoomTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (char.IsNumber(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.') ? false : true;
        }

        private void cboWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWard.SelectedIndex != 0)
            {
                if (rbtnRoom.Checked == true)
                {
                    btnAddSingle.Enabled = true;
                    btnAddBulk.Enabled = true;
                    if (dgvRoomAndBed.Rows.Count > 1)
                    {
                        dgvRoomAndBed.Rows.Clear();
                        dgvRoomAndBed.Refresh();
                        txtRoomPrefix.Text = string.Empty;
                        txtRoomFrom.Text = string.Empty;
                        txtRoomTo.Text = string.Empty;
                        btnSave.Enabled = true;
                    }
                    ViewRoom();
                }
                if (rbtnBed.Checked == true)
                {
                    Common.Common.cis_Room.ward_id = Convert.ToInt32(cboWard.SelectedValue.ToString());
                    loadBed(Common.Common.cis_Room.ward_id);
                }
            }
            else
            {
                btnAddSingle.Enabled = false;
                btnAddBulk.Enabled = false;
                dgvRoomAndBed.Rows.Clear();
                dgvRoomAndBed.Refresh();

                if (cboRoom.Items.Count > 0)
                {
                    //cboRoom.Items.RemoveAt(0);
                    cboRoom.DataSource = null;
                }
                //cboRoom.Items.Clear();
                //cboRoom.SelectedIndex = -1;
            }
        }

        #endregion

        #region Functions
        private void loadWard()
        {
            try
            {
                dtSource = objBusinessFacade.loadWard();
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);
                    cboWard.ValueMember = "WARD_DEPARTMENT_ID";
                    cboWard.DisplayMember = "WARD_DEPARTMENT_NAME";
                    cboWard.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void loadBed(int wardId)
        {
            try
            {
                dtSource = objBusinessFacade.loadRoom(wardId);
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    /*DataRow row = dtSource.NewRow();
                    row[0] = -1;
                    row[1] = "";
                    dtSource.Rows.InsertAt(row, 0);*/
                    cboRoom.ValueMember = "room_id";
                    cboRoom.DisplayMember = "room_no";
                    cboRoom.DataSource = dtSource;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ViewRoom()
        {
            Common.Common.cis_Room.ward_id = Convert.ToInt32(cboWard.SelectedValue.ToString());
            try
            {
                dtSource = objBusinessFacade.getRoomDetails(Common.Common.cis_Room.ward_id);
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSource.Rows.Count; i++)
                    {
                        //Add New Row
                        dgvRoomAndBed.Rows.Add();
                        dgvRoomAndBed.Rows[i].Cells[0].Value = dtSource.Rows[i]["room_id"].ToString();
                        dgvRoomAndBed.Rows[i].Cells[1].Value = dgvRoomAndBed.Rows.Count;
                        dgvRoomAndBed.Rows[i].Cells[2].Value = dtSource.Rows[i]["room_no"].ToString();
                        //roomStatus= dtSource.Rows[i]["status"].ToString();
                        dgvRoomAndBed.Rows[i].Cells[3].Value = dtSource.Rows[i]["status"].ToString();
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

        }

        public void ViewBed()
        {
            if (cboRoom.SelectedIndex >= 0)
            {
                Common.Common.cis_Room.ward_id = Convert.ToInt32(cboWard.SelectedValue.ToString());
                Common.Common.cis_Room.roomId = Convert.ToInt32(cboRoom.SelectedValue.ToString());
                try
                {
                    dtSource = objBusinessFacade.getBedDetails(Common.Common.cis_Room.ward_id, Common.Common.cis_Room.roomId);
                    if (dtSource != null && dtSource.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtSource.Rows.Count; i++)
                        {
                            //Add New Row
                            dgvRoomAndBed.Rows.Add();
                            dgvRoomAndBed.Rows[i].Cells[0].Value = dtSource.Rows[i]["bed_id"].ToString();
                            dgvRoomAndBed.Rows[i].Cells[1].Value = dgvRoomAndBed.Rows.Count;
                            dgvRoomAndBed.Rows[i].Cells[2].Value = dtSource.Rows[i]["bed_number"].ToString();
                            //roomStatus= dtSource.Rows[i]["status"].ToString();
                            dgvRoomAndBed.Rows[i].Cells[3].Value = dtSource.Rows[i]["status"].ToString();
                        }
                    }
                }

                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        private void dgvRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)//Edit Button Click
            {
                txtRoomNo.Text = dgvRoomAndBed.Rows[e.RowIndex].Cells[2].Value.ToString();
                cboRoomStatus.Text = dgvRoomAndBed.Rows[e.RowIndex].Cells[3].Value.ToString();
                lblRoomId.Text = dgvRoomAndBed.Rows[e.RowIndex].Cells[0].Value.ToString();
                lblCheckEditModeRoom.Text = e.RowIndex.ToString();
            }

            if (e.ColumnIndex == 5)//Delete Button Click
            {
                if (Convert.ToInt32(dgvRoomAndBed.Rows[e.RowIndex].Cells[0].Value.ToString()) == 0)//Check if record is from DB
                {
                    if (MessageBox.Show("Are you sure to Delete the record?", "CIS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvRoomAndBed.Rows.Remove(dgvRoomAndBed.Rows[e.RowIndex]);
                    }
                }
                else
                {
                    MessageBox.Show("You can't delete this record....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnAddSingle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRoomNo.Text.ToString()))
            {
                string roomId;

                if (!string.IsNullOrEmpty(lblRoomId.Text.ToString()))
                {
                    roomId = lblRoomId.Text.ToString();
                }
                else
                {
                    roomId = "0";
                }
                Common.Common.cis_Room.roomNo = txtRoomNo.Text.ToString().Trim();
                string roomStatus = cboRoomStatus.SelectedItem.ToString();

                if (!string.IsNullOrEmpty(Common.Common.cis_Room.roomNo))
                {
                    if (string.IsNullOrEmpty(lblCheckEditModeRoom.Text.ToString()))//Add Item
                    {
                        bool entryFound = false;
                        foreach (DataGridViewRow row in dgvRoomAndBed.Rows)//Check Item exits aleady
                        {
                            string roomNumber = Convert.ToString(row.Cells[2].Value);
                            if (roomNumber == Common.Common.cis_Room.roomNo)
                            {
                                MessageBox.Show("Already Exists....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                entryFound = true;
                                txtRoomNo.Focus();
                            }
                        }
                        if (!entryFound)//If not add the item
                        {
                            dgvRoomAndBed.Rows.Add(roomId, dgvRoomAndBed.Rows.Count + 1, Common.Common.cis_Room.roomNo, roomStatus);
                            clearRoomInput();
                            txtRoomNo.Focus();
                        }
                    }

                    else //Edit Item
                    {
                        int rowNo = Convert.ToInt32(lblCheckEditModeRoom.Text.ToString());
                        dgvRoomAndBed.Rows[rowNo].Cells[0].Value = roomId;
                        dgvRoomAndBed.Rows[rowNo].Cells[2].Value = Common.Common.cis_Room.roomNo;
                        dgvRoomAndBed.Rows[rowNo].Cells[3].Value = roomStatus;
                        clearRoomInput();
                        txtRoomNo.Focus();
                    }
                }

                else
                {
                    MessageBox.Show("Fields are required....!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void clearRoomInput()
        {
            lblRoomId.Text = string.Empty;
            txtRoomNo.Text = string.Empty;
            cboRoomStatus.SelectedIndex = 1;
            lblCheckEditModeRoom.Text = string.Empty;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (dgvRoomAndBed.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in dgvRoomAndBed.Rows)
                {
                    Common.Common.cis_Room.ward_id = Convert.ToInt32(cboWard.SelectedValue.ToString());

                    if (row.Cells[3].Value.ToString() == "Active")
                    {
                        Common.Common.status = 1;
                    }
                    else
                    {
                        Common.Common.status = 0;
                    }

                    if (rbtnRoom.Checked == true)
                    {
                        Common.Common.cis_Room.roomId = Convert.ToInt32(row.Cells[0].Value.ToString());
                        Common.Common.cis_Room.roomNo = row.Cells[2].Value.ToString().Trim();

                        objArg.ParamList["room_id"] = Common.Common.cis_Room.roomId;
                        objArg.ParamList["ward_id"] = Common.Common.cis_Room.ward_id;
                        objArg.ParamList["room_no"] = Common.Common.cis_Room.roomNo;
                        objArg.ParamList["status"] = Common.Common.status;

                        try
                        {
                            Common.Common.flag = objBusinessFacade.insertRoom(objArg);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    if (rbtnBed.Checked == true)
                    {
                        Common.Common.cis_Room.roomId = Convert.ToInt32(cboRoom.SelectedValue.ToString());
                        Common.Common.cis_bed.bedId = Convert.ToInt32(row.Cells[0].Value.ToString());
                        Common.Common.cis_bed.bedNo = row.Cells[2].Value.ToString().Trim();

                        objArg.ParamList["bed_id"] = Common.Common.cis_bed.bedId;
                        objArg.ParamList["ward_id"] = Common.Common.cis_Room.ward_id;
                        objArg.ParamList["room_id"] = Common.Common.cis_Room.roomId;
                        objArg.ParamList["bed_no"] = Common.Common.cis_bed.bedNo;
                        objArg.ParamList["status"] = Common.Common.status;

                        try
                        {
                            Common.Common.flag = objBusinessFacade.insertBed(objArg);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                }
                if (Common.Common.flag == 1)
                {
                    MessageBox.Show("Saved Sucessfully....!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearRoomInput();
            dgvRoomAndBed.Rows.Clear();
            dgvRoomAndBed.Refresh();
            txtRoomPrefix.Text = string.Empty;
            txtRoomFrom.Text = string.Empty;
            txtRoomTo.Text = string.Empty;
            btnSave.Enabled = true;
            cboWard.SelectedIndex = 0;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnRoom_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnRoom.Checked==true)
            {
                lblRoomPrefix.Text = "Room Prefix";
                lblRoomNo.Text = "Room No";
                lblRoom.Visible = false;
                cboRoom.Visible = false;
                //cboRoom.SelectedIndex = 0;
                dgvRoomAndBed.Columns[2].HeaderText = "Room No";
                cboWard.SelectedIndex = 0;
            }
        }

        private void rbtnBed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBed.Checked == true)
            {
                lblRoomPrefix.Text = "Bed Prefix";
                lblRoomNo.Text = "Bed No";
                lblRoom.Visible = true;
                cboRoom.Visible = true;
                //cboRoom.SelectedIndex = 0;
                dgvRoomAndBed.Columns[2].HeaderText = "Bed No";
                cboWard.SelectedIndex = 0;
            }
        }

        private void cboRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnBed.Checked == true)
            {
                btnAddSingle.Enabled = true;
                btnAddBulk.Enabled = true;
                if (dgvRoomAndBed.Rows.Count > 1)
                {
                    dgvRoomAndBed.Rows.Clear();
                    dgvRoomAndBed.Refresh();
                    txtRoomPrefix.Text = string.Empty;
                    txtRoomFrom.Text = string.Empty;
                    txtRoomTo.Text = string.Empty;
                    btnSave.Enabled = true;
                }
                ViewBed();
            }
        }
    }
}
