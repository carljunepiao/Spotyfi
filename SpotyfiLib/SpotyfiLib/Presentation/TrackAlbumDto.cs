using System;
using System.Collections.Generic;

namespace SpotyfiLib.Presentation
{
    public class TrackAlbumDto : BaseDto
    {
        public string TrackTitle {get;set;}
        public long Duration {get;set;}
        public long ReleaseDate {get;set;}

        public TrackAlbumDto(long id, string title, long duration, long year){
            Id = id;
            TrackTitle = title;
            Duration = duration;
            ReleaseDate = year;
        }
    }
}