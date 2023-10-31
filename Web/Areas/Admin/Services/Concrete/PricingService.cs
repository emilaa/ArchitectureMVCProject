using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Pricing;

namespace Web.Areas.Admin.Services.Concrete
{
    public class PricingService : IPricingService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IPricingRepository _pricingRepository;

        public PricingService(IActionContextAccessor actionContextAccessor,
            IPricingRepository pricingRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _pricingRepository = pricingRepository;
        }
        public async Task<bool> CreateAsync(PricingCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _pricingRepository.AnyAsync(p => p.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }

            var pricing = new Pricing
            {
                ActiveStatus = model.ActiveStatus,
                CreatedAt = DateTime.Now,
                Features = model.Features,
                SubTitle = model.SubTitle,
                Price = model.Price,
                PricePer = model.PricePer,
                Title = model.Title
            };
            await _pricingRepository.CreateAsync(pricing);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var pricing=await _pricingRepository.GetAsync(id);
            await _pricingRepository.DeleteAsync(pricing);
        }

        public async Task<PricingIndexVM> GetAllAsync()
        {
            var pricing = await _pricingRepository.GetAllAsync();
            var model = new PricingIndexVM
            {
                Pricings = pricing
            };
            return model;
        }

        public async Task<PricingUpdateVM> GetUpdateModelAsync(int id)
        {
            var pricing = await _pricingRepository.GetAsync(id);
            var model = new PricingUpdateVM
            {
                Id = pricing.Id,
                ActiveStatus = pricing.ActiveStatus,
                Features = pricing.Features,
                SubTitle = pricing.SubTitle,
                Price = pricing.Price,
                PricePer = pricing.PricePer,
                Title = pricing.Title
            };
            return model;
        }

        public async Task<bool> UpdateAsync(PricingUpdateVM model)
        {
            var pricing = await _pricingRepository.GetAsync(model.Id);
            if (!_modelState.IsValid) return false;
            var isExist = await _pricingRepository.AnyAsync(p => p.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != pricing.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }

            pricing.PricePer = model.PricePer;
            pricing.Price = model.Price;
            pricing.ActiveStatus = model.ActiveStatus;
            pricing.ModifiedAt = DateTime.Now;
            pricing.Features = model.Features;
            pricing.SubTitle = model.SubTitle;
            pricing.Title = model.Title;

            await _pricingRepository.UpdateAsync(pricing);
            return true;
        }
    }
}
