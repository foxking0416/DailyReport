using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class DailyReportEditForm : DailyReportIncreaseForm
    {
        bool g_DisableHandler = false;


        public DailyReportEditForm(string projectNo, MySQL Sql)
            : base(projectNo, Sql)
        {
            InitializeComponent();
            LoadProjectInfo(projectNo);
        }

        public override void LoadProjectInfo(string projectNo)
        {
            DayCompute dayCompute = new DayCompute(m_Sql);
            m_Sql.OpenSqlChannel();
            Cursor.Current = Cursors.WaitCursor;
            //專案編號
            this.textBoxProjectNo.Text = projectNo;
            g_ProjectNumber = projectNo;
            //專案名稱   
            this.textBoxProjectName.Text = m_Sql.ReadSqlDataWithoutOpenClose("project_name", "project_info", "project_no ='" + projectNo + "'");
            //開工日期
            g_StartDate = m_Sql.ReadSqlDataWithoutOpenClose("startdate", "project_info", "project_no ='" + projectNo + "'");
            this.dateStart.Value = Functions.TransferSQLDateToDateTime(g_StartDate);
            //契約工期
            this.textBoxContractDuration.Text = m_Sql.ReadSqlDataWithoutOpenClose("contractduration", "project_info", "project_no ='" + projectNo + "'");
            //契約天數
            this.textBoxContractDays.Text = m_Sql.ReadSqlDataWithoutOpenClose("contractdays", "project_info", "project_no ='" + projectNo + "'");


            //今日日期
            string[] reportDates = m_Sql.Read1DArray_SQL_Data("date", "dailyreport", "project_no ='" + projectNo + "' ORDER BY date DESC");
            //if (reportDates.Length == 0)//表示這個工程目前並沒有輸入任何日報表
            //{
            //    MessageBox.Show("此工程目前並沒有任何已存在的日報表\r\n請重新選擇工程", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    //DisableAll();

            //    return false;
                
            //}
            //else
            {
                this.dateToday.Enabled = true;
                //清除掉datagridview的資料
                ClearDataTable();
                //今日日期
                string report = m_Sql.ReadSqlDataWithoutOpenClose("nonecounting", "dailyreport", "project_no ='" + g_ProjectNumber + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                if (report == string.Empty)//表示這個日期目前並沒有輸入任何日報表
                {
                    this.dateToday.Value = Functions.TransferSQLDateToDateTime(reportDates[0]);
                    EnableAll();
                }
                else
                {
                    this.label25.Visible = false;
                    EnableAll();

                    //利用工程編號以及今日日期來load工程日報表資料
                    LoadReportInfo(this.textBoxProjectNo.Text, this.dateToday.Value);
                }
            }
            ComputeDayOfWeek();


            //工期計算方式
            g_ComputeType = m_Sql.ReadSqlDataWithoutOpenClose("computetype", "project_info", "project_no = '" + projectNo + "'");
            g_CountHoliday = m_Sql.ReadSqlDataWithoutOpenClose("holiday", "project_info", "project_no = '" + projectNo + "'");

            if (g_ComputeType == "1")//限期完工  日曆天
            {
                //追加工期後總計天數
                this.textBoxTotalDays.Text = this.textBoxTotalDuration.Text;
                this.label29.Text = "工期計算方式為限期完工";
            }
            else if (g_ComputeType == "2")
            {
                //追加工期後總計天數
                this.textBoxTotalDays.Text = this.textBoxTotalDuration.Text;
                this.label29.Text = "工期計算方式為日曆工";
            }
            else
            {
                #region
                if (g_ComputeType == "3")//工作天 無休
                {
                    dayCompute.restOnSaturday = false;
                    dayCompute.restOnSunday = false;
                    this.label29.Text = "工期計算方式為工作工，無週休二日";
                }
                else if (g_ComputeType == "4")//工作天 周休一日
                {
                    dayCompute.restOnSaturday = false;
                    dayCompute.restOnSunday = true;
                    this.label29.Text = "工期計算方式為工作工，週休一日";
                }
                else if (g_ComputeType == "5")//工作天 周休二日
                {
                    dayCompute.restOnSaturday = true;
                    dayCompute.restOnSunday = true;
                    this.label29.Text = "工期計算方式為工作工，週休二日";
                }

                if (g_CountHoliday == "1")
                {
                    dayCompute.restOnHoliday = true;
                    this.label29.Text += "，國定假日不施工";
                }
                else if (g_CountHoliday == "0")
                {
                    dayCompute.restOnHoliday = false;
                    this.label29.Text += "，國定假日照常施工";
                }
                #endregion

                DateTime FinishDate = dayCompute.CountByDuration(Functions.TransferSQLDateToDateTime(g_StartDate), Convert.ToSingle(this.textBoxTotalDuration.Text));
                //追加工期後總計天數
                this.textBoxTotalDays.Text = Convert.ToString(FinishDate.Subtract(dateStart.Value).Days + 1);
            }

            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void LoadReportInfo(string projectNo, DateTime date)
        {
            Cursor.Current = Cursors.WaitCursor;
            DayCompute dayCompute = new DayCompute(m_Sql);
            m_Sql.OpenSqlChannel();
            g_DisableHandler = true;
            //上午天氣
            comboBoxWeatherMorning.Text = m_Sql.ReadSqlDataWithoutOpenClose("morning_weather", "dailyreport", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            //下午天氣
            comboBoxWeatherAfternoon.Text = m_Sql.ReadSqlDataWithoutOpenClose("afternoon_weather", "dailyreport", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            //干擾因素
            textBoxInterference.Text = m_Sql.ReadSqlDataWithoutOpenClose("interference", "dailyreport", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            //上午條件
            comboBoxConditionMorning.Text = m_Sql.ReadSqlDataWithoutOpenClose("morning_condition", "dailyreport", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            //下午條件
            comboBoxConditionAfternoon.Text = m_Sql.ReadSqlDataWithoutOpenClose("afternoon_condition", "dailyreport", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            
            //本日不計
            comboBoxNoCount.Text = m_Sql.ReadSqlDataWithoutOpenClose("nonecounting", "dailyreport", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            //今日開始追加工期
            int accuextendduration = 0;
            ArrayList extendDate = new ArrayList();
            string[] extendDates = m_Sql.Read1DArray_SQL_Data("extendstartdate", "extendduration", "project_no ='" + projectNo + "'");
            this.textBoxExtendToday.Text = "0";
            for (int i = 0; i < extendDates.Length; i++)
            {
                DateTime extDate = Functions.TransferSQLDateToDateTime(extendDates[i]);
                if (extDate.Date.CompareTo(dateToday.Value.Date) == 0)//為追加起始日
                {
                    this.textBoxExtendToday.Text = m_Sql.ReadSqlDataWithoutOpenClose("extendduration", "extendduration", "project_no ='" + projectNo + "' AND extendstartdate = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                }

                if ((extDate.Date.CompareTo(dateToday.Value.Date) == 0 || extDate.Date.CompareTo(dateToday.Value.Date) == -1) && extDate.Date.CompareTo(Functions.TransferSQLDateToDateTime(g_StartDate)) != -1)//0為追加起始日   -1為開始日比今日日期早
                {
                    int extendDuration = Convert.ToInt32(m_Sql.ReadSqlDataWithoutOpenClose("extendduration", "extendduration", "project_no = '" + projectNo + "' AND extendstartdate = '" + Functions.TransferDateTimeToSQL(extDate) + "'"));
                    accuextendduration += extendDuration;
                }
            }
            //累計追加工期
            this.textBoxAccumulateExtend.Text = accuextendduration.ToString();

            //工期總計
            this.textBoxTotalDuration.Text = Convert.ToString(Convert.ToSingle(m_Sql.ReadSqlDataWithoutOpenClose("contractduration", "project_info", "project_no ='" + projectNo + "'")) + accuextendduration);
            //開工迄今天數 = 今日日期 - 開工日期 + 1
            if (this.dateToday.Value.Date.Subtract(this.dateStart.Value.Date).Days + 1 < 0)
                this.dateToday.Value = this.dateStart.Value;
            else
                this.textBoxDaysStartToCurrent.Text = Convert.ToString(this.dateToday.Value.Date.Subtract(this.dateStart.Value.Date).Days + 1);

            //不計工期
            string[] reportDates = m_Sql.Read1DArray_SQL_Data("date", "dailyreport", "project_no = '" + projectNo + "'");
            for (int i = 0; i < reportDates.Length; i++)
            {
                if (Functions.TransferSQLDateToDateTime(reportDates[i]).CompareTo(dateToday.Value) == -1)//發生早於今日日期
                {
                    float nonCountingDays = Convert.ToSingle(m_Sql.ReadSqlDataWithoutOpenClose("nonecounting", "dailyreport", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferSQLDateToDateOnly(reportDates[i]) + "'"));
                    if (nonCountingDays == 0.5)
                        dayCompute.AddNotWorking(Functions.TransferSQLDateToDateTime(reportDates[i]), 0);
                    else if (nonCountingDays == 1)
                    {
                        dayCompute.AddNotWorking(Functions.TransferSQLDateToDateTime(reportDates[i]), 0);
                        dayCompute.AddNotWorking(Functions.TransferSQLDateToDateTime(reportDates[i]), 1);
                    }
                }
            }

            this.textBoxDurationNotCount.Text = Convert.ToString(dayCompute.CountTotalNotWorkingDay(dateStart.Value, dateToday.Value));
            //實際工期 = 開工迄今天數 - 不計工期
            this.textBoxRealDuration.Text = Convert.ToString(Convert.ToDecimal(this.textBoxDaysStartToCurrent.Text) - Convert.ToDecimal(this.textBoxDurationNotCount.Text));
            //剩餘工期 = 工期總計 - 實際工期 
            this.textBoxRestDuration.Text = Convert.ToString(Convert.ToDecimal(this.textBoxTotalDuration.Text) - Convert.ToDecimal(this.textBoxRealDuration.Text));

            if (Convert.ToSingle(this.textBoxRestDuration.Text) < 0)
            {

                //變動完工日從SQL讀出來
                this.dateProjectEnd_Modify.Value = Functions.TransferSQLDateToDateTime(m_Sql.ReadSqlDataWithoutOpenClose("modified_finishdate", "project_info", "project_no = '" + g_ProjectNumber + "'"));
                //剩餘天數 = 變動完工日 - 今日日期
                this.textBoxRestDays.Text = Convert.ToString(this.dateProjectEnd_Modify.Value.Subtract(dateToday.Value).Days);
            }
            else
            {
                //變動完工日
                this.dateProjectEnd_Modify.Value = dayCompute.CountByDuration(dateToday.Value.AddDays(1), Convert.ToSingle(this.textBoxRestDuration.Text));
                //把變動完工日寫進SQL
                m_Sql.Set_SQL_data("modified_finishdate", "project_info", "project_no = '" + g_ProjectNumber + "'", Functions.TransferDateTimeToSQL(this.dateProjectEnd_Modify.Value));


                //剩餘天數 = 變動完工日 - 今日日期
                this.textBoxRestDays.Text = Convert.ToString(this.dateProjectEnd_Modify.Value.Subtract(dateToday.Value).Days);
            }


            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;

            LoadDataTable(projectNo, date);
            g_DisableHandler = false;
        }

        private void LoadDataTable(string projectNo, DateTime date)
        {
            Cursor.Current = Cursors.WaitCursor;
            m_Sql.OpenSqlChannel();
            //Load 材料使用
            string[] indexMaterial = m_Sql.Read1DArray_SQL_Data("data_index", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            for (int i = 0; i < indexMaterial.Length; i++)
            {
                DataRow dataRow;
                dataRow = dataTableMaterial.NewRow();
                dataRow["廠商編號"] = m_Sql.ReadSqlDataWithoutOpenClose("vendor_no", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["廠商名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("vendor_name", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["料號"] = m_Sql.ReadSqlDataWithoutOpenClose("material_no", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("material_name", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["單位"] = m_Sql.ReadSqlDataWithoutOpenClose("unit", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["位置"] = m_Sql.ReadSqlDataWithoutOpenClose("location", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["已進數量"] = m_Sql.ReadSqlDataWithoutOpenClose("amount_past", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["本日進量"] = m_Sql.ReadSqlDataWithoutOpenClose("amount_today", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["累計進數"] = m_Sql.ReadSqlDataWithoutOpenClose("amount_all", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["已使用"] = m_Sql.ReadSqlDataWithoutOpenClose("used_past", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["本日用量"] = m_Sql.ReadSqlDataWithoutOpenClose("used_today", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["累計用量"] = m_Sql.ReadSqlDataWithoutOpenClose("used_all", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataRow["庫存"] = m_Sql.ReadSqlDataWithoutOpenClose("storage", "dailyreport_material", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexMaterial[i] + "'");
                dataTableMaterial.Rows.Add(dataRow);           
            }


            //Load 出工人數
            string[] indexManpower = m_Sql.Read1DArray_SQL_Data("data_index", "dailyreport_manpower", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            for (int i = 0; i < indexManpower.Length; i++)
            {
                DataRow dataRow;
                dataRow = dataTableManPower.NewRow();
                dataRow["廠商編號"] = m_Sql.ReadSqlDataWithoutOpenClose("vendor_no", "dailyreport_manpower", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexManpower[i] + "'");
                dataRow["廠商名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("vendor_name", "dailyreport_manpower", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexManpower[i] + "'");
                dataRow["工別編號"] = m_Sql.ReadSqlDataWithoutOpenClose("manpower_no", "dailyreport_manpower", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexManpower[i] + "'");
                dataRow["工別名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("manpower_name", "dailyreport_manpower", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexManpower[i] + "'");
                dataRow["出工人數"] = m_Sql.ReadSqlDataWithoutOpenClose("amount", "dailyreport_manpower", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexManpower[i] + "'");
                dataRow["工時"] = m_Sql.ReadSqlDataWithoutOpenClose("hour", "dailyreport_manpower", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexManpower[i] + "'");
                dataRow["本日工數"] = m_Sql.ReadSqlDataWithoutOpenClose("amount_today", "dailyreport_manpower", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexManpower[i] + "'");
                dataRow["備註"] = m_Sql.ReadSqlDataWithoutOpenClose("ps", "dailyreport_manpower", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexManpower[i] + "'");
                dataTableManPower.Rows.Add(dataRow);   
            }

            //Load 機具使用
            string[] indexTool = m_Sql.Read1DArray_SQL_Data("data_index", "dailyreport_tool", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            for (int i = 0; i < indexTool.Length; i++)
            {
                DataRow dataRow;
                dataRow = dataTableTool.NewRow();
                dataRow["廠商編號"] = m_Sql.ReadSqlDataWithoutOpenClose("vendor_no", "dailyreport_tool", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexTool[i] + "'");
                dataRow["廠商名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("vendor_name", "dailyreport_tool", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexTool[i] + "'");
                dataRow["機具編號"] = m_Sql.ReadSqlDataWithoutOpenClose("tool_no", "dailyreport_tool", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexTool[i] + "'");
                dataRow["機具名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("tool_name", "dailyreport_tool", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexTool[i] + "'");
                dataRow["出工數"] = m_Sql.ReadSqlDataWithoutOpenClose("amount", "dailyreport_tool", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexTool[i] + "'");
                dataRow["工時"] = m_Sql.ReadSqlDataWithoutOpenClose("hour", "dailyreport_tool", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexTool[i] + "'");
                dataRow["本日工數"] = m_Sql.ReadSqlDataWithoutOpenClose("amount_today", "dailyreport_tool", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexTool[i] + "'");
                dataRow["備註"] = m_Sql.ReadSqlDataWithoutOpenClose("ps", "dailyreport_tool", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexTool[i] + "'");
                dataTableTool.Rows.Add(dataRow);  
            }
            //Load 外包項目
            string[] indexOutsourcing = m_Sql.Read1DArray_SQL_Data("data_index", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            for (int i = 0; i < indexOutsourcing.Length; i++)
            {
                DataRow dataRow;
                dataRow = dataTableOutsourcing.NewRow();
                dataRow["廠商編號"] = m_Sql.ReadSqlDataWithoutOpenClose("vendor_no", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["廠商名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("vendor_name", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["施工編號"] = m_Sql.ReadSqlDataWithoutOpenClose("process_no", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["施工名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("process_name", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["單位"] = m_Sql.ReadSqlDataWithoutOpenClose("unit", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["已出工"] = m_Sql.ReadSqlDataWithoutOpenClose("dispatch_past", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["出工"] = m_Sql.ReadSqlDataWithoutOpenClose("dispatch_today", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["累計出工"] = m_Sql.ReadSqlDataWithoutOpenClose("dispatch_all", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["已施作"] = m_Sql.ReadSqlDataWithoutOpenClose("build_past", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["施作"] = m_Sql.ReadSqlDataWithoutOpenClose("build_today", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["累計施作"] = m_Sql.ReadSqlDataWithoutOpenClose("build_all", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataRow["備註"] = m_Sql.ReadSqlDataWithoutOpenClose("ps", "dailyreport_outsourcing", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexOutsourcing[i] + "'");
                dataTableOutsourcing.Rows.Add(dataRow); 
            }
            //Load 休假紀錄
            string[] indexVacation = m_Sql.Read1DArray_SQL_Data("data_index", "dailyreport_vacation", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "'");
            for (int i = 0; i < indexVacation.Length; i++)
            {
                DataRow dataRow;
                dataRow = dataTableVacation.NewRow();
                dataRow["員工編號"] = m_Sql.ReadSqlDataWithoutOpenClose("employee_no", "dailyreport_vacation", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexVacation[i] + "'");
                dataRow["姓名"] = m_Sql.ReadSqlDataWithoutOpenClose("employee_name", "dailyreport_vacation", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexVacation[i] + "'");
                dataRow["休假天數"] = m_Sql.ReadSqlDataWithoutOpenClose("days", "dailyreport_vacation", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexVacation[i] + "'");
                dataRow["備註"] = m_Sql.ReadSqlDataWithoutOpenClose("ps", "dailyreport_vacation", "project_no = '" + projectNo + "' AND date = '" + Functions.TransferDateTimeToSQL(date) + "' AND data_index = '" + indexVacation[i] + "'");

                dataTableVacation.Rows.Add(dataRow); 
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void ClearDataTable()
        {
            dataTableMaterial.Clear();
            dataTableManPower.Clear();
            dataTableOutsourcing.Clear();
            dataTableTool.Clear();
            dataTableVacation.Clear();
        }

        public override void SetDateTodayValue(DateTime date)
        {
            dateToday.Value = date;
        }

        //選擇今日日期
        protected override void dateToday_ValueChanged(object sender, EventArgs e)
        {
            ComputeDayOfWeek();
            //清除掉datagridview的資料
            ClearDataTable();
            //今日日期
            string report = m_Sql.Read_SQL_data("nonecounting", "dailyreport", "project_no ='" + g_ProjectNumber + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
            if (report == string.Empty)//表示這個日期目前並沒有輸入任何日報表
            {
                this.label25.Text = "此日期尚未輸入日報表，無法編輯";
                this.label25.Visible = true;
                DisableAllExceptDateToday();
                return;
            }
            else
            {
                this.label25.Visible = false;
                this.dateToday.Enabled = true;
                EnableAll();

                //利用工程編號以及今日日期來load工程日報表資料
                LoadReportInfo(this.textBoxProjectNo.Text, this.dateToday.Value);
            }
        }

        protected override void comboBoxNoCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (g_DisableHandler)
                return;

            if (g_ProjectNumber != null && g_ComputeType != null && g_CountHoliday != null && g_StartDate != null)
                Compute(g_ProjectNumber, g_ComputeType, g_CountHoliday, g_StartDate);
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {

            //覆寫原有資料
            DialogResult result = MessageBox.Show("確定要修改工程資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                m_Sql.OpenSqlChannel();


                label25.Visible = false;
                label26.Visible = false;

                m_Sql.SetSqlDataWithoutOpenClose("morning_weather", "dailyreport", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'", comboBoxWeatherMorning.Text);
                m_Sql.SetSqlDataWithoutOpenClose("afternoon_weather", "dailyreport", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'", comboBoxWeatherAfternoon.Text);
                m_Sql.SetSqlDataWithoutOpenClose("interference", "dailyreport", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'", textBoxInterference.Text);

                m_Sql.SetSqlDataWithoutOpenClose("morning_condition", "dailyreport", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'", comboBoxConditionMorning.Text);
                m_Sql.SetSqlDataWithoutOpenClose("afternoon_condition", "dailyreport", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'", comboBoxConditionAfternoon.Text);
                m_Sql.SetSqlDataWithoutOpenClose("nonecounting", "dailyreport", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'", comboBoxNoCount.Text);

                

                //刪除出工人數資料
                m_Sql.NoHistoryDelete_SQL("dailyreport_manpower", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                //刪除材料使用數量資料
                m_Sql.NoHistoryDelete_SQL("dailyreport_material", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                //刪除外包項目資料
                m_Sql.NoHistoryDelete_SQL("dailyreport_outsourcing", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                //刪除機具使用資料
                m_Sql.NoHistoryDelete_SQL("dailyreport_tool", "project_no = '" + this.textBoxProjectNo.Text + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");

                //儲存材料使用數量資料進SQL
                #region
                for (int i = 0; i < dataGridViewMaterial.RowCount; i++)
                {
                    string commandStr = "Insert into dailyreport_material(";
                    commandStr = commandStr + "project_no,";
                    commandStr = commandStr + "date,";
                    commandStr = commandStr + "data_index,";
                    commandStr = commandStr + "vendor_no,";
                    commandStr = commandStr + "vendor_name,";
                    commandStr = commandStr + "material_no,";
                    commandStr = commandStr + "material_name,";
                    commandStr = commandStr + "unit,";
                    commandStr = commandStr + "location,";
                    commandStr = commandStr + "amount_past,";
                    commandStr = commandStr + "amount_today,";
                    commandStr = commandStr + "amount_all,";
                    commandStr = commandStr + "used_past,";
                    commandStr = commandStr + "used_today,";
                    commandStr = commandStr + "used_all,";
                    commandStr = commandStr + "storage";
                    commandStr = commandStr + ") values('";
                    commandStr = commandStr + textBoxProjectNo.Text + "','";
                    commandStr = commandStr + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
                    commandStr = commandStr + i + "','";
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[0].Value + "','";//廠商編號
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[1].Value + "','";//廠商名稱
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[2].Value + "','";//材料編號
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[3].Value + "','";//材料名稱
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[4].Value + "','";//單位
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[5].Value + "','";//地點
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[6].Value + "','";//已進數量
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[7].Value + "','";//本日進量
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[8].Value + "','";//累計進數
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[9].Value + "','";//已使用
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[10].Value + "','";//本日用量
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[11].Value + "','";//累計用量
                    commandStr = commandStr + dataGridViewMaterial.Rows[i].Cells[12].Value + "')";//庫存

                    m_Sql.ExecuteNonQueryCommand(commandStr);

                }
                #endregion

                //儲存出工人數資料進SQL
                #region
                for (int i = 0; i < dataGridViewManPower.RowCount; i++)
                {
                    string commandStr = "Insert into dailyreport_manpower(";
                    commandStr = commandStr + "project_no,";
                    commandStr = commandStr + "date,";
                    commandStr = commandStr + "data_index,";
                    commandStr = commandStr + "vendor_no,";
                    commandStr = commandStr + "vendor_name,";
                    commandStr = commandStr + "manpower_no,";
                    commandStr = commandStr + "manpower_name,";
                    commandStr = commandStr + "amount,";
                    commandStr = commandStr + "hour,";
                    commandStr = commandStr + "amount_today,";
                    commandStr = commandStr + "ps";
                    commandStr = commandStr + ") values('";
                    commandStr = commandStr + textBoxProjectNo.Text + "','";
                    commandStr = commandStr + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
                    commandStr = commandStr + i + "','";
                    commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[0].Value + "','";//廠商編號
                    commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[1].Value + "','";//廠商名稱
                    commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[2].Value + "','";//工別編號
                    commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[3].Value + "','";//工別名稱
                    commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[4].Value + "','";//出工人數
                    commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[5].Value + "','";//工時
                    commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[6].Value + "','";//本日工數
                    commandStr = commandStr + dataGridViewManPower.Rows[i].Cells[7].Value + "')";//備註

                    m_Sql.ExecuteNonQueryCommand(commandStr);
                }
                #endregion

                //儲存機具使用資料進SQL
                #region
                for (int i = 0; i < dataGridViewTool.RowCount; i++)
                {
                    string commandStr = "Insert into dailyreport_tool(";
                    commandStr = commandStr + "project_no,";
                    commandStr = commandStr + "date,";
                    commandStr = commandStr + "data_index,";
                    commandStr = commandStr + "vendor_no,";
                    commandStr = commandStr + "vendor_name,";
                    commandStr = commandStr + "tool_no,";
                    commandStr = commandStr + "tool_name,";
                    commandStr = commandStr + "amount,";
                    commandStr = commandStr + "hour,";
                    commandStr = commandStr + "amount_today,";
                    commandStr = commandStr + "ps";
                    commandStr = commandStr + ") values('";
                    commandStr = commandStr + textBoxProjectNo.Text + "','";
                    commandStr = commandStr + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
                    commandStr = commandStr + i + "','";
                    commandStr = commandStr + dataGridViewTool.Rows[i].Cells[0].Value + "','";//廠商編號
                    commandStr = commandStr + dataGridViewTool.Rows[i].Cells[1].Value + "','";//廠商名稱
                    commandStr = commandStr + dataGridViewTool.Rows[i].Cells[2].Value + "','";//機具編號
                    commandStr = commandStr + dataGridViewTool.Rows[i].Cells[3].Value + "','";//機具名稱
                    commandStr = commandStr + dataGridViewTool.Rows[i].Cells[4].Value + "','";//出工數
                    commandStr = commandStr + dataGridViewTool.Rows[i].Cells[5].Value + "','";//工時
                    commandStr = commandStr + dataGridViewTool.Rows[i].Cells[6].Value + "','";//本日工數
                    commandStr = commandStr + dataGridViewTool.Rows[i].Cells[7].Value + "')";//備註
                }
                #endregion

                //儲存外包項目資料進SQL
                #region
                for (int i = 0; i < dataGridViewOutsourcing.RowCount; i++)
                {
                    string commandStr = "Insert into dailyreport_outsourcing(";
                    commandStr = commandStr + "project_no,";
                    commandStr = commandStr + "date,";
                    commandStr = commandStr + "data_index,";
                    commandStr = commandStr + "vendor_no,";
                    commandStr = commandStr + "vendor_name,";
                    commandStr = commandStr + "process_no,";
                    commandStr = commandStr + "process_name,";
                    commandStr = commandStr + "unit,";
                    commandStr = commandStr + "dispatch_past,";
                    commandStr = commandStr + "dispatch_today,";
                    commandStr = commandStr + "dispatch_all,";
                    commandStr = commandStr + "build_past,";
                    commandStr = commandStr + "build_today,";
                    commandStr = commandStr + "build_all,";
                    commandStr = commandStr + "ps";
                    commandStr = commandStr + ") values('";
                    commandStr = commandStr + textBoxProjectNo.Text + "','";
                    commandStr = commandStr + Functions.TransferDateTimeToSQL(dateToday.Value) + "','";
                    commandStr = commandStr + i + "','";
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[0].Value + "','";//廠商編號
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[1].Value + "','";//廠商名稱
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[2].Value + "','";//施工編號
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[3].Value + "','";//施工名稱
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[4].Value + "','";//單位
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[5].Value + "','";//已出工
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[6].Value + "','";//出工
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[7].Value + "','";//累計出工
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[8].Value + "','";//已施作
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[9].Value + "','";//施作
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[10].Value + "','";//累計施作
                    commandStr = commandStr + dataGridViewOutsourcing.Rows[i].Cells[11].Value + "')";//備註
                }
                #endregion

                m_Sql.CloseSqlChannel();
            }
        }
    }
}
