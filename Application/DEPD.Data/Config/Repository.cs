using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Abstractions.Dependencies.Data.Config;
using Abstractions.BLL.Entity;
using System.Collections.Generic;

namespace ConcreteDependencies.Data.Config
{
    public abstract class Repository<T> : IRepository<T> 
    {

        protected internal IRepositoryProcedure _StoredProcedure;
        protected internal IEntityContainer _entityContainer;
        protected internal IRepositoryContainer _repositoryContainer;


        public Repository(IRepositoryProcedure _RepositoryProcedure , IEntityContainer entityContainer, IRepositoryContainer repositoryContainer)
        {
            _StoredProcedure = _RepositoryProcedure;
            _entityContainer = entityContainer;
            _repositoryContainer = repositoryContainer;
        }
        
        protected internal void SetItemFromRow(T item, DataRow row) 
        {            
            foreach (DataColumn c in row.Table.Columns)
            {
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }

        protected internal SqlParameter[] objToParameterList(T obj)
        {
            Type t = typeof(T);
            SqlParameter[] sqlParameters;
            List<SqlParameter> parametersList = new List<SqlParameter>();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                Type PrpertyType = info.PropertyType;
                Type[] AcceptableTypes = new Type[] {
                                                        typeof(Enum),
                                                        typeof(String),
                                                        typeof(Decimal),
                                                        typeof(int),
                                                        typeof(float),
                                                        typeof(short),
                                                        typeof(bool),
                                                        typeof(byte),
                                                        typeof(byte[]),
                                                        typeof(double),
                                                        typeof(DateTime),
                                                        typeof(DateTimeOffset),
                                                        typeof(TimeSpan),
                                                        typeof(Guid),
                                                        typeof(Decimal?),
                                                        typeof(int?),
                                                        typeof(float?),
                                                        typeof(short?),
                                                        typeof(bool?),
                                                        typeof(byte?),
                                                        typeof(double?),
                                                        typeof(DateTime?),
                                                        typeof(DateTimeOffset?),
                                                        typeof(TimeSpan?),
                                                        typeof(Guid?)

                                                    };
                if (PrpertyType.IsPrimitive || AcceptableTypes.Contains(PrpertyType))
                {
                    parametersList.Add(new SqlParameter("@" + info.Name, info.GetValue(obj)));
                }

            }

            sqlParameters = parametersList.ToArray();
            return sqlParameters;

        }

        public void Begin()
        {
            _StoredProcedure.Begin();
        }

        public void Commit()
        {
            _StoredProcedure.Commit();
        }

        public void Rollback()
        {
            _StoredProcedure.Rollback();
        }

        public virtual T Get(object ID, bool AggregateRoot = false)
        {
            return ExecuteGet("Get_" + this.GetType().Name.Replace("Repository", ""), ID, AggregateRoot);
        }

        public virtual int Insert(T Obj)
        {
            return ExecuteInsert("Add_" + this.GetType().Name.Replace("Repository", ""), Obj);
        }

        public virtual int Delete(int ID)
        {
            return ExecuteDelete("Delete_" + this.GetType().Name.Replace("Repository", ""), ID);
        }

        public virtual int Update(T Obj)
        {
            return ExecuteUpdate("Update_" + this.GetType().Name.Replace("Repository", ""), Obj);
        }

        public virtual IList<T> GetAll(bool AggregateRoot = false)
        {
            DataTable table = (DataTable)_StoredProcedure.ExecuteTable("GetAll_" + GetType().Name.Replace("Repository", ""));
            return DataTableToList(table, AggregateRoot);
        }

        protected internal int ExecuteDelete(string SPName, int ID)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ID", ID);
            return (int)_StoredProcedure.Execute(SPName, sqlParameters);

        }

        protected internal T ExecuteGet(string SPName,  object ID, bool AggregateRoot = false)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ID", ID);
            DataTable dataTable = (DataTable)_StoredProcedure.ExecuteTable(SPName, sqlParameters);
            T Obj = (T)_entityContainer.Resolve<T>();
            if (dataTable.Rows.Count > 0)
            {
                SetItemFromRow(Obj, dataTable.Rows[0]);
            }

            if (AggregateRoot == true)
            {
                Obj = PopulateAggregates(Obj);
            }

            return Obj;
        }

        protected internal int ExecuteInsert(string SPName, T Obj)
        {
            SqlParameter[] sqlParameters = objToParameterList((T)Obj);
            object obj = _StoredProcedure.ExecuteInsert(SPName, sqlParameters);
            return Convert.ToInt32(obj);
        }

        protected internal int ExecuteUpdate(string SPName, T Obj)
        {
            SqlParameter[] sqlParameters = objToParameterList((T)Obj);
            return (int)_StoredProcedure.Execute(SPName, sqlParameters);
        }

        protected internal virtual T PopulateAggregates(T Obj)
        {
            return Obj;
        }

        public IList<T> GetObjectsAgainstFK(string SPName, object ID, bool AggregateRoot)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ID", ID);
            DataTable dataTable = (DataTable)_StoredProcedure.ExecuteTable(SPName, sqlParameters);
            return DataTableToList(dataTable, AggregateRoot);
        }

        public IList<T> GetObjectsAgainstFKs(string SPName, SqlParameter[] sqlParameters, bool AggregateRoot)
        {           
            DataTable dataTable = (DataTable)_StoredProcedure.ExecuteTable(SPName, sqlParameters);
            return DataTableToList(dataTable, AggregateRoot);
        }

        private IList<T> DataTableToList(DataTable dataTable, bool AggregateRoot)
        {
            IList<T> ObjectsList = new List<T>();
            foreach (DataRow r in dataTable.Rows)
            {
                T obj = (T)_entityContainer.Resolve<T>();
                SetItemFromRow(obj, r);
                if (AggregateRoot == true)
                {
                    obj = PopulateAggregates(obj);
                }
                ObjectsList.Add(obj);
            }
            return ObjectsList;
        }
    }
}