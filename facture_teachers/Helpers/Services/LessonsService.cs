using facture_teachers.Helpers.Interfaces;
using facture_teachers.Helpers.Repositories;
using facture_teachers.Models.DB;
using facture_teachers.Models.Response;
using facture_teachers.Models.Validators;
using facture_teachers.Models.Views.Lesson;
using System.Globalization;

namespace facture_teachers.Helpers.Services
{
    public class LessonsService : ILessonsService
    {
        private int HOURS_MAXIM_4_MONTH = 160;
        private int HOURS_MAXIM_4_MONTH_COURSE = 20;
        private LessonsValidator _lessonsValidator;
        private LessonsRepository _lessonsRepository;
        private TeachersRepository _teacherRepository;
        public LessonsService(facture_profesoresContext _context)
        {
            _lessonsValidator = new LessonsValidator();
            _lessonsRepository = new LessonsRepository(_context);
            _teacherRepository = new TeachersRepository(_context);
        }

        public ResponseServices<int> AddLesson(AddLesson _addLesson) 
        {
            var teacher = _teacherRepository.GetByIdentification(_addLesson.IdentificationTeacher);
            if (teacher == null)
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"No se encontro profesor con Identificación: {_addLesson.IdentificationTeacher}.");

            var _LessonToAdd = new Lesson()
            {
                IdTeacher = teacher.Id,
                Date = _addLesson.Date,
                Course = _addLesson.Course,
                DictatedHours = _addLesson.DictatedHours,
                IdTeacherNavigation = teacher,
                Value = GetTotalValueLesson(_addLesson.DictatedHours, teacher.HourlyRate)
            };

            var validation = _lessonsValidator.Validate(_LessonToAdd);
            if (!validation.IsValid)
                return new ResponseServices<int>(_code: System.Net.HttpStatusCode.BadRequest, _errors: validation?.Errors);

            var hourDictadedInPeriod = _lessonsRepository.GetHoursInPeriodAndCourse(_LessonToAdd.Date.Year, _LessonToAdd.Date.Month, _LessonToAdd.IdTeacher, default);
            if (hourDictadedInPeriod + _LessonToAdd.DictatedHours > HOURS_MAXIM_4_MONTH)
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"EL total de horas del mes:  {_LessonToAdd.Date.ToString("Y", CultureInfo.GetCultureInfo("es-ES"))}, no puede ser mayor a: {HOURS_MAXIM_4_MONTH}, horas registradas: {hourDictadedInPeriod}, horas ingresadas: {_LessonToAdd.DictatedHours}.");

            var hourDictadedInPeriodAndCourse = _lessonsRepository.GetHoursInPeriodAndCourse(_LessonToAdd.Date.Year, _LessonToAdd.Date.Month, _LessonToAdd.IdTeacher, _LessonToAdd.Course);
            if (hourDictadedInPeriodAndCourse + _LessonToAdd.DictatedHours > HOURS_MAXIM_4_MONTH_COURSE)
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"EL total de horas del mes: {_LessonToAdd.Date.ToString("Y", CultureInfo.GetCultureInfo("es-ES"))} y del curso: {_LessonToAdd.Course}, no puede ser mayor a: {HOURS_MAXIM_4_MONTH_COURSE}, horas registradas: {hourDictadedInPeriodAndCourse}, horas ingresadas: {_LessonToAdd.DictatedHours}.");

            var IdAdded = _lessonsRepository.Add(_LessonToAdd);
            return new ResponseServices<int>(System.Net.HttpStatusCode.OK, "Ok", IdAdded, 1);
        }

        private decimal GetTotalValueLesson(int _LessonHour,decimal _hourlyRate) => _hourlyRate * _LessonHour;
        public ResponseServices<List<GetLesson>> GetListLessons() 
        {
            var response = _lessonsRepository.GetByIdentification();
            return new ResponseServices<List<GetLesson>>(System.Net.HttpStatusCode.OK, "Ok", response.Select(l => new GetLesson(l, _teacherRepository.Get(l.IdTeacher))).ToList(), response.Count()); 
        }

    }
}
