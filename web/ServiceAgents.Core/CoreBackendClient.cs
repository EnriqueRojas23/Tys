

using CommandContracts.Common;
using QueryContracts.Common;
using ServiceAgents.Common;
using ServiceAgents.Core.CoreReference;
using System.Reflection;



namespace ServiceAgents.Core
{
    public class CoreBackendClient : IBackendClient
    {
        static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        BackendServiceClient client = new BackendServiceClient("CoreWSHttpBinding_IBackendService", BindingClient.UrlDisponible(AssemblyName));
        public CommandResult ExecuteCommand(Command command)
        {
            return client.ExecuteCommand(command);
        }

        public QueryResult ExecuteQuery(QueryParameter parameter)
        {
            return client.ExecuteQuery(parameter);
        }

        public void Dispose()
        {
            client.Close();
        }
    }
}
