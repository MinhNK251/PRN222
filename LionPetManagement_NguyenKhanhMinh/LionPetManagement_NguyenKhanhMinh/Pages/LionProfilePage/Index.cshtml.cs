using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LionPetManagement_NguyenKhanhMinh.Pages.LionProfilePage
{
    public class IndexModel : PageModel
    {
        private readonly ILionProfileRepository _lionProfileRepository;
        private readonly ILionTypeRepository _lionTypeRepository;

        public IndexModel(ILionProfileRepository lionProfileRepository, ILionTypeRepository lionTypeRepository)
        {
            _lionProfileRepository = lionProfileRepository;
            _lionTypeRepository = lionTypeRepository;
        }

        public IList<LionProfile> LionProfile { get; set; } = default!;

        public string? LionName { get; set; }
        public string? LionTypeName { get; set; }
        public double? Weight { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 3;

        public async Task OnGetAsync(string? lionName, string? lionTypeName, double? weight, int currentPage = 1)
        {
            CurrentPage = currentPage;

            LionName = lionName;
            LionTypeName = lionTypeName;
            Weight = weight;

            var allLionsQuery = _lionProfileRepository.GetLionProfiles().AsQueryable();

            if (!string.IsNullOrEmpty(LionName))
            {
                allLionsQuery = allLionsQuery.Where(m => m.LionName != null && m.LionName.Contains(LionName));
            }
            if (!string.IsNullOrEmpty(LionTypeName))
            {
                allLionsQuery = allLionsQuery.Where(m => m.LionType.LionTypeName != null && m.LionType.LionTypeName.ToString().Contains(LionTypeName));
            }
            if (Weight != null)
            {
                allLionsQuery = allLionsQuery.Where(m => m.Weight != null && m.Weight == Weight);
            }

            var groupedLions = allLionsQuery
                .GroupBy(m => new { m.LionName, m.LionType.LionTypeName, m.Weight })
                .Select(g => g.FirstOrDefault());

            int totalItems = groupedLions.Count();
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            LionProfile = groupedLions
                .OrderByDescending(m => m.LionProfileId)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public JsonResult OnGetLoadData(string? lionName, string? lionTypeName, double? weight, int currentPage = 1)
        {
            CurrentPage = currentPage;

            var query = _lionProfileRepository.GetLionProfiles().AsQueryable();

            if (!string.IsNullOrEmpty(lionName))
            {
                query = query.Where(m => m.LionName != null && m.LionName.Contains(lionName, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(lionTypeName))
            {
                query = query.Where(m => m.LionType.LionTypeName != null && m.LionType.LionTypeName.ToString().Contains(lionTypeName, StringComparison.CurrentCultureIgnoreCase));
            }
            if (weight != null)
            {
                query = query.Where(m => m.Weight == weight);
            }
            string role = HttpContext.Session.GetString("Role");
            if (!("3").Equals(role) || !("2").Equals(role))
                query = _lionProfileRepository.GetLionProfiles().AsQueryable();
            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            var lions = query
                            .OrderByDescending(t => t.LionProfileId)
                            .Skip((currentPage - 1) * PageSize)
                            .Take(PageSize)
                            .Select(t => new
                            {
                                t.LionProfileId,
                                t.LionName,
                                t.Weight,
                                t.Characteristics,
                                t.Warning,
                                t.ModifiedDate,
                                LionTypeName = t.LionType!.LionTypeName!,
                            })
                            .ToList();

            return new JsonResult(new
            {
                lions,
                totalPages,
                currentPage,
            });
        }
    }
}
