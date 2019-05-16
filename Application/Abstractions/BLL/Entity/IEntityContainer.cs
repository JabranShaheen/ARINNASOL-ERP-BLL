namespace Abstractions.BLL.Entity
{
    public interface IEntityContainer
    {
        object Resolve<T>();
    }
}
