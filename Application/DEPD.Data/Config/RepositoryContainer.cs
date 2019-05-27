using Unity;
using Unity.Injection;
using Abstractions.Dependencies.Data.Config;
using Unity.Lifetime;
using Unity.Resolution;
using Abstractions.BLL.Entity;

namespace ConcreteDependencies.Data.Config
{
    public class RepositoryContainer : IRepositoryContainer
    {

        private UnityContainer container;
        IEntityContainer _entityContainer;

        public RepositoryContainer(IEntityContainer entityContainer, string connection = "")
        {

            container = new UnityContainer();
            _entityContainer = entityContainer;
            
            container.RegisterType<IRepositoryConnection, SQLRepositoryConnection>(new ContainerControlledLifetimeManager(), new InjectionConstructor(connection));
            container.RegisterType<IRepositoryProcedure, StoredProcedure>(new InjectionConstructor(typeof(IRepositoryConnection)));

        }

        public object Resolve<T>(object OverrideStoredProcedure)
        {
            return container.Resolve<T>((ParameterOverride)OverrideStoredProcedure);
        }

        public object Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}