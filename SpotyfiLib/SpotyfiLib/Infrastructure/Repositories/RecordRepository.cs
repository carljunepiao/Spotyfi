using System;
using SpotyfiLib.Domain.RecordAgg;
using SpotyfiLib.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using SpotyfiLib.Specification;

namespace SpotyfiLib.Infrastructure.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly MainUnitOfWork _unitOfWork;

        public RecordRepository(MainUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MainUnitOfWork unitOfWork => _unitOfWork; //What does this do?


        public void Add(Record entity)
        {
            _unitOfWork._firstGenUoW.Records.Add(entity);
        }

        public IEnumerable<Record> Find(Specification<Record> spec)
        {
            throw new NotImplementedException();
        }

        public Record Get(long id)
        {
            return _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Record> GetAll()
        {
            return _unitOfWork._firstGenUoW.Records.AsNoTracking().AsEnumerable();
        }

        public void Modify(Record entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Record entity)
        {
            var record = _unitOfWork._firstGenUoW.Records.FirstOrDefault(p => p.Id == entity.Id);

            if(record != null)
            {
                var artist = _unitOfWork._secondGenUoW.Artists.FirstOrDefault(a => a.Id == entity.Id);

                if(artist != null)
                {
                    _unitOfWork._secondGenUoW.Artists.Remove(artist);
                    _unitOfWork._secondGenUoW.Commit();
                }

                _unitOfWork._firstGenUoW.Records.Remove(record);
                _unitOfWork._firstGenUoW.Commit();
            }
        }
    }
}