using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace acme_publishing_app.Models
{
    public class Subscription
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}