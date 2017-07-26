using System;
using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Domain.TrackAgg;

namespace SpotyfiLib.Domain.ArtistAgg
{
    public class ArtistTrack
    {
        public long ArtistId {get;set;}
        public long TrackId {get;set;}
        public virtual Artist Artist {get;set;}
        public virtual Track Track {get;set;}
    }
}