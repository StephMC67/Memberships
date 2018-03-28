using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    [Table ("Part")]     // si pas précisé, EF va créé la table "Parts" avec un s
    public class Part
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
    }
}