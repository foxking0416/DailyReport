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
    public partial class OutsourceEditForm : LaborEditForm
    {
        public OutsourceEditForm(MySQL Sql) 
            : base( Sql )
        {
            InitializeComponent();
            strFunctionName = "材料";
            strFunctionNameEng = "Outsource";

            this.Text = "材料編輯作業";
            this.labelNumber.Text = strFunctionName + "編號";
            this.labelName.Text = "名稱";
            this.btnAddEdit.Text = "修改";
            this.dataGridView.CurrentCellChanged += new EventHandler(dataGridView1_CurrentCellChanged);
            this.textBox_No.ReadOnly = true;
            Initialize();
        }

        protected override void EventBtnSearch_Click(object sender, EventArgs e)
        {
            OutsourceSearchForm searchform = new OutsourceSearchForm(this, m_Sql);
            searchform.ShowDialog();
        }
    }
}
