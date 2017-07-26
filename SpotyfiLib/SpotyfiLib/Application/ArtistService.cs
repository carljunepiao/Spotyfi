using System;
using SpotyfiLib.Presentation;
using SpotyfiLib.Domain.ArtistAgg;
using AutoMapper;
using SpotyfiLib.Factory;
using System.Collections.Generic;
using SpotyfiLib.Helper;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Domain.AlbumAgg;

namespace SpotyfiLib.Application
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;
        
        public ArtistService(IArtistRepository artistRepo,IMapper mapper){
            _artistRepository = artistRepo;
            _mapper = mapper;
        }

        public void CreateArtist(ArtistDto artistDto)
        {
            Console.WriteLine("Create artist service.");
            using(var transaction = _artistRepository.unitOfWork.BeginTransaction(2))
            {
                _artistRepository.Add(ArtistFactory.CreateArtist(artistDto.Firstname,artistDto.Lastname,artistDto.YearDebut,artistDto.ArtistDetails));
                transaction.Commit();
            }
        }

        public void DeleteArtist(long id)
        {
            var artist = _artistRepository.Get(id);

            if(artist == null){
                throw new ArgumentException($"There is no artist with id:{id}");
            }

            using(var transaction = _artistRepository.unitOfWork.BeginTransaction(2))
            {
                _artistRepository.Remove(artist);
                transaction.Commit();
            }
        }

        public void AddTrackArtist(long id, TrackDto trackDto)
        {
            var artist = _artistRepository.Get(id);

            if(artist == null){
                throw new ArgumentException($"There is no artist with id:{id}");
            }

            Track track = artist.createTrack(trackDto.TrackTitle,trackDto.Duration,trackDto.ReleaseDate);
            artist.addTrack(track);

            _artistRepository.unitOfWork._secondGenUoW.Commit();
            Console.WriteLine("Successfully added track in artist (committed)");
        }
        
        public void AddAlbumArtist(long id, AlbumDto albumDto)
        {
            var artist = _artistRepository.Get(id);

            if(artist == null){
                throw new ArgumentException($"There is no artist with id:{id}");
            }

            Album album = artist.createAlbum(albumDto.AlbumTitle,albumDto.RecordLabel,albumDto.Year);
            artist.addAlbum(album);

            _artistRepository.unitOfWork._secondGenUoW.Commit();
        }

        public void ModifyArtist(long id, ArtistDto artistDto)
        {
            var artist = _artistRepository.Get(id);

            if(artist == null){
                throw new ArgumentException($"There is no artist with id:{id}");
            }

            artist.changeFirstName(artistDto.Firstname);
            artist.changeLastName(artistDto.Lastname);
            artist.changeYearDebut(artistDto.YearDebut);
            artist.changeStageName(artistDto.ArtistDetails.StageName);

            using(var transaction = _artistRepository.unitOfWork.BeginTransaction(2))
            {
                _artistRepository.Modify(artist);
                transaction.Commit();
            }
        }

        public ArtistDto getArtistById(long id)
        {
            var artist = _artistRepository.Get(id);

            if (artist == null){
                throw new ArgumentException($"There is no artist wih id:{id}");
            }

            return _mapper.Map<ArtistDto>(MapArtist(artist));
        }

        public ICollection<ArtistDto> GetAllArtist()
        {
            var artists = _artistRepository.GetAll();
            return !artists.isNullOrEmpty() ? _mapper.Map<ICollection<ArtistDto>>(MapArtists(artists)) : null; //create customized mapper here
        }

        public ArtistDto MapArtist(Artist artist)
        {
            ArtistDto artistDto = new ArtistDto(
                artist.Id, 
                artist.FirstName, 
                artist.LastName, 
                artist.YearDebut,
                new ArtistDetailDto(artist.ArtistDetails.Id,artist.ArtistDetails.StageName),
                artist.ConcurrencyStamp
            );

            foreach(var track in artist.Tracks)
            {
                //TrackDto trackDto = new TrackDto(track.Track.Id, track.Track.TrackTitle, track.Track.Duration, track.Track.ReleaseDate, track.Track.ConcurrencyStamp);
                artistDto.Tracks.Add(new ArtistTrackDto(track.Track.Id,track.Track.TrackTitle, track.Track.Duration, track.Track.ReleaseDate));
            }

            foreach(var album in artist.Albums)
            {
                artistDto.Albums.Add(new ArtistAlbumDto(album.Album.Id, album.Album.AlbumTitle, album.Album.RecordLabel, album.Album.Year));
            }

            return artistDto;
        }

        public ICollection<ArtistDto> MapArtists(IEnumerable<Artist> artists)
        {
            ICollection<ArtistDto> artistsDto = new LinkedList<ArtistDto>();

            foreach(var artist in artists)
            {
                ArtistDto artistDto = new ArtistDto(
                    artist.Id, 
                    artist.FirstName,
                    artist.LastName,
                    artist.YearDebut,
                    new ArtistDetailDto(artist.ArtistDetails.Id, artist.ArtistDetails.StageName),
                    artist.ConcurrencyStamp
                );

                foreach (var track in artist.Tracks)
                {
                    artistDto.Tracks.Add(new ArtistTrackDto(track.Track.Id, track.Track.TrackTitle, track.Track.Duration, track.Track.ReleaseDate));
                }

                foreach (var album in artist.Albums)
                {
                    artistDto.Albums.Add(new ArtistAlbumDto(album.Album.Id, album.Album.AlbumTitle, album.Album.RecordLabel, album.Album.Year));
                }

                artistsDto.Add(artistDto);
            }

            return artistsDto;
        }
    }
}