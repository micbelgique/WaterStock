using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Trasys.Dev.Data;

namespace SummerCamp.WaterStock.Services.Models
{
    /// <summary>
    /// Main Data Service 
    /// </summary>
    public class DataService : IDisposable
    {
        private SqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of DataService
        /// </summary>
        public DataService()
        {
            this.Configuration = new Configuration(this);

            _connection = new SqlConnection(this.Configuration.ConnectionString);
            _connection.Open();
        }

        /// <summary>
        /// Returns a reference to a new SqlDatabaseCommand
        /// </summary>
        /// <returns></returns>
        public SqlDatabaseCommand GetDatabaseCommand()
        {
            return new SqlDatabaseCommand(_connection);
        }

        /// <summary>
        /// Get all DataService configurations
        /// </summary>
        public Configuration Configuration { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    if (_connection.State != System.Data.ConnectionState.Closed)
                        _connection.Close();
                    _connection = null;
                    GC.SuppressFinalize(this);
                }
            }
        }

        ~DataService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}