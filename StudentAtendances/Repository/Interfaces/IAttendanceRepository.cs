using StudentAtendances.Models;

namespace StudentAtendances.Repository.Interfaces.Groups
{
    public interface IAttendanceRepository
    {
        Task<List<StudentSubjectAttendance>> GetAllAttendances();
        Task UpdateAttendanceAsync(StudentSubjectAttendance attendance);
        Task SaveChangesAsync();
    }
}
