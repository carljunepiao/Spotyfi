using System;
using SpotyfiLib.Domain.ArtistAgg;
using System.Linq.Expressions;

namespace SpotyfiLib.Specification
{
    public class CompleteArtistDetailSpecification : Specification<Artist>
    {
        private readonly long id;

        public CompleteArtistDetailSpecification(long id){
            this.id = id;
        }

        public override Expression<Func<Artist, bool>> ToExpression(){
            return artist => artist.Id == this.id;
        }

    }
}