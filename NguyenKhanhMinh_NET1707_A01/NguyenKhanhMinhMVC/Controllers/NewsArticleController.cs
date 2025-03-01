using BusinessObjectsLayer.Models;
using BusinessObjectsLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryLayer;

namespace NguyenKhanhMinhMVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ITagRepo _tagRepo;

        public NewsArticleController(INewsArticleRepo newsArticleRepo, ICategoryRepo categoryRepo, ITagRepo tagRepo)
        {
            _newsArticleRepo = newsArticleRepo;
            _categoryRepo = categoryRepo;
            _tagRepo = tagRepo;
        }

        // GET: Public view of active news articles (Public & Lecturer)
        public async Task<IActionResult> Index()
        {
            var accountRole = HttpContext.Session.GetInt32("AccountRole") ?? -1;
            List<NewsArticle> newsArticles;

            if (accountRole == 2) // Lecturer role
            {
                newsArticles = await _newsArticleRepo.GetActiveNewsArticles();
            }
            else
            {
                newsArticles = await _newsArticleRepo.GetAllNewsArticles();
            }

            // Pass account role to the view using ViewBag
            ViewBag.AccountRole = accountRole;
            ViewBag.AccountName = HttpContext.Session.GetString("AccountName");
            return View(newsArticles);
        }


        // GET: News Article Details by ID
        public async Task<IActionResult> Details(int id)
        {
            var newsArticle = await _newsArticleRepo.GetNewsArticleById(id);
            if (newsArticle == null) return NotFound();
            return View(newsArticle);
        }

        // GET: Manage news articles (Admin & Staff)
        public async Task<IActionResult> Manage()
        {
            var newsArticles = await _newsArticleRepo.GetAllNewsArticles();
            return View(newsArticles);
        }

        // GET: Create/Edit News Article (Popup Modal)
        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            var newsArticleDTO = new NewsArticleDTO();

            if (id.HasValue)
            {
                var existingArticle = await _newsArticleRepo.GetNewsArticleById(id.Value);
                if (existingArticle != null)
                {
                    newsArticleDTO = new NewsArticleDTO
                    {
                        NewsArticleId = existingArticle.NewsArticleId,
                        NewsTitle = existingArticle.NewsTitle,
                        Headline = existingArticle.Headline,
                        NewsContent = existingArticle.NewsContent,
                        NewsSource = existingArticle.NewsSource,
                        CategoryId = existingArticle.CategoryId,
                        NewsStatus = existingArticle.NewsStatus,
                        CreatedById = existingArticle.CreatedById,
                        UpdatedById = existingArticle.UpdatedById,
                        CreatedDate = existingArticle.CreatedDate,
                        ModifiedDate = existingArticle.ModifiedDate,
                        SelectedTagIds = existingArticle.Tags?.Select(t => t.TagId).ToArray() ?? Array.Empty<int>()
                    };
                }
            }

            // Load categories and tags for dropdowns
            ViewBag.Categories = new SelectList(await _categoryRepo.GetAllCategories(), "CategoryId", "CategoryName");
            ViewBag.Tags = new MultiSelectList(await _tagRepo.GetAllTags(), "TagId", "TagName", newsArticleDTO.SelectedTagIds);

            return View(newsArticleDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(NewsArticleDTO newsArticleDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int currentUserId = HttpContext.Session.GetInt32("AccountId") ?? 0;
                    int createdBy = newsArticleDTO.NewsArticleId == 0 ? currentUserId : newsArticleDTO.CreatedById;

                    int updatedBy = currentUserId;

                    if (newsArticleDTO.NewsArticleId == 0)
                    {
                        await _newsArticleRepo.AddNewsArticle(newsArticleDTO, createdBy, updatedBy, newsArticleDTO.SelectedTagIds);
                    }
                    else
                    {
                        await _newsArticleRepo.UpdateNewsArticle(newsArticleDTO, createdBy, updatedBy, newsArticleDTO.SelectedTagIds);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Reload categories and tags if validation fails
            ViewBag.Categories = new SelectList(await _categoryRepo.GetAllCategories(), "CategoryId", "CategoryName");
            ViewBag.Tags = new MultiSelectList(await _tagRepo.GetAllTags(), "TagId", "TagName", newsArticleDTO.SelectedTagIds);

            return View(newsArticleDTO);
        }

        // GET: Delete Confirmation (Popup Modal)
        public async Task<IActionResult> Delete(int id)
        {
            var newsArticle = await _newsArticleRepo.GetNewsArticleById(id);

            if (newsArticle == null)
            {
                return NotFound(); // Return 404 if the article doesn't exist
            }

            return View(newsArticle);
        }

        // POST: Delete News Article
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _newsArticleRepo.DeleteNewsArticle(id);
            return RedirectToAction("Index");
        }

        // GET: News History for Current Staff
        public async Task<IActionResult> History(int userId = 1) // Example default user ID
        {
            var history = await _newsArticleRepo.GetNewsByCreator(userId);
            ViewBag.UserId = userId; // Pass the user ID to the view
            ViewBag.AccountRole = HttpContext.Session.GetInt32("AccountRole") ?? -1;
            ViewBag.AccountName = HttpContext.Session.GetString("AccountName") ?? "Unknown";
            return View(history);
        }

        //// GET: Report Generation
        //public IActionResult Report()
        //{
        //    return View();
        //}

        //// POST: Generate Report by Date Range
        //[HttpPost]
        //public async Task<IActionResult> GenerateReport(DateTime startDate, DateTime endDate)
        //{
        //    var report = await _newsArticleRepo.GenerateReport(startDate, endDate);
        //    return View("Report", report);
        //}
    }
}
