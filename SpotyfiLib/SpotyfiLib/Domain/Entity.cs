using System;
using System.Collections.Generic;
using System.Text;

namespace SpotyfiLib.Domain
{
    public abstract class Entity
    {
        public long Id {get;set;}

        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Entity)){
                return false;
            }
            Entity e = (Entity)obj;

            return this.Id == e.Id;
        }

        public override int GetHashCode(){
            return base.GetHashCode();
        }
    }
}