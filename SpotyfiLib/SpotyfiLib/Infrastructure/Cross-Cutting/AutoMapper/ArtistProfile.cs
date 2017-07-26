using System;
using AutoMapper;
using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Domain.AlbumAgg;
using SpotyfiLib.Presentation;
using System.Collections.Generic;
using System.Text;

namespace SpotyfiLib.Infrastructure.Cross_Cutting.AutoMapper
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, ArtistDto>();
            CreateMap<ArtistDetail, ArtistDetailDto>();
            CreateMap<Track, TrackDto>();
            CreateMap<Album, AlbumDto>();
            CreateMap<Genre, GenreDto>();
        }
    }
}