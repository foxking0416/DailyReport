using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class LoginForm : Form
    {
        MySQL m_Sql;

        Main formMain;

        public LoginForm(Main form, MySQL Sql)
        {
            m_Sql = Sql;
            formMain = form;
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void EventLoginOk_Click(object sender, EventArgs e)
        {
            if (textBoxAccount.Text == string.Empty)
            {
                MessageBox.Show("請輸入使用者帳號", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBoxPassword.Text == string.Empty)
            {
                MessageBox.Show("請輸入使用者密碼", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string strMember = m_Sql.Read_SQL_data("name", "member", "account = '" + textBoxAccount.Text +"' AND password = '" + textBoxPassword.Text + "'");

                if (strMember == string.Empty)
                {
                    DialogResult kResult = MessageBox.Show("密碼與帳號不符", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (kResult == DialogResult.OK)
                    {
                        textBoxPassword.Text = string.Empty;
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(strMember + "您好", "歡迎", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        this.formMain.Login( 1 );
                        this.Close();
                    }
                }
            }
        }



        private void EventLoginCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
