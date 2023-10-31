using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Doctor
{
    public class DoctorCreateVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public Position Position { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string ShortInfo { get; set; }
        [Required]
        public string Introducing { get; set; }
        [Required]
        public string Skills { get; set; }

        [Required, MaxLength(40)]
        public string Phone { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime MondayToFridayStart { get; set; } 
        public DateTime MondayToFridayEnd { get; set; }  
        public DateTime SaturdayStart { get; set; }  
        public DateTime SaturdayEnd { get; set; }  
        public bool SundayIsWorking { get; set; }
        public bool ShowInHome { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? LinkedIn { get; set; }

    }
}
