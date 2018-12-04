using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Business;
using UnionMall.Business.Dto;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.FlashSale;
using UnionMall.FlashSale.Dto;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.FlashSaleMall.Controllers
{
    [Area("FlashSale")]
    [AbpMvcAuthorize("FlashSale.FlashSaleList")]
    public class GoodsController : UnionMallControllerBase
    {

        private readonly IBusinessAppService _businessAppService;
        private readonly IImageAppService _imgAppService;
        private readonly IFlashSaleAppService _AppService;
        private readonly ICommonAppService _comService;
        public GoodsController(IBusinessAppService businessAppService,
            IFlashSaleAppService AppService,
            ICommonAppService comService, IImageAppService imgAppService)
        {
            _businessAppService = businessAppService;
            _AppService = AppService;
            _comService = comService;
            _imgAppService = imgAppService;
        }
        public async Task<IActionResult> List(string businessId, string title, int pageIndex = 1, int pageSize = 10)
        {
            string where = _comService.GetWhere();
            if (!string.IsNullOrEmpty(title))
                where += $" and f.title like '%{title}%' ";
            if (!string.IsNullOrEmpty(businessId))
            { where += $" and f.BusinessId = {businessId}"; }
            int total;
            DataSet ds = _AppService.GetPage(pageIndex, pageSize, "f.id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            List<BusinessDropDownDto> dtoList = (await _businessAppService.GetDropDown());
            ViewData.Add("Business", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(dtoList, "Id", "BusinessName"));
            return View(pageList);
        }
        public async Task<IActionResult> Add(long? id)
        {
            CreateOrEditDto s = new CreateOrEditDto();
            List<Entity.Image> imageList = new List<Entity.Image>();
            Entity.FlashSale a = new Entity.FlashSale();
            if (id != null)
            {
                a = await _AppService.GetByIdAsync((long)id);
                imageList = await _imgAppService.GetList(c => c.ObjectId == id);
                string imgs = "", config = "";
                for (int i = 0; i < imageList.Count; i++)
                {
                    imgs += $"\'{imageList[i].Url.ToString()}\',";
                    //config += string.Format("{" + "key: 'item{0}',url:'{1}',size:{2}" + "}",
                    //    imageList[i].ToString(), imageList[i].Url, imageList[i].Size);
                    config += "{img:'" + imageList[i].Url + "', key: '" + imageList[i].Id.ToString() + "',url:'/common/deleteimg'},";
                }
                ViewBag.Images = imgs;
                ViewBag.Config = config;
            }
            s.FlashSale = a;
            s.ImageList = imageList;

            return View(s);
        }
    }
}