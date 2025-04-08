using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class ProductUpdateDTO : ProductCreateDTO
    {
        public int ProductId { get; set; }
    }
}