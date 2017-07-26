using System;
using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Infrastructure.UnitOfWork;
using SpotyfiLib.Domain.RecordAgg;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SpotyfiLib.Infrastructure;
using SpotyfiLib.Specification;

namespace SpotyfiLib.Infrastructure.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly MainUnitOfWork _unitOfWork;

        public ArtistRepository(MainUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }
        public MainUnitOfWork unitOfWork => _unitOfWork; //What does this do?

        public void Add(Artist entity){
            Record record = new Record();
            _unitOfWork._firstGenUoW.Records.Add(record);
            _unitOfWork._firstGenUoW.Commit();

            Console.WriteLine(record.Id);
            entity.Id = record.Id;

            _unitOfWork._secondGenUoW.Artists.Add(entity);
            _unitOfWork._secondGenUoW.Commit();
        }
        
        public void Remove(Artist entity){
            _unitOfWork._secondGenUoW.Artists.Remove(entity);
            _unitOfWork._secondGenUoW.Commit();

            var record = _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == entity.Id);
            _unitOfWork._firstGenUoW.Records.Remove(record);
            _unitOfWork._firstGenUoW.Commit();
        }

        public void Modify(Artist entity){
            var record = _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == entity.Id);
            _unitOfWork._firstGenUoW.Entry(record).State = EntityState.Modified;
            _unitOfWork._firstGenUoW.Commit();

            // var artist = _unitOfWork._secondGenUoW.Artists.FirstOrDefault(p => p.Id == entity.Id); //not sure
            // artist.FirstName = entity.FirstName;
            // artist.LastName = entity.LastName;
            // _unitOfWork._secondGenUoW.Entry().State = EntityState.Modified;
            _unitOfWork._secondGenUoW.Commit();
        }
        
        public Artist Get(long id){
            // var artist = _unitOfWork._secondGenUoW.Artists.Where(a => a.Id == id).Include(a => a.ArtistDetails)
            //     .Include(a => a.ArtistDetails).Include(a => a.Tracks).ThenInclude(t => t.Track).Include(a => a.Albums)
            //         .ThenInclude(a => a.Album).FirstOrDefault();

            //var artist = _unitOfWork._secondGenUoW.Artists.Where(a => a.Id == id).Include(a => a.ArtistDetails).FirstOrDefault();

            var artist = _unitOfWork._secondGenUoW.Artists.Where(a => a.Id == id)
               .Include(a => a.ArtistDetails)
               .Include(a => a.Tracks).ThenInclude(t => t.Track)
               .Include(a => a.Albums).ThenInclude(a => a.Album).FirstOrDefault();

            //var artist = _unitOfWork._secondGenUoW.Artists.Where(a => a.Id == id).FirstOrDefault();

            if (artist != null)
            {
                var record = _unitOfWork._firstGenUoW.Records.Where(p => p.Id == id).FirstOrDefault();

                artist = InheritanceConstructor.ReConstructArtist(record, artist);

                return artist;
            }
            return null;
        }

        public IEnumerable<Artist> GetAll(){
            try
            {
                return _unitOfWork._firstGenUoW.Records.AsNoTracking().ToList()
                .Join(_unitOfWork._secondGenUoW.Artists
                    .Include(a => a.ArtistDetails)
                    .Include(a => a.Tracks).ThenInclude(t => t.Track)
                    .Include(a => a.Albums).ThenInclude(al => al.Album)
                    .AsNoTracking().ToList()
                    , record => record.Id, artist => artist.Id, (record, artist) => InheritanceConstructor
                        .ReConstructArtist(record, artist)).OrderBy(a => a.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
            //return _unitOfWork._secondGenUoW.Artists.AsNoTracking().ToList();
        }

        public IEnumerable<Artist> Find(Specification<Artist> spec){
            var artist = _unitOfWork._secondGenUoW.Artists.Where(spec.ToExpression());
            return artist;
        }
    }
}