#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using facture_teachers.Helpers.Interfaces;
using facture_teachers.Models.Views.Teachers;
using System.Net;
using facture_teachers.Models.DB;
using facture_teachers.Helpers.Services;

namespace facture_teachers.Controllers.Teachers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeachersService _teachersServices;
        private readonly ExchangeRateService _exchangeRatesService;
        private readonly IConfiguration _configuration;

        public TeachersController(facture_profesoresContext context, IConfiguration iConfig)
        {
            _configuration = iConfig;
            _exchangeRatesService = new ExchangeRateService(_configuration);
            _teachersServices = new TeachersService(context, _exchangeRatesService);
        }

        [HttpPost]
        [Route("SaveTeacher")]
        public ActionResult PostTeacher([FromBody] AddTeacher teacher)
        {
            var response = _teachersServices.AddTeacher(teacher);
            if (response.Code == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetExchangeRates/{code}")]
        public async Task<ActionResult<dynamic>> GetExchangeRates(string code)
        {
            return await _exchangeRatesService.FindExchangeByCode(code);
        }

        [HttpDelete]
        [Route("DeleteTeacher/{id}")]
        public ActionResult DeleteTeacher(int id)
        {
            var response = _teachersServices.DeleteTeacher(id); ;
            if (response.Code == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetTeacher/{identification}")]
        public ActionResult GetTeacher(string identification)
        {
            var response = _teachersServices.GetTeacher(identification);
            if (response.Code == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateTeacher")]
        public ActionResult PutTeacher([FromBody] AddTeacher teacher)
        {
            var response = _teachersServices.UpdateTeacher(teacher);
            if (response.Code == (int)HttpStatusCode.InternalServerError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}