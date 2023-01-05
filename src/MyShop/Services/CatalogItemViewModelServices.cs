using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IRepository<CatalogBrand> _brandRepository;
        private readonly IRepository<CatalogType> _typeRepository;
        public CatalogItemViewModelServices(IRepository<CatalogItem> catalogItemRepository,
            IAppLogger<CatalogItemViewModelServices> logger, 
            IRepository<CatalogBrand> brandRepository,
            IRepository<CatalogType> typeRepository)
        {
            _catalogItemRepository = catalogItemRepository;
            _logger = logger;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            _logger.LogInformation("Get Brands called");
            var brands = await _brandRepository.GetAllAsync();

            var items = brands
                .Select(brand => new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand})
                .OrderBy(brand=>brand.Text)
                .ToList();

            var allItem = new SelectListItem() {Value =null, Text="All", Selected = true};

            items.Insert(0, allItem);

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            _logger.LogInformation("Get Types called");
            var types = await _typeRepository.GetAllAsync();

            var items = types
                .Select(types => new SelectListItem() { Value = types.Id.ToString(), Text = types.Type })
                .OrderBy(types => types.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };

            items.Insert(0, allItem);

            return items;
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int? brandId, int? typedId)
        {
            var entities =await _catalogItemRepository.GetAllAsync();
            var catalogItems = entities
                .Where(item => (!brandId.HasValue || item.CatalogBrandId== brandId)
                &&(!typedId.HasValue || item.CatalogTypeId ==typedId))
                .Select(item => new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price,
            }).ToList();

            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = catalogItems,
                Brands = (await GetBrands()).ToList(),
                Types = (await GetTypes()).ToList(),
            };

            return vm;
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
