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
        protected string[] arrNumbers;
        protected string[] arrNames;
        protected DataTable kDataTable;
        protected string strDataBaseTableName;
        protected string strDataBaseNumber;
        protected string strDataBaseName;
        protected string strRowNo;
        protected string strRowName;
        protected int nFormType;
        protected int nRowIndex;
        protected int nColumnIndex;
        protected int nTabIndex;
        protected DailyReportIncreaseForm formDailyReportIncrease;

        public SearchFormBase(MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
        }

        protected virtual void Initialize()
        {
            textBox_Name.Enabled = false;
            textBox_Class.Enabled = false;

            arrNumbers = m_Sql.Read1DArrayNoCondition_SQL_Data(strDataBaseNumber, strDataBaseTableName);
            arrNames = m_Sql.Read1DArrayNoCondition_SQL_Data(strDataBaseName, strDataBaseTableName);

            kDataTable = new DataTable("MyNewTable");
            kDataTable.Columns.Add(strRowNo, typeof(String));
            kDataTable.Columns.Add(strRowName, typeof(String));
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
                kDataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        private void EventCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox_No.Enabled = false;
            textBox_Name.Enabled = false;
            textBox_Class.Enabled = false;

            if ( radioBtnNo.Checked )
            {
                textBox_No.Enabled = true;
            }
            else if ( radioBtnName.Checked )
            {
                textBox_Name.Enabled = true;
            }
            else
            {
                textBox_Class.Enabled = true;
            }
        }

        protected virtual void btnCheck_Click(object sender, EventArgs e)
        {
        }

        protected virtual void textBox1_TextChanged(object sender, EventArgs e)
        {
            kDataTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < arrNumbers.Length; i++)
            {
                if (arrNumbers[i].Contains(textBox_No.Text))
                    array.Add(arrNumbers[i]);
            }


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < array.Count; i++)
            {
                dataRow = kDataTable.NewRow();
                dataRow[strRowNo] = array[i];
                dataRow[strRowName] = m_Sql.ReadSqlDataWithoutOpenClose(strDataBaseName, strDataBaseTableName, strDataBaseNumber + " = '" + array[i] + "'");
                kDataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        protected virtual void textBox2_TextChanged(object sender, EventArgs e)
        {
            kDataTable.Clear();

            ArrayList array = new ArrayList();
            for (int i = 0; i < arrNames.Length; i++)
            {
                if (arrNames[i].Contains(textBox_Name.Text))
                    array.Add(arrNames[i]);
            }


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for (int i = 0; i < array.Count; i++)
            {
                dataRow = kDataTable.NewRow();
                dataRow[strRowNo] = m_Sql.ReadSqlDataWithoutOpenClose(strDataBaseNumber, strDataBaseTableName, strDataBaseName + " = '" + array[i] + "'");
                dataRow[strRowName] = array[i];
                kDataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        protected virtual void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
