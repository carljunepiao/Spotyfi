using System;
using System.Collections.Generic;
using SpotyfiLib.Presentation;

namespace SpotyfiLib.Application
{
    public interface IGenreService
    {
        ICollection<GenreDto> getAllGenres();
    }
}