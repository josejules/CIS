using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Configuration;

using CIS;
using CIS.Common;
using CIS.Reports;
using CIS.BusinessFacade;


namespace CIS
{
    public partial class HomePageCIS : Form
    {
        public static bool isInvoice = true;
        public static bool isRegistration = true;
        public static bool isInvestigation = true;
        public static bool isPharmacy = true;
        public static bool isBiling = true;
        public static bool isAdmin = true;
        private int childFormNumber = 0;

        #region Declaration
        BusinessFacade.BusinessFacade objBusinessFacade = new BusinessFacade.BusinessFacade();
        DataTable dtSource = null;
        ComArugments objArg = new ComArugments();
        #endregion

        public HomePageCIS(string LoginUserId, int userId, int userRoleId)
        {
            InitializeComponent();
            lblUserName.Text = LoginUserId;
            tslblUserId.Text = userId.ToString();
            lblHosptialName.Text = Convert.ToString(ConfigurationSettings.AppSettings["HospitalName"]);
            lblPlace.Text = Convert.ToString(ConfigurationSettings.AppSettings["Place"]);
            Common.Common.sLoggedUser = LoginUserId;
            Common.Common.userId = userId;
            Common.Common.userRoleId = userRoleId;

            ConstructMenu(userRoleId, 1,tsInvoiceActivities); // 1 Invoice Parent Id
            tsInvoiceActivities.GripStyle = ToolStripGripStyle.Hidden;
            tsInvoiceActivities.LayoutStyle = ToolStripLayoutStyle.Flow;
            ((FlowLayoutSettings)(tsInvoiceActivities.LayoutSettings)).FlowDirection = FlowDirection.TopDown;
            tsInvoiceActivities.LayoutStyle = ToolStripLayoutStyle.Table;
            ((TableLayoutSettings)(tsInvoiceActivities.LayoutSettings)).ColumnCount = 1;

            ConstructMenu(userRoleId, 43,tsRegistrationActivities); //43 Registration Parent Id
            tsRegistrationActivities.GripStyle = ToolStripGripStyle.Hidden;
            tsRegistrationActivities.LayoutStyle = ToolStripLayoutStyle.Flow;
            ((FlowLayoutSettings)(tsRegistrationActivities.LayoutSettings)).FlowDirection = FlowDirection.TopDown;
            tsRegistrationActivities.LayoutStyle = ToolStripLayoutStyle.Table;
            ((TableLayoutSettings)(tsRegistrationActivities.LayoutSettings)).ColumnCount = 1;

            ConstructMenu(userRoleId, 24, tsInvestigationActivites); //24 Investiation Parent Id
            tsInvestigationActivites.GripStyle = ToolStripGripStyle.Hidden;
            tsInvestigationActivites.LayoutStyle = ToolStripLayoutStyle.Flow;
            ((FlowLayoutSettings)(tsInvestigationActivites.LayoutSettings)).FlowDirection = FlowDirection.TopDown;
            tsInvestigationActivites.LayoutStyle = ToolStripLayoutStyle.Table;
            ((TableLayoutSettings)(tsInvestigationActivites.LayoutSettings)).ColumnCount = 1;

            ConstructMenu(userRoleId, 44, tsPharmacyActivities); //44 Pharmacy Parent Id
            tsPharmacyActivities.GripStyle = ToolStripGripStyle.Hidden;
            tsPharmacyActivities.LayoutStyle = ToolStripLayoutStyle.Flow;
            ((FlowLayoutSettings)(tsPharmacyActivities.LayoutSettings)).FlowDirection = FlowDirection.TopDown;
            tsPharmacyActivities.LayoutStyle = ToolStripLayoutStyle.Table;
            ((TableLayoutSettings)(tsPharmacyActivities.LayoutSettings)).ColumnCount = 1;

            ConstructMenu(userRoleId, 31, tsBillingActivities); //31 Billing Parent Id
            tsBillingActivities.GripStyle = ToolStripGripStyle.Hidden;
            tsBillingActivities.LayoutStyle = ToolStripLayoutStyle.Flow;
            ((FlowLayoutSettings)(tsBillingActivities.LayoutSettings)).FlowDirection = FlowDirection.TopDown;
            tsBillingActivities.LayoutStyle = ToolStripLayoutStyle.Table;
            ((TableLayoutSettings)(tsBillingActivities.LayoutSettings)).ColumnCount = 1;

            ConstructMenu(userRoleId, 11, tsAdminActivites); //11 Admin Parent Id
            tsAdminActivites.GripStyle = ToolStripGripStyle.Hidden;
            tsAdminActivites.LayoutStyle = ToolStripLayoutStyle.Flow;
            ((FlowLayoutSettings)(tsAdminActivites.LayoutSettings)).FlowDirection = FlowDirection.TopDown;
            tsAdminActivites.LayoutStyle = ToolStripLayoutStyle.Table;
            ((TableLayoutSettings)(tsAdminActivites.LayoutSettings)).ColumnCount = 1;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void showChildForms(Form child)
        {
            disposeChildForms();
            child.MdiParent = this;
            child.Dock = DockStyle.Fill;
            child.Show();
        }

        private void disposeChildForms()
        {
            foreach (Form chd in this.MdiChildren)
            {
                if (!chd.IsDisposed)
                {
                    chd.Dispose();
                }
            }
        }

        private void tsBtnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLISLogin objShow = new frmLISLogin();
            objShow.ShowDialog();
            Common.Common.userId = 0;
        }

        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConstructMenu(int role_id, int partent_id, ToolStrip tsMenuActivities)
        {
            dtSource = objBusinessFacade.getUserActivities(role_id, partent_id);

            foreach (DataRow sRow in dtSource.Rows)
            {
                ToolStripButton tsmi = new ToolStripButton();
                tsmi.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsmi.Name = sRow["action_id"].ToString();
                tsmi.Text = sRow["action"].ToString();
                tsmi.ToolTipText = sRow["action"].ToString();
                //if (menuImages[toolStrip.Items.Count] != null)
                // tsmi.Image = menuImages[toolStrip.Items.Count];
                tsmi.TextDirection = ToolStripTextDirection.Horizontal;
                tsmi.TextImageRelation = TextImageRelation.ImageBeforeText;
                tsmi.ImageAlign = ContentAlignment.MiddleLeft;
                tsmi.TextAlign = ContentAlignment.MiddleLeft;
                tsmi.Anchor = ((AnchorStyles)(AnchorStyles.Left | AnchorStyles.Right));

                //tsmi.BackgroundImage = global::iMAS.Properties.Resources.MenuButton;
                //tsmi.BackgroundImageLayout = ImageLayout.Stretch;
                tsmi.Font = new Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                tsmi.Click += new EventHandler(tsmi_Click);
                tsMenuActivities.Items.Add(tsmi);
            }
        }

