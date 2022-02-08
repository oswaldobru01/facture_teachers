using facture_teachers.Helpers.Interfaces;
using facture_teachers.Helpers.Repositories;
using facture_teachers.Models.DB;
using facture_teachers.Models.Response;
using facture_teachers.Models.Validators;
using facture_teachers.Models.Views.Teachers;

namespace facture_teachers.Helpers.Services
{
    public class TeachersService : ITeachersService
    {
        private TeachersValidator _teachersValidator;
        private TeachersRepository _teacherRepository;
        private ExchangeRateService _exchangeRateService;

        private string PLANT_CODE = "PLANTA";
        private string COLOMBIAN_PAYMENT = "COP";
        private string ERROR_CODE_API_EXCHANGE = "error";
        private string[] TeacherValidsTypes = new[] { "PLANTA", "EXTRANJERO" };

        public TeachersService(facture_profesoresContext _context, ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
            _teachersValidator = new TeachersValidator();
            _teacherRepository = new TeachersRepository(_context);
        }

        public ResponseServices<int> AddTeacher(AddTeacher _addTeacher)
        {
            var addTeachersEntity = new Teacher()
            {
                Name = _addTeacher.Name.ToUpper(),
                Identification = _addTeacher.Identification,
                Type = _addTeacher.Type.ToUpper(),
                PaymentCurrent = _addTeacher.PaymentCurrent.ToUpper(),
                HourlyRate = _addTeacher.HourlyRate

            };

            /*Validate model*/
            var validation = _teachersValidator.Validate(addTeachersEntity);

            if (!validation.IsValid)
                return new ResponseServices<int>(_code: System.Net.HttpStatusCode.BadRequest, _errors: validation?.Errors);

            /*Validate Data*/
            var finded = _teacherRepository.GetByIdentification(addTeachersEntity.Identification);
            if (finded != null)
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"Ya existe un profesor con la cedula: {addTeachersEntity.Identification}.");

            if (!TeacherValidsTypes.Contains(addTeachersEntity.Type.ToUpper()))
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"Solo es valido profesores de PLANTA o EXTRANJERO.");

            if (addTeachersEntity.Type.Equals(PLANT_CODE) && addTeachersEntity.PaymentCurrent != COLOMBIAN_PAYMENT)
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"Los profesores de planta solo pueden ser pagos con pesos colombianos(COP).");

            var paymentCUrrencyValid = _exchangeRateService.FindExchangeByCode(addTeachersEntity.PaymentCurrent);
            if (paymentCUrrencyValid.Result.result == ERROR_CODE_API_EXCHANGE)
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"El código de moneda no existe.");

            addTeachersEntity.Equivalence = paymentCUrrencyValid.Result.conversion_rates.COP;

            //if (addTeachersEntity.PaymentCurrent != COLOMBIAN_PAYMENT)
            //    addTeachersEntity.HourlyRate = SetHourlyRate(addTeachersEntity.HourlyRate, paymentCUrrencyValid.Result.conversion_rates?.COP ?? 1);



            /*Save*/
            var IdAdded = _teacherRepository.Add(addTeachersEntity);
            return new ResponseServices<int>(System.Net.HttpStatusCode.OK, "Ok", IdAdded, 1);
        }

       // private decimal SetHourlyRate(decimal _hourlyRate, decimal _equivalence) => _hourlyRate * _equivalence;

        public ResponseServices<bool> DeleteTeacher(int _id)
        {
            try
            {
                var teacher = _teacherRepository.Get(_id);
                if (teacher == null)
                    return new ResponseServices<bool>(System.Net.HttpStatusCode.BadRequest, $"No se encontro profesor con ID: {_id}.");

                _teacherRepository.Delete(_id);
                return new ResponseServices<bool>(System.Net.HttpStatusCode.OK, $"El profesor {teacher.Name} - {teacher.Identification}, se ha eliminado exitosamente.", true);
            }
            catch (Exception ex)
            {
                return new ResponseServices<bool>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public ResponseServices<GetTeacher> GetTeacher(string _identificacion)
        {
            try
            {
                var teacher = _teacherRepository.GetByIdentification(_identificacion);
                if (teacher == null)
                    return new ResponseServices<GetTeacher>(System.Net.HttpStatusCode.BadRequest, $"No se encontro profesor con identificación: {_identificacion}.");

                return new ResponseServices<GetTeacher>(System.Net.HttpStatusCode.OK, $"OK", new GetTeacher(teacher), 1);
            }
            catch (Exception ex)
            {
                return new ResponseServices<GetTeacher>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public ResponseServices<int> UpdateTeacher(AddTeacher _addTeacher)
        {
            var finded = _teacherRepository.Get(_addTeacher.Id);

            if (finded == null)
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"No existe un profesor con los datos ingresados.");

            finded.Name = _addTeacher.Name.ToUpper();
            finded.Identification = _addTeacher.Identification;
            finded.Type = _addTeacher.Type.ToUpper();
            finded.PaymentCurrent = _addTeacher.PaymentCurrent.ToUpper();
            finded.HourlyRate = _addTeacher.HourlyRate;
            finded.Id = _addTeacher.Id;

            /*Validate model*/
            var validation = _teachersValidator.Validate(finded);

            if (!validation.IsValid)
                return new ResponseServices<int>(_code: System.Net.HttpStatusCode.BadRequest, _errors: validation?.Errors);

            /*Validate Data*/

            if (!TeacherValidsTypes.Contains(finded.Type.ToUpper()))
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"Solo es valido profesores de PLANTA o EXTRANJERO.");

            if (finded.Type.Equals(PLANT_CODE) && finded.PaymentCurrent != COLOMBIAN_PAYMENT)
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"Los profesores de planta solo pueden ser pagos con pesos colombianos(COP).");

            var paymentCUrrencyValid = _exchangeRateService.FindExchangeByCode(finded.PaymentCurrent);
            if (paymentCUrrencyValid.Result.result == ERROR_CODE_API_EXCHANGE)
                return new ResponseServices<int>(System.Net.HttpStatusCode.BadRequest, $"El código de moneda no existe.");

            /*Save*/
            _teacherRepository.Update(finded);
            return new ResponseServices<int>(System.Net.HttpStatusCode.OK, "Ok", finded.Id, 1);
        }
    }
}