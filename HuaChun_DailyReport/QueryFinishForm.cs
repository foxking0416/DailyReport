using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace HuaChun_DailyReport
{
    public partial class QueryFinishForm : Form
    {
        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;
        private DataTable dataTableStatistic;
        DateTime dtStartDate;
        string g_strProjectNo;
        string g_strPath;
        string g_strSavePath;
        string g_strProjectName;

        public QueryFinishForm(string projectNo)
        {
            InitializeComponent();
            Initialize();
            LoadProjectInfo(projectNo);
        }

        private void Initialize()
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            dataTableStatistic = new DataTable("FinishStatisticTable");
            dataTableStatistic.Columns.Add("日期", typeof(String));
            dataTableStatistic.Columns.Add("開工迄今", typeof(String));
            dataTableStatistic.Columns.Add("星期", typeof(String));
            dataTableStatistic.Columns.Add("節日", typeof(String));
            dataTableStatistic.Columns.Add("上午天氣", typeof(String));
            dataTableStatistic.Columns.Add("下午天氣", typeof(String));
            dataTableStatistic.Columns.Add("上午人為因素", typeof(String));
            dataTableStatistic.Columns.Add("下午人為因素", typeof(String));
            dataTableStatistic.Columns.Add("本日不計工期", typeof(String));
            dataTableStatistic.Columns.Add("累計不計工期", typeof(String));
            dataTableStatistic.Columns.Add("累計工期", typeof(String));
            dataTableStatistic.Columns.Add("原剩餘工期", typeof(String));
            dataTableStatistic.Columns.Add("原剩餘天數", typeof(String));
            dataTableStatistic.Columns.Add("原完工日", typeof(String));

            dataTableStatistic.Columns.Add("追加工期", typeof(String));

            dataTableStatistic.Columns.Add("變動剩餘工期", typeof(String));
            dataTableStatistic.Columns.Add("變動剩餘天數", typeof(String));
            dataTableStatistic.Columns.Add("變動完工日", typeof(String));
            dataTableStatistic.Columns.Add("原百分比", typeof(String));
            dataGridView1.DataSource = dataTableStatistic;
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;


            dataTableStatistic.Columns[3].AllowDBNull = true;
        }

        public void LoadProjectInfo(string number)
        {
            Cursor.Current = Cursors.WaitCursor;
            g_strProjectNo = number;
            dataTableStatistic.Clear();


            DayCompute dayCompute = new DayCompute();
            string computeType = SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + g_strProjectNo + "'");
            string countHoliday = SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + g_strProjectNo + "'");


            if (computeType == "1")//限期完工  日曆天
            {
                this.label1.Text = "工期計算方式為限期完工";
            }
            else if (computeType == "2")
            {
                this.label1.Text = "工期計算方式為日曆天";
            }
            else if (computeType == "3")//工作天 無休
            {
                dayCompute.restOnSaturday = false;
                dayCompute.restOnSunday = false;
                this.label1.Text = "工期計算方式為工作工，無週休二日";
            }
            else if (computeType == "4")//工作天 周休一日
            {
                dayCompute.restOnSaturday = false;
                dayCompute.restOnSunday = true;
                this.label1.Text = "工期計算方式為工作工，週休一日";
            }
            else if (computeType == "5")//工作天 周休二日
            {
                dayCompute.restOnSaturday = true;
                dayCompute.restOnSunday = true;
                this.label1.Text = "工期計算方式為工作工，週休二日";
            }

            if (countHoliday == "1")
            {
                dayCompute.restOnHoliday = true;
                this.label1.Text += "，國定假日不施工";
            }
            else if (countHoliday == "0")
            {
                dayCompute.restOnHoliday = false;
                this.label1.Text += "，國定假日照常施工";
            }

            string rainyDayCountType = SQL.Read_SQL_data("rainyday", "project_info", "project_no = '" + g_strProjectNo + "'");
            if (rainyDayCountType == "1")
            {
                this.label3.Text += "需豪雨才不計工期";
            }
            else if (rainyDayCountType == "0")
            {
                this.label3.Text += "下雨即不計工期";
            }

            float fOriginalTotalDuration = Convert.ToSingle(SQL.Read_SQL_data("contractduration", "project_info", "project_no = '" + g_strProjectNo + "'"));
            float fOriginalTotalDays = Convert.ToSingle(SQL.Read_SQL_data("contractdays", "project_info", "project_no = '" + g_strProjectNo + "'"));
            DateTime dtOriginalFinishDate = Functions.TransferSQLDateToDateTime(SQL.Read_SQL_data("contract_finishdate", "project_info", "project_no = '" + g_strProjectNo + "'"));
            string[] arrExtendDurationStartDates = SQL.Read1DArray_SQL_Data("extendstartdate", "extendduration", "project_no = '" + g_strProjectNo + "'");
            float fAccumulateExtendDurations = 0;

            string strStartDate = SQL.Read_SQL_data("startdate", "project_info", "project_no = '" + g_strProjectNo + "'");
            dtStartDate = Functions.TransferSQLDateToDateTime(strStartDate);
            DataRow uiDataRow;

            int i = 0;
            bool bStop = false;
            while (!bStop)
            {

                DateTime dtDateToday = dtStartDate.AddDays(i);

                uiDataRow = dataTableStatistic.NewRow();


                uiDataRow["日期"] = dtDateToday.ToString("yyyy/MM/dd");
                uiDataRow["開工迄今"] = (i + 1).ToString();
                uiDataRow["星期"] = Functions.ComputeDayOfWeek(dtDateToday);
                //Image img = Image.FromFile("D:\\12Small.jpg");
                uiDataRow["節日"] = dayCompute.GetCondition(dtDateToday);
                string morningWeather = SQL.Read_SQL_data("morning_weather", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                uiDataRow["上午天氣"] = (morningWeather == string.Empty) ? "無資料" : morningWeather;
                string afternoonWeather = SQL.Read_SQL_data("afternoon_weather", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                uiDataRow["下午天氣"] = (afternoonWeather == string.Empty) ? "無資料" : afternoonWeather;
                string morningCondition = SQL.Read_SQL_data("morning_condition", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                uiDataRow["上午人為因素"] = (morningCondition == string.Empty) ? "無資料" : morningCondition;
                string afternoonCondition = SQL.Read_SQL_data("afternoon_condition", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                uiDataRow["下午人為因素"] = (afternoonCondition == string.Empty) ? "無資料" : afternoonCondition;
                
                
                string strNonCountingToday = SQL.Read_SQL_data("nonecounting", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");



                if (strNonCountingToday == "0.5")
                    dayCompute.AddNotWorking(dtDateToday, 0);
                else if (strNonCountingToday == "1")
                {
                    dayCompute.AddNotWorking(dtDateToday, 0);
                    dayCompute.AddNotWorking(dtDateToday, 1);
                }

                uiDataRow["本日不計工期"] = dayCompute.GetWorkingDayNonCounting(dtDateToday);

                float nonCountingTotal = dayCompute.CountTotalNotWorkingDay(dtStartDate, dtDateToday);

                uiDataRow["累計不計工期"] = nonCountingTotal;
                uiDataRow["累計工期"] = i + 1 - nonCountingTotal;


                uiDataRow["原剩餘工期"] = fOriginalTotalDuration - 1 - i + dayCompute.CountNotWorkingDayWithoutEverydayCondition(dtStartDate, dtDateToday);
                uiDataRow["原剩餘天數"] = fOriginalTotalDays - 1 - i;
                uiDataRow["原完工日"] = dtOriginalFinishDate.ToString("yyyy/MM/dd");


                string extendDuration = SQL.Read_SQL_data("extendduration", "extendduration", "project_no = '" + g_strProjectNo + "' AND extendstartdate = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                if (extendDuration != string.Empty)
                {
                    fAccumulateExtendDurations += Convert.ToSingle(extendDuration);
                    uiDataRow["追加工期"] = extendDuration;
                }

                float modifiedRestDuration = fOriginalTotalDuration - 1 - i + nonCountingTotal + fAccumulateExtendDurations;
                uiDataRow["變動剩餘工期"] = modifiedRestDuration;


                DateTime modifiedFinishDate = dayCompute.CountByDuration(dtDateToday.AddDays(1), modifiedRestDuration);
                uiDataRow["變動完工日"] = modifiedFinishDate.ToString("yyyy/MM/dd");
                if (dtDateToday.CompareTo(modifiedFinishDate) == 0)
                {
                    bStop = true;
                }
                uiDataRow["變動剩餘天數"] = modifiedFinishDate.Subtract(dtDateToday).Days;

                uiDataRow["原百分比"] = "";
                dataTableStatistic.Rows.Add(uiDataRow);
                i++;
            }
            Cursor.Current = Cursors.Default;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime dateClick = dtStartDate.AddDays(e.RowIndex);
            string morningWeather = SQL.Read_SQL_data("morning_weather", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dateClick) + "'");
            if (morningWeather == string.Empty)//表示這天沒有日報表
            {
                DailyReportIncreaseForm reportBuildForm = new DailyReportIncreaseForm(false);
                reportBuildForm.LoadProjectInfo(g_strProjectNo);
                reportBuildForm.SetDateTodayValue(dateClick);
                reportBuildForm.ShowDialog();
                LoadProjectInfo(g_strProjectNo);
            }
            else//表示這天已經有日報表
            {
                DailyReportEditForm reportEditForm = new DailyReportEditForm(g_strProjectNo);
                reportEditForm.LoadProjectInfo(g_strProjectNo);
                reportEditForm.SetDateTodayValue(dateClick);
                reportEditForm.ShowDialog();
                LoadProjectInfo(g_strProjectNo);
            }

        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File|*.xls";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = g_strProjectName + "完工總表";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                Cursor.Current = Cursors.WaitCursor;

                g_strPath = Directory.GetCurrentDirectory();
                g_strSavePath = saveFileDialog.FileName;


                var xlApp = new Excel.Application();
                Excel.Workbooks xlWorkBooks = xlApp.Workbooks;
                Excel.Workbook xlWorkBook = xlWorkBooks.Open(g_strPath + "\\完工總表.xls");
                Excel.Worksheet xlWorkSheet = xlWorkBook.Sheets[1];

                g_strProjectName = SQL.Read_SQL_data("project_name", "project_info", "project_no = '" + g_strProjectNo + "'");
                xlWorkBook.Sheets[1].Name = g_strProjectName + "完工總表";
                xlWorkSheet.Cells[1, 2] = g_strProjectName;


                for (int i = 0; i < dataTableStatistic.Rows.Count; ++i)
                {
                    DataRow dataRow = dataTableStatistic.Rows[i];
                    for (int j = 0; j < dataTableStatistic.Columns.Count; ++j)
                    {
                        xlWorkSheet.Cells[i + 3, j+1] = dataRow[j].ToString();
                    }
                }

                xlApp.DisplayAlerts = false;
                xlWorkBook.SaveAs(g_strSavePath);
                xlApp.DisplayAlerts = true;
                xlWorkBook.Close(0);
                xlWorkBooks.Close();
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp.Workbooks);
                Marshal.ReleaseComObject(xlApp);
                MessageBox.Show("完工總表儲存完成", "完成");
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
