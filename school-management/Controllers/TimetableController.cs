using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using school_management.Models;

namespace school_management.Controllers
{
    public class TimetableController : Controller
    {
        private readonly SchoolManagementContext _dbContext;
        private readonly IMapper _mapper;
        public TimetableController(SchoolManagementContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

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
    }
}
