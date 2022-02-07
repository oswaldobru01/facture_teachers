using facture_teachers.Models.DB;

namespace facture_teachers.Helpers.Interfaces
{
    public interface ITeachersRepository
    {
        public int Add(Models.DB.Teacher _teacher);

        public void Update(Models.DB.Teacher _teacher);

        public void Delete(int Id);

        public Teacher Get(int Id);

        public Teacher? GetByIdentification(string _identification);
    }
}