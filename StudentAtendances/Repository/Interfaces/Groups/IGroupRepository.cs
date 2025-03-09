using StudentAtendances.Models;

namespace StudentAtendances.Repository.Interfaces.Groups
{
    public interface IGroupRepository
    {
        Task<IList<StudentSubjectAttendance>> GetAll();
        Task<Group?> GetById(int id);
        Task Add(Group group);
        Task Update(Group group);
        Task Delete(int id);
        Task<int> SaveChanges();
    }
}
