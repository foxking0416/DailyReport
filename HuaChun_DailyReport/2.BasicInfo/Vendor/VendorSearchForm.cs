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
    public partial class VendorSearchForm : SearchFormBase
    {
        private VendorEditForm editForm;

        public VendorSearchForm(VendorEditForm form, MySQL Sql)
            : base(Sql)
        {
            nFormType = 0;
            InitializeComponent();
            editForm = form;
            InitializeVendorSearchForm();
            Initialize();
        }

        public VendorSearchForm(DailyReportIncreaseForm form, int index, int row, int column, MySQL Sql) 
            : base(Sql)
        {
            nFormType = 1;
            nTabIndex = index;
            nRowIndex = row;
            nColumnIndex = column;
            InitializeComponent();
            formDailyReportIncrease = form;
            InitializeVendorSearchForm();
            Initialize();
        }

        private void InitializeVendorSearchForm()
        {
            this.Text = "搜尋廠商";
            this.strDataBaseTableName = "vendor";
            this.strDataBaseNumber = "vendor_no";
            this.strDataBaseName = "vendor_name";

            this.strRowNo = "廠商編號";
            this.strRowName = "廠商名稱";

            this.radioBtnNo.Text = "搜尋廠商編號";
            this.radioBtnName.Text = "搜尋廠商名稱";
        }

        protected override void btnCheck_Click(object sender, EventArgs e)
        {
            string number = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string name = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            if (nFormType == 0)
                editForm.LoadInformation(number);
            else if (nFormType == 1)
            {
                switch (nTabIndex)
                {
                    case 0:
                        formDailyReportIncrease.SetDataGridViewValue(0, number, nColumnIndex, nRowIndex);
                        formDailyReportIncrease.SetDataGridViewValue(0, name, nColumnIndex + 1, nRowIndex);
                        break;
                    case 1:
                        formDailyReportIncrease.SetDataGridViewValue(1, number, nColumnIndex, nRowIndex);
                        formDailyReportIncrease.SetDataGridViewValue(1, name, nColumnIndex + 1, nRowIndex);
                        break;
                    case 2:
                        formDailyReportIncrease.SetDataGridViewValue(2, number, nColumnIndex, nRowIndex);
                        formDailyReportIncrease.SetDataGridViewValue(2, name, nColumnIndex + 1, nRowIndex);
                        break;
                    case 3:
                        formDailyReportIncrease.SetDataGridViewValue(3, number, nColumnIndex, nRowIndex);
                        formDailyReportIncrease.SetDataGridViewValue(3, name, nColumnIndex + 1, nRowIndex);
                        break;
                    case 4:
                        formDailyReportIncrease.SetDataGridViewValue(4, number, nColumnIndex, nRowIndex);
                        formDailyReportIncrease.SetDataGridViewValue(4, name, nColumnIndex + 1, nRowIndex);
                        break;
                    default:
                        break;
                }
                
            }
            this.Close();
        }
    }
}
