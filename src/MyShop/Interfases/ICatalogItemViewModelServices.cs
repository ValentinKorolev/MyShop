using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Models;

namespace MyShop.Interfases
{
    public interface ICatalogItemViewModelServices
    {
        void UpdateCatalogItem(CatalogItemViewModel catalogItemViewModel);
        Task<CatalogIndexViewModel> GetCatalogItems(int? brandId, int? typeId);
        Task<IEnumerable<SelectListItem>> GetBrands();
        Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
