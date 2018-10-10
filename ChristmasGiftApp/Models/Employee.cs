using System.Collections.Generic;

namespace ChristmasGiftApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public ICollection<EmployeeGift> Gifts { get; set; }

        public Employee()
        {
            this.Gifts = new HashSet<EmployeeGift>();
        }
    }
}