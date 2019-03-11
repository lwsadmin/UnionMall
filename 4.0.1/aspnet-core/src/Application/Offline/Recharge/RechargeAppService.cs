using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnionMall.Business;
using UnionMall.Common;
using UnionMall.Enum;
using UnionMall.IRepositorySql;
using UnionMall.Sessions;
using UnionMall.Statistics;
using static UnionMall.Entity.ChainStoreCapitalNote;

namespace UnionMall.Offline
{
    public class RechargeAppService : ApplicationService, IRechargeAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly ISessionAppService _sessionAppService;
        private readonly IChainStoreAppService _storeServices;
        private readonly ICommonAppService _comServices;
        private readonly IRechargeNoteAppService _noteAppService;
        private readonly IRepository<Entity.ChainStoreCapitalNote, long> _capRepository;
        private readonly IRepository<UnionMall.Entity.Member, long> _memberRepository;
        public RechargeAppService(ISqlExecuter sqlExecuter, IAbpSession AbpSession,
            IRepository<UnionMall.Entity.Member, long> memberRepository, IRepository<Entity.ChainStoreCapitalNote, long> capRepository,
            ISessionAppService sessionAppService, IRechargeNoteAppService noteAppService,
            IChainStoreAppService storeServices, ICommonAppService comServices)
        {
            _sqlExecuter = sqlExecuter;
            _AbpSession = AbpSession;
            _memberRepository = memberRepository;
            _noteAppService = noteAppService;
            _comServices = comServices;
            _sessionAppService = sessionAppService;
            _storeServices = storeServices;
            _capRepository = capRepository;
            LocalizationSourceName = UnionMallConsts.LocalizationSourceName;
        }
        //[RemoteService(IsEnabled = false)]
        [Abp.Domain.Uow.UnitOfWork]
        public async Task<JsonResult> MemberRecharge(Entity.RechargeNote note)
        {
            var m = _memberRepository.FirstOrDefault(c => c.Id == note.MemberId);
            if (m == null)
            {
                return new JsonResult(new { succ = false, msg = L("NotExist{0}!", L("Member") + L("Records")) });
            }
            var UserInfo = await _sessionAppService.GetCurrentLoginInformations();
            var store = await _storeServices.GetByIdAsync(UserInfo.User.ChainStoreId);
            // Logger.Warn("11111111111111111111111111111111111111111" + _comServices.GetBillNumber(OrderNumberType.OFQ));
            //if (store.AvailableValue < note.Value)
            //{
            //    return new JsonResult(new { succ = false, msg = L("Store") + L("Usable") + L("Balance") + L("NEnough") + "!" });
            //}
            note.TenantId = UserInfo.Tenant.Id;
            note.Balance = m.Balance + note.Value;
            note.BusinessId = store.BusinessId;
            note.BillNumber = _comServices.GetBillNumber(OrderNumberType.OFMR);
            note.UserAccount = UserInfo.User.UserName;
            note.ChainStoreId = store.Id;
            m.Balance += note.Value;
            await _memberRepository.UpdateAsync(m);
            await _noteAppService.Create(note);
            //财务结算
            if (!store.IsSystem)
            {
                string business = $"select IsSystemBusiness from dbo.TBusiness where id=(select c.BusinessId from dbo.TChainStore c where id={store.Id})";
                string s = _sqlExecuter.ExecuteDataSet(business).Tables[0].Rows[0][0].ToString();
                if (s!= "True")
                {
                    store.SettlementMoney -= note.Value;
                    await _storeServices.CreateOrEditAsync(store);
                    UnionMall.Entity.ChainStoreCapitalNote capNote = new Entity.ChainStoreCapitalNote();

                    capNote.TenantId = store.TenantId;
                    capNote.ChainStoreId = store.Id;
                    capNote.BillNumber = note.BillNumber;
                    capNote.Type = 1;
                    capNote.Way = FinanceType.MemberRecharge;
                    capNote.Value = note.Value;
                    capNote.Balance = store.SettlementMoney;

                    capNote.UserAccount = UserInfo.User.UserName;
                    capNote.CreationTime = DateTime.Now;
                    capNote.Status = 0;
                    capNote.ApplyTime = DateTime.Now;
                    _capRepository.Insert(capNote);
                }
            }
            return new JsonResult(new { succ = true, msg = L("Recharge") + L("Success") + "!" });
        }
    }
}
