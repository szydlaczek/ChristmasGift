using System.ComponentModel.DataAnnotations;

namespace ChristmasGiftApp.ViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}