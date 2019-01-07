const btnNuevo = "#btnNuevo";
const gridlistaots = "#gridguias";
const gridlistaotspager = "#gridguiaspager";

$(document).ready(function () {
    preinicio();
});

function preinicio() {
    $("html, body").animate({ scrollTop: "100px" });
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
    });
    $("#numcp").keypress(function (event) {
        if (event.which == 13) {
            $("#btnBuscar").click();
        }
    });
    CargaListaots();
    cargarotshijas();

    $("#cantidad").keypress(function (event) {
        if (event.which == 13) {
            cargarguias();
        }
    });
}

function editarorden(id) {
    var url = UrlHelper.Action("EditarOrdenTrabajoLigeraCC", "Orden", "Seguimiento") + "?id=" + id;
    window.location.href = url;
}
function reload() {
    var numcp = $('#numcp').val();
    var fecinicio = $('#fechainicio').val();
    var fecfin = $('#fechafin').val();
    var idcliente = $('#idcliente').val();
    var idestacion = $('#idestacion').val();

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
        colNames: ['', 'O/T', 'Tipo', 'camioncompleto', 'registrorapido', 'Devolución', 'OT Asociada', 'Fec. Registro', 'Cliente', 'Tipo Transporte', 'Concepto Cobro', 'Placa', 'Destino', 'Destinatario', 'Agregar OT', 'Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '50', align: 'center', classes: "grid-col", formatter: semaforo, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'tipoot', index: 'tipoot', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: true, editable: false, name: 'camioncompleto', index: 'camioncompleto', width: '50', align: 'center', classes: "grid-col", formatter: semaforo, classes: "grid-col" },
            { key: false, hidden: true, editable: false, name: 'registrorapido', index: 'registrorapido', width: '50', align: 'center', classes: "grid-col", formatter: semaforo, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'devolucion', index: 'devolucion', width: '40', align: 'center', classes: "grid-col", formatter: conditional, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'otasociada', index: 'otasociada', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'fecharegistro', index: 'fecharegistro', width: '40', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'razonsocial', index: 'razonsocial', width: '60', align: 'left', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'tipotransporte', index: 'tipotransporte', width: '50', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'conceptocobro', index: 'conceptocobro', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'placa', index: 'placa', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '50', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '50', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '70', index: 'idordentrabajo', align: 'left', formatter: asignarot, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '40', index: 'idordentrabajo', align: 'left', formatter: displayButtonOrdenes, classes: "grid-col" }
        ],
        pager: $(pagergrilla),
        rowNum: 30,
        rowList: [30, 60, 90, 120],
        autowidth: true,
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
        editable: false,
        multiboxonly: true,
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
            jQuery("#gridcargas").jqGrid('resetSelection');
            return (true);
        }
    });
}
function semaforo(cellvalue, options, rowObject) {
    return "<span class='label label-info pull-xs-center'>" + cellvalue + "</span>";
}
function conditional(cellvalue, options, rowObject) {
    if (cellvalue == true) {
        return "<span class='label label-primary pull-xs-center'>Sí</span>";
    }
    else {
        return "<span class='label label-primary pull-xs-center'>No</span>";
    }
}

