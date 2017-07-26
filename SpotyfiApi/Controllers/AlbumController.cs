using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotyfiLib.Domain.AlbumAgg;
using SpotyfiLib.Application;
using Microsoft.AspNetCore.Authorization;
using SpotyfiLib.Presentation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SpotyfiLib.SpotyfiLib.Infrastructure.Cross_Cutting.LoggingFac;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpotyfiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/albums")]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly ILogger log = FedLogger.CreateLogger<AlbumController>();

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            return Json(_albumService.getAllAlbums());
        }

        //[HttpGet("{id}")]
        //public IActionResult GetAlbumById(long id)
        //{
        //    return Json(_albumService);
        //}
        

        //[Authorize("album.modify")]
        [HttpPut("{id}/add_track_to_album")]
        public IActionResult AddTrackAlbum(long id) //long id, [FromBody] TrackDto trackDto
        {
            try
            {
                //TESTING
                TrackDto trackDto = new TrackDto(1,"Fight of the Machine", 2, 2019, 321);

                _albumService.addTrackAlbum(id, trackDto);
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

        //// GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
