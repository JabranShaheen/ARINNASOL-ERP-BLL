namespace Abstractions.Dependencies.Data.Config
{
    public interface IRepositoryProcedure
    {
        void Begin();
        void Commit();
        void Rollback();

        object ExecuteTable(string ProcedureName, object[] ProcParms = null);
        object ExecuteInsert(string ProcedureName, object[] ProcParms);
        object Execute(string ProcedureName, object[] ProcParms);
    }
}