using FluentValidation;

namespace facture_teachers.Models.Validators
{
    public class TeachersValidator : AbstractValidator<Models.DB.Teacher>
    {
        public TeachersValidator()
        {
            RuleFor(t => t.Name).NotNull().NotEmpty().WithMessage("El campo nombre (name) es obligatorio.");
            RuleFor(t => t.Identification).NotNull().NotEmpty().WithMessage("El campo identificacion (identification) es obligatorio.");
            RuleFor(t => t.Type).NotNull().NotEmpty().WithMessage("El campo tipo de profesor (type) es obligatorio.");
            RuleFor(t => t.HourlyRate).NotNull().NotEmpty().WithMessage("El campo horas de pago (paymentCurrent) es obligatorio.");
            RuleFor(t => t.PaymentCurrent).NotNull().NotEmpty().WithMessage("El campo moneda de pago (hourlyRate) es obligatorio.");
            RuleFor(t => t.HourlyRate).GreaterThan(0).WithMessage("El valor del pago debe ser mayor a 0.");
        }
    }
}