const btnNuevo = "#btnNuevo";
const gridlistaots = "#gridguias";
const gridlistaotspager = "#gridguiaspager";
var editOptionsNew = {
    keys: true,
    successfunc: function () {
        var $self = $(this);
        setTimeout(function () {
            $self.trigger("reloadGrid");
        }, 50);
    }
};

$(document).ready(function () {

    $("html, body").animate({ scrollTop: "100px" });
    inicializandoEventosModalDocumentos();

    $('#dtpfechaini .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd/mm/yyyy'
    });

    $('#dtpfechafin .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd/mm/yyyy'
    });

    $('#btnBuscar').click(function () {
        reload();
    })
    $("#numcp").keypress(function (event) {
        if (event.which == 13) {
            $("#btnBuscar").click();
        }
    });
    CargaListaots();
    $("#cantidad").keypress(function (event) {
        if (event.which == 13) {
            cargarguias();
        }
    });
});

function editarorden(id) {
    var url = UrlHelper.Action("EditarOrdenTrabajo", "Orden", "Seguimiento") + "?id=" + id;
    window.location.href = url;
}
function reload() {
    var numcp = $('#numcp').val();
    var fecinicio = $('#fechainicio').val();
    var fecfin = $('#fechafin').val();
    var idcliente = $('#idcliente').val();
    var idestacion = $('#idestacion').val();

    if (fecinicio == '' || fecfin == '') {
        swal("OT", "Debe ingresar un rango de fechas", "warning")
        return
    }

    var vdataurl = $("#gridordenes").data("dataurl") + "?numcp=" + numcp + "&fecinicio=" + fecinicio + "&fecfin=" + fecfin + "&idcliente=" + idcliente + "&idestacion=" + idestacion;
    $("#gridordenes").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function inicializandoEventosModalDocumentos() {
    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-results': { no_results_text: 'Oops, no se encontró el ubigeo!' }
    }

    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }

    ChangeComboProveedor();
}
function ChangeComboProveedor() {
    $("#idproveedor").chosen().change(function () {
    })
}

function bottonasignarrol_formatter(cellvalue, options, rowObject) {
    var editar = "<button type='button' title='Editar' class='btn btn-success btn-xs btn-outline' onclick=\"$('#gridguias').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
    var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridguias').saveRow('" + options.rowId + "', successfunc)\";><i class='fa fa-save'></i> </button>";
    var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
    return editar + guardar + control;
}

function formatedit(cellvalue, options, rowObject) {
    if (cellvalue == null)
        return "";
    else
        return " " + cellvalue;
}

