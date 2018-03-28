using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    public class UserSubscription
    {

        [Require]
        [Key, Column (Order =1)]
        public int SubscriptionId { get; set; }

        [Require]
        [Key, Column(Order = 2)]
        [MaxLength(128)]
        public string UserId { get; set; }

        public DateTime? StartDate { get; set; }  // ? pour dire que cela peut être null
        public DateTime? EndDate { get; set; }  // ? pour dire que cela peut être null

    }
}