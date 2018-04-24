using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Academic_Solution_NPC
{
    class SQLUtil
    {
        MySqlConnection conn;
        MySqlCommand com;
        MySqlDataAdapter myAdapter;

        public SQLUtil()
        {
            com = new MySqlCommand();
        }
        public MySqlConnection OpenConnection()
        {
            conn = new MySqlConnection();
            try
            {
                conn.ConnectionString = GetSettings();//Get Connection String
                conn.Open();
                return conn;
            }
            catch
            {
                return null;
            }
        }

        public void CloseConnection()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public string GetSettings()
        {//Fixed for testing
            return "SERVER=localhost;PORT=3306;DATABASE=DB;UID=root;PASSWORD=mysqllao;";
        }
        /// <summary>
        /// Select Datas from Database and convert it to local DataTable and CloseConnection.
        /// </summary>
        /// <param name="query">Specific Select Query for Database</param>
        /// <returns></returns>
        public DataTable SelectTable(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                myAdapter = new MySqlDataAdapter(query, OpenConnection());
                myAdapter.Fill(dt);
                myAdapter.Dispose();
                CloseConnection();
                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Insert any non query commands to database
        /// </summary>
        /// <param name="query">Any non Query commands</param>
        /// <returns>-1 for error and >0 when accepted</returns>
        public int InsertQuery(string query)
        {
            com.CommandText = query;
            try
            {
                com.Connection = OpenConnection();
                int res = com.ExecuteNonQuery();
                return res;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
        }
        /// <summary>
        /// Gets Database Server's current date and time
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetServerDateTime()
        {
            DataTable dt = SelectTable("SELECT NOW();");
            try
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DateTime now = Convert.ToDateTime(dt.Rows[0][0].ToString());
                        return now;
                    }
                }
                return new DateTime();
            }
            catch { return new DateTime(); }
        }
        /// <summary>
        /// Gets specific data from a table. Always gets the first row data if has multiple row in query.
        /// </summary>
        /// <param name="columnName">Column name in table/Column data you want to get</param>
        /// <param name="table">Table name in database</param>
        /// <param name="defaultValue">Return value if there is no record found</param>
        /// <param name="criteria">Condition/Constraint to filter the query</param>
        /// <returns>string data</returns>
        public string DataLookUp(string columnName, string table, string defaultValue, string criteria)
        {
            string query = "SELECT " + columnName + " FROM " + table + " " +
                (criteria.Trim().Length > 0 ? "WHERE " + criteria + " " : "");
            DataTable dt = SelectTable(query);
            if (dt.Rows.Count == 0)
                return defaultValue;
            else
            {
                DataRow r = dt.Rows[0];
                return r[columnName].ToString();
            }
        }
        /// <summary>
        /// Can only be used in a table with autoincrement. Gets the last inserted row in specific table
        /// </summary>
        /// <param name="table">Table name in database</param>
        /// <param name="columnIncrementalID">Auto increment column in table</param>
        /// <returns>DataRow last inserted row</returns>
        public DataRow GetLastInsertItem(string table, string columnIncrementalID)
        {
            DataTable dt = SelectTable("SELECT * FROM " + table + " ORDER BY " + columnIncrementalID + " DESC LIMIT 1");
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
                return null;
        }
        /// <summary>
        /// Binds <key,value> pair in combobox
        /// </summary>
        /// <param name="query">Source</param>
        /// <param name="cmb">ComboBox control</param>
        /// <param name="columnKey">Key</param>
        /// <param name="columnValue">Value</param>
        public void BindComboboxItems(string query, ComboBox cmb, string columnKey, string columnValue)
        {
            DataTable dt = SelectTable(query);
            Dictionary<string, string> d = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                d.Add(r[columnKey].ToString(), r[columnValue].ToString());
            }
            if (dt.Rows.Count > 0)
            {
                cmb.DataSource = new BindingSource(d, null);
                cmb.ValueMember = "key";
                cmb.DisplayMember = "value";
            }
        }
        /// <summary>
        /// Inserts multiple commands at once. Rollbacks if a single command returns error code
        /// </summary>
        /// <param name="queries">Collection of Commands</param>
        public bool InsertMultiple(List<string> queries)
        {
            OpenConnection();
            MySqlCommand command = conn.CreateCommand();
            MySqlTransaction trans = conn.BeginTransaction();

            command.Connection = conn;
            command.Transaction = trans;
            try
            {
                for (int i = 0; i < queries.Count; i++)
                {
                    command.CommandText = queries[i].ToString();
                    command.ExecuteNonQuery();
                }
                trans.Commit();
                CloseConnection();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                try
                {
                    trans.Rollback();
                }
                catch (MySqlException ex)
                {
                    if (trans.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                                          " was encountered while attempting to roll back the transaction.");
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return false;
        }
        /// <summary>
        /// Insert Update Delete Datas by MySQL parameter to prevent invalid input
        /// </summary>
        /// <param name="query">Command query to execute</param>
        /// <param name="dics">KeyValuePair to match the Parameter (e.g "?ID",ID)<key,value></param>
        /// <returns>true if the command was successful false if not</returns>
        public bool GenericCommand(string query, Dictionary<string, object> dics)
        {
            //INSERT INTO tblCourse(CourseDescription) VALUES(?CD);
            //dics.Add("?CD","Asdsad");
            try
            {
                OpenConnection();
                com = conn.CreateCommand();
                com.CommandText = query;
                for (int i = 0; i < dics.Count; i++)
                {
                    com.Parameters.Add(dics.Keys.ToList()[i], dics[dics.Keys.ToList()[i]]);
                }
                int res = com.ExecuteNonQuery();
                if (res > -1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /*
         * comm.CommandText = "INSERT INTO room(person,address) VALUES(?person, ?address)";
comm.Parameters.Add("?person", "Myname");
comm.Parameters.Add("?address", "Myaddress");
comm.ExecuteNonQuery();
conn.Close();
         */
    }
}