function CargaListaots() {
    $.jgrid.defaults.width = 200;
    $.jgrid.defaults.height = 450;

    var grilla = $("#gridordenes");
    var pagergrilla = $("#gridordenespager");

    var numcp = $('#numcp').val();
    var fecinicio = $('#fechainicio').val();
    var fecfin = $('#fechafin').val();
    var idcliente = $('#idcliente').val();
    var idestacion = $('#idestacion').val();

    var vdataurl = $("#gridordenes").data("dataurl") + "?numcp=" + numcp + "&fecinicio=" + fecinicio + "&fecfin=" + fecfin + "&idcliente=" + idcliente + "&idestacion=" + idestacion;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'O/T', 'Fec. Registro', 'Cliente', 'T.Transporte', 'Concepto Cobro', 'Destino', 'Remitente', 'Destinatario', 'idpreliquidacion', 'Files', '', 'Generar', 'Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '40', align: 'center', classes: "grid-col", formatter: semaforo, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'fecharegistro', index: 'fecharegistro', width: '40', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'razonsocial', index: 'razonsocial', width: '90', align: 'left', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'tipotransporte', index: 'tipotransporte', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'conceptocobro', index: 'conceptocobro', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'remitente', index: 'remitente', width: '60', align: 'left', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '60', align: 'left', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: true,  editable: false, name: 'idpreliquidacion', index: 'idpreliquidacion', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: true,  editable: false, name: 'facturado', index: 'facturado', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '20', index: 'idordentrabajo', align: 'center', formatter: displayButtonFiles, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '60', index: 'idordentrabajo', align: 'center', formatter: displayButton, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '60', index: 'idordentrabajo', align: 'center', formatter: displayButtonOrdenes, classes: "grid-col" }
        ],
        pager: $(pagergrilla),
        rowNum: 30,
        rowList: [30, 60, 90, 120],
        autowidth: true,
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
        editable: false,

        jsonReader:
        {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: 0
        },
        loadComplete: function (data) {
            // setStyleCheckBoxGrid(this);

            // for (var i = 0; i < idsOfSelectedRows.length; i++){
            //       $("#gridcargas").setSelection(idsOfSelectedRows[i], true);
            // }
            // var numerofilas = $(this).getGridParam("records");
            //
        },
        beforeSelectRow: function (rowid, e) {
            jQuery("#gridordenes").jqGrid('resetSelection');
            return (true);
        }
    });

    $(grilla).setGridParam({ sortname: 'fecharegistro', sortorder: 'desc' }).trigger('reloadGrid');
}
function semaforo(cellvalue, options, rowObject) {
    return "<span class='label label-info pull-xs-center'>" + cellvalue + "</span>";
}
function displayButton(cellvalue, options, rowObject) {

    if (rowObject["idpreliquidacion"] != 0) {
        if (rowObject["facturado"] == false) {
            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='renegerarpago(" + rowObject["idpreliquidacion"] + ");' href='#' > Factura </button>" +
                   "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarboleta(" + rowObject["idpreliquidacion"] + ");' href='#' > Boleta </button></div>";
        }
        else
            return "";
    }
    else return "";

}
function editarboleta(idpreliquidacion) {
    var url = UrlHelper.Action("PreBoleta", "Orden", "Seguimiento") + "?idpreliquidacion=" + idpreliquidacion;
    var url2 = UrlHelper.Action("JsonGenerarBoleta", "Orden", "Seguimiento");
    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal('show');

        configurarGrillaDetalle()

        $("#addrow").click(function () {
            $("#griddetallefactura").jqGrid('addRowData', 0, 1, "last");
            $("#griddetallefactura").editRow(0, true);
        });


        $("#idnumerodocumento").change(function () {
            var url3 = UrlHelper.Action("JsonObtenerNumeroDocumento", "Orden", "Seguimiento") + "?id=" + $("#idnumerodocumento").val();

            $.ajax({
                url: url3,
                type: 'POST',
                dataType: 'json',
                data: {}
            })
         .done(function (data) {
             $("#numerocomprobante").val(data.next);
         })
         .fail(function () {
             console.log("error");
         })
        });

        $('#dtpfechacomp .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });





        $("#txtdescripcion").val($("#descripcion").val());
        $("#btnGenBoleta").click(function (event) {
            var id = $("#idpreliquidacion").val();

            $.ajax({
                url: url2,
                type: 'POST',
                dataType: 'json',
                data: {
                    idpreliquidacion: $("#idpreliquidacion").val()
                    , subtotal: $("#strsubtotal").val()
                    , igv: $("#strigv").val()
                    , __descripcion: $("#txtdescripcion").val()
                    , fecha: $("#fechaemision").val()
                    , numerodocumento: $("#idnumerodocumento").val()
                    , ordencompra : $("#ordencompra").val()
                }
            })
            .done(function (data) {
                if (data.res) {
                    var id = data.idcomprobante;
                    $("#modalcontainerL").modal("hide");
                    reload();

                     var url = "http://104.36.166.65/webreports/facturaelectronica.aspx?idcomprobante=" + String(id) + "&valorigv=" + data.valorigv;
                     window.open(url);
                }
                else swal("No puede continuar", data.msj, "warning");
            })
            .fail(function () {
                console.log("error");
            })
        });
        $(".editor").jqte({
        });
    });
}
function displayButtonOrdenes(cellvalue, options, rowObject) {
    return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarorden(" + cellvalue + ");' href='#' > <i class='fa fa-edit'></i> </button>"
                           + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='eliminarorden(" + cellvalue + ");' href='#' > <i class='fa fa-trash'></i> </button>"
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='copiarorden(" + cellvalue + ");' href='#' > <i class='fa fa-copy'></i> </button>"
                        + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='imprimirot(" + cellvalue + ");' href='#' > <i class='fa fa-print'></i> </button>"
                       + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='renegerargrt(" + cellvalue + ");' href='#' > <i class='fa fa-telegram'></i> </button></div>";
}
function displayButtonFiles(cellvalue, options, rowObject) {
    return "<button type='button'  data-toggle='tooltip' data-placement='top' title='Ver Archivos'  class='btn btn-primary btn-xs btn-outline'  onclick='verarchivos(" + cellvalue + ");' href='#' > <i class='fa fa-cloud-upload'></i></button>";
}
function verarchivos(id) {
    var vUrl = UrlHelper.Action("ListarArchivos", "Liquidacion", "Liquidacion") + "?idorden=" + id;

    $.get(vUrl, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        cargargrillaarchivos(id);
    });
}
function cargargrillaarchivos(id) {
    $.jgrid.defaults.responsive = true;
    $.jgrid.defaults.height = 220;

    var grilla = $("#gridarchivos");
    var pagergrilla = $("#gridarchivospager");

    var vdataurl = $(grilla).data("dataurl") + "?idorden=" + id;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Nombre de Archivo', 'Extensión', 'Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idarchivo', index: 'idarchivo', classes: "grid-col" },
            { key: false, hidden: false, name: 'nombrearchivo', index: 'nombrearchivo', classes: "grid-col" },
            { key: false, hidden: false, name: 'extension', index: 'extension', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, name: 'idarchivo', index: 'idarchivo', align: 'center', classes: "grid-col", formatter: displayButtonAnular, classes: "grid-col" }
        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        width: "100%",
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
        shrinktofit: true,
        editable: false,
        jsonReader:
        {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: 0
        },
    });
}

