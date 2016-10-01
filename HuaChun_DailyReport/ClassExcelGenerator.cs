using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;

namespace HuaChun_DailyReport
{
    class ClassExcelGenerator
    {
        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;

        string g_strProjectNo;
        string g_strProjectName;
        string g_strComputeType;
        string g_strComputeHoliday;
        string g_strRainyDayCountType;
        string g_strPath;
        string g_strSavePath;

        float g_fCellWidth = 30.75f;
        float g_fStartPositionH = 37;
        float g_fCellHeight = 39.75f;
        float g_fStartPositionV = 174;
        int g_iChartType = 0;
        Excel.Worksheet xlWorkSheet;
        Excel.Workbook xlWorkBook;
        
        public ClassExcelGenerator(string strProjectNo, string strSavePath, int iType)
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            g_strComputeType = SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + strProjectNo + "'");
            g_strComputeHoliday = SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + strProjectNo + "'");
            g_strRainyDayCountType = SQL.Read_SQL_data("rainyday", "project_info", "project_no = '" + strProjectNo + "'");

            g_strProjectNo = strProjectNo;
            g_strPath = Directory.GetCurrentDirectory();
            g_strSavePath = strSavePath;
            g_iChartType = iType;
        }

        public void GenerateExcel()
        {
            var xlApp = new Excel.Application();
            Excel.Workbooks xlWorkBooks = xlApp.Workbooks;
            xlWorkBook = xlWorkBooks.Open(g_strPath + "\\晴雨暨工期表.xls");
            xlWorkSheet = xlWorkBook.Sheets[1];


            g_strProjectName = SQL.Read_SQL_data("project_name", "project_info", "project_no = '" + g_strProjectNo + "'");
            string strStartDate = SQL.Read_SQL_data("startdate", "project_info", "project_no = '" + g_strProjectNo + "'");
            string strContractEndDate = SQL.Read_SQL_data("contract_finishdate", "project_info", "project_no = '" + g_strProjectNo + "'"); ;
            DateTime dtStartDate = Functions.TransferSQLDateToDateTime(strStartDate);
            DateTime dtEndDate = new DateTime();
            DateTime dtContractEndDate = Functions.TransferSQLDateToDateTime(strContractEndDate);

            if (g_iChartType == (int)ChartType.WeatherChart)
            {
                dtEndDate = ComputeValidFinishDate(dtStartDate);
            }
            else if (g_iChartType == (int)ChartType.ExpectFinishChart)
            {
                dtEndDate = dtContractEndDate;
            }

            int iYearStart = dtStartDate.Year;
            int iYearEnd = dtEndDate.Year;
            for (int i = 0; i < iYearEnd - iYearStart; ++i)
            {
                xlWorkSheet.Copy(Type.Missing, xlWorkBook.Sheets[xlWorkBook.Sheets.Count]); // copy
            }
            if (g_iChartType == (int)ChartType.WeatherChart)
            {
                WriteDataIntoExcel(dtStartDate,
                                   dtEndDate,
                                   dtContractEndDate,
                                   false);
            }
            else if (g_iChartType == (int)ChartType.ExpectFinishChart)
            {
                WriteDataIntoExcel(dtStartDate,
                                   dtEndDate,
                                   dtContractEndDate,
                                   true);
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

            MessageBox.Show("晴雨表儲存完成", "完成");
        }

        private void WriteDataIntoExcel(DateTime dtDateStart, DateTime dtDateEnd, DateTime dtContractDateEnd, bool bWriteEmptyChart)
        {
            int iYearStart = dtDateStart.Year;
            int iYearEnd = dtDateEnd.Year;

            for (int iYear = iYearStart; iYear <= iYearEnd; iYear++)
            {
                xlWorkSheet = xlWorkBook.Sheets[ iYear - iYearStart + 1 ];
                xlWorkSheet.Cells[1, 1] = g_strProjectName;
                xlWorkSheet.Cells[5, 2] = (iYear - 1911).ToString() + "年";

                xlWorkSheet.Name = (iYear - 1911).ToString() + "年度" + g_strProjectName + "工期晴雨表";

                string strPS = "";
                if (g_strComputeType == "1")//工期計算方式為：限期完工
                {
                    strPS += "工期計算方式為限期完工";
                }
                else if (g_strComputeType == "2")//工期計算方式為：日曆天
                {
                    strPS += "工期計算方式為日曆天";
                }
                else if (g_strComputeType == "3")//工期計算方式為：工作天，無週休二日
                {
                    strPS += "工期計算方式為工作工，無週休二日";
                }
                else if (g_strComputeType == "4")//工期計算方式為：工作天，週休一日
                {
                    strPS += "工期計算方式為工作工，週休一日";
                }
                else if (g_strComputeType == "5")//工期計算方式為：工作天，週休二日
                {
                    strPS += "工期計算方式為工作工，週休二日";
                }

                if (g_strComputeHoliday == "0")//國定假日照常施工
                {
                    strPS += "，國定假日照常施工";
                }
                else if (g_strComputeHoliday == "1")//國定假日不施工
                {
                    strPS += "，國定假日不施工";
                }

                if (g_strRainyDayCountType == "1")
                {
                    strPS += "(需豪雨才不計工期)";
                }
                else if (g_strRainyDayCountType == "0")
                {
                    strPS += "(下雨即不計工期)";
                }
                xlWorkSheet.Cells[33, 3] = strPS;

                for (int iMonth = 1; iMonth <= 12; iMonth++)
                {

                    float fDaysInMonth = 0;
                    float fHolidaysInMonth = 0;
                    float fWeatherNonWorkingDaysInMonth = 0;
                    float fConditionNonWorkingDaysInMonth = 0;


                    #region
                    int iDayNumbers = 0;
                    switch (iMonth)
                    {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                            iDayNumbers = 31;
                            break;
                        case 2:
                            if (iYear == 2016 || iYear == 2020 || iYear == 2024 || iYear == 2028 || iYear == 2032 || iYear == 2036)
                                iDayNumbers = 29;
                            else
                                iDayNumbers = 28;
                            break;
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            iDayNumbers = 30;
                            break;

                    }
                    #endregion


                    int iWeekCount = 0;
                    for (int iDay = 1; iDay <= iDayNumbers; iDay++)
                    {
                        DateTime dtDate = new DateTime(iYear, iMonth, iDay);
                        #region 計算dateIndex
                        int iDateIndex = 1;
                        switch (dtDate.DayOfWeek)
                        {
                            case DayOfWeek.Sunday:
                                iDateIndex = 1;
                                if (iDay == 1)
                                    iWeekCount = 0;
                                else
                                    iWeekCount++;
                                break;
                            case DayOfWeek.Monday:
                                iDateIndex = 2;
                                break;
                            case DayOfWeek.Tuesday:
                                iDateIndex = 3;
                                break;
                            case DayOfWeek.Wednesday:
                                iDateIndex = 4;
                                break;
                            case DayOfWeek.Thursday:
                                iDateIndex = 5;
                                break;
                            case DayOfWeek.Friday:
                                iDateIndex = 6;
                                break;
                            case DayOfWeek.Saturday:
                                iDateIndex = 7;
                                break;
                        }
                        iDateIndex += iWeekCount * 7;
                        string strHolidayReason = SQL.Read_SQL_data("reason", "holiday", "date = '" + Functions.TransferDateTimeToSQL(dtDate) + "'");
                        if (strHolidayReason != string.Empty)
                        {
                            xlWorkSheet.Cells[8 + iMonth * 2, 1 + iDateIndex] = strHolidayReason;
                        }
                        else
                        {
                            xlWorkSheet.Cells[8 + iMonth * 2, 1 + iDateIndex] = iDay;
                        }
                        #endregion

                        string strMorningWeather = SQL.Read_SQL_data("morning_weather", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDate) + "'");
                        string strAfternoonWeather = SQL.Read_SQL_data("afternoon_weather", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDate) + "'");
                        string strMorningCondition = SQL.Read_SQL_data("morning_condition", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDate) + "'");
                        string strAfternoonCondition = SQL.Read_SQL_data("afternoon_condition", "dailyreport", "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDate) + "'");



                        if (dtDate.CompareTo(dtDateStart) >= 0 && dtDate.CompareTo(dtDateEnd) <= 0)//開工日期之後才需要貼晴雨圖
                        {             
                            fDaysInMonth += 1;
                            #region 例假日
                            if (g_strComputeType == "1")//工期計算方式為限期完工
                            {
                                ComputeNonWorkingDay(false,
                                                     iMonth,
                                                     iDateIndex,
                                                     strMorningWeather,
                                                     strAfternoonWeather,
                                                     strMorningCondition,
                                                     strAfternoonCondition,
                                                     ref fWeatherNonWorkingDaysInMonth,
                                                     ref fConditionNonWorkingDaysInMonth,
                                                     bWriteEmptyChart);
                            }
                            else if (g_strComputeType == "2")//工期計算方式為日曆天
                            {
                                ComputeNonWorkingDay(false,
                                                     iMonth,
                                                     iDateIndex,
                                                     strMorningWeather,
                                                     strAfternoonWeather,
                                                     strMorningCondition,
                                                     strAfternoonCondition,
                                                     ref fWeatherNonWorkingDaysInMonth,
                                                     ref fConditionNonWorkingDaysInMonth,
                                                     bWriteEmptyChart);
                            }
                            else if (g_strComputeType == "3")//工期計算方式為工作天，無週休二日
                            {
                                #region
                                if (g_strComputeHoliday == "0")//國定假日照常施工
                                {
                                    ComputeNonWorkingDay(true, 
                                                         iMonth, 
                                                         iDateIndex, 
                                                         strMorningWeather, 
                                                         strAfternoonWeather, 
                                                         strMorningCondition,
                                                         strAfternoonCondition, 
                                                         ref fWeatherNonWorkingDaysInMonth, 
                                                         ref fConditionNonWorkingDaysInMonth, 
                                                         bWriteEmptyChart);
                                }
                                else if (g_strComputeHoliday == "1")//國定假日不施工
                                {
                                    #region
                                    string working = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(dtDate) + "'");
                                    if (working != string.Empty && working == "1")//遇到國定假日
                                    {
                                        ComputeNonWorkingDay(false, 
                                                             iMonth, 
                                                             iDateIndex, 
                                                             strMorningWeather, 
                                                             strAfternoonWeather, 
                                                             strMorningCondition, 
                                                             strAfternoonCondition, 
                                                             ref fWeatherNonWorkingDaysInMonth, 
                                                             ref fConditionNonWorkingDaysInMonth, 
                                                             bWriteEmptyChart);
                                            
                                        fHolidaysInMonth += 1;
                                        PrintHoliday(iMonth, iDateIndex);
                                    }
                                    else
                                    {

                                        ComputeNonWorkingDay(true, 
                                                             iMonth, 
                                                             iDateIndex, 
                                                             strMorningWeather, 
                                                             strAfternoonWeather, 
                                                             strMorningCondition, 
                                                             strAfternoonCondition, 
                                                             ref fWeatherNonWorkingDaysInMonth, 
                                                             ref fConditionNonWorkingDaysInMonth,
                                                             bWriteEmptyChart);
                                                                         
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else if (g_strComputeType == "4")//工期計算方式為工作天，週休一日
                            {
                                #region
                                if (dtDate.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    ComputeNonWorkingDay(false, 
                                                         iMonth,
                                                         iDateIndex, 
                                                         strMorningWeather,
                                                         strAfternoonWeather,
                                                         strMorningCondition,
                                                         strAfternoonCondition, 
                                                         ref fWeatherNonWorkingDaysInMonth,
                                                         ref fConditionNonWorkingDaysInMonth, 
                                                         bWriteEmptyChart);
                                    fHolidaysInMonth += 1;
                                    PrintHoliday(iMonth, iDateIndex);
                                }
                                else
                                {
                                    if (g_strComputeHoliday == "0")//國定假日照常施工
                                    {
                                        ComputeNonWorkingDay(true, 
                                                             iMonth, 
                                                             iDateIndex, 
                                                             strMorningWeather, 
                                                             strAfternoonWeather, 
                                                             strMorningCondition, 
                                                             strAfternoonCondition, 
                                                             ref fWeatherNonWorkingDaysInMonth,
                                                             ref fConditionNonWorkingDaysInMonth,
                                                             bWriteEmptyChart);
                                    }
                                    else if (g_strComputeHoliday == "1")//國定假日不施工
                                    {
                                        string working = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(dtDate) + "'");
                                        if (working != string.Empty && working == "1")
                                        {
                                            ComputeNonWorkingDay(false, 
                                                                 iMonth, iDateIndex, 
                                                                 strMorningWeather,
                                                                 strAfternoonWeather, 
                                                                 strMorningCondition, 
                                                                 strAfternoonCondition, 
                                                                 ref fWeatherNonWorkingDaysInMonth,
                                                                 ref fConditionNonWorkingDaysInMonth, 
                                                                 bWriteEmptyChart);
                                            fHolidaysInMonth += 1;
                                            PrintHoliday(iMonth, iDateIndex);
                                        }
                                        else
                                        {
                                            ComputeNonWorkingDay(true, 
                                                                 iMonth, 
                                                                 iDateIndex, 
                                                                 strMorningWeather,
                                                                 strAfternoonWeather, 
                                                                 strMorningCondition, 
                                                                 strAfternoonCondition, 
                                                                 ref fWeatherNonWorkingDaysInMonth,
                                                                 ref fConditionNonWorkingDaysInMonth, 
                                                                 bWriteEmptyChart);
                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (g_strComputeType == "5")//工期計算方式為工作天，週休二日
                            {
                                #region
                                if (dtDate.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    ComputeNonWorkingDay(false, 
                                                         iMonth, 
                                                         iDateIndex,
                                                         strMorningWeather, 
                                                         strAfternoonWeather,
                                                         strMorningCondition, 
                                                         strAfternoonCondition,
                                                         ref fWeatherNonWorkingDaysInMonth,
                                                         ref fConditionNonWorkingDaysInMonth,
                                                         bWriteEmptyChart);
                                    fHolidaysInMonth += 1;
                                    PrintHoliday(iMonth, iDateIndex);
                                }
                                else if (dtDate.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    string working = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(dtDate) + "'");
                                    if (working == string.Empty || working == "1")
                                    {
                                        ComputeNonWorkingDay(false,
                                                             iMonth,
                                                             iDateIndex,
                                                             strMorningWeather, 
                                                             strAfternoonWeather,
                                                             strMorningCondition,
                                                             strAfternoonCondition,
                                                             ref fWeatherNonWorkingDaysInMonth,
                                                             ref fConditionNonWorkingDaysInMonth,
                                                             bWriteEmptyChart);
                                        fHolidaysInMonth += 1;
                                        PrintHoliday(iMonth, iDateIndex);
                                    }
                                    else//這應該是要補班的狀況
                                    {
                                        ComputeNonWorkingDay(true,
                                                             iMonth,
                                                             iDateIndex,
                                                             strMorningWeather, 
                                                             strAfternoonWeather, 
                                                             strMorningCondition,
                                                             strAfternoonCondition,
                                                             ref fWeatherNonWorkingDaysInMonth,
                                                             ref fConditionNonWorkingDaysInMonth, 
                                                             bWriteEmptyChart);
                                    }
                                }
                                else
                                {
                                    if (g_strComputeHoliday == "0")//國定假日照常施工
                                    {
                                        ComputeNonWorkingDay(true, 
                                                             iMonth, 
                                                             iDateIndex, 
                                                             strMorningWeather, 
                                                             strAfternoonWeather,
                                                             strMorningCondition, 
                                                             strAfternoonCondition,
                                                             ref fWeatherNonWorkingDaysInMonth,
                                                             ref fConditionNonWorkingDaysInMonth, 
                                                             bWriteEmptyChart);
                                    }
                                    else if (g_strComputeHoliday == "1")//國定假日不施工
                                    {
                                        string working = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(dtDate) + "'");
                                        if (working != string.Empty && working == "1")//遇到國定假日
                                        {
                                            ComputeNonWorkingDay(false,
                                                                 iMonth, 
                                                                 iDateIndex,
                                                                 strMorningWeather,
                                                                 strAfternoonWeather,
                                                                 strMorningCondition,
                                                                 strAfternoonCondition, 
                                                                 ref fWeatherNonWorkingDaysInMonth,
                                                                 ref fConditionNonWorkingDaysInMonth, 
                                                                 bWriteEmptyChart);
                                            fHolidaysInMonth += 1;
                                            PrintHoliday(iMonth, iDateIndex);
                                        }
                                        else
                                        {
                                            ComputeNonWorkingDay(true,
                                                                 iMonth, 
                                                                 iDateIndex, 
                                                                 strMorningWeather,
                                                                 strAfternoonWeather, 
                                                                 strMorningCondition, 
                                                                 strAfternoonCondition, 
                                                                 ref fWeatherNonWorkingDaysInMonth,
                                                                 ref fConditionNonWorkingDaysInMonth, 
                                                                 bWriteEmptyChart);
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion         
                        }
                        if (dtDate.CompareTo(dtContractDateEnd) == 0)
                        {
                            PrintFinish(iMonth, iDateIndex, true);
                        }
                        if (dtDate.CompareTo(dtDateEnd) == 0)
                        {
                            PrintFinish(iMonth, iDateIndex, false);
                        }
                    }


                    xlWorkSheet.Cells[7 + iMonth * 2, 39] = fDaysInMonth;
                    xlWorkSheet.Cells[7 + iMonth * 2, 40] = fHolidaysInMonth;
                    xlWorkSheet.Cells[7 + iMonth * 2, 41] = fWeatherNonWorkingDaysInMonth;
                    xlWorkSheet.Cells[7 + iMonth * 2, 42] = fConditionNonWorkingDaysInMonth;
                }
            }
        }

        private DateTime ComputeValidFinishDate(DateTime dtStartDate)
        {
            DayCompute dayCompute = new DayCompute();

            if (g_strComputeType == "1")//限期完工  日曆天
            {

            }
            else if (g_strComputeType == "2")
            {

            }
            else if (g_strComputeType == "3")//工作天 無休
            {
                dayCompute.restOnSaturday = false;
                dayCompute.restOnSunday = false;

            }
            else if (g_strComputeType == "4")//工作天 周休一日
            {
                dayCompute.restOnSaturday = false;
                dayCompute.restOnSunday = true;

            }
            else if (g_strComputeType == "5")//工作天 周休二日
            {
                dayCompute.restOnSaturday = true;
                dayCompute.restOnSunday = true;

            }

            if (g_strComputeHoliday == "1")
            {
                dayCompute.restOnHoliday = true;
            }
            else if (g_strComputeHoliday == "0")
            {
                dayCompute.restOnHoliday = false;
            }


            float fOriginalTotalDuration = Convert.ToSingle(SQL.Read_SQL_data("contractduration", "project_info", "project_no = '" + g_strProjectNo + "'"));
            float fAccumulateExtendDurations = 0;

            int i = 0;
            bool bStop = false;
            while (!bStop)
            {
                DateTime dtDateToday = dtStartDate.AddDays(i);

                string strNonCountingToday = SQL.Read_SQL_data("nonecounting", 
                                                               "dailyreport", 
                                                               "project_no = '" + g_strProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                if (strNonCountingToday == "0.5")
                {
                    dayCompute.AddNotWorking(dtDateToday, 0);
                }
                else if (strNonCountingToday == "1")
                {
                    dayCompute.AddNotWorking(dtDateToday, 0);
                    dayCompute.AddNotWorking(dtDateToday, 1);
                }


                float fNonCountingTotal = dayCompute.CountTotalNotWorkingDay(dtStartDate, dtDateToday);


                string strExtendDuration = SQL.Read_SQL_data("extendduration", 
                                                             "extendduration", 
                                                             "project_no = '" + g_strProjectNo + "' AND extendstartdate = '" + Functions.TransferDateTimeToSQL(dtDateToday) + "'");
                if (strExtendDuration != string.Empty)
                {
                    fAccumulateExtendDurations += Convert.ToSingle(strExtendDuration);
                }

                float modifiedRestDuration = fOriginalTotalDuration - 1 - i + fNonCountingTotal + fAccumulateExtendDurations;


                i++;
                DateTime dtModifiedFinishDate = dayCompute.CountByDuration(dtDateToday.AddDays(1), modifiedRestDuration);
                if (dtDateToday.CompareTo(dtModifiedFinishDate) == 0)
                {
                    bStop = true;  
                }  
            }
            return dtStartDate.AddDays(i-1);
        }

        private void ComputeNonWorkingDay(bool bCountNonWorking, 
                                          int iMonth, 
                                          int iDateIndex, 
                                          string strMorningWeather, 
                                          string strAfternoonWeather, 
                                          string strMorningCondition, 
                                          string afternoonCondition, 
                                          ref float fWeatherNonWorkingDaysInMonth, 
                                          ref float fConditionNonWorkingDaysInMonth, 
                                          bool bWriteEmptyChart)
        {
            float fNonWorkingCount = 0;
            if (bCountNonWorking == true)
            {
                fNonWorkingCount = 0.5f;
            }
            else
            {
                fNonWorkingCount = 0;
            }
            #region 天候狀況
            if (strMorningWeather == string.Empty && !bWriteEmptyChart)//無日報表資料
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\上午無資料無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))), 
                                              g_fStartPositionV + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
            }
            else if (strMorningWeather == "晴" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\上午晴無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))), 
                                              g_fStartPositionV + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
            }
            else if (strMorningWeather == "雨" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\上午雨無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))), 
                                              g_fStartPositionV + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);

                if (g_strRainyDayCountType == "0")//下雨即不計工期
                {
                    fWeatherNonWorkingDaysInMonth += fNonWorkingCount;
                }       
            }
            else if (strMorningWeather == "豪雨" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\上午豪雨無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))), 
                                              g_fStartPositionV + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))),
                                              21, 12);
                fWeatherNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (strMorningWeather == "颱風" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\上午颱風.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
                fWeatherNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (strMorningWeather == "酷熱" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\上午酷熱無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
                fWeatherNonWorkingDaysInMonth += fNonWorkingCount;
            }


            if (strAfternoonWeather == string.Empty && !bWriteEmptyChart)//無日報表資料
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\下午無資料無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + 10 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
            }
            else if (strAfternoonWeather == "晴" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\下午晴無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + 10 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
            }
            else if (strAfternoonWeather == "雨" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\下午雨無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + 10 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
                if (g_strRainyDayCountType == "0")//下雨即不計工期
                {
                    fWeatherNonWorkingDaysInMonth += fNonWorkingCount;
                }  
            }
            else if (strAfternoonWeather == "豪雨" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\下午豪雨無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + 10 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
                fWeatherNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (strAfternoonWeather == "颱風" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\下午颱風.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + 10 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
                fWeatherNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (strAfternoonWeather == "酷熱" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\下午酷熱無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue,
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + 10 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 12);
                fWeatherNonWorkingDaysInMonth += fNonWorkingCount;
            }
            #endregion

            if (strMorningCondition == string.Empty)
            { }
            else if (strMorningCondition == "無" && !bWriteEmptyChart)
            { }
            else if (strMorningCondition == "停電" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\全日停電.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + 1 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 21);
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (strMorningCondition == "停工" && !bWriteEmptyChart)
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\全日停工.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV + 1 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              21, 21);
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (strMorningCondition == "補假")
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\補假.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH - 7 + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV - 7 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              35, 35);
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (strMorningCondition == "選舉")
            {
                xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\選舉.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_fStartPositionH - 7 + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                              g_fStartPositionV - 7 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                              35, 35);
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (strMorningCondition == "雨後泥濘" && !bWriteEmptyChart)
            {
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }

            if (afternoonCondition == string.Empty)
            { }
            else if (afternoonCondition == "無")
            { }
            else if (afternoonCondition == "停電" && !bWriteEmptyChart)
            {
                if (strMorningCondition != "停電")
                {
                    xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\下午停電.png", 
                                                 MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                                  g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                                  g_fStartPositionV + 1 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                                  21, 21);
                }
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (afternoonCondition == "停工" && !bWriteEmptyChart)
            {
                if (strMorningCondition != "停工")
                {
                    xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\下午停工.png", 
                                                  MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                                  g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                                  g_fStartPositionV + 1 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                                  21, 21);
                }
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (afternoonCondition == "補假")
            {
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (afternoonCondition == "選舉")
            {
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }
            else if (afternoonCondition == "雨後泥濘" && !bWriteEmptyChart)
            {
                fConditionNonWorkingDaysInMonth += fNonWorkingCount;
            }
        }

        private void PrintHoliday(int iMonth, int iDateIndex)
        {
            xlWorkSheet.Shapes.AddPicture(g_strPath + "\\image\\例假日.png", 
                                          MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                          g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                          g_fStartPositionV - 1 + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))), 
                                          21, 21);
        }

        private void PrintFinish(int iMonth, int iDateIndex, bool bContract)
        {
            xlWorkSheet.Shapes.AddPicture(bContract ? g_strPath + "\\image\\完工.png" : g_strPath + "\\image\\變動完工日.png",
                                          MsoTriState.msoFalse, MsoTriState.msoTrue,
                                          g_fStartPositionH + Convert.ToInt32(Math.Round(g_fCellWidth * (iDateIndex - 1))),
                                          g_fStartPositionV + Convert.ToInt32(Math.Round(g_fCellHeight * (iMonth - 1))),
                                          21, 21);
        }

        private void PrintNonWorking(int month, int dateIndex)
        {
 
        }
    }
}
