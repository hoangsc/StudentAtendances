using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentAtendances.Models;
using StudentAtendances.Models.DTO;
using StudentAtendances.Repository.Interfaces.Groups;

namespace StudentAtendances.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly ISubjectRepository _subjectRepository;

        private int LectureId => HttpContext.Session.GetInt32("LectureId") ?? 0;

        public SubjectController(
            ILogger<SubjectController> logger,
            ISubjectRepository subjectRepository
        )
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }

        public async Task<IActionResult> Index(int lectureId)
        {
            var loggedAccount = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(loggedAccount))
            {
                return RedirectToAction("Login", "Home");
            }

            var subjects = await _subjectRepository.GetSubjects(lectureId);
            return View(subjects);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.AddSubject(subject);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditSubject(
            [Bind("Id,SubjectCode,SubjectName,LecturerId")] Subject subject
        )
        {
            var studentSubjectAttendances = await _subjectRepository.GetStudentSubjectAttendances(
                subject.Id
            );

            var listDate = studentSubjectAttendances.DistinctBy(x => x.Date).ToList();

            // await _subjectRepository.UpdateSubject(subject);
            return View("EditSubject", listDate);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSubject(
            int subjectId,
            string subjectname,
            string subjectCode,
            List<DateTime> dates
        )
        {
            // Kiểm tra dữ liệu nhận được
            Console.WriteLine($"SubjectId: {subjectCode}");
            foreach (var date in dates)
            {
                Console.WriteLine($"Date: {date.ToString("dd/MM/yyyy")}");
            }

            await _subjectRepository.UpdateSubject(
                new UpdateSubjectParam
                {
                    SubjectId = subjectId,
                    SubjectCode = subjectCode,
                    SubjectName = subjectname,
                    Dates = dates,
                }
            );
            return RedirectToAction("Index", new { LectureId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubject(int id, int lectureId)
        {
            await _subjectRepository.DeleteSubject(id);

            return RedirectToAction("Index", new { lectureId });
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
