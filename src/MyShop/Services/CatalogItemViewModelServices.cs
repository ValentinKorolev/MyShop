using MyShop.ApplicationCore.Entities;
using MyShop.ApplicationCore.Interfaces;
using MyShop.Interfases;
using MyShop.Models;
using System.Data;

namespace MyShop.Services
{
    public sealed class CatalogItemViewModelServices : ICatalogItemViewModelServices
    {
        private readonly IRepository<CatalogItem> _catalogItemRepository;
        private readonly IAppLogger<CatalogItemViewModelServices> _logger;
        public CatalogItemViewModelServices(IRepository<CatalogItem> catalogItemRepository, IAppLogger<CatalogItemViewModelServices> logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _logger= logger;
        }

        public async Task<IEnumerable<CatalogItemViewModel>> GetCatalogItems()
        {
            var entities =await _catalogItemRepository.GetAllAsync();
            var catalogItems = entities.Select(item => new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price,
            }).ToList();

            return catalogItems;
        }

        public void UpdateCatalogItem(CatalogItemViewModel catalogItemViewModel)
        {
            var existingCatalogItem = _catalogItemRepository.GetById(catalogItemViewModel.Id);
            if (existingCatalogItem is null)
            {
                var exception = new Exception($"Catalog otem {catalogItemViewModel.Id} was not found");
                _logger.LogError(exception,exception.Message);
                throw exception;
            } 

            CatalogItem.CatalogItemDetails details = new(catalogItemViewModel.Name, catalogItemViewModel.Price);
            existingCatalogItem.UpdateDetails(details);

            _logger.LogInformation($"Updaiting catalog item {existingCatalogItem.Id} " +
                $"Name {existingCatalogItem.Name} " +
                $"Price {existingCatalogItem.Price}");
            _catalogItemRepository.Update(existingCatalogItem);

        }
    }
}
