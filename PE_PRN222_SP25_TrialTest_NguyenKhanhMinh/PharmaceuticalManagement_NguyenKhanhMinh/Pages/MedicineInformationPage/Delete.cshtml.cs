using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjectLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;

namespace PharmaceuticalManagement_NguyenKhanhMinh.Pages.MedicineInformationPage
{
    public class DeleteModel : PageModel
    {
        private readonly IMedicineInformationRepository _medicineInformationRepository;
        private readonly IHubContext<SignalrServer> _hubContext;

        public DeleteModel(IMedicineInformationRepository medicineInformationRepository, IHubContext<SignalrServer> hubContext)
        {
            _medicineInformationRepository = medicineInformationRepository; 
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

            var medicineinformation = _medicineInformationRepository.GetMedicineInformationById(id);

            if (medicineinformation == null)
            {
                return NotFound();
            }
            else
            {
                MedicineInformation = medicineinformation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicineinformation = _medicineInformationRepository.GetMedicineInformationById(id);
            if (medicineinformation != null)
            {
                _medicineInformationRepository.RemoveMedicineInformation(id);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }

            return RedirectToPage("./Index");
        }
    }
}
