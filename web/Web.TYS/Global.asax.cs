

namespace Web.TYS
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Web.Common.Caches;
    using Web.Common.HttpApplications;
    using Web.TYS.Security;

    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : CommonHttpApplication  
    {
        protected virtual void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null) return;
            if (!this.Request.IsAuthenticated) return;
            var identity = new CustomIdentity(HttpContext.Current.User.Identity);
            var principal = new CustomPrincipal(identity);
            HttpContext.Current.User = principal;
        }

        public static IEnumerable<Multiuso> GetMultiuso()
        {
            return MultiusoCache.Multiuso;
        }
      
    }
}