using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DapperEntities
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("sys_user")]

    public class UserModel : BaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Column("user_name")] public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column("password")] public string Password { get; set; }
        /// <summary>
        /// 加密盐值
        /// </summary>
        [Column("salt")] public string Salt { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("email")] public string Email { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        [Column("phone")] public string Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Column("gender")] public GenderEnum Gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Column("age")] public int Age { get; set; }

    }

    public class UserDto : UserModel
    {
        public string CustomProperty { get; set; }
    }
}
