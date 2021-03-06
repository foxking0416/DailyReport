﻿using System;
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
        private int g_nAuthorityLevel = 0;

        LoginForm formLogin;
        public Main()
        {
            InitializeComponent();
            Login( 3 );

            g_Sql = new MySQL();

        }

        #region 登入/登出
        //登入
        private void EventMainLogin_Click(object sender, EventArgs e)
        {
            formLogin = new LoginForm(this, g_Sql);
            formLogin.ShowDialog();
        }
        //登出
        private void EventMainLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }
        //選擇專案
        private void EventMainSelectProject_Click(object sender, EventArgs e)
        {
            ProjectSearchForm formProjectSearch = new ProjectSearchForm(this, g_Sql);
            formProjectSearch.ShowDialog();
        }

        private void EventMainCreateProject_Click( object sender, EventArgs e )
        {

        }
        #endregion

        #region 基本資料維護
        //基本資料維護
        //新增廠商
        private void EventMainVendorIncrease_Click(object sender, EventArgs e)
        {
            VendorIncreaseForm formVendorIncrease = new VendorIncreaseForm(g_Sql);
            formVendorIncrease.ShowDialog();
        }
        //編輯廠商
        private void EventMainVendorEdit_Click(object sender, EventArgs e)
        {
            VendorEditForm formVendorEdit = new VendorEditForm(g_Sql);
            formVendorEdit.ShowDialog();
        }
        //新增材料
        private void EventMainMaterialIncrease_Click(object sender, EventArgs e)
        {
            MaterialIncreaseForm formMaterialIncrease = new MaterialIncreaseForm(g_Sql);
            formMaterialIncrease.ShowDialog();
        }
        //編輯材料
        private void EventMainMaterialEdit_Click(object sender, EventArgs e)
        {
            MaterialEditForm formMaterialEdit = new MaterialEditForm(g_Sql);
            formMaterialEdit.ShowDialog();
        }
        //新增機具
        private void EventMainToolIncrease_Click(object sender, EventArgs e)
        {
            ToolIncreaseForm formToolIncrease = new ToolIncreaseForm(g_Sql);
            formToolIncrease.ShowDialog();
        }
        //編輯機具
        private void EventMainToolEdit_Click(object sender, EventArgs e)
        {
            ToolEditForm formToolEdit = new ToolEditForm(g_Sql);
            formToolEdit.ShowDialog();
        }
        //新增工人別
        private void EventMainLaborIncrease_Click(object sender, EventArgs e)
        {
            //新增工人別
            LaborIncreaseForm formLaborIncrease = new LaborIncreaseForm(g_Sql);
            formLaborIncrease.ShowDialog();
        }
        //編輯工人別
        private void EventMainLaborEdit_Click(object sender, EventArgs e)
        {
            LaborEditForm formLaborEdit = new LaborEditForm(g_Sql);
            formLaborEdit.ShowDialog();
        }
        //新增外包
        private void EventMainOutsourceIncrease_Click( object sender, EventArgs e )
        {
            OutsourceIncreaseForm formOutsourceIncrease = new OutsourceIncreaseForm( g_Sql );
            formOutsourceIncrease.ShowDialog();
        }
        //編輯外包
        private void EventMainOutsourceEdit_Click( object sender, EventArgs e )
        {
            OutsourceEditForm formOutsourceEdit = new OutsourceEditForm( g_Sql );
            formOutsourceEdit.ShowDialog();
        }
        
        //新增人事
        private void EventMainEmployeeIncrease_Click(object sender, EventArgs e)
        {
            MemberIncreaseForm formMemberIncrease = new MemberIncreaseForm(g_Sql);
            formMemberIncrease.ShowDialog();
        }
        //編輯現有人事
        private void EventMainEmployeeEdit_Click(object sender, EventArgs e)
        {
            MemberEditForm formMemberEdit = new MemberEditForm(g_Sql);
            formMemberEdit.ShowDialog();
        }
        //新增施工項目編碼
        private void EventMainProcessCodeIncrease_Click(object sender, EventArgs e)
        {
            ProcessCodeIncreaseForm formProcessCodeIncrease = new ProcessCodeIncreaseForm(g_Sql);
            formProcessCodeIncrease.ShowDialog();
        }
        //編輯施工項目編碼
        private void EventMainProcessCodeEdit_Click(object sender, EventArgs e)
        {
            ProcessCodeEditForm formProcessCodeEdit = new ProcessCodeEditForm(g_Sql);
            formProcessCodeEdit.ShowDialog();
        }
        //編輯假日
        private void EventMainHolidayManage_Click( object sender, EventArgs e )
        {
            HolidaySettingForm formHolidaySetting = new HolidaySettingForm( g_Sql );
            formHolidaySetting.ShowDialog();
        }
        #endregion

        #region 投標處理
        //新增投標規格表
        private void EventTendorIncrease_Click( object sender, EventArgs e )
        {
            TendorManageForm formTendorManage = new TendorManageForm( g_Sql );
            formTendorManage.ShowDialog();
        }
        //編輯投標規格表
        private void MenuItemTendorEdit_Click( object sender, EventArgs e )
        {

        }
        #endregion

        #region 工程資料維護
        //工程資料維護
        //新增工程
        private void EventProjectIncrease_Click( object sender, EventArgs e )
        {
            ProjectIncreaseForm formProjectIncrease = new ProjectIncreaseForm( g_Sql );
            formProjectIncrease.ShowDialog();
        }
        //編輯工程
        private void EventProjectEdit_Click( object sender, EventArgs e )
        {
            ProjectEditForm formProjectEdit = new ProjectEditForm( g_Sql );
            formProjectEdit.ShowDialog();
        }

        private void EventConstantFactor_Click( object sender, EventArgs e )
        {
            ConstantFactorSettingForm formConstantFactorSetting = new ConstantFactorSettingForm( g_strProjectNo, g_Sql );
            formConstantFactorSetting.ShowDialog();
        }

        private void EventVeriedFactor_Click( object sender, EventArgs e )
        {
            VariedFactorSettingForm formVariedFactorSetting = new VariedFactorSettingForm( g_strProjectNo, g_Sql );
            formVariedFactorSetting.ShowDialog();
        }

        private void EventCreateSubItem_Click( object sender, EventArgs e )
        {
            CreateSubItemForm formCreateSubItem = new CreateSubItemForm( g_strProjectNo, g_Sql );
            formCreateSubItem.ShowDialog();
        }

        private void EventCreateQuotation_Click( object sender, EventArgs e )
        {
            CreateVendorQuotationForm formCreateQuotation = new CreateVendorQuotationForm( g_strProjectNo, g_Sql );
            formCreateQuotation.ShowDialog();
        }
        #endregion

        #region 日報表作業
        //新增日報表
        private void EventMainDailyReportIncrease_Click(object sender, EventArgs e)
        {
            DailyReportIncreaseForm formDailyReportIncrease = new DailyReportIncreaseForm( g_strProjectNo, g_Sql );
            formDailyReportIncrease.ShowDialog();
        }
        //編輯日報表
        private void EventMainDailyReportEdit_Click(object sender, EventArgs e)
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
        //檢查日報表
        private void EventMainDailyReportCheck_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 查詢
        //查詢廠商明細表
        private void EventMainVendorList_Click(object sender, EventArgs e)
        {

        }
        //查詢人事明細表
        private void EventMainEmployeeList_Click(object sender, EventArgs e)
        {

        }
        //查詢預計完工表
        private void EventMainExpectFinishChart_Click(object sender, EventArgs e)
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
        private void EventMainWeatherChart_Click(object sender, EventArgs e)
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
        private void EventMainDailyReportList_Click(object sender, EventArgs e)
        {
            QueryDailyReportListForm queryDailyReportListForm = new QueryDailyReportListForm(g_Sql);
            queryDailyReportListForm.ShowDialog();
        }
        //查詢不計工期圖表
        private void EventMainNonworkingDayChart_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayChartForm queryNonworkingDayChartForm = new QueryNonworkingDayChartForm(g_Sql);
            queryNonworkingDayChartForm.ShowDialog();
        }
        //查詢不計工期統計表
        private void EventMainNonworkingDayStatistic_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayStatisticForm queryNonworkingDayStatisticForm = new QueryNonworkingDayStatisticForm(g_Sql);
            queryNonworkingDayStatisticForm.ShowDialog();
        }
        //查詢不計工期明細表
        private void EventMainNonworkingDayDetail_Click(object sender, EventArgs e)
        {
            QueryNonworkingDayDetailForm queryNonworkingDayDetailForm = new QueryNonworkingDayDetailForm(g_Sql);
            queryNonworkingDayDetailForm.ShowDialog();
        }
        //查詢完工總表
        private void EventMainFinishChart_Click(object sender, EventArgs e)
        {
            QueryFinishForm queryFinishChartForm = new QueryFinishForm(g_strProjectNo, g_Sql);
            queryFinishChartForm.ShowDialog();
        }
        #endregion

        private static void OnColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            string test = e.Column.ColumnName;
            DataRow test2 = e.Row;
            string test3 = test2.ItemArray[0].ToString();
        }
        //登入
        public void Login( int nAuthorityLevel )
        {
            g_nAuthorityLevel = nAuthorityLevel;

            switch ( g_nAuthorityLevel )
            {
                case ( int )AuthorityLevle.NON:
                    break;
                case ( int )AuthorityLevle.GENERAL_EMPLOYEE:
                    this.MenuItemSelectProject.Enabled = true;
                    this.MenuItemBasicInfo.Enabled = false;
                    break;
                case ( int )AuthorityLevle.MANAGER:
                    this.MenuItemSelectProject.Enabled = true;
                    this.MenuItemBasicInfo.Enabled = true;
                    this.MenuItemProjectInfo.Enabled = true;
                    break;
                case ( int )AuthorityLevle.POWER_USE:
                    this.MenuItemSelectProject.Enabled = true;
                    this.MenuItemBasicInfo.Enabled = true;
                    this.MenuItemProjectInfo.Enabled = true;
                    break;
            }

            this.MenuItemSystem.Enabled = true;
            this.MenuItemLogout.Enabled = true;
            this.MenuItemLogin.Enabled = false;
        }
        //登出
        private void Logout()
        {
            this.MenuItemLogin.Enabled = true;
            this.MenuItemLogout.Enabled = false;
            this.MenuItemSelectProject.Enabled = false;


            this.MenuItemBasicInfo.Enabled = false;
            this.MenuItemProjectInfo.Enabled = false;
            this.MenuItemDailyReport.Enabled = false;
            this.MenuItemQuery.Enabled = false;
            this.MenuItemSystem.Enabled = false;

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
