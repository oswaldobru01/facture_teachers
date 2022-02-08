namespace facture_teachers.Models.Views.Nomina
{
    public class GetNomina
    {

        public string Period { get; set; } = null!;

        public decimal Total { get; set; } 
        
        public List<GetTeacher4Nomina> Teachers { get; set; }
    }
    public class GetTeacher4Nomina
    {
        public GetTeacher4Nomina(string name, string identification, string type, string paymentCurrent, decimal hourlyRate, decimal value)
        {
            Name = name;
            Identification = identification;
            Type = type;
            PaymentCurrent = paymentCurrent;
            HourlyRate = hourlyRate;
            Value = value;
        }

        public string Name { get; set; } = null!;

        public string Identification { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string PaymentCurrent { get; set; } = null!;

        public decimal HourlyRate { get; set; }

        public decimal Value { get; set; }
    }

    public class GetNominaRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }

    }

}