function eliminarorden(id) {
    var url = UrlHelper.Action("EliminarOrden", "Orden", "Seguimiento");
    swal({
        title: "Eliminar Orden de Transporte",
        text: "¿Está seguro que desea eliminar esta Orden de Transporte?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Eliminar',
        closeOnConfirm: false,
        closeOnCancel: true
    },
      function (isConfirm) {
          if (isConfirm) {
              $.ajax(
                          {
                              type: "POST",
                              async: true,
                              url: url,
                              data: { "id": id },
                              success: function (data) {
                                  if (data.res) {
                                      swal("¡Se ha eliminado correctamente!", data.msj, "success");
                                      reload();
                                  }
                                  else {
                                      swal("¡No se pudo eliminar!", data.msj, "error");
                                  }
                              },
                              error: function (request, status, error) {
                                  swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                              }
                          });
          }
      });
}
function copiarorden(id) {
    var url = UrlHelper.Action("CopiarOrdenTrabajo", "Orden", "Seguimiento") + "?id=" + id;
    window.location.href = url;
}

function imprimirot(id) {
    var url = "http://104.36.166.65/webreports/ot.aspx?idorden=" + String(id);
    window.open(url);
}
function renegerargrt(ot) {
    var url = UrlHelper.Action("JsongenerarguiatransportistaxOT", "Seguimiento", "Seguimiento");
    swal({
        title: "Reimprimir la GRT",
        text: "Ingrese el número de GRT",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder: "Número de GRT"
    },
      function (inputValue) {
          if (inputValue === false) return false;

          var re = /^(-?\d*\.?\d)-(-?\d*\.?\d)$/i;
          if (re.test(inputValue) == false) {
              swal.showInputError("Debe ingresar un número de Guia correcto!");
              return false;
          }

          if (inputValue === "") {
              swal.showInputError("Debe ingresar un número de Guia.");
              return false;
          }
          else {
              $.ajax({
                  url: url,
                  type: "post",
                  async: true,
                  datatype: "json",
                  data: { "guia": inputValue, "idorden": ot },
                  success: function (data) {
                      if (data.res) {
                          var url = "http://104.36.166.65/webreports/guiatransportistaxot.aspx?idorden=" + String(ot);
                          window.open(url);

                          swal("¡Se ha generado correctamente!", "Nros de Guia Transportista generado: " + data.guias, "success");
                      }
                      else {
                          swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
                      }
                  },
                  error: function (data) {
                      alert(data.res);
                  }
              });
              swal("Registró el número de GRT", "Nro de GRT ingresado: " + inputValue, "success");
          }
      });
}

