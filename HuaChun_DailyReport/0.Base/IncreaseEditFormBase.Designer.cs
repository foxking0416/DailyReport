namespace HuaChun_DailyReport
{
    partial class IncreaseEditFormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddEdit = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.labelNumber = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelUnit = new System.Windows.Forms.Label();
            this.textBox_No = new System.Windows.Forms.TextBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_Unit = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.labelWarningNumber = new System.Windows.Forms.Label();
            this.labelWarningName = new System.Windows.Forms.Label();
            this.labelWarningUnit = new System.Windows.Forms.Label();
            this.labelClass = new System.Windows.Forms.Label();
            this.labelWarningClass = new System.Windows.Forms.Label();
            this.textBox_Class = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddEdit
            // 
            this.btnAddEdit.Location = new System.Drawing.Point(10, 380);
            this.btnAddEdit.Name = "btnAddEdit";
            this.btnAddEdit.Size = new System.Drawing.Size(170, 23);
            this.btnAddEdit.TabIndex = 3;
            this.btnAddEdit.Text = "新增/編輯";
            this.btnAddEdit.UseVisualStyleBackColor = true;
            this.btnAddEdit.Click += new System.EventHandler(this.EventBtnAddEdit_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(190, 380);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(170, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "離開";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.EventBtnExit_Click);
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumber.Location = new System.Drawing.Point(10, 10);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(38, 16);
            this.labelNumber.TabIndex = 2;
            this.labelNumber.Text = "編號";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(10, 90);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 16);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "名稱";
            // 
            // labelUnit
            // 
            this.labelUnit.AutoSize = true;
            this.labelUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUnit.Location = new System.Drawing.Point(10, 130);
            this.labelUnit.Name = "labelUnit";
            this.labelUnit.Size = new System.Drawing.Size(38, 16);
            this.labelUnit.TabIndex = 3;
            this.labelUnit.Text = "單位";
            // 
            // textBox_No
            // 
            this.textBox_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_No.Location = new System.Drawing.Point(97, 7);
            this.textBox_No.Name = "textBox_No";
            this.textBox_No.Size = new System.Drawing.Size(170, 22);
            this.textBox_No.TabIndex = 0;
            // 
            // textBox_Name
            // 
            this.textBox_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Name.Location = new System.Drawing.Point(97, 87);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(170, 22);
            this.textBox_Name.TabIndex = 2;
            // 
            // textBox_Unit
            // 
            this.textBox_Unit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Unit.Location = new System.Drawing.Point(97, 127);
            this.textBox_Unit.Name = "textBox_Unit";
            this.textBox_Unit.Size = new System.Drawing.Size(170, 22);
            this.textBox_Unit.TabIndex = 3;
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(10, 170);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(350, 200);
            this.dataGridView.TabIndex = 5;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.EventDataGridView_SelectionChanged);
            // 
            // labelWarningNumber
            // 
            this.labelWarningNumber.AutoSize = true;
            this.labelWarningNumber.ForeColor = System.Drawing.Color.Red;
            this.labelWarningNumber.Location = new System.Drawing.Point(100, 30);
            this.labelWarningNumber.Name = "labelWarningNumber";
            this.labelWarningNumber.Size = new System.Drawing.Size(91, 13);
            this.labelWarningNumber.TabIndex = 6;
            this.labelWarningNumber.Text = "編號不可為空白";
            this.labelWarningNumber.Visible = false;
            // 
            // labelWarningName
            // 
            this.labelWarningName.AutoSize = true;
            this.labelWarningName.ForeColor = System.Drawing.Color.Red;
            this.labelWarningName.Location = new System.Drawing.Point(100, 110);
            this.labelWarningName.Name = "labelWarningName";
            this.labelWarningName.Size = new System.Drawing.Size(91, 13);
            this.labelWarningName.TabIndex = 6;
            this.labelWarningName.Text = "名稱不可為空白";
            this.labelWarningName.Visible = false;
            // 
            // labelWarningUnit
            // 
            this.labelWarningUnit.AutoSize = true;
            this.labelWarningUnit.ForeColor = System.Drawing.Color.Red;
            this.labelWarningUnit.Location = new System.Drawing.Point(100, 150);
            this.labelWarningUnit.Name = "labelWarningUnit";
            this.labelWarningUnit.Size = new System.Drawing.Size(91, 13);
            this.labelWarningUnit.TabIndex = 6;
            this.labelWarningUnit.Text = "單位不可為空白";
            this.labelWarningUnit.Visible = false;
            // 
            // labelClass
            // 
            this.labelClass.AutoSize = true;
            this.labelClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClass.Location = new System.Drawing.Point(10, 50);
            this.labelClass.Name = "labelClass";
            this.labelClass.Size = new System.Drawing.Size(38, 16);
            this.labelClass.TabIndex = 7;
            this.labelClass.Text = "類別";
            // 
            // labelWarningClass
            // 
            this.labelWarningClass.AutoSize = true;
            this.labelWarningClass.ForeColor = System.Drawing.Color.Red;
            this.labelWarningClass.Location = new System.Drawing.Point(100, 70);
            this.labelWarningClass.Name = "labelWarningClass";
            this.labelWarningClass.Size = new System.Drawing.Size(91, 13);
            this.labelWarningClass.TabIndex = 9;
            this.labelWarningClass.Text = "類別不可為空白";
            this.labelWarningClass.Visible = false;
            // 
            // textBox_Class
            // 
            this.textBox_Class.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Class.Location = new System.Drawing.Point(97, 47);
            this.textBox_Class.Name = "textBox_Class";
            this.textBox_Class.Size = new System.Drawing.Size(170, 22);
            this.textBox_Class.TabIndex = 1;
            // 
            // IncreaseEditFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 408);
            this.Controls.Add(this.labelWarningClass);
            this.Controls.Add(this.textBox_Class);
            this.Controls.Add(this.labelClass);
            this.Controls.Add(this.labelWarningUnit);
            this.Controls.Add(this.labelWarningName);
            this.Controls.Add(this.labelWarningNumber);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.textBox_Unit);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.textBox_No);
            this.Controls.Add(this.labelUnit);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "IncreaseEditFormBase";
            this.Text = "IncreaseEditFormBase";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnAddEdit;
        protected System.Windows.Forms.Button btnExit;
        protected System.Windows.Forms.Label labelNumber;
        protected System.Windows.Forms.Label labelName;
        protected System.Windows.Forms.Label labelUnit;
        protected System.Windows.Forms.TextBox textBox_No;
        protected System.Windows.Forms.TextBox textBox_Name;
        protected System.Windows.Forms.TextBox textBox_Unit;
        protected System.Windows.Forms.DataGridView dataGridView;
        protected System.Windows.Forms.Label labelWarningNumber;
        protected System.Windows.Forms.Label labelWarningName;
        protected System.Windows.Forms.Label labelWarningUnit;
        protected System.Windows.Forms.Label labelClass;
        protected System.Windows.Forms.Label labelWarningClass;
        protected System.Windows.Forms.TextBox textBox_Class;
    }
}