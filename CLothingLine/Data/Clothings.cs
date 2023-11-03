using CLothingLine.Models;
using Microsoft.EntityFrameworkCore;

namespace CLothingLine.Data
{
    public class Clothings : IClothing
    {
        private readonly ClothingDB _db;
        public Clothings(ClothingDB db) { this._db = db; }


        public async Task<Clothing> CreateClothes(Clothing clothes)
        {
           var result= await _db.ClothingSex.AddAsync(clothes);
            _db.SaveChanges();
            return result.Entity;
        }

        public async Task DeleteClothes(int id)
        {
            var result= await _db.ClothingSex.FirstOrDefaultAsync(e=>e.Id==id);
            if (result != null)
            {
               _db.ClothingSex.Remove(result);
               await _db.SaveChangesAsync();
            }
            
            
        }

        public async Task<Clothing?> GetClothById(int id)
        {
            return await _db.ClothingSex.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Clothing>> GetClothings()
        {
            return await _db.ClothingSex.ToListAsync();
        }

        public async Task<Clothing?> UpDateClothes(Clothing clothings)
        {
            var result = await _db.ClothingSex.FirstOrDefaultAsync(e => e.Id == clothings.Id);
          if(result != null) { 
            result.Name = clothings.Name;
                result.Color = clothings.Color;
                result.Type = clothings.Type;   
                result.Id= clothings.Id;  
                result.CustomerServiceReport= clothings.CustomerServiceReport;

                await _db.SaveChangesAsync();
                return result;
            
          }
            return null;
        }

      
    }
}
