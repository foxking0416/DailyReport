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
    public partial class ExtentionIncreaseForm : Form
    {
        protected MySQL m_Sql;
        protected string ProjectNumber;

        public ExtentionIncreaseForm(MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
            Initialize();
        }

        public ExtentionIncreaseForm(string ID, MySQL Sql)
        {
            m_Sql = Sql;
            ProjectNumber = ID;
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
        }
        
        protected void InsertIntoDB()
        {
            m_Sql.OpenSqlChannel();

            string commandStr = "Insert into extendduration(";
            commandStr = commandStr + "project_no,";//
            commandStr = commandStr + "grantdate,";//核准日期
            commandStr = commandStr + "grantnumber,";//核准文號
            commandStr = commandStr + "extendvalue,";//追加金額
            commandStr = commandStr + "extendstartdate,";//追加起算日
            commandStr = commandStr + "extendduration,";//追加工期
            commandStr = commandStr + "writedate";//填寫日期
            commandStr = commandStr + ") values('";
            commandStr = commandStr + ProjectNumber + "','";
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeGrantDate.Value) + "','";//核准日期
            commandStr = commandStr + textBoxGrantNumber.Text + "','";//核准文號
            commandStr = commandStr + numericExtendValue.Value + "','";//追加金額
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeExtendStartDate.Value) + "','";//追加起算日
            commandStr = commandStr + numericExtendDuration.Value + "','";//追加工期
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeFilledDate.Value);//填寫日期
            commandStr = commandStr + "')";

            m_Sql.ExecuteNonQueryCommand(commandStr);
            m_Sql.CloseSqlChannel();
        }

        protected virtual void btnOK_Click(object sender, EventArgs e)
        {
            label12.Visible = false;

            if (textBoxGrantNumber.Text == string.Empty)
                label12.Visible = true;

            if (textBoxGrantNumber.Text == string.Empty)
                return;

            string[] sameNo = m_Sql.Read1DArray_SQL_Data("grantnumber", "extendduration", "project_no = '" + ProjectNumber + "' AND grantnumber = '" + textBoxGrantNumber.Text + "'");
            if (sameNo.Length != 0)
            {
                label12.Text = "已存在相同核准文號";
                label12.Visible = true;
                return;
            }


            InsertIntoDB();
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
