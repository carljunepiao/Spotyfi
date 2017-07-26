using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SpotyfiLib.Presentation;
using SpotyfiLib.Application;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpotyfiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/records")]
    public class RecordController : Controller
    {
        private readonly IRecordService _recordService;

        public RecordController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpPost]
        public IActionResult CreateRecord() //Not working properly | this should not be used. (waka sab out sa code)
        {
            RecordDto recordDto;
            try
            {
                recordDto = new RecordDto();
                _recordService.CreateRecord(recordDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecord(long id)
        {
            try
            {
                _recordService.DeleteRecord(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        public IActionResult GetAllRecords()
        {
            return Json(_recordService.GetAllRecords());
        }
    }
}
