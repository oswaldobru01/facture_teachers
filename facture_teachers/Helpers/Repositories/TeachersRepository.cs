using facture_teachers.Helpers.Interfaces;
using facture_teachers.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace facture_teachers.Helpers.Repositories
{
    public class TeachersRepository : ITeachersRepository
    {
        private readonly facture_profesoresContext Context;

        public TeachersRepository(facture_profesoresContext _context)
        {
            Context = _context;
        }

        public int Add(Teacher _teacher)
        {
            Context.Teachers.Add(_teacher);
            Context.SaveChanges();
            return _teacher.Id;
        }

        public void Update(Teacher _teacher)
        {
            Context.Entry(_teacher).State = EntityState.Modified;

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
            var teacher = Context.Teachers.Find(Id);

            if (teacher != null)
            {
                Context.Teachers.Remove(teacher);
                Context.SaveChanges();
            }
        }

        public Teacher? GetByIdentification(string _identification)
        {
            var teacher = Context.Teachers.Where(t => t.Identification == _identification);
            if (teacher.Count() == 0)
            {
                return null;
            }
            return teacher?.First();
        }

        public Teacher Get(int Id)
        {
            var finded = Context.Teachers.Find(Id);

            if (finded != null)
            {
                return finded;
            }
            else
            {
                return null;
            }
        }
    }
}