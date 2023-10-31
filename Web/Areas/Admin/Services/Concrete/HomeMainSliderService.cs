using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Org.BouncyCastle.Asn1.X509;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeMainSliderService : IHomeMainSliderService
    {
        private readonly IHomeMainSliderRepository _homeRepository;
        private readonly IFileService _fileservice;

        private readonly ModelStateDictionary _modelState;

        public HomeMainSliderService(IHomeMainSliderRepository homeRepository,
            IActionContextAccessor actionContextAccessor,
            IFileService fileservice)
        {
            _fileservice = fileservice;
            _homeRepository = homeRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<HomeMainSliderIndexVM> GetAllAsync()
        {
            var model = new HomeMainSliderIndexVM
            {
                HomeMainSliders = await _homeRepository.GetAllAsync()
            };
            return model;
        }
        public async Task<bool> UpdateAsync(HomeMainSliderUpdateVM model)
        {
            var sliderUpdate = await _homeRepository.GetAsync(model.Id);
            var isExist = await _homeRepository.AnyAsync(ms => ms.Title.Trim().ToLower() == model.Title.Trim().ToLower() && ms.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This slider already created");
                return false;
            }
            sliderUpdate.ButtonLink = model.ButtonLink;
            sliderUpdate.Slogan = model.Slogan;
            sliderUpdate.Order = model.Order;
            sliderUpdate.Title = model.Title;
            sliderUpdate.ModifiedAt = DateTime.Now;



            if (model.Photo != null)
            {
                int maxSize = 1000;
                if (!_fileservice.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", $"File must be image");
                    return false;
                }
                else if (!_fileservice.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"File size must be less than {maxSize}kb");
                    return true;
                }
                _fileservice.Delete(sliderUpdate.PhotoName);
                sliderUpdate.PhotoName = await _fileservice.UploadAsync(model.Photo);
            }

            await _homeRepository.UpdateAsync(sliderUpdate);
            return true;
        }
        public async Task<bool> CreateAsync(HomeMainSliderCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _homeRepository.AnyAsync(ms => ms.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This slider already created");
                return false;
            }
            int maxSize = 1000;

            if (!_fileservice.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photos", $"{model.Photo.Name} must be image");
                return false;
            }
            else if (!_fileservice.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photos", $"{model.Photo.Name} must be less than {maxSize}kb");
                return false;
            }
            var count = await _homeRepository.GetAllAsync();

            int order = count.Count();
            if (order == 0)
            {
                order = 1;
            }
            else
            {
                order++;
            }
            var mainSlider = new HomeMainSlider
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                ButtonLink = model.ButtonLink,
                Slogan = model.Slogan,
                Order = order,
                PhotoName = await _fileservice.UploadAsync(model.Photo)
            };

            await _homeRepository.CreateAsync(mainSlider);
            return true;
        }
        public async Task DeleteAsync(int id)
        {
            var mainSlider = await _homeRepository.GetAsync(id);
            if (mainSlider != null)
            {
                _fileservice.Delete(mainSlider.PhotoName);
                await _homeRepository.DeleteAsync(mainSlider);
            }
        }
        public async Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id)
        {
            var slider = await _homeRepository.GetAsync(id);
            var model = new HomeMainSliderUpdateVM
            {
                Id = slider.Id,
                Slogan = slider.Slogan,
                ButtonLink = slider.ButtonLink,
                Order = slider.Order,
                Title = slider.Title
            };
            return model;
        }

        public async Task<HomeMainSliderDetailsVM> GetDetailsAsync(int id)
        {
            var slider = await _homeRepository.GetAsync(id);
            var model = new HomeMainSliderDetailsVM
            {
                Id = slider.Id,
                ButtonLink = slider.ButtonLink,
                Order = slider.Order,
                Slogan = slider.Slogan,
                CreatedAt = slider.CreatedAt,
                ModifiedAt = slider.ModifiedAt,
                Title = slider.Title,
                PhotoName = slider.PhotoName
            };
            return model;
        }
    }


}

