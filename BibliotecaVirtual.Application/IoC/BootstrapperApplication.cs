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
                scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(a => a.FullName.Contains("BibliotecaVirtual"));
                scanner.WithDefaultConventions();
                scanner.RegisterConcreteTypesAgainstTheFirstInterface();
            });
        }

        #endregion
    }
}
