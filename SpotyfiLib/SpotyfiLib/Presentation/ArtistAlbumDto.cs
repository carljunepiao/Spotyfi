using System;

namespace SpotyfiLib.Presentation
{
    public class ArtistAlbumDto : BaseDto
    {
        public string AlbumTitle {get;set;}
        public string RecordLabel {get;set;}
        public long Year {get;set;}
        
        public ArtistAlbumDto(long id, string title, string recordLabel, long year){
            Id = id;
            AlbumTitle = title;
            RecordLabel = recordLabel;
            Year = year;
        }
    }
}