using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Application.Resources;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Data.Entities;
using BibliotecaVirtual.Data.Extensions;
using BibliotecaVirtual.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Application.Services
{
    public class PublisherService : BaseService, IPublisherService
    {
        private readonly IPublisherRepository _repository;

        #region Construtor

        public PublisherService(IPublisherRepository repositorio,
                                IApplicationUnitOfWork uow)
            : base(uow)
        {
            _repository = repositorio;
        }

        #endregion

        /// <summary>
        /// Cadastra uma nova editora.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da editora.</param>
        /// <returns></returns>
        public async Task<PublisherViewModel> AddPublisher(PublisherViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p=> p.Name == viewModel.Name))
            {
                ModelError = string.Format(Criticas.Ja_Cadastrada_0, "Editora");
                return viewModel;
            }

            #endregion

            var publisher = viewModel.AutoMapear<PublisherViewModel, Publisher>();
            _repository.Insert(publisher);
            await Commit();

            //Recuperando o valor recebido pelo PublisherId.
            viewModel.PublisherId = publisher.PublisherId;

            return viewModel;
        }

        /// <summary>
        /// Altera informações de uma editora cadastrada.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da editora.</param>
        /// <returns></returns>
        public async Task<PublisherViewModel> UpdatePublisher(PublisherViewModel viewModel)
        {
            #region Validação da regra de negócios

            if (await _repository.Exists(p => p.Name == viewModel.Name && p.PublisherId != viewModel.PublisherId))
            {
                ModelError = string.Format(Criticas.Ja_Existe_0, "outra Editora com este nome.");
                return viewModel;
            }

            #endregion

            var publisher = viewModel.AutoMapear<PublisherViewModel, Publisher>();
            _repository.Update(publisher);
            await Commit();

            return viewModel;
        }

        /// <summary>
        /// Remove uma editora.
        /// </summary>
        /// <param name="viewModel">ViewModel com as informações da editora.</param>
        /// <returns></returns>
        public async Task<bool> DeletePublisher(PublisherViewModel viewModel)
        {
            var publisher = viewModel.AutoMapear<PublisherViewModel, Publisher>();
            _repository.Delete(publisher);
            await Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Deleta uma editora cadastrado.
        /// </summary>
        /// <param name="PublisherId">Identificador da editora.</param>
        /// <returns></returns>
        public async Task<bool> DeletePublisher(int PublisherId)
        {
            _repository.Delete(p=> p.PublisherId == PublisherId);
            await Commit();

            return OperationSuccesful;
        }

        /// <summary>
        /// Obtém uma lista com os editoraes cadastrados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PublisherViewModel>> ObtainPublishers()
        {
            var publishers = await _repository.SelectAll();
            var viewModel = publishers.AutoMapearLista<Publisher, PublisherViewModel>();
            return viewModel;
        }

        /// <summary>
        /// Obtém um editora a partir do seu identificador.
        /// </summary>
        /// <param name="PublisherId">Identificador do editora.</param>
        /// <returns></returns>
        public async Task<PublisherViewModel> ObtainPublisher(int PublisherId)
        {
            var publishers = await _repository.Select(p=> p.PublisherId == PublisherId);
            var viewModel = publishers.AutoMapear<Publisher, PublisherViewModel>();
            return viewModel;
        }
    }
}
