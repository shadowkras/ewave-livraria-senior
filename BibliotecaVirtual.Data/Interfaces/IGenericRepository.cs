using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Data.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Insert a new entity to our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Insert(newEntity);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be saved to our repository.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Method to insert a list of entities to our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Insert(entityList);</para>
        /// </summary>
        /// <param name="entities">List of entities to be saved to our repository.</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Method to return the current value of a certain property assigned to an entity.
        /// </summary>
        /// <param name="entity">Entity instance.</param>
        /// <param name="property">Property to be checked.</param>
        /// <returns></returns>
        int GetInsertCurrentKey(TEntity entity, Expression<Func<TEntity, int>> property);

        /// <summary>
        /// Method to update a single entity.
        /// <para>Examples:</para>
        /// <para>_repository.Update(entity);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be saved to our repository.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Method to update our repository using a list of entities.
        /// <para>Examples:</para>
        /// <para>_repository.Update(entityList);</para>
        /// </summary>
        /// <param name="entities">List of entities to be saved to our repository.</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Method to update specific properties of an entity.
        /// <para>Examples:</para>
        /// <para>_repository.Update(user, p => p.FirstName, p => p.LastName);</para>
        /// <para>_repository.Update(user, p => p.Password);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be saved to our repository.</param>
        /// <param name="propriedades">Array of expressions with the properties that will be changed.</param>
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] propriedades);

        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Delete(entity);</para>
        /// </summary>
        /// <param name="entity">Entity instance to be deleted to our repository.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Delete(entityList);</para>
        /// </summary>
        /// <param name="entities">List of entities to be deleted to our repository.</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Delete(p=> p.UserId == userId);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Select an entity using it's primary keys as search criteria.
        /// <para>Examples:</para>
        /// <para>_repository.SelectByKey(userId);</para>
        /// <para>_repository.SelectByKey(companyId, userId);</para>
        /// </summary>
        /// <param name="primaryKeys">Primary key properties of our entity.</param>
        /// <returns>Returns an entity from our repository.</returns>
        Task<TEntity> SelectByKey(params object[] primaryKeys);

        /// <summary>
        /// Select all entities from our repository
        /// <para>Examples:</para>
        /// <para>_repository.SelectAll();</para>
        /// </summary>
        /// <returns>Returns all entities from our repository.</returns>
        Task<IList<TEntity>> SelectAll();

        /// <summary>
        /// Select entities using pagination (take N).
        /// <para>Examples:</para>
        /// <para>_repository.SelectAllByPage(1, 20);</para>
        /// <para>_repository.SelectAllByPage(pageNumber, quantityPerPage);</para>
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="quantity">Number of entities to select per page.</param>
        /// <returns>Returns entities from our repository.</returns>
        Task<IList<TEntity>> SelectAllByPage(int pageNumber, int quantity);

        /// <summary>
        /// Select an entity from our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.Select(p=> p.UserId == 1);</para>
        /// <para>_repository.Select(p=> p.UserName.Contains("John") == true);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns an entity from our repository.</returns>
        Task<TEntity> Select(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Select specific properties of an entity from our repository.
        /// <para>Examples:</para>
        /// <para>_repository.Select(p=> p.UserId == userId, p=> p.LastName);</para>
        /// </summary>
        /// <typeparam name="TResult">Entity returned from our search.</typeparam>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <param name="properties">Fields that will be selected and populated in our result.</param>
        /// <returns>Returns an entity from our repository.</returns>
        Task<TResult> Select<TResult>(Expression<Func<TEntity, bool>> predicate,
                                      Expression<Func<TEntity, TResult>> properties);

        /// <summary>
        /// Select a list of entities from our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.SelectList(p => p.LastName.Contains("Doe"));</para>
        /// <para>_repository.SelectList(null);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns a list of entities from our repository.</returns>
        Task<IList<TEntity>> SelectList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Select specific properties in list of entities from our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.SelectList(p => p.UserId, p => p.LastName.Contains("Doe"));</para>
        /// <para>_repository.SelectList(p => p.UserId, p => p.IsAdmin == true);</para>
        /// </summary>
        /// <typeparam name="TResult">Entity returned from our search.</typeparam>
        /// <param name="properties">Fields that will be selected and populated.</param>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns a list of entities from our repository.</returns>
        Task<IList<TResult>> SelectList<TResult>(Expression<Func<TEntity, bool>> predicate,
                                                 Expression<Func<TEntity, TResult>> properties);

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
        Task<IList<TResultado>> SelectList<TResultado>(int pageNumber, int quantity,
                                                       Expression<Func<TEntity, TResultado>> properties,
                                                       Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Method to verify if there are any entries in our repository using a filter.
        /// <para>Examples:</para>
        /// <para>_repository.Exists(p => p.UserId == user.Id)</para>
        /// <para>_repository.Exists(p => p.UserId == id &amp;&amp; p.IsAdmin == false);</para>
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns>Returns if any entity was found using the search criteria.</returns>
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns the max value from our repository using a filter.
        /// </summary>
        /// <param name="predicate">Filter applied to our search</param>
        /// <returns></returns>
        Task<int> Max(Expression<Func<TEntity, int>> properties,
                      Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Returns the max value from our repository using a filter.
        /// </summary>
        /// <param name="predicate">Filter applied to our search</param>
        /// <returns></returns>
        Task<long> Max(Expression<Func<TEntity, long>> properties,
                       Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Returns the number of entities in our repository.
        /// </summary>
        /// <param name="predicate">Filter applied to our search.</param>
        /// <returns></returns>
        Task<int> Count(Expression<Func<TEntity, bool>> predicate = null);
    }
}
