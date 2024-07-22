using CustomerMVC.Models;
using CustomerMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerServices _customerServices;
        public CustomerController(CustomerServices customerServices)
        {
                _customerServices = customerServices;
        }
        public async Task<IActionResult> Index()
        {
            var customers = await _customerServices.GetCustomersAsync();
            return View(customers);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerServices.AddCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerServices.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _customerServices.UpdateCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerServices.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerServices.DeleteCustomerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}