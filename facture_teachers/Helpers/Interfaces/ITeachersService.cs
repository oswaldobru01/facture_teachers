using facture_teachers.Models.Response;
using facture_teachers.Models.Views.Teachers;

namespace facture_teachers.Helpers.Interfaces
{
    public interface ITeachersService
    {
        public ResponseServices<int> AddTeacher(AddTeacher _addTeacher);
        public ResponseServices<GetTeacher> GetTeacher(string _identificacion);
    }
}
