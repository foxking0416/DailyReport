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
    public partial class LaborIncreaseForm : IncreaseEditFormBase
    {
        public LaborIncreaseForm(MySQL Sql)
            : base(Sql)
        {
            InitializeComponent();
            strFunctionName = "工別";
            strFunctionNameEng = "labor";
            strTitle = "工人別新增作業";

            this.Text = "工人別新增作業";
            this.labelNumber.Text = strFunctionName + "編號";
            this.labelName.Text = strFunctionName + "名稱";
            this.labelUnit.Visible = false;
            this.textBox_Unit.Visible = false;
            this.btnAddEdit.Text = "新增";
            this.dataGridView.Size = new System.Drawing.Size(350, 240);
            this.dataGridView.Location = new System.Drawing.Point(10, 90);
            Initialize();
        }

        protected override void InitializeDataTable()
        {
            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add(strFunctionName + "編號", typeof(String));
            dataTable.Columns.Add(strFunctionName + "名稱", typeof(String));
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

            string[] arrNumbers = m_Sql.Read1DArrayNoCondition_SQL_Data("number", strFunctionNameEng);
            Array.Sort(arrNumbers);

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < arrNumbers.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[strFunctionName + "編號"] = arrNumbers[i];
                dataRow[strFunctionName + "名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("name", strFunctionNameEng, "number = '" + arrNumbers[i] + "'");
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
            commandStr = commandStr + "name";
            commandStr = commandStr + ") values('";
            commandStr = commandStr + textBox_No.Text + "','";
            commandStr = commandStr + textBox_Name.Text;
            commandStr = commandStr + "')";

            m_Sql.ExecuteNonQueryCommand(commandStr);
            m_Sql.CloseSqlChannel();
        }

        protected override void EventBtnAddEdit_Click(object sender, EventArgs e)
        {
            labelWarningNumber.Visible = false;
            labelWarningName.Visible = false;

            if (textBox_No.Text == string.Empty)
            {
                labelWarningNumber.Visible = true;
                return;
            }

            if (textBox_Name.Text == string.Empty)
            {
                labelWarningName.Visible = true;
                return;
            }

            int i = 0;
            bool result = int.TryParse( textBox_No.Text, out i );
            if( !result )
            {
                labelWarningNumber.Text = "編號只能為數字";
                labelWarningNumber.Visible = true;
                return;
            }
            else
            {
                labelWarningNumber.Text = "編號不可為空白"; 
            }
                

            string[] arrSameNo = m_Sql.Read1DArray_SQL_Data("number", strFunctionNameEng, "number = '" + textBox_No.Text + "'");
            if( arrSameNo.Length != 0 )
            {
                labelWarningNumber.Text = "已存在相同" + strFunctionName + "編號";
                labelWarningNumber.Visible = true;
                return;
            }


            InsertIntoDB();
            RefreshDatagridview();
            textBox_No.Clear();
            textBox_Name.Clear();
        }
    }
}
