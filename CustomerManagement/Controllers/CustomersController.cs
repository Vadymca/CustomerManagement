using CustomerManagement.Models;
using CustomerManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IServiceCustomers? _serviceCustomers;
        private readonly CustomerContext? _customerContext;
        public CustomersController(IServiceCustomers? serviceCustomers, CustomerContext? customerContext)
        {
            _serviceCustomers = serviceCustomers;
            _customerContext = customerContext;
            _serviceCustomers._customerContext = customerContext;
        }

        public ViewResult Index() => View(_serviceCustomers?.Read());
        public ViewResult Details(int id) => View(_serviceCustomers?.GetById(id));
        public ViewResult Create() => View();
        public ViewResult Edit(int? id) => View();
        public ViewResult Delete(int? id)
        {
            Customer? customer = _customerContext?.Customers
                .FirstOrDefault(x => x.Id == id);
            return View(customer); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email")] Customer customer)
        {
            if(ModelState.IsValid)
            {
                _= _serviceCustomers?.Create(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _serviceCustomers?.Update(id, customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
           _= _serviceCustomers?.Delete(id);
            return RedirectToAction(nameof(Index));
        }

     }
}
