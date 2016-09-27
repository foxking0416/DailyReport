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
        string dbHost;
        string dbUser;
        string dbPass;
        string dbName;
        protected MySQL SQL;


        public ProjectIncreaseForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize() 
        {
            dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.1");
            dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");

            SQL = new MySQL(dbHost, dbUser, dbPass, dbName);

            uiNumericDuration.ReadOnly = true;//初始化工期設定
            uiNumericDays.ReadOnly = true;//初始化天數設定
            uiDateTimeFinish.Enabled = true;
            groupBox2.Enabled = false;
            radioBtnHolidayNeedWorking.Checked = true;
        }

        protected void InsertIntoDB()
        {
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();

            string commandStr = "Insert into project_info(";
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
            commandStr = commandStr + "contractamount,";
            commandStr = commandStr + "contractduration,";
            commandStr = commandStr + "contractdays,";
            commandStr = commandStr + "handle1,";
            commandStr = commandStr + "phone1,";
            commandStr = commandStr + "handle2,";
            commandStr = commandStr + "phone2,";
            commandStr = commandStr + "handle3,";
            commandStr = commandStr + "phone3,";
            commandStr = commandStr + "phone4,";
            commandStr = commandStr + "handle4,";
            commandStr = commandStr + "onsite,";
            commandStr = commandStr + "security,";
            commandStr = commandStr + "computetype,";
            commandStr = commandStr + "holiday,";
            commandStr = commandStr + "rainyday";
            commandStr = commandStr + ") values('";
            commandStr = commandStr + textBoxProjectNo.Text + "','";
            commandStr = commandStr + textBoxContractNo.Text + "','";
            commandStr = commandStr + textBoxProjectName.Text + "','";
            commandStr = commandStr + textBoxProjectLocation.Text + "','";
            commandStr = commandStr + textBoxContractor.Text + "','";
            commandStr = commandStr + textBoxOwner.Text + "','";
            commandStr = commandStr + textBoxManage.Text + "','";
            commandStr = commandStr + textBoxDesign.Text + "','";
            commandStr = commandStr + textBoxSupervisor.Text + "','";
            commandStr = commandStr + textBoxResponsible.Text + "','";
            commandStr = commandStr + textBoxQuality.Text + "','";
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeBid.Value)  + "','";
            commandStr = commandStr + Functions.TransferDateTimeToSQL(dateTimeStart.Value) + "','";
            commandStr = commandStr + Functions.TransferDateTimeToSQL(uiDateTimeFinish.Value) + "','";
            commandStr = commandStr + numericAmount.Text + "','";
            commandStr = commandStr + uiNumericDuration.Text + "','";
            commandStr = commandStr + uiNumericDays.Text + "','";
            commandStr = commandStr + textBoxHandle1.Text + "','";
            commandStr = commandStr + textBoxPhone1.Text + "','";
            commandStr = commandStr + textBoxHandle2.Text + "','";
            commandStr = commandStr + textBoxPhone2.Text + "','";
            commandStr = commandStr + textBoxHandle3.Text + "','";
            commandStr = commandStr + textBoxPhone3.Text + "','";
            commandStr = commandStr + textBoxHandle4.Text + "','";
            commandStr = commandStr + textBoxPhone4.Text + "','";
            commandStr = commandStr + textBoxOnsite.Text + "','";
            commandStr = commandStr + textBoxSecurity.Text + "','";
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
                    commandStr = commandStr + "5" + "','";
            }

            if (radioBtnHolidayNoWorking.Checked)
                commandStr = commandStr + "1";
            else
                commandStr = commandStr + "0";

            if (radioBtnNoWorkingOnHeavyRainyDay.Checked)
                commandStr = commandStr + "1";//大雨才不計工期
            else
                commandStr = commandStr + "0";//小雨就不計工期

            commandStr = commandStr + "')";


            command.CommandText = commandStr;
            command.ExecuteNonQuery();
            conn.Close();
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
            DayCompute dayCompute = new DayCompute();

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
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxResponsible);
            searchForm.ShowDialog();
        }

        private void BtnSearchQA_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxQuality);
            searchForm.ShowDialog();
        }

        private void BtnSearchOnsite_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxOnsite);
            searchForm.ShowDialog();
        }

        private void BtnSearchSecurity_Click(object sender, EventArgs e)
        {
            MemberSearchForm searchForm = new MemberSearchForm(this.textBoxSecurity);
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

            string[] arrSameNo = SQL.Read1DArray_SQL_Data("project_no", "project_info", "project_no = '" + textBoxProjectNo.Text + "'");
            if (arrSameNo.Length != 0)
            {
                label28.Text = "已存在相同工程編號";
                label28.Visible = true;
                return;
            }

            arrSameNo = SQL.Read1DArray_SQL_Data("contract_no", "project_info", "contract_no = '" + textBoxContractNo.Text + "'");
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
