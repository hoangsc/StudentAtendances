using Microsoft.EntityFrameworkCore;
using StudentAtendances.Models;
using StudentAtendances.Repository.Interfaces.Groups;

namespace StudentAtendances.Repository.Interfaces
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task Add(Group group)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<StudentSubjectAttendance>?> GetAll()
        {
            var result = await _context
                .StudentSubjectAttendances.Where(ssa => ssa.SubjectId == 1)
                .OrderBy(ssa => ssa.Student.Name) // Sắp xếp theo tên sinh viên
                .GroupBy(ssa => new
                {
                    ssa.StudentId,
                    ssa.Student.Name,
                    ssa.SubjectId,
                    ssa.Student.StudentCode,
                })
                .Select(g => new StudentAttendance
                {
                    StudentId = g.Key.StudentId,
                    StudentName = g.Key.Name,
                    StudentCode = g.Key.StudentCode,
                    SubjectId = g.Key.SubjectId,
                    Dates = g.Select(x => x.IsPresent).ToList(), // Lấy danh sách ngày điểm danh
                })
                .ToListAsync();

            var groups = await _context
                .StudentSubjectAttendances.Where(x => x.SubjectId == 1)
                .Include(x => x.Student)
                .Include(x => x.Subject)
                .ToListAsync();

            return groups;
        }

        public Task<Group?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public Task Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
