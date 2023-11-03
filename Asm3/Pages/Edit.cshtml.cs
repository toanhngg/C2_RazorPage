using Asm3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asm3.Pages
{
    public class EditModel : PageModel
    {
        private readonly MVVMContext context;

        public EditModel(MVVMContext _context)
        {
            this.context = _context;
        }
        [BindProperty]
        public Customer customer { get; set; }
        public void OnGet(String? idEdit)
        {
            try
            {
                if (idEdit != null)
                {
                     customer = context.Customers.SingleOrDefault(x => x.CustomerId.Equals(idEdit));
                }

            }
            catch (Exception ex)
            {

            }
        }
        public IActionResult OnPost()
        {
            try
            {
                context.Customers.Update(customer);
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
