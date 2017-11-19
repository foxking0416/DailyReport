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
            m_uiLabelProjectName.Text = Sql.Read_SQL_data( "project_name", "project_info", "project_no = '" + strProjectNo + "'" );
            InitializeComponent();
        }

        private void UpdateUI()
        {
             
        }

        private void btnSave_Click( object sender, EventArgs e )
        {

        }

        private void btnSaveExit_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private void btnExit_Click( object sender, EventArgs e )
        {
            this.Close();
        }
    }
}
