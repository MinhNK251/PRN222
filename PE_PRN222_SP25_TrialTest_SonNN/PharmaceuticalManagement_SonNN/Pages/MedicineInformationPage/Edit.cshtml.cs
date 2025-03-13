using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Sp25PharmaceuticalDB_BusinessObjects;
using Sp25PharmaceuticalDB_Repository;

namespace PharmaceuticalManagement_SonNN.Pages.MedicineInformationPage
{
    public class EditModel : PageModel
    {
        private readonly IMedicineInformationRepository _medicineInformationRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IHubContext<SignalrServer> _hubContext;

        public EditModel(IMedicineInformationRepository medicineInformationRepository, IManufacturerRepository manufacturerRepository, IHubContext<SignalrServer> hubContext)
        {
            _medicineInformationRepository = medicineInformationRepository;
            _manufacturerRepository = manufacturerRepository;
            _hubContext = hubContext;
        }

        [BindProperty]
        public MedicineInformation MedicineInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var medicineinformation =   _medicineInformationRepository.GetMedicineInformationById(id);
            if (medicineinformation == null)
            {
                return NotFound();
            }
            MedicineInformation = medicineinformation;
            ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetManufacturers(), "ManufacturerId", "ManufacturerName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ManufacturerId"] = new SelectList(_manufacturerRepository.GetManufacturers(), "ManufacturerId", "ManufacturerName");
                return Page();
            }
            try
            {
                _medicineInformationRepository.UpdateMedicineInformation(MedicineInformation.MedicineId, MedicineInformation);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_medicineInformationRepository.GetMedicineInformationById(MedicineInformation.MedicineId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
