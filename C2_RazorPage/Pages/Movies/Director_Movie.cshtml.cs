using C2_RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace C2_RazorPage.Pages.Movies
{
    public class Director_MovieModel : PageModel
    {
        private readonly PE_PRN_Fall22B1Context context;

        public Director_MovieModel(PE_PRN_Fall22B1Context _context)
        {
            this.context = _context;
        }
        public List<Director> directors;
        public List<Movie> movies;
        public int? directorID;
        public void OnGet(int? id, int? deleteId)
        {
            try
            {
          
                if (deleteId != null)
                {
                    var movie = context.Movies.Include(x => x.Stars).Include(x => x.Genres).SingleOrDefault(x => x.Id == deleteId);
                    foreach (var star in context.Stars.ToList())
                    {
                        movie.Stars.Remove(star);
                    }
                    foreach (var star in context.Stars)
                    {
                        foreach (var moviedelete in star.Movies.ToList())
                        {
                            star.Movies.Remove(moviedelete);
                        }
                    }

                    foreach (var genre in context.Genres.ToList())
                    {
                        movie.Genres.Remove(genre);
                    }
                    foreach (var genre in context.Genres)
                    {
                        foreach (var moviedelete in genre.Movies.ToList())
                        {
                            genre.Movies.Remove(moviedelete);
                        }
                    }
                    context.Movies.Remove(movie);
                    context.SaveChanges();
                }
                if (id == null)
                {
                    movies = context.Movies.Include(x => x.Producer).Include(x => x.Stars).ToList();
                }
                else
                {
                    directorID = id;
                    movies = context.Movies.Include(x => x.Producer).Include(x => x.Stars).Where(x => x.Director.Id == id).ToList();
                }
                directors = context.Directors.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


    }
}
