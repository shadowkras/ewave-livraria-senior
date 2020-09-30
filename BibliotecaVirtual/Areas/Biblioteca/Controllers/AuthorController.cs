using BibliotecaVirtual.Application.Interfaces;
using BibliotecaVirtual.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BibliotecaVirtual.Areas.Biblioteca.Controllers
{
    [Authorize]
    [Area("Biblioteca")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        #region Construtor

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        } 

        #endregion

        #region Métodos privados

        /// <summary>
        /// Adiciona uma mensagem de erro ao model state da view model.
        /// </summary>
        /// <param name="message"></param>
        private void AddModelError(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        #endregion

        #region Métodos de View

        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.ObtainAuthors();
            return View(nameof(Index), authors);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(nameof(Edit), new AuthorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AuthorViewModel viewModel)
        {
            var author = await _authorService.AddAuthor(viewModel);

            if (_authorService.IsSuccessful() == false)
            {
                AddModelError(_authorService.GetModelErrors());
                return View(nameof(Edit), author);
            }

            return RedirectToAction(nameof(Edit), new { author.AuthorId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int authorId)
        {
            var author = await _authorService.ObtainAuthor(authorId);
            return View(nameof(Edit), author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AuthorViewModel viewModel)
        {
            var author = await _authorService.UpdateAuthor(viewModel);

            if (_authorService.IsSuccessful() == false)
            {
                AddModelError(_authorService.GetModelErrors());
                return View(nameof(Edit), author);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int authorId)
        {
            await _authorService.DeleteAuthor(authorId);

            if (_authorService.IsSuccessful() == false)
            {
                AddModelError(_authorService.GetModelErrors());
            }

            return RedirectToAction(nameof(Index));
        } 

        #endregion

        #region Métodos de Api

        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.ObtainAuthors();
            return Json(authors);
        }

        #endregion
    }
}
