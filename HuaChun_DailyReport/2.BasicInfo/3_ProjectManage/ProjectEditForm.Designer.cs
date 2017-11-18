namespace HuaChun_DailyReport
{
    partial class ProjectEditForm
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
            ((System.ComponentModel.ISupportInitialize)(this.uiNumericDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiNumericDuration)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimeStart
            // 
            this.uiDateTimeStart.ValueChanged += new System.EventHandler(this.TimeAndValueChanged);
            // 
            // dateTimeFinish
            // 
            this.uiDateTimeFinish.ValueChanged += new System.EventHandler(this.TimeAndValueChanged);
            // 
            // numericDuration
            // 
            this.uiNumericDuration.ValueChanged += new System.EventHandler(this.TimeAndValueChanged);
            // 
            // ProjectEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 518);
            this.Name = "ProjectEditForm";
            this.Text = "ProjectEditForm";
            ((System.ComponentModel.ISupportInitialize)(this.uiNumericDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiNumericDuration)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}