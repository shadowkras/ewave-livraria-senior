using BibliotecaVirtual.Application.Enums;
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
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;

        #region Construtor

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        } 

        #endregion

        #region Métodos de View

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.ObtainAuthors();
            return View(nameof(Index), authors);
        }

        [Authorize(Roles = UserRoles.Moderator)]
        [HttpGet]
        public IActionResult Add()
        {
            return View(nameof(Edit), new AuthorViewModel());
        }

        [Authorize(Roles = UserRoles.Moderator)]
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

        [Authorize(Roles = UserRoles.Moderator)]
        [HttpGet]
        public async Task<IActionResult> Edit(int authorId)
        {
            var author = await _authorService.ObtainAuthor(authorId);
            return View(nameof(Edit), author);
        }

        [Authorize(Roles = UserRoles.Moderator)]
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

        [Authorize(Roles = UserRoles.Moderator)]
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
            return ReturnApi(true, string.Empty, authors);
        }

        #endregion
    }
}
