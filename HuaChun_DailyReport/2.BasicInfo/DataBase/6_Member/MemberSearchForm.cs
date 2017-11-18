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
    public partial class MemberSearchForm : SearchFormBase
    {
        private MemberEditForm editForm;
        private TextBox textBoxMember;

        public MemberSearchForm(MemberEditForm form, MySQL Sql) 
            : base(Sql)
        {
            InitializeComponent();
            editForm = form;
            InitializeMaterialSearchForm();
            Initialize();
            nFormType = 0;
        }

        public MemberSearchForm(DailyReportIncreaseForm form, int index, int row, int column, MySQL Sql) 
            : base(Sql)
        {
            nFormType = 1;
            nTabIndex = index;
            nRowIndex = row;
            nColumnIndex = column;
            InitializeComponent();
            formDailyReportIncrease = form;
            InitializeMaterialSearchForm();
            Initialize();
        }

        public MemberSearchForm(TextBox textbox, MySQL Sql)
            : base(Sql)
        {
            InitializeComponent();
            textBoxMember = textbox;
            InitializeMaterialSearchForm();
            Initialize();
            nFormType = 2;
        }

        private void InitializeMaterialSearchForm()
        {
            this.Text = "搜尋員工";
            this.strDataBaseTableName = "member";
            this.strDataBaseNumber = "account";
            this.strDataBaseName = "name";

            this.strRowNo = "員工帳號";
            this.strRowName = "員工姓名";

            this.radioBtnNo.Text = "搜尋員工帳號";
            this.radioBtnName.Text = "搜尋員工姓名";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            if (nFormType == 0)
                editForm.LoadInformation(number);
            if (nFormType == 1)
            {
                formDailyReportIncrease.SetDataGridViewValue(4, number, nColumnIndex, nRowIndex);
                formDailyReportIncrease.SetDataGridViewValue(4, name, nColumnIndex + 1, nRowIndex);
            }
            else if (nFormType == 2)
                textBoxMember.Text = name;
            this.Close();
        }
    }
}
