using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotyfiLib.Helper
{
    public static class NullOrEmptyExtension
    {
        public static bool isNullOrEmpty <T> (this IEnumerable<T> source){
            if(source!=null){
                return !source.Any();
            }

            return true;
        }
    }
}