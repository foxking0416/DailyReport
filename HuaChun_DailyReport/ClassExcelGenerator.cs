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
using System.Threading;

namespace HuaChun_DailyReport
{
    class ClassExcelGenerator
    {
        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;

        Excel.Workbook xlWorkBook;
        //Excel.Workbooks xlWorkBooks;
        Excel.Worksheet xlWorkSheet;
        string g_ProjectNo;
        string g_ProjectName;
        string g_ComputeType;
        string g_ComputeHoliday;
        string g_StrPath;
        string g_StrSavePath;
        float g_CellWidth = 30.75f;
        float g_StartPositionH = 37;
        float g_CellHeight = 39.75f;
        float g_startPositionV = 174;
        
        public ClassExcelGenerator(string strProjectNo, string strSavePath, int type)
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);



            string computeType = SQL.Read_SQL_data("computetype", "project_info", "project_no = '" + strProjectNo + "'");
            g_ComputeType = computeType;

            string computeHoliday = SQL.Read_SQL_data("holiday", "project_info", "project_no = '" + strProjectNo + "'");
            g_ComputeHoliday = computeHoliday;


            g_ProjectNo = strProjectNo;
            g_StrPath = Directory.GetCurrentDirectory();
            g_StrSavePath = strSavePath;

            var xlApp = new Excel.Application();
            //xlWorkBooks = xlApp.Workbooks;
            xlWorkBook = xlApp.Workbooks.Open(g_StrPath + "\\晴雨暨工期表.xls");
            xlWorkSheet = xlWorkBook.Sheets[1];

            g_ProjectName = SQL.Read_SQL_data("project_name", "project_info", "project_no = '" + g_ProjectNo + "'");
            string startDate = SQL.Read_SQL_data("startdate", "project_info", "project_no = '" + g_ProjectNo + "'");
            string endDate = "";

            if (type == 0)
            {
                endDate = SQL.Read_SQL_data("date", "dailyreport", "project_no = '" + g_ProjectNo + "' ORDER BY date DESC ");//把日報表有填的日期的最後一天當enddate
            }
            else if (type == 1)
            {
                endDate = SQL.Read_SQL_data("contract_finishdate", "project_info", "project_no = '" + g_ProjectNo + "'");
            }

            int yearStart = Functions.TransferSQLDateToDateTime(startDate).Year;
            int yearEnd = Functions.TransferSQLDateToDateTime(endDate).Year;
            for (int i = 0; i < yearEnd - yearStart; ++i)
            {
                xlWorkSheet.Copy(Type.Missing, xlWorkBook.Sheets[xlWorkBook.Sheets.Count]); // copy
            }

            WriteDataIntoExcel(Functions.TransferSQLDateToDateTime(startDate), Functions.TransferSQLDateToDateTime(endDate));

            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs(g_StrSavePath);
            xlApp.DisplayAlerts = true;
            xlWorkBook.Close(false);
            //xlApp.Workbooks.Close();
            xlApp.Quit();

            Marshal.ReleaseComObject(xlApp);
            Marshal.ReleaseComObject(xlWorkBook);
            //Marshal.ReleaseComObject(xlApp.Workbooks);

