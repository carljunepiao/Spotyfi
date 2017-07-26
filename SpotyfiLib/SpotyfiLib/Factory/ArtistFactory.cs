using System;
using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Presentation;

namespace SpotyfiLib.Factory
{
    public static class ArtistFactory
    {
        public static Artist CreateArtist(string fname, string lname, long year, ArtistDetailDto artistDetailsDto){
            Artist artist = new Artist(fname,lname,year);
            artist.createArtistDetail(artistDetailsDto.StageName);
            return artist;
        }
    }
}