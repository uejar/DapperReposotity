using DapperEntities;
using DapperExtensions.Sql;
using DapperReposotity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            DapperExtensions.DapperExtensions.Configure(typeof(SnowClassMapper<>), new List<Assembly>(), new MySqlDialect());
            DapperExtensions.DapperExtensions.InstanceFactory = (config) => { return new SnowDapperImplementor(new SnowSqlGeneratorImpl(config)); };
            userRepository = ContainerConfig.Resolve<IUserRepository>();
        }

        [Test]
        public void Singel_Insert_Ture()
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
            int affected = userRepository.ExecuteInsert(user);
            Assert.IsTrue(affected == 1);
        }
        [Test]
        public void Mutil_Insert_Ture()
        {
            List<UserModel> list = new List<UserModel>();
            for (int i = 0; i < 10; i++)
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
                list.Add(user);
            }
            int affected = userRepository.ExecuteInsert(list);
            Assert.IsTrue(affected == 10);
        }
        [Test]
        public void SnowID()
        {
            Assert.IsNaN(SnowflakeIDcreator.nextId());
        }

    }
}
