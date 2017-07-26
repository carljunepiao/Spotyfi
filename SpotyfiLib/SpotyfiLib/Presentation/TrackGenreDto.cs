using System;
using System.Collections.Generic;

namespace SpotyfiLib.Presentation
{
    public class TrackGenreDto : BaseDto
    {
        public string Type {get;set;}

        public TrackGenreDto(long id, string type){
            Id = id;
            Type = type;
        }
    }
}