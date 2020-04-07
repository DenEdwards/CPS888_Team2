using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cafeteria.Models
{
    public class Caterer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string OrderList { get; set; }
        //public DateTime DatePlaced { get; set; }
        public string CompletedOrders { get; set; }
    }
}