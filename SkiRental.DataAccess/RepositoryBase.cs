using SkiRental.Domain;

namespace SkiRental.DataAccess
{
    public abstract class RepositoryBase
    {
        protected readonly SkiRentalDbContext dbContext;

        protected ITransactionScope operationScope;

        protected RepositoryBase(SkiRentalDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ITransactionScope BeginScope()
        {
            if (this.operationScope == null)
            {
                this.operationScope = new TransactionScope(this.dbContext);
            }

            return this.operationScope;
        }
    }
}
