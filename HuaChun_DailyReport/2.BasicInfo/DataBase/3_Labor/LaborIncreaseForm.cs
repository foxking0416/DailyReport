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
            this.labelName.Text = "名稱";
            this.btnAddEdit.Text = "新增";
            Initialize();
        }

        protected override void InitializeDataTable()
        {
            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add( strFunctionName + "編號", typeof( String ) );
            dataTable.Columns.Add( strFunctionName + "類別", typeof( String ) );
            dataTable.Columns.Add( strFunctionName + "名稱", typeof( String ) );
            dataTable.Columns.Add( "單位", typeof( String ) );
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
                dataRow [ strFunctionName + "編號" ] = arrNumbers [ i ];
                dataRow [ strFunctionName + "類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", strFunctionNameEng, "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ strFunctionName + "名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", strFunctionNameEng, "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", strFunctionNameEng, "number = '" + arrNumbers [ i ] + "'" );
                dataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void InsertIntoDB()
        {
            m_Sql.OpenSqlChannel();

            string commandStr = "Insert into " + strFunctionNameEng + "(";
            commandStr += "number,";
            commandStr += "class,";
            commandStr += "name,";
            commandStr += "unit";
            commandStr += ") values('";
            commandStr += textBox_No.Text + "','";
            commandStr += textBox_Class.Text + "','";
            commandStr += textBox_Name.Text + "','";
            commandStr += textBox_Unit.Text;
            commandStr += "')";

            m_Sql.ExecuteNonQueryCommand(commandStr);
            m_Sql.CloseSqlChannel();
        }

        protected override void EventBtnAddEdit_Click(object sender, EventArgs e)
        {
            labelWarningNumber.Visible = false;
            labelWarningClass.Visible = false;
            labelWarningName.Visible = false;
            labelWarningUnit.Visible = false;

            if (textBox_No.Text == string.Empty)
            {
                labelWarningNumber.Visible = true;
            }

            if (textBox_Class.Text == string.Empty)
            {
                labelWarningClass.Visible = true;
            }

            if ( textBox_Name.Text == string.Empty )
            {
                labelWarningName.Visible = true;
            }
            if ( textBox_Unit.Text == string.Empty )
            {
                labelWarningUnit.Visible = true;
            }

            if ( textBox_No.Text == string.Empty ||
                 textBox_Class.Text == string.Empty ||
                 textBox_Name.Text == string.Empty ||
                 textBox_Unit.Text == string.Empty )
            {
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
            textBox_Class.Clear();
            textBox_Name.Clear();
            textBox_Unit.Clear();
        }
    }
}
