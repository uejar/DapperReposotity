using DapperEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DapperReposotity
{
    public class UserRepository : DapperRepository<UserModel>, IUserRepository
    {
        public UserRepository(IDbConnection connection) : base(connection) { }
    }
}
