using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace InvertedIndex
{
    class DBConnection : IDBConnection
    {
        private static DBConnection instance = null;
        private static object SyncObj = new Object();

        private readonly string ConnectionString;
        private SqlConnection SqlConnection;
        private DBConnection() { }
        private DBConnection(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public static DBConnection GetInstance(string ConnectionString)
        {
            if (instance == null)
            {
                lock (SyncObj)
                {
                    if (instance == null)
                        instance = new DBConnection(ConnectionString);
                }
            }

            return instance;
        }
        
        public bool Open() {

            this.SqlConnection = new SqlConnection(this.ConnectionString);
            this.SqlConnection.Open();

            return true; 
        }
        public bool Close() {

            this.SqlConnection.Close();

            return false;         
        }

        public DataTable ExecuteQuery(string query) {
            DataTable result = new DataTable();

            SqlCommand command = new SqlCommand(query, this.SqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            result.Load(reader);

            return result;        
        }
       

    }
}
