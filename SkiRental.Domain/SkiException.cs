using System;

namespace SkiRental.Domain
{
    public class SkiException: Exception
    {
        public SkiException(int skiId, string message = null, Exception innerException = null)
            : base (message, innerException)
        {
            this.SkiId = skiId;
        }

        public int SkiId { get; set; }
    }
}
