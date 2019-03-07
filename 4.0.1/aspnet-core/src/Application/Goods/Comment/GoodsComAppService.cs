using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using UnionMall.IRepositorySql;
using UnionMall.EntityFrameworkCore;
using UnionMall.Goods.Dto;
namespace UnionMall.Goods
{
    public class GoodsComAppService : ApplicationService, IGoodsComAppService
    {
        private readonly ISqlExecuter _sqlExecuter;
        public readonly IAbpSession _AbpSession;
        private readonly IRepository<Entity.Comment, long> _Repository;
        public GoodsComAppService(ISqlExecuter sqlExecuter,
            IRepository<Entity.Comment, long> Repository, IAbpSession AbpSession)
        {
            _sqlExecuter = sqlExecuter;
            _Repository = Repository;
            _AbpSession = AbpSession;
        }
        public async Task DeleteAsync(long id)
        {
            try
            {
                var query = _Repository.FirstOrDefault(c => c.Id == id);
                if (query != null)
                {
                    await _Repository.DeleteAsync(query);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace + e.Message);
                throw;
            }
        }
        [RemoteService(IsEnabled = false)]
        public DataSet GetPage(int pageIndex, int pageSize, string orderBy, out int total, string where = "", string table = "")
        {
            if (string.IsNullOrEmpty(table))
            {
                table = $@"select c.id,c.Praise,c.ReplyCount,a.Name,c.level,c.Images, c.CommentGrade, c.ReplyId,c.Content,c.CreationTime,
 m.WeChatName,m.HeadImg 
from dbo.TComment c left join	 dbo.TMember m on c.MemberId=m.id
left join dbo.TGoods a  on c.ObjectId=a.Id  left  join dbo.TChainStore s on a.ChainStoreId=s.Id where c.Type=1
and c.ReplyId=0 ";
            }
            where = where.Replace("*.BusinessId", "s.BusinessId").Replace(" *", " c");
            table += where;
            return _sqlExecuter.GetPagedList(pageIndex, pageSize, table, orderBy, out total);
        }
        public async Task<Entity.Comment> GetByIdAsync(long id)
        {
            return await _Repository.FirstOrDefaultAsync(c => c.Id == id);
        }
        [RemoteService(IsEnabled = false)]
        public async Task CreateOrEditAsync(Entity.Comment model)
        {
            model.TenantId = _AbpSession.TenantId ?? 0;
            if (model.Id <= 0)
            {
                await _Repository.InsertAsync(model);
            }
            else
            {
                await _Repository.UpdateAsync(model);
            }
        }
        [RemoteService(IsEnabled = false)]
        public async Task<List<ReplyListDto>> GetComByParentIdAsync(long parntId)
        {
            List<ReplyListDto> list = new List<ReplyListDto>();
            string table = $"select c.id,c.Content,c.Praise,c.CreationTime,m.WeChatName from  dbo.TComment c " +
                $"left join dbo.TMember m on c.MemberId=m.id where c.replyid={parntId}";
            DataTable dt = _sqlExecuter.ExecuteDataSet(table).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                list.Add(new ReplyListDto((long)item["id"],
                    item["WeChatName"].ToString(),
                    item["content"].ToString(),
                   item["praise"].ToString()));
            }
            return list;
            // var listStudent1 = _Repository.GetAllIncluding(s => s.).ToList();
            //  var s= from c in _Repository.
        }
    }
}
