using Microsoft.EntityFrameworkCore;
using MyShop.ApplicationCore.Entities;


namespace MyShop.Infastructure.Data
{
    public sealed class CatalogContext : DbContext 
    {
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }
                
    }
}
