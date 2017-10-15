using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ef.Models;

namespace Bussiness.Interface
{
    public interface IUserAccountService : IBaseService
    {        /// <summary>
             /// 用户登陆 支持账号/手机/邮箱作为账号
             /// </summary>
             /// <param name="name"></param>
             /// <returns></returns>
        UserAccount UserLogin(string name);

        /// <summary>
        /// 登陆成功后更新最后登陆时间
        /// </summary>
        /// <param name="user"></param>
        void LastLogin(UserAccount user);

       UserAccount InsertUser(UserAccount user);
    }
}
