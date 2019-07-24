using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetCustomers()
        {
            return Ok(_context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>));
        }

        public IHttpActionResult GetCustomerById(int id)
        {
            var customer = _context.Customers.SingleOrDefault(e => e.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return Created(new Uri(Request.RequestUri+"/"+customer.Id) ,customer);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id,CustomerDto customerDto)
        {
            var customerInDB = _context.Customers.SingleOrDefault(e => e.Id == id);
            if (customerInDB == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDB);
            
            //Or

            //customerInDB.Name = customerDto.Name;
            //customerInDB.Birthdate = customerDto.Birthdate;
            //customerInDB.IsSubscribedToNewsLetter = customerDto.IsSubscribedToNewsLetter;
            //customerDto.MembershipTypeId = customerDto.MembershipTypeId;
            _context.SaveChanges();
            return Ok(customerInDB);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var cust = _context.Customers.SingleOrDefault(e => e.Id == id);
            if (cust ==null)
                return BadRequest();
            
            _context.Customers.Remove(cust);
            _context.SaveChanges();
            return Ok(cust);
        }
    }
}
