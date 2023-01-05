﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HalcyonApparelsDomain.Entities
{
    public class AccessoryDetails
    {
        [Key]
        [DisplayName("Id")]
        //[Required(ErrorMessage = "Accessory Id is required")]
        [Column(TypeName = "INT")]
        public int AccessoryId { get; set; } =0;

        [DisplayName("Title")]
        //[Required(ErrorMessage = "Accessory Name is required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50, MinimumLength = 3)]
        public string AccessoryName { get; set; } = null!;

        [DisplayName("Type")]
        //[Required(ErrorMessage = "Accessory Type is required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50, MinimumLength = 3)]
        public string AccessoryType { get; set; } = null!;

        [DisplayName("Brand")]
        //[Required(ErrorMessage = "Accessory Brand is required")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50, MinimumLength = 3)]
        public string AccessoryBrand { get; set; } = null!;

        [DisplayName("Price")]
        //[Required(ErrorMessage = "Accessory Price is required")]
        [Column(TypeName = "INT")]
        public int AccessoryPrice { get; set; }

        [DisplayName("Discount")]
        [Column(TypeName = "INT")]
        public int AccessoryDiscount { get; set; }

        public string ImageUrl { get; set; }


        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
