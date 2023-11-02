using C2_RazorPage.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace C2_RazorPage.Hubs
{
    public class MovieHub : Hub
    {
        private readonly PE_PRN_Fall22B1Context _context;
        public MovieHub(PE_PRN_Fall22B1Context context)
        {
            _context = context;
        }
        public async Task DeleteMovie(int? deleteId)
        {
            if (deleteId != null)
            {
                var movie = _context.Movies.Include(x => x.Stars).Include(x => x.Genres).SingleOrDefault(x => x.Id == deleteId);
                foreach (var star in _context.Stars.ToList())
                {
                    movie.Stars.Remove(star);
                }
                foreach (var star in _context.Stars)
                {
                    foreach (var moviedelete in star.Movies.ToList())
                    {
                        star.Movies.Remove(moviedelete);
                    }
                }

                foreach (var genre  in _context.Genres.ToList())
                {
                    movie.Genres.Remove(genre);
                }
                foreach (var genre in _context.Genres)
                {
                    foreach (var moviedelete in genre.Movies.ToList())
                    {
                        genre.Movies.Remove(moviedelete);
                    }
                }
                _context.Movies.Remove(movie);
                _context.SaveChanges();

            }
            await Clients.All.SendAsync("LoadMovie", deleteId);
            // sẽ gửi về method đó là method LoadMovie
            //  await Clients.All.SendAsync("LoadMovie", có thể truyền param trả về như  deleteId);
            

        }
    }
}
