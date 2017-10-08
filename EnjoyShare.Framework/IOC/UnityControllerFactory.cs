using Microsoft.Practices.Unity;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnjoyShare.Framework.IOC
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private IUnityContainer UnityContainer => DIFactory.GetContainer();
        /// <summary>
        /// 创建控制器对象
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (null == controllerType)
            {
                return null;
            }
            IController controller = (IController)this.UnityContainer.Resolve(controllerType);
            return controller;
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            this.UnityContainer.Teardown(controller);//释放对象
        }
    }
}
