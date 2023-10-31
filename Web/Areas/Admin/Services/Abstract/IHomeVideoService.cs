using Web.Areas.Admin.ViewModels.HomeMainSlider;
using Web.Areas.Admin.ViewModels.HomeVideo;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeVideoService
    {
        Task<HomeVideoIndexVM> GetAsync();
        Task<bool> CreateAsync(HomeVideoCreateVM model);
        public Task<bool> IsExistAsync();
        Task<bool> UpdateAsync(HomeVideoUpdateVM model);
        Task<HomeVideoUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);

    }
}
