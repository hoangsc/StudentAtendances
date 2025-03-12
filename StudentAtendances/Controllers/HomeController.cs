﻿using System.Diagnostics;
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

        public async Task<IActionResult> Index(int subjectId = 1)
        {
            var studentSubjectAttendances = await _groupRepository.GetStudentSubjectAttendances(
                subjectId
            );
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
