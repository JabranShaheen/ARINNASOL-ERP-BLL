using System;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using Abstractions.Dependencies.Data.Config;
using System.Xml;

namespace ConcreteDependencies.Data.Config
{
    public class SQLRepositoryConnection : IRepositoryConnection
    {

        private SqlConnection RepositoryConnection;

        string ConnectionString = string.Empty;

        public object ConnectionObject => RepositoryConnection;

        public object ConnectionSettings => ConnectionString;

        public SQLRepositoryConnection(string _connectionString)
        {
            ConnectionString = _connectionString;
            RepositoryConnection = new SqlConnection(_connectionString);
            RepositoryConnection.Open();
        }
    }
}