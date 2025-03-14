using StudentAtendances.Models;
using StudentAtendances.Models.DTO;

namespace StudentAtendances.Repository.Interfaces.Groups
{
    public interface ISubjectRepository
    {
        Task<IList<StudentSubjectAttendance>> GetStudentSubjectAttendances(int subjectId);
        Task Update(List<StudentSubjectAttendance> attendances);
        Task<int> SaveChanges();

        #region Subject
        Task<IList<Subject>> GetSubjects(int groupId);
        Task AddSubject(Subject subject);
        Task UpdateSubject(UpdateSubjectParam updateSubjectParam);
        Task DeleteSubject(int id);
        #endregion

        #region Lecturer
        Task<Lecturer?> GetLecturer(string email, string password);
        #endregion
    }
}
