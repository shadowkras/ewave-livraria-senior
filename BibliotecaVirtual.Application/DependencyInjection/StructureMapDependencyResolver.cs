using StructureMap;
using System;

namespace BibliotecaVirtual.Application.DependencyInjection
{
    public class StructureMapDependencyResolver
    {
        public static Func<IContainer> ContainerAcesso { get; set; }
        private static IContainer Container => ContainerAcesso();

        public static T GetContainer<T>()
        {
            return Container.TryGetInstance<T>();
        }
    }
}
