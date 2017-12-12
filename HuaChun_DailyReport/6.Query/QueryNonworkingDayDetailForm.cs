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
    public partial class QueryNonworkingDayDetailForm : Form
    {
        MySQL m_Sql;
        public QueryNonworkingDayDetailForm(MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
        }
    }
}
