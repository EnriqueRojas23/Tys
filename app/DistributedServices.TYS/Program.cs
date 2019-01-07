using DistributedServices.Common;

namespace DistributedServices.TYS
{   
    class Program
    {
        static void Main(string[] args)
        {
            BackendServiceHost<TysBackendService>.Run(args);
        }
    }
}
