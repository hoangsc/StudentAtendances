using Microsoft.EntityFrameworkCore;
using StudentAtendances.Models;
using StudentAtendances.Models.DTO;
using StudentAtendances.Repository.Interfaces.Groups;

namespace StudentAtendances.Repository.Interfaces
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task Add(Group group)
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
            var subjects = await _context
                .Subjects.Where(x => x.LecturerId == lectureId)
                .Include(x => x.Lecturer)
                .ToListAsync();

            return subjects;
        }

        public async Task AddSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubject(UpdateSubjectParam updateSubjectParam)
        {
            try
            {
                var existingSubject = await _context.Subjects.FirstOrDefaultAsync(x =>
                    x.Id == updateSubjectParam.SubjectId
                );

                if (existingSubject != null)
                {
                    existingSubject.SubjectCode = updateSubjectParam.SubjectCode;
                    existingSubject.SubjectName = updateSubjectParam.SubjectName;
                }

                var date = updateSubjectParam.Dates;

                // Xoá những record theo ngày hiện tại trong database
                var datesNeedToRemove = await _context
                    .StudentSubjectAttendances.Where(x =>
                        x.SubjectId == updateSubjectParam.SubjectId && !date.Contains(x.Date)
                    )
                    .ExecuteDeleteAsync();

                // Lấy toàn bộ danh sách ngày có trong database
                var datesInDb = await _context
                    .StudentSubjectAttendances.Where(x =>
                        x.SubjectId == updateSubjectParam.SubjectId
                    )
                    .Select(x => x.Date)
                    .Distinct()
                    .ToListAsync();

                // Lọc ra danh sách ngày có trong `date` nhưng không có trong database
                var datesNeedToAdd = date.Except(datesInDb).ToList();

                var listStudentId = await _context
                    .StudentSubjectAttendances.Where(x =>
                        x.SubjectId == updateSubjectParam.SubjectId
                    )
                    .Select(x => x.StudentId)
                    .Distinct()
                    .ToListAsync();

                var studentSubjects = new List<StudentSubjectAttendance>();

                foreach (var studentId in listStudentId)
                {
                    foreach (var newDate in datesNeedToAdd)
                    {
                        studentSubjects.Add(
                            new StudentSubjectAttendance
                            {
                                StudentId = studentId,
                                SubjectId = updateSubjectParam.SubjectId,
                                IsPresent = false,
                                Date = newDate,
                            }
                        );
                    }
                }
                await _context.StudentSubjectAttendances.AddRangeAsync(studentSubjects);

                await _context.SaveChangesAsync();

                Console.WriteLine("SaveChanges executed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task DeleteSubject(int id)
        {
            // Net core <=6
            //var existingstudentSubjectAttendances = _context.StudentSubjectAttendances.Where(x =>
            //    x.SubjectId == id
            //);
            //_context.StudentSubjectAttendances.RemoveRange(existingstudentSubjectAttendances);

            // Net core > 7
            _context.StudentSubjectAttendances.Where(x => x.SubjectId == id).ExecuteDelete();

            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Lecture
        public async Task<Lecturer?> GetLecturer(string email, string password)
        {
            var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x =>
                x.Email == email && x.Password == password
            );

            return lecturer;
        }
        #endregion
    }
}
