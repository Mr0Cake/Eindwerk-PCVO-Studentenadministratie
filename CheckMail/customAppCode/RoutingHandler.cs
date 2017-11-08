// automatic header
// <copyright file="routinghandler.cs" company="selligent">
//     Selligent
// </copyright>

namespace Routing
{
    using System;
    using System.Web;
    using System.Web.Compilation;
    using System.Web.Routing;
    using System.Web.UI;
    using ContentRenderer.General;

    using Routing;

    /// <summary>
    /// is a routing handler
    /// </summary>
    public class RoutingHandler : IRouteHandler
    {
        #region Public Methods

        /// <summary>
        /// Creates a http handler
        /// </summary>
        /// <param name="requestContext">the request context</param>
        /// <returns>an interface</returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentException("Method 'GetHttpHandler' -> Parameter 'requestContext' is null");
            }

            string step = HttpUtility.HtmlDecode(Functions.FilterXss(requestContext.RouteData.Values["step"] as string) ?? string.Empty);
            string rest1 = HttpUtility.HtmlDecode(Functions.FilterXss(requestContext.RouteData.Values["rest1"] as string) ?? string.Empty);
            string rest2 = HttpUtility.HtmlDecode(Functions.FilterXss(requestContext.RouteData.Values["rest2"] as string) ?? string.Empty);

            HttpContext.Current.Items["step"] = step;
            HttpContext.Current.Items["rest1"] = rest1;
            HttpContext.Current.Items["rest2"] = rest2;

            string aspxVirtualPath = "~/index.aspx";

            return BuildManager.CreateInstanceFromVirtualPath(aspxVirtualPath, typeof(Page)) as Page;
        }

        #endregion Public Methods
    }
}