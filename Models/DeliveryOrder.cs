using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acme_publishing_app.Models
{
    public class DeliveryOrder
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string SubscriptionId { get; set; }

        [Required]
        public string DeliveryAddressId { get; set; }

        [Required]
        public string PrintDistCompanyId { get; set; }

        public virtual Subscription Subscription { get; set; }
        public virtual DeliveryAddress DeliveryAddress { get; set; }
        public virtual PrintDistCompany PrintDistCompany { get; set; }
    }
}