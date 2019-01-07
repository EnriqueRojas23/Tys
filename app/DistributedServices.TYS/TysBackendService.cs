using CommandContracts.Common;
using CommandHandlers.Common;
using DistributedServices.Common;
using QueryContracts.Common;
using QueryHandlers.Common;
using System.ServiceModel;

namespace DistributedServices.TYS
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TysBackendService : IBackendService
    {
        public CommandResult ExecuteCommand(Command command)
        {
            var commandDispatcher = new CommandDispatcher();
            return commandDispatcher.Dispatch(command);
        }

        public QueryResult ExecuteQuery(QueryParameter parameter)
        {
            var commandDispatcher = new QueryDispatcher();
            var parametro = commandDispatcher.Dispatch(parameter);
            return parametro;
        }
    }
}