using BusinessObjectsLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryLayer;

namespace NguyenKhanhMinhMVC.Controllers
{
    public class SystemAccountController : Controller
    {
        private readonly ISystemAccountRepo _systemAccountRepo;

        public SystemAccountController(ISystemAccountRepo systemAccountRepo)
        {
            _systemAccountRepo = systemAccountRepo;
        }

        // GET: SystemAccount - List all accounts (Admin only)
        public async Task<IActionResult> Index()
        {
            var accountRole = HttpContext.Session.GetInt32("AccountRole") ?? -1;
            ViewBag.AccountRole = accountRole;
            ViewBag.AccountName = HttpContext.Session.GetString("AccountName");
            var accounts = await _systemAccountRepo.GetAllAccounts();
            return View(accounts);
        }

        // GET: Profile (for Staff)
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");

            if (accountId == null)
            {
                return RedirectToAction("Login", "SystemAccount");
            }

            var systemAccount = await _systemAccountRepo.GetAccountById(accountId.Value);
            var systemAccountDTO = new SystemAccountDTO
            {
                AccountId = systemAccount.AccountId,
                AccountName = systemAccount.AccountName,
                AccountEmail = systemAccount.AccountEmail,
                AccountRole = systemAccount.AccountRole
            };

            return View(systemAccountDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(SystemAccountDTO systemAccountDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Get the current user's ID from the session
                    var accountId = HttpContext.Session.GetInt32("AccountId");
                    if (accountId == null)
                    {
                        return RedirectToAction("Login", "SystemAccount"); // Redirect to login if not authenticated
                    }
                    await _systemAccountRepo.UpdateAccount(systemAccountDTO);
                    return RedirectToAction("Index", "NewsArticle"); // Redirect to NewsArticle/Index after update
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the profile. Please try again.");
                }
            }

            return View(systemAccountDTO);
        }

        // GET: SystemAccount/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var systemAccount = await _systemAccountRepo.GetAccountById(id);
            if (systemAccount == null)
            {
                return NotFound();
            }
            return View(systemAccount);
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _systemAccountRepo.Login(email, password);
            if (user != null)
            {
                // Store user information in session
                HttpContext.Session.SetInt32("AccountId", user.AccountId);
                HttpContext.Session.SetString("AccountName", user.AccountName ?? "Admin");
                HttpContext.Session.SetInt32("AccountRole", user.AccountRole);

                // Redirect based on user role
                return user.AccountRole switch
                {
                    0 => RedirectToAction("Index", "SystemAccount"), // Admin
                    1 => RedirectToAction("Index", "NewsArticle"),   // Staff
                    2 => RedirectToAction("Index", "NewsArticle"),    // Lecturer
                    _ => RedirectToAction("Index", "Home")
                };
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        // POST: Logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "SystemAccount");
        }

        // GET: Create/Edit Account (Popup Modal)
        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            var systemAccountDTO = new SystemAccountDTO();

            if (id.HasValue)
            {
                var existingAccount = await _systemAccountRepo.GetAccountById(id.Value);
                if (existingAccount != null)
                {
                    systemAccountDTO = new SystemAccountDTO
                    {
                        AccountId = existingAccount.AccountId,
                        AccountName = existingAccount.AccountName,
                        AccountEmail = existingAccount.AccountEmail,
                        AccountRole = existingAccount.AccountRole,
                        AccountPassword = existingAccount.AccountPassword
                    };
                }
            }

            // Load roles for dropdown (if applicable)
            ViewBag.Roles = new SelectList(new List<int> { 0, 1, 2}); // Example roles: 1 = Admin, 2 = Staff, 3 = Lecturer

            return View(systemAccountDTO);
        }

        // POST: SystemAccount/CreateEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(SystemAccountDTO systemAccountDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (systemAccountDTO.AccountId == 0)
                    {
                        // Create new account
                        if (string.IsNullOrEmpty(systemAccountDTO.AccountPassword))
                        {
                            ModelState.AddModelError("", "Password is required.");
                            ViewBag.Roles = new SelectList(new List<int> { 0, 1, 2}, systemAccountDTO.AccountRole);
                            return View(systemAccountDTO);
                        }
                        await _systemAccountRepo.AddAccount(systemAccountDTO);
                    }
                    else
                    {
                        await _systemAccountRepo.UpdateAccount(systemAccountDTO);
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Reload roles if validation fails
            ViewBag.Roles = new SelectList(new List<int> { 0, 1, 2 }, systemAccountDTO.AccountRole);

            return View(systemAccountDTO);
        }

        // GET: Delete Account Confirmation (Popup Modal)
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _systemAccountRepo.GetAccountById(id);
            return PartialView(account);
        }

        // POST: Delete Account
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _systemAccountRepo.DeleteAccount(id);
            return RedirectToAction("Index");
        }
    }
}
