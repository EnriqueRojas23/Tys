

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
using System.Collections.Generic;
using CommandContracts.TYS.Seguimiento;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarCamionCompletoHandler : ICommandHandler<InsertarActualizarCamionCompletoCommand>
    {
        private readonly IRepository<CamionCompleto> _CamionCompletoRepository;


        public InsertarActualizarCamionCompletoHandler(IRepository<CamionCompleto> pCamionCompletoRepository)
        {
            this._CamionCompletoRepository = pCamionCompletoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarCamionCompletoCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");




            CamionCompleto dominio = null;
           if (command.idcamioncompleto.HasValue)
               dominio = _CamionCompletoRepository.Get(x => x.idcamioncompleto == command.idcamioncompleto).LastOrDefault();
            else
               dominio = new CamionCompleto();

           dominio.idcliente = command.idcliente;
           dominio.iddestino = command.iddestino;
           dominio.idorigen = command.idorigen;
           dominio.idformula = command.idformula;
           dominio.idtipotransporte = command.idtipotransporte;
           dominio.idtipounidad = command.idtipounidad;
           dominio.cantidaddestinos = command.cantidaddestinos;
           dominio.cantidadotscreaadas = command.cantidadotscreaadas;
           dominio.codigocamioncompleto = command.codigocamioncompleto;
           dominio.sobreestadia = command.sobreestadia;
           dominio.idvehiculo = command.idvehiculo;
           dominio.idcarga = command.idcarga;
           dominio.idtipooperacion = command.idtipooperacion;
           dominio.fecharegistro = DateTime.Now;
           dominio.sobreestadia = command.sobreestadia;
           dominio.subtotal = command.subtotal;
           dominio.total = command.total;
           dominio.igv = command.igv;
           dominio.idestacionorigen = command.idestacionorigen;
           dominio.idruta = command.idruta;
           dominio.usuarioregistro = command.usuarioregistro;
           dominio.idconceptocobro = command.idconceptocobro;
           

            try
            {
                if (!command.idcamioncompleto.HasValue)
                    _CamionCompletoRepository.Add(dominio);
                _CamionCompletoRepository.Commit();


                return new InsertarActualizarCamionCompletoOutput() { idcamioncompleto = dominio.idcamioncompleto };

            }
            catch (Exception ex)
            {
              //  _ValortablaRepository.Delete(dominio);
                //_ValortablaRepository.Commit();
                throw;
            }

        }
    }
}
