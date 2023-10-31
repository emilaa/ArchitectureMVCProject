using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.AccessControl;
using Ubiety.Dns.Core.Records;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.About;
using Web.Areas.Admin.ViewModels.AboutUs;
using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Services.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IAboutPhotoRepository _aboutPhotoRepository;
        private readonly IAboutRepository _aboutRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public AboutService(IAboutPhotoRepository aboutPhotoRepository,
            IAboutRepository aboutRepository,
            IActionContextAccessor contextAccessor,
            IFileService fileService)
        {
            _aboutPhotoRepository = aboutPhotoRepository;
            _aboutRepository = aboutRepository;
            _fileService = fileService;
            _modelState = contextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(AboutCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _aboutRepository.GetAsync();
            if (isExist != null)
            {
                _modelState.AddModelError("Title", "You can create this content only one ");
                return false;
            }
            int maxSize = 1000;
            if (!_fileService.CheckPhoto(model.IconPhoto))
            {
                _modelState.AddModelError("IconPhoto", "File must be image");
                return false;
            }
            else if (!_fileService.MaxSize(model.IconPhoto, maxSize))
            {
                _modelState.AddModelError("IconPhoto", $"Photo size must less than {maxSize}kb");
                return false;
            }

            var about = new About
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                Header = model.Header,
                IconName = await _fileService.UploadAsync(model.IconPhoto)
            };

            await _aboutRepository.CreateAsync(about);

            bool hasError = false;
            int order = 1;
            foreach (var photo in model.Photos)
            {
                if (!_fileService.CheckPhoto(photo))
                {
                    _modelState.AddModelError("Photo", $"{photo.Name} must be image");
                    hasError = true;
                }
                else if (!_fileService.MaxSize(photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"{photo.Name} size must be less than {maxSize}kb");
                    hasError = true;
                }
            }
            if (hasError) return false;

            foreach (var checkePhoto in model.Photos)
            {
                var aboutPhoto = new AboutPhoto
                {
                    Order = order++,
                    AboutId = about.Id,
                    CreatedAt = DateTime.Now,
                    PhotoName = await _fileService.UploadAsync(checkePhoto)
                };
                await _aboutPhotoRepository.CreateAsync(aboutPhoto);
            }
            return true;

        }

        public async Task DeleteAsync(int id)
        {
            var about = await _aboutRepository.GetAsync(id);
            if (about != null)
                await _aboutRepository.DeleteAsync(about);
        }

        public async Task DeletePhotoAsync(int id)
        {
            var aboutPhoto = await _aboutPhotoRepository.GetAsync(id);
            await _aboutPhotoRepository.DeleteAsync(aboutPhoto);
        }

        public async Task<AboutIndexVM> GetAsync()
        {
            var about = await _aboutRepository.GetAsync();
            var model = new AboutIndexVM
            {
                About = about,
                Photos = await _aboutPhotoRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<AboutDetailsVM> GetDetailsAsync(int id)
        {
            var aboutUs = await _aboutRepository.GetAsync(id);

            var model = new AboutDetailsVM
            {
                CreatedAt = aboutUs.CreatedAt,
                Description = aboutUs.Description,
                ModifiedAt = aboutUs.ModifiedAt,
                Header = aboutUs.Header,
                Id = aboutUs.Id,
                IconPhoto = aboutUs.IconName,
                Title = aboutUs.Title,
                Photos = await _aboutPhotoRepository.GetAllAsync()
            };
            return model;

        }

        public async Task<AboutUpdateVM> GetUpdateModelAsync(int id)
        {
            var about = await _aboutRepository.GetAsync(id);
            var model = new AboutUpdateVM
            {
                Id = about.Id,
                Description = about.Description,
                Header = about.Header,
                Title = about.Title,
                AboutPhoto = await _aboutPhotoRepository.GetAllAsync(),
            };
            return model;
        }

        public async Task<AboutPhotoUpdateVM> GetUpdatePhotoModelAsync(int id)
        {
            var aboutPhoto = await _aboutPhotoRepository.GetAsync(id);
            var model = new AboutPhotoUpdateVM
            {
                Id = aboutPhoto.Id,
                Order = aboutPhoto.Order,
                AboutId = aboutPhoto.AboutId,
            };
            return model;
        }

        public async Task<bool> IsExistAsync()
        {
            var isExist = await _aboutRepository.GetAsync();
            if (isExist == null) return false;
            return true;
        }

        public async Task<bool> UpdateAsync(AboutUpdateVM model)
        {
            var aboutPhoto = await _aboutPhotoRepository.GetAllAsync();
            if (!_modelState.IsValid) return false;
            var about = await _aboutRepository.GetAsync(model.Id);
            if (about == null) return false;

            about.Description = model.Description;
            about.Title = model.Title;
            about.ModifiedAt = DateTime.Now;
            model.AboutPhoto = aboutPhoto;
            about.Header = model.Header;


            int maxSize = 1000;

            if (model.IconPhoto != null)
            {

                if (!_fileService.CheckPhoto(model.IconPhoto))
                {
                    _modelState.AddModelError("IconPhoto", "File must be image");
                    return false;
                }
                else if (!_fileService.MaxSize(model.IconPhoto, maxSize))
                {
                    _modelState.AddModelError("IconPhoto", $"Photo size must be less than {maxSize}kb");
                    return false;
                }
                _fileService.Delete(about.IconName);
                about.IconName = await _fileService.UploadAsync(model.IconPhoto);
            }
            if (model.Photos != null)
            {
                bool hasError = false;
                foreach (var photo in model.Photos)
                {
                    if (!_fileService.CheckPhoto(photo))
                    {
                        _modelState.AddModelError("Photos", $"{photo.FileName} must be image");
                        hasError = true;
                    }
                    else if (!_fileService.MaxSize(photo, maxSize))
                    {
                        _modelState.AddModelError("Photos", $"{photo.FileName} size must be less than {maxSize}kb");
                        hasError = true;
                    }
                }
                if (hasError) return false;
                int order = 1;
                foreach (var photo in model.Photos)
                {
                    var aboutPhotos = new AboutPhoto
                    {
                        AboutId = about.Id,
                        Order = order++,
                        CreatedAt = DateTime.Now,
                        PhotoName = await _fileService.UploadAsync(photo),
                    };
                    await _aboutPhotoRepository.CreateAsync(aboutPhotos);
                }
            }
            await _aboutRepository.UpdateAsync(about);
            return true;

        }

        public async Task<bool> UpdatePhotoAsync(AboutPhotoUpdateVM model)
        {
            var aboutPhoto = await _aboutPhotoRepository.GetAsync(model.Id);
            aboutPhoto.Order = model.Order;
            model.AboutId = aboutPhoto.AboutId;
            aboutPhoto.ModifiedAt = DateTime.Now;
            await _aboutPhotoRepository.UpdateAsync(aboutPhoto);
            return true;
        }
    }
}
