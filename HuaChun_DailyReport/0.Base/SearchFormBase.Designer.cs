namespace HuaChun_DailyReport
{
    partial class SearchFormBase
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.radioBtnNo = new System.Windows.Forms.RadioButton();
            this.radioBtnName = new System.Windows.Forms.RadioButton();
            this.textBox_No = new System.Windows.Forms.TextBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radioBtnClass = new System.Windows.Forms.RadioButton();
            this.textBox_Class = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 100);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(350, 260);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // radioBtnNo
            // 
            this.radioBtnNo.AutoSize = true;
            this.radioBtnNo.Checked = true;
            this.radioBtnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnNo.Location = new System.Drawing.Point(10, 10);
            this.radioBtnNo.Name = "radioBtnNo";
            this.radioBtnNo.Size = new System.Drawing.Size(56, 20);
            this.radioBtnNo.TabIndex = 1;
            this.radioBtnNo.TabStop = true;
            this.radioBtnNo.Text = "編號";
            this.radioBtnNo.UseVisualStyleBackColor = true;
            this.radioBtnNo.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioBtnName
            // 
            this.radioBtnName.AutoSize = true;
            this.radioBtnName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnName.Location = new System.Drawing.Point(10, 40);
            this.radioBtnName.Name = "radioBtnName";
            this.radioBtnName.Size = new System.Drawing.Size(56, 20);
            this.radioBtnName.TabIndex = 1;
            this.radioBtnName.Text = "名稱";
            this.radioBtnName.UseVisualStyleBackColor = true;
            this.radioBtnName.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // textBox_No
            // 
            this.textBox_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_No.Location = new System.Drawing.Point(140, 9);
            this.textBox_No.Name = "textBox_No";
            this.textBox_No.Size = new System.Drawing.Size(220, 22);
            this.textBox_No.TabIndex = 2;
            this.textBox_No.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox_Name
            // 
            this.textBox_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Name.Location = new System.Drawing.Point(140, 39);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(220, 22);
            this.textBox_Name.TabIndex = 2;
            this.textBox_Name.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(10, 370);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(170, 23);
            this.btnCheck.TabIndex = 3;
            this.btnCheck.Text = "確定";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(190, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(170, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.EventCancel_Click);
            // 
            // radioBtnClass
            // 
            this.radioBtnClass.AutoSize = true;
            this.radioBtnClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBtnClass.Location = new System.Drawing.Point(10, 70);
            this.radioBtnClass.Name = "radioBtnClass";
            this.radioBtnClass.Size = new System.Drawing.Size(56, 20);
            this.radioBtnClass.TabIndex = 4;
            this.radioBtnClass.Text = "類別";
            this.radioBtnClass.UseVisualStyleBackColor = true;
            this.radioBtnClass.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // textBox_Class
            // 
            this.textBox_Class.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Class.Location = new System.Drawing.Point(140, 69);
            this.textBox_Class.Name = "textBox_Class";
            this.textBox_Class.Size = new System.Drawing.Size(220, 22);
            this.textBox_Class.TabIndex = 5;
            // 
            // SearchFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 398);
            this.Controls.Add(this.textBox_Class);
            this.Controls.Add(this.radioBtnClass);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.textBox_No);
            this.Controls.Add(this.radioBtnName);
            this.Controls.Add(this.radioBtnNo);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SearchFormBase";
            this.Text = "搜尋";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.DataGridView dataGridView1;
        protected System.Windows.Forms.RadioButton radioBtnNo;
        protected System.Windows.Forms.RadioButton radioBtnName;
        protected System.Windows.Forms.TextBox textBox_No;
        protected System.Windows.Forms.TextBox textBox_Name;
        protected System.Windows.Forms.Button btnCheck;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.RadioButton radioBtnClass;
        protected System.Windows.Forms.TextBox textBox_Class;
    }
}