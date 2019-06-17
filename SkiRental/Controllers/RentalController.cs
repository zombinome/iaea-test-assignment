using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkiRental.Domain.Entities;
using SkiRental.Domain.Services;
using SkiRental.Models;

namespace SkiRental.Controllers
{
    [ApiController]
    [Route("api/rent")]
    public class RentalController: ControllerBase
    {
        private readonly SkiRentalService service;

        public RentalController(SkiRentalService service)
        {
            this.service = service;
        }

        [HttpPost("{skiId}")]
        public async Task<JsonResult> Rent([FromRoute] int skiId, [FromBody] RentSkiModel model)
        {
            var rentedSki = await this.service.RentSkiAsync(skiId, model.CustomerName);
            return RequestResult<Ski>.Success(rentedSki);
        }

        [HttpPost("{skiId}/return")]
        public async Task<JsonResult> Return([FromRoute] int skiId)
        {
            var rentCost = await this.service.ReturnRentedSkiAsync(skiId);
            return RequestResult<SkiRentalCost>.Success(rentCost);
        }
    }
}