using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class OutsourceIncreaseForm : LaborIncreaseForm
    {
        public OutsourceIncreaseForm(MySQL Sql) 
            : base(Sql)
        {
            InitializeComponent();
            strFunctionName = "外包";
            strFunctionNameEng = "Outsource";

            this.Text = "外包新增作業";
            this.labelNumber.Text = strFunctionName + "編號";
            this.labelName.Text = "名稱";
            this.btnAddEdit.Text = "新增";
            Initialize();
        }
    }
}
