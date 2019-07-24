using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        //public int Id { get; set; }
        [Required(ErrorMessage = "Place Enter Customer's Name.")]
        [StringLength(250)]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }

        public byte MembershipTypeId { get; set; }
    }
}