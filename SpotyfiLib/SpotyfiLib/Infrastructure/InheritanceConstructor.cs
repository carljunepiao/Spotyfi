using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Domain.RecordAgg;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Domain.AlbumAgg;

// using SpotyfiLib.Domain.PersonAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotyfiLib.Infrastructure
{
    public static class InheritanceConstructor
    {
        // public static Record ReConstructPerson(Record person)
        // {
        //     person.DisplayName = party.DisplayName;
        //     return person;
        // }

        public static Artist ReConstructArtist(Record record, Artist artist)
        {
            return artist;
        }

        public static Track ReConstructTrack(Record record, Track track)
        {
            return track;
        }

        public static Album ReConstructAlbum(Record record, Album album)
        {
            return album;
        }

        public static Genre ReConstructGenre(Record record, Genre genre)
        {
            return genre;
        }
    }
}
