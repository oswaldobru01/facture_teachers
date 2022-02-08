using facture_teachers.Models.DB;
using facture_teachers.Models.Views.Nomina;

namespace facture_teachers.Helpers.Interfaces
{
    public interface ILessonsRepository
    {
        public int Add(Lesson _lesson);
        public void Update(Lesson _lesson);
        public void Delete(int Id);
        public List<Lesson>? GetByIdentification(int? _idTeacher = null);
        public Lesson Get(int Id);
        public int GetHoursInPeriodAndCourse(int _year, int _month, int _idTeacher, string _course = null);
        public List<GetTeacher4Nomina> GetNominaInPeriod(int _year, int _month);

    }
}
