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
    public partial class ToolSearchForm : SearchFormBase
    {
        private ToolEditForm editForm;

        public ToolSearchForm(ToolEditForm form, MySQL Sql)
            : base(Sql)
        {
            InitializeComponent();
            editForm = form;
            InitializeToolSearchForm();
            Initialize();
        }

        public ToolSearchForm(DailyReportIncreaseForm form, int index, int row, int column, MySQL Sql) 
            : base(Sql)
        {
            nFormType = 1;
            nTabIndex = index;
            nRowIndex = row;
            nColumnIndex = column;
            InitializeComponent();
            formDailyReportIncrease = form;
            InitializeToolSearchForm();
            Initialize();
        }

        private void InitializeToolSearchForm()
        {
            this.Text = "搜尋機具";
            this.strDataBaseTableName = "tool";
            this.strDataBaseNumber = "number";
            this.strDataBaseName = "name";

            this.strRowNo = "機具編號";
            this.strRowName = "機具名稱";

            this.radioBtnNo.Text = "搜尋機具編號";
            this.radioBtnName.Text = "搜尋機具名稱";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            if (nFormType == 0)
                editForm.LoadInformation(number);
            else if (nFormType == 1)
            {
                formDailyReportIncrease.SetDataGridViewValue(2, number, nColumnIndex, nRowIndex);
                formDailyReportIncrease.SetDataGridViewValue(2, name, nColumnIndex + 1, nRowIndex);
            }
            this.Close();
        }
    }
}
