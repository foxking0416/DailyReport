namespace HuaChun_DailyReport
{
    partial class ConstantFactorSettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uiRadioBtnCalenderDay = new System.Windows.Forms.RadioButton();
            this.uiRadioBtnWorkingDay = new System.Windows.Forms.RadioButton();
            this.uiRadioBtnSatSun = new System.Windows.Forms.RadioButton();
            this.uiRadioBtnSun = new System.Windows.Forms.RadioButton();
            this.uiRadioBtnNoWeekend = new System.Windows.Forms.RadioButton();
            this.btnCopy = new System.Windows.Forms.Button();
            this.uiRadioBtnRestrictSchedule = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_uiDataGridView = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_uiLabelProjectName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_uiDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "契約工期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "星期例假日";
            // 
            // uiRadioBtnCalenderDay
            // 
            this.uiRadioBtnCalenderDay.AutoSize = true;
            this.uiRadioBtnCalenderDay.Location = new System.Drawing.Point(154, 42);
            this.uiRadioBtnCalenderDay.Name = "uiRadioBtnCalenderDay";
            this.uiRadioBtnCalenderDay.Size = new System.Drawing.Size(61, 17);
            this.uiRadioBtnCalenderDay.TabIndex = 1;
            this.uiRadioBtnCalenderDay.Text = "日曆天";
            this.uiRadioBtnCalenderDay.UseVisualStyleBackColor = true;
            this.uiRadioBtnCalenderDay.CheckedChanged += new System.EventHandler(this.radioBtnWorkingCondition_CheckedChanged);
            // 
            // uiRadioBtnWorkingDay
            // 
            this.uiRadioBtnWorkingDay.AutoSize = true;
            this.uiRadioBtnWorkingDay.Checked = true;
            this.uiRadioBtnWorkingDay.Location = new System.Drawing.Point(154, 19);
            this.uiRadioBtnWorkingDay.Name = "uiRadioBtnWorkingDay";
            this.uiRadioBtnWorkingDay.Size = new System.Drawing.Size(61, 17);
            this.uiRadioBtnWorkingDay.TabIndex = 1;
            this.uiRadioBtnWorkingDay.TabStop = true;
            this.uiRadioBtnWorkingDay.Text = "工作天";
            this.uiRadioBtnWorkingDay.UseVisualStyleBackColor = true;
            this.uiRadioBtnWorkingDay.CheckedChanged += new System.EventHandler(this.radioBtnWorkingCondition_CheckedChanged);
            // 
            // uiRadioBtnSatSun
            // 
            this.uiRadioBtnSatSun.AutoSize = true;
            this.uiRadioBtnSatSun.Checked = true;
            this.uiRadioBtnSatSun.Location = new System.Drawing.Point(170, 36);
            this.uiRadioBtnSatSun.Name = "uiRadioBtnSatSun";
            this.uiRadioBtnSatSun.Size = new System.Drawing.Size(73, 17);
            this.uiRadioBtnSatSun.TabIndex = 1;
            this.uiRadioBtnSatSun.TabStop = true;
            this.uiRadioBtnSatSun.Text = "周休二日";
            this.uiRadioBtnSatSun.UseVisualStyleBackColor = true;
            // 
            // uiRadioBtnSun
            // 
            this.uiRadioBtnSun.AutoSize = true;
            this.uiRadioBtnSun.Location = new System.Drawing.Point(170, 59);
            this.uiRadioBtnSun.Name = "uiRadioBtnSun";
            this.uiRadioBtnSun.Size = new System.Drawing.Size(73, 17);
            this.uiRadioBtnSun.TabIndex = 1;
            this.uiRadioBtnSun.TabStop = true;
            this.uiRadioBtnSun.Text = "周休一日";
            this.uiRadioBtnSun.UseVisualStyleBackColor = true;
            // 
            // uiRadioBtnNoWeekend
            // 
            this.uiRadioBtnNoWeekend.AutoSize = true;
            this.uiRadioBtnNoWeekend.Location = new System.Drawing.Point(170, 82);
            this.uiRadioBtnNoWeekend.Name = "uiRadioBtnNoWeekend";
            this.uiRadioBtnNoWeekend.Size = new System.Drawing.Size(37, 17);
            this.uiRadioBtnNoWeekend.TabIndex = 1;
            this.uiRadioBtnNoWeekend.TabStop = true;
            this.uiRadioBtnNoWeekend.Text = "無";
            this.uiRadioBtnNoWeekend.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(335, 24);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(296, 29);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "從主資料庫複製";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // uiRadioBtnRestrictSchedule
            // 
            this.uiRadioBtnRestrictSchedule.AutoSize = true;
            this.uiRadioBtnRestrictSchedule.Location = new System.Drawing.Point(154, 65);
            this.uiRadioBtnRestrictSchedule.Name = "uiRadioBtnRestrictSchedule";
            this.uiRadioBtnRestrictSchedule.Size = new System.Drawing.Size(73, 17);
            this.uiRadioBtnRestrictSchedule.TabIndex = 1;
            this.uiRadioBtnRestrictSchedule.TabStop = true;
            this.uiRadioBtnRestrictSchedule.Text = "限期完工";
            this.uiRadioBtnRestrictSchedule.UseVisualStyleBackColor = true;
            this.uiRadioBtnRestrictSchedule.CheckedChanged += new System.EventHandler(this.radioBtnWorkingCondition_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uiRadioBtnWorkingDay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.uiRadioBtnCalenderDay);
            this.groupBox1.Controls.Add(this.uiRadioBtnRestrictSchedule);
            this.groupBox1.Location = new System.Drawing.Point(12, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.uiRadioBtnSatSun);
            this.groupBox2.Controls.Add(this.uiRadioBtnSun);
            this.groupBox2.Controls.Add(this.uiRadioBtnNoWeekend);
            this.groupBox2.Location = new System.Drawing.Point(12, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 134);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            this.m_uiDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_uiDataGridView.Location = new System.Drawing.Point(335, 59);
            this.m_uiDataGridView.Name = "dataGridView1";
            this.m_uiDataGridView.Size = new System.Drawing.Size(296, 422);
            this.m_uiDataGridView.TabIndex = 5;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(335, 487);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(296, 29);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "刪除此日期";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(77, 343);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(202, 29);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "儲存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "工程名稱";
            // 
            // labelProjectName
            // 
            this.m_uiLabelProjectName.AutoSize = true;
            this.m_uiLabelProjectName.Location = new System.Drawing.Point(74, 23);
            this.m_uiLabelProjectName.Name = "labelProjectName";
            this.m_uiLabelProjectName.Size = new System.Drawing.Size(35, 13);
            this.m_uiLabelProjectName.TabIndex = 7;
            this.m_uiLabelProjectName.Text = "label4";
            // 
            // ConstantFactorSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 524);
            this.Controls.Add(this.m_uiLabelProjectName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_uiDataGridView);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCopy);
            this.Name = "ConstantFactorSettingForm";
            this.Text = "編輯固定因素";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_uiDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton uiRadioBtnCalenderDay;
        private System.Windows.Forms.RadioButton uiRadioBtnWorkingDay;
        private System.Windows.Forms.RadioButton uiRadioBtnSatSun;
        private System.Windows.Forms.RadioButton uiRadioBtnSun;
        private System.Windows.Forms.RadioButton uiRadioBtnNoWeekend;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.RadioButton uiRadioBtnRestrictSchedule;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView m_uiDataGridView;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label m_uiLabelProjectName;

    }
}