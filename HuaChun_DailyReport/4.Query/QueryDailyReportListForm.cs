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
    public partial class QueryDailyReportListForm : Form
    {
        MySQL m_Sql;
        public QueryDailyReportListForm(MySQL Sql) 
        {
            m_Sql = Sql;
            InitializeComponent();
        }
    }
}
