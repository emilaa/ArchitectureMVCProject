using Web.Areas.Admin.ViewModels.HomeMainSlider;
using Web.Areas.Admin.ViewModels.LastestNews;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface INewsService
    {
        Task<bool> CreateAsync(NewsCreateVM model);
        Task<bool> UpdateAsync(NewsUpdateVM model);
        Task<NewsUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);
        Task<NewsIndexVM> GetAllAsync();
        //Task<HomeMainSliderDetailsVM> GetDetailsAsync(int id);
    }
}
