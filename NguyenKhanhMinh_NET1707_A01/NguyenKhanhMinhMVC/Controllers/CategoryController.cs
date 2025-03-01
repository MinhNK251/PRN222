using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryLayer;

namespace NguyenKhanhMinhMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            var accountRole = HttpContext.Session.GetInt32("AccountRole") ?? -1;
            ViewBag.AccountRole = accountRole;
            ViewBag.AccountName = HttpContext.Session.GetString("AccountName");
            var categories = await _categoryRepo.GetAllCategories();
            return View(categories);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryRepo.GetCategoryById(id);
            return View(category);
        }

        // GET: Category/CreateEdit
        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            var categoryDTO = new CategoryDTO();

            if (id.HasValue)
            {
                var existingCategory = await _categoryRepo.GetCategoryById(id.Value);
                if (existingCategory != null)
                {
                    categoryDTO = new CategoryDTO
                    {
                        CategoryId = existingCategory.CategoryId,
                        CategoryName = existingCategory.CategoryName,
                        CategoryDescription = existingCategory.CategoryDescription,
                        ParentCategoryId = existingCategory.ParentCategoryId,
                        IsActive = existingCategory.IsActive
                    };
                }
            }

            // Load parent categories for dropdown
            ViewBag.ParentCategories = new SelectList(await _categoryRepo.GetAllCategories(), "CategoryId", "CategoryName");

            return View(categoryDTO);
        }

        // POST: Category/CreateEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (categoryDTO.CategoryId == 0)
                    {
                        // Create new category
                        await _categoryRepo.AddCategory(categoryDTO);
                    }
                    else
                    {
                        // Update existing category
                        await _categoryRepo.UpdateCategory(categoryDTO);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Reload parent categories if validation fails
            ViewBag.ParentCategories = new SelectList(await _categoryRepo.GetAllCategories(), "CategoryId", "CategoryName");

            return View(categoryDTO);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepo.GetCategoryById(id);

            if (category == null)
            {
                return NotFound(); // Return 404 if the category doesn't exist
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _categoryRepo.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
