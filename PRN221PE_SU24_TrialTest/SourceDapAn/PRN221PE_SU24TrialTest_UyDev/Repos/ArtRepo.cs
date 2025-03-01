using BOs;
using DAOs;

namespace Repos
{
    public class ArtRepo : IArtRepo
    {
        public Task AddPainting(OilPaintingArt oilPaintingArt)
        {
            return OilPaintingArtDAO.Instance.AddPainting(oilPaintingArt);
        }

        public Task DeletePainting(int id)
        {
            return OilPaintingArtDAO.Instance.DeleteArt(id);
        }

        public Task<List<OilPaintingArt>> GetList()
        {
            return OilPaintingArtDAO.Instance.GetList();
        }

        public Task<OilPaintingArtDAO.PaintingResponse> GetList(string searchTerm, int pageIndex, int pageSize)
        {
            return OilPaintingArtDAO.Instance.GetList(searchTerm, pageIndex, pageSize);
        }

        public Task<OilPaintingArt> GetOilPaintingArtById(int id)
        {
            return OilPaintingArtDAO.Instance.GetOilPaintingArtById(id);
        }

        public Task UpdatePainting(OilPaintingArt oilPaintingArt)
        {
            return OilPaintingArtDAO.Instance.UpdatePainting(oilPaintingArt);
        }
    }
}
