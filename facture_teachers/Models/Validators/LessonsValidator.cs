using FluentValidation;

namespace facture_teachers.Models.Validators
{
    public class LessonsValidator : AbstractValidator<Models.DB.Lesson>
    {
        public LessonsValidator()
        {
            RuleFor(l => l.Course).NotNull().NotEmpty().WithMessage("El campo curso (course) es obligatorio.");
            RuleFor(l => l.DictatedHours).NotNull().NotEmpty().WithMessage("El campo horas dictadas (dictatedHours) es obligatorio.");
            RuleFor(l => l.DictatedHours).GreaterThan(0).WithMessage("Las horas dictadas deben ser mayor a 0.");
            RuleFor(l => l.Date).NotNull().NotEmpty().WithMessage("El campo fecha (date) es obligatorio.");
            RuleFor(l => l.IdTeacher).NotNull().NotEmpty().WithMessage("El campo identificacion profesor (idTeacher) es obligatorio.");

        }
    }
}
