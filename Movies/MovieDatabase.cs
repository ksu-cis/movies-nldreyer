using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies;

        public static List<Movie> All
        {
            get
            {
                if (movies == null)
                {
                    using (StreamReader file = System.IO.File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
                return movies;
            }
        }

        public static List<Movie> SearchAndFilter(List<Movie> movies, string searchString, List<string> rating)
        {
            // Case 0: No search string, no ratings
            if (searchString == null && rating.Count == 0)
            {
                return All;
            }
            List<Movie> filtered = new List<Movie>();
            foreach (Movie movie in movies)
            {
                // Case 1: Search string AND ratings
                if (searchString != null && rating.Count > 0)
                {
                    if (movie.Title != null && movie.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) && rating.Contains(movie.MPAA_Rating))
                    {
                        filtered.Add(movie);
                    }
                }
                // Case 2: Search string only
                else if (searchString != null)
                {
                    if (movie.Title != null && movie.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
                    {
                        filtered.Add(movie);
                    }
                }
                // Case 3: Ratings only
                else if (rating.Count > 0)
                {
                    if (rating.Contains(movie.MPAA_Rating))
                    {
                        filtered.Add(movie);
                    }
                }
            }
            return filtered;
        }

        public static List<Movie> FilterMinRating(List<Movie> movies, float minIMDB)
        {
            List<Movie> filtered = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.IMDB_Rating != null && minIMDB <= movie.IMDB_Rating)
                {
                    filtered.Add(movie);
                }
            }
            return filtered;
        }

        public static List<Movie> FilterMaxRating(List<Movie> movies, float maxIMDB)
        {
            List<Movie> filtered = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.IMDB_Rating != null && maxIMDB >= movie.IMDB_Rating)
                {
                    filtered.Add(movie);
                }
            }
            return filtered;
        }

        /*
        public List<Movie> FilterByMPAA(List<Movie> movies, List<string> mpaa)
        {
            List<Movie> filtered = new List<Movie>();

            foreach (Movie movie in movies)
            {
                if (mpaa.Contains)
                {
                    filtered.Add(movie);
                }
            }

            return filtered;
        }
        */
    }
}
