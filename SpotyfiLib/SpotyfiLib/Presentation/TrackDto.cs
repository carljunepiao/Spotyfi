using System;
using System.Collections.Generic;

namespace SpotyfiLib.Presentation
{
    public class TrackDto : RecordDto
    {
        public string TrackTitle {get;set;}
        public long Duration {get;set;}
        public long ReleaseDate {get;set;}

        public virtual ICollection<ArtistDto> Artists {get;set;}
        public virtual ICollection<AlbumDto> Albums {get;set;} //? Ang track naay albums ?
        public virtual ICollection<TrackGenreDto> Genres {get;set;}

        public TrackDto(long id, string title, long duration, long release, uint rowVersion){
            Id = id;
            TrackTitle = title;
            Duration = duration;
            ReleaseDate = release;
            ConcurrencyStamp = rowVersion;

            Artists = new LinkedList<ArtistDto>();
            Albums = new LinkedList<AlbumDto>();
            Genres = new LinkedList<TrackGenreDto>();
        }
    }
}