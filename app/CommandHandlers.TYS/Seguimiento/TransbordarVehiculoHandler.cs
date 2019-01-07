

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class TransbordarVehiculoHandler : ICommandHandler<TransbordarVehiculoCommand>
    {
        private readonly IRepository<OperacionCarga> _OperacionCargaRepository;
        private readonly IRepository<Despacho> _DespachoRepository;
        private readonly IRepository<Vehiculo> _VehiculoRepository;


        public TransbordarVehiculoHandler(IRepository<OperacionCarga> pOperacionCargaRepository,
           IRepository<Despacho> pDespachoRepository, IRepository<Vehiculo> pVehiculoRepository)
        {
            this._OperacionCargaRepository = pOperacionCargaRepository;
            this._DespachoRepository = pDespachoRepository;
            this._VehiculoRepository = pVehiculoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(TransbordarVehiculoCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que seleccionar un vehículo");

            
            //Actualiza la operacion carga con el nuevo vehiculo
            List<OperacionCarga> operacioncarga = null;
            operacioncarga = _OperacionCargaRepository.Get(x => x.idvehiculo == command.idvehiculo_old 
                && x.idestado == 15).ToList();
            
            foreach (var item in operacioncarga)
                item.idvehiculo = command.idvehiculo_new;

            //Actualiza el despacho carga con el nuevo vehiculo
            Despacho despacho = null;
            despacho = _DespachoRepository.Get(x => x.idvehiculo.Equals(command.idvehiculo_old)
                && x.idestado == 16).LastOrDefault();
            if(despacho!=null)
            despacho.idvehiculo = command.idvehiculo_new;



            //Actualiza el vehiculo a su estado creado
            Vehiculo vehiculo = null;
            vehiculo = _VehiculoRepository.Get(x => x.idvehiculo.Equals(command.idvehiculo_old)).LastOrDefault();
            vehiculo.idestado = 1; 


            try
            {
                _OperacionCargaRepository.SaveChanges();
                _DespachoRepository.SaveChanges();
                _VehiculoRepository.SaveChanges();

                return new TransbordarVehiculoOutput() {  idvehiculo = command.idvehiculo_new };

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
