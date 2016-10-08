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
    public partial class MemberEditForm : MemberIncreaseForm
    {
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSearch;
        private string[] members;
        private int selectIndex = 0;
        private int memberCount;
        public MemberEditForm() : base()
        {
        }

        public MemberEditForm(MySQL Sql) : base(Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            this.Size = new System.Drawing.Size(543, 510);
            this.Text = "人事編輯作業";

            this.textBoxAccount.ReadOnly = true;
            this.textBoxName.ReadOnly = true;
            this.btnClear.Text = "刪除";
            
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();

            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(14, 419);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(165, 23);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = "上一個";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(185, 419);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(165, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一個";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(356, 419);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(165, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "搜尋";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnSearch);

            this.btnSave.Location = new System.Drawing.Point(14, 445);
            this.btnClear.Location = new System.Drawing.Point(185, 445);
            this.btnExit.Location = new System.Drawing.Point(356, 445);

            members = m_Sql.Read1DArrayNoCondition_SQL_Data("account", "member");
            memberCount = members.Length;
            if (memberCount != 0)
                this.btnClear.Enabled = true;
            else
                this.btnClear.Enabled = false;
            LoadInformation(members[selectIndex]);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            selectIndex--;
            if (selectIndex < 0)
                selectIndex = memberCount - 1;

            LoadInformation(members[selectIndex]);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            selectIndex++;
            if (selectIndex >= memberCount)
                selectIndex = 0;

            LoadInformation(members[selectIndex]);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchform = new MemberSearchForm(this, m_Sql);
            searchform.Show();
        }

        protected override void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要刪除員工資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                m_Sql.NoHistoryDelete_SQL("member", "account = '" + this.textBoxAccount.Text + "'");

                members = m_Sql.Read1DArrayNoCondition_SQL_Data("account", "member");
                memberCount = members.Length;
                --selectIndex;
                if (selectIndex >= 0)
                    LoadInformation(members[selectIndex]);
                else
                {
                    this.btnClear.Enabled = false;
                    selectIndex = 0;
                    Clear();
                }
            }
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要修改人事資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                m_Sql.OpenSqlChannel();
                m_Sql.SetSqlDataWithoutOpenClose("id", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxID.Text);//
                m_Sql.SetSqlDataWithoutOpenClose("sex", "member", "account = '" + this.textBoxAccount.Text + "'", (radioBtnSexM.Checked) ? ("1") : ("2"));
                m_Sql.SetSqlDataWithoutOpenClose("birthdate", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.TransferDateTimeToSQL(dateTimeBirthdate.Value));
                m_Sql.SetSqlDataWithoutOpenClose("degree", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxEducation.Text);//degree
                m_Sql.SetSqlDataWithoutOpenClose("resident_city", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxCity.Text);//resident_city
                m_Sql.SetSqlDataWithoutOpenClose("resident_district", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxDistrict.Text);//resident_district
                m_Sql.SetSqlDataWithoutOpenClose("resident_address", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxAddress.Text);//resident_address
                m_Sql.SetSqlDataWithoutOpenClose("living_city", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxCity2.Text);//living_city
                m_Sql.SetSqlDataWithoutOpenClose("living_district", "member", "account = '" + this.textBoxAccount.Text + "'", this.comboBoxDistrict2.Text);//living_district
                m_Sql.SetSqlDataWithoutOpenClose("living_address", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxAddress2.Text);//living_address
                m_Sql.SetSqlDataWithoutOpenClose("phone", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxPhone.Text);//phone
                m_Sql.SetSqlDataWithoutOpenClose("cell", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxCell.Text);//cell
                m_Sql.SetSqlDataWithoutOpenClose("startdate", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.TransferDateTimeToSQL(dateTimeStart.Value));//startdate
                m_Sql.SetSqlDataWithoutOpenClose("insurancedate", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.TransferDateTimeToSQL(dateTimeInsurance.Value));//insurancedate
                m_Sql.SetSqlDataWithoutOpenClose("enddate", "member", "account = '" + this.textBoxAccount.Text + "'", Functions.TransferDateTimeToSQL(dateTimeLeave.Value));//enddate
                m_Sql.SetSqlDataWithoutOpenClose("position", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxPosition.Text);//position
                m_Sql.SetSqlDataWithoutOpenClose("serviceyear", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxServiceYear.Text);//serviceyear
                m_Sql.SetSqlDataWithoutOpenClose("relative", "member", "account = '" + this.textBoxAccount.Text + "'", this.numericRelative.Text);//relative
                m_Sql.SetSqlDataWithoutOpenClose("bank_name", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankName.Text);//bank_name
                m_Sql.SetSqlDataWithoutOpenClose("bank_account", "member", "account = '" + this.textBoxAccount.Text + "'", this.textBoxBankAccount.Text);//bank_account
                
                if (radioButton1.Checked)
                    m_Sql.SetSqlDataWithoutOpenClose("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "1");//
                else if (radioButton2.Checked)
                    m_Sql.SetSqlDataWithoutOpenClose("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "2");//
                else if (radioButton3.Checked)
                    m_Sql.SetSqlDataWithoutOpenClose("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "3");//
                else if (radioButton4.Checked)
                    m_Sql.SetSqlDataWithoutOpenClose("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "4");//
                else if (radioButton5.Checked)
                    m_Sql.SetSqlDataWithoutOpenClose("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "5");//
                else if (radioButton6.Checked)
                    m_Sql.SetSqlDataWithoutOpenClose("workingtype", "member", "account = '" + this.textBoxAccount.Text + "'", "6");//

                if (radioBtnOnJobN.Checked)
                    m_Sql.SetSqlDataWithoutOpenClose("onjob", "member", "account = '" + this.textBoxAccount.Text + "'", "1");//
                else if (radioBtnOnJobY.Checked)
                    m_Sql.SetSqlDataWithoutOpenClose("onjob", "member", "account = '" + this.textBoxAccount.Text + "'", "2");//

                m_Sql.CloseSqlChannel();
                Cursor.Current = Cursors.Default;

                this.Close();
            }
        }

        public void LoadInformation(string member_account)
        {
            Cursor.Current = Cursors.WaitCursor;
            m_Sql.OpenSqlChannel();
            this.textBoxAccount.Text = m_Sql.ReadSqlDataWithoutOpenClose("account", "member", "account = '" + member_account + "'");
            this.textBoxName.Text = m_Sql.ReadSqlDataWithoutOpenClose("name", "member", "account = '" + member_account + "'");
            this.textBoxID.Text = m_Sql.ReadSqlDataWithoutOpenClose("id", "member", "account = '" + member_account + "'");
            if (m_Sql.ReadSqlDataWithoutOpenClose("sex", "member", "account = '" + member_account + "'") == "1")
                radioBtnSexM.Checked = true;
            else
                radioBtnSexF.Checked = true;

            string birthDate = m_Sql.ReadSqlDataWithoutOpenClose("birthdate", "member", "account = '" + member_account + "'");
            dateTimeBirthdate.Value = Functions.TransferSQLDateToDateTime(birthDate);

            this.textBoxEducation.Text = m_Sql.ReadSqlDataWithoutOpenClose("degree", "member", "account = '" + member_account + "'");
            this.comboBoxCity.Text = m_Sql.ReadSqlDataWithoutOpenClose("resident_city", "member", "account = '" + member_account + "'");
            this.comboBoxDistrict.Text = m_Sql.ReadSqlDataWithoutOpenClose("resident_district", "member", "account = '" + member_account + "'");
            this.textBoxAddress.Text = m_Sql.ReadSqlDataWithoutOpenClose("resident_address", "member", "account = '" + member_account + "'");
            this.comboBoxCity2.Text = m_Sql.ReadSqlDataWithoutOpenClose("living_city", "member", "account = '" + member_account + "'");
            this.comboBoxDistrict2.Text = m_Sql.ReadSqlDataWithoutOpenClose("living_district", "member", "account = '" + member_account + "'");
            this.textBoxAddress2.Text = m_Sql.ReadSqlDataWithoutOpenClose("living_address", "member", "account = '" + member_account + "'");
            this.textBoxPhone.Text = m_Sql.ReadSqlDataWithoutOpenClose("phone", "member", "account = '" + member_account + "'");
            this.textBoxCell.Text = m_Sql.ReadSqlDataWithoutOpenClose("cell", "member", "account = '" + member_account + "'");

            string startDate = m_Sql.ReadSqlDataWithoutOpenClose("startdate", "member", "account = '" + member_account + "'");
            dateTimeStart.Value = Functions.TransferSQLDateToDateTime(startDate);


            string insuranceDate = m_Sql.ReadSqlDataWithoutOpenClose("insurancedate", "member", "account = '" + member_account + "'");
            dateTimeInsurance.Value = Functions.TransferSQLDateToDateTime(insuranceDate);


            string leaveDate = m_Sql.ReadSqlDataWithoutOpenClose("enddate", "member", "account = '" + member_account + "'");
            dateTimeLeave.Value = Functions.TransferSQLDateToDateTime(leaveDate);

            this.textBoxPosition.Text = m_Sql.ReadSqlDataWithoutOpenClose("position", "member", "account = '" + member_account + "'");
            //this.textBoxServiceYear.Text = SQL.ReadSqlDataWithoutOpenClose("phone1", "vendor", "vendor_no = '" + member_account + "'");
            this.numericRelative.Text = m_Sql.ReadSqlDataWithoutOpenClose("relative", "member", "account = '" + member_account + "'");
            this.textBoxBankName.Text = m_Sql.ReadSqlDataWithoutOpenClose("bank_name", "member", "account = '" + member_account + "'");
            this.textBoxBankAccount.Text = m_Sql.ReadSqlDataWithoutOpenClose("bank_account", "member", "account = '" + member_account + "'");
            if (m_Sql.ReadSqlDataWithoutOpenClose("workingtype", "member", "account = '" + member_account + "'") == "1")
                radioButton1.Checked = true;
            else if (m_Sql.ReadSqlDataWithoutOpenClose("workingtype", "member", "account = '" + member_account + "'") == "2")
                radioButton2.Checked = true;
            else if (m_Sql.ReadSqlDataWithoutOpenClose("workingtype", "member", "account = '" + member_account + "'") == "3")
                radioButton3.Checked = true;
            else if (m_Sql.ReadSqlDataWithoutOpenClose("workingtype", "member", "account = '" + member_account + "'") == "4")
                radioButton4.Checked = true;
            else if (m_Sql.ReadSqlDataWithoutOpenClose("workingtype", "member", "account = '" + member_account + "'") == "5")
                radioButton5.Checked = true;
            else if (m_Sql.ReadSqlDataWithoutOpenClose("workingtype", "member", "account = '" + member_account + "'") == "6")
                radioButton6.Checked = true;

            if (m_Sql.ReadSqlDataWithoutOpenClose("onjob", "member", "account = '" + member_account + "'") == "1")
                radioBtnOnJobY.Checked = true;
            else
                radioBtnOnJobN.Checked = true;

            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;

        }

    }
}
