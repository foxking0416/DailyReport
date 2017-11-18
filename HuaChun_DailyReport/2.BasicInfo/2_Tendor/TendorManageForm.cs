using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HuaChun_DailyReport
{
    public partial class TendorManageForm : Form
    {
        protected MySQL m_Sql;
        public TendorManageForm( MySQL Sql )
        {
            InitializeComponent();
        }
    }
}
