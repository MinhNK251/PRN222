using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjectLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using RepositoryLayer;

namespace PharmaceuticalManagement_NguyenKhanhMinh.Pages.MedicineInformationPage
{
    public class CreateModel : PageModel
    {
        private readonly IMedicineInformationRepository _medicineInformationRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IHubContext<SignalrServer> _hubContext;

        public CreateModel(IMedicineInformationRepository medicineInformationRepository, IManufacturerRepository manufacturerRepository, IHubContext<SignalrServer> hubContext)
        {
            _medicineInformationRepository = medicineInformationRepository;
            _manufacturerRepository = manufacturerRepository;
            _hubContext = hubContext;
        }

        public IActionResult OnGet()
        {
            ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetManufacturers(), "ManufacturerId", "ManufacturerName");
            return Page();
        }

        [BindProperty]
        public MedicineInformation MedicineInformation { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(MedicineInformation.MedicineId))
            {
                var latestMedicineId = _medicineInformationRepository.GetMedicineInformations()
                    .OrderByDescending(m => m.MedicineId)
                    .Select(m => m.MedicineId)
                    .FirstOrDefault();

                int nextIdNumber = 1;
                if (!string.IsNullOrEmpty(latestMedicineId) && latestMedicineId.StartsWith("MI"))
                {
                    int.TryParse(latestMedicineId.Substring(2), out nextIdNumber);
                    nextIdNumber++;
                }

                MedicineInformation.MedicineId = $"MI{nextIdNumber:D6}";
            }

            ModelState.Remove("MedicineInformation.MedicineId");

            if (!ModelState.IsValid)
            {
                ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetManufacturers(), "ManufacturerId", "ManufacturerName");
                return Page();
            }

            try
            {
                _medicineInformationRepository.AddMedicineInformation(MedicineInformation);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the medicine.");
                return Page();
            }

            return RedirectToPage("./Index");
        }

    }
}
