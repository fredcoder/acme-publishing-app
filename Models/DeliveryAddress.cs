using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acme_publishing_app.Models
{
    public class DeliveryAddress
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string CountryId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Country Country { get; set; }
    }
}