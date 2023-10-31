using Web.Areas.Admin.ViewModels.AboutUs;
using Web.Areas.Admin.ViewModels.Doctor;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IDoctorService
    {
        Task<bool> CreateAsync(DoctorCreateVM model, List<string> SkillsList);
        Task<DoctorUpdateVM> GetUpdateModelAsync(int id);
        Task<DoctorDetailsVM> GetDetailsModelAsync(int id);
        Task<bool> UpdateAsync(DoctorUpdateVM model);
        Task DeleteAsync(int id);
        Task<DoctorIndexVM> GetAllAsync();
    }
}
