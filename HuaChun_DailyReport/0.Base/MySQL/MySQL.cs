using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace HuaChun_DailyReport
{
    public class MySQL
    {
        static int iOpenCount = 0;
        MySqlConnection conn;
        public MySQL()
        {
            //實際華春用
            //string dbHost = AppSetting.LoadInitialSetting("DB_IP", "127.0.0.0");
            //string dbUser = AppSetting.LoadInitialSetting("DB_USER", "root");
            //string dbPass = AppSetting.LoadInitialSetting("DB_PASSWORD", "123");
            //string dbName = AppSetting.LoadInitialSetting("DB_NAME", "huachun");
            //string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";database=huachun;Charset = utf8";

            //local測試用
            string dbHost = "127.0.0.1";
            string dbUser = "root";
            string dbPass = "chichi1219" ;
            string dbName = "huachunsystem";
            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";Charset = utf8";
            
            conn = new MySqlConnection(connStr);
        }


        //===================================Basic function=============================================
        public void OpenSqlChannel()
        {
            try
            {
                if (iOpenCount == 0)
                {
                    conn.Open();
                }
                iOpenCount++;
            }
            catch
            {
            }
        }

        public void CloseSqlChannel()
        {
            try
            {
                if (iOpenCount == 1)
                {
                    conn.Close();
                }
                iOpenCount--;
            }
            catch
            {
            }
        }

        public void ExecuteNonQueryCommand(string strCommand)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = strCommand;
            cmd.ExecuteNonQuery();
        }

        public void SetSqlDataWithoutOpenClose(string Value_Name, string table, string condition, string New_Value)
        {
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE " + table + " SET " + Value_Name + " = " + "'" + New_Value + "'" + " WHERE " + condition;
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
        }

        public string ReadSqlDataWithoutOpenClose(string Value_Name, string table, string condition)
        {
            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT " + Value_Name + " FROM " + table + " WHERE " + condition;
                object ExeScalar = cmd.ExecuteScalar();
                return ExeScalar.ToString();
            }
            catch
            {
                return "";
            }
        }

        public void Set_SQL_data(string Value_Name, string table, string condition, string New_Value)
        {
            try
            {
                OpenSqlChannel();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE " + table + " SET " + Value_Name + " = " + "'" + New_Value + "'" + " WHERE " + condition;
                cmd.ExecuteNonQuery();
                CloseSqlChannel();
            }
            catch
            {
            }
        }
        public string Read_SQL_data(string Value_Name, string table, string condition)
        {
            try
            {
                OpenSqlChannel();
                string CmdText = "SELECT " + Value_Name + " FROM " + table + " WHERE " + condition;
                MySqlCommand cmd = new MySqlCommand(CmdText, conn);
                object ExeScalar = cmd.ExecuteScalar();
                CloseSqlChannel();
                return ExeScalar.ToString();
            }
            catch
            {
                return "";
            }
        }
        public void NoHistoryDelete_SQL(string table, string condition)
        {
            OpenSqlChannel();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM " + table + " WHERE " + condition;
            cmd.ExecuteNonQuery();
            //OPTIMIZE TABLE  
            cmd.CommandText = "OPTIMIZE TABLE " + table;
            cmd.ExecuteNonQuery();
            CloseSqlChannel();
        }
        public string[] Read1DArray_SQL_Data(string Value_Name, string table, string condition)
        {
            OpenSqlChannel();
            string CmdText = "SELECT " + Value_Name + " FROM " + table + " WHERE " + condition;
            MySqlCommand cmd = new MySqlCommand(CmdText, conn);
            MySqlDataReader myReader;
            List<string> data_array = new List<string>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    data_array.Add(myReader.GetString(0));
                }
            }
            catch
            {
            }
            myReader.Close();
            CloseSqlChannel();
            return data_array.ToArray();
        }
        public string[] Read1DArrayNoCondition_SQL_Data(string Value_Name, string table)
        {
            OpenSqlChannel();
            string CmdText = "SELECT " + Value_Name + " FROM " + table;

            MySqlCommand cmd = new MySqlCommand(CmdText, conn);
            MySqlDataReader myReader;
            List<string> data_array = new List<string>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    data_array.Add(myReader.GetString(0));
                }
            }
            catch
            {
            }
            myReader.Close();
            CloseSqlChannel();
            return data_array.ToArray();
        }
        public void ClearEntireTable( string table )
        {
            OpenSqlChannel();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "TRUNCATE " + table;
            cmd.ExecuteNonQuery();
            CloseSqlChannel();
        }

        public bool InsertSqlData( string strTable, string[] arrCellName, string[] arrValue )
        {
            if ( arrCellName.Length != arrValue.Length || arrCellName.Length == 0 )
            {
                return false;
            }
            
            try
            {
                string strCommand = "Insert into " + strTable + "(";

                for ( int i = 0; i < arrCellName.Length; ++i )
                {
                    strCommand += arrCellName[i];
                    if ( i != arrCellName.Length - 1 )
                    {
                        strCommand += ",";
                    }
                    else
                    {
                        strCommand += ") values('";
                    }
                }

                for ( int i = 0; i < arrValue.Length; ++i )
                {
                    strCommand += arrValue [ i ];
                    if ( i != arrValue.Length - 1 )
                    {
                        strCommand += "','";
                    }
                    else
                    {
                        strCommand += "')";
                    }
                }

                OpenSqlChannel();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = strCommand;
                cmd.ExecuteNonQuery();
                CloseSqlChannel();

            }
            catch
            {
            }


            return true;
        }

        //===================================Basic function=============================================
    }
}
