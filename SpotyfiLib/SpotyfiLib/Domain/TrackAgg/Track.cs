using System;
using System.Collections.Generic;
using SpotyfiLib.Domain.RecordAgg;

//This shouldn't be done
using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Domain.AlbumAgg;

namespace SpotyfiLib.Domain.TrackAgg
{
    public class Track : Record
    {
        public string TrackTitle {get;set;}
        public long Duration {get;set;}
        public long ReleaseDate {get;set;}
        // public uint TrackNum {get;set;}
        // private static uint TrackCount;

        public virtual ICollection<ArtistTrack> Artists {get;set;}
        public virtual ICollection<TrackAlbum> Albums {get;set;}
        public virtual ICollection<TrackGenre> Genres {get;set;}
        // public virtual Album Album {get;set;}

        public Track(){
            Artists = new LinkedList<ArtistTrack>();
            Albums = new LinkedList<TrackAlbum>(); //I think there's something wrong with this.
            Genres = new LinkedList<TrackGenre>();
            // TrackCount++;
        }

        public Track(string title, long hour, long releasedate):this()
        {
            TrackTitle = title;
            Duration = hour;
            ReleaseDate = releasedate;
            // TrackNum++;
            // TrackCount++;
        }

        //Add Genres to Track
        public virtual void addGenreType(Genre genre){
            Genres.Add(new TrackGenre { Track = this, Genre = genre});
        }

        //Testing Purposes
        public void displayTrackDetail(){
            Console.WriteLine("---------------------------------------");
            // Console.WriteLine("Track No.: {0}",TrackNum);
            Console.WriteLine("Track Title: {0}",TrackTitle);
            Console.WriteLine("Track Duration (hrs): {0}",Duration);
            Console.WriteLine("Track Release Date: {0}",ReleaseDate);

            Console.WriteLine("Track Genre: ");
            foreach (var item in Genres)
            {
                item.Genre.displayGenre();
            }
        }
    }
}