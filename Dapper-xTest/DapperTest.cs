using DapperEntities;
using DapperExtensions.Sql;
using DapperReposotity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dapper_xTest
{
    [TestFixture]
    public class DapperTest
    {
        private IUserRepository userRepository = null;
        [SetUp]
        public void Initliazation()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(DapperClassMapper<>);
            userRepository = ContainerConfig.Resolve<IUserRepository>();
        }

        [Test]
        public void Dapper_Insert_Ture()
        {
            UserModel user = new UserModel()
            {
                UserName = "w.f",
                Age = 25,
                Email = "2578879902",
                Gender = GenderEnum.男,
                Memo = "",
                Password = "123456",
                Phone = "",
                Salt = "",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
            int row = userRepository.ExecuteInsert(user);

            string sql = "select t.*,'cus_prop' customproperty from sys_user t where deleted=0";
            long count = 0;
            IEnumerable<UserDto> userDto = userRepository.ExecutePageList<UserDto>(sql,0,10,out count,null);
            Assert.AreEqual(count, 1);
            Assert.AreEqual(userDto.FirstOrDefault().CustomProperty, "cus_prop");

        }

    }
}
