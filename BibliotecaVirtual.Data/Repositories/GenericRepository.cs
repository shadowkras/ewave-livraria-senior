using BibliotecaVirtual.Data.Contexts;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Interfaces;
using BibliotecaVirtual.Data.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Data.Repositories
{
    /// <summary>
    /// Generic repository class with standard CRUD methods.
    /// </summary>
    /// <typeparam name="TEntity">Database entity.</typeparam>
    /// <typeparam name="TContext">Database context. Class must inherit GenericContext.</typeparam>
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>, IDisposable where TEntity : class
    {
        #region Fields

        protected readonly GenericContext<TContext> _dbContext;
        public DbSet<TEntity> DbSet;

        #endregion

        #region Construtor

        /// <summary>
        /// Generic construtor based on a specific DbContext.
        /// </summary>
        /// <param name="dbContext"></param>
        public GenericRepository(GenericContext<TContext> dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
            _dbContext.SetTrackingBehavior();

            try
            {
                DbSet = dbContext.Set<TEntity>();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(string.Format(Errors.Possible_0, $"map the entity: {this.GetType().Name}"), ex);
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// Insert a new entity to our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Insert(newEntity);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be saved to our repository.</param>
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Method to insert a list of entities to our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Insert(entityList);</para>
        /// </summary>
        /// <param name="entities">List of entities to be saved to our repository.</param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        #endregion

        #region Update

        /// <summary>
        /// Method to update a single entity.
        /// <para>Examples:</para>
        /// <para>_repository.Update(entity);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be saved to our repository.</param>
        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        /// <summary>
        /// Method to update our repository using a list of entities.
        /// <para>Examples:</para>
        /// <para>_repository.Update(entityList);</para>
        /// </summary>
        /// <param name="entities">List of entities to be saved to our repository.</param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        /// <summary>
        /// Method to update specific properties of an entity.
        /// <para>Examples:</para>
        /// <para>_repository.Update(user, p => p.FirstName, p => p.LastName);</para>
        /// <para>_repository.Update(user, p => p.Password);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be saved to our repository.</param>
        /// <param name="propriedades">Array of expressions with the properties that will be changed.</param>
        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] propriedades)
        {
            _dbContext.Attach(entity);

            foreach (var item in propriedades.AsParallel())
            {
                _dbContext.Entry(entity).Property(item).IsModified = true;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Delete(entity);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be deleted to our repository.</param>
        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Delete(entityList);</para>
        /// </summary>
        /// <param name="entities">List of entities to be deleted to our repository.</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Delete(p=> p.UserId == userId);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            DbSet.AsQueryable()
                 .Where(predicate)
                 .ToList()
                 .ForEach(entity => DbSet.Remove(entity));
        }

        #endregion

        #region Select

        /// <summary>
        /// Select an entity using it's primary keys as search criteria.
        /// <para>Examples:</para>
        /// <para>_repository.SelectByKey(userId);</para>
        /// <para>_repository.SelectByKey(companyId, userId);</para>
        /// </summary>
        /// <param name="primaryKeys">Primary key properties of our entity.</param>
        /// <returns>Returns an entity from our repository.</returns>
        public async Task<TEntity> SelectByKey(params object[] primaryKeys)
        {
            var entity = await DbSet.FindAsync(primaryKeys);
            return entity.Detach(_dbContext);
        }

        /// <summary>
        /// Select all entities from our repository
        /// <para>Examples:</para>
        /// <para>_repository.SelectAll();</para>
        /// </summary>
        /// <returns>Returns all entities from our repository.</returns>
        public virtual async Task<IList<TEntity>> SelectAll()
        {
            return await DbSet.ToListAsync();
        }

        /// <summary>
        /// Select entities using pagination (take N).
        /// <para>Examples:</para>
        /// <para>_repository.SelectAllByPage(1, 20);</para>
        /// <para>_repository.SelectAllByPage(pageNumber, quantityPerPage);</para>
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="quantity">Number of entities to select per page.</param>
        /// <returns>Returns entities from our repository.</returns>
        public virtual async Task<IList<TEntity>> SelectAllByPage(int pageNumber, int quantity)
        {
            return await DbSet.Skip(Math.Max(pageNumber - 1, 0) * quantity)
                              .Take(quantity)
                              .ToListAsync();
        }

        /// <summary>
        /// Select entities using pagination (take N) and a filter.
        /// <para>Examples:</para>
        /// <para>_repository.SelectAllByPage(1, 20, p => p.UserId);</para>
        /// <para>_repository.SelectAllByPage(pageNumber, quantityPerPage, p => p.UserId);</para>
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="quantity">Number of entities to select per page.</param>
        /// <param name="properties">Fields that will be selected and populated.</param>
        /// <returns>Returns entities from our repository.</returns>
        public virtual async Task<IList<TResultado>> SelectAllByPage<TResultado>(int pageNumber, int quantity, Expression<Func<TEntity, TResultado>> properties)
        {
            return await DbSet.Skip(Math.Max(pageNumber - 1, 9) * quantity)
                              .Take(quantity)
                              .Select(properties)
                              .ToListAsync();
        }

        /// <summary>
        /// Select an entity from our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.Select(p=> p.UserId == 1);</para>
        /// <para>_repository.Select(p=> p.UserName.Contains("John") == true);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns an entity from our repository.</returns>
        public virtual async Task<TEntity> Select(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.WhereNullSafe(predicate)
                              .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Select specific properties of an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Select(p=> p.UserId == userId, p=> p.LastName);</para>
        /// </summary>
        /// <typeparam name="TResult">Entity returned from our search.</typeparam>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <param name="properties">Fields that will be selected and populated in our result.</param>
        /// <returns>Returns an entity from our repository.</returns>
        public async Task<TResult> Select<TResult>(Expression<Func<TEntity, bool>> predicate,
                                       Expression<Func<TEntity, TResult>> properties)
        {
            return await DbSet.WhereNullSafe(predicate)
                              .Select(properties)
                              .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Select a list of entities from our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.SelectList(p => p.LastName.Contains("Doe"));</para>
        /// <para>_repository.SelectList(null);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns a list of entities from our repository.</returns>
        public async Task<IList<TEntity>> SelectList(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.WhereNullSafe(predicate)
                              .ToListAsync();
        }

        /// <summary>
        /// Select specific properties in list of entities from our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.SelectList(p => p.LastName.Contains("Doe"), p => p.UserId);</para>
        /// <para>_repository.SelectList(p => p.IsAdmin == true, p => p.UserId);</para>
        /// </summary>
        /// <typeparam name="TResult">Entity returned from our search.</typeparam>
        /// <param name="properties">Fields that will be selected and populated.</param>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns a list of entities from our repository.</returns>
        public async Task<IList<TResult>> SelectList<TResult>(Expression<Func<TEntity, bool>> predicate,
                                                              Expression<Func<TEntity, TResult>> properties)
        {
            return await DbSet.WhereNullSafe(predicate)
                              .Select(properties)
                              .ToListAsync();
        }

        /// <summary>
        /// Select specific properties in list of entities from our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.SelectList(0, 10, p => p.GrupoId, p => p.PreferenciaId == preferencia.PreferenciaId);</para>
        /// <para>_repository.SelectList(1, 20, p => p.UsuarioId, p => p.GrupoId == grupoId &amp;&amp; p.Preferencial == true);</para>
        /// <para>_repository.SelectList(0, 10, p=> new Usuario(p.UsuarioId, p.UsuarioNome), p => p.Inativo == false);</para>
        /// <para>_repository.SelectList(0, 100, p=> new Pais(p.PaisId, p.PaisNome));</para>
        /// </summary>
        /// <typeparam name="TResultado"></typeparam>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="quantity">Number of entities to select per page.</param>
        /// <param name="properties">Fields that will be selected and populated in our result.</param>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns a list of entities from our repository</returns>
        public virtual async Task<IList<TResultado>> SelectList<TResultado>(int pageNumber, int quantity,
                                                                            Expression<Func<TEntity, TResultado>> properties,
                                                                            Expression<Func<TEntity, bool>> predicate = null)
        {
            return await DbSet.WhereNullSafe(predicate)
                              .Skip(Math.Max(pageNumber - 1, 0) * quantity)
                              .Take(quantity)
                              .Select(properties)
                              .ToListAsync();
        }

        /// <summary>
        /// Method to verify if there are any entries in our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.Exists(p => p.UserId == user.Id)</para>
        /// <para>_repository.Exists(p => p.UserId == id &amp;&amp; p.IsAdmin == false);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns if any entity was found using the search criteria.</returns>
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }

        /// <summary>
        /// Returns the max value from our repository using a filter.
        /// </summary>
        /// <param name="properties">Fields that will be selected and populated in our result.</param>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns></returns>
        public virtual async Task<int> Max(Expression<Func<TEntity, int>> properties,
                                           Expression<Func<TEntity, bool>> predicate = null)
        {
            var sequencia = await DbSet.WhereNullSafe(predicate)
                                       .DefaultIfEmpty()
                                       .MaxAsync(properties);

            return sequencia;
        }

        /// <summary>
        /// Returns the max value from our repository using a filter.
        /// </summary>
        /// <param name="properties">Fields that will be selected and populated in our result.</param>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns></returns>
        public virtual async Task<long> Max(Expression<Func<TEntity, long>> properties,
                                            Expression<Func<TEntity, bool>> predicate = null)
        {
            var sequencia = await DbSet.WhereNullSafe(predicate)
                                       .DefaultIfEmpty()
                                       .MaxAsync(properties);

            return sequencia;
        }

        /// <summary>
        /// Returns the number of entities in our repository.
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns></returns>
        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            var countEntities = await DbSet.WhereNullSafe(predicate)
                                           .CountAsync();
            return countEntities;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose method.
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
