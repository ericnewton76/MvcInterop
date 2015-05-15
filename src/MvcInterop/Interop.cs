using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcInterop
{
    internal static class Interop
    {
        #region -- Init Helpers & Request Context --

        internal static RequestContext InitRequestContext(string controllerName, RouteData routeData, HttpContextBase httpContext, Type type)
        {
            // Create RouteData
            routeData.Values["controller"] = controllerName;

            if (type != null && type.BaseType != null)
            {
                // Default the action to the BaseType's Name
                routeData.Values["action"] = type.BaseType.Name;
            }

            // Create RequestContext
            return new RequestContext(httpContext, routeData);
        }

        internal static T CreateController<T>(RequestContext requestContext, string controllerName) where T : ControllerBase
        {
            if (requestContext == null) throw new ArgumentNullException("requestContext");
            if (String.IsNullOrWhiteSpace(controllerName)) throw new ArgumentNullException("controllerName");

            var controller = ControllerBuilder.Current.GetControllerFactory().CreateController(requestContext, controllerName) as T;

            if (controller == null)
                throw new InvalidOperationException(String.Format("Cannot create instance of controller {0}", controllerName));

            // If we still do not have a Controller, Exception

            // Create a controller context for the route and HttpContext
            var controllerContext = new ControllerContext(requestContext, controller);
            controller.ControllerContext = controllerContext;

            return controller;
        }

        /// <summary>
        /// Will get the default controller name.  First based on the parent namespace, second based on the default route
        /// </summary>
        /// <returns></returns>
        internal static string GetDefaultControllerName(Type type, RouteData mvcRouteData, HttpContextBase httpContext, ref RequestContext requestContext, ref ControllerBase controller)
        {
            if (type == null || type.BaseType == null || type.BaseType.Namespace == null) return string.Empty;

            var parentNamespace = type.BaseType.Namespace.Split('.').Last();

            requestContext = InitRequestContext(parentNamespace, mvcRouteData, httpContext, type);

            try
            {
                controller = CreateController<ControllerBase>(requestContext, parentNamespace);
            }
            catch (HttpException)
            {
                // Continue
            }
            
            if (controller != null)
                return parentNamespace;

            // Try the defaultRoute
            var defaultRoute = RouteTable.Routes.OfType<Route>().LastOrDefault();
            if (defaultRoute == null)
                throw new InvalidOperationException("Cannot find a defaultRoute");

            object controllerName;
            if (!defaultRoute.Defaults.TryGetValue("controller", out controllerName))
                throw new InvalidOperationException("Cannot find default controller name from defaultRouteData");

            return Convert.ToString(controllerName);
        }

        #endregion
    }
}
