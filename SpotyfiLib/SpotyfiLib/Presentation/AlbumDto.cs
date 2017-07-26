using System;
using System.Collections.Generic;

namespace SpotyfiLib.Presentation
{
    public class AlbumDto : RecordDto
    {
        public string AlbumTitle {get;set;}
        public string RecordLabel {get;set;}
        public long Year {get;set;}

        public ICollection<ArtistDto> Artists {get;set;}
        public ICollection<TrackAlbumDto> Tracks {get;set;}
        
        public AlbumDto(long id, string title, string recordLabel, uint rowVersion, long year){
            Id = id;
            AlbumTitle = title;
            RecordLabel = recordLabel;
            Year = year;
            ConcurrencyStamp = rowVersion;

            Artists = new LinkedList<ArtistDto>();
            Tracks = new LinkedList<TrackAlbumDto>();
        }
    }
}