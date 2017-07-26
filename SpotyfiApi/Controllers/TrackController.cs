using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotyfiLib.Application;
using SpotyfiLib.Presentation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using SpotyfiLib.SpotyfiLib.Infrastructure.Cross_Cutting.LoggingFac;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpotyfiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/tracks")]
    public class TrackController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly ILogger log = FedLogger.CreateLogger<TrackController>();

        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet] //Working
        public IActionResult GetAllTracks()
        {
            return Json(_trackService.getAllTracks());
        }

        //[Authorize("track.modify")]
        [HttpPut("{id}/add_genre_to_track")]
        public IActionResult AddGenre(long id) //long id, [FromBody] GenreDto genreDto
        {
            try
            {
                Console.WriteLine(id);
                //Testing
                GenreDto genreDto = new GenreDto(id, "OPM", 221);

                _trackService.addGenreTrack(id, genreDto);
            }
            catch (ArgumentException e)
            {
                log.LogError("Error on updating track:  " + e.Message);
                return NotFound(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(StatusCodes.Status200OK);
        }

        //[Authorize("artist.modify")]
        [HttpPut("{id}")]
        public IActionResult UpdateTrackInfo(long id) //long id, [FromBody] TrackDto trackDto
        {
            try
            {
                //Testing
                TrackDto trackDto = new TrackDto(2, "Pancito", 1, 2018, 212);

                _trackService.modifyTrack(id, trackDto);
            }
            catch (ArgumentException e)
            {
                log.LogError("Error on updating track:  " + e.Message);
                return NotFound(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(StatusCodes.Status200OK);
        }
       
        //// GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
