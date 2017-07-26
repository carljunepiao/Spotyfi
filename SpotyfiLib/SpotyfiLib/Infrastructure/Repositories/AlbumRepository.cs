using System;
using System.Collections.Generic;
using SpotyfiLib.Domain.AlbumAgg;
using SpotyfiLib.Infrastructure.UnitOfWork;
using SpotyfiLib.Specification;
using SpotyfiLib.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SpotyfiLib.Domain.RecordAgg;
using System.Linq;

namespace SpotyfiLib.Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly MainUnitOfWork _unitOfWork;

        public AlbumRepository(MainUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public MainUnitOfWork unitOfWork => _unitOfWork; //What does this do?

        public void Add(Album entity){
            Record record = new Record();
            _unitOfWork._firstGenUoW.Records.Add(record);
            _unitOfWork._firstGenUoW.Commit();
            
            entity.Id = record.Id;

            _unitOfWork._secondGenUoW.Albums.Add(entity);
            _unitOfWork._secondGenUoW.Commit();
        }
        
        public void Remove(Album entity){
            _unitOfWork._secondGenUoW.Albums.Remove(entity);
            _unitOfWork._secondGenUoW.Commit();

            var record = _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == entity.Id);
            _unitOfWork._firstGenUoW.Records.Remove(record);
            _unitOfWork._firstGenUoW.Commit();
        }

        public void Modify(Album entity){
            var record = _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == entity.Id);
            _unitOfWork._firstGenUoW.Entry(record).State = EntityState.Modified;
            _unitOfWork._firstGenUoW.Commit();

            _unitOfWork._secondGenUoW.Commit();
        }
        
        public Album Get(long id){
            var album = _unitOfWork._secondGenUoW.Albums.Where(a => a.Id == id)
                .Include(a => a.Artists).ThenInclude(a => a.Artist).ThenInclude(a => a.ArtistDetails)
                .Include(a => a.Tracks).ThenInclude(a => a.Track).FirstOrDefault();

            if(album!=null){
                var record = _unitOfWork._firstGenUoW.Records.Where(p => p.Id == id).FirstOrDefault();

                album = InheritanceConstructor.ReConstructAlbum(record, album);
                return album;
            }
            return null;
        }

        public IEnumerable<Album> GetAll()
        {
            return _unitOfWork._firstGenUoW.Records.AsNoTracking().ToList()
                .Join(_unitOfWork._secondGenUoW.Albums
                    .Include(t => t.Artists).ThenInclude(a => a.Artist).ThenInclude(a => a.ArtistDetails)
                    .Include(a => a.Tracks).ThenInclude(a => a.Track)
                , record => record.Id, album => album.Id, (record, album) => InheritanceConstructor.ReConstructAlbum(record, album));

            //return _unitOfWork._secondGenUoW.Albums.Include(a => a.Tracks).ThenInclude(a => a.Track).AsNoTracking().ToList();
        }

        public IEnumerable<Album> Find(Specification<Album> spec)
        {
            var album = _unitOfWork._secondGenUoW.Albums.Where(spec.ToExpression()); //what does this do?
            return album;
        }
    }
}