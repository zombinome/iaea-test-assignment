using Microsoft.EntityFrameworkCore.Storage;
using SkiRental.Domain;
using System;

namespace SkiRental.DataAccess
{
    internal class TransactionScope : ITransactionScope
    {
        private readonly SkiRentalDbContext dbContext;
        private readonly IDbContextTransaction transaction;
        private readonly bool commitTransactionOnDispose;

        private bool commitRequested;
        private bool closed;

        public TransactionScope(SkiRentalDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.commitTransactionOnDispose = commitTransactionOnDispose;
            this.transaction = dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (closed)
            {
                throw new Exception("Transaction was already resolved");
            }

            this.transaction.Commit();
            this.closed = true;
        }

        public void Rollback()
        {
            if (closed)
            {
                throw new Exception("Transaction was already resolved");
            }

            this.transaction.Rollback();
            this.closed = true;
        }

        public void Dispose()
        {
            if (!this.closed)
            {
                this.Rollback();
            }

            this.transaction.Dispose();
        }
    }
}
