using System;
using StructureMap;

namespace BibliotecaVirtual.Application.IoC
{
    public class BootstrapperApplication : Registry
    {
        #region Construtor

        public BootstrapperApplication()
        {
            Scan(scanner =>
            {
                scanner.AssembliesFromApplicationBaseDirectory(p => p.FullName.Contains("BibliotecaVirtual"));
                scanner.WithDefaultConventions();
                scanner.RegisterConcreteTypesAgainstTheFirstInterface();
            });
        }

        #endregion
    }
}
