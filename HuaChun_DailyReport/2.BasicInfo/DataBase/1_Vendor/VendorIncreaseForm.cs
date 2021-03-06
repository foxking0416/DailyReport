﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MySql.Data.MySqlClient;

namespace HuaChun_DailyReport
{
    public partial class VendorIncreaseForm : Form
    {

        protected MySQL m_Sql;
        ArrayList arrayCity;

        public VendorIncreaseForm()
        {
        }

        public VendorIncreaseForm(MySQL Sql)
        {
            m_Sql = Sql;
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            arrayCity = new ArrayList();

            string[] cities = m_Sql.Read1DArrayNoCondition_SQL_Data("distinct city", "city");

            for (int i = 0; i < cities.Length; i++)
            {
                comboBoxCity.Items.Add(cities[i]);
            }
            comboBoxCity.SelectedIndex = 0;
        }


        protected void InsertIntoDB()
        {
            Cursor.Current = Cursors.WaitCursor;
            m_Sql.OpenSqlChannel();

            string commandStr = "Insert into vendor(";
            commandStr = commandStr + "vendor_no,";
            commandStr = commandStr + "vendor_name,";
            commandStr = commandStr + "vendor_abbre,";
            commandStr = commandStr + "contact1,";
            commandStr = commandStr + "contact2,";
            commandStr = commandStr + "phone1,";
            commandStr = commandStr + "phone2,";
            commandStr = commandStr + "cell,";
            commandStr = commandStr + "email,";
            commandStr = commandStr + "fax,";
            commandStr = commandStr + "code3,";
            commandStr = commandStr + "code2,";
            commandStr = commandStr + "address_city,";
            commandStr = commandStr + "address_district,";
            commandStr = commandStr + "address_road,";
            commandStr = commandStr + "id,";
            commandStr = commandStr + "taxtitle,";
            commandStr = commandStr + "businessitems";
            commandStr = commandStr + ") values('";
            commandStr = commandStr + textBoxVendor_No.Text + "','";
            commandStr = commandStr + textBoxVendor_Name.Text + "','";
            commandStr = commandStr + textBoxVendor_Abbre.Text + "','";
            commandStr = commandStr + textBoxContact1.Text + "','";
            commandStr = commandStr + textBoxContact2.Text + "','";
            commandStr = commandStr + textBoxPhone1.Text + "','";
            commandStr = commandStr + textBoxPhone2.Text + "','";
            commandStr = commandStr + textBoxCell.Text + "','";
            commandStr = commandStr + textBoxEmail.Text + "','";
            commandStr = commandStr + textBoxFax.Text + "','";
            commandStr = commandStr + textBoxCode3.Text + "','";
            commandStr = commandStr + textBoxCode2.Text + "','";
            commandStr = commandStr + comboBoxCity.SelectedItem + "','";
            commandStr = commandStr + comboBoxDistrict.SelectedItem + "','";
            commandStr = commandStr + textBoxAddress.Text + "','";
            commandStr = commandStr + textBoxID.Text + "','";
            commandStr = commandStr + textBoxTaxTitle.Text + "','";
            commandStr = commandStr + textBoxBusinessItem.Text;
            commandStr = commandStr + "')";


            m_Sql.ExecuteNonQueryCommand(commandStr);
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void Clear()
        {
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

        ////////////////////////
        //Event Handler
        ////////////////////////
        protected virtual void btnSave_Click(object sender, EventArgs e)
        {

            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;

            if (textBoxVendor_No.Text == string.Empty)
            {
                label18.Text = "廠商編號不可為空白";
                label18.Visible = true;
            }


            if (textBoxVendor_Name.Text == string.Empty)
                label19.Visible = true;
                
            if (textBoxVendor_No.Text == string.Empty)
                return;
            if (textBoxVendor_Name.Text == string.Empty)
                return;

            string[] sameNo = m_Sql.Read1DArray_SQL_Data("vendor_no", "vendor", "vendor_no = '" + textBoxVendor_No.Text + "'");
            if (sameNo.Length != 0)
            {
                label18.Text = "已存在相同廠商編號";
                label18.Visible = true;
                return;
            }

            if (textBoxEmail.Text != string.Empty)
            { 
                if(!textBoxEmail.Text.Contains("@"))
                {
                    label20.Visible = true;
                    return;
                }
            }

            InsertIntoDB();
            Clear();
        }

        protected virtual void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected virtual void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] districts = m_Sql.Read1DArray_SQL_Data("district", "city", "city = '" + comboBoxCity.SelectedItem + "'");
            comboBoxDistrict.Items.Clear();
            for(int i = 0; i < districts.Length; i++)
            {
                comboBoxDistrict.Items.Add(districts[i]);
            }
            comboBoxDistrict.SelectedIndex = 0;
        }

        private void comboBoxDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string code = m_Sql.Read_SQL_data("code", "city", "city = '" + comboBoxCity.SelectedItem + "' AND district = '" + comboBoxDistrict.SelectedItem + "'");
            textBoxCode3.Text = code;
        }


    }
}
