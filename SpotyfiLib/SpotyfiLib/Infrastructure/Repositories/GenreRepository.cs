using System;
using System.Collections.Generic;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Infrastructure.UnitOfWork;
using SpotyfiLib.Specification;
using SpotyfiLib.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SpotyfiLib.Domain.RecordAgg;
using System.Linq;

namespace SpotyfiLib.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MainUnitOfWork _unitOfWork;

        public GenreRepository(MainUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public MainUnitOfWork unitOfWork => _unitOfWork; //What does this do?

        public void Add(Genre entity){
            Record record = new Record();
            _unitOfWork._firstGenUoW.Records.Add(record);
            _unitOfWork._firstGenUoW.Commit();
            
            entity.Id = record.Id;

            _unitOfWork._secondGenUoW.Genres.Add(entity);
            _unitOfWork._secondGenUoW.Commit();
        }
        public void Remove(Genre entity){
            _unitOfWork._secondGenUoW.Genres.Remove(entity);
            _unitOfWork._secondGenUoW.Commit();

            var record = _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == entity.Id);
            _unitOfWork._firstGenUoW.Records.Remove(record);
            _unitOfWork._firstGenUoW.Commit();
        }

        public void Modify(Genre entity){
            var record = _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == entity.Id);
            _unitOfWork._firstGenUoW.Entry(record).State = EntityState.Modified;
            _unitOfWork._firstGenUoW.Commit();

            _unitOfWork._secondGenUoW.Commit();
        }
        
        public Genre Get(long id){
            var genre = _unitOfWork._secondGenUoW.Genres.Where(a => a.Id == id)
                .Include(a => a.Tracks).ThenInclude(a => a.Track).FirstOrDefault();

            if(genre!=null){
                var record = _unitOfWork._firstGenUoW.Records.Where(p => p.Id == id).FirstOrDefault();

                genre = InheritanceConstructor.ReConstructGenre(record, genre);
                return genre;
            }
            return null;
        }

        public IEnumerable<Genre> GetAll()
        {
            //return _unitOfWork._firstGenUoW.Records.AsNoTracking().ToList()
            //    .Join(_unitOfWork._secondGenUoW.Genres
            //        .Include(a => a.Tracks).ThenInclude(a => a.Track)
            //    ,record => record.Id, genre => genre.Id,(record, genre) => InheritanceConstructor.ReConstructGenre(record,genre));

            return _unitOfWork._secondGenUoW.Genres.AsNoTracking().ToList();
        }

        public IEnumerable<Genre> Find(Specification<Genre> spec)
        {
            var genre = _unitOfWork._secondGenUoW.Genres.Where(spec.ToExpression()); //what does this do?
            return genre;
        }
    }
}