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
    public partial class ExtentionEditForm : ExtentionIncreaseForm
    {
        public ExtentionEditForm(MySQL Sql) : base(Sql)
        {
            InitializeComponent();
        }

        public ExtentionEditForm(string ID, string grantNo, MySQL Sql)
            : base(ID, Sql)
        {
            InitializeComponent();
            this.textBoxGrantNumber.ReadOnly = true;
            LoadInformation(grantNo);

        }

        public void LoadInformation(string grantNumber)
        {
            
            Cursor.Current = Cursors.WaitCursor;
            m_Sql.OpenSqlChannel();
            //核准日期
            string grantdate = m_Sql.ReadSqlDataWithoutOpenClose("grantdate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeGrantDate.Value = Functions.TransferSQLDateToDateTime(grantdate);
            //核准文號
            this.textBoxGrantNumber.Text = grantNumber;
            //追加金額
            this.numericExtendValue.Value = Convert.ToDecimal(m_Sql.ReadSqlDataWithoutOpenClose("extendvalue", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'"));
            //追加起算日
            string extendstartdate = m_Sql.ReadSqlDataWithoutOpenClose("extendstartdate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeExtendStartDate.Value = Functions.TransferSQLDateToDateTime(extendstartdate);
            //追加工期
            this.numericExtendDuration.Value = Convert.ToDecimal(m_Sql.ReadSqlDataWithoutOpenClose("extendduration", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'"));
            //填寫日期
            string writedate = m_Sql.ReadSqlDataWithoutOpenClose("writedate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + grantNumber + "'");
            this.dateTimeFilledDate.Value = Functions.TransferSQLDateToDateTime(writedate);
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        protected override void btnOK_Click(object sender, EventArgs e)
        {
            label12.Visible = false;

            if (textBoxGrantNumber.Text == string.Empty)
                label12.Visible = true;

            if (textBoxGrantNumber.Text == string.Empty)
                return;


            //覆寫原有資料
            DialogResult result = MessageBox.Show("確定要修改追加工期資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                m_Sql.OpenSqlChannel();
                //核准日期
                m_Sql.SetSqlDataWithoutOpenClose("grantdate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", Functions.TransferDateTimeToSQL(dateTimeGrantDate.Value));
                //核准文號不變動
                //追加金額
                m_Sql.SetSqlDataWithoutOpenClose("extendvalue", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", numericExtendValue.Value.ToString());
                //追加起算日
                m_Sql.SetSqlDataWithoutOpenClose("extendstartdate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", Functions.TransferDateTimeToSQL(dateTimeExtendStartDate.Value));
                //追加工期
                m_Sql.SetSqlDataWithoutOpenClose("extendduration", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", numericExtendDuration.Value.ToString());
                //填寫日期
                m_Sql.SetSqlDataWithoutOpenClose("writedate", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + this.textBoxGrantNumber.Text + "'", Functions.TransferDateTimeToSQL(dateTimeFilledDate.Value));
                m_Sql.CloseSqlChannel();
            }
            this.Close();
        }
    }
}
