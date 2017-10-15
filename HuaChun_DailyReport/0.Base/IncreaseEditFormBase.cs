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
    public partial class IncreaseEditFormBase : Form
    {
        protected MySQL m_Sql;
        protected DataTable dataTable;
        protected string strFunctionName;
        protected string strFunctionNameEng;
        protected string strTitle;

        public IncreaseEditFormBase(MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
        }

        protected void Initialize()
        {
            InitializeDataTable();
        }

        protected virtual void InitializeDataTable()
        { }

        private void EventDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView.ClearSelection();
        }

        protected virtual void EventBtnAddEdit_Click(object sender, EventArgs e)
        {

        }

        private void EventBtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
