using System;
using System.Collections.Generic;
using SpotyfiLib.Presentation;
using SpotyfiLib.Domain.AlbumAgg;
using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Domain.TrackAgg;
using AutoMapper;
using SpotyfiLib.Helper;

namespace SpotyfiLib.Application
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public AlbumService(IAlbumRepository albumRepo, IMapper mapper){
            _albumRepository = albumRepo;
            _mapper = mapper;
        }

        public void addTrackAlbum(long id, TrackDto trackDto)
        {
            var album = _albumRepository.Get(id);

            if(album == null){
                throw new ArgumentException($"There is no album with id:{id}");
            }

            Track track = new Track(trackDto.TrackTitle,trackDto.Duration,trackDto.ReleaseDate);
            album.addTrackinAlbum(track);
            _albumRepository.unitOfWork._secondGenUoW.Commit();
        }

        public ICollection<AlbumDto> getAllAlbums()
        {
            var albums = _albumRepository.GetAll();

            //if(!albums.isNullOrEmpty()){
            //    ICollection<AlbumDto> albumsDto = new LinkedList<AlbumDto>();
            //    AlbumDto albumDto = null;

            //    foreach (Album album in albums)
            //    {
            //        albumDto = new AlbumDto(album.Id, album.AlbumTitle, album.RecordLabel, album.ConcurrencyStamp,album.Year);

            //        if(album.Artists.Count > 0){
            //            foreach (ArtistAlbum artistAlbum in album.Artists)
            //            {
            //                albumDto.Artists.Add(new ArtistDto(
            //                    artistAlbum.Artist.Id,
            //                    artistAlbum.Artist.FirstName,
            //                    artistAlbum.Artist.LastName,
            //                    artistAlbum.Artist.YearDebut,
            //                    new ArtistDetailDto {StageName = artistAlbum.Artist.ArtistDetails.StageName},
            //                    artistAlbum.Artist.ConcurrencyStamp
            //                ));
            //            }
            //        }
            //        albumsDto.Add(albumDto);
            //    }

            //    //return MapAlbums(albumsDto);
            //    return albumsDto;
            //}

            //return null;

            return !albums.isNullOrEmpty() ? _mapper.Map<ICollection<AlbumDto>>(MapAlbums(albums)) : null; //create customized mapper here

        }

        //MapAlbumsDto
        public ICollection<AlbumDto> MapAlbums(IEnumerable<Album> albums)
        {
            ICollection<AlbumDto> albumsDto = new LinkedList<AlbumDto>();

            foreach (var album in albums)
            {
                AlbumDto albumDto = new AlbumDto(
                    album.Id,
                    album.AlbumTitle,
                    album.RecordLabel,
                    album.ConcurrencyStamp,
                    album.Year
                );

                foreach (var artist in album.Artists)
                {
                    albumDto.Artists.Add(new ArtistDto(artist.Artist.Id, 
                        artist.Artist.FirstName, 
                        artist.Artist.LastName,
                        artist.Artist.YearDebut,
                        new ArtistDetailDto(artist.Artist.ArtistDetails.Id, artist.Artist.ArtistDetails.StageName),
                        artist.Artist.ConcurrencyStamp));
                }

                foreach (var track in album.Tracks)
                {
                    albumDto.Tracks.Add(new TrackAlbumDto(track.Track.Id, track.Track.TrackTitle, track.Track.Duration, track.Track.ReleaseDate));
                }

                albumsDto.Add(albumDto);
            }

            return albumsDto;
        }

        public AlbumDto getAlbumById(long id)
        {
            var album = _albumRepository.Get(id);

            if (album == null)
            {
                throw new ArgumentException($"There is no artist wih id:{id}");
            }

            return _mapper.Map<AlbumDto>(MapAlbum(album));
            // return _mapper.Map<AlbumDto>(album);
        }

        //MapAlbumDto
        public AlbumDto MapAlbum(Album album)
        {
            AlbumDto albumDto = new AlbumDto(
                album.Id,
                album.AlbumTitle,
                album.RecordLabel,
                album.ConcurrencyStamp,
                album.Year
            );

            foreach (var artist in album.Artists)
            {
                albumDto.Artists.Add(new ArtistDto(artist.Artist.Id, artist.Artist.FirstName, artist.Artist.LastName, artist.Artist.YearDebut, new ArtistDetailDto(artist.Artist.Id, artist.Artist.ArtistDetails.StageName), artist.Artist.ConcurrencyStamp));
            }

            foreach (var track in album.Tracks)
            {
                albumDto.Tracks.Add(new TrackAlbumDto(track.Track.Id, track.Track.TrackTitle, track.Track.Duration, track.Track.ReleaseDate));
            }

            return albumDto;
        }
    }
}