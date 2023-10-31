

using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Services.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IFileService fileService,
            IDoctorRepository doctorRepository,
            IActionContextAccessor actionContextAccessor
            )
        {
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _doctorRepository = doctorRepository;
        }
        public async Task<bool> CreateAsync(DoctorCreateVM model, List<string> SkillsList)
        {
            if (!_modelState.IsValid) return false;
            int maxSize = 1000;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "File must be image");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"File size must be less than {maxSize}kb");
                return false;
            }
            string skills = string.Join(",", SkillsList);
            var doctor = new Doctor
            {
                CreatedAt = DateTime.Now,
                Facebook = model.Facebook,
                FullName = model.FullName,
                LinkedIn = model.LinkedIn,
                Position = model.Position,
                Twitter = model.Twitter,
                ShowInHome = model.ShowInHome,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                Skills = skills,
                Email = model.Email,
                Introducing = model.Introducing,
                MondayToFridayStart = model.MondayToFridayStart,
                MondayToFridayEnd = model.MondayToFridayEnd,
                SaturdayStart = model.SaturdayStart,
                SaturdayEnd = model.SaturdayEnd,
                ShortInfo = model.ShortInfo,
                Phone = model.Phone,
                SundayIsWorking = model.SundayIsWorking,
                Qualification = model.Qualification,

            };

            await _doctorRepository.CreateAsync(doctor);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            _fileService.Delete(doctor.PhotoName);
            await _doctorRepository.DeleteAsync(doctor);
        }

        public async Task<DoctorIndexVM> GetAllAsync()
        {
            var model = new DoctorIndexVM
            {
                Doctors = await _doctorRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<DoctorDetailsVM> GetDetailsModelAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            var model = new DoctorDetailsVM
            {
                Id = doctor.Id,
                SaturdayEnd = doctor.SaturdayEnd,
                SaturdayStart = doctor.SaturdayStart,
                ShortInfo = doctor.ShortInfo,
                Skills = doctor.Skills,
                SundayIsWorking = doctor.SundayIsWorking,
                MondayToFridayStart = doctor.MondayToFridayStart,
                MondayToFridayEnd = doctor.MondayToFridayEnd,
                ShowInHome = doctor.ShowInHome,
                Qualification = doctor.Qualification,
                Email = doctor.Email,
                FullName = doctor.FullName,
                Facebook = doctor.Facebook,
                LinkedIn = doctor.LinkedIn,
                Introducing = doctor.Introducing,
                Phone = doctor.Phone,
                Position = doctor.Position,
                Twitter = doctor.Twitter,
                PhotoName = doctor.PhotoName,
                CreatedAt = doctor.CreatedAt,
                ModifiedAt = doctor.ModifiedAt
            };
            return model;
        }
        public async Task<DoctorUpdateVM> GetUpdateModelAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            var model = new DoctorUpdateVM
            {
                FullName = doctor.FullName,
                ShowInHome = doctor.ShowInHome,
                Facebook = doctor.Facebook,
                LinkedIn = doctor.LinkedIn,
                Position = doctor.Position,
                Twitter = doctor.Twitter,
                Id = doctor.Id,
                Email = doctor.Email,
                Introducing = doctor.Introducing,
                MondayToFridayEnd = doctor.MondayToFridayEnd,
                MondayToFridayStart = doctor.MondayToFridayStart,
                Phone = doctor.Phone,
                SaturdayEnd = doctor.SaturdayEnd,
                SaturdayStart = doctor.SaturdayStart,
                ShortInfo = doctor.ShortInfo,
                SundayIsWorking = doctor.SundayIsWorking,
                Qualification = doctor.Qualification,
                Skills = doctor.Skills,
            };
            return model;
        }
        public async Task<bool> UpdateAsync(DoctorUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var doctor = await _doctorRepository.GetAsync(model.Id);

            doctor.Position = model.Position;
            doctor.Twitter = model.Twitter;
            doctor.ShowInHome = model.ShowInHome;
            doctor.Facebook = model.Facebook;
            doctor.LinkedIn = model.LinkedIn;
            doctor.ModifiedAt = DateTime.Now;
            doctor.FullName = model.FullName;
            doctor.SundayIsWorking = model.SundayIsWorking;
            doctor.Skills = model.Skills;
            doctor.SaturdayStart = model.SaturdayStart;
            doctor.SaturdayEnd = model.SaturdayEnd;
            doctor.MondayToFridayStart = model.MondayToFridayStart;
            doctor.MondayToFridayEnd = model.MondayToFridayEnd;
            doctor.Email = model.Email;
            doctor.Phone = model.Phone;
            doctor.Qualification = model.Qualification;
            doctor.Introducing = model.Introducing;
            doctor.ShortInfo = model.ShortInfo;


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
                    _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize}");
                    return false;
                }
                _fileService.Delete(doctor.PhotoName);
                doctor.PhotoName = await _fileService.UploadAsync(model.Photo);
            }

            await _doctorRepository.UpdateAsync(doctor);
            return true;

        }
    }
}
