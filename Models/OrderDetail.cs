using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{
    public class OrderDetail
    {
		[Key]
		public int Id { get; set; }
		public int OrderId { get; set; }
		public string FoodItemName { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
		public string Comment { get; set; }
	}
}