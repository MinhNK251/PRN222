using BOs;
using static DAOs.OilPaintingArtDAO;

namespace Repos
{
    public interface IArtRepo
    {
        Task<List<OilPaintingArt>> GetList();

        Task<PaintingResponse> GetList(string searchTerm, int pageIndex, int pageSize);
        Task<OilPaintingArt> GetOilPaintingArtById(int id);
        Task AddPainting(OilPaintingArt oilPaintingArt);
        Task UpdatePainting(OilPaintingArt oilPaintingArt);
        Task DeletePainting(int id);
    }
}
