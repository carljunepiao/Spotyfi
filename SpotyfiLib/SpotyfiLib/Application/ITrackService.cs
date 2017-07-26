using System;
using System.Collections.Generic;
using SpotyfiLib.Presentation;

namespace SpotyfiLib.Application
{
    public interface ITrackService
    {
        void modifyTrack(long id,TrackDto trackDto);
        ICollection<TrackDto> getAllTracks();
        void addGenreTrack(long id, GenreDto genreDto);
    }
}