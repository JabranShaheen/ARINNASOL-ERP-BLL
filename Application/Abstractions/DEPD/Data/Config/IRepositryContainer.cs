namespace Abstractions.Dependencies.Data.Config
{
    public interface IRepositoryContainer
    {
        object Resolve<T>(object parameterOverride);
        object Resolve<T>();
    }
}
