using System;
using System.Data;
using System.Data.SqlClient;
using Abstractions.Dependencies.Data.Config;

namespace ConcreteDependencies.Data.Config
{
    public class StoredProcedure : IRepositoryProcedure
    {
        private SqlConnection _SqlConn;
        private SQLRepositoryConnection _RepositoryConnection;
        private SqlTransaction _Transaction;

        public StoredProcedure(IRepositoryConnection RepositoryConnection)
        {
            _RepositoryConnection = (SQLRepositoryConnection)RepositoryConnection;
            _SqlConn = (SqlConnection)_RepositoryConnection.ConnectionObject;            
        }
  
        public object ExecuteTable(string ProcedureName, object[] ProcParms = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _SqlConn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = ProcedureName;

                    if (_Transaction != null)
                    {
                        cmd.Transaction = _Transaction;
                    }

                    if (ProcParms != null)
                    {
                        foreach (SqlParameter param in ProcParms)
                            cmd.Parameters.Add(param);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return dt;
        }

        public object ExecuteInsert(string ProcedureName, object[] ProcParms)
        {
            DataTable dt = new DataTable();
            try
            {
                
                dt = (DataTable)ExecuteTable(ProcedureName, ProcParms);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            if (dt.Rows.Count > 0)
                return dt.Rows[0].ItemArray[0];
            else
                return null;
        }
        
        public object Execute(string ProcedureName, object[] ProcParms)
        {

            object RetObj = null; 

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = _SqlConn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcedureName;
                if (_Transaction != null)
                {
                    cmd.Transaction = _Transaction;
                }
                if (ProcParms != null)
                {
                    foreach (SqlParameter param in ProcParms)
                        cmd.Parameters.Add(param);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    RetObj = cmd.ExecuteNonQuery();
                }
            }
            return RetObj;
            
        }

        public void Begin()
        {
            try
            {
                _Transaction = _SqlConn.BeginTransaction();
            }
            catch
            {                
                _SqlConn = new SqlConnection((string)_RepositoryConnection.ConnectionSettings);
                _SqlConn.Open();
                _Transaction = _SqlConn.BeginTransaction();
            }
            
        }

        public void Commit()
        {
            _Transaction.Commit();
        }

        public void Rollback()
        {
            _Transaction.Rollback();
        }
    }
}