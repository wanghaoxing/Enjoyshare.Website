using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace EnjoyShare.Framework.IOC
{    /// <summary>
     /// 依赖注入工厂
     /// </summary>
    public class DIFactory
    {
        private static readonly object SyncHelper = new object();
        private static readonly Dictionary<string, IUnityContainer> UnityContainerDictionary = new Dictionary<string, IUnityContainer>();
        /// <summary>
        /// 根据containerName获取指定的container
        /// </summary>
        /// <param name="containerName">配置的containerName，默认为defaultContainer</param>
        /// <returns></returns>
        public static IUnityContainer GetContainer(string containerName = "Container")
        {
            if (!UnityContainerDictionary.ContainsKey(containerName))
            {
                lock (SyncHelper)
                {
                    if (!UnityContainerDictionary.ContainsKey(containerName))
                    {
                        //配置UnityContainer
                        IUnityContainer container = new UnityContainer();
                        ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                        fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config.xml");
                        Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                        UnityConfigurationSection configSection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
                        configSection.Configure(container, containerName);

                        UnityContainerDictionary.Add(containerName, container);
                    }
                }
            }
            return UnityContainerDictionary[containerName];
        }
    }

}
