using facture_teachers.Helpers.Services;
using facture_teachers.Models.DB;
using facture_teachers.Models.Views.Lesson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace facture_teachers.Controllers.Teachers
{
    [Route("api/Teachers/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly LessonsService _lessonsServices;

        public LessonsController(facture_profesoresContext context)
        {
            _lessonsServices = new LessonsService(context);
        }

        [HttpPost]
        [Route("SaveLesson")]
        public ActionResult Postlesson([FromBody] AddLesson lesson)
        {
            var response = _lessonsServices.AddLesson(lesson);
            if (response.Code == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetLessons")]
        public ActionResult GetLessons( )
        {
            var response = _lessonsServices.GetListLessons();
            if (response.Code == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
