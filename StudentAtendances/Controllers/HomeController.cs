using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAtendances.Models;
using StudentAtendances.Repository.Interfaces;
using StudentAtendances.Repository.Interfaces.Groups;

namespace StudentAtendances.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISubjectRepository _subjectRepository;

        private string? loggedAccount;

        public HomeController(ILogger<HomeController> logger, ISubjectRepository subjectRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }

        /// <summary>
        /// Kiểm tra đã login hay chưa
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(int lectureId)
        {
            loggedAccount = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(loggedAccount))
            {
                return View("Login");
            }

            return RedirectToAction("Index", "Subject", new { lectureId });
        }

        public async Task<IActionResult> SubjectAttendance(int subjectId)
        {
            var studentSubjectAttendances = await _subjectRepository.GetStudentSubjectAttendances(
                subjectId
            );
            var studentAttendances = studentSubjectAttendances
                .DistinctBy(x => x.StudentId)
                .ToList();

            return View("SubjectAttendance", studentAttendances);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAttendance(List<StudentSubjectAttendance> attendances)
        {
            if (attendances == null || attendances.Count == 0)
            {
                return BadRequest("Không có dữ liệu điểm danh.");
            }

            var subjectId = attendances[0].SubjectId;

            await _subjectRepository.Update(attendances);

            return RedirectToAction("SubjectAttendance", new { subjectId });
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

        #region Login

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Username");

            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> ValidateLogin(string email, string password, bool remember)
        {
            var lecturer = await _subjectRepository.GetLecturer(email, password);

            if (lecturer != null)
            {
                HttpContext.Session.SetString("Username", lecturer?.Name ?? "Admin");
                HttpContext.Session.SetInt32("LectureId", lecturer?.Id ?? 0);

                TempData["Message"] = "Login successful!";
                return RedirectToAction("Index", "Home", new { lectureId = lecturer.Id }); // Chuyển hướng đến trang chính
            }
            else
            {
                ViewBag.Error = "Invalid email or password!";
                return View("Login"); // Trả về lại trang đăng nhập với thông báo lỗi
            }
        }

        #endregion
    }
}
