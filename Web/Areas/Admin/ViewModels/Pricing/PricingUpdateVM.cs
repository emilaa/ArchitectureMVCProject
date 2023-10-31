﻿using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Pricing
{
    public class PricingUpdateVM
    {
        public int Id { get; set; }
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
