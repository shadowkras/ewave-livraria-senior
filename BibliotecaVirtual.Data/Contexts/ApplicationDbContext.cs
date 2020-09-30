using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace BibliotecaVirtual.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        #region OnModelCreating

        /// <summary>
        /// Abstract method to map our entities using reflection.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder instance.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Base ModelBuilder

            try
            {
                base.OnModelCreating(modelBuilder);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }

            #endregion

            #region Generic model building

            // Interface of our entities.
            var mappingInterface = typeof(IEntityTypeConfiguration<>);

            // Entity types to be mapped.
            var mappingTypes = typeof(ApplicationDbContext).GetTypeInfo()
                                               .Assembly.GetTypes()
                                               .Where(x => x.GetInterfaces()
                                               .Any(y => y.GetTypeInfo().IsGenericType &&
                                                         y.GetGenericTypeDefinition() == mappingInterface));

            // ModelBuilder's generic method.
            var entityMethod = typeof(ModelBuilder).GetMethods()
                                                   .Single(x => x.Name == "Entity" &&
                                                                x.IsGenericMethod &&
                                                                x.ReturnType.Name == "EntityTypeBuilder`1");

            foreach (var mappingType in mappingTypes)
            {
                try
                {
                    // Entity type to be mapped.
                    var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.Single();

                    // builder.Entity<TEntity> method.
                    var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);

                    // Calling builder.Entity<TEntity> to obtain the model builder of our entity.
                    var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);

                    // Creating a new mapping instance.
                    var mapper = Activator.CreateInstance(mappingType);

                    //Invokes the "Configure" method of each entity's mapping class.
                    mapper.GetType().GetMethod("Configure")?.Invoke(mapper, new[] { entityBuilder });
                }
                catch (Exception ex)
                {
                    //Pausa para identificar o erro.
                    System.Diagnostics.Debugger.Break();
                }
            }

            #endregion
        }

        #endregion
    }
}
