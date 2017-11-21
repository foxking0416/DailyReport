using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class VariedFactorSettingForm : Form
    {
        protected MySQL m_Sql;
        string m_strProjectNumber = "";

        public VariedFactorSettingForm( string strProjectNo, MySQL Sql )
        {
            m_strProjectNumber = strProjectNo;
            m_Sql = Sql;
            InitializeComponent();
            m_uiLabelProjectName.Text = Sql.Read_SQL_data( "project_name", "project_info", "project_no = '" + m_strProjectNumber + "'" );
            UpdateUI();
        }

        private void UpdateUI()
        {
            m_Sql.OpenSqlChannel();
            string strMorningRainy = m_Sql.ReadSqlDataWithoutOpenClose( "morning_rainy", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
            string strAfternoonRainy = "";
            string strMorningHeavyRain = "";
            string strAfternoonHeavyRain = "";
            string strMorningTyphoon = "";
            string strAfternoonTyphoon = "";
            string strMorningHot = "";
            string strAfternoonHot = "";
            string strMorningMuddy = "";
            string strAfternoonMuddy = "";
            string strMorningPowerShutdown = "";
            string strAfternoonPowerShutdown = "";
            string strMorningPauseWorking = "";
            string strAfternoonPauseWorking = "";
            string strMorningOther = "";
            string strAfternoonOther = "";
            if ( strMorningRainy != string.Empty )
            {
                strAfternoonRainy = m_Sql.ReadSqlDataWithoutOpenClose( "afternoon_rainy", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strMorningHeavyRain = m_Sql.ReadSqlDataWithoutOpenClose( "morning_heavyrain", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strAfternoonHeavyRain = m_Sql.ReadSqlDataWithoutOpenClose( "afternoon_heavyrain", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strMorningTyphoon = m_Sql.ReadSqlDataWithoutOpenClose( "morning_typhoon", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strAfternoonTyphoon = m_Sql.ReadSqlDataWithoutOpenClose( "afternoon_typhoon", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strMorningHot = m_Sql.ReadSqlDataWithoutOpenClose( "morning_hot", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strAfternoonHot = m_Sql.ReadSqlDataWithoutOpenClose( "afternoon_hot", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strMorningMuddy = m_Sql.ReadSqlDataWithoutOpenClose( "morning_muddy", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strAfternoonMuddy = m_Sql.ReadSqlDataWithoutOpenClose( "afternoon_muddy", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strMorningPowerShutdown = m_Sql.ReadSqlDataWithoutOpenClose( "morning_powershutdown", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strAfternoonPowerShutdown = m_Sql.ReadSqlDataWithoutOpenClose( "afternoon_powershutdown", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strMorningPauseWorking = m_Sql.ReadSqlDataWithoutOpenClose( "morning_pauseworking", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strAfternoonPauseWorking = m_Sql.ReadSqlDataWithoutOpenClose( "afternoon_pauseworking", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" ); ;
                strMorningOther = m_Sql.ReadSqlDataWithoutOpenClose( "morning_other", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
                strAfternoonOther = m_Sql.ReadSqlDataWithoutOpenClose( "afternoon_other", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'" );
            }

            m_Sql.CloseSqlChannel();
            #region Rainy
            if ( strMorningRainy == "0" )
            {
                RBtnMNRainyNoCountAll.Checked = true;
            }
            else if ( strMorningRainy == "1" )
            {
                RBtnMNRainyNoCountHalf.Checked = true;
            }
            else if ( strMorningRainy == "2" )
            {
                RBtnMNRainyStillCount.Checked = true;
            }

            if ( strAfternoonRainy == "0" )
            {
                RBtnANRainyNoCountAll.Checked = true;
            }
            else if ( strAfternoonRainy == "1" )
            {
                RBtnANRainyNoCountHalf.Checked = true;
            }
            else if ( strAfternoonRainy == "2" )
            {
                RBtnANRainyStillCount.Checked = true;
            }
            #endregion

            #region HeavyRain
            if ( strMorningHeavyRain == "0" )
            {
                RBtnMNHeavyRainNoCountAll.Checked = true;
            }
            else if ( strMorningHeavyRain == "1" )
            {
                RBtnMNHeavyRainNoCountHalf.Checked = true;
            }
            else if ( strMorningHeavyRain == "2" )
            {
                RBtnMNHeavyRainStillCount.Checked = true;
            }

            if ( strAfternoonHeavyRain == "0" )
            {
                RBtnANHeavyRainNoCountAll.Checked = true;
            }
            else if ( strAfternoonHeavyRain == "1" )
            {
                RBtnANHeavyRainNoCountHalf.Checked = true;
            }
            else if ( strAfternoonHeavyRain == "2" )
            {
                RBtnANHeavyRainStillCount.Checked = true;
            }
            #endregion

            #region Typhoon
            if ( strMorningTyphoon == "0" )
            {
                RBtnMNTyphoonNoCountAll.Checked = true;
            }
            else if ( strMorningTyphoon == "1" )
            {
                RBtnMNTyphoonNoCountHalf.Checked = true;
            }
            else if ( strMorningTyphoon == "2" )
            {
                RBtnMNTyphoonStillCount.Checked = true;
            }

            if ( strAfternoonTyphoon == "0" )
            {
                RBtnANTyphoonNoCountAll.Checked = true;
            }
            else if ( strAfternoonTyphoon == "1" )
            {
                RBtnANTyphoonNoCountHalf.Checked = true;
            }
            else if ( strAfternoonTyphoon == "2" )
            {
                RBtnANTyphoonStillCount.Checked = true;
            }
            #endregion

            #region Hot
            if ( strMorningHot == "0" )
            {
                RBtnMNHotNoCountAll.Checked = true;
            }
            else if ( strMorningHot == "1" )
            {
                RBtnMNHotNoCountHalf.Checked = true;
            }
            else if ( strMorningHot == "2" )
            {
                RBtnMNHotStillCount.Checked = true;
            }

            if ( strAfternoonHot == "0" )
            {
                RBtnANHotNoCountAll.Checked = true;
            }
            else if ( strAfternoonHot == "1" )
            {
                RBtnANHotNoCountHalf.Checked = true;
            }
            else if ( strAfternoonHot == "2" )
            {
                RBtnANHotStillCount.Checked = true;
            }
            #endregion

            #region Muddy
            if ( strMorningMuddy == "0" )
            {
                RBtnMNMuddyNoCountAll.Checked = true;
            }
            else if ( strMorningMuddy == "1" )
            {
                RBtnMNMuddyNoCountHalf.Checked = true;
            }
            else if ( strMorningMuddy == "2" )
            {
                RBtnMNMuddyStillCount.Checked = true;
            }

            if ( strAfternoonMuddy == "0" )
            {
                RBtnANMuddyNoCountAll.Checked = true;
            }
            else if ( strAfternoonMuddy == "1" )
            {
                RBtnANMuddyNoCountHalf.Checked = true;
            }
            else if ( strAfternoonMuddy == "2" )
            {
                RBtnANMuddyStillCount.Checked = true;
            }
            #endregion

            #region PowerShutdown
            if ( strMorningPowerShutdown == "0" )
            {
                RBtnMNPowerShutdownNoCountAll.Checked = true;
            }
            else if ( strMorningPowerShutdown == "1" )
            {
                RBtnMNPowerShutdownNoCountHalf.Checked = true;
            }
            else if ( strMorningPowerShutdown == "2" )
            {
                RBtnMNPowerShutdownStillCount.Checked = true;
            }

            if ( strAfternoonPowerShutdown == "0" )
            {
                RBtnANPowerShutdownNoCountAll.Checked = true;
            }
            else if ( strAfternoonPowerShutdown == "1" )
            {
                RBtnANPowerShutdownNoCountHalf.Checked = true;
            }
            else if ( strAfternoonPowerShutdown == "2" )
            {
                RBtnANPowerShutdownStillCount.Checked = true;
            }
            #endregion

            #region PauseWorking
            if ( strMorningPauseWorking == "0" )
            {
                RBtnMNPauseWorkingNoCountAll.Checked = true;
            }
            else if ( strMorningPauseWorking == "1" )
            {
                RBtnMNPauseWorkingNoCountHalf.Checked = true;
            }
            else if ( strMorningPauseWorking == "2" )
            {
                RBtnMNPauseWorkingStillCount.Checked = true;
            }

            if ( strAfternoonPauseWorking == "0" )
            {
                RBtnANPauseWorkingNoCountAll.Checked = true;
            }
            else if ( strAfternoonPauseWorking == "1" )
            {
                RBtnANPauseWorkingNoCountHalf.Checked = true;
            }
            else if ( strAfternoonPauseWorking == "2" )
            {
                RBtnANPauseWorkingStillCount.Checked = true;
            }
            #endregion

            #region Other
            if ( strMorningOther == "0" )
            {
                RBtnMNOtherNoCountAll.Checked = true;
            }
            else if ( strMorningOther == "1" )
            {
                RBtnMNOtherNoCountHalf.Checked = true;
            }
            else if ( strMorningOther == "2" )
            {
                RBtnMNOtherStillCount.Checked = true;
            }

            if ( strAfternoonOther == "0" )
            {
                RBtnANOtherNoCountAll.Checked = true;
            }
            else if ( strAfternoonOther == "1" )
            {
                RBtnANOtherNoCountHalf.Checked = true;
            }
            else if ( strAfternoonOther == "2" )
            {
                RBtnANOtherStillCount.Checked = true;
            }
            #endregion

        }

        private void Save()
        {
            string strExistComputeType = m_Sql.Read_SQL_data( "morning_rainy", "project_varied_condition", "project_no = '" + this.m_strProjectNumber + "'" );

            int nMorningRainy = -1;
            int nAfternoonRainy = -1;
            int nMorningHeavyRain = -1;
            int nAfternoonHeavyRain = -1;
            int nMorningTyphoon = -1;
            int nAfternoonTyphoon = -1;
            int nMorningHot = -1;
            int nAfternoonHot = -1;
            int nMorningMuddy = -1;
            int nAfternoonMuddy = -1;
            int nMorningPowerShutdown = -1;
            int nAfternoonPowerShutdown = -1;
            int nMorningPauseWorking = -1;
            int nAfternoonPauseWorking = -1;
            int nMorningOther = -1;
            int nAfternoonOther = -1;

            #region Rainy
            if ( RBtnMNRainyNoCountAll.Checked )
            {
                nMorningRainy = 0;
            }
            else if ( RBtnMNRainyNoCountHalf.Checked )
            {
                nMorningRainy = 1;
            }
            else if ( RBtnMNRainyStillCount.Checked )
            {
                nMorningRainy = 2;
            }

            if ( RBtnANRainyNoCountAll.Checked )
            {
                nAfternoonRainy = 0;
            }
            else if ( RBtnANRainyNoCountHalf.Checked )
            {
                nAfternoonRainy = 1;
            }
            else if ( RBtnANRainyStillCount.Checked )
            {
                nAfternoonRainy = 2;
            }
            #endregion

            #region HeavyRain
            if ( RBtnMNHeavyRainNoCountAll.Checked )
            {
                nMorningHeavyRain = 0;
            }
            else if ( RBtnMNHeavyRainNoCountHalf.Checked )
            {
                nMorningHeavyRain = 1;
            }
            else if ( RBtnMNHeavyRainStillCount.Checked )
            {
                nMorningHeavyRain = 2;
            }

            if ( RBtnANHeavyRainNoCountAll.Checked )
            {
                nAfternoonHeavyRain = 0;
            }
            else if ( RBtnANHeavyRainNoCountHalf.Checked )
            {
                nAfternoonHeavyRain = 1;
            }
            else if ( RBtnANHeavyRainStillCount.Checked )
            {
                nAfternoonHeavyRain = 2;
            }
            #endregion

            #region Typhoon
            if ( RBtnMNTyphoonNoCountAll.Checked )
            {
                nMorningTyphoon = 0;
            }
            else if ( RBtnMNTyphoonNoCountHalf.Checked )
            {
                nMorningTyphoon = 1;
            }
            else if ( RBtnMNTyphoonStillCount.Checked )
            {
                nMorningTyphoon = 2;
            }

            if ( RBtnANTyphoonNoCountAll.Checked )
            {
                nAfternoonTyphoon = 0;
            }
            else if ( RBtnANTyphoonNoCountHalf.Checked )
            {
                nAfternoonTyphoon = 1;
            }
            else if ( RBtnANTyphoonStillCount.Checked )
            {
                nAfternoonTyphoon = 2;
            }
            #endregion

            #region Hot
            if ( RBtnMNHotNoCountAll.Checked )
            {
                nMorningHot = 0;
            }
            else if ( RBtnMNHotNoCountHalf.Checked )
            {
                nMorningHot = 1;
            }
            else if ( RBtnMNHotStillCount.Checked )
            {
                nMorningHot = 2;
            }

            if ( RBtnANHotNoCountAll.Checked )
            {
                nAfternoonHot = 0;
            }
            else if ( RBtnANHotNoCountHalf.Checked )
            {
                nAfternoonHot = 1;
            }
            else if ( RBtnANHotStillCount.Checked )
            {
                nAfternoonHot = 2;
            }
            #endregion

            #region Muddy
            if ( RBtnMNMuddyNoCountAll.Checked )
            {
                nMorningMuddy = 0;
            }
            else if ( RBtnMNMuddyNoCountHalf.Checked )
            {
                nMorningMuddy = 1;
            }
            else if ( RBtnMNMuddyStillCount.Checked )
            {
                nMorningMuddy = 2;
            }

            if ( RBtnANMuddyNoCountAll.Checked )
            {
                nAfternoonMuddy = 0;
            }
            else if ( RBtnANMuddyNoCountHalf.Checked )
            {
                nAfternoonMuddy = 1;
            }
            else if ( RBtnANMuddyStillCount.Checked )
            {
                nAfternoonMuddy = 2;
            }
            #endregion

            #region PowerShutdown
            if ( RBtnMNPowerShutdownNoCountAll.Checked )
            {
                nMorningPowerShutdown = 0;
            }
            else if ( RBtnMNPowerShutdownNoCountHalf.Checked )
            {
                nMorningPowerShutdown = 1;
            }
            else if ( RBtnMNPowerShutdownStillCount.Checked )
            {
                nMorningPowerShutdown = 2;
            }

            if ( RBtnANPowerShutdownNoCountAll.Checked )
            {
                nAfternoonPowerShutdown = 0;
            }
            else if ( RBtnANPowerShutdownNoCountHalf.Checked )
            {
                nAfternoonPowerShutdown = 1;
            }
            else if ( RBtnANPowerShutdownStillCount.Checked )
            {
                nAfternoonPowerShutdown = 2;
            }
            #endregion

            #region PauseWorking
            if ( RBtnMNPauseWorkingNoCountAll.Checked )
            {
                nMorningPauseWorking = 0;
            }
            else if ( RBtnMNPauseWorkingNoCountHalf.Checked )
            {
                nMorningPauseWorking = 1;
            }
            else if ( RBtnMNPauseWorkingStillCount.Checked )
            {
                nMorningPauseWorking = 2;
            }

            if ( RBtnANPauseWorkingNoCountAll.Checked )
            {
                nAfternoonPauseWorking = 0;
            }
            else if ( RBtnANPauseWorkingNoCountHalf.Checked )
            {
                nAfternoonPauseWorking = 1;
            }
            else if ( RBtnANPauseWorkingStillCount.Checked )
            {
                nAfternoonPauseWorking = 2;
            }
            #endregion

            #region Other
            if ( RBtnMNOtherNoCountAll.Checked )
            {
                nMorningOther = 0;
            }
            else if ( RBtnMNOtherNoCountHalf.Checked )
            {
                nMorningOther = 1;
            }
            else if ( RBtnMNOtherStillCount.Checked )
            {
                nMorningOther = 2;
            }

            if ( RBtnANOtherNoCountAll.Checked )
            {
                nAfternoonOther = 0;
            }
            else if ( RBtnANOtherNoCountHalf.Checked )
            {
                nAfternoonOther = 1;
            }
            else if ( RBtnANOtherStillCount.Checked )
            {
                nAfternoonOther = 2;
            }
            #endregion

            if ( strExistComputeType == string.Empty )
            {
                string[] arrCellName = { "project_no",
                                         "morning_rainy",
                                         "afternoon_rainy",
                                         "morning_heavyrain",
                                         "afternoon_heavyrain",
                                         "morning_typhoon",
                                         "afternoon_typhoon",
                                         "morning_hot",
                                         "afternoon_hot",
                                         "morning_muddy",
                                         "afternoon_muddy",
                                         "morning_powershutdown",
                                         "afternoon_powershutdown",
                                         "morning_pauseworking",
                                         "afternoon_pauseworking",
                                         "morning_other",
                                         "afternoon_other" };

                string [] arrValue = { m_strProjectNumber,
                                       nMorningRainy.ToString(),
                                       nAfternoonRainy.ToString(),
                                       nMorningHeavyRain.ToString(),
                                       nAfternoonHeavyRain.ToString(),
                                       nMorningTyphoon.ToString(),
                                       nAfternoonTyphoon.ToString(),
                                       nMorningHot.ToString(),
                                       nAfternoonHot.ToString(),
                                       nMorningMuddy.ToString(),
                                       nAfternoonMuddy.ToString(),
                                       nMorningPowerShutdown.ToString(),
                                       nAfternoonPowerShutdown.ToString(),
                                       nMorningPauseWorking.ToString(),
                                       nAfternoonPauseWorking.ToString(),
                                       nMorningOther.ToString(),
                                       nAfternoonOther.ToString() };

                m_Sql.InsertSqlData( "project_varied_condition", arrCellName, arrValue );                    
            }
            else 
            {
                m_Sql.OpenSqlChannel();
                m_Sql.SetSqlDataWithoutOpenClose( "morning_rainy",           "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nMorningRainy.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "afternoon_rainy",         "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nAfternoonRainy.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "morning_heavyrain",       "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nMorningHeavyRain.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "afternoon_heavyrain",     "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nAfternoonHeavyRain.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "morning_typhoon",         "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nMorningTyphoon.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "afternoon_typhoon",       "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nAfternoonTyphoon.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "morning_hot",             "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nMorningHot.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "afternoon_hot",           "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nAfternoonHot.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "morning_muddy",           "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nMorningMuddy.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "afternoon_muddy",         "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nAfternoonMuddy.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "morning_powershutdown",   "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nMorningPowerShutdown.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "afternoon_powershutdown", "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nAfternoonPowerShutdown.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "morning_pauseworking",    "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nMorningPauseWorking.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "afternoon_pauseworking",  "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nAfternoonPauseWorking.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "morning_other",           "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nMorningOther.ToString() );
                m_Sql.SetSqlDataWithoutOpenClose( "afternoon_other",         "project_varied_condition", "project_no = '" + m_strProjectNumber + "'", nAfternoonOther.ToString() );
                m_Sql.CloseSqlChannel();
            }
        }

        private void btnSave_Click( object sender, EventArgs e )
        {
            Save();
        }

        private void btnSaveExit_Click( object sender, EventArgs e )
        {
            Save();
            this.Close();
        }

        private void btnExit_Click( object sender, EventArgs e )
        {
            this.Close();
        }
    }
}
