using System;
using System.Collections.Generic;
using SpotyfiLib.Domain.RecordAgg;

//I think this souldn't be done
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Domain.ArtistAgg;

namespace SpotyfiLib.Domain.AlbumAgg
{
    public class Album : Record
    {
        public string AlbumTitle {get;set;}
        public string RecordLabel {get;set;}
        public long Year {get;set;}
        // private static uint AlbumCount;

        public virtual ICollection<ArtistAlbum> Artists {get;set;}
        public virtual ICollection<TrackAlbum> Tracks {get;set;}

        public Album(){
            Artists = new LinkedList<ArtistAlbum>();
            Tracks = new LinkedList<TrackAlbum>();
            // AlbumCount++;            
        }

        public Album(/*string displayName,*/ string title, string company, long year):this()
        {
            // DisplayName = displayName;
            AlbumTitle = title;
            RecordLabel = company;
            Year = year;
            // AlbumCount++;
        }

        //Add Track in Album
        public virtual Album addTrackinAlbum(Track track){
            Tracks.Add(new TrackAlbum { Album = this, Track = track});
            return this;
        }

        //For Testing Purposes
        public void displayAlbumDetail(){
            Console.WriteLine("----------------ALBUM-------------------");
            // Console.WriteLine("Album No.: {0}",AlbumCount);
            Console.WriteLine("Album Title: {0}",AlbumTitle);
            Console.WriteLine("Album Record Label: {0}",RecordLabel);
            Console.WriteLine("Album Release Date: {0}",Year);

            Console.WriteLine("Album Tracks: ");
            foreach (var item in Tracks)
            {
                item.Track.displayTrackDetail();
            }
        }
    }
}