            MessageBox.Show("晴雨表儲存完成", "完成");

        }

        private void WriteDataIntoExcel(DateTime dateStart, DateTime dateEnd)
        {
            //dateEnd = dateStart.AddDays(2);

            int yearStart = dateStart.Year;
            int yearEnd = dateEnd.Year;

            for (int year = yearStart; year <= yearEnd; year++)
            {
                xlWorkSheet = xlWorkBook.Sheets[ year - yearStart + 1 ];
                xlWorkSheet.Cells[1, 1] = g_ProjectName;
                xlWorkSheet.Cells[5, 2] = (year - 1911).ToString() + "年";

                xlWorkSheet.Name = (year - 1911).ToString() + "年度" + g_ProjectName + "工期晴雨表";


                for (int month = 1; month <= 12; month++)
                {

                    float daysInMonth = 0;
                    float holidaysInMonth = 0;
                    float weatherNonWorkingDaysInMonth = 0;
                    float conditionNonWorkingDaysInMonth = 0;


                    #region
                    int dayNumbers = 0;
                    switch (month)
                    {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 8:
                        case 10:
                        case 12:
                            dayNumbers = 31;
                            break;
                        case 2:
                            if (year == 2016 || year == 2020 || year == 2024 || year == 2028 || year == 2032 || year == 2036)
                                dayNumbers = 29;
                            else
                                dayNumbers = 28;
                            break;
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            dayNumbers = 30;
                            break;

                    }
                    #endregion


                    int weekCount = 0;
                    for (int day = 1; day <= dayNumbers; day++)
                    {
                        DateTime date = new DateTime(year, month, day);
                        #region 計算dateIndex
                        int dateIndex = 1;
                        switch (date.DayOfWeek)
                        {
                            case DayOfWeek.Sunday:
                                dateIndex = 1;
                                if (day == 1)
                                    weekCount = 0;
                                else
                                    weekCount++;
                                break;
                            case DayOfWeek.Monday:
                                dateIndex = 2;
                                break;
                            case DayOfWeek.Tuesday:
                                dateIndex = 3;
                                break;
                            case DayOfWeek.Wednesday:
                                dateIndex = 4;
                                break;
                            case DayOfWeek.Thursday:
                                dateIndex = 5;
                                break;
                            case DayOfWeek.Friday:
                                dateIndex = 6;
                                break;
                            case DayOfWeek.Saturday:
                                dateIndex = 7;
                                break;
                        }
                        dateIndex += weekCount * 7;
                        string holidayReason = SQL.Read_SQL_data("reason", "holiday", "date = '" + Functions.TransferDateTimeToSQL(date) + "'");
                        if (holidayReason != string.Empty)
                        {
                            xlWorkSheet.Cells[8 + month * 2, 1 + dateIndex] = holidayReason;
                        }
                        else
                        {
                            xlWorkSheet.Cells[8 + month * 2, 1 + dateIndex] = day;
                        }
                        #endregion

                        string morningWeather = SQL.Read_SQL_data("morning_weather", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
                        string afternoonWeather = SQL.Read_SQL_data("afternoon_weather", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
                        string morningCondition = SQL.Read_SQL_data("morning_condition", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
                        string afternoonCondition = SQL.Read_SQL_data("afternoon_condition", "dailyreport", "project_no = '" + g_ProjectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");


                        if (date.CompareTo(dateStart) >= 0 && date.CompareTo(dateEnd) <= 0)//開工日期之後才需要貼晴雨圖
                        {             
                            daysInMonth += 1;
                            #region 例假日
                            if (g_ComputeType == "1")//工期計算方式為限期完工
                            {
                            }
                            else if (g_ComputeType == "2")//工期計算方式為日曆天
                            {
                            }
                            else if (g_ComputeType == "3")//工期計算方式為工作天，無週休二日
                            {
                                #region
                                if (g_ComputeHoliday == "0")//國定假日照常施工
                                {
                                    computeNonWorkingDay(true, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                }
                                else if (g_ComputeHoliday == "1")//國定假日不施工
                                {
                                    #region
                                    string working = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(date) + "'");
                                    if (working != string.Empty && working == "1")//遇到國定假日
                                    {
                                        computeNonWorkingDay(false, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                        holidaysInMonth += 1;
                                        PrintHoliday(month, dateIndex);
                                    }
                                    else
                                    {
                                        computeNonWorkingDay(true, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else if (g_ComputeType == "4")//工期計算方式為工作天，週休一日
                            {
                                #region
                                if (date.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    computeNonWorkingDay(false, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                    holidaysInMonth += 1;
                                    PrintHoliday(month, dateIndex);
                                }
                                else
                                {
                                    if (g_ComputeHoliday == "0")//國定假日照常施工
                                    {
                                        computeNonWorkingDay(true, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                    }
                                    else if (g_ComputeHoliday == "1")//國定假日不施工
                                    {
                                        string working = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(date) + "'");
                                        if (working != string.Empty && working == "1")
                                        {
                                            computeNonWorkingDay(false, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                            holidaysInMonth += 1;
                                            PrintHoliday(month, dateIndex);
                                        }
                                        else
                                        {
                                            computeNonWorkingDay(true, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (g_ComputeType == "5")//工期計算方式為工作天，週休二日
                            {
                                #region
                                if (date.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    computeNonWorkingDay(false, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                    holidaysInMonth += 1;
                                    PrintHoliday(month, dateIndex);
                                }
                                else if (date.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    string working = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(date) + "'");
                                    if (working == string.Empty || working == "1")
                                    {
                                        computeNonWorkingDay(false, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                        holidaysInMonth += 1;
                                        PrintHoliday(month, dateIndex);
                                    }
                                    else//這應該是要補班的狀況
                                    {
                                        computeNonWorkingDay(true, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                    }
                                }
                                else
                                {
                                    if (g_ComputeHoliday == "0")//國定假日照常施工
                                    {
                                        computeNonWorkingDay(true, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                    }
                                    else if (g_ComputeHoliday == "1")//國定假日不施工
                                    {
                                        string working = SQL.Read_SQL_data("working", "holiday", "date = '" + Functions.TransferDateTimeToSQL(date) + "'");
                                        if (working != string.Empty && working == "1")//遇到國定假日
                                        {
                                            computeNonWorkingDay(false, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                            holidaysInMonth += 1;
                                            PrintHoliday(month, dateIndex);
                                        }
                                        else
                                        {
                                            computeNonWorkingDay(true, month, dateIndex, morningWeather, afternoonWeather, morningCondition, afternoonCondition, ref weatherNonWorkingDaysInMonth, ref conditionNonWorkingDaysInMonth);
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion         
                        }
                    }


                    xlWorkSheet.Cells[7 + month * 2, 39] = daysInMonth;
                    xlWorkSheet.Cells[7 + month * 2, 40] = holidaysInMonth;
                    xlWorkSheet.Cells[7 + month * 2, 41] = weatherNonWorkingDaysInMonth;
                    xlWorkSheet.Cells[7 + month * 2, 42] = conditionNonWorkingDaysInMonth;
                }
            }
        }

        private void computeNonWorkingDay(bool countNonWorking, int month, int dateIndex, string morningWeather, string afternoonWeather, string morningCondition, string afternoonCondition, ref float weatherNonWorkingDaysInMonth, ref float conditionNonWorkingDaysInMonth)
        {
            float nonWorkingCount = 0;
            if (countNonWorking == true)
                nonWorkingCount = 0.5f;
            else
                nonWorkingCount = 0;

            #region 天候狀況
            if (morningWeather == string.Empty)//無日報表資料
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\上午無資料無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))), 
                                              g_startPositionV + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
            }
            else if (morningWeather == "晴")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\上午晴無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))), 
                                              g_startPositionV + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
            }
            else if (morningWeather == "雨")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\上午雨無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))), 
                                              g_startPositionV + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
                weatherNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (morningWeather == "豪雨")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\上午豪雨無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))), 
                                              g_startPositionV + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))),
                                              21, 12);
                weatherNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (morningWeather == "颱風")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\上午颱風.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
                weatherNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (morningWeather == "酷熱")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\上午酷熱無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
                weatherNonWorkingDaysInMonth += nonWorkingCount;
            }


            if (afternoonWeather == string.Empty)//無日報表資料
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\下午無資料無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + 10 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
                Thread.Sleep(10);
            }
            else if (afternoonWeather == "晴")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\下午晴無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + 10 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
                Thread.Sleep(10);
            }
            else if (afternoonWeather == "雨")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\下午雨無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + 10 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
                weatherNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (afternoonWeather == "豪雨")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\下午豪雨無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + 10 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
                weatherNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (afternoonWeather == "颱風")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\下午颱風.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + 10 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
                weatherNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (afternoonWeather == "酷熱")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\下午酷熱無框.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue,
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + 10 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 12);
                weatherNonWorkingDaysInMonth += nonWorkingCount;
            }
            #endregion

            if (morningCondition == string.Empty)
            { }
            else if (morningCondition == "無")
            { }
            else if (morningCondition == "停電")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\全日停電.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + 1 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 21);
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (morningCondition == "停工")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\全日停工.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV + 1 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              21, 21);
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (morningCondition == "補假")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\補假.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH - 7 + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV - 7 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              35, 35);
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (morningCondition == "選舉")
            {
                xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\選舉.png", 
                                              MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                              g_StartPositionH - 7 + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                              g_startPositionV - 7 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                              35, 35);
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (morningCondition == "雨後泥濘")
            {
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }

            if (afternoonCondition == string.Empty)
            { }
            else if (afternoonCondition == "無")
            { }
            else if (afternoonCondition == "停電")
            {
                if (morningCondition != "停電")
                {
                    xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\下午停電.png", 
                                                 MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                                  g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                                  g_startPositionV + 1 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                                  21, 21);
                }
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (afternoonCondition == "停工")
            {
                if (morningCondition != "停工")
                {
                    xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\下午停工.png", 
                                                  MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                                  g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                                  g_startPositionV + 1 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                                  21, 21);
                }
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (afternoonCondition == "補假")
            {
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (afternoonCondition == "選舉")
            {
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }
            else if (afternoonCondition == "雨後泥濘")
            {
                conditionNonWorkingDaysInMonth += nonWorkingCount;
            }
        }

        private void PrintHoliday(int month, int dateIndex)
        {
            xlWorkSheet.Shapes.AddPicture(g_StrPath + "\\image\\例假日.png", 
                                          MsoTriState.msoFalse, MsoTriState.msoTrue, 
                                          g_StartPositionH + Convert.ToInt32(Math.Round(g_CellWidth * (dateIndex - 1))),
                                          g_startPositionV - 1 + Convert.ToInt32(Math.Round(g_CellHeight * (month - 1))), 
                                          21, 21);
        }

        private void PrintNonWorking(int month, int dateIndex)
        {
 
        }
    }
}
