using System;
using System.Collections.Generic;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Infrastructure.UnitOfWork;
using System.Linq;
using SpotyfiLib.Specification;
using SpotyfiLib.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SpotyfiLib.Domain.RecordAgg;

namespace SpotyfiLib.Infrastructure.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly MainUnitOfWork _unitOfWork;

        public TrackRepository(MainUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public MainUnitOfWork unitOfWork => _unitOfWork; //What does this do?

        public void Add(Track entity){
            Record record = new Record();
            _unitOfWork._firstGenUoW.Records.Add(record);
            _unitOfWork._firstGenUoW.Commit();
            
            entity.Id = record.Id;

            _unitOfWork._secondGenUoW.Tracks.Add(entity);
            _unitOfWork._secondGenUoW.Commit();
        }

        public void Remove(Track entity){
            _unitOfWork._secondGenUoW.Tracks.Remove(entity);
            _unitOfWork._secondGenUoW.Commit();

            var record = _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == entity.Id);
            _unitOfWork._firstGenUoW.Records.Remove(record);
            _unitOfWork._firstGenUoW.Commit();
        }

        public void Modify(Track entity){
            var record = _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == entity.Id);
            _unitOfWork._firstGenUoW.Entry(record).State = EntityState.Modified;
            _unitOfWork._firstGenUoW.Commit();

            // var track = _unitOfWork._secondGenUoW.Tracks.FirstOrDefault(p => p.Id == entity.Id); //not sure
            // track.TrackTitle = entity.TrackTitle;
            // track.Duration = entity.Duration;
            // track.ReleaseDate = entity.ReleaseDate;
            // _unitOfWork._secondGenUoW.Entry(track).State = EntityState.Modified;
            _unitOfWork._secondGenUoW.Commit();
        }
        
        public Track Get(long id){
            var track = _unitOfWork._secondGenUoW.Tracks.Where(a => a.Id == id)
                .Include(a => a.Artists).ThenInclude(a => a.Artist).ThenInclude(a => a.ArtistDetails)
                .Include(a => a.Albums).ThenInclude(a => a.Album)
                .Include(a => a.Genres).ThenInclude(a => a.Genre).FirstOrDefault();

            if(track!=null){
                var record = _unitOfWork._firstGenUoW.Records.Where(p => p.Id == id).FirstOrDefault();

                track = InheritanceConstructor.ReConstructTrack(record, track);
                return track;
            }
            return null;
        }

        public IEnumerable<Track> GetAll()
        {
            //return _unitOfWork._firstGenUoW.Records.AsNoTracking().ToList()
            //    .Join(_unitOfWork._secondGenUoW.Tracks
            //        .Include(t => t.Artists).ThenInclude(a => a.Artist).ThenInclude(a => a.ArtistDetails)
            //        .Include(a => a.Albums).ThenInclude(a => a.Album)
            //        .Include(a => a.Genres).ThenInclude(a => a.Genre)
            //        .AsNoTracking().ToList()
            //    , record => record.Id, track => track.Id, (record, track) => InheritanceConstructor.ReConstructTrack(record, track));

            return _unitOfWork._firstGenUoW.Records.AsNoTracking().ToList().Join(_unitOfWork._secondGenUoW.Tracks
                .Include(a => a.Artists).ThenInclude(a => a.Artist)
                .ThenInclude(a => a.ArtistDetails).AsNoTracking().ToList(), 
                record => record.Id, track => track.Id, (record, track) => InheritanceConstructor.ReConstructTrack(record, track));

            //return _unitOfWork._secondGenUoW.Tracks.AsNoTracking().ToList();
        }

        public IEnumerable<Track> Find(Specification<Track> spec)
        {
            var track = _unitOfWork._secondGenUoW.Tracks.Where(spec.ToExpression()); //what does this do?
            return track;
        }
    }
}