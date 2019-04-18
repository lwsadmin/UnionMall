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
using UnionMall.Enum;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Microsoft.Extensions.Caching.Memory;

namespace UnionMall.Common
{
    public class CommonAppService : ApplicationService, ICommonAppService
    {
        private readonly IRepository<MultiTenancy.Tenant> _Repository;
        private readonly IAbpSession _AbpSession;
        private readonly IHostingEnvironment _HostingEnvironment;
        private readonly ISqlExecuter _sqlExecuter;
        //https://www.cnblogs.com/MicroHeart/p/9448869.html
        private readonly IMemoryCache _memoryCache;
        public CommonAppService(IRepository<MultiTenancy.Tenant> Repository, IAbpSession AbpSession, IMemoryCache memoryCache,
            IHostingEnvironment HostingEnvironment, ISqlExecuter sqlExecuter)

        {
            _Repository = Repository;
            _AbpSession = AbpSession;
            _HostingEnvironment = HostingEnvironment;
            _memoryCache = memoryCache;
            _sqlExecuter = sqlExecuter;
        }
        [RemoteService(IsEnabled = false)]
        #region DataTableToExcel
        public async Task<MemoryStream> DataTableToExcel(string sql)
        {

            XSSFWorkbook workbook = null;
            MemoryStream ms = null;
            ISheet sheet = null;
            XSSFRow headerRow = null;
            try
            {
                DataTable dt = _sqlExecuter.ExecuteDataSet(sql).Tables[0];

                //1、滑动过期时间  缓存在两秒内没有被命中，则失效，命中后缓存时间又变为2秒
                _memoryCache.Set("DataTableToExcel", "0", new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = new TimeSpan(0, 0, 10),
                    Priority = CacheItemPriority.NeverRemove
                });

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
                    _memoryCache.Set("DataTableToExcel", (dt.Rows.Count/rowIndex), new MemoryCacheEntryOptions()
                    {
                        SlidingExpiration = new TimeSpan(0, 0, 10),
                        Priority = CacheItemPriority.NeverRemove
                    });
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
        [RemoteService(IsEnabled = false)]
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

        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string table, string orderBy, out int total)
        {
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
        [RemoteService(IsEnabled = false)]
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
        [RemoteService(IsEnabled = false)]
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
                if (fs.Length > 10240)
                {
                    Compress(fs, path + uploadFileName);
                }
                else
                {
                    file.CopyTo(fs);
                    fs.Flush();
                    fs.Dispose();
                }

            }
            //   FileStream fs = new FileStream(directoryPath, FileMode.Create);

            string url = path + uploadFileName;//返回的没有转换的相对路径到前端，前端传入后台存入数据库
            return new JsonResult(new { error = 0, succ = true, url = url, size = size, name = name });
        }
        [RemoteService(IsEnabled = false)]
        public string GetBillNumber(OrderNumberType orderTypes)
        {
            string TypeName = System.Enum.GetName(typeof(OrderNumberType), orderTypes);
            string num1 = DateTime.Now.ToString("yyyyMMdd");
            Random rd = new Random();
            string BillNumber = TypeName + num1 + rd.Next(0, 99999).ToString("D5");
            return BillNumber;
        }
        [RemoteService(IsEnabled = false)]
        public string GetCodeNumber()
        {
            Random rd = new Random();
            return rd.Next(0, 99999999).ToString("D8");
        }
        [RemoteService(IsEnabled = false)]
        /// <summary>
        /// 生成随机字母与数字组合
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public string GetStr(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }

        /// <summary>
        /// 压缩到指定尺寸
        /// </summary>
        /// <param name="oldfile">原文件</param>
        /// <param name="newfile">新文件</param>
        [RemoteService(IsEnabled = false)]
        public bool Compress(Stream stream, string newfile)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                System.Drawing.Imaging.ImageFormat thisFormat = img.RawFormat;

                Size newSize = new Size(img.Width, img.Height);
                Bitmap outBmp = new Bitmap(newSize.Width, newSize.Height);
                Graphics g = Graphics.FromImage(outBmp);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, newSize.Width, newSize.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                g.Dispose();
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x]; //设置JPEG编码
                        break;
                    }
                img.Dispose();
                if (jpegICI != null) outBmp.Save(newfile, System.Drawing.Imaging.ImageFormat.Jpeg);
                outBmp.Dispose();
                stream.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public  object GetMemoryCache(string key)
        {
            return _memoryCache.Get(key);
        }
    }
}
