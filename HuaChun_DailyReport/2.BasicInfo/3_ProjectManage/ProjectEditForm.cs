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
    public partial class ProjectEditForm : ProjectIncreaseForm
    {
        private string[] projects;
        private int selectIndex = 0;
        private int projectCount;
        private string grantNo = "";
        private DataTable dataTable;
        protected string rowNo;
        protected string rowName;


        public ProjectEditForm(MySQL Sql) 
            : base(Sql)
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            this.Size = new System.Drawing.Size(1000, 700);
            this.btnLast.Visible = true;
            this.btnNext.Visible = true;
            this.btnSearch.Visible = true;

            this.btnSave.Location = new System.Drawing.Point(10, 635);
            this.btnExit.Location = new System.Drawing.Point(220, 635);
            this.tabControl1.Visible = true;
            this.textBoxProjectNo.ReadOnly = true;
            this.Text = "工程編輯作業";
            


            dataTable = new DataTable("MyNewTable");
            dataTable.Columns.Add("No", typeof(String));
            dataTable.Columns.Add("核准日期", typeof(String));
            dataTable.Columns.Add("核准文號", typeof(String));
            dataTable.Columns.Add("追加金額", typeof(String));
            dataTable.Columns.Add("總金額", typeof(String));
            dataTable.Columns.Add("追加起算日", typeof(String));
            dataTable.Columns.Add("追加工期", typeof(String));
            dataTable.Columns.Add("累計追加工期", typeof(String));
            dataTable.Columns.Add("總工期", typeof(String));
            dataTable.Columns.Add("契約完工日", typeof(String));
            dataTable.Columns.Add("變動完工日", typeof(String));
            dataTable.Columns.Add("填寫日期", typeof(String));
            // 


            this.dataGridView1.DataSource = dataTable;





            projects = m_Sql.Read1DArrayNoCondition_SQL_Data("project_no", "project_info");
            projectCount = projects.Length;
            if (projectCount == 0)
                DisableAll();
            else
                LoadInformation(projects[selectIndex]);
        }

        protected override void btnLast_Click(object sender, EventArgs e)
        {
            selectIndex--;
            if (selectIndex < 0)
                selectIndex = projectCount - 1;

            LoadInformation(projects[selectIndex]);
        }

        protected override void btnNext_Click(object sender, EventArgs e)
        {
            selectIndex++;
            if (selectIndex >= projectCount)
                selectIndex = 0;

            LoadInformation(projects[selectIndex]);
        }

        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            ProjectSearchForm searchform = new ProjectSearchForm(this, m_Sql);
            searchform.Show();
        }

        protected override void btnAddExtention_Click(object sender, EventArgs e)
        {
            ExtentionIncreaseForm addExtentionForm = new ExtentionIncreaseForm(textBoxProjectNo.Text, m_Sql);
            addExtentionForm.ShowDialog();
            LoadDataTable();
        }

        protected override void btnEditExtention_Click(object sender, EventArgs e)
        {
            ExtentionEditForm editExtentionForm = new ExtentionEditForm(textBoxProjectNo.Text, grantNo, m_Sql);
            editExtentionForm.ShowDialog();
            LoadDataTable();
        }

        protected override void btnDeleteExtention_Click(object sender, EventArgs e)
        {
            if (grantNo != string.Empty)
            {
                DialogResult result = MessageBox.Show("確定要刪除工期追加資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    m_Sql.NoHistoryDelete_SQL("extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' AND grantnumber = '" + grantNo + "'");
                }
            }
            else
            {
                MessageBox.Show("請選擇要刪除的資料?", "確定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            LoadDataTable();
        }

        protected override void btnConfirmFinish_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("確定設定此日期為核定完工日", "確定?", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.OK)
            {
                m_Sql.Set_SQL_data("confirm_finishdate", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", Functions.TransferDateTimeToSQL(dtPickerConfirmFinish.Value));

                string strConfirmFinishDate = m_Sql.Read_SQL_data("confirm_finishdate", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'");
                if (strConfirmFinishDate == string.Empty)
                {
                    this.labelConfirmFinishDate.Text = "此工程尚無核定完工日";
                }
                else
                {
                    this.labelConfirmFinishDate.Text = "此工程核定完工日為：" + Functions.TransferSQLDateToDateOnly(strConfirmFinishDate);
                }
            }
        }

        protected override void btnDeleteFinish_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("確定刪除核定完工日", "確定?", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.OK)
            {
                m_Sql.Set_SQL_data("confirm_finishdate", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "");
                this.labelConfirmFinishDate.Text = "此工程尚無核定完工日";
            }
        }

        protected override void BtnSave_Click(object sender, EventArgs e)
        {
            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;

            if (textBoxProjectNo.Text == string.Empty)
                label28.Visible = true;

            if (textBoxProjectName.Text == string.Empty)
                label29.Visible = true;

            if (textBoxContractNo.Text == string.Empty)
                label30.Visible = true;

            if (textBoxProjectNo.Text == string.Empty)
                return;
            if (textBoxProjectName.Text == string.Empty)
                return;
            if (textBoxContractNo.Text == string.Empty)
                return;

            string[] sameNo = m_Sql.Read1DArray_SQL_Data("contract_no", "project_info", "contract_no = '" + textBoxContractNo.Text + "' AND project_no <> '" + textBoxProjectNo.Text + "'");
            if (sameNo.Length != 0)
            {
                label30.Text = "已存在相同契約號";
                label30.Visible = true;
                return;
            }

            m_Sql.OpenSqlChannel();

            //覆寫原有資料
            DialogResult result = MessageBox.Show("確定要修改工程資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                //案號及契約號
                m_Sql.SetSqlDataWithoutOpenClose("contract_no", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxContractNo.Text);//
                //工程名稱
                m_Sql.SetSqlDataWithoutOpenClose("project_name", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxProjectName.Text);
                //工程地點
                m_Sql.SetSqlDataWithoutOpenClose("project_location", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxProjectLocation.Text);
                //承包廠商
                m_Sql.SetSqlDataWithoutOpenClose("contractor", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxContractor.Text);
                //業主
                m_Sql.SetSqlDataWithoutOpenClose("owner", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxOwner.Text);
                //專業管理
                m_Sql.SetSqlDataWithoutOpenClose("manage", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxManage.Text);
                //設計
                m_Sql.SetSqlDataWithoutOpenClose("design", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxDesign.Text);
                //監造
                m_Sql.SetSqlDataWithoutOpenClose("supervise", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxSupervisor.Text);
                //工地負責人
                m_Sql.SetSqlDataWithoutOpenClose("responsible", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxResponsible.Text);
                //品管
                m_Sql.SetSqlDataWithoutOpenClose("quality", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxQuality.Text);
                //決標日期
                m_Sql.SetSqlDataWithoutOpenClose("biddate", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", Functions.TransferDateTimeToSQL(uiDateTimeBid.Value));
                //開工日期
                m_Sql.SetSqlDataWithoutOpenClose("startdate", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", Functions.TransferDateTimeToSQL(uiDateTimeStart.Value));
                //契約完工日
                m_Sql.SetSqlDataWithoutOpenClose("contract_finishdate", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", Functions.TransferDateTimeToSQL(uiDateTimeFinish.Value));
                //契約金額
                m_Sql.SetSqlDataWithoutOpenClose("contractamount", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.numericAmount.Text);
                //契約工期
                m_Sql.SetSqlDataWithoutOpenClose("contractduration", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.uiNumericDuration.Text);
                //工程總天數
                m_Sql.SetSqlDataWithoutOpenClose("contractdays", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.uiNumericDays.Text);
                //主辦1
                m_Sql.SetSqlDataWithoutOpenClose("handle1", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxHandle1.Text);
                //主辦1電話
                m_Sql.SetSqlDataWithoutOpenClose("phone1", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxPhone1.Text);
                //主辦2
                m_Sql.SetSqlDataWithoutOpenClose("handle2", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxHandle2.Text);
                //主辦2電話
                m_Sql.SetSqlDataWithoutOpenClose("phone2", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxPhone2.Text);
                //主辦3
                m_Sql.SetSqlDataWithoutOpenClose("handle3", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxHandle3.Text);
                //主辦3電話
                m_Sql.SetSqlDataWithoutOpenClose("phone3", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxPhone3.Text);
                //主辦4
                m_Sql.SetSqlDataWithoutOpenClose("handle4", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxHandle4.Text);
                //主辦4電話
                m_Sql.SetSqlDataWithoutOpenClose("phone4", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxPhone4.Text);
                //現場
                m_Sql.SetSqlDataWithoutOpenClose("onsite", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxOnsite.Text);
                //勞安
                m_Sql.SetSqlDataWithoutOpenClose("security", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", this.textBoxSecurity.Text);
                //工期型式
                
                if (uiRadioBtnRestrictSchedule.Checked == true)
                {
                    m_Sql.SetSqlDataWithoutOpenClose("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "1");
                }
                else if (uiRadioBtnCalenderDay.Checked == true)
                {
                    m_Sql.SetSqlDataWithoutOpenClose("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "2");
                }
                else if (uiRadioBtnWorkingDay.Checked == true)
                {
                    if (radioBtnNoWeekend.Checked == true)
                        m_Sql.SetSqlDataWithoutOpenClose("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "3");
                    else if (radioBtnSun.Checked == true)
                        m_Sql.SetSqlDataWithoutOpenClose("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "4");
                    else if (radioBtnSatSun.Checked == true)
                        m_Sql.SetSqlDataWithoutOpenClose("computetype", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "5");
                }
                //計算假日
                if(radioBtnHolidayNoWorking.Checked)
                    m_Sql.SetSqlDataWithoutOpenClose("holiday", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "1");
                else
                    m_Sql.SetSqlDataWithoutOpenClose("holiday", "project_info", "project_no = '" + this.textBoxProjectNo.Text + "'", "0");


            }
            m_Sql.CloseSqlChannel();
        }

        protected override void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                grantNo = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            }
            catch
            { }
        }

        public void LoadInformation(string projectNumber)
        {
            Cursor.Current = Cursors.WaitCursor;
            m_Sql.OpenSqlChannel();
            string strComputeType = m_Sql.ReadSqlDataWithoutOpenClose("computetype", "project_info", "project_no = '" + projectNumber + "'");

            string strConfirmFinishDate = m_Sql.ReadSqlDataWithoutOpenClose("confirm_finishdate", "project_info", "project_no = '" + projectNumber + "'");
            if(strConfirmFinishDate == string.Empty)
            {
                this.labelConfirmFinishDate.Text = "此工程尚無核定完工日";
            }
            else
            {
                this.labelConfirmFinishDate.Text = "此工程核定完工日為：" + Functions.TransferSQLDateToDateOnly(strConfirmFinishDate);
            }



            if (strComputeType == "1")
            {
                this.uiRadioBtnRestrictSchedule.Checked = true;
                this.uiRadioBtnCalenderDay.Checked = false;
                this.uiRadioBtnWorkingDay.Checked = false;
                this.radioBtnNoWeekend.Checked = true;
                this.radioBtnSun.Checked = false;
                this.radioBtnSatSun.Checked = false;
            }
            else if (strComputeType == "2")
            {
                this.uiRadioBtnRestrictSchedule.Checked = false;
                this.uiRadioBtnCalenderDay.Checked = true;
                this.uiRadioBtnWorkingDay.Checked = false;
                this.radioBtnNoWeekend.Checked = true;
                this.radioBtnSun.Checked = false;
                this.radioBtnSatSun.Checked = false;
            }
            else if (strComputeType == "3")
            {
                this.uiRadioBtnRestrictSchedule.Checked = false;
                this.uiRadioBtnCalenderDay.Checked = false;
                this.uiRadioBtnWorkingDay.Checked = true;
                this.radioBtnNoWeekend.Checked = true;
                this.radioBtnSun.Checked = false;
                this.radioBtnSatSun.Checked = false;
            }
            else if (strComputeType == "4")
            {
                this.uiRadioBtnRestrictSchedule.Checked = false;
                this.uiRadioBtnCalenderDay.Checked = false;
                this.uiRadioBtnWorkingDay.Checked = true;
                this.radioBtnNoWeekend.Checked = false;
                this.radioBtnSun.Checked = true;
                this.radioBtnSatSun.Checked = false;
            }
            else if (strComputeType == "5")
            {
                this.uiRadioBtnRestrictSchedule.Checked = false;
                this.uiRadioBtnCalenderDay.Checked = false;
                this.uiRadioBtnWorkingDay.Checked = true;
                this.radioBtnNoWeekend.Checked = false;
                this.radioBtnSun.Checked = false;
                this.radioBtnSatSun.Checked = true;
            }

            string strHoliday = m_Sql.ReadSqlDataWithoutOpenClose("holiday", "project_info", "project_no = '" + projectNumber + "'");
            if (strHoliday == "1")
            {
                this.radioBtnHolidayNoWorking.Checked = true;
                this.radioBtnHolidayNeedWorking.Checked = false;
            }
            else if (strHoliday == "0")
            {
                this.radioBtnHolidayNoWorking.Checked = false;
                this.radioBtnHolidayNeedWorking.Checked = true;
            }

            string strRainyDay = m_Sql.ReadSqlDataWithoutOpenClose("rainyday", "project_info", "project_no = '" + projectNumber + "'");
            if (strRainyDay == "1")
            {
                this.radioBtnNoWorkingOnHeavyRainyDay.Checked = true;
                this.radioBtnNoWorkingOnSmallRainyDay.Checked = false;
            }
            else if (strRainyDay == "0")
            {
                this.radioBtnNoWorkingOnHeavyRainyDay.Checked = false;
                this.radioBtnNoWorkingOnSmallRainyDay.Checked = true;
            }


            this.textBoxProjectNo.Text = m_Sql.ReadSqlDataWithoutOpenClose("project_no", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxProjectName.Text = m_Sql.ReadSqlDataWithoutOpenClose("project_name", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxContractNo.Text = m_Sql.ReadSqlDataWithoutOpenClose("contract_no", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxProjectLocation.Text = m_Sql.ReadSqlDataWithoutOpenClose("project_location", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxContractor.Text = m_Sql.ReadSqlDataWithoutOpenClose("contractor", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxOwner.Text = m_Sql.ReadSqlDataWithoutOpenClose("owner", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxManage.Text = m_Sql.ReadSqlDataWithoutOpenClose("manage", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxDesign.Text = m_Sql.ReadSqlDataWithoutOpenClose("design", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxSupervisor.Text = m_Sql.ReadSqlDataWithoutOpenClose("supervise", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxResponsible.Text = m_Sql.ReadSqlDataWithoutOpenClose("responsible", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxQuality.Text = m_Sql.ReadSqlDataWithoutOpenClose("quality", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxOnsite.Text = m_Sql.ReadSqlDataWithoutOpenClose("onsite", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxSecurity.Text = m_Sql.ReadSqlDataWithoutOpenClose("security", "project_info", "project_no = '" + projectNumber + "'");

            this.numericAmount.Value = Convert.ToDecimal(m_Sql.ReadSqlDataWithoutOpenClose("contractamount", "project_info", "project_no = '" + projectNumber + "'"));
            this.textBoxHandle1.Text = m_Sql.ReadSqlDataWithoutOpenClose("handle1", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle2.Text = m_Sql.ReadSqlDataWithoutOpenClose("handle2", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle3.Text = m_Sql.ReadSqlDataWithoutOpenClose("handle3", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxHandle4.Text = m_Sql.ReadSqlDataWithoutOpenClose("handle4", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone1.Text = m_Sql.ReadSqlDataWithoutOpenClose("phone1", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone2.Text = m_Sql.ReadSqlDataWithoutOpenClose("phone2", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone3.Text = m_Sql.ReadSqlDataWithoutOpenClose("phone3", "project_info", "project_no = '" + projectNumber + "'");
            this.textBoxPhone4.Text = m_Sql.ReadSqlDataWithoutOpenClose("phone4", "project_info", "project_no = '" + projectNumber + "'");
            this.uiNumericDays.Value = Convert.ToDecimal(m_Sql.ReadSqlDataWithoutOpenClose("contractdays", "project_info", "project_no = '" + projectNumber + "'"));
            this.uiNumericDuration.Value = Convert.ToDecimal(m_Sql.ReadSqlDataWithoutOpenClose("contractduration", "project_info", "project_no = '" + projectNumber + "'"));

          
            string startDate = m_Sql.ReadSqlDataWithoutOpenClose("startdate", "project_info", "project_no = '" + projectNumber + "'");
            uiDateTimeStart.Value = Functions.TransferSQLDateToDateTime(startDate);
         
            string bidDate = m_Sql.ReadSqlDataWithoutOpenClose("biddate", "project_info", "project_no = '" + projectNumber + "'");
            uiDateTimeBid.Value = Functions.TransferSQLDateToDateTime(bidDate);

            string finishDate = m_Sql.ReadSqlDataWithoutOpenClose("contract_finishdate", "project_info", "project_no = '" + projectNumber + "'");
            uiDateTimeFinish.Value = Functions.TransferSQLDateToDateTime(finishDate);


            m_Sql.CloseSqlChannel();

            LoadDataTable();
            Cursor.Current = Cursors.Default;
        }

        private void LoadDataTable()
        {
            m_Sql.OpenSqlChannel();
            dataTable.Clear();

            ArrayList array = new ArrayList();
            string[] numbers = m_Sql.Read1DArray_SQL_Data("grantnumber", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' ORDER BY grantdate ASC");

            double totalValue = Convert.ToDouble(this.numericAmount.Value);
            float accuextendduration = 0;
            float totalduration = Convert.ToSingle(this.uiNumericDuration.Value);

            DataRow dataRow;
            for (int i = 1; i <= numbers.Length; i++)
            {
                dataRow = dataTable.NewRow();
                dataRow["No"] = i.ToString();
                dataRow["核准日期"] = Functions.TransferSQLDateToDateOnly(m_Sql.ReadSqlDataWithoutOpenClose("grantdate", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));
                dataRow["核准文號"] = m_Sql.ReadSqlDataWithoutOpenClose("grantnumber", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["追加金額"] = m_Sql.ReadSqlDataWithoutOpenClose("extendvalue", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                totalValue += Convert.ToDouble(m_Sql.ReadSqlDataWithoutOpenClose("extendvalue", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));
                dataRow["總金額"] = totalValue;
                dataRow["追加起算日"] = Functions.TransferSQLDateToDateOnly(m_Sql.ReadSqlDataWithoutOpenClose("extendstartdate", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));
                float extendDuration = Convert.ToSingle(m_Sql.ReadSqlDataWithoutOpenClose("extendduration", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));
                dataRow["追加工期"] = extendDuration;
                accuextendduration += extendDuration;
                dataRow["累計追加工期"] = accuextendduration;
                totalduration += extendDuration;
                dataRow["總工期"] = totalduration;
                dataRow["契約完工日"] = Functions.GetDateTimeValueSlash(uiDateTimeFinish.Value);

                DayCompute dayCompute = new DayCompute(m_Sql);

                SetupDayComputer(dayCompute);
                DateTime FinishDate = dayCompute.CountByDuration(uiDateTimeStart.Value, totalduration);

                dataRow["變動完工日"] = Functions.GetDateTimeValueSlash(FinishDate);//SQL.ReadSqlDataWithoutOpenClose("modified_finishdate", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'");
                dataRow["填寫日期"] = Functions.TransferSQLDateToDateOnly(m_Sql.ReadSqlDataWithoutOpenClose("writedate", "extendduration", "project_no = '" + this.textBoxProjectNo.Text + "' && grantnumber = '" + numbers[i - 1] + "'"));

                dataTable.Rows.Add(dataRow);
            }
            m_Sql.CloseSqlChannel();
        }

        private void SetupDayComputer(DayCompute dayCompute)
        {

            if (uiRadioBtnRestrictSchedule.Checked == true || uiRadioBtnCalenderDay.Checked == true)
            {
                dayCompute.restOnSaturday = false;
                dayCompute.restOnSunday = false;
                dayCompute.restOnHoliday = false;
            }
            else
            {
                if (radioBtnNoWeekend.Checked == true)
                {
                    dayCompute.restOnSaturday = false;//表示週六要施工
                    dayCompute.restOnSunday = false;//表示週日要施工
                }
                else if (radioBtnSun.Checked == true)
                {
                    dayCompute.restOnSaturday = false;//表示週六要施工
                    dayCompute.restOnSunday = true;//表示週日不施工
                }
                else if (radioBtnSatSun.Checked == true)
                {
                    dayCompute.restOnSaturday = true;//表示週六不施工
                    dayCompute.restOnSunday = true;//表示週日不施工
                }

                if (radioBtnHolidayNoWorking.Checked)
                    dayCompute.restOnHoliday = true;//表示國定假日不施工
                else
                    dayCompute.restOnHoliday = false;//表示國定假日依然要施工
            }
        }

        private void TimeAndValueChanged(object sender, EventArgs e)
        {
            int v1 = (int)Math.Round((double)uiNumericDuration.Value / 0.5);//為了讓numericUpDown固定以0.5為單位
            uiNumericDuration.Value = Convert.ToDecimal(v1 * 0.5);

            if (uiRadioBtnCalenderDay.Checked == true)
            {
                uiNumericDays.Value = uiNumericDuration.Value;
                uiDateTimeFinish.Value = uiDateTimeStart.Value.AddDays(Convert.ToInt16(Math.Ceiling(v1 * 0.5)) - 1);
            }
            else if (uiRadioBtnWorkingDay.Checked == true)
            {
                CalculateByDuration();
            }
            LoadDataTable();
        }

        private void DisableAll()
        {
            foreach (Control child in this.Controls) {
                child.Enabled = false;
            }
        }

    }
}
