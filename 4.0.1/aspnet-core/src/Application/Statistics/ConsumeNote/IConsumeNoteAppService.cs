﻿using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UnionMall.ConsumeNote
{
    public interface IConsumeNoteAppService : IApplicationService
    {
        DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "");
    }
}