using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnionMall.Common;
using UnionMall.Controllers;
using UnionMall.Entity;
using UnionMall.Gift;
using UnionMall.GiftMall.Dto;
using X.PagedList;

namespace UnionMall.Web.Mvc.Areas.GiftMall.Controllers
{
    [Area("GiftMall")]
    [AbpMvcAuthorize("GiftMall.GiftCategory")]
    public class GiftController : UnionMallControllerBase
    {

        private readonly ICommonCategoryAppService _catAppService;
        private readonly IImageAppService _imgAppService;
        private readonly IGiftAppService _AppService;
        private readonly ICommonAppService _comService;
        public GiftController(ICommonCategoryAppService catAppService,
            IGiftAppService AppService,
            ICommonAppService comService, IImageAppService imgAppService)
        {
            _catAppService = catAppService;
            _AppService = AppService;
            _comService = comService;
            _imgAppService = imgAppService;
        }
        public async Task<IActionResult> List(int pageIndex = 1, int pageSize = 10, string categoryId = "", string title = "")
        {
            string where = _comService.GetWhere();
            ViewBag.Title = title;
            ViewBag.Cat = categoryId;
            if (!string.IsNullOrEmpty(title))
                where += $" and g.name like '%{title}%' ";
            if (!string.IsNullOrEmpty(categoryId))
                where += $" and g.categoryId ={categoryId} ";
            int total;
            DataSet ds = _AppService.GetPage(pageIndex, pageSize, "g.sort desc,g.id desc", out total, where);
            IPagedList pageList = new PagedList<DataRow>(ds.Tables[0].Select(), pageIndex, pageSize, total);
            ViewBag.PageSize = pageSize;
            if (Request.Headers.ContainsKey("x-requested-with"))
            {
                return View("_Table", pageList);
            }
            ViewBag.Category = await _catAppService.GetCategoryDropDownList(0, 1);
            return View(pageList);
        }
        public async Task<IActionResult> Add(long? id)
        {
            CreateOrEditDto s = new CreateOrEditDto();
            List<Entity.Image> imageList = new List<Entity.Image>();
            Entity.Gift a = new Entity.Gift();
            if (id != null)
            {
                a = await _AppService.GetByIdAsync((long)id);
                imageList = await _imgAppService.GetList(c => c.ObjectId == id && c.Type == (int)ImageType.礼品图片);
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
            s.Gift = a;
            s.ImageList = imageList;
            var list = await _catAppService.GetCategoryDropDownList(0, 1);
            ViewData.Add("Category", new Microsoft.AspNetCore.Mvc.Rendering.SelectList(list, "Id", "Title"));
            return View(s);
        }
    }
}