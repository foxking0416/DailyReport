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
    public partial class HolidaySettingForm : Form
    {
        protected MySQL m_Sql;
        protected DataTable dataTable;

        public HolidaySettingForm(MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
            Initialize();
        }

        protected void Initialize()
        {
            InitializeDataTable();
        }

        protected void InitializeDataTable()
        {
            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add("日期", typeof(String));
            dataTable.Columns.Add("星期", typeof(String));
            dataTable.Columns.Add("理由", typeof(String));
            dataTable.Columns.Add("放假/補班", typeof(String));
            dataGridView1.DataSource = dataTable;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;

            RefreshDatagridview();
        }

        private void RefreshDatagridview()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTable.Clear();

            string[] dateArr = m_Sql.Read1DArrayNoCondition_SQL_Data("date", "holiday");
            DateTime[] dates = new DateTime[dateArr.Length];
            for (int i = 0; i < dateArr.Length; i++)
            {
                dates[i] = Functions.TransferSQLDateToDateTime(dateArr[i]);
            }
            Array.Sort(dates);

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < dates.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow["日期"] = dates[i].Year.ToString() + "/" + dates[i].Month.ToString() + "/" + dates[i].Day.ToString() ;
                dataRow["星期"] = Functions.ComputeDayOfWeek(dates[i]);
                dataRow["理由"] = m_Sql.ReadSqlDataWithoutOpenClose("reason", "holiday", "date = '" + dates[i].Year.ToString() + "-" + dates[i].Month.ToString() + "-" + dates[i].Day.ToString() + "'");
                string working = m_Sql.ReadSqlDataWithoutOpenClose("working", "holiday", "date = '" + dates[i].Year.ToString() + "-" + dates[i].Month.ToString() + "-" + dates[i].Day.ToString() + "'");
                if(working == "1")
                    dataRow["放假/補班"] = "放假";
                else
                    dataRow["放假/補班"] = "補班";
                dataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void InsertIntoDB()
        {
            Cursor.Current = Cursors.WaitCursor;
            m_Sql.OpenSqlChannel();

            string commandStr = "Insert into holiday(";
            commandStr = commandStr + "date,";
            commandStr = commandStr + "reason,";
            commandStr = commandStr + "working";
            commandStr = commandStr + ") values('";
            int holidayYear = dateTimeHoliday.Value.Year;
            int holidayMonth = dateTimeHoliday.Value.Month;
            int holidayDay = dateTimeHoliday.Value.Day;
            commandStr = commandStr + holidayYear.ToString() + "-" + holidayMonth.ToString() + "-" + holidayDay.ToString() + "','";


            commandStr = commandStr + textBoxReason.Text + "','";
            if(radioButton1.Checked)
                commandStr = commandStr + "1";//放假
            else
                commandStr = commandStr + "2";//補班
            commandStr = commandStr + "')";


            m_Sql.ExecuteNonQueryCommand(commandStr);
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void EditExistDB() 
        {
            m_Sql.OpenSqlChannel();
            int holidayYear = dateTimeHoliday.Value.Year;
            int holidayMonth = dateTimeHoliday.Value.Month;
            int holidayDay = dateTimeHoliday.Value.Day;
            m_Sql.SetSqlDataWithoutOpenClose("reason", "holiday", "date = '" + holidayYear.ToString() + "-" + holidayMonth.ToString() + "-" + holidayDay.ToString() + "'",  textBoxReason.Text);
            if (radioButton1.Checked)
                m_Sql.SetSqlDataWithoutOpenClose("working", "holiday", "date = '" + holidayYear.ToString() + "-" + holidayMonth.ToString() + "-" + holidayDay.ToString() + "'", "1");//放假
            else
                m_Sql.SetSqlDataWithoutOpenClose("working", "holiday", "date = '" + holidayYear.ToString() + "-" + holidayMonth.ToString() + "-" + holidayDay.ToString() + "'", "2");//補班
            m_Sql.CloseSqlChannel();
        }

        private void DeleteExistDB()
        {
            int holidayYear = dateTimeHoliday.Value.Year;
            int holidayMonth = dateTimeHoliday.Value.Month;
            int holidayDay = dateTimeHoliday.Value.Day;
            m_Sql.NoHistoryDelete_SQL("holiday", "date = '" + holidayYear.ToString() + "-" + holidayMonth.ToString() + "-" + holidayDay.ToString() + "'");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int holidayYear = dateTimeHoliday.Value.Year;
            int holidayMonth = dateTimeHoliday.Value.Month;
            int holidayDay = dateTimeHoliday.Value.Day;
            string[] duplicateDates = m_Sql.Read1DArray_SQL_Data("date", "holiday", "date = '" + holidayYear.ToString() + "-" + holidayMonth.ToString() + "-" + holidayDay.ToString() + "'");
            if (duplicateDates.Length != 0)
            {
                DialogResult result = MessageBox.Show("此日期已存在，確定要修改?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    EditExistDB();
                    RefreshDatagridview();
                    this.textBoxReason.Clear();
                    this.radioButton1.Checked = true;
                    this.radioButton2.Checked = false;
                }
            }
            else
            {
                InsertIntoDB();
                RefreshDatagridview();
                this.textBoxReason.Clear();
                this.radioButton1.Checked = true;
                this.radioButton2.Checked = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要刪除此日期?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                DeleteExistDB();
                RefreshDatagridview();
                this.textBoxReason.Clear();
                this.radioButton1.Checked = true;
                this.radioButton2.Checked = false;
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                string date = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                int firstIndex = date.IndexOf("/");
                int secondIndex = date.IndexOf("/", firstIndex + 1);

                string Year = date.Substring(0, firstIndex);
                string Month = date.Substring(firstIndex + 1, secondIndex - firstIndex - 1);
                string Day = date.Substring(secondIndex + 1);
                this.dateTimeHoliday.Value = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), Convert.ToInt32(Day));
                this.textBoxReason.Text = m_Sql.Read_SQL_data("reason", "holiday", "date = '" + Year + "-" + Month + "-" + Day + "'");
                string working = m_Sql.Read_SQL_data("working", "holiday", "date = '" + Year + "-" + Month + "-" + Day + "'");
                if (working == "1")
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
                
            }
            catch
            { }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
