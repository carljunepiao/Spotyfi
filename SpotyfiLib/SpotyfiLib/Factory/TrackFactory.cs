using System;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Presentation;

namespace SpotyfiLib.Factory
{
    public static class TrackFactory
    {
        public static Track CreateTrack(string title, uint hour, uint releasedate){
            Track track = new Track(title,hour,releasedate);
            return track;
        }
    }
}