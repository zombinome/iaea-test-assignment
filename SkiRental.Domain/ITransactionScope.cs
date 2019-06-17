using System;

namespace SkiRental.Domain
{
    public interface ITransactionScope: IDisposable
    {
        void Commit();

        void Rollback();
    }
}
