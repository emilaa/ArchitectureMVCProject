using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.ViewModels.Question;
using Web.Areas.Admin.ViewModels.Statistic;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IStatisticService
    {
        Task<bool> CreateAsync(StatisticCreateVM model);
        Task<StatisticIndexVM> GetAllAsync();
        Task<bool> UpdateAsync(StatisticUpdateVM model);
        Task<StatisticUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);
    }
}
