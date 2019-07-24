using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            //Include() => Eager Loading => To load object 'MembershipType' with Customers
           // var customer = _context.Customers.Include(c => c.MembershipType).ToList();
            return View();//customer
        }

        public ActionResult Details(int id)
        {
            var cust = _context.Customers.Include(c => c.MembershipType).FirstOrDefault(c => c.Id == id);
            if (cust == null) return HttpNotFound();
            return View(cust);
        }

        public ActionResult New()
        {
            var membershipType = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipType
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (ModelState.IsValid)
            {


                if (customer.Id == 0)
                {
                    _context.Customers.Add(customer);
                }
                else
                {
                    var oldCustomer = _context.Customers.Single(e => e.Id == customer.Id);
                    if (oldCustomer != null)
                    {
                        // Mapper.Map(customer,oldCustomer);
                        oldCustomer.Name = customer.Name;
                        oldCustomer.Birthdate = customer.Birthdate;
                        oldCustomer.MembershipType = customer.MembershipType;
                        oldCustomer.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
        }

         

        //public IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer() {Id=1, Name = "Ramy Atef"},
        //        new Customer() {Id=2, Name = "Mena Hany"}
        //    };
        //}
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}