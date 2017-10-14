using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using MySql.Data.MySqlClient;

namespace HuaChun_DailyReport
{
    public partial class Main : Form
    {
        private MySQL g_Sql;
        private string g_strProjectNo = "";

        LoginForm formLogin;
        public Main()
        {
            InitializeComponent();
            Login();

            g_Sql = new MySQL();

        }

        //MenuItem Click Event

        //登入登出
        private void EventMenuItemLogin_Click(object sender, EventArgs e)
        {
            formLogin = new LoginForm(this, g_Sql);
            formLogin.ShowDialog();
        }

        private void EventMenuItemLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }
        //選擇專案
        private void EventMenuItemSelectProject_Click(object sender, EventArgs e)
        {
            ProjectSearchForm formProjectSearch = new ProjectSearchForm(this, g_Sql);
            formProjectSearch.ShowDialog();
        }

        //基本資料維護
        private void EventMenuItemProjectIncrease_Click(object sender, EventArgs e)
        {
            ProjectIncreaseForm formProjectIncrease = new ProjectIncreaseForm(g_Sql);
            formProjectIncrease.ShowDialog();
        }

        private void EventMenuItemProjectEdit_Click(object sender, EventArgs e)
        {
            ProjectEditForm formProjectEdit = new ProjectEditForm(g_Sql);
            formProjectEdit.ShowDialog();
        }

        private void EventMenuItemVendorIncrease_Click(object sender, EventArgs e)
        {
            VendorIncreaseForm formVendorIncrease = new VendorIncreaseForm(g_Sql);
            formVendorIncrease.ShowDialog();
        }

        private void EventMenuItemVendorEdit_Click(object sender, EventArgs e)
        {
            VendorEditForm formVendorEdit = new VendorEditForm(g_Sql);
            formVendorEdit.ShowDialog();
        }

        private void EventMenuItemMaterialIncrease_Click(object sender, EventArgs e)
        {
            MaterialIncreaseForm formMaterialIncrease = new MaterialIncreaseForm(g_Sql);
            formMaterialIncrease.ShowDialog();
        }

        private void EventMenuItemMaterialEdit_Click(object sender, EventArgs e)
        {
            MaterialEditForm formMaterialEdit = new MaterialEditForm(g_Sql);
            formMaterialEdit.ShowDialog();
        }

        private void EventMenuItemToolIncrease_Click(object sender, EventArgs e)
        {
            ToolIncreaseForm formToolIncrease = new ToolIncreaseForm(g_Sql);
            formToolIncrease.ShowDialog();
        }

        private void EventMenuItemToolEdit_Click(object sender, EventArgs e)
        {
            ToolEditForm formToolEdit = new ToolEditForm(g_Sql);
            formToolEdit.ShowDialog();
        }

        private void EventMenuItemLaborIncrease_Click(object sender, EventArgs e)
        {
            //新增工人別
            LaborIncreaseForm formLaborIncrease = new LaborIncreaseForm(g_Sql);
            formLaborIncrease.ShowDialog();
        }

        private void EventMenuItemLaborEdit_Click(object sender, EventArgs e)
        {
            LaborEditForm formLaborEdit = new LaborEditForm(g_Sql);
            formLaborEdit.ShowDialog();
        }

        private void EventMenuItemEmployeeIncrease_Click(object sender, EventArgs e)
        {
            MemberIncreaseForm formMemberIncrease = new MemberIncreaseForm(g_Sql);
            formMemberIncrease.ShowDialog();
        }

        private void EventMenuItemEmployeeEdit_Click(object sender, EventArgs e)
        {
            MemberEditForm formMemberEdit = new MemberEditForm(g_Sql);
            formMemberEdit.ShowDialog();
        }

        private void EventMenuItemHolidayManage_Click(object sender, EventArgs e)
        {
            HolidaySettingForm formHolidaySetting = new HolidaySettingForm(g_Sql);
            formHolidaySetting.ShowDialog();
        }

        private void EventMenuItemProcessCodeIncrease_Click(object sender, EventArgs e)
        {
            ProcessCodeIncreaseForm formProcessCodeIncrease = new ProcessCodeIncreaseForm(g_Sql);
            formProcessCodeIncrease.ShowDialog();
        }

        private void EventMenuItemProcessCodeEdit_Click(object sender, EventArgs e)
        {
            ProcessCodeEditForm formProcessCodeEdit = new ProcessCodeEditForm(g_Sql);
            formProcessCodeEdit.ShowDialog();
        }

        private void MenuItemHolidayName_Click(object sender, EventArgs e)
        {

        }



        //日報表作業
        private void EventMenuItemDailyReportBuild_Click(object sender, EventArgs e)
        {
            DailyReportIncreaseForm formDailyReportIncrease = new DailyReportIncreaseForm(g_strProjectNo, g_Sql);
            formDailyReportIncrease.ShowDialog();
        }

