using Cafeteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafeteria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   //creates the database using the declarations in the AppDbContext file. Comment them out when not in use
            AppDbContext c = new AppDbContext();
            c.Database.CreateIfNotExists();
            return View();
        }

        public ActionResult CatererIndex()
        {   //creates the database using the declarations in the AppDbContext file. Comment them out when not in use
            AppDbContext c = new AppDbContext();
            c.Database.CreateIfNotExists();
            return View();
        }

        public ActionResult UserIndex()
        {   //creates the database using the declarations in the AppDbContext file. Comment them out when not in use
            AppDbContext c = new AppDbContext();
            c.Database.CreateIfNotExists();
            return View();
        }

        // GET: returns items in Json format
        public IList<OrderDetail> orderList;// this holds the order items
        [HttpGet]
        public ActionResult Orders()
        {
            //now initialize menuItems:
            orderList = new List<OrderDetail>();
            //now call the db 
            using (var db = new AppDbContext())
            //now iterate through foodItems and add to menuItems list
            {
                foreach (var f in db.OrderDetails)
                {
                    orderList.Add(f);
                }
            }
            //finally return it as Json data
            return View(orderList); //to call this in browser: localhost:port/Data/GetMenuList
        }

  

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}