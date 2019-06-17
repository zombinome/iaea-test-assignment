using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkiRental.Domain.Contracts;
using SkiRental.Domain.Entities;

namespace SkiRental.Domain.Services
{
    public class SkiRentalService
    {
        private IRepository<int, Ski> repository;

        public SkiRentalService(IRepository<int, Ski> repository)
        {
            this.repository = repository;
        }

        public async Task<Ski> RegisterSkiAsync(string name, double rate)
        {
            var ski = new Ski { Name = name, Rate = rate };

            using (var transactionScope = repository.BeginScope())
            {
                var savedSki = await this.repository.SaveAsync(ski);

                transactionScope.Commit();

                return savedSki;
            }
        }

        public async Task<Ski> RentSkiAsync(int skiId, string customerName)
        {
            using (var transactionScope = this.repository.BeginScope())
            {
                var ski = await this.repository.GetAsync(skiId);
                if (ski == null)
                {
                    throw new SkiException(skiId, "Can\'t find ski with requested id: " + skiId);
                }

                if (ski.RentTime.HasValue)
                {
                    throw new SkiException(skiId, "Ski already rented");
                }

                ski.RentTime = DateTime.Now;
                ski.CustomerName = customerName;

                await this.repository.SaveAsync(ski);

                transactionScope.Commit();

                return ski;
            }
        }

        public async Task<SkiRentalCost> ReturnRentedSkiAsync(int skiId)
        {
            using (var transactionScope = this.repository.BeginScope())
            {
                var ski = await this.repository.GetAsync(skiId);
                if (ski == null)
                {
                    throw new SkiException(skiId, "Ski with requested id not found in database");
                }

                if (!ski.RentTime.HasValue)
                {
                    throw new SkiException(skiId, "Ski with provided id is not rented");
                }

                var hours = (DateTime.Now - ski.RentTime.Value).TotalHours;
                var rentalCost = new SkiRentalCost
                {
                    SkiId = skiId,
                    CustomerName = ski.CustomerName,
                    RentCost = ski.Rate * hours
                };

                ski.RentTime = null;
                ski.CustomerName = null;
                await repository.SaveAsync(ski);

                transactionScope.Commit();
                return rentalCost;
            }
        }

        public Task<IReadOnlyCollection<Ski>> GetAllSkisAsync()
        {
            return this.repository.GetAsync();
        }
    }
}
