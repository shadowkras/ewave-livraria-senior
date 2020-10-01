using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public class BaseService
    {
        private readonly IApplicationUnitOfWork _uow;
        internal string ModelError = string.Empty;
        internal bool OperationSuccesful = false;

        #region Construtor

        public BaseService(IApplicationUnitOfWork uow)
        {
            _uow = uow;
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Retorna se a operação foi bem sucedida.
        /// </summary>
        /// <returns></returns>
        public bool IsSuccessful()
        {
            return OperationSuccesful;
        }

        /// <summary>
        /// Retorna as mensagens de erro da validação da regra de negócios.
        /// </summary>
        /// <returns></returns>
        public string GetModelErrors()
        {
            return ModelError;
        }

        #endregion

        /// <summary>
        /// Retorna as mensagens de erro da validação da regra de negócios.
        /// </summary>
        /// <returns></returns>
        public string AddModelError(string mensagem)
        {
            if (string.IsNullOrEmpty(ModelError) == false)
            {
                mensagem += " " + Environment.NewLine + mensagem;
                return ModelError = mensagem;
            }
            else
            {
                return ModelError = mensagem;
            }
        }

        #region Begin Transaction

        public void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        #endregion

        #region Commit

        public async Task<bool> Commit()
        {
            OperationSuccesful = await _uow.CommitAsync();
            AddModelError(_uow.GetOperationMessage());

            return _uow.IsSucess();
        }

        #endregion
    }
}
