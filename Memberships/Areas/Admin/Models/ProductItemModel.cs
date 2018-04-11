using Memberships.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    public class ProductItemModel
    { 
        [Displayname ("Product Id")]
        public int ProductId { get; set; }

        [Displayname("Item Id")]
        public int ItemId { get; set; }


        [Displayname("Product Title")]
        
        public string ProductTitle { get; set; }

        [Displayname("Item Tittle")]
        public string ItemTitle { get; set; }

        public ICollection<Product> Products {get;set;}
        public ICollection<Item> Items { get; set; }


    }
}