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
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        #region Construtor

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        } 

        #endregion

        #region Métodos de View

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.ObtainCategories();
            return View(nameof(Index), categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(nameof(Edit), new CategoryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryViewModel viewModel)
        {
            var category = await _categoryService.AddCategory(viewModel);

            if (_categoryService.IsSuccessful() == false)
            {
                AddModelError(_categoryService.GetModelErrors());
                return View(nameof(Edit), category);
            }

            return RedirectToAction(nameof(Edit), new { category.CategoryId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int CategoryId)
        {
            var category = await _categoryService.ObtainCategory(CategoryId);
            return View(nameof(Edit), category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel viewModel)
        {
            var category = await _categoryService.UpdateCategory(viewModel);

            if (_categoryService.IsSuccessful() == false)
            {
                AddModelError(_categoryService.GetModelErrors());
                return View(nameof(Edit), category);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int CategoryId)
        {
            await _categoryService.DeleteCategory(CategoryId);

            if (_categoryService.IsSuccessful() == false)
            {
                AddModelError(_categoryService.GetModelErrors());
            }

            return RedirectToAction(nameof(Index));
        } 

        #endregion

        #region Métodos de Api

        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.ObtainCategories();
            return ReturnApi(true, string.Empty, categories);
        }

        #endregion
    }
}
