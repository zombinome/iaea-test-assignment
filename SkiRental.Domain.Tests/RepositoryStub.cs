using Moq;
using SkiRental.Domain.Contracts;
using SkiRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkiRental.Domain.Tests
{
    internal class RepositoryStub : IRepository<int, Ski>
    {
        public readonly Mock<ITransactionScope> TransactionMock = new Mock<ITransactionScope>();

        public readonly List<Ski> Items = new List<Ski>();

        public ITransactionScope BeginScope()
        {
            return this.TransactionMock.Object;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var item = this.Items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                this.Items.Remove(item);
            }
            return Task.FromResult(item != null);
        }

        public Task<Ski> GetAsync(int id)
        {
            var item = this.Items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);
        }

        public Task<IReadOnlyCollection<Ski>> GetAsync(Expression<Func<Ski, bool>> filter = null, int skip = 0, int? take = null)
        {
            IEnumerable<Ski> query = this.Items;
            if (filter != null)
            {
                query = query.Where(filter.Compile());
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take != null)
            {
                query = query.Take(take.Value);
            }

            return Task.FromResult<IReadOnlyCollection<Ski>>(query.ToList());
        }

        public Task<Ski> SaveAsync(Ski newSki)
        {
            Ski result = null;
            if (newSki.Id > 0)
            {
                var existingSki = this.Items.Find(x => x.Id == newSki.Id);
                if (existingSki == null)
                {
                    return null;
                }

                // In scope of this task we can update only RentTime and Customer name
                existingSki.RentTime = newSki.RentTime;
                existingSki.CustomerName = newSki.CustomerName;

                result = existingSki;
            }
            else
            {
                this.Items.Add(newSki);
                result = newSki;
            }

            return Task.FromResult(result);
        }
    }
}
