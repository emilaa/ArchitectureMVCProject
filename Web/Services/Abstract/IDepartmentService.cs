using Web.ViewModels.Department;
using Web.ViewModels.Home;

namespace Web.Services.Abstract
{
    public interface IDepartmentService
    {
        Task<DepartmentIndexVM> GetAllAsync();
    }
}
