using ServiceStack.Redis;

namespace EnjoyShare.Framework.Caching.Init
{
    /// <summary>
    /// Redis管理中心
    /// </summary>
    public class RedisManager
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static readonly RedisConfigInfo RedisConfigInfo = new RedisConfigInfo();

        /// <summary>
        /// Redis客户端池化管理
        /// </summary>
        private static PooledRedisClientManager _prcManager;

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        static RedisManager()
        {
            CreateManager();
        }

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            string[] writeServerConStr = RedisConfigInfo.WriteServerList.Split(',');
            string[] readServerConStr = RedisConfigInfo.ReadServerList.Split(',');
            _prcManager = new PooledRedisClientManager(readServerConStr, writeServerConStr,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = RedisConfigInfo.MaxWritePoolSize,
                                 MaxReadPoolSize = RedisConfigInfo.MaxReadPoolSize,
                                 AutoStart = RedisConfigInfo.AutoStart,
                             });
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static IRedisClient GetClient()
        {
            return _prcManager.GetClient();
        }
    }
}
