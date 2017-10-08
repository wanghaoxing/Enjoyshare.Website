using System;
using EnjoyShare.Framework.Caching.Init;
using ServiceStack.Redis;

namespace EnjoyShare.Framework.Caching.Interface
{
    public abstract class RedisBase : IDisposable
    {
        public IRedisClient Client { get; private set; }

        protected RedisBase()
        {
            Client = RedisManager.GetClient();
        }

        public virtual void FlushAll()
        {
            Client.FlushAll();
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Client.Dispose();
                    Client = null;
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            Client.Save();
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            Client.SaveAsync();
        }
    }
}
