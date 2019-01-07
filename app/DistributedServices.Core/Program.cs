using DistributedServices.Common;


namespace DistributedServices.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendServiceHost<CoreBackendService>.Run(args);
        }
    }
}
