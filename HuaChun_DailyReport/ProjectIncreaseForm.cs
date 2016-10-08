using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HuaChun_DailyReport
{
    public partial class ProjectIncreaseForm : Form
    {
        protected MySQL m_Sql;

        public ProjectIncreaseForm(MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
            Initialize();
        }

        private void Initialize() 
        {
            uiNumericDuration.ReadOnly = true;//初始化工期設定
            uiNumericDays.ReadOnly = true;//初始化天數設定
            uiDateTimeFinish.Enabled = true;
            groupBox2.Enabled = false;
            radioBtnHolidayNeedWorking.Checked = true;
        }

        protected void InsertIntoDB()
        {
            Cursor.Current = Cursors.WaitCursor;
            m_Sql.OpenSqlChannel();

            string commandStr = "INSERT INTO project_info(";
            commandStr = commandStr + "project_no,";
            commandStr = commandStr + "contract_no,";
            commandStr = commandStr + "project_name,";
            commandStr = commandStr + "project_location,";
            commandStr = commandStr + "contractor,";
            commandStr = commandStr + "owner,";
            commandStr = commandStr + "manage,";
            commandStr = commandStr + "design,";
            commandStr = commandStr + "supervise,";
            commandStr = commandStr + "responsible,";
            commandStr = commandStr + "quality,";
            commandStr = commandStr + "biddate,";
            commandStr = commandStr + "startdate,";
            commandStr = commandStr + "contract_finishdate,";
            commandStr = commandStr + "modified_finishdate,";
            commandStr = commandStr + "contractamount,";
            commandStr = commandStr + "contractduration,";
            commandStr = commandStr + "contractdays,";
            commandStr = commandStr + "handle1,";
            commandStr = commandStr + "phone1,";
            commandStr = commandStr + "handle2,";
            commandStr = commandStr + "phone2,";
            commandStr = commandStr + "handle3,";
            commandStr = commandStr + "phone3,";
            commandStr = commandStr + "handle4,";
            commandStr = commandStr + "phone4,";
            commandStr = commandStr + "onsite,";
            commandStr = commandStr + "security,";
            commandStr = commandStr + "computetype,";
            commandStr = commandStr + "holiday,";
            commandStr = commandStr + "rainyday";
            commandStr = commandStr + ") VALUES('";
            commandStr = commandStr + textBoxProjectNo.Text + "','";//2
            commandStr = commandStr + textBoxContractNo.Text + "','";//3
            commandStr = commandStr + textBoxProjectName.Text + "','";//4
            commandStr = commandStr + textBoxProjectLocation.Text + "','";//5
            commandStr = commandStr + textBoxContractor.Text + "','";//6
            commandStr = commandStr + textBoxOwner.Text + "','";//7
            commandStr = commandStr + textBoxManage.Text + "','";//8
            commandStr = commandStr + textBoxDesign.Text + "','";//9
            commandStr = commandStr + textBoxSupervisor.Text + "','";//10
            commandStr = commandStr + textBoxResponsible.Text + "','";//11
            commandStr = commandStr + textBoxQuality.Text + "','";//12
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeBid.Value)  + "','";//13
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeStart.Value) + "','";//14
            commandStr = commandStr + Functions.TransferDateTimeToSQL(uiDateTimeFinish.Value) + "','";//15
            commandStr = commandStr + Functions.TransferDateTimeToSQL(uiDateTimeFinish.Value) + "','";//16
            commandStr = commandStr + numericAmount.Text + "','";//17
            commandStr = commandStr + uiNumericDuration.Text + "','";//18
            commandStr = commandStr + uiNumericDays.Text + "','";//19
            commandStr = commandStr + textBoxHandle1.Text + "','";//20
            commandStr = commandStr + textBoxPhone1.Text + "','";//21
            commandStr = commandStr + textBoxHandle2.Text + "','";//22
            commandStr = commandStr + textBoxPhone2.Text + "','";//23
            commandStr = commandStr + textBoxHandle3.Text + "','";//24
            commandStr = commandStr + textBoxPhone3.Text + "','";//25
            commandStr = commandStr + textBoxHandle4.Text + "','";//26
            commandStr = commandStr + textBoxPhone4.Text + "','";//27
            commandStr = commandStr + textBoxOnsite.Text + "','";//28
            commandStr = commandStr + textBoxSecurity.Text + "','";//29
            if (uiRadioBtnRestrictSchedule.Checked == true)
            {
                commandStr = commandStr + "1" + "','";
            }
            else if (uiRadioBtnCalenderDay.Checked == true)
            {
                commandStr = commandStr + "2" + "','";
            }
            else if (uiRadioBtnWorkingDay.Checked == true)
            {
                if (radioBtnNoWeekend.Checked == true)
                    commandStr = commandStr + "3" + "','";
                else if (radioBtnSun.Checked == true)
                    commandStr = commandStr + "4" + "','";
                else if (radioBtnSatSun.Checked == true)
                    commandStr = commandStr + "5" + "','";//30
            }

            if (radioBtnHolidayNoWorking.Checked)
                commandStr = commandStr + "1" + "','";
            else
                commandStr = commandStr + "0" + "','";//31

            if (radioBtnNoWorkingOnHeavyRainyDay.Checked)
                commandStr = commandStr + "1";//大雨才不計工期
            else
                commandStr = commandStr + "0";//小雨就不計工期 //32

            commandStr = commandStr + "')";

            m_Sql.ExecuteNonQueryCommand(commandStr);
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        protected void Clear()
        {
            this.textBoxProjectNo.Clear();
            this.textBoxProjectName.Clear();
            this.textBoxContractNo.Clear();
            this.textBoxProjectLocation.Clear();
            this.textBoxContractor.Clear();
            this.textBoxOwner.Clear();
            this.textBoxManage.Clear();
            this.textBoxDesign.Clear();
            this.textBoxSupervisor.Clear();
            this.textBoxResponsible.Clear();
            this.textBoxQuality.Clear();
            this.uiRadioBtnCalenderDay.Checked = true;
            this.uiRadioBtnWorkingDay.Checked = false;
            this.radioBtnNoWeekend.Checked = true;
            this.radioBtnSun.Checked = false;
            this.radioBtnSatSun.Checked = false;
            this.radioBtnHolidayNeedWorking.Checked = true;
            this.radioBtnHolidayNoWorking.Checked = false;
            this.textBoxHandle1.Clear();
            this.textBoxHandle2.Clear();
            this.textBoxHandle3.Clear();
            this.textBoxHandle4.Clear();
            this.textBoxPhone1.Clear();
            this.textBoxPhone2.Clear();
            this.textBoxPhone3.Clear();
            this.textBoxPhone4.Clear();
            this.numericAmount.Value = 0;
            this.uiNumericDays.Value = 0;
            this.uiNumericDuration.Value = 0;
        }

        protected void CalculateByDuration()
        {
            DayCompute dayCompute = new DayCompute(m_Sql);

            //設定工期計算方式 
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


            DateTime FinishDate = dayCompute.CountByDuration(dateTimeStart.Value, Convert.ToSingle(uiNumericDuration.Value));
            uiDateTimeFinish.Value = FinishDate;
            uiNumericDays.Value = FinishDate.Subtract(dateTimeStart.Value).Days + 1;
        }

        //private void btnCalculateByDuration_Click(object sender, EventArgs e)
        //{
        //    DayCompute dayCompute = new DayCompute(dateTimeStart.Value, Convert.ToInt32(numericDuration.Value));

        //    SetupDayComputer(dayCompute);


        //    DateTime FinishDate =  dayCompute.CountByDuration(dateTimeStart.Value, Convert.ToInt32(numericDuration.Value));
        //    dateTimeFinish.Value = FinishDate;
        //    numericDays.Value = FinishDate.Subtract(dateTimeStart.Value).Days + 1;
        //}



        //private void btnCalculateByFinish_Click(object sender, EventArgs e)
        //{
        //    DayCompute dayCompute = new DayCompute(dateTimeStart.Value, Convert.ToInt32(numericDuration.Value));
        //    SetupDayComputer(dayCompute);

        //    int duration = dateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;
        //    numericDays.Value = duration + 1;
        //    numericDuration.Value = dayCompute.CountByFinishDay(dateTimeStart.Value, dateTimeFinish.Value);
        //}

        //private void btnCalculateByTotalDays_Click(object sender, EventArgs e)
        //{
        //    DayCompute dayCompute = new DayCompute(dateTimeStart.Value, Convert.ToInt32(numericDuration.Value));
        //    SetupDayComputer(dayCompute);

        //    dateTimeFinish.Value = dateTimeStart.Value.AddDays(Convert.ToInt32(numericDays.Value) - 1);
        //    numericDuration.Value = dayCompute.CountByFinishDay(dateTimeStart.Value, dateTimeFinish.Value);
        //}

        private void BtnSearchSponsor_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxResponsible, m_Sql);
            searchForm.ShowDialog();
        }

        private void BtnSearchQA_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxQuality, m_Sql);
            searchForm.ShowDialog();
        }

        private void BtnSearchOnsite_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxOnsite, m_Sql);
            searchForm.ShowDialog();
        }

        private void BtnSearchSecurity_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxSecurity, m_Sql);
            searchForm.ShowDialog();
        }

        protected virtual void BtnSave_Click(object sender, EventArgs e)
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

            string[] arrSameNo = m_Sql.Read1DArray_SQL_Data("project_no", "project_info", "project_no = '" + textBoxProjectNo.Text + "'");
            if (arrSameNo.Length != 0)
            {
                label28.Text = "已存在相同工程編號";
                label28.Visible = true;
                return;
            }

            arrSameNo = m_Sql.Read1DArray_SQL_Data("contract_no", "project_info", "contract_no = '" + textBoxContractNo.Text + "'");
            if (arrSameNo.Length != 0)
            {
                label30.Text = "已存在相同契約號";
                label30.Visible = true;
                return;
            }


            InsertIntoDB();//插入資料進SQL
            this.Close();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RadioButton123_CheckedChanged(object sender, EventArgs e)
        {
            if (uiRadioBtnRestrictSchedule.Checked == true)//限期完工
            {
                uiNumericDuration.ReadOnly = true;
                uiNumericDays.ReadOnly = true;
                uiDateTimeFinish.Enabled = true;
                groupBox2.Enabled = false;
                //checkBoxHoliday.Enabled = false;
                radioBtnHolidayNoWorking.Enabled = false;
                radioBtnHolidayNeedWorking.Enabled = false;
                int duration = uiDateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;
                this.uiNumericDays.Value = Convert.ToDecimal(duration) + 1;
                this.uiNumericDuration.Value = Convert.ToDecimal(duration) + 1;
                
            }
            else if (uiRadioBtnCalenderDay.Checked == true)//日曆天
            {
                uiNumericDuration.ReadOnly = false;
                uiNumericDays.ReadOnly = true;
                uiDateTimeFinish.Enabled = false;
                groupBox2.Enabled = false;
                //checkBoxHoliday.Enabled = false;
                radioBtnHolidayNeedWorking.Enabled = false;
                radioBtnHolidayNoWorking.Enabled = false;

                int v1 = (int)Math.Round((double)uiNumericDuration.Value / 0.5);//為了讓numericUpDown固定以0.5為單位
                uiNumericDuration.Value = Convert.ToDecimal(v1 * 0.5);
                uiNumericDays.Value = uiNumericDuration.Value;
                uiDateTimeFinish.Value = dateTimeStart.Value.AddDays(Convert.ToInt16(Math.Ceiling(v1 * 0.5))-1);
                

            }
            else if (uiRadioBtnWorkingDay.Checked == true)//工作天
            {
                uiNumericDuration.ReadOnly = false;
                uiNumericDays.ReadOnly = true;
                uiDateTimeFinish.Enabled = false;
                
                groupBox2.Enabled = true;
                //checkBoxHoliday.Enabled = true;
                radioBtnHolidayNoWorking.Enabled = true;
                radioBtnHolidayNeedWorking.Enabled = true;
                CalculateByDuration();
            }
        }

        private void NumericDays_ValueChanged(object sender, EventArgs e)
        {
            int v1 = (int)Math.Round((double)uiNumericDays.Value / 0.5);//為了讓numericUpDown固定以0.5為單位
            uiNumericDays.Value = Convert.ToDecimal(v1 * 0.5);

            if (uiRadioBtnCalenderDay.Checked == true)
            {
                uiNumericDuration.Value = uiNumericDays.Value;
            }
        }

        private void NumericDuration_ValueChanged(object sender, EventArgs e)
        {
            int v1 = (int)Math.Round((double)uiNumericDuration.Value / 0.5);//為了讓numericUpDown固定以0.5為單位
            uiNumericDuration.Value = Convert.ToDecimal(v1 * 0.5);

            if (uiRadioBtnCalenderDay.Checked == true)
            {
                uiNumericDays.Value = uiNumericDuration.Value;
                uiDateTimeFinish.Value = dateTimeStart.Value.AddDays(Convert.ToInt16(Math.Ceiling(v1 * 0.5)) - 1);
            }
            else if (uiRadioBtnWorkingDay.Checked == true)
            {
                CalculateByDuration();
            }
        }

        private void WorkingDayConditionChanged(object sender, EventArgs e)
        {
            CalculateByDuration();
        }

        private void DateTimeFinish_ValueChanged(object sender, EventArgs e)
        {
            int iDuration = uiDateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;

            if (iDuration < 0)
            {
                MessageBox.Show("完工日期不得早於開工日期", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                uiDateTimeFinish.Value = dateTimeStart.Value;
            }
            else
            {
                if (uiRadioBtnRestrictSchedule.Checked == true)
                {
                    this.uiNumericDays.Value = Convert.ToDecimal(iDuration) + 1;
                    this.uiNumericDuration.Value = Convert.ToDecimal(iDuration) + 1;
                }
            }
        }

        private void DateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            int duration = uiDateTimeFinish.Value.Date.Subtract(dateTimeStart.Value.Date).Days;

            if (duration < 0)
            {
                uiDateTimeFinish.Value = dateTimeStart.Value;
                duration = 0;
            }
            if (uiRadioBtnRestrictSchedule.Checked == true)
            {
                this.uiNumericDays.Value = Convert.ToDecimal(duration) + 1;
                this.uiNumericDuration.Value = Convert.ToDecimal(duration) + 1;
            }
        }
    }
}
