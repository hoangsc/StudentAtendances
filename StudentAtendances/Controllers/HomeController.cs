using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentAtendances.Models;
using StudentAtendances.Repository.Interfaces;
using StudentAtendances.Repository.Interfaces.Groups;

namespace StudentAtendances.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IGroupRepository _groupRepository;

        public HomeController(ILogger<HomeController> logger, IGroupRepository groupRepository)
        {
            _logger = logger;
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> Index()
        {
            var studentSubjectAttendances = await _groupRepository.GetAll();
            var studentAttendances = studentSubjectAttendances
                .DistinctBy(x => x.StudentId)
                .ToList();

            var students = new List<dynamic>
            {
                new
                {
                    Ten = "Nguyễn Văn A",
                    MaSV = "SV001",
                    Ngay1 = true,
                    Ngay2 = false,
                    Ngay3 = true,
                },
                new
                {
                    Ten = "Trần Thị B",
                    MaSV = "SV002",
                    Ngay1 = false,
                    Ngay2 = true,
                    Ngay3 = false,
                },
                new
                {
                    Ten = "Lê Văn C",
                    MaSV = "SV003",
                    Ngay1 = true,
                    Ngay2 = true,
                    Ngay3 = false,
                },
                new
                {
                    Ten = "Lê Văn D",
                    MaSV = "SV003",
                    Ngay1 = true,
                    Ngay2 = true,
                    Ngay3 = false,
                },
            };

            ViewBag.StudentAttendances = studentAttendances;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                }
            );
        }
    }
}
