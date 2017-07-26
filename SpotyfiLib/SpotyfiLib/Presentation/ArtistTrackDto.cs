using System;

namespace SpotyfiLib.Presentation
{
    public class ArtistTrackDto : BaseDto
    {
        public string TrackTitle {get;set;}
        public long Duration {get;set;}
        public long ReleaseDate {get;set;}

        public ArtistTrackDto(long id, string title, long duration, long release){
            Id = id;
            TrackTitle = title;
            Duration = duration;
            ReleaseDate = release;
        }
    }
}