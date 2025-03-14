using Microsoft.EntityFrameworkCore;
using StudentAtendances.Models;
using Group = StudentAtendances.Models.Group;

public class AppDbContext : DbContext
{
    private static DateTime FixedDate = new DateTime(2024, 1, 1);

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        //Database.Migrate();
    }

    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<StudentSubjectAttendance> StudentSubjectAttendances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigurationRelationships(modelBuilder);
        SeedData(modelBuilder);
    }

    private static void ConfigurationRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Student>()
            .HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Subject>()
            .HasOne(s => s.Lecturer)
            .WithMany(l => l.Subjects)
            .HasForeignKey(s => s.LecturerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<StudentSubjectAttendance>()
            .HasKey(ssa => new
            {
                ssa.StudentId,
                ssa.SubjectId,
                ssa.Id,
            });

        modelBuilder
            .Entity<StudentSubjectAttendance>()
            .HasOne(ssa => ssa.Student)
            .WithMany(ssa => ssa.StudentSubjectAttendances)
            .HasForeignKey(ssa => ssa.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<StudentSubjectAttendance>()
            .HasOne(ssa => ssa.Subject)
            .WithMany(ssa => ssa.StudentSubjectAttendances)
            .HasForeignKey(ssa => ssa.SubjectId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
            .Entity<Group>()
            .HasOne(g => g.Lecturer)
            .WithMany(l => l.Groups)
            .HasForeignKey(g => g.LecturerId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var lecturers = new List<Lecturer>
        {
            new()
            {
                Id = 1,
                Name = "Le Van Hieu",
                Email = "Hieu@ictu.com",
                Password = "123",
            },
            new()
            {
                Id = 2,
                Name = "Tran Van Nam",
                Email = "Nam@ictu.com",
                Password = "123",
            },
            new()
            {
                Id = 3,
                Name = "Duc Canh",
                Email = "Canh@ictu.com",
                Password = "123",
            },
        };
        modelBuilder.Entity<Lecturer>().HasData(lecturers);

        var groups = new List<Group>
        {
            new()
            {
                Id = 1,
                GroupName = "CNTT",
                SchoolYears = "2023-2024",
                Major = "IT",
                LecturerId = 1,
            },
            new()
            {
                Id = 2,
                GroupName = "QTKD",
                SchoolYears = "2023-2024",
                Major = "Business",
                LecturerId = 2,
            },
        };
        modelBuilder.Entity<Group>().HasData(groups);

        var students = new List<Student>();
        for (int i = 1; i <= 10; i++)
        {
            students.Add(
                new Student
                {
                    Id = i,
                    Name = $"Student {i}",
                    StudentCode = $"SV{i:000}",
                    StudyCode = $"SC{i:000}",
                    GroupId = (i <= 5) ? 1 : 2,
                }
            );
        }
        modelBuilder.Entity<Student>().HasData(students);

        var subjects = new List<Subject>
        {
            new()
            {
                Id = 1,
                SubjectCode = "IT101",
                SubjectName = "Lập trình C#",
                LecturerId = 1,
            },
            new()
            {
                Id = 2,
                SubjectCode = "IT102",
                SubjectName = "Cấu trúc dữ liệu",
                LecturerId = 1,
            },
            new()
            {
                Id = 3,
                SubjectCode = "IT103",
                SubjectName = "SQL Server",
                LecturerId = 1,
            },
            new()
            {
                Id = 4,
                SubjectCode = "BUS201",
                SubjectName = "Marketing",
                LecturerId = 2,
            },
            new()
            {
                Id = 5,
                SubjectCode = "BUS202",
                SubjectName = "Quản lý tài chính",
                LecturerId = 2,
            },
            new()
            {
                Id = 6,
                SubjectCode = "BUS203",
                SubjectName = "Kinh tế vi mô",
                LecturerId = 2,
            },
        };
        modelBuilder.Entity<Subject>().HasData(subjects);

        var attendances = new List<StudentSubjectAttendance>();
        int studentSubjectAttendanceId = 1;
        for (int s = 1; s <= 6; s++)
        {
            for (int i = 1; i <= 6; i++)
            {
                var startDate = FixedDate;
                for (int k = 1; k <= 3; k++)
                {
                    attendances.Add(
                        new StudentSubjectAttendance
                        {
                            Id = studentSubjectAttendanceId,
                            StudentId = i,
                            SubjectId = s,
                            Date = startDate,
                            IsPresent = (i % 2 == 0),
                        }
                    );
                    studentSubjectAttendanceId++;
                    startDate = startDate.AddDays(1);
                }
            }
        }
        modelBuilder.Entity<StudentSubjectAttendance>().HasData(attendances);
    }
}
