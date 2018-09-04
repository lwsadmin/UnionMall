using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UnionMall.Sessions;

namespace UnionMall.Web.Views.Shared.Components.TopBar
{

    public class TopBarLanguageSwitchViewModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }
    }

    public class TopBarViewComponent : UnionMallViewComponent
    {
        private readonly ILanguageManager _languageManager;
        private readonly ISessionAppService _sessionAppService;
        public TopBarViewComponent(ILanguageManager languageManager, ISessionAppService sessionAppService)
        {
            _languageManager = languageManager;
            _sessionAppService = sessionAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new TopBarLanguageSwitchViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList()
            };
            ViewBag.LoginInfo = await _sessionAppService.GetCurrentLoginInformations();
            return View("TopBar", model);
          //  return Task.FromResult(View(model) as IViewComponentResult);
        }
    }
}
