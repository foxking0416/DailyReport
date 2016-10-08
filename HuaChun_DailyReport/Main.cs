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

        private string dbHost;
        private string dbUser;
        private string dbPass;
        private string dbName;
        private MySQL SQL;
        private string g_strProjectNo = "";

        LoginForm loginForm;
        public Main()
        {
            InitializeComponent();
            Login();

            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");

            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

        }

        //MenuItem Click Event

        //登入登出
        private void MenuItemLogin_Click(object sender, EventArgs e)
        {
            loginForm = new LoginForm(this);
            loginForm.ShowDialog();
        }

        private void MenuItemLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }
        //選擇專案
        private void MenuItemSelectProject_Click(object sender, EventArgs e)
        {
            ProjectSearchForm form = new ProjectSearchForm(this);
            form.ShowDialog();
        }

        //基本資料維護
        private void MenuItemProjectIncrease_Click(object sender, EventArgs e)
        {
            ProjectIncreaseForm form = new ProjectIncreaseForm();
            form.ShowDialog();
        }

        private void MenuItemProjectEdit_Click(object sender, EventArgs e)
        {
            ProjectEditForm form = new ProjectEditForm();
            form.ShowDialog();
        }

        private void MenuItemVendorIncrease_Click(object sender, EventArgs e)
        {
            VendorIncreaseForm form = new VendorIncreaseForm();
            form.ShowDialog();
        }

        private void MenuItemVendorEdit_Click(object sender, EventArgs e)
        {
            VendorEditForm form = new VendorEditForm();
            form.ShowDialog();
        }

        private void MenuItemMaterialIncrease_Click(object sender, EventArgs e)
        {
            MaterialIncreaseForm materialIncreaseForm = new MaterialIncreaseForm();
            materialIncreaseForm.ShowDialog();
        }

        private void MenuItemMaterialEdit_Click(object sender, EventArgs e)
        {
            MaterialEditForm materialEditForm = new MaterialEditForm();
            materialEditForm.ShowDialog();
        }

        private void MenuItemToolIncrease_Click(object sender, EventArgs e)
        {
            ToolIncreaseForm toolIncreaseForm = new ToolIncreaseForm();
            toolIncreaseForm.ShowDialog();
        }

        private void MenuItemToolEdit_Click(object sender, EventArgs e)
        {
            ToolEditForm toolEditForm = new ToolEditForm();
            toolEditForm.ShowDialog();
        }

        private void MenuItemLaborIncrease_Click(object sender, EventArgs e)
        {
            //新增工人別
            LaborIncreaseForm laborIncreaseForm = new LaborIncreaseForm();
            laborIncreaseForm.ShowDialog();
        }

        private void MenuItemLaborEdit_Click(object sender, EventArgs e)
        {
            LaborEditForm laborEditForm = new LaborEditForm();
            laborEditForm.ShowDialog();
        }

        private void MenuItemEmployeeIncrease_Click(object sender, EventArgs e)
        {
            MemberIncreaseForm form = new MemberIncreaseForm();
            form.ShowDialog();
        }

        private void MenuItemEmployeeEdit_Click(object sender, EventArgs e)
        {
            MemberEditForm form = new MemberEditForm();
            form.ShowDialog();
        }

        private void MenuItemHolidayManage_Click(object sender, EventArgs e)
        {
            HolidaySettingForm holidaySettingForm = new HolidaySettingForm();
            holidaySettingForm.ShowDialog();
        }

        private void MenuItemProcessCodeIncrease_Click(object sender, EventArgs e)
        {
            ProcessCodeIncreaseForm processCodeIncreaseForm = new ProcessCodeIncreaseForm();
            processCodeIncreaseForm.ShowDialog();
        }

        private void MenuItemProcessCodeEdit_Click(object sender, EventArgs e)
        {
            ProcessCodeEditForm processCodeEditForm = new ProcessCodeEditForm();
            processCodeEditForm.ShowDialog();
        }

        private void MenuItemHolidayName_Click(object sender, EventArgs e)
        {

        }



        //日報表作業
        private void MenuItemDailyReportBuild_Click(object sender, EventArgs e)
        {
            DailyReportIncreaseForm reportBuildForm = new DailyReportIncreaseForm(g_strProjectNo);
            reportBuildForm.ShowDialog();
        }

        private void MenuItemDailyReportEdit_Click(object sender, EventArgs e)
        {
            string[] reportDates = SQL.Read1DArray_SQL_Data("date", "dailyreport", "project_no ='" + g_strProjectNo + "' ORDER BY date DESC");
            if (reportDates.Length == 0)//表示這個工程目前並沒有輸入任何日報表
            {
                MessageBox.Show("此工程目前並沒有任何已存在的日報表,\r\n請重新選擇工程或建立日報表", "無法編輯", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DailyReportEditForm reportEditForm = new DailyReportEditForm(g_strProjectNo);
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
            string strProjectName = SQL.Read_SQL_data("project_name", "project_info", "project_no ='" + g_strProjectNo + "'");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File|*.xls";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = strProjectName + "預計完工表";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Cursor.Current = Cursors.WaitCursor;
                ClassExcelGenerator cExcelGen = new ClassExcelGenerator(g_strProjectNo, saveFileDialog.FileName, (int)ChartType.ExpectFinishChart);
                cExcelGen.GenerateExcel();
                Cursor.Current = Cursors.Default;
            }
        }
        //查詢晴雨表
        private void MenuItemWeatherChart_Click(object sender, EventArgs e)
        {
            string[] arrReportDates = SQL.Read1DArray_SQL_Data("date", "dailyreport", "project_no ='" + g_strProjectNo + "' ORDER BY date DESC");

            if (arrReportDates.Length == 0)//表示這個工程目前並沒有輸入任何日報表
            {
                MessageBox.Show("此工程目前並沒有任何已存在的日報表,\r\n請重新選擇工程或建立日報表", "無法建立晴雨表", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string strProjectName = SQL.Read_SQL_data("project_name", "project_info", "project_no ='" + g_strProjectNo + "'");
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File|*.xls";
                saveFileDialog.Title = "Save an Excel File";
                saveFileDialog.FileName = strProjectName + "晴雨表";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    ClassExcelGenerator excelGen = new ClassExcelGenerator(g_strProjectNo, saveFileDialog.FileName, (int)ChartType.WeatherChart);
                    excelGen.GenerateExcel();
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        //查詢日報明細表
        private void MenuItemDailyReportList_Click(object sender, EventArgs e)
        {
            QueryDailyReportListForm queryDailyReportListForm = new QueryDailyReportListForm();
            queryDailyReportListForm.ShowDialog();
        }
        //查詢不計工期圖表
        private void MenuItemNonworkingDayChart_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayChartForm queryNonworkingDayChartForm = new QueryNonworkingDayChartForm();
            queryNonworkingDayChartForm.ShowDialog();
        }
        //查詢不計工期統計表
        private void MenuItemNonworkingDayStatistic_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayStatisticForm queryNonworkingDayStatisticForm = new QueryNonworkingDayStatisticForm();
            queryNonworkingDayStatisticForm.ShowDialog();
        }
        //查詢不計工期明細表
        private void MenuItemNonworkingDayDetail_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayDetailForm queryNonworkingDayDetailForm = new QueryNonworkingDayDetailForm();
            queryNonworkingDayDetailForm.ShowDialog();
        }
        //查詢完工表表
        private void MenuItemFinishChart_Click(object sender, EventArgs e)
        {
            QueryFinishForm queryFinishChartForm = new QueryFinishForm(g_strProjectNo);
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

            SQL.TestSqlCommand();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQL.TestSqlCommand2();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string dbHost = "192.168.1.104";
            string dbUser = "weichien";
            string dbPass = "chichi1219";
            string dbName = "huachun";
            MySQL SQL = new MySQL(dbHost, dbUser, dbPass, dbName);
            string computeType = SQL.Read_SQL_data("computetype", "project_info", "project_no = 'w04'");
            bool stop = true;
        }

        public void LoadProjectInfo(string projectNo)
        {
            this.labelProject.Text = SQL.Read_SQL_data("project_name", "project_info", "project_no = '" + projectNo + "'");
            g_strProjectNo = projectNo;

            this.MenuItemDailyReport.Enabled = true;
            this.MenuItemQuery.Enabled = true;

        }



 


  


































        

  

    }
}
