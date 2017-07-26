using System;
using System.Collections.Generic;
using System.Text;
using SpotyfiLib.Domain.RecordAgg;
//This shouldn't be done because it causes high level of coupling & dependencies
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Domain.AlbumAgg;

/*
 *  Created by: Charlito Piao
 *
 *  Note: Must be modified. (Line 35,54)
 *  1. Such that you can't add Genre without Track.
 *  2. You can't add Album without Track.
 *  3. You can't add Track or Album without Artist.
 *  4. namespaces shouldn't be use. (line 6)
 *
 *  One possible solution: Check an instance of Artist, Track, Album.
 */

namespace SpotyfiLib.Domain.ArtistAgg
{
    public class Artist : Record
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RealName { get { return $"{FirstName} {LastName}";} }

        public long YearDebut { get; set; }

        //Relation|Association with Track Many-Many
        public virtual ICollection<ArtistTrack> Tracks { get; set; }
        //Relation|Association with Album Many-Many
        public virtual ICollection<ArtistAlbum> Albums { get; set; }

        public virtual ArtistDetail ArtistDetails { get; set; }

        //This should be modified, Artist can have a Track but no Album.
        public Artist(){
            Tracks = new LinkedList<ArtistTrack>();
            
            //if track ra, album shouldn't be instantiated at the first place
            Albums = new LinkedList<ArtistAlbum>();
        }

        public Artist(string fname,string lname, long year) /*,string displayName*/:this(){
            // DisplayName = displayName;
            FirstName = fname;
            LastName = lname;
            YearDebut = year;
        }

        public virtual void addTrack(Track track){
            Tracks.Add(new ArtistTrack { Artist = this, Track = track});
        }

        public virtual Track createTrack(string title, long duration, long year){
            return new Track(title,duration,year);
        }

        // *Note: At the same time should create a track
        public virtual void addAlbum(Album album){
            Albums.Add(new ArtistAlbum { Artist = this, Album = album});
        }

        public virtual Album createAlbum(string title, string company, long year){
            return new Album(title,company, year);
        }
        
        //Artist Details
        public virtual void createArtistDetail(string sname){
            ArtistDetails = new ArtistDetail(this,sname);
        }

        public virtual void changeFirstName(string fname){
            FirstName = fname;
        }

        public virtual void changeLastName(string lname){
            LastName = lname;
        }

        public virtual void changeYearDebut(long year){
            YearDebut = year;
        }

        public virtual void changeStageName(string name){
            ArtistDetails.StageName = name;
        }

        //Testing Purposes
        public void display(){
            Console.WriteLine("-------------------------------------------------");            
            Console.WriteLine("----------------ARTIST-----------------");
            Console.WriteLine("Artist Real Name: {0}",RealName);
            //Console.WriteLine("Artist Stage Name: {0}",ArtistDetails.StageName);
            Console.WriteLine("Artist YearDebut: {0}",YearDebut);
        }
    }
}