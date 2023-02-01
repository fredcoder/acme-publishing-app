using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acme_publishing_app.Models
{
    public class DeliveryCompany
    {
        [Required]
        public string SubscriptionId { get; set; }

        [Required]
        public string CountryId { get; set; }

        [Required]
        public string PrintDistCompanyId { get; set; }
    }
}