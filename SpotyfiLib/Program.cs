using System;
using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Domain.AlbumAgg;

namespace Spotyfi
{
    class Program
    {
        static void Main(string[] args)
        {
            Artist a1;
            
            a1 = new Artist("Rich","Chigga",2013);
            a1.createArtistDetail("Cool Chinese");

            Track track1 = a1.createTrack("SeeShark",1,2017);
            Album album1 = a1.createAlbum("Ham Chigga","Lion Inc.",2017);

            track1.addGenreType(new Genre("OPM"));

            //Add Albums
            a1.addAlbum(a1.createAlbum("White Beast","Monkey Inc.",2017).addTrackinAlbum(track1));
            
            //  Adds existing tracks to album
            a1.addAlbum(album1.addTrackinAlbum(a1.createTrack("Lindo",1,2021)));
            album1.addTrackinAlbum(a1.createTrack("Jaba",2,2011));
            
            //Display artist info
            a1.display();

            foreach (var item in a1.Tracks)
            {
                item.Track.displayTrackDetail();
            }

            foreach (var item in a1.Albums)
            {
                item.Album.displayAlbumDetail();
            }

            Console.ReadLine();
        }
    }
}
