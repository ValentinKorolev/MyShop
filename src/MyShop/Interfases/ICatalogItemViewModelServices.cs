using MyShop.Models;

namespace MyShop.Interfases
{
    public interface ICatalogItemViewModelServices
    {
        void UpdateCatalogItem(CatalogItemViewModel catalogItemViewModel);
        Task<IEnumerable<CatalogItemViewModel>> GetCatalogItems();
    }
}
