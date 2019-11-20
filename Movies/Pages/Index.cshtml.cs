using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public List<Movie> Movies;
        [BindProperty]
        public string search { get; set; }
        [BindProperty]
        public List<string> rating { get; set; } = new List<string>();
        [BindProperty]
        public float? minIMDB { get; set; }
        [BindProperty]
        public float? maxIMDB { get; set; }

        public void OnGet()
        {
            Movies = MovieDatabase.All;
        }

        public void OnPost(string search, List<string> rating, float? minIMDB, float? maxIMDB)
        {
            Movies = MovieDatabase.All;
            Movies = MovieDatabase.SearchAndFilter(Movies, search, rating);

            if (minIMDB != null)
            {
                Movies = MovieDatabase.FilterMinRating(Movies, (float)minIMDB);
            }

            if (maxIMDB != null)
            {
                Movies = MovieDatabase.FilterMaxRating(Movies, (float)maxIMDB);
            }
        }
    }
}
