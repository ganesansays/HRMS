using Hrms.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hrms.Controllers
{
    public class CrudControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Get Controller Instance
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns>return Controller instance</returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return GetControllerInstance(controllerType);
        }

        /// <summary>
        /// Instancie un contrôleur
        /// </summary>
        /// <param name="controllerType">Type de contrôleur (ex ContactController)</param>
        /// <returns></returns>
        private static IController GetControllerInstance(Type controllerType)
        {
            return controllerType == null ? null :
                CompositionRoot.GetTypeFromUIContainer<IController>(controllerType);
        }

        /// <summary>
        /// Release Controller
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
        }
    }
}