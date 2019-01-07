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

using Domain.TYS.Monitoreo;
using QueryHandlers.TYS.Seguimiento;
using System.Transactions;

namespace CommandHandlers.TYS
{
    public class InsertarActualizarOrdenTrabajoHandler : ICommandHandler<InsertarActualizarOrdenTrabajoCommand>
    {
        private readonly IRepository<OrdenTrabajo> _OrdenTrabajoRepository;
        private readonly IRepository<Cliente> _ClienteReporitory;
        private readonly IRepository<Direccion> _DireccionRepository;

        public InsertarActualizarOrdenTrabajoHandler(IRepository<OrdenTrabajo> pOrdenTrabajoRepository,
             IRepository<Cliente> pClienteRepository, IRepository<Direccion> pDireccionRepository)
        {
            this._OrdenTrabajoRepository = pOrdenTrabajoRepository;
            this._ClienteReporitory = pClienteRepository;
            this._DireccionRepository = pDireccionRepository;
            

        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarOrdenTrabajoCommand command)
        {
            OrdenTrabajo dominio = null;
            if (command.idordentrabajo.HasValue)
                dominio = _OrdenTrabajoRepository.Get(x => x.idordentrabajo == command.idordentrabajo).LastOrDefault();
            else
                dominio = new OrdenTrabajo();

            dominio.activo = command.activo;

            //using (TransactionScope trans = new TransactionScope())
            //{

            switch (command._tipoop)
            {
                case 1:

                    if (command.iddestinatario == 0)
                    {
                        //Agregar Cliente nuevo
                        Cliente modCliente = new Cliente();
                        modCliente.activo = true;
                        modCliente.razonsocial = command.razonsocialdestinatario;
                        modCliente.ruc = command.rucdestinatario;
                        modCliente.pagocontado = true;
                        modCliente.idmonedalinea = 70;

                        _ClienteReporitory.Add(modCliente);
                        _ClienteReporitory.SaveChanges();

                        command.iddestinatario = modCliente.idcliente;

                        //Agregar Direccion Nueva nuevo
                        Direccion modDireccion = new Direccion();
                        modDireccion.idcliente = command.iddestinatario;
                        modDireccion.direccion = command.direcciondestinatario;
                        modDireccion.iddistrito = command.iddestino;
                        modDireccion.principal = true;
                        modDireccion.activo = true;

                        _DireccionRepository.Add(modDireccion);
                        _DireccionRepository.SaveChanges();

                        dominio.iddestinatariodireccion = modDireccion.iddireccion;
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

                    dominio.idtarifa = command.idtarifa;
                    dominio.dnipersonarecojo = command.dnipersonarecojo;
                    dominio.idcliente = command.idcliente;
                    dominio.idclientetipounidad = command.idclientetipounidad;
                    dominio.idformula = command.idformula;
                    dominio.idconceptocobro = command.idconceptocobro;
                    dominio.iddestinatario = command.iddestinatario;
                    dominio.idusuarioregistro = command.idusuarioregistro;
                    dominio.idorigen = command.idorigen;
                    dominio.iddestino = command.iddestino;
                    dominio.identregara = command.identregara;
                    dominio.idestacionorigen = command.idestacionorigen;
                    dominio.idremitente = command.idremitente;
                    dominio.idremitentedireccion = command.idremitentedireccion;
                    dominio.idtipomercaderia = command.idtipomercaderia;
                    dominio.idtipotransporte = command.idtipotransporte;
                    dominio.personarecojo = command.personarecojo;

                    dominio.guiarecojo = command.guiarecojo;
                    dominio.guiatercero = command.guiatercero;
                    dominio.idvehiculo = command.idvehiculo;
                    dominio.peso = command.peso;
                    dominio.volumen = command.volumen;
                    dominio.bulto = command.bulto;
                    dominio.docgeneral = command.docgeneral;
                    dominio.iddescripciongeneral = command.descripciongeneral;
                    dominio.total = command.total;
                    dominio.subtotal = command.subtotal;
                    dominio.igv = command.igv;
                    dominio.dni = command.dni;
                    dominio.placa = command.placa;
                    dominio.chofer = command.chofer;
                    dominio.conceptocobro = command.conceptocobro;
                    dominio.puntopartida = command.puntopartida;
                    dominio.cepan = command.cepan;
                    dominio.esembarque = command.esembarque;
                    dominio.base1 = command.base1;
                    dominio.tarifa = command.tarifa;
                    dominio.minimo = command.minino;
                    dominio.pesovol = command.pesovol;
                    dominio.activo = command.activo;
                    dominio.lineaconsumida = command.subtotal;
                    dominio.fecharecojo = command.fecharecojo;
                    dominio.reintegrotributario = command.reintegrotributario;
                    dominio.registrorapido = command.registrorapido;

                    if (!command.idordentrabajo.HasValue)
                    {
                        dominio.idestado = command.idestado;
                        dominio.fecharegistro = command.fecharegistro;
                        dominio.idmanifiesto = null;
                        dominio.idcamioncompleto = command.idcamioncompleto;
                        dominio.facturado = false;
                        dominio.numcp = obtenerUltimaOT();
                    }

                    dominio.subtotalfinal = command.subtotal;

                    break;

                case 2:

                    dominio.idcarga = command.idcarga;
                    dominio.idestado = command.idestado;
                    dominio.idtipooperacion = command.idtipooperacion;
                    dominio.idruta = command.idruta;
                    dominio.idestaciondestino = command.idestaciondestino;
                    dominio.idagencia = command.idagencia;
                    dominio.fechaplanificacion = command.fechaplanificacion;

                    break;

                case 3:
                    dominio.idmanifiesto = command.idmanifiesto;
                    dominio.iddespacho = command.iddespacho;
                    dominio.idestado = command.idestado;
                    dominio.idcarga = command.idcarga;
                    dominio.idtipooperacion = command.idtipooperacion;

                    break;

                case 4:
                    dominio.activo = command.activo;
                    break;

                case 5: // anular carga
                    dominio.idcarga = command.idcarga;
                    dominio.idestado = command.idestado;
                    dominio.idtipooperacion = command.idtipooperacion;
                    dominio.idruta = command.idruta;
                    dominio.idagencia = command.idagencia;
                    dominio.idmanifiesto = command.idmanifiesto;
                    dominio.idcamioncompleto = command.idcamioncompleto;
                    dominio.iddespacho = command.iddespacho;
                    dominio.activo = dominio.activo;

                    break;

                case 6: //Confirmar
                    dominio.fechaconfirmacion = command.fechaconfirmacion;
                    dominio.idestado = command.idestado;
                    dominio.activo = command.activo;
                    break;

                case 7: //Salida de Vehiculo
                    dominio.fechadespacho = command.fechadespacho;
                    dominio.idestado = command.idestado;
                    dominio.activo = command.activo;
                    break;

                case 8: //Confirmar Entrega
                    dominio.fechaentrega = command.fechaentrega;
                    dominio.idestado = command.idestado;
                    dominio.idusuarioentrega = command.idusuarioentrega;
                    break;

                case 9: //Confirmar Recibo
                    dominio.idestado = command.idestado;
                    dominio.iddespacho = command.iddespacho;
                    dominio.idmanifiesto = command.idmanifiesto;
                    dominio.idcarga = command.idcarga;
                    dominio.activo = command.activo;
                    dominio.idestacionorigen = command.idestacionorigen;
                    break;
                case 10:



                default:
                    break;

            }

                try
                {


                    if (!command.idordentrabajo.HasValue)
                        _OrdenTrabajoRepository.Add(dominio);
                        _OrdenTrabajoRepository.Commit();
                        //trans.Complete();  

                        return new InsertarActualizarOrdenTrabajoOutput() { idordentrabajo = dominio.idordentrabajo };
                }
                catch (Exception ex)
                {
                    //  _ValortablaRepository.Delete(dominio);
                    //_ValortablaRepository.Commit();
                    throw;
                }
            
        }

        public string obtenerUltimaOT()
        {
            var query = new ObtenerUltimaOrdenTrabajoQuery();
            var queryresult = query.Handle(new ObtenerUltimaOrdenTrabajoParameter() { });
            var model = (ObtenerUltimaOrdenTrabajoResult)queryresult;
            return model.numcp.Split('-')[0].ToString() + "-" + (Convert.ToInt32(model.numcp.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');
        }
    }
}