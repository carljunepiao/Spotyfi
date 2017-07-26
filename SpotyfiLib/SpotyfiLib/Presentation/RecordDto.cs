using System;

namespace SpotyfiLib.Presentation
{
    public class RecordDto : BaseDto
    {
        public uint ConcurrencyStamp {get;set;}
        public RecordDto(){}
    }
}