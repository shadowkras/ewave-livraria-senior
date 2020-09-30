using BibliotecaVirtual.Data.Contexts;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Interfaces;
using BibliotecaVirtual.Data.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Data.UnitOfWork
{
    /// <summary>
    /// Classe de UnitOfWork genérica que implementa os métodos padrões.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class GenericUnitOfWork<TContext> : IGenericUnitOfWork
    {
        #region Fields

        protected readonly GenericContext<TContext> _dbContext;
        private bool _disposed;

        #endregion

        #region Properties

        /// <summary>
        /// Returns if our transaction was succesful.
        /// </summary>
        public bool OperationSuccesful { get; private set; } = false;

        /// <summary>
        /// Message returned from our transaction.
        /// </summary>
        public string OperationMessage { get; private set; }

        #endregion

        #region Constructor

        public GenericUnitOfWork(GenericContext<TContext> dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("dbContext");
            _dbContext.SetTrackingBehavior();
        }

        #endregion

        #region Begin Transaction

        /// <summary>
        /// Start a new transaction.
        /// </summary>
        public void BeginTransaction()
        {
            _disposed = false;
        }

        #endregion

        #region Commit

        /// <summary>
        /// Save changes to our database.
        /// </summary>
        public bool Commit()
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    DetachAll();

                    OperationSuccesful = true;
                    OperationMessage = Info.OperationSuccess;
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    OperationSuccesful = false;
                    OperationMessage = string.Format(Errors.Rollback_0, ex.GetMessages());
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    OperationSuccesful = false;
                    OperationMessage = string.Format(Errors.Error_0, ex.GetMessages());
                }
            }

            return OperationSuccesful;
        }

        /// <summary>
        /// Salvar informações na base de dados de forma assíncrona.
        /// </summary>
        public async Task<bool> CommitAsync()
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                    await Task.Run(() => transaction.Commit());

                    OperationSuccesful = true;
                    OperationMessage = Info.OperationSuccess;
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    OperationSuccesful = false;
                    OperationMessage = string.Format(Errors.Rollback_0, ex.GetMessages());
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    OperationSuccesful = false;
                    OperationMessage = string.Format(Errors.Error_0, ex.GetMessages());
                }
                finally
                {
                    DetachAll();
                }
            }

            return OperationSuccesful;
        }

        #endregion

        #region Detach

        /// <summary>
        /// Detaches all entities from the context that were added or modified.
        /// </summary>
        private void DetachAll()
        {
            foreach (var dbEntityEntry in _dbContext.ChangeTracker.Entries().ToArray())
            {
                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
        }

        #endregion

        #region Operação Sucesso

        /// <summary>
        /// Return whether the Commit statement was successful.
        /// </summary>
        /// <returns></returns>
        public bool IsSucess()
        {
            return OperationSuccesful;
        }

        #endregion

        #region Mensagem da Operação

        /// <summary>
        /// Returns the operation message.
        /// </summary>
        /// <returns></returns>
        public string GetOperationMessage()
        {
            return OperationMessage;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose method.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Dispose method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
