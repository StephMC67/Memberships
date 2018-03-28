using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    [Table ("ItemType")]     // si pas précisé, EF va créé la table "ItemTypes" avec un s
    public class ItemType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
    }
}