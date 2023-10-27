using C2_RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace C2_RazorPage.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly PE_PRN_Fall22B1Context context;

        public EditModel(PE_PRN_Fall22B1Context _context)
        {
            this.context = _context;
        }
        [BindProperty]
        public Movie movie { get; set; }
        public void OnGet(int? idEdit)
        {
            // binding data sang đây là sử dụng cách asp-for
            try
            {
                if(idEdit != null)
                {
                     movie = context.Movies.SingleOrDefault(x => x.Id == idEdit);
                }
                ViewData["Directors"] = new SelectList(context.Directors.ToList(),"Id","FullName");
                ViewData["Producers"] = new SelectList(context.Producers.ToList(), "Id", "Name");

            }
            catch (Exception ex)
            {

            }

        }
        public IActionResult OnPost()
        {
            try
            {
                context.Movies.Update(movie);
                context.SaveChanges();
                return RedirectToPage("./Director_Movie");

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
