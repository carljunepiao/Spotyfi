using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotyfiLib.Application;
using SpotyfiLib.SpotyfiLib.Infrastructure.Cross_Cutting.LoggingFac;
using Microsoft.Extensions.Logging;
using SpotyfiLib.Presentation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpotyfiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/artists")]
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly ILogger log = FedLogger.CreateLogger<ArtistController>();

        //// GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        //[Authorize("artist.write")] //Working
        [HttpPost]
        public IActionResult CreateArtist() //[FromBody] ArtistDto artist 
        {
            ArtistDto artist;
            try
            {
                int i = 0;
                //----------------------------TESTING-----------------------------------

                //Console.WriteLine("Goes to creation of artist.");
                //ArtistDetailDto artistDetails = new ArtistDetailDto(1,"Mad Max");
                //artist = new ArtistDto(1, "Carl", "Piao", 2013, artistDetails,232);
                //Console.WriteLine("Creating artist #{0}...",i++);

                ArtistDetailDto artistDetail = new ArtistDetailDto(1, "Mad Max");
                artist = new ArtistDto(1, "Lito", "Piao", 2018, artistDetail, 111);
                Console.WriteLine("Creating artist #{0}...", i++);

                _artistService.CreateArtist(artist);


            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(StatusCodes.Status200OK);
        }

        // [Authorize("artist.delete")] //Working
        [HttpDelete("{id}")]
        public IActionResult DeleteArtist(long id)
        {
            try
            {
                _artistService.DeleteArtist(id);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return NotFound(StatusCodes.Status404NotFound);
            }

            return Ok(StatusCodes.Status200OK);
        }

        // [Authorize("artist.read")]
        [HttpGet]
        public IActionResult GetAllArtists()
        {
            return Json(_artistService.GetAllArtist());
        }

        [HttpGet("{id}")]
        public IActionResult GetArtist(long id)
        {
            try
            {
                Console.WriteLine($"Goes to Get with id: {id}",id);
                return Json(_artistService.getArtistById(id));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return NotFound(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[Authorize("artist.read")]
        [HttpGet("{id}/tracks")]
        public IActionResult GetArtistTracks(long id)
        {
            try
            {
                var artist = _artistService.getArtistById(id);
                var tracks = artist.Tracks;

                return Json(tracks.ToList());
            }
            catch (ArgumentException e)
            {
                log.LogError("Error in getting tracks of artist with id: {0}\n Error: {1}",id,e.Message);
            }

            return Json("");
        }

        //[Authorize("artist.modify")] // Working
        [HttpPut("{id}/add_track_to_artist")]
        public IActionResult AddTrackToArtist(long id) //long id, [FromBody] TrackDto trackDto
        {
            try
            {
                //Testing
                Console.WriteLine("ID artist: ",id);
                TrackDto trackDto = new TrackDto(id,"Meh",1,2017,222);
                
                _artistService.AddTrackArtist(id, trackDto);
            }
            catch (ArgumentException e)
            {
                log.LogError("Error on adding a new track. " + e.Message);
                return NotFound(StatusCodes.Status404NotFound);
            }

            return Ok(StatusCodes.Status200OK);
        }

        //[Authorize("artist.modify")] // Working
        [HttpPut("{id}/add_album_to_artist")]
        public IActionResult AddAlbumToArtist(long id){ //long id, [FromBody] AlbumDto albumDto
            try{
                //TESTING
                AlbumDto albumDto = new AlbumDto(id,"MehAlbum","Lion Inc.",222,2017);
                _artistService.AddAlbumArtist(id, albumDto);
            }
            catch(ArgumentException e)
            {
                log.LogError("Error adding a new album. " + e.Message);
                return NotFound(StatusCodes.Status404NotFound);
            }
            return Ok(StatusCodes.Status200OK);
        }

        //[Authorize("artist.modify")] // Working
        [HttpPut("{id}")]
        public IActionResult UpdateArtistInfo(long id) //long id, [FromBody] ArtistDto artistDto
        {
            try
            {
                ArtistDetailDto artistDetail = new ArtistDetailDto(2, "Babyo");
                var artist = new ArtistDto(2, "Ina Marie", "Kintanar", 2013, artistDetail, 212);

                _artistService.ModifyArtist(id, artist);
            }
            catch (ArgumentException e)
            {
                log.LogError("Error on updating artist:  " + e.Message);
                return NotFound(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(StatusCodes.Status200OK);
        }
    }
}
