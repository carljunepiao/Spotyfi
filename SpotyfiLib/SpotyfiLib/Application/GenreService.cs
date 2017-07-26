using System;
using System.Collections.Generic;
using SpotyfiLib.Domain.TrackAgg;
using AutoMapper;
using SpotyfiLib.Presentation;
using SpotyfiLib.Helper;

namespace SpotyfiLib.Application
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepo, IMapper mapper){
            _genreRepository = genreRepo;
            _mapper = mapper;
        }

        public ICollection<GenreDto> getAllGenres()
        {
            var genres = _genreRepository.GetAll();

            if(!genres.isNullOrEmpty()){
                ICollection<GenreDto> genresDto = new LinkedList<GenreDto>();
                GenreDto genreDto = null;

                foreach (Genre genre in genres)
                {
                    genreDto = new GenreDto(genre.Id,genre.Type, genre.ConcurrencyStamp);

                    if(genre.Tracks.Count > 0){
                        foreach (TrackGenre trackGenre in genre.Tracks)
                        {
                            genreDto.Tracks.Add(new TrackDto(
                                trackGenre.Track.Id,
                                trackGenre.Track.TrackTitle,
                                trackGenre.Track.Duration,
                                trackGenre.Track.ReleaseDate,
                                trackGenre.Track.ConcurrencyStamp
                            ));
                        }
                    }
                    genresDto.Add(genreDto);
                }

                return genresDto;
            }

            return null;
        }

        // public ICollection<GenreDto> MapGenre(IEnumerable<GenreDto> genres){
        //     ICollection<GenreDto> genresDto = new LinkedList<GenreDto>();


        // }
    }
}