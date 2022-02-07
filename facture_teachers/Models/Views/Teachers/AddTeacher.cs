using System.Runtime.Serialization;

namespace facture_teachers.Models.Views.Teachers
{
    [DataContract]
    public class AddTeacher
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
    }
}