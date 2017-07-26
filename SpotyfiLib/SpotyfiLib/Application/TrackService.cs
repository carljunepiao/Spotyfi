using System;
using System.Collections.Generic;
using SpotyfiLib.Presentation;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Domain.ArtistAgg;
using AutoMapper;
using SpotyfiLib.Helper;

namespace SpotyfiLib.Application
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IMapper _mapper;

        public TrackService(ITrackRepository trackRepo, IMapper mapper){
            _trackRepository = trackRepo;
            _mapper = mapper;
        }

        public void addGenreTrack(long id, GenreDto genreDto) //Not sure about this.
        {
            var track = _trackRepository.Get(id);

            track.displayTrackDetail();

            if(track == null){
                throw new ArgumentException($"Track with id:{id} doesn't exist.");
            }

            Genre genre = new Genre(genreDto.Type);
            track.addGenreType(genre);

            //_trackRepository.unitOfWork.Commit();
            _trackRepository.unitOfWork._secondGenUoW.Commit();
        }

        public void modifyTrack(long id, TrackDto trackDto)
        {
            //Not yet implemented.
        }

        public ICollection<TrackDto> getAllTracks()
        {
            var tracks = _trackRepository.GetAll();

            // if(!tracks.isNullOrEmpty()){
                // ICollection<TrackDto> tracksDto = new LinkedList<TrackDto>();
                // TrackDto trackDto = null;

                // foreach (Track track in tracks)
                // {
                //     trackDto = new TrackDto(track.Id,track.TrackTitle,track.Duration,track.ReleaseDate, track.ConcurrencyStamp);

                //     if(track.Artists.Count > 0){
                //         foreach (ArtistTrack artistTrack in track.Artists)
                //         {
                //             trackDto.Artists.Add(new ArtistDto(
                //                 artistTrack.Artist.Id,
                //                 artistTrack.Artist.FirstName,
                //                 artistTrack.Artist.LastName,
                //                 artistTrack.Artist.YearDebut,
                //                 new ArtistDetailDto {StageName = artistTrack.Artist.ArtistDetails.StageName},
                //                 artistTrack.Artist.ConcurrencyStamp
                //             ));
                //         }
                //     }
                //     tracksDto.Add(trackDto);
                // }

                // // return MapTracks(tracksDto);

                // return tracksDto;
            // }

            return !tracks.isNullOrEmpty() ? _mapper.Map<ICollection<TrackDto>>(MapTracks(tracks)) : null;
        }

        public ICollection<TrackDto> MapTracks (IEnumerable<Track> tracks)
        {
            ICollection<TrackDto> tracksDto = new LinkedList<TrackDto>();

            foreach (var track in tracks)
            {
                TrackDto trackDto = new TrackDto(
                    track.Id,
                    track.TrackTitle,
                    track.Duration,
                    track.ReleaseDate,
                    track.ConcurrencyStamp
                );

                if(track.Artists.Count > 0)
                {
                    foreach (var artist in track.Artists)
                    {
                        trackDto.Artists.Add(
                            new ArtistDto(
                                artist.Artist.Id,
                                artist.Artist.FirstName, 
                                artist.Artist.LastName,
                                artist.Artist.YearDebut,
                                new ArtistDetailDto(
                                    artist.Artist.Id,
                                    artist.Artist.ArtistDetails.StageName
                                ), 
                                artist.Artist.ConcurrencyStamp
                            )
                        );
                    }
                }
                
                if(track.Albums.Count > 0)
                {        
                    foreach (var album in track.Albums)
                    {
                        trackDto.Albums.Add(
                            new AlbumDto(
                                album.Album.Id, 
                                album.Album.AlbumTitle, 
                                album.Album.RecordLabel, 
                                album.Album.ConcurrencyStamp, 
                                album.Album.Year
                            )
                        );
                    }
                }

                if(track.Genres.Count > 0)
                {
                    foreach (var genre in track.Genres)
                    {
                        trackDto.Genres.Add(
                            new TrackGenreDto(
                                genre.GenreId,
                                genre.Genre.Type
                            )
                        );
                    }
                }

                tracksDto.Add(trackDto);
            }

            return tracksDto;
        }
    }
}