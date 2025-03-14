namespace StudentAtendances.Models.DTO
{
    public class UpdateSubjectParam
    {
        public int SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public List<DateTime> Dates { get; set; }
    }
}
