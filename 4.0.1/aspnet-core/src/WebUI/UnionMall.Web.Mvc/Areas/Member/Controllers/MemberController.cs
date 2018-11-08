using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Business;
using UnionMall.Business.Dto;
using UnionMall.Controllers;
using UnionMall.Member;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Common;

namespace UnionMall.Web.Mvc.Areas.Member.Controllers
{
    [Area("Member")]
    [AbpMvcAuthorize("Member.MemberList")]
    public class MemberController : UnionMallControllerBase
    {
        private readonly IMemberAppService _AppService;
        private readonly ICommonAppService _comAppService;
        private readonly IMemberLevelAppService _levelAppService;
        private readonly IBusinessAppService _businessAppService;
        private readonly IChainStoreAppService _storeAppService;
        public MemberController(IMemberAppService AppService, IMemberLevelAppService levelAppService,
            IBusinessAppService businessAppService, IChainStoreAppService storeAppService, ICommonAppService comAppService)
        {
            _AppService = AppService;
            _levelAppService = levelAppService;
            _businessAppService = businessAppService;
            _storeAppService = storeAppService;
            _comAppService = comAppService;
        }
        public async Task<IActionResult> List(int page = 1, int pageSize = 10, string Level = "", string Name = "",
            string Business = "", string Store = "", string RegTimeFrom = "", string RegTimeTo = "")
        {

            string where = string.Empty; where += _comAppService.GetWhere();
            if (!string.IsNullOrEmpty(Level))
                where += $" and levelId={Level}";
            if (!string.IsNullOrEmpty(Name))
                where += $" and Name like'%{Name}%'";
            if (!string.IsNullOrEmpty(Level))
                where += $" and BusinessId={Business}";
            if (!string.IsNullOrEmpty(Store))
                where += $" and ChainStoreId={Store}";
            if (!string.IsNullOrEmpty(RegTimeFrom))
                where += $" and RegTime>='{RegTimeFrom} 00:00:00'";
            if (!string.IsNullOrEmpty(RegTimeTo))
                where += $" and RegTime<='{RegTimeTo} 23:59:59'";

            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, "id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }

            var businessDropDown = (await _businessAppService.GetDropDown());
            ViewData.Add("Business", new SelectList(businessDropDown, "Id", "BusinessName"));

            var levelDropDown = (await _levelAppService.GetDropDown());
            ViewData.Add("Level", new SelectList(businessDropDown, "Id", "Title"));

            var storeDropDown = (await _storeAppService.GetDropDown());
            ViewData.Add("ChainStore", new SelectList(storeDropDown, "Id", "Name"));
            return View(pageList);
        }
    }
}

//USE[UnionMallDb]
//GO

///****** Object:  Table [dbo].[TMember]    Script Date: 2018/11/8 17:57:20 ******/
//set ansi_nulls on
//go

//set quoted_identifier on
//go

//create table[dbo].[TMember]
//(

//   [Id][bigint] identity(1,1) not null,
//	[TenantId] [int] not null,
//	[BusinessId] [bigint] not null,
//	[LevelId] [bigint] not null,
//	[ChainStoreId] [bigint] null,
//	[CardID] [nvarchar] (200) null,
//	[Name] [nvarchar] (100) null,
//	[NickName] [nvarchar] (100) null,
//	[PassWord] [nvarchar] (500) not null,
//	[HeadImg] [nvarchar] (300) null,
//	[Sex] [int] not null,
//	[BirthDay] [datetime] null,
//	[Mobile] [nvarchar] (50) null,
//	[Email] [nvarchar] (100) null,
//	[Profession] [nvarchar] (30) null,
//	[ProvinceId] [int] not null,
//	[CityId] [int] not null,
//	[DistrictId] [int] not null,
//	[Address] [nvarchar] (200) not null,
//	[Integral] [decimal] (18, 2) not null,
//	[Balance] [decimal] (18, 2) not null,
//	[TotalConsumeCount] [int] not null,
//	[LastConsumeTime] [datetime] null,
//	[Status] [int] null,
//	[RegTime] [datetime] not null,
//	[RegSource] [int] null,
//	[OpenID] [nvarchar] (100) null,
//	[Memo] [nchar] (10) null,
// constraint[PK_TMember_1] primary key clustered
//(
//   [Id] asc
//)with(pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on[PRIMARY]
//) on[PRIMARY]

//go

//alter table[dbo].[TMember] add constraint[DF_TMember_Status]  default ((0)) for [Status]
//go

//exec sys.sp_addextendedproperty @name = N'MS_Description', @value=N'0正常  1 锁定  2 删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TMember', @level2type=N'COLUMN',@level2name=N'Status'
//go
