using CustomerManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Services
{
    public interface  IServiceCustomers
    {
        public CustomerContext? _customerContext { get; set; }
        public Customer? Create(Customer? customer);
        public IEnumerable<Customer>? Read();
        public Customer? GetById(int id);
        public Customer? Update(int id, Customer? customer);
        public bool Delete(int id);
    }
    public class ServiceCustomers : IServiceCustomers
    {
        public CustomerContext? _customerContext { get; set; }

        public Customer? Create(Customer? customer)
        {
            _customerContext?.Customers.Add(customer);
            _customerContext?.SaveChanges();
            return customer;
        }

        public bool Delete(int id)
        {
           Customer? customer = _customerContext?.Customers
                .FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return false;
            }
            else
            {
                _customerContext?.Customers .Remove(customer);
                _customerContext?.SaveChanges();
                return true;
            }
        }

        public Customer? GetById(int id)
        {
            return _customerContext?.Customers
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Customer>? Read()
        {
            return _customerContext?.Customers.ToList();
        }

        public Customer? Update(int id, Customer? customer)
        {
            if(id != customer?.Id)
            {
                return null;
            }
            else
            {
                try
                {
                    _customerContext?.Customers.Update(customer);
                    _customerContext?.SaveChanges();
                    return customer;
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }
            }
        }
    }
}
