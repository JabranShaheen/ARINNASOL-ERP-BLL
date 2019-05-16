namespace Abstractions.Dependencies.Data.Config
{
    public interface IRepositoryConnection
    {
         object ConnectionObject{ get; }
         object ConnectionSettings { get; }
    }
}