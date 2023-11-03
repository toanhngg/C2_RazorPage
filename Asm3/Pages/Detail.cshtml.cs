using Asm3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Asm3.Pages
{
    public class DetailModel : PageModel
    {
        private readonly MVVMContext context;

        public DetailModel(MVVMContext _context)
        {
            this.context = _context;
        }
        public Customer customer { get; set; }
        public IActionResult OnGet(string? idDetail)
        {
            try
            {
                customer = context.Customers.SingleOrDefault(x => x.CustomerId.Equals(idDetail));
                if (customer == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

            }
            return Page();
        }
    }
}
