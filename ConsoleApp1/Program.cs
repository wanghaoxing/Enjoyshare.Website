using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ef.Models;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var dbContext = new Enjoyshare())
            //{
            //    dbContext.Database.Log += Console.WriteLine;//不用sql分析器就能查看efsql执行过程
            //    var user2 = dbContext.Address.Find(2);
            //    var list3 = dbContext.BookInfo.Where(u => u.Pkid < 3).ToList();
            //    dbContext.SaveChanges();

            //}

            IocTest.Show();
            Console.ReadKey();
        }
    }
}
