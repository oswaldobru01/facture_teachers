using System.Runtime.Serialization;

namespace facture_teachers.Models.Views.Teachers
{
    [DataContract]
    public class GetTeacher
    {
        [DataMember(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [DataMember(Name = "Identificacion")]
        public string Identification { get; set; } = null!;

        [DataMember(Name = "Tipo")]
        public string Type { get; set; } = null!;

        [DataMember(Name = "Moneda")]
        public string PaymentCurrent { get; set; } = null!;

        [DataMember(Name = "Valor_hora")]
        public decimal HourlyRate { get; set; }

        [DataMember(Name = "Id")]
        public int Id { get; set; }

        public GetTeacher() { }

        public GetTeacher(Models.DB.Teacher _teachers)
        {
            Name = _teachers.Name;
            Identification = _teachers.Identification;
            Type = _teachers.Type;
            PaymentCurrent = _teachers.PaymentCurrent;
            HourlyRate = _teachers.HourlyRate;
            Id = _teachers.Id;
        }
    }
}