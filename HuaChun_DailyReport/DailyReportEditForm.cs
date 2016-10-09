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
        public DailyReportEditForm(string projectNo, MySQL Sql)
            : base(projectNo, Sql)
        {
            InitializeComponent();
            ClearDataTable();
        }

        public override void SetDateTodayValue()
        {
            //搜尋最新日期的日報表
            string strLatestDailyReportDate = m_Sql.ReadSqlDataWithoutOpenClose("date", "dailyreport", "project_no = '" + g_ProjectNumber + "' ORDER BY date DESC");
            if (strLatestDailyReportDate != string.Empty)
            {
                EnableAllEvent();
                DateTime lastInputDate = Functions.TransferSQLDateToDateTime(strLatestDailyReportDate);
                this.dateToday.Value = lastInputDate;

            }
        }

        public void SetDateTodayValue(DateTime dtDate)
        {
            EnableAllEvent();
            this.dateToday.Value = dtDate;
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


        //選擇今日日期
        protected override void dateToday_ValueChanged(object sender, EventArgs e)
        {
            if (bDisableDateToday)
            {
                return;
            }
            m_Sql.OpenSqlChannel();
            //DisableAllEvent();
            ComputeDayOfWeek();
            //清除掉datagridview的資料
            ClearDataTable();
            //今日日期
            string report = m_Sql.ReadSqlDataWithoutOpenClose("nonecounting", "dailyreport", "project_no ='" + g_ProjectNumber + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
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
                LoadInfoByDate(g_ProjectNumber);
                //LoadReportInfo(this.textBoxProjectNo.Text, this.dateToday.Value);
                this.comboBoxWeatherMorning.Text = m_Sql.ReadSqlDataWithoutOpenClose("morning_weather", "dailyreport", "project_no ='" + g_ProjectNumber + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                this.comboBoxWeatherAfternoon.Text = m_Sql.ReadSqlDataWithoutOpenClose("afternoon_weather", "dailyreport", "project_no ='" + g_ProjectNumber + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                this.comboBoxConditionMorning.Text = m_Sql.ReadSqlDataWithoutOpenClose("morning_condition", "dailyreport", "project_no ='" + g_ProjectNumber + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                this.comboBoxConditionAfternoon.Text = m_Sql.ReadSqlDataWithoutOpenClose("afternoon_condition", "dailyreport", "project_no ='" + g_ProjectNumber + "' AND date = '" + Functions.TransferDateTimeToSQL(dateToday.Value) + "'");
                Compute(g_ProjectNumber, g_ComputeType, g_CountHoliday, g_StartDate);
            }
            //EnableAllEvent();
            m_Sql.CloseSqlChannel();
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
