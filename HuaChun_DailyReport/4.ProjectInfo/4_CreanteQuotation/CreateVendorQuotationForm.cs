using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace HuaChun_DailyReport
{
    public partial class CreateVendorQuotationForm : Form
    {
        private string m_strProjectNumber;
        private MySQL m_Sql;
        private DataTable DTMaterialItemSelected;
        private DataTable DTMaterialItemQuote;
        private DataTable DTMaterialVendorAll;
        private DataTable DTMaterialVendorSelected;

        private DataTable DTLaborItemSelected;
        private DataTable DTLaborItemQuote;
        private DataTable DTLaborVendorAll;
        private DataTable DTLaborVendorSelected;

        private DataTable DTOutsourceItemSelected;
        private DataTable DTOutsourceItemQuote;
        private DataTable DTOutsourceVendorAll;
        private DataTable DTOutsourceVendorSelected;

        private DataTable DTToolItemSelected;
        private DataTable DTToolItemQuote;
        private DataTable DTToolVendorAll;
        private DataTable DTToolVendorSelected;

        private ArrayList arrVendorAll = new ArrayList();

        private ArrayList arrMaterialItemSelected = new ArrayList();
        private ArrayList arrMaterialItemQuote = new ArrayList();
        private ArrayList arrMaterialVendorSelected = new ArrayList();

        private ArrayList arrLaborItemSelected = new ArrayList();
        private ArrayList arrLaborItemQuote = new ArrayList();
        private ArrayList arrLaborVendorSelected = new ArrayList();

        private ArrayList arrOutsourceItemSelected = new ArrayList();
        private ArrayList arrOutsourceItemQuote = new ArrayList();
        private ArrayList arrOutsourceVendorSelected = new ArrayList();

        private ArrayList arrToolItemSelected = new ArrayList();
        private ArrayList arrToolItemQuote = new ArrayList();
        private ArrayList arrToolVendorSelected = new ArrayList();


        public CreateVendorQuotationForm( string strProjectNo, MySQL Sql )
        {
            m_Sql = Sql;
            m_strProjectNumber = strProjectNo;
            

            InitializeComponent();
            labelProjectName.Text = m_Sql.Read_SQL_data( "project_name", "project_info", "project_no = '" + m_strProjectNumber + "'" );

            InitializeMaterialItemSelectedDataTable();
            InitializeMaterialItemQuoteDataTable();
            InitializeMaterialVendorAllDataTable();
            InitializeMaterialVendorSelectedDataTable();

            InitializeLaborItemSelectedDataTable();
            InitializeLaborItemQuoteDataTable();
            InitializeLaborVendorAllDataTable();
            InitializeLaborVendorSelectedDataTable();

            InitializeOutsourceItemSelectedDataTable();
            InitializeOutsourceItemQuoteDataTable();
            InitializeOutsourceVendorAllDataTable();
            InitializeOutsourceVendorSelectedDataTable();

            InitializeToolItemSelectedDataTable();
            InitializeToolItemQuoteDataTable();
            InitializeToolVendorAllDataTable();
            InitializeToolVendorSelectedDataTable();


            RefreshMaterialItemSelected();
            RefreshLaborItemSelected();
            RefreshOutsourceItemSelected();
            RefreshToolItemSelected();

            RefreshVendorAll();
        }

        #region Initialize Material
        private void InitializeMaterialItemSelectedDataTable()
        {
            DTMaterialItemSelected = new DataTable( "MaterialItemSelectedTable" );
            DTMaterialItemSelected.Columns.Add( "材料編號", typeof( String ) );
            DTMaterialItemSelected.Columns.Add( "材料類別", typeof( String ) );
            DTMaterialItemSelected.Columns.Add( "材料名稱", typeof( String ) );
            DTMaterialItemSelected.Columns.Add( "總量", typeof( String ) );
            DGVMaterialItemSelected.DataSource = DTMaterialItemSelected;
            DGVMaterialItemSelected.ReadOnly = true;
            DGVMaterialItemSelected.AllowUserToAddRows = false;
            DGVMaterialItemSelected.MultiSelect = true;
        }

        private void InitializeMaterialItemQuoteDataTable()
        {
            DTMaterialItemQuote = new DataTable( "MaterialItemQuoteTable" );
            DTMaterialItemQuote.Columns.Add( "材料編號", typeof( String ) );
            DTMaterialItemQuote.Columns.Add( "材料類別", typeof( String ) );
            DTMaterialItemQuote.Columns.Add( "材料名稱", typeof( String ) );
            DGVMaterialItemQuote.DataSource = DTMaterialItemQuote;
            DGVMaterialItemQuote.ReadOnly = true;
            DGVMaterialItemQuote.AllowUserToAddRows = false;
            DGVMaterialItemQuote.MultiSelect = true;
        }

        private void InitializeMaterialVendorAllDataTable()
        {
            DTMaterialVendorAll = new DataTable( "MaterialVendorAllTable" );
            DTMaterialVendorAll.Columns.Add( "廠商編號", typeof( String ) );
            DTMaterialVendorAll.Columns.Add( "廠商名稱", typeof( String ) );
            DGVMaterialVendorAll.DataSource = DTMaterialVendorAll;
            DGVMaterialVendorAll.ReadOnly = true;
            DGVMaterialVendorAll.AllowUserToAddRows = false;
            DGVMaterialVendorAll.MultiSelect = true;
        }

        private void InitializeMaterialVendorSelectedDataTable()
        {
            DTMaterialVendorSelected = new DataTable( "MaterialVendorSelectedTable" );
            DTMaterialVendorSelected.Columns.Add( "廠商編號", typeof( String ) );
            DTMaterialVendorSelected.Columns.Add( "廠商名稱", typeof( String ) );
            DGVMaterialVendorSelected.DataSource = DTMaterialVendorSelected;
            DGVMaterialVendorSelected.ReadOnly = true;
            DGVMaterialVendorSelected.AllowUserToAddRows = false;
            DGVMaterialVendorSelected.MultiSelect = true;
        }
        #endregion 

        #region Initialize Labor
        private void InitializeLaborItemSelectedDataTable()
        {
            DTLaborItemSelected = new DataTable( "LaborItemSelectedTable" );
            DTLaborItemSelected.Columns.Add( "人工編號", typeof( String ) );
            DTLaborItemSelected.Columns.Add( "人工類別", typeof( String ) );
            DTLaborItemSelected.Columns.Add( "人工名稱", typeof( String ) );
            DTLaborItemSelected.Columns.Add( "總量", typeof( String ) );
            DGVLaborItemSelected.DataSource = DTLaborItemSelected;
            DGVLaborItemSelected.ReadOnly = true;
            DGVLaborItemSelected.AllowUserToAddRows = false;
            DGVLaborItemSelected.MultiSelect = true;
        }

        private void InitializeLaborItemQuoteDataTable()
        {
            DTLaborItemQuote = new DataTable( "LaborItemQuoteTable" );
            DTLaborItemQuote.Columns.Add( "人工編號", typeof( String ) );
            DTLaborItemQuote.Columns.Add( "人工類別", typeof( String ) );
            DTLaborItemQuote.Columns.Add( "人工名稱", typeof( String ) );
            DGVLaborItemQuote.DataSource = DTLaborItemQuote;
            DGVLaborItemQuote.ReadOnly = true;
            DGVLaborItemQuote.AllowUserToAddRows = false;
            DGVLaborItemQuote.MultiSelect = true;
        }

        private void InitializeLaborVendorAllDataTable()
        {
            DTLaborVendorAll = new DataTable( "LaborVendorAllTable" );
            DTLaborVendorAll.Columns.Add( "廠商編號", typeof( String ) );
            DTLaborVendorAll.Columns.Add( "廠商名稱", typeof( String ) );
            DGVLaborVendorAll.DataSource = DTLaborVendorAll;
            DGVLaborVendorAll.ReadOnly = true;
            DGVLaborVendorAll.AllowUserToAddRows = false;
            DGVLaborVendorAll.MultiSelect = true;
        }

        private void InitializeLaborVendorSelectedDataTable()
        {
            DTLaborVendorSelected = new DataTable( "LaborVendorSelectedTable" );
            DTLaborVendorSelected.Columns.Add( "廠商編號", typeof( String ) );
            DTLaborVendorSelected.Columns.Add( "廠商名稱", typeof( String ) );
            DGVLaborVendorSelected.DataSource = DTLaborVendorSelected;
            DGVLaborVendorSelected.ReadOnly = true;
            DGVLaborVendorSelected.AllowUserToAddRows = false;
            DGVLaborVendorSelected.MultiSelect = true;
        }
        #endregion 

        #region Initialize Outsource
        private void InitializeOutsourceItemSelectedDataTable()
        {
            DTOutsourceItemSelected = new DataTable( "OutsourceItemSelectedTable" );
            DTOutsourceItemSelected.Columns.Add( "外包編號", typeof( String ) );
            DTOutsourceItemSelected.Columns.Add( "外包類別", typeof( String ) );
            DTOutsourceItemSelected.Columns.Add( "外包名稱", typeof( String ) );
            DTOutsourceItemSelected.Columns.Add( "總量", typeof( String ) );
            DGVOutsourceItemSelected.DataSource = DTOutsourceItemSelected;
            DGVOutsourceItemSelected.ReadOnly = true;
            DGVOutsourceItemSelected.AllowUserToAddRows = false;
            DGVOutsourceItemSelected.MultiSelect = true;
        }

        private void InitializeOutsourceItemQuoteDataTable()
        {
            DTOutsourceItemQuote = new DataTable( "OutsourceItemQuoteTable" );
            DTOutsourceItemQuote.Columns.Add( "外包編號", typeof( String ) );
            DTOutsourceItemQuote.Columns.Add( "外包類別", typeof( String ) );
            DTOutsourceItemQuote.Columns.Add( "外包名稱", typeof( String ) );
            DGVOutsourceItemQuote.DataSource = DTOutsourceItemQuote;
            DGVOutsourceItemQuote.ReadOnly = true;
            DGVOutsourceItemQuote.AllowUserToAddRows = false;
            DGVOutsourceItemQuote.MultiSelect = true;
        }

        private void InitializeOutsourceVendorAllDataTable()
        {
            DTOutsourceVendorAll = new DataTable( "OutsourceVendorAllTable" );
            DTOutsourceVendorAll.Columns.Add( "廠商編號", typeof( String ) );
            DTOutsourceVendorAll.Columns.Add( "廠商名稱", typeof( String ) );
            DGVOutsourceVendorAll.DataSource = DTOutsourceVendorAll;
            DGVOutsourceVendorAll.ReadOnly = true;
            DGVOutsourceVendorAll.AllowUserToAddRows = false;
            DGVOutsourceVendorAll.MultiSelect = true;
        }

        private void InitializeOutsourceVendorSelectedDataTable()
        {
            DTOutsourceVendorSelected = new DataTable( "OutsourceVendorSelectedTable" );
            DTOutsourceVendorSelected.Columns.Add( "廠商編號", typeof( String ) );
            DTOutsourceVendorSelected.Columns.Add( "廠商名稱", typeof( String ) );
            DGVOutsourceVendorSelected.DataSource = DTOutsourceVendorSelected;
            DGVOutsourceVendorSelected.ReadOnly = true;
            DGVOutsourceVendorSelected.AllowUserToAddRows = false;
            DGVOutsourceVendorSelected.MultiSelect = true;
        }
        #endregion 

        #region Initialize Tool
        private void InitializeToolItemSelectedDataTable()
        {
            DTToolItemSelected = new DataTable( "ToolItemSelectedTable" );
            DTToolItemSelected.Columns.Add( "機具編號", typeof( String ) );
            DTToolItemSelected.Columns.Add( "機具類別", typeof( String ) );
            DTToolItemSelected.Columns.Add( "機具名稱", typeof( String ) );
            DTToolItemSelected.Columns.Add( "總量", typeof( String ) );
            DGVToolItemSelected.DataSource = DTToolItemSelected;
            DGVToolItemSelected.ReadOnly = true;
            DGVToolItemSelected.AllowUserToAddRows = false;
            DGVToolItemSelected.MultiSelect = true;
        }

        private void InitializeToolItemQuoteDataTable()
        {
            DTToolItemQuote = new DataTable( "ToolItemQuoteTable" );
            DTToolItemQuote.Columns.Add( "機具編號", typeof( String ) );
            DTToolItemQuote.Columns.Add( "機具類別", typeof( String ) );
            DTToolItemQuote.Columns.Add( "機具名稱", typeof( String ) );
            DGVToolItemQuote.DataSource = DTToolItemQuote;
            DGVToolItemQuote.ReadOnly = true;
            DGVToolItemQuote.AllowUserToAddRows = false;
            DGVToolItemQuote.MultiSelect = true;
        }

        private void InitializeToolVendorAllDataTable()
        {
            DTToolVendorAll = new DataTable( "ToolVendorAllTable" );
            DTToolVendorAll.Columns.Add( "廠商編號", typeof( String ) );
            DTToolVendorAll.Columns.Add( "廠商名稱", typeof( String ) );
            DGVToolVendorAll.DataSource = DTToolVendorAll;
            DGVToolVendorAll.ReadOnly = true;
            DGVToolVendorAll.AllowUserToAddRows = false;
            DGVToolVendorAll.MultiSelect = true;
        }

        private void InitializeToolVendorSelectedDataTable()
        {
            DTToolVendorSelected = new DataTable( "ToolVendorSelectedTable" );
            DTToolVendorSelected.Columns.Add( "廠商編號", typeof( String ) );
            DTToolVendorSelected.Columns.Add( "廠商名稱", typeof( String ) );
            DGVToolVendorSelected.DataSource = DTToolVendorSelected;
            DGVToolVendorSelected.ReadOnly = true;
            DGVToolVendorSelected.AllowUserToAddRows = false;
            DGVToolVendorSelected.MultiSelect = true;
        }
        #endregion 

        private void RefreshVendorAll()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTMaterialVendorAll.Clear();
            DTLaborVendorAll.Clear();
            DTOutsourceVendorAll.Clear();
            DTToolVendorAll.Clear();

            string [] arrNumbers = m_Sql.Read1DArrayNoCondition_SQL_Data( "vendor_no", "vendor" );
            Array.Sort( arrNumbers );
            arrVendorAll.AddRange( arrNumbers );


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrVendorAll.Count; i++ )
            {
                dataRow = DTMaterialVendorAll.NewRow();
                dataRow [ "廠商編號" ] = arrVendorAll [ i ];
                dataRow [ "廠商名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "vendor_name", "vendor", "vendor_no = '" + arrVendorAll [ i ] + "'" );
                DTMaterialVendorAll.Rows.Add( dataRow );

                dataRow = DTLaborVendorAll.NewRow();
                dataRow [ "廠商編號" ] = arrVendorAll [ i ];
                dataRow [ "廠商名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "vendor_name", "vendor", "vendor_no = '" + arrVendorAll [ i ] + "'" );
                DTLaborVendorAll.Rows.Add( dataRow );

                dataRow = DTOutsourceVendorAll.NewRow();
                dataRow [ "廠商編號" ] = arrVendorAll [ i ];
                dataRow [ "廠商名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "vendor_name", "vendor", "vendor_no = '" + arrVendorAll [ i ] + "'" );
                DTOutsourceVendorAll.Rows.Add( dataRow );

                dataRow = DTToolVendorAll.NewRow();
                dataRow [ "廠商編號" ] = arrVendorAll [ i ];
                dataRow [ "廠商名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "vendor_name", "vendor", "vendor_no = '" + arrVendorAll [ i ] + "'" );
                DTToolVendorAll.Rows.Add( dataRow );

            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }


        #region Refresh Material
        private void RefreshMaterialItemSelected()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTMaterialItemSelected.Clear();

            string [] arrNumbers = m_Sql.Read1DArray_SQL_Data( "number", "project_material_contract_used", "project_no = '" + m_strProjectNumber + "' AND already_quote = 0" );
            Array.Sort( arrNumbers );
            arrMaterialItemSelected.AddRange( arrNumbers );


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrMaterialItemSelected.Count; i++ )
            {
                dataRow = DTMaterialItemSelected.NewRow();
                dataRow [ "材料編號" ] = arrMaterialItemSelected [ i ];
                dataRow [ "材料類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "project_material_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrMaterialItemSelected [ i ] + "'" );
                dataRow [ "材料名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "project_material_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrMaterialItemSelected [ i ] + "'" );
                dataRow [ "總量" ] = m_Sql.ReadSqlDataWithoutOpenClose( "quantity", "project_material_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrMaterialItemSelected [ i ] + "'" );
                DTMaterialItemSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshMaterialItemQuoteList()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTMaterialItemQuote.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrMaterialItemQuote.Count; i++ )
            {
                dataRow = DTMaterialItemQuote.NewRow();
                dataRow [ "材料編號" ] = arrMaterialItemQuote [ i ];
                dataRow [ "材料類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "project_material_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrMaterialItemQuote [ i ] + "'" );
                dataRow [ "材料名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "project_material_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrMaterialItemQuote [ i ] + "'" );
                DTMaterialItemQuote.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshMaterialVendorSelectedList()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTMaterialVendorSelected.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrMaterialVendorSelected.Count; i++ )
            {
                dataRow = DTMaterialVendorSelected.NewRow();
                dataRow [ "廠商編號" ] = arrMaterialVendorSelected [ i ];
                dataRow [ "廠商名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "vendor_name", "vendor", "vendor_no = '" + arrMaterialVendorSelected [ i ] + "'" );
                DTMaterialVendorSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Refresh Labor
        private void RefreshLaborItemSelected()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTLaborItemSelected.Clear();

            string [] arrNumbers = m_Sql.Read1DArray_SQL_Data( "number", "project_labor_contract_used", "project_no = '" + m_strProjectNumber + "' AND already_quote = 0" );
            Array.Sort( arrNumbers );
            arrLaborItemSelected.AddRange( arrNumbers );


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrLaborItemSelected.Count; i++ )
            {
                dataRow = DTLaborItemSelected.NewRow();
                dataRow [ "人工編號" ] = arrLaborItemSelected [ i ];
                dataRow [ "人工類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "project_labor_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrLaborItemSelected [ i ] + "'" );
                dataRow [ "人工名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "project_labor_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrLaborItemSelected [ i ] + "'" );
                dataRow [ "總量" ] = m_Sql.ReadSqlDataWithoutOpenClose( "quantity", "project_labor_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrLaborItemSelected [ i ] + "'" );
                DTLaborItemSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshLaborItemQuoteList()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTLaborItemQuote.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrLaborItemQuote.Count; i++ )
            {
                dataRow = DTLaborItemQuote.NewRow();
                dataRow [ "人工編號" ] = arrLaborItemQuote [ i ];
                dataRow [ "人工類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "project_labor_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrLaborItemQuote [ i ] + "'" );
                dataRow [ "人工名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "project_labor_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrLaborItemQuote [ i ] + "'" );
                DTLaborItemQuote.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshLaborVendorSelectedList()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTLaborVendorSelected.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrLaborVendorSelected.Count; i++ )
            {
                dataRow = DTLaborVendorSelected.NewRow();
                dataRow [ "廠商編號" ] = arrLaborVendorSelected [ i ];
                dataRow [ "廠商名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "vendor_name", "vendor", "vendor_no = '" + arrLaborVendorSelected [ i ] + "'" );
                DTLaborVendorSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }
        #endregion 

        #region Refresh Outsource
        private void RefreshOutsourceItemSelected()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTOutsourceItemSelected.Clear();

            string [] arrNumbers = m_Sql.Read1DArray_SQL_Data( "number", "project_outsource_contract_used", "project_no = '" + m_strProjectNumber + "' AND already_quote = 0" );
            Array.Sort( arrNumbers );
            arrOutsourceItemSelected.AddRange( arrNumbers );


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrOutsourceItemSelected.Count; i++ )
            {
                dataRow = DTOutsourceItemSelected.NewRow();
                dataRow [ "外包編號" ] = arrOutsourceItemSelected [ i ];
                dataRow [ "外包類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "project_outsource_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrOutsourceItemSelected [ i ] + "'" );
                dataRow [ "外包名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "project_outsource_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrOutsourceItemSelected [ i ] + "'" );
                dataRow [ "總量" ] = m_Sql.ReadSqlDataWithoutOpenClose( "quantity", "project_outsource_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrOutsourceItemSelected [ i ] + "'" );
                DTOutsourceItemSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshOutsourceItemQuoteList()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTOutsourceItemQuote.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrOutsourceItemQuote.Count; i++ )
            {
                dataRow = DTOutsourceItemQuote.NewRow();
                dataRow [ "外包編號" ] = arrOutsourceItemQuote [ i ];
                dataRow [ "外包類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "project_outsource_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrOutsourceItemQuote [ i ] + "'" );
                dataRow [ "外包名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "project_outsource_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrOutsourceItemQuote [ i ] + "'" );
                DTOutsourceItemQuote.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshOutsourceVendorSelectedList()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTOutsourceVendorSelected.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrOutsourceVendorSelected.Count; i++ )
            {
                dataRow = DTOutsourceVendorSelected.NewRow();
                dataRow [ "廠商編號" ] = arrOutsourceVendorSelected [ i ];
                dataRow [ "廠商名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "vendor_name", "vendor", "vendor_no = '" + arrOutsourceVendorSelected [ i ] + "'" );
                DTOutsourceVendorSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Refresh Tool
        private void RefreshToolItemSelected()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTToolItemSelected.Clear();

            string [] arrNumbers = m_Sql.Read1DArray_SQL_Data( "number", "project_tool_contract_used", "project_no = '" + m_strProjectNumber + "' AND already_quote = 0" );
            Array.Sort( arrNumbers );
            arrToolItemSelected.AddRange( arrNumbers );


            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrToolItemSelected.Count; i++ )
            {
                dataRow = DTToolItemSelected.NewRow();
                dataRow [ "機具編號" ] = arrToolItemSelected [ i ];
                dataRow [ "機具類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "project_tool_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrToolItemSelected [ i ] + "'" );
                dataRow [ "機具名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "project_tool_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrToolItemSelected [ i ] + "'" );
                dataRow [ "總量" ] = m_Sql.ReadSqlDataWithoutOpenClose( "quantity", "project_tool_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrToolItemSelected [ i ] + "'" );
                DTToolItemSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshToolItemQuoteList()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTToolItemQuote.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrToolItemQuote.Count; i++ )
            {
                dataRow = DTToolItemQuote.NewRow();
                dataRow [ "機具編號" ] = arrToolItemQuote [ i ];
                dataRow [ "機具類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "project_tool_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrToolItemQuote [ i ] + "'" );
                dataRow [ "機具名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "project_tool_contract_used", "project_no = '" + m_strProjectNumber + "' AND number = '" + arrToolItemQuote [ i ] + "'" );
                DTToolItemQuote.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshToolVendorSelectedList()
        {
            Cursor.Current = Cursors.WaitCursor;
            DTToolVendorSelected.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrToolVendorSelected.Count; i++ )
            {
                dataRow = DTToolVendorSelected.NewRow();
                dataRow [ "廠商編號" ] = arrToolVendorSelected [ i ];
                dataRow [ "廠商名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "vendor_name", "vendor", "vendor_no = '" + arrToolVendorSelected [ i ] + "'" );
                DTToolVendorSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Material Event
        private void EventBtnlItemImport_Click( object sender, EventArgs e )
        {
            if ( 0 == tabClass.SelectedIndex )//material
            {
                if ( DGVMaterialItemSelected.CurrentRow != null )
                {
                    string number = DGVMaterialItemSelected [ 0, DGVMaterialItemSelected.CurrentRow.Index ].Value.ToString();

                    if ( !arrMaterialItemQuote.Contains( number ) )
                    {
                        arrMaterialItemQuote.Add( number );
                        arrMaterialItemQuote.Sort();
                        RefreshMaterialItemQuoteList();
                    }
                }
            }
            else if ( 1 == tabClass.SelectedIndex )//labor
            {
                if ( DGVLaborItemSelected.CurrentRow != null )
                {
                    string number = DGVLaborItemSelected [ 0, DGVLaborItemSelected.CurrentRow.Index ].Value.ToString();

                    if ( !arrLaborItemQuote.Contains( number ) )
                    {
                        arrLaborItemQuote.Add( number );
                        arrLaborItemQuote.Sort();
                        RefreshLaborItemQuoteList();
                    }
                }
            }
            else if ( 2 == tabClass.SelectedIndex )//outsource
            {
                if ( DGVOutsourceItemSelected.CurrentRow != null )
                {
                    string number = DGVOutsourceItemSelected [ 0, DGVOutsourceItemSelected.CurrentRow.Index ].Value.ToString();

                    if ( !arrOutsourceItemQuote.Contains( number ) )
                    {
                        arrOutsourceItemQuote.Add( number );
                        arrOutsourceItemQuote.Sort();
                        RefreshOutsourceItemQuoteList();
                    }
                }
            }
            else if ( 3 == tabClass.SelectedIndex )//tool
            {
                if ( DGVToolItemSelected.CurrentRow != null )
                {
                    string number = DGVToolItemSelected [ 0, DGVToolItemSelected.CurrentRow.Index ].Value.ToString();

                    if ( !arrToolItemQuote.Contains( number ) )
                    {
                        arrToolItemQuote.Add( number );
                        arrToolItemQuote.Sort();
                        RefreshToolItemQuoteList();
                    }
                }
            }



        }

        private void EventBtnItemDelete_Click( object sender, EventArgs e )
        {
            if ( DGVMaterialItemQuote.CurrentRow != null )
            {
                string number = DGVMaterialItemQuote [ 0, DGVMaterialItemQuote.CurrentRow.Index ].Value.ToString();

                arrMaterialItemQuote.Remove( number );
                arrMaterialItemQuote.Sort();
                RefreshMaterialItemQuoteList();
            }
        }

        private void EventBtnVendorImport_Click( object sender, EventArgs e )
        {
            if ( DGVMaterialVendorAll.CurrentRow != null )
            {
                string number = DGVMaterialVendorAll [ 0, DGVMaterialVendorAll.CurrentRow.Index ].Value.ToString();

                if ( !arrMaterialVendorSelected.Contains( number ) )
                {
                    arrMaterialVendorSelected.Add( number );
                    arrMaterialVendorSelected.Sort();
                    RefreshMaterialVendorSelectedList();
                }
            }
        }

        private void EventBtnVendorDelete_Click( object sender, EventArgs e )
        {
            if ( DGVMaterialVendorSelected.CurrentRow != null )
            {
                string number = DGVMaterialVendorSelected [ 0, DGVMaterialVendorSelected.CurrentRow.Index ].Value.ToString();

                arrMaterialVendorSelected.Remove( number );
                arrMaterialVendorSelected.Sort();
                RefreshMaterialVendorSelectedList();
            }
        }
        #endregion


        private void EventBtnSave_Click( object sender, EventArgs e )
        {
            labelNoEmptyWarning.Visible = false;
            labelNameEmptyWarning.Visible = false;
            labelMaterialItemEmptyWarning.Visible = false;
            labelMaterialVendorEmptyWarning.Visible = false;
            labelLaborItemEmptyWarning.Visible = false;
            labelLaborVendorEmptyWarning.Visible = false;
            labelOutsourceItemEmptyWarning.Visible = false;
            labelOutsourceVendorEmptyWarning.Visible = false;
            labelToolItemEmptyWarning.Visible = false;
            labelToolVendorEmptyWarning.Visible = false;

            if ( textBoxQuoteNo.Text == string.Empty )
            {
                labelNoEmptyWarning.Visible = true;
                return;
            }

            if ( textBoxQuoteName.Text == string.Empty )
            {
                labelNameEmptyWarning.Visible = true;
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            //m_Sql.NoHistoryDelete_SQL( "project_material_contract_used", "project_no = '" + m_strProjectNumber + "'" );
            //m_Sql.NoHistoryDelete_SQL( "project_labor_contract_used", "project_no = '" + m_strProjectNumber + "'" );
            //m_Sql.NoHistoryDelete_SQL( "project_tool_contract_used", "project_no = '" + m_strProjectNumber + "'" );
            //m_Sql.NoHistoryDelete_SQL( "project_outsource_contract_used", "project_no = '" + m_strProjectNumber + "'" );

            m_Sql.OpenSqlChannel();





            string commandStr = "";
            if ( tabClass.SelectedIndex == 0 )//material
            {
                #region material
                if ( arrMaterialItemQuote.Count == 0 )
                {
                    labelMaterialItemEmptyWarning.Visible = true;
                    return; 
                }
                if ( arrMaterialVendorSelected.Count == 0 )
                {
                    labelMaterialVendorEmptyWarning.Visible = true;
                    return;
                }

                string strMaterialNumberCombination = "";
                string strMaterialVendorCombination = "";
                for ( int i = 0; i < arrMaterialItemQuote.Count; i++ )
                {
                    strMaterialNumberCombination += arrMaterialItemQuote [ i ];

                    if ( i != arrMaterialItemQuote.Count - 1 )
                    {
                        strMaterialNumberCombination += "/";
                    }
                }

                for ( int i = 0; i < arrMaterialVendorSelected.Count; i++ )
                {
                    strMaterialVendorCombination += arrMaterialVendorSelected [ i ];

                    if ( i != arrMaterialVendorSelected.Count - 1 )
                    {
                        strMaterialVendorCombination += "/";
                    }
                }

                commandStr += "Insert into project_material_quotation(";
                commandStr += "project_no,";
                commandStr += "quote_no,";
                commandStr += "quote_desc,";
                commandStr += "material_no,";
                commandStr += "vendor_no";
                commandStr += ") values('";
                commandStr += m_strProjectNumber + "','";
                commandStr += textBoxQuoteNo.Text + "','";
                commandStr += textBoxQuoteName.Text + "','";
                commandStr += strMaterialNumberCombination + "','";
                commandStr += strMaterialVendorCombination;
                commandStr += "')";

                #endregion
            }
            else if ( tabClass.SelectedIndex == 1 )//labor
            {
                #region labor
                if ( 0 == arrLaborItemQuote.Count )
                {
                    labelLaborItemEmptyWarning.Visible = true;
                    return;
                }
                if ( 0 == arrLaborVendorSelected.Count )
                {
                    labelLaborVendorEmptyWarning.Visible = true;
                    return;
                }

                string strLaborNumberCombination = "";
                string strLaborVendorCombination = "";
                for ( int i = 0; i < arrLaborItemQuote.Count; i++ )
                {
                    strLaborNumberCombination += arrLaborItemQuote [ i ];

                    if ( i != arrLaborItemQuote.Count - 1 )
                    {
                        strLaborNumberCombination += "/";
                    }
                }

                for ( int i = 0; i < arrLaborVendorSelected.Count; i++ )
                {
                    strLaborVendorCombination += arrLaborVendorSelected [ i ];

                    if ( i != arrLaborVendorSelected.Count - 1 )
                    {
                        strLaborVendorCombination += "/";
                    }
                }

                commandStr += "Insert into project_labor_quotation(";
                commandStr += "project_no,";
                commandStr += "quote_no,";
                commandStr += "quote_desc,";
                commandStr += "labor_no,";
                commandStr += "vendor_no";
                commandStr += ") values('";
                commandStr += m_strProjectNumber + "','";
                commandStr += textBoxQuoteNo.Text + "','";
                commandStr += textBoxQuoteName.Text + "','";
                commandStr += strLaborNumberCombination + "','";
                commandStr += strLaborVendorCombination;
                commandStr += "')";

                #endregion 
            }
            else if ( tabClass.SelectedIndex == 2 )//outsource
            {
                #region outsource
                if ( 0 == arrOutsourceItemQuote.Count )
                {
                    labelOutsourceItemEmptyWarning.Visible = true;
                    return;
                }
                if ( 0 == arrOutsourceVendorSelected.Count )
                {
                    labelOutsourceVendorEmptyWarning.Visible = true;
                    return;
                }

                string strOutsourceNumberCombination = "";
                string strOutsourceVendorCombination = "";
                for ( int i = 0; i < arrOutsourceItemQuote.Count; i++ )
                {
                    strOutsourceNumberCombination += arrOutsourceItemQuote [ i ];

                    if ( i != arrOutsourceItemQuote.Count - 1 )
                    {
                        strOutsourceNumberCombination += "/";
                    }
                }

                for ( int i = 0; i < arrOutsourceVendorSelected.Count; i++ )
                {
                    strOutsourceVendorCombination += arrOutsourceVendorSelected [ i ];

                    if ( i != arrOutsourceVendorSelected.Count - 1 )
                    {
                        strOutsourceVendorCombination += "/";
                    }
                }

                commandStr += "Insert into project_outsource_quotation(";
                commandStr += "project_no,";
                commandStr += "quote_no,";
                commandStr += "quote_desc,";
                commandStr += "outsource_no,";
                commandStr += "vendor_no";
                commandStr += ") values('";
                commandStr += m_strProjectNumber + "','";
                commandStr += textBoxQuoteNo.Text + "','";
                commandStr += textBoxQuoteName.Text + "','";
                commandStr += strOutsourceNumberCombination + "','";
                commandStr += strOutsourceVendorCombination;
                commandStr += "')";

                #endregion
            }
            else if ( tabClass.SelectedIndex == 3 )//tool
            {
                #region tool
                if ( 0 == arrToolItemQuote.Count )
                {
                    labelToolItemEmptyWarning.Visible = true;
                    return;
                }
                if ( 0 == arrToolVendorSelected.Count )
                {
                    labelToolVendorEmptyWarning.Visible = true;
                    return;
                }

                string strToolNumberCombination = "";
                string strToolVendorCombination = "";
                for ( int i = 0; i < arrToolItemQuote.Count; i++ )
                {
                    strToolNumberCombination += arrToolItemQuote [ i ];

                    if ( i != arrToolItemQuote.Count - 1 )
                    {
                        strToolNumberCombination += "/";
                    }
                }

                for ( int i = 0; i < arrToolVendorSelected.Count; i++ )
                {
                    strToolVendorCombination += arrToolVendorSelected [ i ];

                    if ( i != arrToolVendorSelected.Count - 1 )
                    {
                        strToolVendorCombination += "/";
                    }
                }

                commandStr += "Insert into project_tool_quotation(";
                commandStr += "project_no,";
                commandStr += "quote_no,";
                commandStr += "quote_desc,";
                commandStr += "tool_no,";
                commandStr += "vendor_no";
                commandStr += ") values('";
                commandStr += m_strProjectNumber + "','";
                commandStr += textBoxQuoteNo.Text + "','";
                commandStr += textBoxQuoteName.Text + "','";
                commandStr += strToolNumberCombination + "','";
                commandStr += strToolVendorCombination;
                commandStr += "')";

                #endregion
            }


            m_Sql.ExecuteNonQueryCommand( commandStr );
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;

            this.Close();
        }

        private void EventBtnCancle_Click( object sender, EventArgs e )
        {
            this.Close();
        }

    }
}
