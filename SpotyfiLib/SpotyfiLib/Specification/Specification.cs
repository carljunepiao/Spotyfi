using System;
using System.Collections.Generic;
using SpotyfiLib.Domain;
using System.Linq.Expressions;

namespace SpotyfiLib.Specification
{
    public abstract class Specification<TEntity> where TEntity : Entity
    {
        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public bool IsSatisfiedBy(TEntity entity){
            Func<TEntity, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
}