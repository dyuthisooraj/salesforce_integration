﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalcyonApparelsDomain.Entities
{
    public class OrderDetails
    {
        [Key]
        [DisplayName("Order Id")]
        [Required(ErrorMessage = "Order Id is required")]
        [Column(TypeName = "VARCHAR")]
        public string Parent_Order_Id__c { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = "Product Name is required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50, MinimumLength = 3)]
        public string ordered_Date__c { get; set; } = null!;

        [DisplayName("Product Type")]
        [Required(ErrorMessage = "Product Type is required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50, MinimumLength = 3)]
        public string Product_Type__c { get; set; } = null!;

        [ForeignKey("CustomerDetails")]

        [DisplayName("Customer Id")]
        [Required(ErrorMessage = "Customer Id is required")]
        [Column(TypeName = "VARCHAR")]
        public string ContactId { get; set; }

        
    }
}