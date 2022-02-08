using System;
using System.Collections.Generic;

namespace facture_teachers.Models.DB
{
    public partial class Teacher
    {
        public Teacher()
        {
            Lessons = new HashSet<Lesson>();
        }

        public string Name { get; set; } = null!;
        public string Identification { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string PaymentCurrent { get; set; } = null!;
        public decimal HourlyRate { get; set; }
        public decimal Equivalence { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
