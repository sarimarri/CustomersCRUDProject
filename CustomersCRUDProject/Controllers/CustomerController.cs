using CustomerWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static List<Customer> customers = new List<Customer>() { 
            new Customer { Id=1,Title="Mr",Name="CustomerA" ,EmailAddress="Customer_1@test.com",Phone="01213654789",Mobile="07777777777"},
             new Customer { Id=2,Title="Mrs",Name="CustomerB" ,EmailAddress="Customer_2@test.com",Phone="01213654780",Mobile="07111111111"},
              new Customer { Id=3,Title="Mr",Name="CustomerC"  ,EmailAddress="Customer_3@test.com",Phone="01213654089",Mobile="07222222222"},
               new Customer { Id=4,Title="Mrs",Name="CustomerD"  ,EmailAddress="Customer_4@test.com",Phone="01213654709",Mobile="07345678921"}
        };

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return customers;
        }

        [HttpGet("{id:int}", Name = "GetCustomer")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var customer= customers.FirstOrDefault(x => x.Id == id);
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(customer);

        }

        [HttpPost(Name ="Create")]
        public ActionResult<Customer> AddCustomer(Customer customer)
        {
            if (customers.FirstOrDefault(u => u.Name.ToLower() == customer.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Customer already existis");
                return BadRequest(ModelState);
            }
            if (customer == null)
            {
                return BadRequest();
            }
          

            customer.Id = customers.Max(u => u.Id) + 1;
            customers.Add(customer);
            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);

        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCustomer(int id,Customer UpdateCustomer) {

          
            var updateCustomer= customers.FirstOrDefault(x=>x.Id == id);
            if (updateCustomer == null || id != updateCustomer.Id)
            {
                return BadRequest();
            }
            updateCustomer.Name= UpdateCustomer.Name;
            updateCustomer.Phone= UpdateCustomer.Phone;
            updateCustomer.Title    = UpdateCustomer.Title;
            updateCustomer.EmailAddress= UpdateCustomer.EmailAddress;
            return NoContent();

        }

        [HttpDelete("{id:int}", Name = "DeleteCustomer")]
        public IActionResult DeleteCustomer(int id) {
            if (id == 0)
            {
                return BadRequest();
            }
            var deleteCustomer= customers.FirstOrDefault(x=>x.Id==id);
            if(deleteCustomer == null)
            {
                return NotFound();
            }
            customers.Remove(deleteCustomer);
            return NoContent(); 


        }
    }
}
