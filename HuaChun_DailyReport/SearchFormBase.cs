using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class SearchFormBase : Form
    {
        protected MySQL m_Sql;
        protected string[] numbers;
        protected string[] names;
        protected DataTable dataTable;
        protected string DB_TableName;
        protected string DB_No;
        protected string DB_Name;
        protected string rowNo;
        protected string rowName;
        protected int formType;
        protected int rowIndex;
        protected int columnIndex;
        protected int tabIndex;
        protected DailyReportIncreaseForm reportForm;

        public SearchFormBase(MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
        }

        protected virtual void Initialize()
        {
            textBox2.Enabled = false;

            numbers = m_Sql.Read1DArrayNoCondition_SQL_Data(DB_No, DB_TableName);
            names = m_Sql.Read1DArrayNoCondition_SQL_Data(DB_Name, DB_TableName);

            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add(rowNo, typeof(String));
            dataTable.Columns.Add(rowName, typeof(String));
            dataGridView1.DataSource = dataTable;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < numbers.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[rowNo] = numbers[i];
                dataRow[rowName] = m_Sql.ReadSqlDataWithoutOpenClose(DB_Name, DB_TableName, DB_No + " = '" + numbers[i] + "'");
                dataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnNo.Checked)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = false;
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = true;
            }
        }

        protected virtual void btnCheck_Click(object sender, EventArgs e)
        {
        }

        protected virtual void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].Contains(textBox1.Text))
                    array.Add(numbers[i]);
            }


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < array.Count; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[rowNo] = array[i];
                dataRow[rowName] = m_Sql.ReadSqlDataWithoutOpenClose(DB_Name, DB_TableName, DB_No + " = '" + array[i] + "'");
                dataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        protected virtual void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Contains(textBox2.Text))
                    array.Add(names[i]);
            }


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < array.Count; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[rowNo] = m_Sql.ReadSqlDataWithoutOpenClose(DB_No, DB_TableName, DB_Name + " = '" + array[i] + "'");
                dataRow[rowName] = array[i];
                dataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        protected virtual void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
