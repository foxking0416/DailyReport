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
    public partial class VendorEditForm : VendorIncreaseForm
    {
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSearch;
        private string[] vendors;
        private int selectIndex = 0;
        private int vendorCount;

        public VendorEditForm() : base()
        {
        }

        public VendorEditForm(MySQL Sql) : base(Sql)
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            this.Text = "廠商編輯作業";
            //this.btnClear.Visible = false;
            this.btnClear.Text = "刪除";
            this.textBoxVendor_No.Enabled = false;
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();

            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(201, 17);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = "上一個";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(280, 17);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一個";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(360, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "搜尋";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.groupBox1.Controls.Add(this.btnLast);
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnSearch);

            vendors = m_Sql.Read1DArrayNoCondition_SQL_Data("vendor_no", "vendor");
            vendorCount = vendors.Length;
            if (vendorCount != 0)
                this.btnClear.Enabled = true;
            else
                this.btnClear.Enabled = false;
            LoadInformation(vendors[selectIndex]);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            selectIndex--;
            if (selectIndex < 0)
                selectIndex = vendorCount - 1;

            LoadInformation(vendors[selectIndex]);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            selectIndex++;
            if (selectIndex >= vendorCount)
                selectIndex = 0;

            LoadInformation(vendors[selectIndex]);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            VendorSearchForm searchform = new VendorSearchForm(this, m_Sql);
            searchform.Show();
        }

        public void LoadInformation(string vendor_no)
        {
            Cursor.Current = Cursors.WaitCursor;
            m_Sql.OpenSqlChannel();

            this.textBoxVendor_No.Text    = m_Sql.ReadSqlDataWithoutOpenClose("vendor_no", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxVendor_Name.Text  = m_Sql.ReadSqlDataWithoutOpenClose("vendor_name", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxVendor_Abbre.Text = m_Sql.ReadSqlDataWithoutOpenClose("vendor_abbre", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxContact1.Text     = m_Sql.ReadSqlDataWithoutOpenClose("contact1", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxPhone1.Text       = m_Sql.ReadSqlDataWithoutOpenClose("phone1", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxFax.Text          = m_Sql.ReadSqlDataWithoutOpenClose("fax", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxContact2.Text     = m_Sql.ReadSqlDataWithoutOpenClose("contact2", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxPhone2.Text       = m_Sql.ReadSqlDataWithoutOpenClose("phone2", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxCell.Text         = m_Sql.ReadSqlDataWithoutOpenClose("cell", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxEmail.Text        = m_Sql.ReadSqlDataWithoutOpenClose("email", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxCode2.Text        = m_Sql.ReadSqlDataWithoutOpenClose("code2", "vendor", "vendor_no = '" + vendor_no + "'");
            this.comboBoxCity.SelectedItem = m_Sql.ReadSqlDataWithoutOpenClose("address_city", "vendor", "vendor_no = '" + vendor_no + "'");
            this.comboBoxDistrict.SelectedItem = m_Sql.ReadSqlDataWithoutOpenClose("address_district", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxAddress.Text      = m_Sql.ReadSqlDataWithoutOpenClose("address_road", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxID.Text           = m_Sql.ReadSqlDataWithoutOpenClose("id", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxTaxTitle.Text     = m_Sql.ReadSqlDataWithoutOpenClose("taxtitle", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxBusinessItem.Text = m_Sql.ReadSqlDataWithoutOpenClose("businessitems", "vendor", "vendor_no = '" + vendor_no + "'");
            this.textBoxOther.Text        = m_Sql.ReadSqlDataWithoutOpenClose("other", "vendor", "vendor_no = '" + vendor_no + "'");

            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            label19.Visible = false;
            label20.Visible = false;

            if (textBoxVendor_Name.Text == string.Empty)
                label19.Visible = true;

            if (textBoxVendor_Name.Text == string.Empty)
                return;

            if (textBoxEmail.Text != string.Empty)
            {
                if (!textBoxEmail.Text.Contains("@"))
                {
                    label20.Visible = true;
                    return;
                }
            }

            DialogResult result = MessageBox.Show("確定要修改廠商資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                m_Sql.OpenSqlChannel();

                m_Sql.SetSqlDataWithoutOpenClose("vendor_name", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxVendor_Name.Text);
                m_Sql.SetSqlDataWithoutOpenClose("vendor_abbre", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxVendor_Abbre.Text);
                m_Sql.SetSqlDataWithoutOpenClose("contact1", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxContact1.Text);
                m_Sql.SetSqlDataWithoutOpenClose("phone1", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxPhone1.Text);
                m_Sql.SetSqlDataWithoutOpenClose("fax", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxFax.Text);
                m_Sql.SetSqlDataWithoutOpenClose("contact2", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxContact2.Text);
                m_Sql.SetSqlDataWithoutOpenClose("phone2", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxPhone2.Text);
                m_Sql.SetSqlDataWithoutOpenClose("cell", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxCell.Text);
                m_Sql.SetSqlDataWithoutOpenClose("email", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", "'" + this.textBoxEmail.Text + "'");
                m_Sql.SetSqlDataWithoutOpenClose("code2", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxCode2.Text);
                m_Sql.SetSqlDataWithoutOpenClose("address_city", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.comboBoxCity.SelectedItem.ToString());
                m_Sql.SetSqlDataWithoutOpenClose("address_district", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.comboBoxDistrict.SelectedItem.ToString());
                m_Sql.SetSqlDataWithoutOpenClose("address_road", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxAddress.Text);
                m_Sql.SetSqlDataWithoutOpenClose("id", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxID.Text);
                m_Sql.SetSqlDataWithoutOpenClose("taxtitle", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxTaxTitle.Text);
                m_Sql.SetSqlDataWithoutOpenClose("businessitems", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxBusinessItem.Text);
                m_Sql.SetSqlDataWithoutOpenClose("other", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'", this.textBoxOther.Text);

                m_Sql.CloseSqlChannel();
                Cursor.Current = Cursors.Default;
            }
            this.Close();
        }

        protected override void btnClear_Click(object sender, EventArgs e) 
        {
            DialogResult result = MessageBox.Show("確定要刪除廠商資料?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                m_Sql.NoHistoryDelete_SQL("vendor", "vendor_no = '" + this.textBoxVendor_No.Text + "'");

                vendors = m_Sql.Read1DArrayNoCondition_SQL_Data("vendor_no", "vendor");
                vendorCount = vendors.Length;
                --selectIndex;
                if (selectIndex >= 0)
                    LoadInformation(vendors[selectIndex]);
                else
                {
                    this.btnClear.Enabled = false;
                    selectIndex = 0;
                    textBoxVendor_No.Clear();
                    textBoxVendor_Name.Clear();
                    textBoxVendor_Abbre.Clear();
                    textBoxContact1.Clear();
                    textBoxContact2.Clear();
                    textBoxPhone1.Clear();
                    textBoxPhone2.Clear();
                    textBoxCell.Clear();
                    textBoxEmail.Clear();
                    textBoxFax.Clear();
                    textBoxCode2.Clear();
                    textBoxAddress.Clear();
                    textBoxID.Clear();
                    textBoxTaxTitle.Clear();
                    textBoxBusinessItem.Clear();
                    textBoxOther.Clear();
                }
            }
        }
    }
}
