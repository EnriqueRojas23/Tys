using System.Web.Mvc;

namespace Web.TYS.Areas.Seguimiento
{
    public class SeguimientoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Seguimiento";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Seguimiento_default",
                "Seguimiento/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
