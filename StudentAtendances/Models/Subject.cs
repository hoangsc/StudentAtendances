namespace StudentAtendances.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Lecturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public List<Subject> Subjects { get; set; }

        public List<Group> Groups { get; set; }
    }

    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }

        [ForeignKey(nameof(LecturerId))]
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public List<StudentSubjectAttendance> StudentSubjectAttendances { get; set; }
    }

    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StudyCode { get; set; }
        public string Name { get; set; }

        [Required]
        public string StudentCode { get; set; }

        [ForeignKey(nameof(GroupId))]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public List<StudentSubjectAttendance> StudentSubjectAttendances { get; set; }
    }

    public class StudentSubjectAttendance
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(StudentId))]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime Date { get; set; } // Ngày điểm danh
        public bool IsPresent { get; set; } // true: có mặt, false: vắng
    }

    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string SchoolYears { get; set; }
        public string Major { get; set; }

        [ForeignKey(nameof(LecturerId))]
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public List<Student> Students { get; set; } = new();
    }
}
