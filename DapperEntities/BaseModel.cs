using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DapperEntities
{
    /// <summary>
    /// POCO 基类
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("id")] public virtual int Id { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("deleted")] public virtual bool Deleted { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>

        [Column("create_at")] public virtual DateTime CreateAt { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改时间
        /// </summary>

        [Column("update_at")] public virtual DateTime UpdateAt { get; set; } = DateTime.Now;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("memo")] public virtual string Memo { get; set; }

    }

    public enum GenderEnum
    {
        未知,
        男,
        女
    }
}
