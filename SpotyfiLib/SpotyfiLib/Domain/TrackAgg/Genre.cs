using System;
using System.Collections.Generic;
using SpotyfiLib.Domain.RecordAgg;

namespace SpotyfiLib.Domain.TrackAgg
{
    public class Genre : Record
    {
        public string Type {get;set;}
        public virtual ICollection<TrackGenre> Tracks {get;set;}

        public Genre(){
            Tracks = new LinkedList<TrackGenre>();
        }

        public Genre(string type):this(){
            Type = type;
        }

        //Add Tracks to a Genre, acts as a playlist.
        public void addGenreTrack(Track track){
            Tracks.Add(new TrackGenre {Genre = this, Track = track});
        }

        public void displayGenre(){
            Console.WriteLine("Genre: {0}",Type);
        }
    }
}