function asignarot(cellvalue, options, rowObject) {
    if (rowObject["camioncompleto"] == true) {
        return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick=\"agregarot(" + cellvalue + ");\" href='#' > <i class='fa fa-plus'></i> Agregar OTs</button>"
        + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='verotsasociadas(" + cellvalue + ");' href='#' > <i class='fa fa-search'></i> </button></div>";
    }
    else return '';
}
function displayButtonCCHijas(cellvalue, options, rowObject) {
    var botonera = "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarorden(" + cellvalue + ");' href='#' > <i class='fa fa-edit'></i> </button>";

    botonera = botonera + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='eliminarorden(" + cellvalue + ");' href='#' > <i class='fa fa-trash'></i> </button>"
    "</div>";
    return botonera;
}
function displayButtonOrdenes(cellvalue, options, rowObject) {
    if (rowObject["camioncompleto"] == true) {
        var botonera = "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarcamion(" + cellvalue + ");' href='#' > <i class='fa fa-edit'></i> </button>";
        //+  "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' title='Agregar OT' onclick=agregarot(" + cellvalue + "); href='#' > <i class='fa fa-plus'></i> </button>"
    }
    if (rowObject["registrorapido"] == true)
        var botonera = "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarregistrorapido(" + cellvalue + ");' href='#' > <i class='fa fa-edit'></i> </button>";
    if (rowObject["devolucion"] == true)
        var botonera = "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarorden(" + cellvalue + ");' href='#' > <i class='fa fa-edit'></i> </button>";

    botonera = botonera + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='eliminarorden(" + cellvalue + ");' href='#' > <i class='fa fa-trash'></i> </button>"
                         + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='generar(" + cellvalue + ");' href='#' > <i class='fa fa-bus'></i> </button>"
    "</div>";
    return botonera;
}
function agregarot(id) {
    var url = UrlHelper.Action("NuevaOrdenTrabajoLigeraCC", "Orden", "Seguimiento") + "?id=" + id;
    window.location.href = url;
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
                                      reloadhijas();
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
function editarcamion(id) {
    var url = UrlHelper.Action("EditarCamionCompleto", "Orden", "Seguimiento") + "?id=" + id;
    window.location.href = url;
}
function editarregistrorapido(id) {
    var url = UrlHelper.Action("EditarOrdenTrabajoLigera", "Orden", "Seguimiento") + "?id=" + id;
    window.location.href = url;
}
function generar(id) {
    var url = UrlHelper.Action("AutoGenerarDespacho", "Orden", "Seguimiento");
    swal({
        title: "Generación automática",
        text: "¿Está seguro que desea generar?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Generar',
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
                                      swal("¡Se ha generado correctamente!", data.msj, "success");
                                      reload();
                                  }
                                  else {
                                      swal("¡No se pudo generar!", data.msj, "error");
                                  }
                              },
                              error: function (request, status, error) {
                                  swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                              }
                          });
          }
      });
}
function verotsasociadas(cellvalue, options, rowObject) {
    $('#idcamioncompleto').val(cellvalue);
    var idcamioncompleto = $('#idcamioncompleto').val();

    var vdataurl = $("#griddetalle").data("dataurl") + "?idcamioncompleto=" + idcamioncompleto;
    $("#griddetalle").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function cargarotshijas() {
    $.jgrid.defaults.width = 200;
    $.jgrid.defaults.height = 450;

    var grilla = $("#griddetalle");
    var pagergrilla = $("#griddetallepager");

    var idcamioncompleto = $('#idcamioncompleto').val();

    var vdataurl = $("#griddetalle").data("dataurl") + "?idcamioncompleto=" + idcamioncompleto;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Camión Completo', 'OT', 'registrorapido', 'Fec. Registro', 'Cliente', 'Reintegro Tributario', 'Destino', 'Bultos', 'Peso', 'Volumen', 'Destinatario', 'Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcc', index: 'numcc', width: '50', align: 'center', classes: "grid-col", formatter: semaforo, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '50', align: 'center', classes: "grid-col", formatter: semaforo, classes: "grid-col" },
            { key: false, hidden: true, editable: false, name: 'registrorapido', index: 'registrorapido', width: '50', align: 'center', classes: "grid-col", formatter: semaforo, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'fecharegistro', index: 'fecharegistro', width: '40', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'razonsocial', index: 'razonsocial', width: '100', align: 'left', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'reintegrotributario', index: 'reintegrotributario', width: '60', align: 'center', classes: "grid-col", formatter: conditional, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'bulto', index: 'bulto', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'peso', index: 'peso', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'volumen', index: 'volumen', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '40', index: 'idordentrabajo', align: 'left', formatter: displayButtonCCHijas, classes: "grid-col" }
        ],
        pager: $(pagergrilla),
        rowNum: 30,
        rowList: [30, 60, 90, 120],
        autowidth: true,
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
        editable: false,
        multiboxonly: true,
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
            jQuery("#gridcargas").jqGrid('resetSelection');
            return (true);
        }
    });
}
function reloadhijas() {
    var idcamioncompleto = $('#idcamioncompleto').val();
    var vdataurl = $("#griddetalle").data("dataurl") + "?idcamioncompleto=" + idcamioncompleto;
    $("#griddetalle").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}