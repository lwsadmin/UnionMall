using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
namespace UnionMall.Entity
{
    [Table("TMemberLevel")]
    public class MemberLevel : Entity<long>
    {
        public int? TenantId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "title can not be null")]
=======

namespace UnionMall.Member.MemberLevel
{
    public class MemberLevel : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required(ErrorMessage = "title can not be null")]
>>>>>>> 9c9df8549ffd1b56db3183e0c18e537bafd0bf4c
        public string Title { get; set; }

        public decimal InitPoint { get; set; }

        public decimal SellPrice { get; set; }

        public decimal MinPoint { get; set; }

        public decimal MaxPoint { get; set; }
    }
}
