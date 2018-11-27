using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TComment")]
    public class Comment : Entity<long>, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long MemberId { get; set; }
        public long ObjectId { get; set; }
        public int Praise { get; set; } = 0;
        public int ReplyCount { get; set; } = 0;
        public int Status { get; set; }
        public CommentType Type { get; set; }
        public long ReplyId { get; set; } = 0;
        public int CommentGrade { get; set; }
        public string Content { get; set; }
        public string Account { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;


    }
    public enum CommentType
    {
        资讯评论 = 0,
        商品评论 = 1,
        拼团评论 = 2,
        抢购评论 = 3
    }
}
