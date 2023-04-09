namespace eLearning.Models
{
    public class SubjectGrade
    {
        public int Id { get; set; }
        public int SubjectID { get; set; }
        public int GradeID { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Grade? Grade { get; set; }
    }
}