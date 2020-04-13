using Antlr.Runtime.Tree;
using Cafeteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cafeteria.Controllers
{
    public class DataController : Controller
    {
        // GET: returns items in Json format
        public IList<FoodItem> menuItems;// this holds the food items
        [HttpGet]
        public ActionResult GetMenuList()
        {
            //now initialize menuItems:
            menuItems = new List<FoodItem>();
            //now call the db 
            using(var db = new AppDbContext())
                //now iterate through foodItems and add to menuItems list
            {
                foreach (var f in db.FoodItems) 
                {
                    menuItems.Add(f);
                }
            }
            //finally return it as Json data
            return Json(menuItems, JsonRequestBehavior.AllowGet); //to call this in browser: localhost:port/Data/GetMenuList
        }

        // GET: returns items in Json format
        public IList<OrderDetail> orderList;// this holds the order items
        [HttpGet]
        public ActionResult GetOrderList()
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
            return Json(orderList, JsonRequestBehavior.AllowGet); //to call this in browser: localhost:port/Data/GetMenuList
        }

        [HttpGet]
        public string GetUserId() {
            //if user is signed in then session UserID will not be null. this basically checks if logged in for future functionality. To test call : localhost:port/Data/GetUserId
            // -1 = logged out ; 1 = logged in
            int uid = -1;
            if (Session["UserId"] != null)
                uid = Convert.ToInt32(Session["UserId"].ToString());
            return uid.ToString();

        }

        

        [HttpPost]
        //First get the list of food items from user and user ID
        public ActionResult PlaceOrder(IList<FoodItem> items, int id)
        {
            //to check if order successful
            bool dbSuccess = false;
            try {
                using (var db = new AppDbContext()) {
                    Order o = new Order();
                    o.UserId = id;
                    o.OrderDate = DateTime.Now;

                    db.Orders.Add(o);
                    db.SaveChanges();

                    int orderId = o.Id;
                    decimal grandTotal = 0;
                    //loop through food items and add them to order details table
                    foreach (var f in items)
                    {
                        //initialize orderDetail object
                        OrderDetail orderDetail = new OrderDetail
                        {
                            OrderId = orderId,
                            FoodItemName = f.Name,
                            Quantity = f.Quantity,
                            TotalPrice = f.Price * f.Quantity,
                        };
                        //this adds it to the order details table
                        db.OrderDetails.Add(orderDetail);
                        //keep track of what they need to pay
                        grandTotal += orderDetail.TotalPrice;
                    }
                    //update totalpaid with grandtotal (o is the object)
                    o.TotalPaid = grandTotal;
                    o.Status = 1;
                    o.OrderDate = DateTime.Now;
                    db.SaveChanges();
                    //if all that worked then order worked:
                    dbSuccess = true;
                }

            } catch (Exception ex) 
            { //if theres error set it to false
                dbSuccess = false;
            }
            if (dbSuccess)
                return Json("success: true", JsonRequestBehavior.AllowGet);
            else return Json("success: false", JsonRequestBehavior.AllowGet);

        } //would probably add GetConfirmedOrder method here to get orders from database and give to caterer list



    }
}