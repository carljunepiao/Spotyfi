using System;
using System.Collections.Generic;
using System.Text;
using SpotyfiLib.Presentation;

namespace SpotyfiLib.Application
{
    public interface IRecordService
    {
        void CreateRecord(RecordDto partyDto);
        void DeleteRecord(long id);
        ICollection<RecordDto> GetAllRecords();
    }
}
