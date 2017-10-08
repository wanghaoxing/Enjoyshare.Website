﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EnjoyShare.Framework.DBExtend
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope trans = null;
        public UnitOfWork()
        {
            trans = new TransactionScope();
        }

        public void Commit()
        {
            if (trans != null)
            {
                trans.Complete();//必须要调用scope.Complete()才能将数据更新到数据库
            }
        }

        public void Dispose()
        {
            if (trans != null)
            {
                trans.Dispose();
            }
        }
    }
}