function renegerarpago(idpreliquidacion) {


    var url = UrlHelper.Action("PreFactura", "Orden", "Seguimiento") + "?idpreliquidacion=" + idpreliquidacion;
    var url2 = UrlHelper.Action("JsonGenerarComprobante", "Orden", "Seguimiento");

    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal('show');
        configurarGrillaDetalle();
        $("#strsubtotal").blur(function () {
            $("#strigv").val(($("#strsubtotal").val() * 0.18).toFixed(2));
            $("#strtotal").val((parseFloat($("#strsubtotal").val()) + parseFloat($("#strigv").val())).toFixed(2));
        })


        $('#dtpfechacomp .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        $("#idnumerodocumento").change(function () {
            var url3 = UrlHelper.Action("JsonObtenerNumeroDocumento", "Facturacion", "Facturacion") + "?id=" + $("#idnumerodocumento").val();
            $.ajax({
                url: url3,
                type: 'POST',
                dataType: 'json',
                data: {}
            }).done(function (data) {
                $("#numerocomprobante").val(data.next);
            }).fail(function () {
                console.log("error");
            });
        });
        $("#txtdescripcion").val($("#descripcion").val());
        $("#btnGenFactura").click(function (event) {
            var id = $("#idpreliquidacion").val();

            // url2 = url2 + "?idpreliquidacion=" + $("#idpreliquidacion").val()
            // + "&subtotal=" + $("#strsubtotal").val()
            // + "&igv=" + $("#strigv").val()
            // +"&__descripcion=" + $("#txtdescripcion").val()

            if ($("#numerocomprobante").val() == "") {
                swal("Facturación", "No puede continuar, debe seleccionar la serie", "warning");
                return;
            }

            $.ajax({
                url: url2,
                type: 'POST',
                dataType: 'json',
                data: {
                 //    "idpreliquidacion": $("#idpreliquidacion").val()
                 // , "__descripcion": $("#txtdescripcion").val()
                 // , "igv": $("#strigv").val()
                 // , "subtotal": $("#strsubtotal").val()
                 // , "fecha": $("#fechaemision").val()
                 //  , "numerodocumento": $("#numerocomprobante").val()
                 //  , ordencompra : $("#ordencompra").val()


                  idpreliquidacion: $("#idpreliquidacion").val()
                  , subtotal: $("#strsubtotal").val()
                  , igv: $("#strigv").val()
                  , __descripcion: $("#txtdescripcion").val()
                  , fecha: $("#fechaemision").val()
                  , numerodocumento: $("#idnumerodocumento").val()
                  , ordencompra : $("#ordencompra").val()

                }
            })
            .done(function (data) {
                var id = data.idcomprobante;
                $("#modalcontainerL").modal("hide");
                reload();
                var url = "http://104.36.166.65/webreports/factura.aspx?idcomprobante=" + String(id) + "&valorigv=" + data.valorigv;
                window.open(url);
            })
            .fail(function () {
                console.log("error");
            })
        });
        $(".editor").jqte({
        });
    });
}
function editarcomprobante(idpreliquidacion) {
}
function displayButtonAnular(cellvalue, options, rowObject) {
    return "<div class='btn-group'><button type='button' class='btn-primary btn btn-xs btn-outline' onclick='downloadFile(" + cellvalue + ");return false;' href='#' > Descargar  <i class='fa fa-download'></i></button>";
}
function configurarGrillaDetalle() {

    var grilladetalle = $("#griddetallefactura");
    var pagergrilladetalle = $("#griddetallefacturapager");

    $.jgrid.defaults.width = 1000;
    $.jgrid.defaults.height = 100;

    var idpreliquidacion = $("#idpreliquidacion").val();

    var vdataedit = $(grilladetalle).data("editurl");
    var url = UrlHelper.Action("GetListarDetalleFacturacion",  "Orden", "Seguimiento") + "?idpreliquidacion=" + idpreliquidacion;


    $(grilladetalle).jqGrid({
        url: url,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Cantidad', 'Unidad Medida', 'Descripción', 'Recargo' , 'Valor Unitario', '', 'Acciones'],
        colModel: [
           { key: true, hidden: true, name: 'iddetallecomprobante', index: 'iddetallecomprobante', classes: "grid-col" },
           { key: false, hidden: false, editable: false, name: 'cantidad', index: 'cantidad', width: '60', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'unidadmedida', index: 'unidadmedida', width: '60', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, editrules: { maxlength: 250, required: true }, name: 'descripcion', index: 'descripcion', width: '390', align: 'center', classes: "grid-col", formatter: formatedit },
            {
                key: false, hidden: false, editable: true, editrules: { custom_func: mypricecheck, custom: true, }, name: 'recargo', index: 'recargo', width: '90', align: 'center', classes: "grid-col", formatter: 'number', formatoptions: { decimalPlaces: 2 }
                 , editoptions: {
                     dataInit: function (element) {
                         $(element).keypress(function (e) {
                             return validateFloatKeyPress(this, e);
                         });
                     }
                 }

            },
           {
               key: false, hidden: false, editable: false, editrules: { custom_func: mypricecheck, custom: true, }, name: 'subtotal', index: 'subtotal', width: '90', align: 'center', classes: "grid-col", formatter: 'number', formatoptions: { decimalPlaces: 2 }
                 , editoptions: {
                     dataInit: function (element) {
                         $(element).keypress(function (e) {
                             var resp = validateFloatKeyPress(this, e);
                             if (resp == false)
                                 return false;
                             else return true;

                         });
                     }
                 }

           },
           { key: false, hidden: true, editable: false, name: 'idcomprobantepago', index: 'idcomprobantepago', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'iddetallecomprobante', width: '100', index: 'iddetallecomprobante', align: 'center', formatter: displayButtons2, classes: "grid-col" }
             //{ key: false, hidden: false, editable: false ,name: 'idpreliquidacion', width:'100' , index: 'idpreliquidacion' ,  formatter:  displayButtons2,classes:"grid-col"}
        ],
        pager: $(pagergrilladetalle),
        rowNum: 30,
        rowList: [30, 60, 90],
        autoResizing: { compact: true },
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,
        autoheight: true,
        multiselect: false,
        editable: true,
        addParams: {
            position: "last",
            addRowParams: editOptionsNew
        },
        editParams: editOptionsNew,
        editurl: vdataedit,
        jsonReader:
        {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: 0
        },
        beforeSelectRow: function (rowid, e) {
            $(grilladetalle).jqGrid('resetSelection');
            return (true);
        },
        loadComplete: function () {
            sumarTotales()
        }
    });


}
function mypricecheck(value, colname) {
    var re = /^[a-zA-Z]/
    var isValid = (value.match(re) !== null);


    if (isValid)
        return [false, "Solo se aceptan números"];
    else
        return [true, ""];
}

