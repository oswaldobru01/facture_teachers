using facture_teachers.Helpers.Interfaces;
using facture_teachers.Models.DB;
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
    }
}
