using Web.Areas.Admin.ViewModels.About;
using Web.Areas.Admin.ViewModels.AboutUs;
using Web.Areas.Admin.ViewModels.OurVision;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IAboutService
    {
        Task<AboutIndexVM> GetAsync();
        Task<AboutDetailsVM> GetDetailsAsync(int id);
        Task<bool> CreateAsync(AboutCreateVM model);
        Task<bool> IsExistAsync();
        Task<AboutUpdateVM> GetUpdateModelAsync(int id);
        Task<AboutPhotoUpdateVM> GetUpdatePhotoModelAsync(int id);
        Task<bool> UpdateAsync(AboutUpdateVM model);
        Task<bool> UpdatePhotoAsync(AboutPhotoUpdateVM model);
        Task DeleteAsync(int id);
        Task DeletePhotoAsync(int id);
    }
}
