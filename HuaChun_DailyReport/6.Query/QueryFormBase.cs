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
    public partial class QueryFormBase : Form
    {
        MySQL m_Sql;
        public QueryFormBase(MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
        }

        private void btnProjectSelect_Click(object sender, EventArgs e)
        {
            ProjectSearchForm form = new ProjectSearchForm(this, m_Sql);
            form.ShowDialog();
        }

        public virtual void LoadProjectInfo(string projectNo)
        { }

        private void QueryFormBase_Load(object sender, EventArgs e)
        {

        }
    }
}
