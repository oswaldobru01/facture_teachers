using facture_teachers.Models.Views.Teachers;

namespace facture_teachers.Models.Views.Lesson
{
    public class GetLesson
    {
        public int Id { get; set; }
        public string IdentificationTeacher { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Course { get; set; } = null!;
        public int DictatedHours { get; set; }

        public decimal Value { get; set; }

        public GetTeacher? Teacher { get; set; }

        public GetLesson(Models.DB.Lesson lesson, Models.DB.Teacher teacher)
        {
            Id = lesson.Id;
            IdentificationTeacher = teacher.Identification;
            Date = lesson.Date;
            Course = lesson.Course;
            DictatedHours = lesson.DictatedHours;
            Value = lesson.Value;
            Teacher = new GetTeacher(teacher);
        }

       
    }

}
