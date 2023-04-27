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
        [Route("/student")]
        public IActionResult Student()
        {
            Init.Default();
            return View(Init.Students);
        }

        [HttpGet]
        [Route("/api/search")]
        public IActionResult Search(string studentId)
        {
            studentId = studentId.Trim();
            Timetable timetable = new Timetable();
            foreach (var cls in Init.Timetable.Classes)
            {
                if (cls.Students.Select(x => x.StudentId).ToList().Contains(studentId))
                {
                    timetable.Classes.Add(cls);
                }
            }
            return Json(timetable);
        }

        [HttpGet]
        [Route("api/course")]
        public IActionResult Course()
        {
            return Json(Init.Courses);
        }
    }
}
