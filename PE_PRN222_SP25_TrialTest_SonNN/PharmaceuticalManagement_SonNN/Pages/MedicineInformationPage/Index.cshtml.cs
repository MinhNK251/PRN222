using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sp25PharmaceuticalDB_BusinessObjects;
using Sp25PharmaceuticalDB_Repository;

namespace PharmaceuticalManagement_SonNN.Pages.MedicineInformationPage
{
    public class IndexModel : PageModel
    {
        private readonly IMedicineInformationRepository _medicineInformationRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public IndexModel(IMedicineInformationRepository medicineInformationRepository, IManufacturerRepository manufacturerRepository)
        {
            _medicineInformationRepository = medicineInformationRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public IList<MedicineInformation> MedicineInformation { get; set; } = default!;

        public string? SearchActiveIngredients { get; set; }
        public string? SearchExpirationDate { get; set; }
        public string? SearchWarnings { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 3;

        public async Task OnGetAsync(string? searchActiveIngredients, string? searchExpirationDate, string? searchWarnings, int currentPage = 1)
        {
            CurrentPage = currentPage;

            SearchActiveIngredients = searchActiveIngredients;
            SearchExpirationDate = searchExpirationDate;
            SearchWarnings = searchWarnings;

            var allMedicinesQuery = _medicineInformationRepository.GetMedicineInformations().AsQueryable();

            if (!string.IsNullOrEmpty(SearchActiveIngredients))
            {
                allMedicinesQuery = allMedicinesQuery.Where(m => m.ActiveIngredients != null && m.ActiveIngredients.Contains(SearchActiveIngredients));
            }
            if (!string.IsNullOrEmpty(SearchExpirationDate))
            {
                allMedicinesQuery = allMedicinesQuery.Where(m => m.ExpirationDate != null && m.ExpirationDate.ToString().Contains(SearchExpirationDate));
            }
            if (!string.IsNullOrEmpty(SearchWarnings))
            {
                allMedicinesQuery = allMedicinesQuery.Where(m => m.WarningsAndPrecautions != null && m.WarningsAndPrecautions.Contains(SearchWarnings));
            }

            var groupedMedicines = allMedicinesQuery
                .GroupBy(m => new { m.ActiveIngredients, m.ExpirationDate, m.WarningsAndPrecautions })
                .Select(g => g.FirstOrDefault());

            int totalItems = groupedMedicines.Count();
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            MedicineInformation = groupedMedicines
                .OrderByDescending(m => m.MedicineId)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public JsonResult OnGetLoadData(string? searchActiveIngredients, string? searchExpirationDate, string? searchWarnings, int currentPage = 1)
        {
            CurrentPage = currentPage;

            var query = _medicineInformationRepository.GetMedicineInformations().AsQueryable();

            if (!string.IsNullOrEmpty(searchActiveIngredients))
            {
                query = query.Where(m => m.ActiveIngredients != null && m.ActiveIngredients.Contains(searchActiveIngredients));
            }

            if (!string.IsNullOrEmpty(searchExpirationDate))
            {
                query = query.Where(m => m.ExpirationDate != null && m.ExpirationDate.ToString().Contains(searchExpirationDate));
            }

            if (!string.IsNullOrEmpty(searchWarnings))
            {
                query = query.Where(m => m.WarningsAndPrecautions != null && m.WarningsAndPrecautions.Contains(searchWarnings));
            }

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            var medicines = query
                            .OrderByDescending(t => t.MedicineId)
                            .Skip((currentPage - 1) * PageSize)
                            .Take(PageSize)
                            .Select(t => new
                            {
                                t.MedicineId,
                                t.MedicineName,
                                t.ActiveIngredients,
                                t.ExpirationDate,
                                t.DosageForm,
                                t.WarningsAndPrecautions,
                                ManufacturerName = t.Manufacturer!.ManufacturerName!,
                            })
                            .ToList();

            return new JsonResult(new
            {
                medicines,
                totalPages,
                currentPage,
            });
        }
    }
}
