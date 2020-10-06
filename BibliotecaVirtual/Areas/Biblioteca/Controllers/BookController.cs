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
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        #region Construtor

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        #endregion

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(nameof(Index), null);
        }

        public async Task<IActionResult> GetBooks(string filter)
        {
            var books = await _bookService.ObtainBooks(filter);
            return Json(books);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(nameof(Edit), new BookViewModel());
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BookViewModel viewModel)
        {
            var book = await _bookService.AddBook(viewModel);

            if (_bookService.IsSuccessful() == false)
            {
                AddModelError(_bookService.GetModelErrors());
                return View(nameof(Edit), book);
            }

            return RedirectToAction(nameof(Edit), new { book.BookId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int bookId)
        {
            if(bookId <= 0)
            {
                AddModelError("Livro não encontrado");
                return View(nameof(Edit), new BookViewModel());
            }

            var book = await _bookService.ObtainBook(bookId);
            return View(nameof(Edit), book);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookViewModel viewModel)
        {
            var book = await _bookService.UpdateBook(viewModel);

            if (_bookService.IsSuccessful() == false)
            {
                AddModelError(_bookService.GetModelErrors());
                return View(nameof(Edit), book);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int bookId)
        {
            await _bookService.DeleteBook(bookId);

            if (_bookService.IsSuccessful() == false)
            {
                AddModelError(_bookService.GetModelErrors());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
