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
    public partial class MaterialSearchForm : SearchFormBase
    {
        private MaterialEditForm editForm;
        private string[] units;

        public MaterialSearchForm(MaterialEditForm form, MySQL Sql) 
            : base(Sql)
        {
            InitializeComponent();
            editForm = form;
            InitializeMaterialSearchForm();
            Initialize();
        }

        public MaterialSearchForm(DailyReportIncreaseForm form, int row, int column, MySQL Sql) 
            : base(Sql)
        {
            nFormType = 1;
            nRowIndex = row;
            nColumnIndex = column;
            InitializeComponent();
            formDailyReportIncrease = form;
            InitializeMaterialSearchForm();
            Initialize();
        }

        private void InitializeMaterialSearchForm()
        {
            this.Text = "搜尋材料";
            this.strDataBaseTableName = "material";
            this.strDataBaseNumber = "number";
            this.strDataBaseName = "name";

            this.strRowNo = "材料編號";
            this.strRowName = "材料名稱";

            this.radioBtnNo.Text = "搜尋材料編號";
            this.radioBtnName.Text = "搜尋材料名稱";
        }

        protected override void Initialize()
        {

            textBox2.Enabled = false;


            arrNumbers = m_Sql.Read1DArrayNoCondition_SQL_Data(strDataBaseNumber, strDataBaseTableName);
            arrNames = m_Sql.Read1DArrayNoCondition_SQL_Data(strDataBaseName, strDataBaseTableName);
            units = m_Sql.Read1DArrayNoCondition_SQL_Data("unit", strDataBaseTableName);//unit不是共通的  所以獨立出來寫

            kDataTable = new DataTable("MyNewTable");
            kDataTable.Columns.Add(strRowNo, typeof(String));
            kDataTable.Columns.Add(strRowName, typeof(String));
            kDataTable.Columns.Add("單位", typeof(String));
            dataGridView1.DataSource = kDataTable;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < arrNumbers.Length; i++)
            {
                dataRow = kDataTable.NewRow();
                dataRow[strRowNo] = arrNumbers[i];
                dataRow[strRowName] = m_Sql.ReadSqlDataWithoutOpenClose(strDataBaseName, strDataBaseTableName, strDataBaseNumber + " = '" + arrNumbers[i] + "'");
                dataRow["單位"] = m_Sql.ReadSqlDataWithoutOpenClose("unit", strDataBaseTableName, strDataBaseNumber + " = '" + arrNumbers[i] + "'");
                kDataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {

            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            string unit = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            if (nFormType == 0)
                editForm.LoadInformation(number);
            else if (nFormType == 1)
            {
                formDailyReportIncrease.SetDataGridViewValue(0, number, nColumnIndex, nRowIndex);
                formDailyReportIncrease.SetDataGridViewValue(0, name, nColumnIndex + 1, nRowIndex);
                formDailyReportIncrease.SetDataGridViewValue(0, unit, nColumnIndex + 2, nRowIndex);
            }
            this.Close();
        }

        protected override void textBox1_TextChanged(object sender, EventArgs e)
        {
            kDataTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < arrNumbers.Length; i++)
            {
                if (arrNumbers[i].Contains(textBox1.Text))
                    array.Add(arrNumbers[i]);
            }

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < array.Count; i++)
            {
                dataRow = kDataTable.NewRow();
                dataRow[strRowNo] = array[i];
                dataRow[strRowName] = m_Sql.ReadSqlDataWithoutOpenClose(strDataBaseName, strDataBaseTableName, strDataBaseNumber + " = '" + array[i] + "'");
                dataRow["單位"] = m_Sql.ReadSqlDataWithoutOpenClose("unit", strDataBaseTableName, strDataBaseNumber + " = '" + array[i] + "'");
                kDataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        protected override void textBox2_TextChanged(object sender, EventArgs e)
        {
            kDataTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < arrNames.Length; i++)
            {
                if (arrNames[i].Contains(textBox2.Text))
                    array.Add(arrNames[i]);
            }


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < array.Count; i++)
            {
                dataRow = kDataTable.NewRow();
                dataRow[strRowNo] = m_Sql.ReadSqlDataWithoutOpenClose(strDataBaseNumber, strDataBaseTableName, strDataBaseName + " = '" + array[i] + "'");
                dataRow[strRowName] = array[i];
                dataRow["單位"] = m_Sql.ReadSqlDataWithoutOpenClose("unit", strDataBaseTableName, strDataBaseName + " = '" + array[i] + "'");
                kDataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }
    }
}
