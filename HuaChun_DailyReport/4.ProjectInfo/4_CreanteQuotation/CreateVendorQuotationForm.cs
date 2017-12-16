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




        public CreateVendorQuotationForm( string strProjectNo, MySQL Sql )
        {
            m_Sql = Sql;
            m_strProjectNumber = strProjectNo;
            InitializeComponent();


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


        private void EventBtnMaterialItemImport_Click( object sender, EventArgs e )
        {

        }

        private void EventBtnMaterialItemDelete_Click( object sender, EventArgs e )
        {

        }

        private void EventBtnMaterialVendorImport_Click( object sender, EventArgs e )
        {

        }

        private void EventBtnMaterialVendorDelete_Click( object sender, EventArgs e )
        {

        }






        private void EventBtnSave_Click( object sender, EventArgs e )
        {

        }

        private void EventBtnCancle_Click( object sender, EventArgs e )
        {

        }

        private void EventBtnLaborItemImport_Click( object sender, EventArgs e )
        {

        }

        private void EventBtnLaborItemDelete_Click( object sender, EventArgs e )
        {

        }

        private void EventBtnLaborVendorImport_Click( object sender, EventArgs e )
        {

        }

        private void EventBtnLaborVendorDelete_Click( object sender, EventArgs e )
        {

        }



    }
}
