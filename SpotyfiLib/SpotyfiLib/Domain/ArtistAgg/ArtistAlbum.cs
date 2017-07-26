using System;
using SpotyfiLib.Domain.AlbumAgg; //X

namespace SpotyfiLib.Domain.ArtistAgg
{
    public class ArtistAlbum
    {
        public long ArtistId {get;set;}
        public long AlbumId {get;set;}
        public virtual Artist Artist {get;set;}
        public virtual Album Album {get;set;}
    }
}