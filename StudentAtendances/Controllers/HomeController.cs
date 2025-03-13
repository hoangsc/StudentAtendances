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
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IGroupRepository _groupRepository;

        private string? loggedAccount;

        public HomeController(
            ILogger<HomeController> logger,
            IAttendanceRepository attendanceRepository,
            IGroupRepository groupRepository
        )
        {
            _logger = logger;
            _attendanceRepository = attendanceRepository;
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Kiểm tra đã login hay chưa
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            loggedAccount = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(loggedAccount))
            {
                return View("Login");
            }

            return RedirectToAction("Index", "Subject");
        }

        public async Task<IActionResult> SubjectAttendance(int subjectId = 1)
        {
            var studentSubjectAttendances = await _groupRepository.GetStudentSubjectAttendances(
                subjectId
            );
            var studentAttendances = studentSubjectAttendances
                .DistinctBy(x => x.StudentId)
                .ToList();

            return View("Index", studentAttendances);
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
            var lecturer = await _groupRepository.GetLecturer(email, password);

            if ((email == "admin@gmail.com" && password == "123") || lecturer != null)
            {
                HttpContext.Session.SetString("Username", lecturer?.Name ?? "Admin");

                TempData["Message"] = "Login successful!";
                return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chính
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
