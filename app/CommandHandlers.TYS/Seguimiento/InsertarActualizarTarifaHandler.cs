

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarTarifaHandler : ICommandHandler<InsertarActualizarTarifaCommand>
    {
        private readonly IRepository<Tarifa> _TarifaRepository;
        private readonly IRepository<DetalleTarifa> _DetalleTarifaRepository;


        public InsertarActualizarTarifaHandler(IRepository<Tarifa> pTarifaRepository, IRepository<DetalleTarifa> pDetalleTarifaRepository)
        {
            this._TarifaRepository = pTarifaRepository;
            this._DetalleTarifaRepository = pDetalleTarifaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarTarifaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            //eliminandodetalletarifa
            var all = _DetalleTarifaRepository.Get(x => x.idtarifa == command.idtarifa).ToList();
            foreach (var item in all)
            {
                _DetalleTarifaRepository.Delete(item);
                _DetalleTarifaRepository.Commit();

            }



            Tarifa dominio = null;
           if (command.idtarifa.HasValue)
               dominio = _TarifaRepository.Get(x => x.idtarifa == command.idtarifa).LastOrDefault();
            else
               dominio = new Tarifa();

           dominio.autoserv = command.autoserv;
           dominio.desde = command.desde;
                //dominio.idubigeo = command.idubigeo;
           dominio.hasta = command.hasta;
           dominio.idcliente = command.idcliente;
           dominio.iddistrito = command.iddistrito;
           dominio.idorigendistrito = command.idorigendistrito;
           dominio.idformula = command.idformula;
           dominio.idmoneda = command.idmoneda;
           dominio.idorigendepartamento = command.idorigen;
           dominio.idzona = command.idzona;

           dominio.idprovincia = command.idprovincia;
           dominio.idorigenprovincia = command.idorigenprovincia;
           dominio.iddepartamento = command.iddepartamento;
           dominio.idtipotransporte = command.idtipotransporte;
           dominio.idtipounidad = command.idtipounidad;
           dominio.minimo = command.minimo;
           dominio.montobase = command.montobase;
           dominio.precio = command.precio;
           dominio.urgente = command.urgente;
           dominio.val = command.val;
           dominio.adicional = command.adicional;
          


            try
            {
                if (!command.idtarifa.HasValue)
                    _TarifaRepository.Add(dominio);
                _TarifaRepository.Commit();


                return new InsertarActualizarTarifaOutput() { idtarifa = dominio.idtarifa };

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
