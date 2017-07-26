using System;
using System.Collections.Generic;

namespace SpotyfiLib.Presentation
{
    public class GenreDto : RecordDto
    {
        public string Type {get;set;}

        public ICollection<TrackDto> Tracks {get;set;}
        
        public GenreDto(long id, string type, uint rowVersion){
            Id = id;
            Type = type;
            Tracks = new LinkedList<TrackDto>();
            ConcurrencyStamp = rowVersion;
        }
    }
}