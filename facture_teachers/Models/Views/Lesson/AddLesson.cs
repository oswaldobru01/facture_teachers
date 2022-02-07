namespace facture_teachers.Models.Views.Lesson
{
    public class AddLesson
    {
        public string IdentificationTeacher { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Course { get; set; } = null!;
        public int DictatedHours { get; set; }
    }
}
