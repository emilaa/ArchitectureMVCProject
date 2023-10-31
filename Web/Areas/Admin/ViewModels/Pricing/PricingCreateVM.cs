using Core.Constants;
using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.Pricing
{
    public class PricingCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public string Features { get; set; }
        [Required]
        public double Price { get; set; }
        public PricePer PricePer { get; set; }
        public ActiveStatus ActiveStatus { get; set; }
    }
}