function displayButtons2(cellvalue, options, rowObject) {


    var editar = "<button type='button' title='Editar' class='btn btn-success btn-xs btn-outline' onclick=\"$('#griddetallefactura').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
    var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#griddetallefactura').saveRow('" + options.rowId + "') ;      reload2();        \";><i class='fa fa-save'></i> </button>";
    //var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
    var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#griddetallefactura').restoreRow('" + options.rowId + "'); reload2(); \"><i class='fa fa-times-circle'></i> </button>";

    return "<div class='btn-group'>" + editar + guardar + restore + "</div>";
}

function reload2() {



    var idpreliquidacion = $("#idpreliquidacion").val();
    var url = UrlHelper.Action("GetListarDetalleFacturacion", "Orden", "Seguimiento"    ) + "?idpreliquidacion=" + idpreliquidacion;
    $('#griddetallefactura').jqGrid('setGridParam', { url: url }).trigger('reloadGrid');


}
function sumarTotales() {


    let colSum = $('#griddetallefactura').jqGrid('getCol', 'subtotal', false, 'sum');

    let subtotal = numberWithCommas(colSum);
    let igv = colSum * 0.18;
    let total = colSum + igv;


    $("#strsubtotal").val(numberWithCommas(colSum));
    $("#strigv").val(numberWithCommas(igv));
    $("#strtotal").val(numberWithCommas(total));






}




function irEliminar(id) {
    let URL_EliminarDetalle = UrlHelper.Action("EliminarDetalleFactura", "Facturacion", "Facturacion")



    $.ajax({
        url: URL_EliminarDetalle,
        type: 'POST',
        dataType: 'json',
        data: { id: id },
    })
    .done(function () {
        reload2()
    })
    .fail(function () {
        console.log("error");
    })






}
