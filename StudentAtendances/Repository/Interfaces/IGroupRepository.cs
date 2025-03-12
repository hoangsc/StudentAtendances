using StudentAtendances.Models;

namespace StudentAtendances.Repository.Interfaces.Groups
{
    public interface IGroupRepository
    {
        Task<IList<StudentSubjectAttendance>> GetStudentSubjectAttendances(int subjectId);

        Task<Group?> GetById(int id);
        Task Add(Group group);
        Task Update(List<StudentSubjectAttendance> attendances);
        Task Delete(int id);
        Task<int> SaveChanges();

        #region Subject
        Task<IList<Subject>> GetSubjects(int groupId);
        Task AddSubject(Subject subject);
        Task UpdateSubject(Subject subject);
        Task DeleteSubject(int id);
        #endregion
    }
}
