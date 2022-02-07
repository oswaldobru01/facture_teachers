using facture_teachers.Models.Response;
using facture_teachers.Models.Views.Lesson;

namespace facture_teachers.Helpers.Interfaces
{
    public interface ILessonsService
    {
        public ResponseServices<int> AddLesson(AddLesson _addLesson);
        public ResponseServices<List<GetLesson>> GetListLessons();
    }
}
