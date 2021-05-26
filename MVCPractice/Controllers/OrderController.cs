using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPractice.Data;
using MVCPractice.Models;
using MVCPractice.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCPractice.Controllers
{   
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public OrderDetailsVM OrderDetailsVM { get; set; }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            IEnumerable<Order> objList = _db.Order.Include(u=>u.ApplicationUser);
            return View(objList);
        }
        public IActionResult Create()
        {
            OrderVM orderVM = new OrderVM()
            {
                OrderDetails = new OrderDetails(),
                ProductSelectList = _db.Product.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(orderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderVM orderVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    DateTime = DateTime.Now,
                    ApplicationUserId = claim.Value,
                    Memo = orderVM.Order.Memo

                };

                _db.Order.Add(order);
                _db.SaveChanges();
                var prodId = orderVM.OrderDetails.ProductId;
                var prod = _db.Product.FirstOrDefault(u => u.Id == prodId);
                var prodPrice = prod.Price;
                OrderDetails orderDetails = new OrderDetails()
                {
                    OrderId = order.Id,
                    ProductId = orderVM.OrderDetails.ProductId,
                    Quantity = orderVM.OrderDetails.Quantity,
                    Total = prodPrice * orderVM.OrderDetails.Quantity
                };
                _db.OrderDetails.Add(orderDetails);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            orderVM.ProductSelectList = _db.Product.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(orderVM);
        }

        public IActionResult Details(int? id)
        {
            OrderDetailsVM = new OrderDetailsVM()
            {
                Order = _db.Order.Include(u => u.ApplicationUser).FirstOrDefault(u => u.Id == id),
                OrderDetails = _db.OrderDetails.Include(u => u.Product).FirstOrDefault(u => u.Id == id),
            };
            return View(OrderDetailsVM);
        }
        public IActionResult Delete(int? id)
        {
            OrderDetailsVM = new OrderDetailsVM()
            {

                Order = _db.Order.Include(u => u.ApplicationUser).FirstOrDefault(u => u.Id == id),
                OrderDetails = _db.OrderDetails.Include(u => u.Product).FirstOrDefault(u => u.Id == id),
            };
            return View(OrderDetailsVM);
        }
        [HttpPost]
        
        public IActionResult DeletePost()
        {
            Order order = _db.Order.FirstOrDefault(u => u.Id == OrderDetailsVM.Order.Id);
            OrderDetails orderDetails = _db.OrderDetails.FirstOrDefault(u => u.OrderId == OrderDetailsVM.Order.Id);
            _db.Order.Remove(order);
            _db.OrderDetails.Remove(orderDetails);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}   
