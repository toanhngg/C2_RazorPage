using Asm3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Xml.Linq;

namespace Asm3.Pages
{
    public class AddModel : PageModel
    {
        private readonly MVVMContext context;

        public AddModel(MVVMContext _context)
        {
            this.context = _context;
        }
        public Customer customer { get; set; }
       // public List<Customer> customers { get; set; }

        public void OnGet()
        {
     
        }
        public IActionResult OnPost()
        {
            try
            {
                var cusId = Request.Form["cusId"];
                var cusname = Request.Form["cusname"];
                var address = Request.Form["address"];
                var phone = Request.Form["phone"];
                var discount = Request.Form["discount"];
                customer = new Customer();
                customer.CustomerId = cusId;
                customer.CustomerName = cusname;
                customer.Address = address;
                customer.Phone = phone;
                customer.DiscountRate = int.Parse(discount);
                context.Customers.Add(customer);
                context.SaveChanges();

                return RedirectToPage("./List");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

