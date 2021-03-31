using DapperEntities;
using DapperExtensions;
using DapperExtensions.Sql;
using DapperReposotity;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
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
            IDapperExtensionsConfiguration config = new SnowConfiguration(true, typeof(SnowClassMapper<>), null, new MySqlDialect());
            DapperExtensions.DapperExtensions.InstanceFactory = (config) => { return new SnowDapperImplementor(new SnowSqlGeneratorImpl(config)); };
            DapperExtensions.DapperExtensions.Configure(config);
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
            bool affected = userRepository.ExecuteInsert(list);
            Assert.IsTrue(affected);
        }
        [Test]
        public void Singel_Get_True()
        {
            UserModel user = userRepository.GetById(26495435886347264);
            Assert.AreEqual(user.UserName, "w.f");
        }
        [Test]
        public void Mutil_Get_True()
        {
            IDictionary<string, object> kvp = new Dictionary<string, object>();
            kvp.Add("UserName", "w.f");
            kvp.Add("ID", 26495435886347264);
            IEnumerable<UserModel> users = userRepository.GetList(kvp);
            Assert.IsTrue(users.Count() == 1);
        }

        [Test]
        public void Singel_Update_True()
        {
            UserModel user = userRepository.GetById(26495435886347264);
            UserModel dest = (UserModel)user.Clone();

            dest.UserName = "111111111111111";
            bool affected = userRepository.ExecuteUpdate(dest);
            Assert.IsTrue(affected);
        }

        [Test]
        public void SnowID()
        {
            Assert.IsNaN(SnowflakeIDcreator.nextId());
        }

    }
}
