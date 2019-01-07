using System.Configuration;
using WindowsService.Common;

//
namespace DistributedServices.TYS
{
    public class InstallerParameters : ServiceInstallerBase
    {
        public InstallerParameters()
        {
            Name = "GCTys"; //ConfigurationManager.AppSettings["ServiceName"];
            DisplayName = "Servicio  TYS";// ConfigurationManager.AppSettings["ServiceDisplayName"];
            Description = "Servicio de Aplicacion Window TYS";//ConfigurationManager.AppSettings["ServiceDescription"];
        }
    }
}