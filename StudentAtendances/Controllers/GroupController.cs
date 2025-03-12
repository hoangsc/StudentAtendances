using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentAtendances.Models;
using StudentAtendances.Repository.Interfaces;
using StudentAtendances.Repository.Interfaces.Groups;

namespace StudentAtendances.Controllers
{
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IGroupRepository _groupRepository;

        public GroupController(ILogger<GroupController> logger, IGroupRepository groupRepository)
        {
            _logger = logger;
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> Index()
        {
            var studentSubjectAttendances = await _groupRepository.GetStudentSubjectAttendances(1);
            var studentAttendances = studentSubjectAttendances
                .DistinctBy(x => x.StudentId)
                .ToList();

            return View(studentAttendances);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAttendance(List<StudentSubjectAttendance> attendances)
        {
            if (attendances == null || attendances.Count == 0)
            {
                return BadRequest("Không có dữ liệu điểm danh.");
            }

            await _groupRepository.Update(attendances);

            return RedirectToAction("Index");
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
