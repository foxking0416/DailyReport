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
    public partial class ProcessCodeIncreaseForm : IncreaseEditFormBase
    {
        public ProcessCodeIncreaseForm(MySQL Sql)
            : base(Sql)
        {
            InitializeComponent();
            strFunctionName = "項目";
            strFunctionNameEng = "processcode";

            this.Text = "施工項目新增作業";
            this.labelNumber.Text = strFunctionName + "編號";
            this.labelName.Text = strFunctionName + "名稱";
            this.btnAddEdit.Text = "新增";
            Initialize();
        }

        protected override void InitializeDataTable()
        {
            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add(strFunctionName + "編號", typeof(String));
            dataTable.Columns.Add(strFunctionName + "名稱", typeof(String));
            dataTable.Columns.Add("單位", typeof(String));
            dataGridView.DataSource = dataTable;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.MultiSelect = false;

            RefreshDatagridview();
        }

        private void RefreshDatagridview()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTable.Clear();

            string[] numberArr = m_Sql.Read1DArrayNoCondition_SQL_Data("number", strFunctionNameEng);
            Array.Sort(numberArr);

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < numberArr.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[strFunctionName + "編號"] = numberArr[i];
                dataRow[strFunctionName + "名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("name", strFunctionNameEng, "number = '" + numberArr[i] + "'");
                dataRow["單位"] = m_Sql.ReadSqlDataWithoutOpenClose("unit", strFunctionNameEng, "number = '" + numberArr[i] + "'");
                dataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void InsertIntoDB()
        {
            m_Sql.OpenSqlChannel();

            string commandStr = "Insert into " + strFunctionNameEng + "(";
            commandStr = commandStr + "number,";
            commandStr = commandStr + "name,";
            commandStr = commandStr + "unit";
            commandStr = commandStr + ") values('";
            commandStr = commandStr + textBox_No.Text + "','";
            commandStr = commandStr + textBox_Name.Text + "','";
            commandStr = commandStr + textBox_Unit.Text;
            commandStr = commandStr + "')";

            m_Sql.ExecuteNonQueryCommand(commandStr);
            m_Sql.CloseSqlChannel();
        }

        protected override void EventBtnAddEdit_Click(object sender, EventArgs e)
        {
            labelWarningNumber.Visible = false;
            labelWarningName.Visible = false;
            labelWarning3.Visible = false;

            if (textBox_No.Text == string.Empty)
                labelWarningNumber.Visible = true;
            if (textBox_Name.Text == string.Empty)
                labelWarningName.Visible = true;
            if (textBox_Unit.Text == string.Empty)
                labelWarning3.Visible = true;

            if (textBox_No.Text == string.Empty)
                return;
            if (textBox_Name.Text == string.Empty)
                return;
            if (textBox_Unit.Text == string.Empty)
                return;

            int i = 0;
            bool result = int.TryParse(textBox_No.Text, out i);
            if (!result)
            {
                labelWarningNumber.Text = "編號只能為數字";
                labelWarningNumber.Visible = true;
                return;
            }
            else
                labelWarningNumber.Text = "編號不可為空白";

            string[] sameNo = m_Sql.Read1DArray_SQL_Data("number", strFunctionNameEng, "number = '" + textBox_No.Text + "'");
            if (sameNo.Length != 0)
            {
                labelWarningNumber.Text = "已存在相同" + strFunctionName + "編號";
                labelWarningNumber.Visible = true;
                return;
            }


            InsertIntoDB();
            RefreshDatagridview();
            textBox_No.Clear();
            textBox_Name.Clear();
            textBox_Unit.Clear();
        }
    }
}
