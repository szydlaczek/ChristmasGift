using ChristmasGiftApp.Context;
using ChristmasGiftApp.DTO;
using ChristmasGiftApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChristmasGiftApp
{
    public class GiftService
    {
        private readonly ApplicationDbContext _context;
        private readonly int _currentYear;
        public GiftService(ApplicationDbContext context)
        {
            _context = context;
            _currentYear = DateTime.Now.Year;
        }

        public async Task<ResultDto> AssignRandomEmployee(string emailAddress)
        {
            var employee = await GetEmployeeByEmail(emailAddress);
            if (employee == null)
                return new ResultDto {
                    OperationResult = enums.OperationResult.Fail,
                    Message = $"Email {emailAddress} not found"
                };
            var employeeGift = employee.Gifts.Where(g => g.Year == _currentYear).FirstOrDefault();
            if (employeeGift!=null)
            {
                return new ResultDto
                {
                    OperationResult = enums.OperationResult.Fail,
                    Message = "You participated in the draw"
                };
            }
            var result = await GetRandomEmployee(emailAddress);
            return null;

        }
        private async Task<Employee> GetEmployeeByEmail(string emailAddress)
        {
            return await _context.Employees
                .Where(e => String.Equals(e.EmailAddress, emailAddress, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();
        }
        private async Task<Employee> GetRandomEmployee(string emailAddress)
        {
            var employeesWithoutGift = await _context.Employees.Where(e => !string.Equals(e.EmailAddress, emailAddress) && !e.Gifts.Any(g => g.Year == _currentYear)).ToListAsync();
            Random rand = new Random();
            return employeesWithoutGift[rand.Next(employeesWithoutGift.Count)];
        }
    }
}