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

        LoginForm loginForm;
        public Main()
        {
            InitializeComponent();
            Login();

            g_Sql = new MySQL();

        }

        //MenuItem Click Event

        //登入登出
        private void MenuItemLogin_Click(object sender, EventArgs e)
        {
            loginForm = new LoginForm(this, g_Sql);
            loginForm.ShowDialog();
        }

        private void MenuItemLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }
        //選擇專案
        private void MenuItemSelectProject_Click(object sender, EventArgs e)
        {
            ProjectSearchForm form = new ProjectSearchForm(this, g_Sql);
            form.ShowDialog();
        }

        //基本資料維護
        private void MenuItemProjectIncrease_Click(object sender, EventArgs e)
        {
            ProjectIncreaseForm form = new ProjectIncreaseForm(g_Sql);
            form.ShowDialog();
        }

        private void MenuItemProjectEdit_Click(object sender, EventArgs e)
        {
            ProjectEditForm form = new ProjectEditForm(g_Sql);
            form.ShowDialog();
        }

        private void MenuItemVendorIncrease_Click(object sender, EventArgs e)
        {
            VendorIncreaseForm form = new VendorIncreaseForm(g_Sql);
            form.ShowDialog();
        }

        private void MenuItemVendorEdit_Click(object sender, EventArgs e)
        {
            VendorEditForm form = new VendorEditForm(g_Sql);
            form.ShowDialog();
        }

        private void MenuItemMaterialIncrease_Click(object sender, EventArgs e)
        {
            MaterialIncreaseForm materialIncreaseForm = new MaterialIncreaseForm(g_Sql);
            materialIncreaseForm.ShowDialog();
        }

        private void MenuItemMaterialEdit_Click(object sender, EventArgs e)
        {
            MaterialEditForm materialEditForm = new MaterialEditForm(g_Sql);
            materialEditForm.ShowDialog();
        }

        private void MenuItemToolIncrease_Click(object sender, EventArgs e)
        {
            ToolIncreaseForm toolIncreaseForm = new ToolIncreaseForm(g_Sql);
            toolIncreaseForm.ShowDialog();
        }

        private void MenuItemToolEdit_Click(object sender, EventArgs e)
        {
            ToolEditForm toolEditForm = new ToolEditForm(g_Sql);
            toolEditForm.ShowDialog();
        }

        private void MenuItemLaborIncrease_Click(object sender, EventArgs e)
        {
            //新增工人別
            LaborIncreaseForm laborIncreaseForm = new LaborIncreaseForm(g_Sql);
            laborIncreaseForm.ShowDialog();
        }

        private void MenuItemLaborEdit_Click(object sender, EventArgs e)
        {
            LaborEditForm laborEditForm = new LaborEditForm(g_Sql);
            laborEditForm.ShowDialog();
        }

        private void MenuItemEmployeeIncrease_Click(object sender, EventArgs e)
        {
            MemberIncreaseForm form = new MemberIncreaseForm(g_Sql);
            form.ShowDialog();
        }

        private void MenuItemEmployeeEdit_Click(object sender, EventArgs e)
        {
            MemberEditForm form = new MemberEditForm(g_Sql);
            form.ShowDialog();
        }

        private void MenuItemHolidayManage_Click(object sender, EventArgs e)
        {
            HolidaySettingForm holidaySettingForm = new HolidaySettingForm(g_Sql);
            holidaySettingForm.ShowDialog();
        }

        private void MenuItemProcessCodeIncrease_Click(object sender, EventArgs e)
        {
            ProcessCodeIncreaseForm processCodeIncreaseForm = new ProcessCodeIncreaseForm(g_Sql);
            processCodeIncreaseForm.ShowDialog();
        }

        private void MenuItemProcessCodeEdit_Click(object sender, EventArgs e)
        {
            ProcessCodeEditForm processCodeEditForm = new ProcessCodeEditForm(g_Sql);
            processCodeEditForm.ShowDialog();
        }

        private void MenuItemHolidayName_Click(object sender, EventArgs e)
        {

        }



        //日報表作業
        private void MenuItemDailyReportBuild_Click(object sender, EventArgs e)
        {
            DailyReportIncreaseForm reportBuildForm = new DailyReportIncreaseForm(g_strProjectNo, g_Sql);
            reportBuildForm.ShowDialog();
        }

        private void MenuItemDailyReportEdit_Click(object sender, EventArgs e)
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



 


  


































        

  

    }
}
