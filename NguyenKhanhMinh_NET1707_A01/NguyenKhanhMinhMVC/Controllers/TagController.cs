using BusinessObjectsLayer.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;

namespace NguyenKhanhMinhMVC.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepo _tagRepo;

        public TagController(ITagRepo tagRepo)
        {
            _tagRepo = tagRepo;
        }

        // GET: List all tags
        public async Task<IActionResult> Index()
        {
            var tags = await _tagRepo.GetAllTags();
            return View(tags);
        }

        // GET: Create/Edit Tag (Popup Modal)
        public async Task<IActionResult> CreateEdit(int? id)
        {
            var tag = id.HasValue
                ? await _tagRepo.GetTagById(id.Value)
                : new Tag();

            return PartialView(tag);
        }

        // POST: Create/Edit Tag
        [HttpPost]
        public async Task<IActionResult> CreateEdit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                if (tag.TagId == 0)
                    await _tagRepo.AddTag(tag);
                else
                    await _tagRepo.UpdateTag(tag);

                return RedirectToAction("Index");
            }
            return PartialView(tag);
        }

        // GET: Delete Confirmation (Popup Modal)
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _tagRepo.GetTagById(id);
            return PartialView(tag);
        }

        // POST: Delete Tag
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _tagRepo.DeleteTag(id);
            return RedirectToAction("Index");
        }
    }
}
