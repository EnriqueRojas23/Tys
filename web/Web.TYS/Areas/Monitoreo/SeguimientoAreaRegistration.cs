using System.Web.Mvc;

namespace Web.TYS.Areas.Monitoreo
{
    public class MonitoreoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Monitoreo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Monitoreo_default",
                "Monitoreo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
