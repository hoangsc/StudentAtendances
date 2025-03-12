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

        public async Task<IList<StudentSubjectAttendance>> GetStudentSubjectAttendances(
            int subjectId
        )
        {
            var groups = await _context
                .StudentSubjectAttendances.Where(x => x.SubjectId == subjectId)
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

        public async Task Update(List<StudentSubjectAttendance> attendances)
        {
            foreach (var attendance in attendances)
            {
                foreach (var studentAttendance in attendance.Student.StudentSubjectAttendances)
                {
                    var existingStudentAttendance =
                        await _context.StudentSubjectAttendances.FirstOrDefaultAsync(a =>
                            a.StudentId == attendance.StudentId
                            && a.SubjectId == attendance.SubjectId
                            && a.Id == studentAttendance.Id
                        );
                    if (existingStudentAttendance != null)
                    {
                        existingStudentAttendance.IsPresent = studentAttendance.IsPresent;
                    }
                }
            }
            await this.SaveChanges();
        }

        #region Subject
        public async Task<IList<Subject>> GetSubjects(int lectureId)
        {
            var groups = await _context
                .Subjects.Where(x => x.LecturerId == lectureId)
                .Include(x => x.Lecturer)
                .ToListAsync();

            return groups;
        }

        public async Task AddSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubject(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
