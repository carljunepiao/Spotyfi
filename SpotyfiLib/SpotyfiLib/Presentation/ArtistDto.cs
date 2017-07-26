using System;
using System.Collections.Generic;

namespace SpotyfiLib.Presentation
{
    public class ArtistDto : RecordDto
    {
        public string Firstname {get;set;}
        public string Lastname {get;set;}
        public long YearDebut {get;set;}

        public virtual ArtistDetailDto ArtistDetails {get;set;}
        public ICollection<ArtistTrackDto> Tracks {get;set;}
        public ICollection<ArtistAlbumDto> Albums {get;set;}

        public ArtistDto(){ 
            Tracks = new HashSet<ArtistTrackDto>();
            Albums = new HashSet<ArtistAlbumDto>();
        }

        public ArtistDto(long id, string firstname, string lastname, long yearDebut, ArtistDetailDto artistDetails, uint rowVersion):this(){
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            YearDebut = yearDebut;
            ArtistDetails = artistDetails;
            ConcurrencyStamp = rowVersion; //what is row version here?
        }
    }
}