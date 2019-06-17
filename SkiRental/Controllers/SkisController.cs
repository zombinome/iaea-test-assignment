using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkiRental.Domain.Entities;
using SkiRental.Domain.Services;
using SkiRental.Models;

namespace SkiRental.Controllers
{
    [ApiController]
    [Route("api/skis")]
    public class SkisController: ControllerBase
    {
        private readonly SkiRentalService service;

        public SkisController(SkiRentalService rentalService)
        {
            this.service = rentalService;
        }

        [HttpGet("")]
        public async Task<JsonResult> GetAllSkis()
        {
            var skis = await this.service.GetAllSkisAsync();

            return RequestResult<IReadOnlyCollection<Ski>>.Success(skis);
        }

        [HttpPost("")]
        public async Task<JsonResult> AddSki([FromBody] RegisterSkiModel model)
        {
            var ski = await this.service.RegisterSkiAsync(model.Name, model.Rate);

            return RequestResult<Ski>.Success(ski);
        }
    }
}
