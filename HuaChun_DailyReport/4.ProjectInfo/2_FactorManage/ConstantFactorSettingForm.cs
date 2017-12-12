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
        protected string m_strProjectNumber;
        protected MySQL m_Sql;
        protected DataTable m_kDataTable;

        public ConstantFactorSettingForm( string strProjectNo, MySQL Sql )
        {
            m_Sql = Sql;
            InitializeComponent();
            m_uiLabelProjectName.Text = Sql.Read_SQL_data( "project_name", "project_info", "project_no = '" + strProjectNo + "'" );
            m_strProjectNumber = strProjectNo;
            InitializeDataTable();
            RefreshDatagridview();
            UpdateUI();
        }

        private void InitializeDataTable()
        {
            m_kDataTable = new DataTable( "MyNewTable" );
            m_kDataTable.Columns.Add( "日期", typeof( String ) );
            m_kDataTable.Columns.Add( "星期", typeof( String ) );
            m_kDataTable.Columns.Add( "理由", typeof( String ) );
            m_kDataTable.Columns.Add( "放假/補班", typeof( String ) );
            m_uiDataGridView.DataSource = m_kDataTable;
            m_uiDataGridView.ReadOnly = true;
            m_uiDataGridView.AllowUserToAddRows = false;
            m_uiDataGridView.MultiSelect = false;
        }

        private void RefreshDatagridview()
        {
            Cursor.Current = Cursors.WaitCursor;
            m_kDataTable.Clear();

            string [] arrDate = m_Sql.Read1DArray_SQL_Data( "date", "project_holiday", "project_no = '" + m_strProjectNumber + "'" );
            DateTime [] dates = new DateTime [ arrDate.Length ];
            for ( int i = 0; i < arrDate.Length; i++ )
            {
                dates [ i ] = Functions.TransferSQLDateToDateTime( arrDate [ i ] );
            }
            Array.Sort( dates );

            DataRow kDataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < dates.Length; i++ )
            {
                kDataRow = m_kDataTable.NewRow();
                kDataRow [ "日期" ] = dates [ i ].Year.ToString() + "/" + dates [ i ].Month.ToString() + "/" + dates [ i ].Day.ToString();
                kDataRow [ "星期" ] = Functions.ComputeDayOfWeek( dates [ i ] );
                kDataRow [ "理由" ] = m_Sql.ReadSqlDataWithoutOpenClose( "reason", "project_holiday", "date = '" + dates [ i ].Year.ToString() + "-" + dates [ i ].Month.ToString() + "-" + dates [ i ].Day.ToString() + "'" );
                string working = m_Sql.ReadSqlDataWithoutOpenClose( "working", "project_holiday", "date = '" + dates [ i ].Year.ToString() + "-" + dates [ i ].Month.ToString() + "-" + dates [ i ].Day.ToString() + "'" );
                if ( working == "1" )
                    kDataRow [ "放假/補班" ] = "放假";
                else
                    kDataRow [ "放假/補班" ] = "補班";
                m_kDataTable.Rows.Add( kDataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void DeleteExistDB()
        {
            string strDate = m_uiDataGridView [ 0, m_uiDataGridView.CurrentRow.Index ].Value.ToString();
            int nFirstIndex = strDate.IndexOf( "/" );
            int nSecondIndex = strDate.IndexOf( "/", nFirstIndex + 1 );

            string strCurrentCellYear = strDate.Substring( 0, nFirstIndex );
            string strCurrentCellMonth = strDate.Substring( nFirstIndex + 1, nSecondIndex - nFirstIndex - 1 );
            string strCurrentCellDate = strDate.Substring( nSecondIndex + 1 );

            m_Sql.NoHistoryDelete_SQL( "project_holiday", "project_no = '" + m_strProjectNumber + "' AND date = '" + strCurrentCellYear + "-" + strCurrentCellMonth + "-" + strCurrentCellDate + "'" );
        }

        private void UpdateUI()
        {
            if ( uiRadioBtnRestrictSchedule.Checked || uiRadioBtnCalenderDay.Checked )
            {
                uiRadioBtnNoWeekend.Enabled = false;
                uiRadioBtnSatSun.Enabled = false;
                uiRadioBtnSun.Enabled = false;
            }
            else 
            {
                uiRadioBtnNoWeekend.Enabled = true;
                uiRadioBtnSatSun.Enabled = true;
                uiRadioBtnSun.Enabled = true;
            }
        }

        //以下為UI操作
        private void btnSave_Click( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.WaitCursor;
            m_Sql.OpenSqlChannel();

            string strExistComputeType = m_Sql.ReadSqlDataWithoutOpenClose( "compute_type", "project_constant_condition", "project_no = '" + this.m_strProjectNumber + "'" );
            string strNewComputeType = "";

            if ( uiRadioBtnRestrictSchedule.Checked == true )
            {
                strNewComputeType = ( ( int )ConstantCondition.RestrictSchedule ).ToString();
            }
            else if ( uiRadioBtnCalenderDay.Checked == true )
            {
                strNewComputeType = ( ( int )ConstantCondition.CalenderDay ).ToString();
            }
            else if ( uiRadioBtnWorkingDay.Checked == true )
            {
                if ( uiRadioBtnNoWeekend.Checked == true )
                {
                    strNewComputeType = ( ( int )ConstantCondition.WorkingDayNoWeekend ).ToString();
                }
                else if ( uiRadioBtnSun.Checked == true )
                {
                    strNewComputeType = ( ( int )ConstantCondition.WorkingDaySun ).ToString();
                }
                else if ( uiRadioBtnSatSun.Checked == true )
                {
                    strNewComputeType = ( ( int )ConstantCondition.WorkingDaySatSun ).ToString();
                }
            }

            if ( strExistComputeType == string.Empty )
            {
                string commandStr = "INSERT INTO project_constant_condition(";
                commandStr += "project_no,";
                commandStr += "compute_type";
                commandStr += ") VALUES('";
                commandStr += this.m_strProjectNumber + "','";
                commandStr += strNewComputeType;
                commandStr += "')";

                m_Sql.ExecuteNonQueryCommand( commandStr );
            }
            else
            {
                m_Sql.SetSqlDataWithoutOpenClose( "compute_type", "project_constant_condition", "project_no = '" + this.m_strProjectNumber + "'", strNewComputeType );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;     
        }

        private void btnCopy_Click( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.WaitCursor;
            m_kDataTable.Clear();
            //清掉舊有資料
            m_Sql.NoHistoryDelete_SQL( "project_holiday", "project_no = '" + m_strProjectNumber + "'" );

            //從主檔案讀取資料
            string [] dateArr = m_Sql.Read1DArrayNoCondition_SQL_Data( "date", "holiday" );
            DateTime [] dates = new DateTime [ dateArr.Length ];
            for ( int i = 0; i < dateArr.Length; i++ )
            {
                dates [ i ] = Functions.TransferSQLDateToDateTime( dateArr [ i ] );
            }
            Array.Sort( dates );


            m_Sql.OpenSqlChannel();

            for ( int j = 0; j < dateArr.Length; j++ )
            {
                string commandStr = "Insert into project_holiday(";
                commandStr += "project_no,";
                commandStr += "date,";
                commandStr += "reason,";
                commandStr += "working";
                commandStr += ") values('";
                commandStr += m_strProjectNumber + "','";
                commandStr += dates [ j ].Year.ToString() + "-" + dates [ j ].Month.ToString() + "-" + dates [ j ].Day.ToString() + "','";
                string strReason = m_Sql.ReadSqlDataWithoutOpenClose( "reason", "holiday", "date = '" + dates [ j ].Year.ToString() + "-" + dates [ j ].Month.ToString() + "-" + dates [ j ].Day.ToString() + "'" );
                string strWorking = m_Sql.ReadSqlDataWithoutOpenClose( "working", "holiday", "date = '" + dates [ j ].Year.ToString() + "-" + dates [ j ].Month.ToString() + "-" + dates [ j ].Day.ToString() + "'" );

                commandStr += strReason + "','";
                commandStr += strWorking;//放假
                commandStr += "')";


                m_Sql.ExecuteNonQueryCommand( commandStr );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;


            RefreshDatagridview();
        }

        private void btnDelete_Click( object sender, EventArgs e )
        {
            DialogResult result = MessageBox.Show( "確定要刪除此日期?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation );
            if ( result == DialogResult.Yes )
            {
                DeleteExistDB();
                RefreshDatagridview();
            }
        }

        private void radioBtnWorkingCondition_CheckedChanged( object sender, EventArgs e )
        {
            UpdateUI();
        }
    }
}
