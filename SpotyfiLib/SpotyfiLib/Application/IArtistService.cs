using System;
using SpotyfiLib.Presentation;
using System.Collections.Generic;

namespace SpotyfiLib.Application
{
    public interface IArtistService
    {
        void CreateArtist(ArtistDto artistDto);
        void DeleteArtist(long id);
        ArtistDto getArtistById(long id);
        ICollection<ArtistDto> GetAllArtist();
        void ModifyArtist(long id, ArtistDto artistDto);
        
        void AddTrackArtist(long id, TrackDto trackDto);
        void AddAlbumArtist(long id, AlbumDto albumDto);
    }
}