        void tsmi_Click(object sender, EventArgs e)
        {
            ToolStripButton btnActivity = sender as ToolStripButton;
            string id = btnActivity.Name;
            this.Text = btnActivity.Text;

            switch (Text)
            {
                case "Reg and Invoice":
                    showChildForms(new CIS.Modules.CISViewRegAndInvoice());
                    break;

                case "Inventory Manage":
                    showChildForms(new CIS.Modules.cisViewAndAdjustStockByItem());
                    break;

                case "Purchase":
                    showChildForms(new CIS.Modules.CISViewPurchaseReceipt());
                    break;

                case "Transfer Patient":
                    CIS.Modules.cisTransferPatient objShowTP = new CIS.Modules.cisTransferPatient();
                    objShowTP.ShowDialog();
                    break;

                case "Discharge Patient":
                    CIS.Modules.cisDischargePatient objShowDP = new CIS.Modules.cisDischargePatient();
                    objShowDP.ShowDialog();
                    break;

                case "View Bill Info":
                    showChildForms(new CIS.Modules.cisViewBillDetails());
                    break;

                case "Cancel Visit and Bill":
                    CIS.Modules.cisCancelVisitAndBill objShowCVB = new CIS.Modules.cisCancelVisitAndBill();
                    objShowCVB.ShowDialog();
                    break;

                case "Inventory Movements":
                    showChildForms(new CIS.Modules.cisViewInventoryMovements());
                    break;

                case "Deposits Transaction":
                    showChildForms(new CIS.Modules.cisViewAdvanceCollection());
                    break;

                case "Ward Bill":
                    showChildForms(new CIS.Modules.cisViewWardBill());
                    break;

                case "Reports":
                    frmReportViewer frmReportViewer = new frmReportViewer();
                    frmReportViewer.Show();
                    break;

                case "Department":
                    showChildForms(new frmViewDepartment());
                    break;

                case "Patient Type":
                    showChildForms(new Master.frmViewPatientType());
                    break;

                case "Doctor":
                    showChildForms(new Master.frmViewDoctor());
                    break;

                case "Address Info":
                    showChildForms(new Master.frmViewAddressInfo());
                    break;

                case "Corporate":
                    showChildForms(new Master.frmViewCorporate());
                    break;

                case "Room and Bed":
                    showChildForms(new Master.frmRoom());
                    break;

                case "Define Reg Fee":
                    showChildForms(new Master.frmViewDefineRegFee());
                    break;

                case "Pharmacy Master":
                    showChildForms(new Master.cisPharmacyMaster());
                    break;

                case "Investigation Master":
                    showChildForms(new Master.cisInvestigationMaster());
                    break;

                case "Account Master":
                    showChildForms(new CIS.Master.cis_account_head());
                    break;
                case "Category":
                    showChildForms(new CIS.Laboratory.frmLoadLabData("Category"));
                    break;
                case "Test Field":
                    showChildForms(new CIS.Laboratory.frmLoadLabData("TestField"));
                    break;
                case "Map Test Fields":
                    CIS.Laboratory.Master.frmTestFieldMapping objForm = new Laboratory.Master.frmTestFieldMapping();
                    objForm.Show();
                    break;
                case "Result Entry":
                    showChildForms(new CIS.Laboratory.frmResultEntryView());
                    break;
                case "View Lab Bill":
                    showChildForms(new CIS.Laboratory.frmViewLabBillInfo());
                    break;
                case "User Role":
                    showChildForms(new CIS.Laboratory.frmLoadLabData("UserRole"));
                    break;
                case "User":
                    showChildForms(new CIS.Laboratory.frmLoadLabData("User"));
                    break;
                case "User Rights":
                    CIS.Laboratory.User.frmUserMapping objForm1 = new Laboratory.User.frmUserMapping();
                    objForm1.Show();
                    break;
                case "Printer Setting":
                    showChildForms(new CIS.Laboratory.frmLoadLabData("PrinterSetting"));
                    break;
                case "Corporate Due List":
                    showChildForms(new CIS.Master.frmCorporateDueList());
                    break;
                case "Referral":
                    showChildForms(new CIS.Master.frmViewReferral());
                    break;
                default:
                    break;
            }
        }

        private void HomePageCIS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void tspInvoice_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            isInvoice = !isInvoice;
            tsInvoiceActivities.Visible = isInvoice;
        }

        private void tspRegistration_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            isRegistration = !isRegistration;
            tsRegistrationActivities.Visible = isRegistration;
        }

        private void tspInvestigation_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            isInvestigation = !isInvestigation;
            tsInvestigationActivites.Visible = isInvestigation;
        }

        private void tsPharmacy_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            isPharmacy = !isPharmacy;
            tsPharmacyActivities.Visible = isPharmacy;
        }

        private void tspBiling_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            isBiling = !isBiling;
            tsBillingActivities.Visible = isBiling;
        }

        private void tsAdmin_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            isAdmin = !isAdmin;
            tsAdminActivites.Visible = isAdmin;
        }
    }
}
