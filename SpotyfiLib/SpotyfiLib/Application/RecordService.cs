using System;
using System.Collections.Generic;
using System.Text;
using SpotyfiLib.Presentation;
using SpotyfiLib.Domain.RecordAgg;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using SpotyfiLib.Helper;

namespace SpotyfiLib.Application
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordService(IRecordRepository recordRepo)
        {
            _recordRepository = recordRepo;
        }

        public void CreateRecord(RecordDto recordDto)
        {
            _recordRepository.Add(new Record());
            _recordRepository.unitOfWork.Commit();
        }

        public void DeleteRecord(long id)
        {
            var record = _recordRepository.Get(id);

            using (var transaction = _recordRepository.unitOfWork._firstGenUoW.Database.BeginTransaction())
            {
                var tr = transaction.GetDbTransaction();
                _recordRepository.unitOfWork._secondGenUoW.Database.UseTransaction(tr);
                _recordRepository.Remove(record);

                transaction.Commit();
            }
        }

        public ICollection<RecordDto> GetAllRecords()
        {
            var records = _recordRepository.GetAll();

            if (!records.isNullOrEmpty())
            {
                ICollection<RecordDto> recordDtos = new LinkedList<RecordDto>();
                RecordDto recordDto = null;

                foreach (Record record in records)
                {
                    recordDto = new RecordDto()
                    {
                        Id = record.Id
                    };
                    recordDtos.Add(recordDto);
                }

                return recordDtos;
            }

            return null;
        }
    }
}
