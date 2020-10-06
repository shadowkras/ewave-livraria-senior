using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Application.ViewModels;
using BibliotecaVirtual.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Areas.Biblioteca.Controllers
{
    [Authorize]
    [Area("Biblioteca")]
    public class PublisherController : BaseController
    {
        private readonly IPublisherService _publisherService;

        #region Construtor

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        } 

        #endregion

        #region Métodos de View

        public async Task<IActionResult> Index()
        {
            var publishers = await _publisherService.ObtainPublishers();
            return View(nameof(Index), publishers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(nameof(Edit), new PublisherViewModel());
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PublisherViewModel viewModel)
        {
            var publisher = await _publisherService.AddPublisher(viewModel);

            if (_publisherService.IsSuccessful() == false)
            {
                AddModelError(_publisherService.GetModelErrors());
                return View(nameof(Edit), publisher);
            }

            return RedirectToAction(nameof(Edit), new { publisher.PublisherId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int PublisherId)
        {
            var publisher = await _publisherService.ObtainPublisher(PublisherId);
            return View(nameof(Edit), publisher);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PublisherViewModel viewModel)
        {
            var publisher = await _publisherService.UpdatePublisher(viewModel);

            if (_publisherService.IsSuccessful() == false)
            {
                AddModelError(_publisherService.GetModelErrors());
                return View(nameof(Edit), publisher);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int PublisherId)
        {
            await _publisherService.DeletePublisher(PublisherId);

            if (_publisherService.IsSuccessful() == false)
            {
                AddModelError(_publisherService.GetModelErrors());
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Métodos de Api

        public async Task<IActionResult> GetPublishers()
        {
            var publishers = await _publisherService.ObtainPublishers();
            return ReturnApi(true, string.Empty, publishers);
        }        

        #endregion
    }
}
