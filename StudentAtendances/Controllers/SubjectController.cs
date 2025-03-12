using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentAtendances.Models;
using StudentAtendances.Repository.Interfaces;
using StudentAtendances.Repository.Interfaces.Groups;

namespace StudentAtendances.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IGroupRepository _groupRepository;

        public SubjectController(
            ILogger<SubjectController> logger,
            IAttendanceRepository attendanceRepository,
            IGroupRepository groupRepository
        )
        {
            _logger = logger;
            _attendanceRepository = attendanceRepository;
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _groupRepository.GetSubjects(1);
            return View(subjects);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _groupRepository.AddSubject(subject);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> EditSubject(
            [Bind("Id,SubjectCode,SubjectName,LecturerId")] Subject subject
        )
        {
            await _groupRepository.UpdateSubject(subject);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            await _groupRepository.DeleteSubject(id);
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
