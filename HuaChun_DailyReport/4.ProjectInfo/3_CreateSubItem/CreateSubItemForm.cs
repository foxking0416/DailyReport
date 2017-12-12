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
    public partial class CreateSubItemForm : Form
    {
        protected MySQL m_Sql;
        private DataTable dataTableMaterialAll;
        private DataTable dataTableMaterialSelected;
        private DataTable dataTableLaborAll;
        private DataTable dataTableLaborSelected;
        private DataTable dataTableToolAll;
        private DataTable dataTableToolSelected;
        private DataTable dataTableOutsourceAll;
        private DataTable dataTableOutsourceSelected;
        private ArrayList arrMaterialNumberAll = new ArrayList();
        private ArrayList arrMaterialNumberSelected = new ArrayList();
        private ArrayList arrLaborNumberAll = new ArrayList();
        private ArrayList arrLaborNumberSelected = new ArrayList();
        private ArrayList arrOutsourceNumberAll = new ArrayList();
        private ArrayList arrOutsourceNumberSelected = new ArrayList();
        private ArrayList arrToolNumberAll = new ArrayList();
        private ArrayList arrToolNumberSelected = new ArrayList();

        public CreateSubItemForm( MySQL Sql )
        {
            m_Sql = Sql;
            InitializeComponent();
            InitializeAllDataTable();
            RefreshMaterialAllList();
            RefreshLaborAllList();
            RefreshOutsourceAllList();
            RefreshToolAllList();

        }

        private void InitializeAllDataTable()
        {
            InitializeMaterialAllDataTable();
            InitializeMaterialSelectedDataTable();
            InitializeLaborAllDataTable();
            InitializeLaborSelectedDataTable();
            InitializeOutsourceAllDataTable();
            InitializeOutsourceSelectedDataTable();
            InitializeToolAllDataTable();
            InitializeToolSelectedDataTable();
        }

        private void InitializeMaterialAllDataTable()
        {
            dataTableMaterialAll = new DataTable( "MaterialAllTable" );
            dataTableMaterialAll.Columns.Add( "材料編號", typeof( String ) );
            dataTableMaterialAll.Columns.Add( "材料類別", typeof( String ) );
            dataTableMaterialAll.Columns.Add( "材料名稱", typeof( String ) );
            dataTableMaterialAll.Columns.Add( "單位", typeof( String ) );
            dataGridViewMaterialAll.DataSource = dataTableMaterialAll;
            dataGridViewMaterialAll.ReadOnly = true;
            dataGridViewMaterialAll.AllowUserToAddRows = false;
            dataGridViewMaterialAll.MultiSelect = true;
        }

        private void InitializeMaterialSelectedDataTable()
        {
            dataTableMaterialSelected = new DataTable( "MaterialSelectedTable" );
            dataTableMaterialSelected.Columns.Add( "材料編號", typeof( String ) );
            dataTableMaterialSelected.Columns.Add( "材料類別", typeof( String ) );
            dataTableMaterialSelected.Columns.Add( "材料名稱", typeof( String ) );
            dataTableMaterialSelected.Columns.Add( "單位", typeof( String ) );
            dataGridViewMaterialSelected.DataSource = dataTableMaterialSelected;
            dataGridViewMaterialSelected.ReadOnly = true;
            dataGridViewMaterialSelected.AllowUserToAddRows = false;
            dataGridViewMaterialSelected.MultiSelect = true;
        }

        private void InitializeOutsourceAllDataTable()
        {
            dataTableOutsourceAll = new DataTable( "OutsourceAllTable" );
            dataTableOutsourceAll.Columns.Add( "外包編號", typeof( String ) );
            dataTableOutsourceAll.Columns.Add( "外包類別", typeof( String ) );
            dataTableOutsourceAll.Columns.Add( "外包名稱", typeof( String ) );
            dataTableOutsourceAll.Columns.Add( "單位", typeof( String ) );
            dataGridViewOutsourceAll.DataSource = dataTableOutsourceAll;
            dataGridViewOutsourceAll.ReadOnly = true;
            dataGridViewOutsourceAll.AllowUserToAddRows = false;
            dataGridViewOutsourceAll.MultiSelect = true;
        }

        private void InitializeOutsourceSelectedDataTable()
        {
            dataTableOutsourceSelected = new DataTable( "OutsourceSelectedTable" );
            dataTableOutsourceSelected.Columns.Add( "外包編號", typeof( String ) );
            dataTableOutsourceSelected.Columns.Add( "外包類別", typeof( String ) );
            dataTableOutsourceSelected.Columns.Add( "外包名稱", typeof( String ) );
            dataTableOutsourceSelected.Columns.Add( "單位", typeof( String ) );
            dataGridViewOutsourceSelected.DataSource = dataTableOutsourceSelected;
            dataGridViewOutsourceSelected.ReadOnly = true;
            dataGridViewOutsourceSelected.AllowUserToAddRows = false;
            dataGridViewOutsourceSelected.MultiSelect = true;
        }

        private void InitializeLaborAllDataTable()
        {
            dataTableLaborAll = new DataTable( "LaborAllTable" );
            dataTableLaborAll.Columns.Add( "人工編號", typeof( String ) );
            dataTableLaborAll.Columns.Add( "人工類別", typeof( String ) );
            dataTableLaborAll.Columns.Add( "人工名稱", typeof( String ) );
            dataTableLaborAll.Columns.Add( "單位", typeof( String ) );
            dataGridViewLaborAll.DataSource = dataTableLaborAll;
            dataGridViewLaborAll.ReadOnly = true;
            dataGridViewLaborAll.AllowUserToAddRows = false;
            dataGridViewLaborAll.MultiSelect = true;
        }

        private void InitializeLaborSelectedDataTable()
        {
            dataTableLaborSelected = new DataTable( "LaborSelectedTable" );
            dataTableLaborSelected.Columns.Add( "人工編號", typeof( String ) );
            dataTableLaborSelected.Columns.Add( "人工類別", typeof( String ) );
            dataTableLaborSelected.Columns.Add( "人工名稱", typeof( String ) );
            dataTableLaborSelected.Columns.Add( "單位", typeof( String ) );
            dataGridViewLaborSelected.DataSource = dataTableLaborSelected;
            dataGridViewLaborSelected.ReadOnly = true;
            dataGridViewLaborSelected.AllowUserToAddRows = false;
            dataGridViewLaborSelected.MultiSelect = true;
        }

        private void InitializeToolAllDataTable()
        {
            dataTableToolAll = new DataTable( "ToolAllTable" );
            dataTableToolAll.Columns.Add( "機具編號", typeof( String ) );
            dataTableToolAll.Columns.Add( "機具類別", typeof( String ) );
            dataTableToolAll.Columns.Add( "機具名稱", typeof( String ) );
            dataTableToolAll.Columns.Add( "單位", typeof( String ) );
            dataGridViewToolAll.DataSource = dataTableToolAll;
            dataGridViewToolAll.ReadOnly = true;
            dataGridViewToolAll.AllowUserToAddRows = false;
            dataGridViewToolAll.MultiSelect = true;
        }

        private void InitializeToolSelectedDataTable()
        {
            dataTableToolSelected = new DataTable( "ToolSelectedTable" );
            dataTableToolSelected.Columns.Add( "機具編號", typeof( String ) );
            dataTableToolSelected.Columns.Add( "機具類別", typeof( String ) );
            dataTableToolSelected.Columns.Add( "機具名稱", typeof( String ) );
            dataTableToolSelected.Columns.Add( "單位", typeof( String ) );
            dataGridViewToolSelected.DataSource = dataTableToolSelected;
            dataGridViewToolSelected.ReadOnly = true;
            dataGridViewToolSelected.AllowUserToAddRows = false;
            dataGridViewToolSelected.MultiSelect = true;
        }

        private void RefreshMaterialAllList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTableMaterialAll.Clear();

            string [] arrNumbers = m_Sql.Read1DArrayNoCondition_SQL_Data( "number", "material" );
            Array.Sort( arrNumbers );
            arrMaterialNumberAll.AddRange( arrNumbers );
            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrNumbers.Length; i++ )
            {
                dataRow = dataTableMaterialAll.NewRow();
                dataRow [ "材料編號" ] = arrNumbers [ i ];
                dataRow [ "材料類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "material", "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ "材料名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "material", "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", "material", "number = '" + arrNumbers [ i ] + "'" );
                dataTableMaterialAll.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshMaterialSelectedList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTableMaterialSelected.Clear();
           
            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrMaterialNumberSelected.Count; i++ )
            {
                dataRow = dataTableMaterialSelected.NewRow();
                dataRow [ "材料編號" ] = arrMaterialNumberSelected [ i ];
                dataRow [ "材料類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "material", "number = '" + arrMaterialNumberSelected [ i ] + "'" );
                dataRow [ "材料名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "material", "number = '" + arrMaterialNumberSelected [ i ] + "'" );
                dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", "material", "number = '" + arrMaterialNumberSelected [ i ] + "'" );
                dataTableMaterialSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshLaborAllList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTableLaborAll.Clear();

            string [] arrNumbers = m_Sql.Read1DArrayNoCondition_SQL_Data( "number", "labor" );
            Array.Sort( arrNumbers );

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrNumbers.Length; i++ )
            {
                dataRow = dataTableLaborAll.NewRow();
                dataRow [ "人工編號" ] = arrNumbers [ i ];
                dataRow [ "人工類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "labor", "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ "人工名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "labor", "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", "labor", "number = '" + arrNumbers [ i ] + "'" );
                dataTableLaborAll.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshLaborSelectedList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTableLaborSelected.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrLaborNumberSelected.Count; i++ )
            {
                dataRow = dataTableLaborSelected.NewRow();
                dataRow [ "人工編號" ] = arrLaborNumberSelected [ i ];
                dataRow [ "人工類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "Labor", "number = '" + arrLaborNumberSelected [ i ] + "'" );
                dataRow [ "人工名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "Labor", "number = '" + arrLaborNumberSelected [ i ] + "'" );
                dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", "Labor", "number = '" + arrLaborNumberSelected [ i ] + "'" );
                dataTableLaborSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshToolAllList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTableToolAll.Clear();

            string [] arrNumbers = m_Sql.Read1DArrayNoCondition_SQL_Data( "number", "tool" );
            Array.Sort( arrNumbers );

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrNumbers.Length; i++ )
            {
                dataRow = dataTableToolAll.NewRow();
                dataRow [ "機具編號" ] = arrNumbers [ i ];
                dataRow [ "機具類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "tool", "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ "機具名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "tool", "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", "tool", "number = '" + arrNumbers [ i ] + "'" );
                dataTableToolAll.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshToolSelectedList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTableToolSelected.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrToolNumberSelected.Count; i++ )
            {
                dataRow = dataTableToolSelected.NewRow();
                dataRow [ "機具編號" ] = arrToolNumberSelected [ i ];
                dataRow [ "機具類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "Tool", "number = '" + arrToolNumberSelected [ i ] + "'" );
                dataRow [ "機具名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "Tool", "number = '" + arrToolNumberSelected [ i ] + "'" );
                dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", "Tool", "number = '" + arrToolNumberSelected [ i ] + "'" );
                dataTableToolSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshOutsourceAllList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTableOutsourceAll.Clear();

            string [] arrNumbers = m_Sql.Read1DArrayNoCondition_SQL_Data( "number", "outsource" );
            Array.Sort( arrNumbers );

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrNumbers.Length; i++ )
            {
                dataRow = dataTableOutsourceAll.NewRow();
                dataRow [ "外包編號" ] = arrNumbers [ i ];
                dataRow [ "外包類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "outsource", "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ "外包名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "outsource", "number = '" + arrNumbers [ i ] + "'" );
                dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", "outsource", "number = '" + arrNumbers [ i ] + "'" );
                dataTableOutsourceAll.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshOutsourceSelectedList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataTableOutsourceSelected.Clear();

            DataRow dataRow;
            m_Sql.OpenSqlChannel();
            for ( int i = 0; i < arrOutsourceNumberSelected.Count; i++ )
            {
                dataRow = dataTableOutsourceSelected.NewRow();
                dataRow [ "外包編號" ] = arrOutsourceNumberSelected [ i ];
                dataRow [ "外包類別" ] = m_Sql.ReadSqlDataWithoutOpenClose( "class", "Outsource", "number = '" + arrOutsourceNumberSelected [ i ] + "'" );
                dataRow [ "外包名稱" ] = m_Sql.ReadSqlDataWithoutOpenClose( "name", "Outsource", "number = '" + arrOutsourceNumberSelected [ i ] + "'" );
                dataRow [ "單位" ] = m_Sql.ReadSqlDataWithoutOpenClose( "unit", "Outsource", "number = '" + arrOutsourceNumberSelected [ i ] + "'" );
                dataTableOutsourceSelected.Rows.Add( dataRow );
            }
            m_Sql.CloseSqlChannel();
            Cursor.Current = Cursors.Default;
        }

        private void EventBtnSave_Click( object sender, EventArgs e )
        {

        }

        private void EventBtnMaterialImport_Click( object sender, EventArgs e )
        {
            string number = dataGridViewMaterialAll [ 0, dataGridViewMaterialAll.CurrentRow.Index ].Value.ToString();

            arrMaterialNumberSelected.Add( number );
            arrMaterialNumberSelected.Sort();
            RefreshMaterialSelectedList();
        }

        private void EventBtnMaterialDelete_Click( object sender, EventArgs e )
        {
            string number = dataGridViewMaterialSelected [ 0, dataGridViewMaterialSelected.CurrentRow.Index ].Value.ToString();

            arrMaterialNumberSelected.Remove( number );
            arrMaterialNumberSelected.Sort();
            RefreshMaterialSelectedList();
        }

        private void EventBtnLaborImport_Click( object sender, EventArgs e )
        {
            string number = dataGridViewLaborAll [ 0, dataGridViewLaborAll.CurrentRow.Index ].Value.ToString();

            arrLaborNumberSelected.Add( number );
            arrLaborNumberSelected.Sort();
            RefreshLaborSelectedList();
        }

        private void EventBtnLaborDelete_Click( object sender, EventArgs e )
        {
            string number = dataGridViewLaborSelected [ 0, dataGridViewLaborSelected.CurrentRow.Index ].Value.ToString();

            arrLaborNumberSelected.Remove( number );
            arrLaborNumberSelected.Sort();
            RefreshLaborSelectedList();
        }

        private void EventBtnOutsourceImport_Click( object sender, EventArgs e )
        {
            string number = dataGridViewOutsourceAll [ 0, dataGridViewOutsourceAll.CurrentRow.Index ].Value.ToString();

            arrOutsourceNumberSelected.Add( number );
            arrOutsourceNumberSelected.Sort();
            RefreshOutsourceSelectedList();
        }

        private void EventBtnOutsourceDelete_Click( object sender, EventArgs e )
        {
            string number = dataGridViewOutsourceSelected [ 0, dataGridViewOutsourceSelected.CurrentRow.Index ].Value.ToString();

            arrOutsourceNumberSelected.Remove( number );
            arrOutsourceNumberSelected.Sort();
            RefreshOutsourceSelectedList();
        }

        private void EventBtnToolImport_Click( object sender, EventArgs e )
        {
            string number = dataGridViewToolAll [ 0, dataGridViewToolAll.CurrentRow.Index ].Value.ToString();

            arrToolNumberSelected.Add( number );
            arrToolNumberSelected.Sort();
            RefreshToolSelectedList();
        }

        private void EventBtnToolDelete_Click( object sender, EventArgs e )
        {
            string number = dataGridViewToolSelected [ 0, dataGridViewToolSelected.CurrentRow.Index ].Value.ToString();

            arrToolNumberSelected.Remove( number );
            arrToolNumberSelected.Sort();
            RefreshToolSelectedList();
        }

        private void EventBtnCancel_Click( object sender, EventArgs e )
        {
            this.Close();
        }
    }
}
