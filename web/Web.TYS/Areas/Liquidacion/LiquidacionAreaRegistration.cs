using System.Web.Mvc;

namespace Web.TYS.Areas.Liquidacion
{
    public class LiquidacionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Liquidacion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Liquidacion_default",
                "Liquidacion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
