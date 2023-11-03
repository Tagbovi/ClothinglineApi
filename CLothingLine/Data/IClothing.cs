using CLothingLine.Dtos;
using CLothingLine.Models;

namespace CLothingLine.Data
{
    public interface IClothing
    {
        Task<IEnumerable<Clothing>> GetClothings();
        Task <Clothing?> GetClothById (int id);

        
        Task<Clothing?> UpDateClothes (Clothing clothing);
        Task DeleteClothes (int id);
        Task <Clothing>CreateClothes(Clothing clothing);
    }
}
