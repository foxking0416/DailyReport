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
        protected MySQL m_Sql;
        private DataTable dataTableStatistic;
        DateTime dtStartDate;
        string g_strProjectNo;
        string g_strPath;
        string g_strSavePath;
        string g_strProjectName;

        public QueryFinishForm(string projectNo, MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
            Initialize();
            LoadProjectInfo(projectNo);
        }

        private void Initialize()
        {
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
            g_strProjectName = m_Sql.Read_SQL_data("project_name", "project_info", "project_no = '" + g_strProjectNo + "'");
            dataTableStatistic.Clear();

            m_Sql.OpenSqlChannel();

            DayCompute dayCompute = new DayCompute(m_Sql);
            string computeType = m_Sql.ReadSqlDataWithoutOpenClose("computetype", "project_info", "project_no = '" + g_strProjectNo + "'");
            string countHoliday = m_Sql.ReadSqlDataWithoutOpenClose("holiday", "project_info", "project_no = '" + g_strProjectNo + "'");


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

            string rainyDayCountType = m_Sql.ReadSqlDataWithoutOpenClose("rainyday", "project_info", "project_no = '" + g_strProjectNo + "'");
            if (rainyDayCountType == "1")
            {
                this.label3.Text += "需豪雨才不計工期";
            }
            else if (rainyDayCountType == "0")
            {
                this.label3.Text += "下雨即不計工期";
            }

            float fOriginalTotalDuration = Convert.ToSingle(m_Sql.ReadSqlDataWithoutOpenClose("contractduration", "project_info", "project_no = '" + g_strProjectNo + "'"));
            float fOriginalTotalDays = Convert.ToSingle(m_Sql.ReadSqlDataWithoutOpenClose("contractdays", "project_info", "project_no = '" + g_strProjectNo + "'"));
            DateTime dtOriginalFinishDate = Functions.TransferSQLDateToDateTime(m_Sql.ReadSqlDataWithoutOpenClose("contract_finishdate", "project_info", "project_no = '" + g_strProjectNo + "'"));
            string[] arrExtendDurationStartDates = m_Sql.Read1DArray_SQL_Data("extendstartdate", "extendduration", "project_no = '" + g_strProjectNo + "'");
            float fAccumulateExtendDurations = 0;

            string strStartDate = m_Sql.ReadSqlDataWithoutOpenClose("startdate", "project_info", "project_no = '" + g_strProjectNo + "'");
            dtStartDate = Functions.TransferSQLDateToDateTime(strStartDate);

            string strConfirmFinishDate = m_Sql.ReadSqlDataWithoutOpenClose("confirm_finishdate", "project_info", "project_no = '" + g_strProjectNo + "'");

            string strLatestReportDate = m_Sql.ReadSqlDataWithoutOpenClose("date", "dailyreport", "project_no = '" + g_strProjectNo + "' ORDER BY date DESC");
            DateTime dtLatestReportDate = Functions.TransferSQLDateToDateTime(strLatestReportDate);


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
                uiDataRow["節日"] = dayCompute.GetCondition(dtDateToday);
                string morningWeather = m_Sql.ReadSqlDataWithoutOpenClose("morning_weather", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                uiDataRow["上午天氣"] = (morningWeather == string.Empty) ? "無資料" : morningWeather;
                string afternoonWeather = m_Sql.ReadSqlDataWithoutOpenClose("afternoon_weather", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                uiDataRow["下午天氣"] = (afternoonWeather == string.Empty) ? "無資料" : afternoonWeather;
                string morningCondition = m_Sql.ReadSqlDataWithoutOpenClose("morning_condition", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                uiDataRow["上午人為因素"] = (morningCondition == string.Empty) ? "無資料" : morningCondition;
                string afternoonCondition = m_Sql.ReadSqlDataWithoutOpenClose("afternoon_condition", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                uiDataRow["下午人為因素"] = (afternoonCondition == string.Empty) ? "無資料" : afternoonCondition;


                string strNonCountingToday = m_Sql.ReadSqlDataWithoutOpenClose("nonecounting", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");



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


                string extendDuration = m_Sql.ReadSqlDataWithoutOpenClose("extendduration", "extendduration", "project_no = '" + g_strProjectNo + "' AND extendstartdate = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                if (extendDuration != string.Empty)
                {
                    fAccumulateExtendDurations += Convert.ToSingle(extendDuration);
                    uiDataRow["追加工期"] = extendDuration;
                }

                float modifiedRestDuration = fOriginalTotalDuration - 1 - i + nonCountingTotal + fAccumulateExtendDurations;
                uiDataRow["變動剩餘工期"] = modifiedRestDuration;


                DateTime modifiedFinishDate = new DateTime();
                if (modifiedRestDuration < 0)
                {
                    modifiedFinishDate = dayCompute.CountByDuration(dtDateToday.AddDays(1).AddDays(modifiedRestDuration), 0);
                }
                else
                {
                    modifiedFinishDate = dayCompute.CountByDuration(dtDateToday.AddDays(1), modifiedRestDuration);
                }

                
                uiDataRow["變動完工日"] = modifiedFinishDate.ToString("yyyy/MM/dd");

                if (strConfirmFinishDate == string.Empty)//尚未設定核定完工日
                {
                    if (dtDateToday.CompareTo(dtLatestReportDate.CompareTo(modifiedFinishDate) > 0 ? dtLatestReportDate : modifiedFinishDate) == 0)
                    {
                        bStop = true;
                    }
                }
                else//已設定核定完工日
                {
                    DateTime dtRealFinishDate = dtRealFinishDate = Functions.TransferSQLDateToDateTime(strConfirmFinishDate);
                    if (dtDateToday.CompareTo(dtRealFinishDate) == 0)
                    {
                        bStop = true;
                    }
                }

                uiDataRow["變動剩餘天數"] = modifiedFinishDate.Subtract(dtDateToday).Days;

                uiDataRow["原百分比"] = "";
                dataTableStatistic.Rows.Add(uiDataRow);
                i++;
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime dateClick = dtStartDate.AddDays(e.RowIndex);
            string morningWeather = m_Sql.Read_SQL_data("morning_weather", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dateClick) + "'");
            if (morningWeather == string.Empty)//表示這天沒有日報表
            {
                DailyReportIncreaseForm reportBuildForm = new DailyReportIncreaseForm(g_strProjectNo, m_Sql);
                reportBuildForm.SetDateTodayValue(dateClick);
                reportBuildForm.ShowDialog();
                LoadProjectInfo(g_strProjectNo);
            }
            else//表示這天已經有日報表
            {
                DailyReportEditForm reportEditForm = new DailyReportEditForm(g_strProjectNo, m_Sql);
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
            if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
            {
                Cursor.Current = Cursors.WaitCursor;

                g_strPath = Directory.GetCurrentDirectory();
                g_strSavePath = saveFileDialog.FileName;


                var xlApp = new Excel.Application();
                Excel.Workbooks xlWorkBooks = xlApp.Workbooks;
                Excel.Workbook xlWorkBook = xlWorkBooks.Open(g_strPath + "\\完工總表.xls");
                Excel.Worksheet xlWorkSheet = xlWorkBook.Sheets[1];


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
