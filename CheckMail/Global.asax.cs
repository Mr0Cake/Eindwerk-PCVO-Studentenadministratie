// automatic header
// <copyright file="global.asax.cs" company="selligent">
//     Selligent
// </copyright>

namespace IngGenericTemplate
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Routing;
    using ContentRenderer.Logging;
    using ContentRenderer.Settings;
    using Routing;

    /// <summary>
    /// global handlers
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        #region Protected Methods

        /// <summary>
        /// application start
        /// </summary>
        /// <param name="sender">application object</param>
        /// <param name="e">given arguments</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// application Error
        /// </summary>
        /// <param name="sender">application object</param>
        /// <param name="e">given arguments</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            bool isProduction = (Request.Url.Host.StartsWith("staging", StringComparison.OrdinalIgnoreCase) || Request.Url.Host.StartsWith("localhost", StringComparison.OrdinalIgnoreCase)) ? false : true;
            HttpContext ctx = HttpContext.Current;

            if (isProduction)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ctx.Request.Url.ToString() + System.Environment.NewLine);
                sb.Append("Source:" + System.Environment.NewLine + ctx.Server.GetLastError().Source.ToString());
                sb.Append("Message:" + System.Environment.NewLine + ctx.Server.GetLastError().Message.ToString());
                sb.Append("Stack Trace:" + System.Environment.NewLine + ctx.Server.GetLastError().StackTrace.ToString());

                Logging.LogError(sb.ToString());

                if (HttpContext.Current.Request.Url.AbsolutePath == "/" || HttpContext.Current.Request.Url.AbsolutePath.EndsWith("/index.aspx", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect(Defaults.ErrorPage);
                }
                else
                {
                    ctx.Server.ClearError();
                    ctx.Application.Add("serverError", sb);
                    Response.Redirect("~/index.aspx");
                }

                return;
            }
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// register routes
        /// </summary>
        /// <param name="routes">routes to register</param>
        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add("Route1", new System.Web.Routing.Route("{step}", new RoutingHandler()));
            routes.Add("Route2", new System.Web.Routing.Route("{step}/{rest1}", new RoutingHandler()));
            routes.Add("Route3", new System.Web.Routing.Route("{step}/{rest1}/{rest2}", new RoutingHandler()));
        }

        #endregion Private Methods
    }
}