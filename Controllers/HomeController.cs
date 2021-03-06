﻿using Cafeteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Comment(string username, string id, string instr)
        {

            ViewBag.Message = "Your Checkout Page.";
            //to check if order successful
            OrderDetail o =new OrderDetail();
            string total = "Name: " + username + Environment.NewLine +"Id: "+ id + Environment.NewLine + "Special Instruction: "+instr;
            bool dbSuccess = false;
            try
            {
                using (var db = new AppDbContext())
                {
                        db.OrderDetails.AsEnumerable().Last().Comment = total;
                        db.SaveChanges();

                    //if all that worked then order worked:
                    dbSuccess = true;
                }

            }
            catch (Exception ex)
            { //if theres error set it to false
                dbSuccess = false;
            }
            if (dbSuccess)
            {
                return View("Checkout");
            }
            else return Json("success: false", JsonRequestBehavior.AllowGet);

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