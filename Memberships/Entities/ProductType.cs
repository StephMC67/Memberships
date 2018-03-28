using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    [Table ("ProductType")]     // si pas précisé, EF va créé la table "Parts" avec un s
    public class ProductType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [MaxLength(25)]
        [Required]
        public string Title { get; set; }
    }
}