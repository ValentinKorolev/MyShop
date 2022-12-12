using MyShop.Interfases;
using MyShop.Models;

namespace MyShop.Services
{
    public sealed class CatalogItemViewModelServices : ICatalogItemViewModelServices
    {
        private readonly IRepository<CatalogItem> _catalogItemRepository;

        public CatalogItemViewModelServices(IRepository<CatalogItem> catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public void UpdateCatalogItem(CatalogItemViewModel catalogItemViewModel)
        {
            var existingCatalogItem = _catalogItemRepository.GetById(catalogItemViewModel.Id);
            if(existingCatalogItem is null) throw new Exception($"Catalog otem {catalogItemViewModel.Id} was not found");

            CatalogItem.CatalogItemDetails details = new(catalogItemViewModel.Name, catalogItemViewModel.Price);
            existingCatalogItem.UpdateDetails(details);
            _catalogItemRepository.Update(existingCatalogItem);

        }
    }
}
