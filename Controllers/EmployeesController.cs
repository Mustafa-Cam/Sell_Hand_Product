using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sellhandproduct.Data;
using sellhandproduct.Models;
using sellhandproduct.Models.Domain;
using System.Diagnostics;

namespace sellhandproduct.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mVCDemoDbContext;

        public EmployeesController(MVCDemoDbContext mVCDemoDbContext)
        {
            this.mVCDemoDbContext = mVCDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            var employees = await mVCDemoDbContext.Employees.ToListAsync(); 
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest) 
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name, 
                Email = addEmployeeRequest.Email,
                 Tel = addEmployeeRequest.Tel
            };
            await mVCDemoDbContext.Employees.AddAsync(employee);
            await mVCDemoDbContext.SaveChangesAsync();
            return RedirectToAction("EmployeeList");
        }

        [HttpGet]

        public async Task<IActionResult> ViewEmployees(Guid id)
        {
            var employees = await mVCDemoDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            
            if(employees != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
            {
                Id = employees.Id,
                Name = employees.Name,
                Email = employees.Email, 
               Tel =employees.Tel
            };
                return View(viewModel); 
            }
            return RedirectToAction("EmployeeList");
        }

        

        [HttpPost] 
        public async Task<IActionResult> ViewEmployees(UpdateEmployeeViewModel model) // post geliyor aa burda UpdateEmployeeViewModel varmış verileri buna atayım diyor kısaca
        {
            var employee = await mVCDemoDbContext.Employees.FindAsync(model.Id); 

            if (employee != null)

            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Tel = model.Tel;
                await mVCDemoDbContext.SaveChangesAsync();
                return Redirect("ViewEmployees");

            }
            return RedirectToAction("ViewEmployees");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var todoItem = mVCDemoDbContext.Employees.Find(id);
            if (todoItem != null)
            {
                mVCDemoDbContext.Employees.Remove(todoItem);
                mVCDemoDbContext.SaveChanges();
                return RedirectToAction("EmployeeList");
            }
            return RedirectToAction("EmployeeList"); 
        }

    }
}
