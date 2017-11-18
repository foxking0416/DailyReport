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
    public partial class LaborSearchForm : SearchFormBase
    {
        private LaborEditForm editForm;

        public LaborSearchForm(LaborEditForm form, MySQL Sql)
            : base(Sql)
        {
            InitializeComponent();
            editForm = form;
            InitializeMaterialSearchForm();
            Initialize();
        }

        public LaborSearchForm(DailyReportIncreaseForm form, int index, int row, int column, MySQL Sql)
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

        private void InitializeMaterialSearchForm()
        {
            this.Text = "搜尋工別";
            this.strDataBaseTableName = "labor";
            this.strDataBaseNumber = "number";
            this.strDataBaseName = "name";

            this.strRowNo = "工別編號";
            this.strRowName = "工別名稱";

            this.radioBtnNo.Text = "搜尋工別編號";
            this.radioBtnName.Text = "搜尋工別名稱";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            if (nFormType == 0)
                editForm.LoadInformation(number);
            else if (nFormType == 1)
            {
                //switch (tabIndex)
                //{
                //    case 0:
                //        reportForm.SetDataGridViewValue(0, number, columnIndex, rowIndex);
                //        reportForm.SetDataGridViewValue(0, name, columnIndex + 1, rowIndex);
                //        break;
                //    case 1:
                        formDailyReportIncrease.SetDataGridViewValue(1, number, nColumnIndex, nRowIndex);
                        formDailyReportIncrease.SetDataGridViewValue(1, name, nColumnIndex + 1, nRowIndex);
                //        break;
                //    case 2:
                //        reportForm.SetDataGridViewValue(2, number, columnIndex, rowIndex);
                //        reportForm.SetDataGridViewValue(2, name, columnIndex + 1, rowIndex);
                //        break;
                //    case 3:
                //        reportForm.SetDataGridViewValue(3, number, columnIndex, rowIndex);
                //        reportForm.SetDataGridViewValue(3, name, columnIndex + 1, rowIndex);
                //        break;
                //    case 4:
                //        reportForm.SetDataGridViewValue(4, number, columnIndex, rowIndex);
                //        reportForm.SetDataGridViewValue(4, name, columnIndex + 1, rowIndex);
                //        break;
                //    default:
                //        break;
                //}
            }
            this.Close();
        }
    }
}
