using System;
using SpotyfiLib.Domain.AlbumAgg; // X

namespace SpotyfiLib.Domain.TrackAgg
{
    public class TrackAlbum
    {
        public long TrackId {get;set;}
        public long AlbumId {get;set;}
        public virtual Track Track {get;set;}
        public virtual Album Album {get;set;}
    }
}