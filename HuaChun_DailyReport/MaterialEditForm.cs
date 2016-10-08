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
    public partial class MaterialEditForm : EditFormBase
    {
        public MaterialEditForm(MySQL Sql) 
            : base( Sql )
        {
            InitializeComponent();
            strFunctionName = "材料";
            strFunctionNameEng = "material";

            this.Text = "材料編輯作業";
            this.label1.Text = strFunctionName + "編號";
            this.label2.Text = strFunctionName + "名稱";
            this.btnAddEdit.Text = "修改";
            this.dataGridView1.CurrentCellChanged += new EventHandler(dataGridView1_CurrentCellChanged);
            this.textBox_No.ReadOnly = true;
            Initialize();
        }

        protected override void InitializeDataTable()
        {
            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add(strFunctionName + "編號", typeof(String));
            dataTable.Columns.Add(strFunctionName + "名稱", typeof(String));
            dataTable.Columns.Add("單位", typeof(String));
            dataGridView1.DataSource = dataTable;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;

            RefreshDatagridview();
        }

        public void LoadInformation(string number)
        {
            m_Sql.OpenSqlChannel();
            this.textBox_No.Text = m_Sql.ReadSqlDataWithoutOpenClose("number", strFunctionNameEng, "number = '" + number + "'");
            this.textBox_Name.Text = m_Sql.ReadSqlDataWithoutOpenClose("name", strFunctionNameEng, "number = '" + number + "'");
            this.textBox_Unit.Text = m_Sql.ReadSqlDataWithoutOpenClose("unit", strFunctionNameEng, "number = '" + number + "'");
            m_Sql.CloseSqlChannel();
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

        protected void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                this.textBox_No.Text = number;
                this.textBox_Name.Text = m_Sql.ReadSqlDataWithoutOpenClose("name", strFunctionNameEng, "number = '" + number + "'");
                this.textBox_Unit.Text = m_Sql.ReadSqlDataWithoutOpenClose("unit", strFunctionNameEng, "number = '" + number + "'");
            }
            catch
            { }
        }

        protected override void btnAddEdit_Click(object sender, EventArgs e)
        {
            labelWarning1.Visible = false;
            labelWarning2.Visible = false;
            labelWarning3.Visible = false;

            if (textBox_Name.Text == string.Empty)
                labelWarning2.Visible = true;
            if (textBox_Unit.Text == string.Empty)
                labelWarning3.Visible = true;

            if (textBox_Name.Text == string.Empty)
                return;
            if (textBox_Unit.Text == string.Empty)
                return;


            DialogResult result = MessageBox.Show("確定要修改" + strFunctionName + "資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                m_Sql.Set_SQL_data("name", strFunctionNameEng, "number = '" + this.textBox_No.Text + "'", this.textBox_Name.Text);
                m_Sql.Set_SQL_data("unit", strFunctionNameEng, "number = '" + this.textBox_No.Text + "'", this.textBox_Unit.Text);

                RefreshDatagridview();
                textBox_No.Clear();
                textBox_Name.Clear();
                textBox_Unit.Clear();
            }
        }

        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            MaterialSearchForm searchform = new MaterialSearchForm(this, m_Sql);
            searchform.ShowDialog();
        }

        protected override void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要刪除" + strFunctionName + "資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                m_Sql.NoHistoryDelete_SQL(strFunctionNameEng, "number = '" + this.textBox_No.Text + "'");

                RefreshDatagridview();
                textBox_No.Clear();
                textBox_Name.Clear();
                textBox_Unit.Clear();
            }
        }
    }
}
