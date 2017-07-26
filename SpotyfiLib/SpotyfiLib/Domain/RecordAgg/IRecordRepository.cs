using System;
using SpotyfiLib.Infrastructure.Repositories;
using SpotyfiLib.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Text;

namespace SpotyfiLib.Domain.RecordAgg
{
    public interface IRecordRepository : IRepository<Record, long>
    {
        
    }
}