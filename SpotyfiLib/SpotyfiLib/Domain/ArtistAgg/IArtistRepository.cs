using System;
using SpotyfiLib.Infrastructure.Repositories;
using SpotyfiLib.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Text;

namespace SpotyfiLib.Domain.ArtistAgg
{
    public interface IArtistRepository : IRepository<Artist, long>
    {
        
    }
}