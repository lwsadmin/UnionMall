using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnionMall.IRepositorySql;
using UnionMall.MultiTenancy;
using Abp.UI;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
namespace UnionMall.Common
{
    public class CommonAppService : ApplicationService, ICommonAppService
    {
        private readonly IRepository<MultiTenancy.Tenant> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly IHostingEnvironment _HostingEnvironment;
        private readonly ISqlExecuter _sqlExecuter;
        public CommonAppService(IRepository<MultiTenancy.Tenant> Repository, IAbpSession AbpSession,
            IHostingEnvironment HostingEnvironment, ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _HostingEnvironment = HostingEnvironment;
            _sqlExecuter = sqlExecuter;
        }
        #region DataTableToExcel
        public async Task<MemoryStream> DataTableToExcel(DataTable dt)
        {

            XSSFWorkbook workbook = null;
            MemoryStream ms = null;
            ISheet sheet = null;
            XSSFRow headerRow = null;
            try
            {

                workbook = new XSSFWorkbook();
                ms = new MemoryStream();
                sheet = workbook.CreateSheet();
                headerRow = (XSSFRow)sheet.CreateRow(0);
                foreach (DataColumn column in dt.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                int rowIndex = 1;
                foreach (DataRow row in dt.Rows)
                {
                    XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dt.Columns)
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    ++rowIndex;
                }
                //列宽自适应，只对英文和数字有效
                for (int i = 0; i <= dt.Columns.Count; ++i)
                    sheet.AutoSizeColumn(i);
                workbook.Write(ms);
                ms.Flush();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.Message);
                Logger.Warn(ex.StackTrace);
                //  return null;
            }
            finally
            {
                ms.Close();
                sheet = null;
                headerRow = null;
                workbook = null;
            }
            return ms;
        }
        #endregion

        #region ExcelToDataTable
        public DataTable ExcelToDataTable(IFormFile flie, out string msg)
        {
            msg = "Success";
            try
            {
                IWorkbook workbook = WorkbookFactory.Create(flie.OpenReadStream());  //新建IWorkbook对象  

                ISheet sheet = workbook.GetSheetAt(0);
                DataTable dt = new DataTable();
                if (sheet == null)
                { msg = "dataTable sheet is null"; return dt; }
                //默认，第一行是字段
                IRow headRow = sheet.GetRow(0);
                if (headRow == null || headRow.Cells.Count == 0)
                {
                    msg = "headRow is null or headRow.RowNum==0"; return dt;
                }
                //设置datatable字段
                for (int i = headRow.FirstCellNum, len = headRow.LastCellNum; i < len; i++)
                {
                    dt.Columns.Add(headRow.Cells[i].StringCellValue);
                }

                for (int i = (sheet.FirstRowNum + 1), len = sheet.LastRowNum + 1; i < len; i++)
                {

                    IRow tempRow = sheet.GetRow(i);

                    DataRow dataRow = dt.NewRow();
                    //遍历一行的每一个单元格
                    bool nullCell = false;
                    for (int r = 0, j = tempRow.FirstCellNum, len2 = tempRow.LastCellNum; j < len2; j++, r++)
                    {

                        ICell cell = tempRow.GetCell(j);
                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.String:
                                    dataRow[r] = cell.StringCellValue;
                                    nullCell = nullCell || true;
                                    break;
                                case CellType.Numeric:
                                    dataRow[r] = cell.NumericCellValue;
                                    nullCell = nullCell || true;
                                    break;
                                case CellType.Boolean:
                                    dataRow[r] = cell.BooleanCellValue;
                                    nullCell = nullCell || true;
                                    break;
                                case CellType.Blank:
                                    nullCell = nullCell || false;
                                    break;
                                default:
                                    nullCell = nullCell || true;
                                    dataRow[r] = "ERROR";
                                    break;
                            }
                        }

                    }
                    if (nullCell)
                    { dt.Rows.Add(dataRow); }
                    else { break; }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);

            }
        }
        #endregion




        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }


        public string GetWhere()
        {
            string where = string.Empty;

            if (_AbpSession.TenantId == null || (int)_AbpSession.TenantId <= 0)
            {
                return where;
            }
            if (_AbpSession.TenantId != null && (int)_AbpSession.TenantId > 0)
            {
                where += $" and *.TenantId={_AbpSession.TenantId}";
            }

            DataTable role = _sqlExecuter.ExecuteDataSet($"select s.Id, r.Name RoleName,r.ManageRole,r.BusinessId from dbo.TUsers s " +
                $"left join dbo.TUserRoles ur on s.Id=ur.UserId left join dbo.TRoles r on ur.RoleId = r.Id where s.id={_AbpSession.UserId}").Tables[0];
            if (role.Rows[0]["RoleName"].ToString().ToUpper() != "ADMIN")// 
            {
                where += $" and *.BusinessId={role.Rows[0]["BusinessId"]}";
            }
            return where;


        }

        public JsonResult SaveSingleImg(IFormFile file, int tenandId)
        {
            Tenant t = _Repository.Get(tenandId);
            long size = file.Length;
            string name = file.FileName;
            string path = string.Format("/Upload/{0}/Images/{1}/", t.TenancyName, DateTime.Now.Year.ToString());
            string directoryPath = _HostingEnvironment.WebRootPath + path;

            //WebRootPath,带wwwroot

            //string directoryPath = _HostingEnvironment.ContentRootPath + path;
            //不带wwwroot
            if (!Directory.Exists(directoryPath))//不存在这个文件夹就创建这个文件夹  
            {
                Directory.CreateDirectory(directoryPath);
            }
            Random r = new Random(Guid.NewGuid().GetHashCode());
            string uploadFileName = DateTime.Now.ToString("MMddHHmmss") +
                DateTime.Now.Millisecond.ToString() +
                r.Next(1000, 9999).ToString() + Path.GetExtension(file.FileName);
            directoryPath += uploadFileName;
            using (FileStream fs = File.Create(directoryPath))
            {
                file.CopyTo(fs);
                fs.Flush();
                fs.Dispose();
            }
            //   FileStream fs = new FileStream(directoryPath, FileMode.Create);

            string url = path + uploadFileName;//返回的没有转换的相对路径到前端，前端传入后台存入数据库
            return new JsonResult(new { error = 0, succ = true, url = url, size = size, name = name });
        }
    }
}
