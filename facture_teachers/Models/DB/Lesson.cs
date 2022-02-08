using System;
using System.Collections.Generic;

namespace facture_teachers.Models.DB
{
    public partial class Lesson
    {
        public int Id { get; set; }
        public int IdTeacher { get; set; }
        public DateTime Date { get; set; }
        public string Course { get; set; } = null!;
        public int DictatedHours { get; set; }
        public decimal Value { get; set; }

        public virtual Teacher IdTeacherNavigation { get; set; } = null!;
    }
}
