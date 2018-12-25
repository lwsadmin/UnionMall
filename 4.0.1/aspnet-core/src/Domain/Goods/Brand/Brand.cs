using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
namespace UnionMall.Entity
{
    [Table("TBrand")]
    public class Brand : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        [Required(ErrorMessage = "")]
        public string Title { get; set; }
        public string Logo { get; set; }
        public int Sort { get; set; }
        public string Note { get; set; }
        public string Url { get; set; }
   

    }
}
