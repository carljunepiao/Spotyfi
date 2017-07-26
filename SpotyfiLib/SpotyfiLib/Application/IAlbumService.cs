using System;
using System.Collections.Generic;
using SpotyfiLib.Presentation;

namespace SpotyfiLib.Application
{
    public interface IAlbumService
    {
        ICollection <AlbumDto> getAllAlbums();
        void addTrackAlbum(long id, TrackDto trackDto);
    }
}