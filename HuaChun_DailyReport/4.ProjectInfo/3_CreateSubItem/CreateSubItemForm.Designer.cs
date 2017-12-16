namespace HuaChun_DailyReport
{
    partial class CreateSubItemForm
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
            this.tabClass = new System.Windows.Forms.TabControl();
            this.tabPageMaterial = new System.Windows.Forms.TabPage();
            this.dataGridViewMaterialSelected = new System.Windows.Forms.DataGridView();
            this.dataGridViewMaterialAll = new System.Windows.Forms.DataGridView();
            this.btnMaterialDelete = new System.Windows.Forms.Button();
            this.btnMaterialImport = new System.Windows.Forms.Button();
            this.tabPageLabor = new System.Windows.Forms.TabPage();
            this.btnLaborDelete = new System.Windows.Forms.Button();
            this.btnLaborImport = new System.Windows.Forms.Button();
            this.dataGridViewLaborSelected = new System.Windows.Forms.DataGridView();
            this.dataGridViewLaborAll = new System.Windows.Forms.DataGridView();
            this.tabPageOutsource = new System.Windows.Forms.TabPage();
            this.btnOutsourceDelete = new System.Windows.Forms.Button();
            this.btnOutsourceImport = new System.Windows.Forms.Button();
            this.dataGridViewOutsourceSelected = new System.Windows.Forms.DataGridView();
            this.dataGridViewOutsourceAll = new System.Windows.Forms.DataGridView();
            this.tabPageTool = new System.Windows.Forms.TabPage();
            this.dataGridViewToolAll = new System.Windows.Forms.DataGridView();
            this.dataGridViewToolSelected = new System.Windows.Forms.DataGridView();
            this.btnToolDelete = new System.Windows.Forms.Button();
            this.btnToolImport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabClass.SuspendLayout();
            this.tabPageMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterialSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterialAll)).BeginInit();
            this.tabPageLabor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLaborSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLaborAll)).BeginInit();
            this.tabPageOutsource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutsourceSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutsourceAll)).BeginInit();
            this.tabPageTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToolAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToolSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // tabClass
            // 
            this.tabClass.Controls.Add(this.tabPageMaterial);
            this.tabClass.Controls.Add(this.tabPageLabor);
            this.tabClass.Controls.Add(this.tabPageOutsource);
            this.tabClass.Controls.Add(this.tabPageTool);
            this.tabClass.Location = new System.Drawing.Point(12, 12);
            this.tabClass.Name = "tabClass";
            this.tabClass.SelectedIndex = 0;
            this.tabClass.Size = new System.Drawing.Size(871, 415);
            this.tabClass.TabIndex = 0;
            // 
            // tabPageMaterial
            // 
            this.tabPageMaterial.Controls.Add(this.dataGridViewMaterialSelected);
            this.tabPageMaterial.Controls.Add(this.dataGridViewMaterialAll);
            this.tabPageMaterial.Controls.Add(this.btnMaterialDelete);
            this.tabPageMaterial.Controls.Add(this.btnMaterialImport);
            this.tabPageMaterial.Location = new System.Drawing.Point(4, 22);
            this.tabPageMaterial.Name = "tabPageMaterial";
            this.tabPageMaterial.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMaterial.Size = new System.Drawing.Size(863, 389);
            this.tabPageMaterial.TabIndex = 0;
            this.tabPageMaterial.Text = "材料";
            this.tabPageMaterial.UseVisualStyleBackColor = true;
            // 
            // dataGridViewMaterialSelected
            // 
            this.dataGridViewMaterialSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMaterialSelected.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewMaterialSelected.Location = new System.Drawing.Point(430, 6);
            this.dataGridViewMaterialSelected.Name = "dataGridViewMaterialSelected";
            this.dataGridViewMaterialSelected.Size = new System.Drawing.Size(427, 348);
            this.dataGridViewMaterialSelected.TabIndex = 2;
            this.dataGridViewMaterialSelected.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.EventDataGridViewSelected_CellMouseDoubleClick);
            // 
            // dataGridViewMaterialAll
            // 
            this.dataGridViewMaterialAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMaterialAll.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewMaterialAll.Name = "dataGridViewMaterialAll";
            this.dataGridViewMaterialAll.Size = new System.Drawing.Size(418, 348);
            this.dataGridViewMaterialAll.TabIndex = 2;
            // 
            // btnMaterialDelete
            // 
            this.btnMaterialDelete.Location = new System.Drawing.Point(637, 360);
            this.btnMaterialDelete.Name = "btnMaterialDelete";
            this.btnMaterialDelete.Size = new System.Drawing.Size(75, 23);
            this.btnMaterialDelete.TabIndex = 1;
            this.btnMaterialDelete.Text = "刪除";
            this.btnMaterialDelete.UseVisualStyleBackColor = true;
            this.btnMaterialDelete.Click += new System.EventHandler(this.EventBtnMaterialDelete_Click);
            // 
            // btnMaterialImport
            // 
            this.btnMaterialImport.Location = new System.Drawing.Point(388, 360);
            this.btnMaterialImport.Name = "btnMaterialImport";
            this.btnMaterialImport.Size = new System.Drawing.Size(75, 23);
            this.btnMaterialImport.TabIndex = 1;
            this.btnMaterialImport.Text = "匯入";
            this.btnMaterialImport.UseVisualStyleBackColor = true;
            this.btnMaterialImport.Click += new System.EventHandler(this.EventBtnMaterialImport_Click);
            // 
            // tabPageLabor
            // 
            this.tabPageLabor.Controls.Add(this.btnLaborDelete);
            this.tabPageLabor.Controls.Add(this.btnLaborImport);
            this.tabPageLabor.Controls.Add(this.dataGridViewLaborSelected);
            this.tabPageLabor.Controls.Add(this.dataGridViewLaborAll);
            this.tabPageLabor.Location = new System.Drawing.Point(4, 22);
            this.tabPageLabor.Name = "tabPageLabor";
            this.tabPageLabor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLabor.Size = new System.Drawing.Size(863, 389);
            this.tabPageLabor.TabIndex = 1;
            this.tabPageLabor.Text = "人工";
            this.tabPageLabor.UseVisualStyleBackColor = true;
            // 
            // btnLaborDelete
            // 
            this.btnLaborDelete.Location = new System.Drawing.Point(699, 360);
            this.btnLaborDelete.Name = "btnLaborDelete";
            this.btnLaborDelete.Size = new System.Drawing.Size(75, 23);
            this.btnLaborDelete.TabIndex = 3;
            this.btnLaborDelete.Text = "刪除";
            this.btnLaborDelete.UseVisualStyleBackColor = true;
            this.btnLaborDelete.Click += new System.EventHandler(this.EventBtnLaborDelete_Click);
            // 
            // btnLaborImport
            // 
            this.btnLaborImport.Location = new System.Drawing.Point(383, 360);
            this.btnLaborImport.Name = "btnLaborImport";
            this.btnLaborImport.Size = new System.Drawing.Size(75, 23);
            this.btnLaborImport.TabIndex = 2;
            this.btnLaborImport.Text = "匯入";
            this.btnLaborImport.UseVisualStyleBackColor = true;
            this.btnLaborImport.Click += new System.EventHandler(this.EventBtnLaborImport_Click);
            // 
            // dataGridViewLaborSelected
            // 
            this.dataGridViewLaborSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLaborSelected.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewLaborSelected.Location = new System.Drawing.Point(431, 6);
            this.dataGridViewLaborSelected.Name = "dataGridViewLaborSelected";
            this.dataGridViewLaborSelected.Size = new System.Drawing.Size(432, 348);
            this.dataGridViewLaborSelected.TabIndex = 1;
            this.dataGridViewLaborSelected.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.EventDataGridViewSelected_CellMouseDoubleClick);
            // 
            // dataGridViewLaborAll
            // 
            this.dataGridViewLaborAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLaborAll.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewLaborAll.Name = "dataGridViewLaborAll";
            this.dataGridViewLaborAll.Size = new System.Drawing.Size(416, 348);
            this.dataGridViewLaborAll.TabIndex = 0;
            // 
            // tabPageOutsource
            // 
            this.tabPageOutsource.Controls.Add(this.btnOutsourceDelete);
            this.tabPageOutsource.Controls.Add(this.btnOutsourceImport);
            this.tabPageOutsource.Controls.Add(this.dataGridViewOutsourceSelected);
            this.tabPageOutsource.Controls.Add(this.dataGridViewOutsourceAll);
            this.tabPageOutsource.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutsource.Name = "tabPageOutsource";
            this.tabPageOutsource.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutsource.Size = new System.Drawing.Size(863, 389);
            this.tabPageOutsource.TabIndex = 2;
            this.tabPageOutsource.Text = "外包";
            this.tabPageOutsource.UseVisualStyleBackColor = true;
            // 
            // btnOutsourceDelete
            // 
            this.btnOutsourceDelete.Location = new System.Drawing.Point(640, 360);
            this.btnOutsourceDelete.Name = "btnOutsourceDelete";
            this.btnOutsourceDelete.Size = new System.Drawing.Size(75, 23);
            this.btnOutsourceDelete.TabIndex = 2;
            this.btnOutsourceDelete.Text = "刪除";
            this.btnOutsourceDelete.UseVisualStyleBackColor = true;
            this.btnOutsourceDelete.Click += new System.EventHandler(this.EventBtnOutsourceDelete_Click);
            // 
            // btnOutsourceImport
            // 
            this.btnOutsourceImport.Location = new System.Drawing.Point(396, 360);
            this.btnOutsourceImport.Name = "btnOutsourceImport";
            this.btnOutsourceImport.Size = new System.Drawing.Size(75, 23);
            this.btnOutsourceImport.TabIndex = 1;
            this.btnOutsourceImport.Text = "匯入";
            this.btnOutsourceImport.UseVisualStyleBackColor = true;
            this.btnOutsourceImport.Click += new System.EventHandler(this.EventBtnOutsourceImport_Click);
            // 
            // dataGridViewOutsourceSelected
            // 
            this.dataGridViewOutsourceSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOutsourceSelected.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewOutsourceSelected.Location = new System.Drawing.Point(438, 6);
            this.dataGridViewOutsourceSelected.Name = "dataGridViewOutsourceSelected";
            this.dataGridViewOutsourceSelected.Size = new System.Drawing.Size(419, 348);
            this.dataGridViewOutsourceSelected.TabIndex = 0;
            this.dataGridViewOutsourceSelected.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.EventDataGridViewSelected_CellMouseDoubleClick);
            // 
            // dataGridViewOutsourceAll
            // 
            this.dataGridViewOutsourceAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOutsourceAll.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewOutsourceAll.Name = "dataGridViewOutsourceAll";
            this.dataGridViewOutsourceAll.Size = new System.Drawing.Size(426, 348);
            this.dataGridViewOutsourceAll.TabIndex = 0;
            // 
            // tabPageTool
            // 
            this.tabPageTool.Controls.Add(this.dataGridViewToolAll);
            this.tabPageTool.Controls.Add(this.dataGridViewToolSelected);
            this.tabPageTool.Controls.Add(this.btnToolDelete);
            this.tabPageTool.Controls.Add(this.btnToolImport);
            this.tabPageTool.Location = new System.Drawing.Point(4, 22);
            this.tabPageTool.Name = "tabPageTool";
            this.tabPageTool.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTool.Size = new System.Drawing.Size(863, 389);
            this.tabPageTool.TabIndex = 3;
            this.tabPageTool.Text = "機具";
            this.tabPageTool.UseVisualStyleBackColor = true;
            // 
            // dataGridViewToolAll
            // 
            this.dataGridViewToolAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewToolAll.Location = new System.Drawing.Point(3, 6);
            this.dataGridViewToolAll.Name = "dataGridViewToolAll";
            this.dataGridViewToolAll.Size = new System.Drawing.Size(438, 348);
            this.dataGridViewToolAll.TabIndex = 1;
            // 
            // dataGridViewToolSelected
            // 
            this.dataGridViewToolSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewToolSelected.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewToolSelected.Location = new System.Drawing.Point(447, 6);
            this.dataGridViewToolSelected.Name = "dataGridViewToolSelected";
            this.dataGridViewToolSelected.Size = new System.Drawing.Size(410, 348);
            this.dataGridViewToolSelected.TabIndex = 1;
            this.dataGridViewToolSelected.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.EventDataGridViewSelected_CellMouseDoubleClick);
            // 
            // btnToolDelete
            // 
            this.btnToolDelete.Location = new System.Drawing.Point(670, 360);
            this.btnToolDelete.Name = "btnToolDelete";
            this.btnToolDelete.Size = new System.Drawing.Size(75, 23);
            this.btnToolDelete.TabIndex = 0;
            this.btnToolDelete.Text = "刪除";
            this.btnToolDelete.UseVisualStyleBackColor = true;
            this.btnToolDelete.Click += new System.EventHandler(this.EventBtnToolDelete_Click);
            // 
            // btnToolImport
            // 
            this.btnToolImport.Location = new System.Drawing.Point(406, 360);
            this.btnToolImport.Name = "btnToolImport";
            this.btnToolImport.Size = new System.Drawing.Size(75, 23);
            this.btnToolImport.TabIndex = 0;
            this.btnToolImport.Text = "匯入";
            this.btnToolImport.UseVisualStyleBackColor = true;
            this.btnToolImport.Click += new System.EventHandler(this.EventBtnToolImport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(16, 433);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "存檔";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.EventBtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(97, 433);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "結束";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.EventBtnCancel_Click);
            // 
            // CreateSubItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 470);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabClass);
            this.Name = "CreateSubItemForm";
            this.Text = "建立合約子項目";
            this.tabClass.ResumeLayout(false);
            this.tabPageMaterial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterialSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMaterialAll)).EndInit();
            this.tabPageLabor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLaborSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLaborAll)).EndInit();
            this.tabPageOutsource.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutsourceSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutsourceAll)).EndInit();
            this.tabPageTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToolAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToolSelected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabClass;
        private System.Windows.Forms.TabPage tabPageMaterial;
        private System.Windows.Forms.TabPage tabPageLabor;
        private System.Windows.Forms.TabPage tabPageOutsource;
        private System.Windows.Forms.TabPage tabPageTool;
        private System.Windows.Forms.Button btnMaterialDelete;
        private System.Windows.Forms.Button btnMaterialImport;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dataGridViewMaterialSelected;
        private System.Windows.Forms.DataGridView dataGridViewMaterialAll;
        private System.Windows.Forms.DataGridView dataGridViewLaborSelected;
        private System.Windows.Forms.DataGridView dataGridViewLaborAll;
        private System.Windows.Forms.Button btnLaborDelete;
        private System.Windows.Forms.Button btnLaborImport;
        private System.Windows.Forms.Button btnOutsourceDelete;
        private System.Windows.Forms.Button btnOutsourceImport;
        private System.Windows.Forms.DataGridView dataGridViewOutsourceSelected;
        private System.Windows.Forms.DataGridView dataGridViewOutsourceAll;
        private System.Windows.Forms.DataGridView dataGridViewToolAll;
        private System.Windows.Forms.DataGridView dataGridViewToolSelected;
        private System.Windows.Forms.Button btnToolDelete;
        private System.Windows.Forms.Button btnToolImport;
    }
}