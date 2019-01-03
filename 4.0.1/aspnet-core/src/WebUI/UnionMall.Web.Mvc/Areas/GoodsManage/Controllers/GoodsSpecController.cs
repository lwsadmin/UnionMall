using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UnionMall.Common.CommonSpec;
using UnionMall.Controllers;
using UnionMall.Goods;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GoodsManage.Controllers
{
    [Area("GoodsManage")]
    [AbpMvcAuthorize("OnlineGoods.GoodsSpec")]
    public class GoodsSpecController : UnionMallControllerBase
    {
        private readonly ICommonSpecAppService _AppService;
        private readonly IGoodsCategoryAppService _catAppService;
        private readonly ISpecValueAppService _valueAppService;
        public GoodsSpecController(ICommonSpecAppService AppService, ISpecValueAppService valueAppService, IGoodsCategoryAppService catAppService)
        {
            _AppService = AppService;
            _valueAppService = valueAppService;
            _catAppService = catAppService;
        }
        public async Task<IActionResult> List(string name, string CategoryId, int page = 1, int pageSize = 10)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(name))
            { where = $" and s.Name like '%{name}%'"; }
            if (!string.IsNullOrEmpty(CategoryId))
            { where = $" and s.CategoryId={CategoryId}"; }
            int total;
            DataSet ds = _AppService.GetPage(page, pageSize, " ", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), page, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }

            ViewBag.Category = _catAppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            return View(pageList);
        }

        public async Task<IActionResult> Add(long? id)
        {
            ViewBag.Category = _catAppService.GetCategoryDropDownList(AbpSession.TenantId, 0);
            if (id == null)
            {
                var s = _AppService.GetByIdAsync((long)id);

                return PartialView("_Add", s);
            }
            var dtos = await _AppService.GetByIdAsync((long)id);
            var list = await _valueAppService.GetBySpecId((long)id);
            string str = string.Empty;
            foreach (var item in list)
            {
                str += $@"<li><a href='javascript:void(0);'>{item.Text}</a>
                                                <button onclick='RemoveItem(this);' type='button' class='close'>
                                                    <span aria-hidden= 'true' >×</span><span class='sr-only'>Close</span>
                                                </button>
                                            </li>";
            }
            ViewBag.Spec = str ?? "";
            return PartialView("_Add", dtos);
        }
    }
}