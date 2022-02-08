using facture_teachers.Helpers.Interfaces;
using facture_teachers.Models.DB;
using facture_teachers.Models.Views.Nomina;
using Microsoft.EntityFrameworkCore;

namespace facture_teachers.Helpers.Repositories
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly facture_profesoresContext Context;

        public LessonsRepository(facture_profesoresContext _context)
        {
            Context = _context;
        }

        public int Add(Lesson _lesson)
        {
            Context.Lessons.Add(_lesson);
            Context.SaveChanges();
            return _lesson.Id;
        }

        public void Update(Lesson _lesson)
        {
            Context.Entry(_lesson).State = EntityState.Modified;

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }

        public void Delete(int Id)
        {
            var _lesson = Context.Lessons.Find(Id);

            if (_lesson != null)
            {
                Context.Lessons.Remove(_lesson);
                Context.SaveChanges();
            }
        }

        public List<Lesson>? GetByIdentification(int? _idTeacher = null)
        {
            var _lessons = Context.Lessons.Where(t => t.IdTeacherNavigation.Id == (_idTeacher ?? t.IdTeacherNavigation.Id));
            if (_lessons.Count() == 0)
            {
                return null;
            }
            return _lessons?.ToList();
        }

        public Lesson Get(int Id)
        {
            var finded = Context.Lessons.Find(Id);

            if (finded != null)
            {
                return finded;
            }
            else
            {
                return null;
            }
        }

        public int GetHoursInPeriodAndCourse(int _year, int _month, int _idTeacher, string _course = null)
        {
            var result = from lessons in Context.Lessons
                         where lessons.IdTeacher == _idTeacher
                            && lessons.Date.Year == _year
                            && lessons.Date.Month == _month
                            && lessons.Course == (string.IsNullOrWhiteSpace(_course) ? lessons.Course : _course)
                         select new { hour = lessons.DictatedHours };

            return result.Sum(l => l.hour);
        }

        public List<GetTeacher4Nomina> GetNominaInPeriod(int _year, int _month) 
        {
            DateTime dt = DateTime.Now;
            var lesson = from Datagroup in (from lessons in Context.Lessons
                                            where lessons.Date.Month == _month && lessons.Date.Year == _year
                                            group lessons by lessons.IdTeacher into lessonsGroup
                                            select new { idTeacher = lessonsGroup.Key, Total = lessonsGroup.ToList().Sum(l => l.Value) })
                         join teacher in Context.Teachers on Datagroup.idTeacher equals teacher.Id
                         select new { 
                             Id = teacher.Id, 
                             Name = teacher.Name, 
                             Identification = teacher.Identification, 
                             Type = teacher.Type, 
                             PaymentCurrent = teacher.PaymentCurrent, 
                             HourlyRate = teacher.HourlyRate,
                             Value = Datagroup.Total
                         };

            List<GetTeacher4Nomina> lista = lesson.Select(l => new GetTeacher4Nomina(l.Name, l.Identification, l.Type, l.PaymentCurrent, l.HourlyRate,l.Value)).ToList();

            
            return lista;
            
        }

    }
}
