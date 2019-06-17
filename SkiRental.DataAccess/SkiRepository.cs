using Microsoft.EntityFrameworkCore;
using SkiRental.Domain;
using SkiRental.Domain.Contracts;
using SkiRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkiRental.DataAccess
{
    public class SkiRepository : RepositoryBase, IRepository<int, Ski>
    {
        public SkiRepository(SkiRentalDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<bool> DeleteAsync(int skiId)
        {
            var skiToDelete = await dbContext.Skis.FindAsync(skiId);
            if (skiToDelete == null)
            {
                return false;
            }

            this.dbContext.Remove(skiToDelete);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Ski> GetAsync(int skiId)
        {
            return await this.dbContext.Skis.FindAsync(skiId);
        }

        public async Task<IReadOnlyCollection<Ski>> GetAsync(Expression<Func<Ski, bool>> filter, int skip, int? take)
        {
            IQueryable<Ski> query = this.dbContext.Skis;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (skip > 0)
            {
                query = query.Skip((int)skip);
            }

            if (take.HasValue)
            {
                query = query.Take((int)take.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<Ski> SaveAsync(Ski newSki)
        {
            Ski result = null;
            if (newSki.Id > 0)
            {
                var existingSki = await this.dbContext.Skis.FindAsync(newSki.Id);
                if (existingSki == null)
                {
                    return null;
                }

                // In scope of this task we can update only RentTime and Customer name
                existingSki.RentTime = newSki.RentTime;
                existingSki.CustomerName = newSki.CustomerName;
                await dbContext.SaveChangesAsync();

                result = existingSki;
            }
            else
            {
                var addedSki = dbContext.Skis.Add(newSki);
                await dbContext.SaveChangesAsync();

                result = addedSki.Entity;
            }

            return result;
        }
    }
}