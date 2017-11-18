using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuaChun_DailyReport
{
    public partial class ConstantFactorSettingForm : Form
    {
        protected string g_ProjectNumber;
        protected MySQL m_Sql;
        protected DataTable dataTable;

        public ConstantFactorSettingForm( string strProjectNo, MySQL Sql )
        {
            m_Sql = Sql;
            InitializeComponent();
            this.labelProjectName.Text = Sql.Read_SQL_data( "project_name", "project_info", "project_no = '" + strProjectNo + "'" );
            this.g_ProjectNumber = strProjectNo;
        }

        private void btnSave_Click( object sender, EventArgs e )
        {
            m_Sql.OpenSqlChannel();
            if ( uiRadioBtnRestrictSchedule.Checked == true )
            {
                m_Sql.SetSqlDataWithoutOpenClose( "computetype", "project_info", "project_no = '" + this.g_ProjectNumber + "'", "1" );
            }
            else if ( uiRadioBtnCalenderDay.Checked == true )
            {
                m_Sql.SetSqlDataWithoutOpenClose( "computetype", "project_info", "project_no = '" + this.g_ProjectNumber + "'", "2" );
            }
            else if ( uiRadioBtnWorkingDay.Checked == true )
            {
                if ( uiRadioBtnNoWeekend.Checked == true )
                {
                    m_Sql.SetSqlDataWithoutOpenClose( "computetype", "project_info", "project_no = '" + this.g_ProjectNumber + "'", "3" );
                }
                else if ( uiRadioBtnSun.Checked == true )
                {
                    m_Sql.SetSqlDataWithoutOpenClose( "computetype", "project_info", "project_no = '" + this.g_ProjectNumber + "'", "4" );
                }
                else if ( uiRadioBtnSatSun.Checked == true )
                {
                    m_Sql.SetSqlDataWithoutOpenClose( "computetype", "project_info", "project_no = '" + this.g_ProjectNumber + "'", "5" );
                }
            }

            m_Sql.CloseSqlChannel();
        }

        private void btnCopy_Click( object sender, EventArgs e )
        {

        }

        private void btnDelete_Click( object sender, EventArgs e )
        {

        }

        private void dataGridView1_CurrentCellChanged( object sender, EventArgs e )
        {
            //try
            //{
            //    string date = dataGridView1 [ 0, dataGridView1.CurrentRow.Index ].Value.ToString();
            //    int firstIndex = date.IndexOf( "/" );
            //    int secondIndex = date.IndexOf( "/", firstIndex + 1 );

            //    string Year = date.Substring( 0, firstIndex );
            //    string Month = date.Substring( firstIndex + 1, secondIndex - firstIndex - 1 );
            //    string Day = date.Substring( secondIndex + 1 );
            //    this.dateTimeHoliday.Value = new DateTime( Convert.ToInt32( Year ), Convert.ToInt32( Month ), Convert.ToInt32( Day ) );
            //    this.textBoxReason.Text = m_Sql.Read_SQL_data( "reason", "holiday", "date = '" + Year + "-" + Month + "-" + Day + "'" );
            //    string working = m_Sql.Read_SQL_data( "working", "holiday", "date = '" + Year + "-" + Month + "-" + Day + "'" );
            //    if ( working == "1" )
            //    {
            //        uiRadioBtnCalenderDay.Checked = true;
            //        uiRadioBtnWorkingDay.Checked = false;
            //    }
            //    else
            //    {
            //        uiRadioBtnCalenderDay.Checked = false;
            //        uiRadioBtnWorkingDay.Checked = true;
            //    }

            //}
            //catch
            //{ }
        }


        private void RefreshDatagridview()
        {
            //Cursor.Current = Cursors.WaitCursor;
            //dataTable.Clear();

            //string [] dateArr = m_Sql.Read1DArrayNoCondition_SQL_Data( "date", "holiday" );
            //DateTime [] dates = new DateTime [ dateArr.Length ];
            //for ( int i = 0; i < dateArr.Length; i++ )
            //{
            //    dates [ i ] = Functions.TransferSQLDateToDateTime( dateArr [ i ] );
            //}
            //Array.Sort( dates );

            //DataRow dataRow;
            //m_Sql.OpenSqlChannel();
            //for ( int i = 0; i < dates.Length; i++ )
            //{
            //    dataRow = dataTable.NewRow();
            //    dataRow [ "日期" ] = dates [ i ].Year.ToString() + "/" + dates [ i ].Month.ToString() + "/" + dates [ i ].Day.ToString();
            //    dataRow [ "星期" ] = Functions.ComputeDayOfWeek( dates [ i ] );
            //    dataRow [ "理由" ] = m_Sql.ReadSqlDataWithoutOpenClose( "reason", "holiday", "date = '" + dates [ i ].Year.ToString() + "-" + dates [ i ].Month.ToString() + "-" + dates [ i ].Day.ToString() + "'" );
            //    string working = m_Sql.ReadSqlDataWithoutOpenClose( "working", "holiday", "date = '" + dates [ i ].Year.ToString() + "-" + dates [ i ].Month.ToString() + "-" + dates [ i ].Day.ToString() + "'" );
            //    if ( working == "1" )
            //        dataRow [ "放假/補班" ] = "放假";
            //    else
            //        dataRow [ "放假/補班" ] = "補班";
            //    dataTable.Rows.Add( dataRow );
            //}
            //m_Sql.CloseSqlChannel();
            //Cursor.Current = Cursors.Default;
        }

        private void InsertIntoDB()
        {
            //Cursor.Current = Cursors.WaitCursor;
            //m_Sql.OpenSqlChannel();

            //string commandStr = "Insert into holiday(";
            //commandStr = commandStr + "date,";
            //commandStr = commandStr + "reason,";
            //commandStr = commandStr + "working";
            //commandStr = commandStr + ") values('";
            //int holidayYear = dateTimeHoliday.Value.Year;
            //int holidayMonth = dateTimeHoliday.Value.Month;
            //int holidayDay = dateTimeHoliday.Value.Day;
            //commandStr = commandStr + holidayYear.ToString() + "-" + holidayMonth.ToString() + "-" + holidayDay.ToString() + "','";


            //commandStr = commandStr + textBoxReason.Text + "','";
            //if ( uiRadioBtnCalenderDay.Checked )
            //    commandStr = commandStr + "1";//放假
            //else
            //    commandStr = commandStr + "2";//補班
            //commandStr = commandStr + "')";


            //m_Sql.ExecuteNonQueryCommand( commandStr );
            //m_Sql.CloseSqlChannel();
            //Cursor.Current = Cursors.Default;
        }

        private void DeleteExistDB()
        {
            //int holidayYear = dateTimeHoliday.Value.Year;
            //int holidayMonth = dateTimeHoliday.Value.Month;
            //int holidayDay = dateTimeHoliday.Value.Day;
            //m_Sql.NoHistoryDelete_SQL( "holiday", "date = '" + holidayYear.ToString() + "-" + holidayMonth.ToString() + "-" + holidayDay.ToString() + "'" );
        }

        private void radioBtnWorkingCondition_CheckedChanged( object sender, EventArgs e )
        {

        }

        private void radioBtnHolidayCondition_CheckedChanged( object sender, EventArgs e )
        {

        }



    }
}
