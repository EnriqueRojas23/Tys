using System.Configuration;
using WindowsService.Common;
//
namespace DistributedServices.Core
{
    public class InstallerParameters : ServiceInstallerBase
    {
        public InstallerParameters()
        {
            Name = "GCCore";//ConfigurationManager.AppSettings["ServiceName"];
            DisplayName = "Servicio  Core"; // ConfigurationManager.AppSettings["ServiceDisplayName"];
            Description = "Servicio de Aplicacion Window Terminal";//ConfigurationManager.AppSettings["ServiceDescription"];
        }
    }
}
