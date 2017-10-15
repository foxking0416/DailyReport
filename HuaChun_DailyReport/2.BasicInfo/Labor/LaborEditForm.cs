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
    public partial class LaborEditForm : EditFormBase
    {
        public LaborEditForm(MySQL Sql) 
            : base(Sql)
        {
            InitializeComponent();

            strFunctionName = "工別";
            strFunctionNameEng = "labor";

            this.Text = "工人別編輯作業";
            this.labelNumber.Text = strFunctionName + "編號";
            this.labelName.Text = strFunctionName + "名稱";
            this.labelUnit.Visible = false;
            this.textBox_Unit.Visible = false;
            this.btnAddEdit.Text = "修改";
            this.dataGridView.Size = new System.Drawing.Size(346, 232);
            this.dataGridView.Location = new System.Drawing.Point(12, 95);
            this.dataGridView.CurrentCellChanged += new EventHandler(dataGridView1_CurrentCellChanged);
            this.textBox_No.ReadOnly = true;
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

        public void LoadInformation(string number)
        {
            this.textBox_No.Text = m_Sql.Read_SQL_data("number", strFunctionNameEng, "number = '" + number + "'");
            this.textBox_Name.Text = m_Sql.Read_SQL_data("name", strFunctionNameEng, "number = '" + number + "'");
        }

        protected void RefreshDatagridview()
        {
            dataTable.Clear();
            m_Sql.OpenSqlChannel();

            string[] numberArr = m_Sql.Read1DArrayNoCondition_SQL_Data("number", strFunctionNameEng);
            Array.Sort(numberArr);

            DataRow dataRow;
            for (int i = 0; i < numberArr.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow[strFunctionName + "編號"] = numberArr[i];
                dataRow[strFunctionName + "名稱"] = m_Sql.ReadSqlDataWithoutOpenClose("name", strFunctionNameEng, "number = '" + numberArr[i] + "'");
                dataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        protected void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                string number = dataGridView[0, dataGridView.CurrentRow.Index].Value.ToString();
                this.textBox_No.Text = number;
                this.textBox_Name.Text = m_Sql.Read_SQL_data("name", strFunctionNameEng, "number = '" + number + "'");
            }
            catch 
            { }
        }

        protected override void EventBtnAddEdit_Click(object sender, EventArgs e)
        {
            labelWarningName.Visible = false;
            labelWarning3.Visible = false;

            if (textBox_Name.Text == string.Empty)
                labelWarningName.Visible = true;

            if (textBox_Name.Text == string.Empty)
                return;


            DialogResult result = MessageBox.Show("確定要修改" + strFunctionName + "資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                m_Sql.Set_SQL_data("name", strFunctionNameEng, "number = '" + this.textBox_No.Text + "'", this.textBox_Name.Text);

                RefreshDatagridview();
                textBox_No.Clear();
                textBox_Name.Clear();
            }
        }

        protected override void EventBtnSearch_Click(object sender, EventArgs e)
        {
            LaborSearchForm searchform = new LaborSearchForm(this, m_Sql);
            searchform.ShowDialog();
        }

        protected override void EventBtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要刪除" + strFunctionName + "資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                m_Sql.NoHistoryDelete_SQL(strFunctionNameEng, "number = '" + this.textBox_No.Text + "'");

                RefreshDatagridview();
                textBox_No.Clear();
                textBox_Name.Clear();
            }
        }
    }
}
