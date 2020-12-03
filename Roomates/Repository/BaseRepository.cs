using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Roomates.Repository
{
    class BaseRepository
    {

        private string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection Connection => new SqlConnection(_connectionString);
    }
}
