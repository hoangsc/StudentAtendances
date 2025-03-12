using Microsoft.EntityFrameworkCore;
using StudentAtendances.Models;
using StudentAtendances.Repository.Interfaces.Groups;

namespace StudentAtendances.Repository.Interfaces
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentSubjectAttendance>> GetAllAttendances()
        {
            return await _context
                .StudentSubjectAttendances.Include(a => a.Student)
                .Include(a => a.Subject)
                .ToListAsync();
        }

        public async Task UpdateAttendanceAsync(StudentSubjectAttendance attendance)
        {
            var existingAttendance = await _context.StudentSubjectAttendances.FirstOrDefaultAsync(
                a =>
                    a.StudentId == attendance.StudentId
                    && a.SubjectId == attendance.SubjectId
                    && a.Date == attendance.Date
            );

            if (existingAttendance != null)
            {
                existingAttendance.IsPresent = attendance.IsPresent;
            }
            else
            {
                _context.StudentSubjectAttendances.Add(attendance);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
