using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.Statistic
{
    public class StatisticCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Quantity { get; set; }
    }
}
