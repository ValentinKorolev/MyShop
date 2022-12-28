using Microsoft.AspNetCore.Mvc;
using MyShop.ApplicationCore.Entities;
using MyShop.Interfases;
using MyShop.Models;
using MyShop.Services;

namespace MyShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogItemViewModelServices _catalogItemViewModelServices;
        private readonly IRepository<CatalogItem> _catalogItemRepository;

        public CatalogController(
                IRepository<CatalogItem> catalogItemRepository,
                ICatalogItemViewModelServices catalogItemViewModelServices)
        {
            //TODO: replace to ioc approach
            _catalogItemViewModelServices = catalogItemViewModelServices;
            _catalogItemRepository = catalogItemRepository;
    }
        public IActionResult Index()
        {
            var catalogItemsViewModel = _catalogItemRepository.GetAll().Select(item => new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price,                
            }).ToList();

            return View(catalogItemsViewModel);
        }

        public IActionResult Details(int id)
        {
            var item = _catalogItemRepository.GetById(id);

            if (item == null) return RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price,
            };

            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var item = _catalogItemRepository.GetById(id);

            if (item == null) return RedirectToAction("Index");

            var result = new CatalogItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                Price = item.Price,
            };

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CatalogItemViewModel viewModel)
        {
            try
            {
                _catalogItemViewModelServices.UpdateCatalogItem(viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View();
            }
        }
    }
}
