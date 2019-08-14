using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VehicleRepairShop.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime AcceptedDate { get; set; }
        public bool IsInShop { get; set; }
        [Display(Name = "Owner")]
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        [NotMapped]
        public string FullName
        {
            get { return Make + " " + Model; }
        }
    }
}
