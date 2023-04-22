using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_management.Models;

namespace school_management.Controllers
{
    public class TimetableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/api/timetable")]
        public IActionResult Timetable()
        {
            GeneticAlgorithm algorithm = new GeneticAlgorithm();
            return Json(algorithm.Evolve());
        }

        [HttpGet]
        [Route("/api/student")]
        public IActionResult Student()
        {
            return Json(Init.Students);
        }

        [HttpGet]
        [Route("api/course")]
        public IActionResult Course()
        {
            return Json(Init.Courses);
        }
    }
}
