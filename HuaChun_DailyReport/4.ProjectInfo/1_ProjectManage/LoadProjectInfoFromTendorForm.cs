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
    public partial class LoadProjectInfoFromTendorForm : SearchFormBase
    {
        public LoadProjectInfoFromTendorForm( ProjectIncreaseForm form, MySQL Sql )
            : base( Sql )
        {
            InitializeComponent();
            InitializeProjectInfoFromTendorForm();
            Initialize();
        }

        private void InitializeProjectInfoFromTendorForm()
        {
            this.Text = "搜尋標單";

            this.strRowNo = "材料編號";
            this.strRowName = "材料名稱";

            this.radioBtnNo.Text = "搜尋標單編號";
            this.radioBtnName.Text = "搜尋工程名稱";
        }

        protected override void Initialize()
        {

            //textBox2.Enabled = false;


            //arrNumbers = m_Sql.Read1DArrayNoCondition_SQL_Data( strDataBaseNumber, strDataBaseTableName );
            //arrNames = m_Sql.Read1DArrayNoCondition_SQL_Data( strDataBaseName, strDataBaseTableName );
            //units = m_Sql.Read1DArrayNoCondition_SQL_Data( "unit", strDataBaseTableName );//unit不是共通的  所以獨立出來寫

            //kDataTable = new DataTable( "MyNewTable" );
            //kDataTable.Columns.Add( strRowNo, typeof( String ) );
            //kDataTable.Columns.Add( strRowName, typeof( String ) );
            //kDataTable.Columns.Add( "單位", typeof( String ) );
            //dataGridView1.DataSource = kDataTable;
            //dataGridView1.ReadOnly = true;
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.MultiSelect = false;

            //DataRow dataRow;
            //m_Sql.OpenSqlChannel();
            //for ( int i = 0; i < arrNumbers.Length; i++ )
            //{
            //    dataRow = kDataTable.NewRow();
            //    dataRow [ strRowNo ] = arrNumbers [ i ];
            //    dataRow [ strRowName ] = m_Sql.ReadSqlDataWithoutOpenClose( strDataBaseName, strDataBaseTableName, strDataBaseNumber + " = '" + arrNumbers [ i ] + "'" );
            //    dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", strDataBaseTableName, strDataBaseNumber + " = '" + arrNumbers [ i ] + "'" );
            //    kDataTable.Rows.Add( dataRow );
            //}
            //m_Sql.CloseSqlChannel();
        }
    }
}
