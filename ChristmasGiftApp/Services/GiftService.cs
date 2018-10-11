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
                return new ResultDto
                {
                    OperationResult = enums.OperationResult.Fail,
                    Message = $"Email {emailAddress} not found"
                };
            var employeeGift = employee.Gifts.Where(g => g.Year == _currentYear).FirstOrDefault();
            if (employeeGift != null)
            {
                return new ResultDto
                {
                    OperationResult = enums.OperationResult.Fail,
                    Message = "You have been participated in the draw"
                };
            }
            var result = await GetRandomEmployee(emailAddress);
            _context.EmployeeGifts.Add(new EmployeeGift { Employee = employee, TargetEmployee = result, Year = _currentYear });
            _context.SaveChanges();

            return new ResultDto
                {
                    Message = $"You have drawn an employee {result.FirstName} {result.LastName}",
                    OperationResult = enums.OperationResult.Successfull
                };
        }

        private async Task<Employee> GetEmployeeByEmail(string emailAddress)
        {
            return await _context.Employees.Include(g => g.Gifts)
                .Where(e => String.Equals(e.EmailAddress, emailAddress, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();
        }

        private async Task<Employee> GetRandomEmployee(string emailAddress)
        {
            var GiftLists = await _context.EmployeeGifts.Where(eg => eg.Year == _currentYear).Select(s => s.TargetEmployeeId).ToListAsync();
            var users = await _context.Employees
                .Where(a => !GiftLists.Any(g => g == a.Id) && !string.Equals(a.EmailAddress, emailAddress, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
            Random rand = new Random();
            return users[rand.Next(users.Count)];
        }
        
    }
}