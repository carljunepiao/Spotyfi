using System;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Presentation;

namespace SpotyfiLib.Factory
{
    public static class GenreFactory
    {
        public static Genre CreateGenre(string type){
            Genre genre = new Genre(type);
            return genre;
        }
    }
}