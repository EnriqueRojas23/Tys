using System.Web.Mvc;

namespace Web.TYS.Areas.Pago
{
    public class FacturacionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Pago";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Pago_default",
                "Pago/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
