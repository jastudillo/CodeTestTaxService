using System;
using System.Collections.Generic;
using System.Text;

namespace Tax.Models.Models
{
    public class Order
    {
        /// <summary>
        /// Gets or Sets the FromCountry.
        /// </summary>
        public string FromCountry { get; set; }

        /// <summary>
        /// Gets or Sets the FromZip.
        /// </summary>
        public string FromZip { get; set; }

        /// <summary>
        /// Gets or Sets the FromCity.
        /// </summary>
        public string FromCity { get; set; }

        /// <summary>
        /// Gets or Sets the FromStreet.
        /// </summary>
        public string FromStreet { get; set; }

        /// <summary>
        /// Gets or Sets the FromStreet.
        /// </summary>
        public string FromState { get; set; }

        /// <summary>
        /// Gets or Sets the ToCountry.
        /// </summary>
        public string ToCountry { get; set; }

        /// <summary>
        /// Gets or Sets the ToZip.
        /// </summary>
        public string ToZip { get; set; }

        /// <summary>
        /// Gets or Sets the ToState.
        /// </summary>
        public string ToState { get; set; }

        /// <summary>
        /// Gets or Sets the ToCity.
        /// </summary>
        public string ToCity { get; set; }

        /// <summary>
        /// Gets or Sets the ToStreet.
        /// </summary>
        public string ToStreet { get; set; }

        /// <summary>
        /// Gets or Sets the Amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or Sets the Shipping.
        /// </summary>
        public decimal Shipping { get; set; }

        /// <summary>
        /// Gets or Sets the CustomerId.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or Sets the ExemptionType.
        /// </summary>
        public string ExemptionType { get; set; }

    }
}
