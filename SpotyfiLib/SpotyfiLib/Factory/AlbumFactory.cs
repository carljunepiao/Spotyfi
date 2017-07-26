using System;
using SpotyfiLib.Domain.AlbumAgg;
using SpotyfiLib.Presentation;

namespace SpotyfiLib.Factory
{
    public static class AlbumFactory
    {
        public static Album CreateAlbum(string title, string company, uint year){
            Album album = new Album(title,company,year);
            return album;
        }
    }
}