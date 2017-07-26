using System;

namespace SpotyfiLib.Domain.ArtistAgg
{
    public class ArtistDetail : ValueObject
    {
        public long Id { get; set; }
        public string StageName { get; set; }
        public virtual Artist Artist { get; set; }

        public ArtistDetail(){}

        public ArtistDetail(Artist artist, string sname){
            Artist = artist;
            StageName = sname;
        }
    }
}