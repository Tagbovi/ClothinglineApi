using Microsoft.EntityFrameworkCore;

namespace CLothingLine.Models
{
    public class ClothingDB : DbContext
    {
        public ClothingDB(DbContextOptions<ClothingDB> options) : base(options)
        { }

       public DbSet<Clothing> ClothingSex { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");



            modelBuilder.Entity<Clothing>().HasData(

                new Clothing
                {
                    Id = 1,
                    Name = "RichClothingline",
                    Color = "Red",
                    Type = "T-Shirts",
                    CustomerServiceReport =  "good can be better" 
                }
                );;

            modelBuilder.Entity<Clothing>().HasData(

                new Clothing
                {
                    Id = 2,
                    Name = "MikeClothes",
                    Color = "Blue",
                    Type = "Trousers",
                    CustomerServiceReport = "bad not okay"
                }
                );



        }

    }
}
