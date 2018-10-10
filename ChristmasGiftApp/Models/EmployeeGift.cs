namespace ChristmasGiftApp.Models
{
    public class EmployeeGift
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int Year { get; set; }
        public int TargetEmployeeId { get; set; }
        public Employee TargetEmployee { get; set; }
    }
}