﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QANbuy.Entity
{
    public partial class GoodStatus
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(30)]
        public string state { get; set; }
    }
}