        private void EventMenuItemDailyReportEdit_Click(object sender, EventArgs e)
        {
            string[] reportDates = g_Sql.Read1DArray_SQL_Data("date", "dailyreport", "project_no ='" + g_strProjectNo + "' ORDER BY date DESC");
            if (reportDates.Length == 0)//表示這個工程目前並沒有輸入任何日報表
            {
                MessageBox.Show("此工程目前並沒有任何已存在的日報表,\r\n請重新選擇工程或建立日報表", "無法編輯", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DailyReportEditForm reportEditForm = new DailyReportEditForm(g_strProjectNo, g_Sql);
                reportEditForm.ShowDialog();
            }
        }

        private void MenuItemDailyReportCheck_Click(object sender, EventArgs e)
        {

        }
        

        //查詢廠商明細表
        private void MenuItemVendorList_Click(object sender, EventArgs e)
        {

        }
        //查詢人事明細表
        private void MenuItemEmployeeList_Click(object sender, EventArgs e)
        {

        }
        //查詢預計完工表
        private void MenuItemEepectFinishChart_Click(object sender, EventArgs e)
        {
            string strProjectName = g_Sql.Read_SQL_data("project_name", "project_info", "project_no ='" + g_strProjectNo + "'");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File|*.xls";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = strProjectName + "預計完工表";


            if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "" )
            {
                Cursor.Current = Cursors.WaitCursor;
                ClassExcelGenerator cExcelGen = new ClassExcelGenerator(g_strProjectNo, saveFileDialog.FileName, (int)ChartType.ExpectFinishChart, g_Sql);
                cExcelGen.GenerateExcel();
                Cursor.Current = Cursors.Default;
            }
        }
        //查詢晴雨表
        private void MenuItemWeatherChart_Click(object sender, EventArgs e)
        {
            string[] arrReportDates = g_Sql.Read1DArray_SQL_Data("date", "dailyreport", "project_no ='" + g_strProjectNo + "' ORDER BY date DESC");

            if (arrReportDates.Length == 0)//表示這個工程目前並沒有輸入任何日報表
            {
                MessageBox.Show("此工程目前並沒有任何已存在的日報表,\r\n請重新選擇工程或建立日報表", "無法建立晴雨表", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string strProjectName = g_Sql.Read_SQL_data("project_name", "project_info", "project_no ='" + g_strProjectNo + "'");
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File|*.xls";
                saveFileDialog.Title = "Save an Excel File";
                saveFileDialog.FileName = strProjectName + "晴雨表";
                if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    ClassExcelGenerator excelGen = new ClassExcelGenerator(g_strProjectNo, saveFileDialog.FileName, (int)ChartType.WeatherChart, g_Sql);
                    excelGen.GenerateExcel();
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        //查詢日報明細表
        private void MenuItemDailyReportList_Click(object sender, EventArgs e)
        {
            QueryDailyReportListForm queryDailyReportListForm = new QueryDailyReportListForm(g_Sql);
            queryDailyReportListForm.ShowDialog();
        }
        //查詢不計工期圖表
        private void MenuItemNonworkingDayChart_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayChartForm queryNonworkingDayChartForm = new QueryNonworkingDayChartForm(g_Sql);
            queryNonworkingDayChartForm.ShowDialog();
        }
        //查詢不計工期統計表
        private void MenuItemNonworkingDayStatistic_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayStatisticForm queryNonworkingDayStatisticForm = new QueryNonworkingDayStatisticForm(g_Sql);
            queryNonworkingDayStatisticForm.ShowDialog();
        }
        //查詢不計工期明細表
        private void MenuItemNonworkingDayDetail_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayDetailForm queryNonworkingDayDetailForm = new QueryNonworkingDayDetailForm(g_Sql);
            queryNonworkingDayDetailForm.ShowDialog();
        }
        //查詢完工表表
        private void MenuItemFinishChart_Click(object sender, EventArgs e)
        {
            QueryFinishForm queryFinishChartForm = new QueryFinishForm(g_strProjectNo, g_Sql);
            queryFinishChartForm.ShowDialog();
        }

        private static void OnColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            string test = e.Column.ColumnName;
            DataRow test2 = e.Row;
            string test3 = test2.ItemArray[0].ToString();
        }
        //登入
        public void Login()
        {
            this.MenuItemBasicInfo.Enabled = true;
            this.MenuItemSystem.Enabled = true;
            this.MenuItemSelectProject.Enabled = true;
        }
        //登出
        private void Logout()
        {
            this.MenuItemBasicInfo.Enabled = false;
            this.MenuItemSystem.Enabled = false;
            this.MenuItemSelectProject.Enabled = false;

            this.MenuItemDailyReport.Enabled = false;
            this.MenuItemQuery.Enabled = false;
            this.labelProject.Text = "";
            g_strProjectNo = "";
        }



        private void Print()
        {
            PrintDocument PD = new PrintDocument();

            //寫到 += 的時候按下Tab鍵會自動跳出後面的內容

            // 並且出現void PD_PrintPage(...)的列印事件

            PD.PrintPage += new PrintPageEventHandler(PD_PrintPage);

            PrintPreviewDialog PPD = new PrintPreviewDialog();

            PPD.Document = PD;

            PPD.ShowDialog();
        }

        void PD_PrintPage(object sender, PrintPageEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BiddingForm form = new BiddingForm();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        public void LoadProjectInfo(string projectNo)
        {
            this.labelProject.Text = g_Sql.Read_SQL_data("project_name", "project_info", "project_no = '" + projectNo + "'");
            g_strProjectNo = projectNo;

            this.MenuItemDailyReport.Enabled = true;
            this.MenuItemQuery.Enabled = true;

        }

        private void 標單處理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BiddingForm form = new BiddingForm();
            form.Show();
        }



 


  


































        

  

    }
}
