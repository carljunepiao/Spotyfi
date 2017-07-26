using System;

namespace SpotyfiLib.Domain.RecordAgg
{
    public class Record : Entity
    {
        public uint ConcurrencyStamp { get; set; }
        // public string DisplayName { get; set; }
        public Record(){}
    }
}