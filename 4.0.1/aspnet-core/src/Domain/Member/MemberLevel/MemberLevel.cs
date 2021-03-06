﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
namespace UnionMall.Entity
{
    [Table("TMemberLevel")]
    public class MemberLevel : Entity<long>
    {
        public int? TenantId { get; set; }

        [Required(ErrorMessage = "title can not be null")]

        public string Title { get; set; }

        public decimal InitPoint { get; set; }

        public decimal SellPrice { get; set; }

        public decimal MinPoint { get; set; }

        public decimal MaxPoint { get; set; }

        public decimal Profit { get; set; } = 0;
    }
}
