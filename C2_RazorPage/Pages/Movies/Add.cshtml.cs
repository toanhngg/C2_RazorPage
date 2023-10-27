using C2_RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace C2_RazorPage.Pages.Movies
{
  

    public class AddModel : PageModel
    {
        private readonly PE_PRN_Fall22B1Context context;

        public AddModel(PE_PRN_Fall22B1Context _context)
        {
            this.context = _context;
        }
        [BindProperty]
        public Movie movie { get; set; }
        public List<Director> directors { get; set; }
        public List<Producer> producers { get; set; }


        public void OnGet()
        {
            try
            {
                directors = context.Directors.ToList();
                producers = context.Producers.ToList();

            }catch (Exception ex)
            {

            }
        }
        public IActionResult OnPost()
        {
            try
            {
                var title = Request.Form["title"];
                var date = Request.Form["date"];
                var description = Request.Form["description"];
                var language = Request.Form["language"];
                var idDirector = Request.Form["idDirector"];
                var idProducer = Request.Form["idProducer"];
                Movie movie = new Movie();
                movie.Title = title;
                movie.ReleaseDate = Convert.ToDateTime(date);
                movie.Description = description;
                movie.Language = language;
                movie.DirectorId = int.Parse(idDirector);

                movie.ProducerId = int.Parse(idProducer);
                context.Movies.Add(movie);
                context.SaveChanges();

                return RedirectToPage("./Director_Movie");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
