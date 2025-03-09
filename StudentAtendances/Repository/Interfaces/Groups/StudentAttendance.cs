namespace StudentAtendances.Repository.Interfaces.Groups
{
    public class StudentAttendance
    {
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public List<bool> Dates { get; set; }
    }
}
