using BOs;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class OilPaintingArtDAO
    {
        private readonly OilPaintingArt2024DBContext _context;
        private static OilPaintingArtDAO instance = null;

        private OilPaintingArtDAO()
        {
            _context = new OilPaintingArt2024DBContext();
        }

        public static OilPaintingArtDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    return new OilPaintingArtDAO();
                }
                return instance;
            }
        }

        public async Task<List<OilPaintingArt>> GetList()
        {
            return await _context.OilPaintingArts.Include(x => x.Supplier).ToListAsync();
        }

        public class PaintingResponse
        {
            public List<OilPaintingArt> OilPaintingArts { get; set; }
            public int TotalPages { get; set; }
            public int PageIndex { get; set; }
        }

        public async Task<PaintingResponse> GetList(string searchTerm, int pageIndex, int pageSize)
        {
            var query = _context.OilPaintingArts.Include(x => x.Supplier).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.OilPaintingArtStyle.ToLower().Contains(searchTerm.ToLower()) || x.Artist.ToLower().Contains(searchTerm.ToLower()));
            }

            int count = await query.CountAsync(); //11
            int totalPages = (int)Math.Ceiling(count / (double)pageSize); //3

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PaintingResponse
            {
                OilPaintingArts = await query.ToListAsync(),
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }

        public async Task<OilPaintingArt> GetOilPaintingArtById(int id)
        {
            return await _context.OilPaintingArts.Include(x => x.Supplier).FirstOrDefaultAsync(m => m.OilPaintingArtId == id);
        }

        public async Task AddPainting(OilPaintingArt oilPaintingArt)
        {
            try
            {
                var existingArt = await GetOilPaintingArtById(oilPaintingArt.OilPaintingArtId);
                if (existingArt != null)
                {
                    throw new Exception("Art already exists");
                }
                _context.OilPaintingArts.Add(oilPaintingArt);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdatePainting(OilPaintingArt oilPaintingArt)
        {
            try
            {
                var existingArt = await GetOilPaintingArtById(oilPaintingArt.OilPaintingArtId);
                if (existingArt == null)
                {
                    throw new Exception("Does not exist");
                }

                existingArt.ArtTitle = oilPaintingArt.ArtTitle;
                existingArt.OilPaintingArtLocation = oilPaintingArt.OilPaintingArtLocation;
                existingArt.OilPaintingArtStyle = oilPaintingArt.OilPaintingArtStyle;
                existingArt.Artist = oilPaintingArt.Artist;
                existingArt.NotablFeatures = oilPaintingArt.NotablFeatures;
                existingArt.PriceOfOilPaintingArt = oilPaintingArt.PriceOfOilPaintingArt;
                existingArt.StoreQuantity = oilPaintingArt.StoreQuantity;
                existingArt.CreatedDate = oilPaintingArt.CreatedDate; //DateTime.Now;

                var supplier = await _context.SupplierCompanies.FirstOrDefaultAsync(s => s.SupplierId == oilPaintingArt.SupplierId);
                if (supplier == null)
                {
                    throw new Exception("Supplier does not exist");
                }
                existingArt.SupplierId = oilPaintingArt.SupplierId;

                _context.OilPaintingArts.Update(existingArt);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteArt(int id)
        {
            try
            {
                var existArt = _context.OilPaintingArts.FirstOrDefault(m => m.OilPaintingArtId == id);
                if (existArt == null)
                {
                    throw new Exception("Art not found");
                }
                _context.OilPaintingArts.Remove(existArt);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<SupplierCompany>> GetSuppilers()
        {
            return await _context.SupplierCompanies.ToListAsync();
        }
    }
}
