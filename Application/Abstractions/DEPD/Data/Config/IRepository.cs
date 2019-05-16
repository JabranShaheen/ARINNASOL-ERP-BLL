using System.Collections.Generic;

namespace Abstractions.Dependencies.Data.Config
{
    public interface IRepository<TEntity> 
    {
        void Begin();
        void Commit();
        void Rollback();

        TEntity Get(object ID, bool AggregateRoot = false);        
        int Insert(TEntity Obj);
        int Delete(int ID);
        int Update(TEntity Obj);
        IList<TEntity> GetAll(bool AggregateRoot = false);
    }
}