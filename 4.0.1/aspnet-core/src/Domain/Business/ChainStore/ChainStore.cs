
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
    public class TChainStore : Entity<long>, IHasCreationTime, IMustHaveTenant
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
        public bool IsTakeOut { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal TakeoutPrice { get; set; }
        public bool IsTakeoutSms { get; set; }
        public int AvailableSmsCount { get; set; }
        public int TotalSmsCount { get; set; }
        public decimal TotalPoint { get; set; }
        public decimal AvailablePoint { get; set; }
        public decimal TotalValue { get; set; }
        public decimal AvailableValue { get; set; }
        public decimal SettlementMoney { get; set; }
        public string Memo { get; set; }
        public int Sort { get; set; }
        public string Introduce { get; set; }
        public DateTime CreationTime { get; set; }
    }
}