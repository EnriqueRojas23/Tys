using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CommandHandlers.TYS.Seguimiento
{
    using CommandContracts.TYS.Seguimiento;
    using QueryContracts.TYS.Seguimiento.Parameters;
    using QueryContracts.TYS.Seguimiento.Results;

    public class InsertarActualizarOTLigeraHandler : ICommandHandler<InsertarActualizarOTLigeraCommand>
    {
        private readonly IRepository<OrdenTrabajo> _OrdenTrabajoRepository;
        private readonly IRepository<Cliente> _ClienteReporitory;
        private readonly IRepository<Direccion> _DireccionRepository;

        public InsertarActualizarOTLigeraHandler(IRepository<OrdenTrabajo> pOrdenTrabajoRepository,
                         IRepository<Cliente> pClienteRepository, IRepository<Direccion> pDireccionRepository)
        {
            this._OrdenTrabajoRepository = pOrdenTrabajoRepository;
            this._ClienteReporitory = pClienteRepository;
            this._DireccionRepository = pDireccionRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarOTLigeraCommand command)
        {
            OrdenTrabajo dominio = null;
            if (command.idordentrabajo.HasValue)
                dominio = _OrdenTrabajoRepository.Get(x => x.idordentrabajo == command.idordentrabajo).LastOrDefault();
            else
                dominio = new OrdenTrabajo();

            if (!command.camioncompleto)
            {
                if (command.iddestinatario == 0)
                {
                    //Agregar Cliente nuevo
                    Cliente modCliente = new Cliente();
                    modCliente.activo = true;
                    modCliente.razonsocial = command.razonsocialdestinatario;
                    modCliente.ruc = command.rucdestinatario;

                    _ClienteReporitory.Add(modCliente);
                    _ClienteReporitory.SaveChanges();

                    command.iddestinatario = modCliente.idcliente;

                    //Agregar Direccion Nueva nuevo
                    Direccion modDireccion = new Direccion();
                    modDireccion.idcliente = command.iddestinatario;
                    modDireccion.direccion = command.direcciondestinatario;
                    modDireccion.iddistrito = command.iddestino;
                    modDireccion.principal = false;
                    modDireccion.activo = true;

                    _DireccionRepository.Add(modDireccion);
                    _DireccionRepository.SaveChanges();

                    command.iddestinatariodireccion = modDireccion.iddireccion;
                }
                else
                {
                    //validar si direccion existe.

                    var direccion = _DireccionRepository.Get(x => x.direccion.Equals(command.direcciondestinatario)
                         && x.idcliente.Equals(command.iddestinatario)).SingleOrDefault();

                    if (direccion != null)
                        dominio.iddestinatariodireccion = direccion.iddireccion;
                    else
                    {
                        Direccion modDireccion = new Direccion();
                        modDireccion.idcliente = command.iddestinatario;
                        modDireccion.direccion = command.direcciondestinatario;
                        modDireccion.iddistrito = command.iddestino;
                        modDireccion.principal = false;
                        modDireccion.activo = true;

                        _DireccionRepository.Add(modDireccion);
                        _DireccionRepository.SaveChanges();

                        dominio.iddestinatariodireccion = modDireccion.iddireccion;
                    }
                }
            }

            dominio.activo = command.activo;
            dominio.idcliente = command.idcliente;
            dominio.idformula = command.idformula;
            dominio.idconceptocobro = command.idconceptocobro;
            dominio.iddestinatario = command.iddestinatario;
            dominio.idremitente = command.idremitente;
            dominio.idusuarioregistro = command.idusuarioregistro;
            dominio.idorigen = command.idorigen;
            dominio.iddestino = command.iddestino;
            dominio.idtipotransporte = command.idtipotransporte;
            dominio.numcp = command.numcp;
            dominio.idvehiculo = command.idvehiculo;
            dominio.peso = command.peso;
            dominio.volumen = command.volumen;
            dominio.bulto = command.bulto;
            dominio.iddescripciongeneral = command.descripciongeneral;
            dominio.total = command.total;
            dominio.subtotal = command.subtotal;
            dominio.igv = command.igv;
            dominio.idestado = command.idestado;
            dominio.idcamioncompleto = command.idcamioncompleto;
            dominio.fecharegistro = command.fecharegistro;
            dominio.base1 = command.base1;
            dominio.tarifa = command.tarifa;
            dominio.minimo = command.minino;
            dominio.pesovol = command.pesovol;
            dominio.activo = command.activo;
            dominio.lineaconsumida = command.subtotal;
            dominio.reintegrotributario = command.reintegrotributario;
            dominio.nofacturable = command.nofacturable;
            dominio.registrorapido = command.registrorapido;
            dominio.idruta = command.idruta;
            dominio.guiatransportista = command.guiatransportista;
            dominio.devolucion = command.devolucion;
            dominio.camioncompleto = command.camioncompleto;
            dominio.idclientetipounidad = command.idclientetipounidad;
            dominio.subtotalfinal = command.subtotal;

            try
            {
                if (!command.idordentrabajo.HasValue)
                    _OrdenTrabajoRepository.Add(dominio);
                _OrdenTrabajoRepository.Commit();

                return new InsertarActualizarOrdenTrabajoOutput() { idordentrabajo = dominio.idordentrabajo };
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