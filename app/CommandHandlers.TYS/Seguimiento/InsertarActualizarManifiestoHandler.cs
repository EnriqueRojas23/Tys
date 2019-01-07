

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarManifiestoHandler : ICommandHandler<InsertarActualizarManifiestoCommand>
    {
        private readonly IRepository<Manifiesto> _ManifiestoRepository;


        public InsertarActualizarManifiestoHandler(IRepository<Manifiesto> pManifiestoRepository)
        {
            this._ManifiestoRepository = pManifiestoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarManifiestoCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");




            Manifiesto dominio = null;
            if (command.idmanifiesto.HasValue)
                dominio = _ManifiestoRepository.Get(x => x.idmanifiesto == command.idmanifiesto).LastOrDefault();
            else
                dominio = new Manifiesto();

            switch (command._tipoop)
            {
                case 1: 
                    dominio.activo =  command.activo;
                    dominio.fecharegistro = command.fecharegistro;
                    dominio.iddespacho = command.iddespacho;
                    dominio.nummanifiesto = command.nummanifiesto;
                    dominio.idusuarioregistro = command.idusuarioregistro;
                    dominio.numhojaruta = command.numhojaruta;
                    dominio.idvehiculo = command.idvehiculo;
                    dominio.idtipooperacion = command.idtipooperacion;
                    dominio.iddestino = command.iddestino;
                    
                    break;
                case 2:
                    dominio.activo = command.activo;
                    break;
                default:
                    break;
            }
               
                            
       



            try
            {
                if (!command.idmanifiesto.HasValue)
                    _ManifiestoRepository.Add(dominio);
                _ManifiestoRepository.Commit();


                return new InsertarActualizarManifiestoOutput() {  idmanifiesto = dominio.idmanifiesto };

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
