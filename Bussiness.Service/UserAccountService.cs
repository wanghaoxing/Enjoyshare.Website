using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.Interface;
using Ef.Models;

namespace Bussiness.Service
{
    public class UserAccountService : BaseService, IUserAccountService
    {
        private DbSet<UserAccount> _UserDbSet = null;
        public UserAccountService(DbContext context) : base(context)
        {
            this._UserDbSet = context.Set<UserAccount>();
        }

        public UserAccount UserLogin(string name)
        {
            return this._UserDbSet.FirstOrDefault(u => u.Name.Equals(name));
        }

        public void LastLogin(UserAccount user)
        {
            user.LastLoginTime = DateTime.Now;
            base.Update(user);
        }
        public UserAccount InsertUser(UserAccount user)
        {
            return base.Insert<UserAccount>(user);
        }
    }
}
