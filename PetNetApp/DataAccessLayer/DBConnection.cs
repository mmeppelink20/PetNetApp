using DataAccessLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    
    /// <summary>
    /// Stephen Jaurigue
    /// Created: 2023/02/01
    /// 
    /// Connection class for managing the connection to the PetNet database
    /// </summary>
    internal class DBConnection : IDBConnection
    {
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/01
        /// 
        /// Method for returning a connection to the PetNet database
        /// </summary>
        /// 
        /// <returns>SqlConnection</returns>	

        public SqlConnection GetConnection()
        {
            SqlConnection conn = null;
            string connectionString = @"Data Source=localhost; Initial Catalog=PetNet_db_am; Integrated Security=True";
            conn = new SqlConnection(connectionString);

            return conn;
        }
    }
}
