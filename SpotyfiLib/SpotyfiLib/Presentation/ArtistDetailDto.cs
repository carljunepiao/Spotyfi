using System;

namespace SpotyfiLib.Presentation
{
    public class ArtistDetailDto : BaseDto
    {
        public string StageName {get;set;}
        public ArtistDetailDto(){}
        public ArtistDetailDto(long id, string stageName){
            Id = id;
            StageName = stageName;
        }
    }
}