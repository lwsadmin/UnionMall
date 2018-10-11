
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
namespace UnionMall.Business.ChainStore
{
    //TChainStore
    [Table("TChainStore")]
    public class ChainStore : Entity<long>, IHasCreationTime, IMustHaveTenant
    {

        public int TenantId { get; set; }
        public string Name { get; set; }
        public long BusinessId { get; set; }
        public bool IsSystem { get; set; }
        public string Contact { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public bool IsTakeOut { get; set; } = false;
        public decimal AveragePrice { get; set; } = 0;
        public decimal TakeoutPrice { get; set; } = 0;
        public bool IsTakeoutSms { get; set; }
        public int AvailableSmsCount { get; set; } = 0;
        public decimal AvailablePoint { get; set; } = 0;
        public decimal AvailableValue { get; set; } = 0;
        public decimal SettlementMoney { get; set; } = 0;
        public int Sort { get; set; } = 0;
        public string Introduce { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}