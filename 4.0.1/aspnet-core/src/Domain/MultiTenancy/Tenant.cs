using Abp.MultiTenancy;
using System;
using UnionMall.Authorization.Users;

namespace UnionMall.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public VersionType Version { get; set; }
        public DateTime DueTime { get; set; } = DateTime.Now.AddYears(1);
        public string License { get; set; }
        public string LegalPerson { get; set; }
        public string Tel { get; set; }

        public Tenant()
        {
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }

        public enum VersionType
        {
            Basics = 1,
            Standard = 2,
            Flagship = 3
        }
    }
}
