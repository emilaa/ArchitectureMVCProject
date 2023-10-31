using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.WhyChooseUs;

namespace Web.Areas.Admin.Services.Concrete
{
    public class WhyChooseUsService : IWhyChooseUsService
    {
        private readonly IWhyChooseUsRepository _chooseUsRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public WhyChooseUsService(IWhyChooseUsRepository chooseUsRepository,
            IFileService fileService,
            IActionContextAccessor actionContextAccessor)
        {
            _chooseUsRepository = chooseUsRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> IsExistAsync()
        {
            var isExist = await _chooseUsRepository.GetAsync();
            if (isExist != null) return false;
            return true;

        }

        public async Task<bool> CreateAsync(WhyChooseUsCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _chooseUsRepository.GetAsync();
            if (isExist != null) return false;
            int maxSize = 1000;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "File must be image");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize}kb");
                return false;
            }

            var whyChooseUs = new WhyChooseUs
            {

                Services = model.Services,
                CreatedAt = DateTime.Now,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                Title = model.Title,

            };
            await _chooseUsRepository.CreateAsync(whyChooseUs);
            return true;
        }
        
        public async Task DeleteAsync(int id)
        {
            var whyChooseUs = await _chooseUsRepository.GetAsync(id);
            await _chooseUsRepository.DeleteAsync(whyChooseUs);
        }

        public async Task<WhyChooseUsIndexVM> GetAsync()
        {
            var model = new WhyChooseUsIndexVM
            {
                WhyChooseUs = await _chooseUsRepository.GetAsync()
            };
            return model;
        }

        public async Task<WhyChooseUsUpdateVM> GetUpdateModelAsync(int id)
        {
            var whyChooseUs = await _chooseUsRepository.GetAsync(id);
            var model = new WhyChooseUsUpdateVM
            {

                Description = whyChooseUs.Description,
                Services = whyChooseUs.Services,
                Title = whyChooseUs.Title,
                Id = whyChooseUs.Id
            };
            return model;
        }

        public async Task<bool> UpdateAsync(WhyChooseUsUpdateVM model)
        {
            var whychooseUs = await _chooseUsRepository.GetAsync(model.Id);
            if (whychooseUs == null) return false;
            whychooseUs.Description = model.Description;
            whychooseUs.Services = model.Services;
            whychooseUs.Title = model.Title;
            whychooseUs.ModifiedAt = DateTime.Now;


            if (model.Photo != null)
            {
                int maxSize = 1000;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "File must be image");
                    return false;
                }
                else if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize}kb");
                    return false;
                }
                _fileService.Delete(whychooseUs.PhotoName);
                whychooseUs.PhotoName = await _fileService.UploadAsync(model.Photo);
            }

            await _chooseUsRepository.UpdateAsync(whychooseUs);
            return true;

        }

        public async Task<WhyChooseUsDetailsVM> GetDetailsModelAsync(int id)
        {
            var whyChooseUs = await _chooseUsRepository.GetAsync(id);
            var model = new WhyChooseUsDetailsVM
            {
                Description = whyChooseUs.Description,
                Services = whyChooseUs.Services,
                PhotoName = whyChooseUs.PhotoName,
                Title = whyChooseUs.Title,
                Id = whyChooseUs.Id,
                CreatedAt = whyChooseUs.CreatedAt,
                ModifiedAt = whyChooseUs.ModifiedAt
            };
            return model;
        }
    }
}
