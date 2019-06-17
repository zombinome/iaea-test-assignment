using System;

namespace SkiRental.Domain.Entities
{
    public class Ski
    {
        /// <summary>
        /// Gets or sets entity ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets ski name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Ski rental rate (per hour)
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Gets or sets time when ski was rented. If ski is not rented, this property is null
        /// </summary>
        public DateTime? RentTime { get; set; }

        /// <summary>
        /// Gets or sent name of customer rented this ski. If ski is not rented, this property is null
        /// </summary>
        public string CustomerName { get; set; }
    }
}
