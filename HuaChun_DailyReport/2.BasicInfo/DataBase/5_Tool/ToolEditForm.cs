﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuaChun_DailyReport
{
    public partial class ToolEditForm : LaborEditForm
    {
        public ToolEditForm(MySQL Sql) 
            : base(Sql)
        {
            InitializeComponent();
            strFunctionName = "機具";
            strFunctionNameEng = "tool";

            this.Text = "機具編輯作業";
            this.labelNumber.Text = strFunctionName + "編號";
            this.labelName.Text = "名稱";
            this.btnAddEdit.Text = "修改";
            this.dataGridView.CurrentCellChanged += new EventHandler(dataGridView1_CurrentCellChanged);
            this.textBox_No.ReadOnly = true;
            Initialize();
        }

        protected override void EventBtnSearch_Click(object sender, EventArgs e)
        {
            ToolSearchForm searchform = new ToolSearchForm(this, m_Sql);
            searchform.ShowDialog();
        }
    }
}
