
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Dependency;
using Abp.Runtime.Session;
namespace UnionMall.Entity
{
    [Table("TChainStore")]
    public class ChainStore : Entity<long>, IHasCreationTime, IMustHaveTenant
    {

        [Required]
        public int TenantId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long BusinessId { get; set; }
        [Required]
        public bool IsSystem { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Mobile { get; set; }

        public string Image { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public string Address { get; set; }

        public string Longitude { get; set; } = "";

        public string Latitude { get; set; } = "";
        [Required]
        public bool IsTakeOut { get; set; } = false;
        [Required]
        public decimal AveragePrice { get; set; } = 0;
        [Required]
        public decimal TakeoutPrice { get; set; } = 0;
        public bool IsTakeoutSms { get; set; }
        [Required]
        public int AvailableSmsCount { get; set; } = 0;
        [Required]
        public decimal AvailablePoint { get; set; } = 0;
        [Required]
        public decimal AvailableValue { get; set; } = 0;
        [Required]
        public decimal SettlementMoney { get; set; } = 0;
        [Required]
        public int Sort { get; set; } = 0;
        [Required]
        public string Introduce { get; set; }

        public decimal OffLineDiscount { get; set; } = 0;
        public decimal OffLineCommission { get; set; } = 0;
        public decimal OnLineCommission { get; set; } = 0;
        [Required]
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}