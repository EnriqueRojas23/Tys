﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceAgents.Core.CoreReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CoreReference.IBackendService")]
    public interface IBackendService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackendService/ExecuteCommand", ReplyAction="http://tempuri.org/IBackendService/ExecuteCommandResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(CommandContracts.Common.CommandFault), Action="http://tempuri.org/IBackendService/ExecuteCommandCommandFaultFault", Name="CommandFault", Namespace="http://schemas.datacontract.org/2004/07/CommandContracts.Common")]
        CommandContracts.Common.CommandResult ExecuteCommand(CommandContracts.Common.Command command);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBackendService/ExecuteQuery", ReplyAction="http://tempuri.org/IBackendService/ExecuteQueryResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Seguridad.Parameters.ListarMenusxRolesParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.CRM.Parameters.ObtenerOrdenTransporteParameters))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.CRM.Parameters.ListarOrdenesTransporteParameters))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.SGCW.Parameters.ListarProductosParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.SGCW.Parameters.ListarSubProductosParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.SGCW.Parameters.ObtenerServicioGateOutParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.ObtenerDatosConexionParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Configuracion.Parameters.MultiusoParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ActualizarPagoParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.EliminarDocumentoSolportParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.EliminarDetalleSolportParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ListarClavesAplicacionParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ListarDepotParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.InsertarDocumentosSolportParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ListarTransportistasSearchParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ListarItemSearchParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ListarOficinaParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ListarTamanyoContenedoresParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ListarTiposContenedoresParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ObtenerCIFParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Parameters.ObtenerReservaCabeceraParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.NPTP2.Parameters.ListadoNaveParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.NPTP2.Parameters.ListadoViajeParameter))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ActualizarPagoResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.EliminarDocumentoSolportResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.EliminarDetalleSolportResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ListarClavesAplicacionResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ListarDepotResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.insertarDocumentosSolportResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ListarTransportistasSearchResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ListarItemSearchResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ListarOficinaResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ListarTamanyoContenedoresResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ListarTiposContenedoresResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ObtenerCIFResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.Contenedores.Results.ObtenerReservaCabeceraResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.NPTP2.Results.ListadoNaveResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.NPTP2.Results.ListadoViajeResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.SGCW.Results.ListarProductosResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.SGCW.Results.ListarSubProductosResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.SGCW.Results.ObtenerServicioGateOutResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Configuracion.Results.MultiusoResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.Seguridad.Results.ListarMenusxRolesResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.CRM.Result.ObtenerOrdenTransporteResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Core.CRM.Result.ListarOrdenesTransporteResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(QueryContracts.Common.ObtenerDatosConexionResult))]
        QueryContracts.Common.QueryResult ExecuteQuery(QueryContracts.Common.QueryParameter parameter);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBackendServiceChannel : ServiceAgents.Core.CoreReference.IBackendService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BackendServiceClient : System.ServiceModel.ClientBase<ServiceAgents.Core.CoreReference.IBackendService>, ServiceAgents.Core.CoreReference.IBackendService {
        
        public BackendServiceClient() {
        }
        
        public BackendServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BackendServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BackendServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BackendServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public CommandContracts.Common.CommandResult ExecuteCommand(CommandContracts.Common.Command command) {
            return base.Channel.ExecuteCommand(command);
        }
        
        public QueryContracts.Common.QueryResult ExecuteQuery(QueryContracts.Common.QueryParameter parameter) {
            return base.Channel.ExecuteQuery(parameter);
        }
    }
}
