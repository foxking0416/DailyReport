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
    public partial class ProcessCodeSearchForm : SearchFormBase
    {
        private ProcessCodeEditForm editForm;
        private string[] units;

        public ProcessCodeSearchForm(ProcessCodeEditForm form, MySQL Sql)
            :base(Sql)
        {
            InitializeComponent();
            editForm = form;
            InitializeMaterialSearchForm();
            Initialize();   
        }

        public ProcessCodeSearchForm(DailyReportIncreaseForm form, int index, int row, int column, MySQL Sql)
            : base(Sql)
        {
            formType = 1;
            tabIndex = index;
            rowIndex = row;
            columnIndex = column;
            InitializeComponent();
            reportForm = form;
            InitializeMaterialSearchForm();
            Initialize();
        }

        private void InitializeMaterialSearchForm()
        {
            this.Text = "搜尋施工項目";
            this.DB_TableName = "processcode";
            this.DB_No = "number";
            this.DB_Name = "name";

            this.rowNo = "施工編號";
            this.rowName = "施工名稱";

            this.radioBtnNo.Text = "搜尋施工編號";
            this.radioBtnName.Text = "搜尋施工名稱";
        }

        protected override void Initialize()
        {
            Cursor.Current = Cursors.WaitCursor;
            textBox2.Enabled = false;
            numbers = m_Sql.Read1DArrayNoCondition_SQL_Data(DB_No, DB_TableName);
            names = m_Sql.Read1DArrayNoCondition_SQL_Data(DB_Name, DB_TableName);
            units = m_Sql.Read1DArrayNoCondition_SQL_Data("unit", DB_TableName);//unit不是共通的  所以獨立出來寫

            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add(rowNo, typeof(String));
            dataTable.Columns.Add(rowName, typeof(String));
            dataTable.Columns.Add("單位", typeof(String));
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
                dataRow["單位"] = m_Sql.ReadSqlDataWithoutOpenClose("unit", DB_TableName, DB_No + " = '" + numbers[i] + "'");
                dataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            string unit = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            if (formType == 0)
                editForm.LoadInformation(number);
            else if (formType == 1)
            {
                reportForm.SetDataGridViewValue(3, number, columnIndex, rowIndex);
                reportForm.SetDataGridViewValue(3, name, columnIndex + 1, rowIndex);
                reportForm.SetDataGridViewValue(3, unit, columnIndex + 2, rowIndex);
            }

            this.Close();
        }
    }
}
