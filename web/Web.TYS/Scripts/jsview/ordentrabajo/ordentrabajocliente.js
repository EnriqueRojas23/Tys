const btnNuevo = "#btnNuevo";
const gridlistaots = "#gridguias";
const gridlistaotspager = "#gridguiaspager";
const $btnVolver = $("#btnVolver")

$(document).ready(function () {
    $("html, body").animate({ scrollTop: "100px" });
    inicializandoEventosModalDocumentos();
    configurarGrillaEventos()

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
    $btnVolver.click(function (event) {
        $("html, body").animate({ scrollTop: "100px" });
    })
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
    var grr = $("#grr").val()

    if (fecinicio == '' || fecfin == '') {
        swal("OT", "Debe ingresar un rango de fechas", "warning")
        return
    }

    var vdataurl = $("#gridordenes").data("dataurl") + "?numcp=" + numcp + "&fecinicio=" + fecinicio + "&fecfin=" + fecfin + "&idcliente=" + idcliente + "&grr=" + grr
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
        colNames: ['', 'O/T', 'Fec. Registro', 'Cliente', 'T.Transporte', 'Concepto Cobro', 'Destino', 'Remitente', 'Destinatario', 'idpreliquidacion', 'GRR', 'Files', 'Eventos'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '40', align: 'center', classes: "grid-col", formatter: semaforo, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'fecharegistro', index: 'fecharegistro', width: '40', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" }, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'razonsocial', index: 'razonsocial', width: '100', align: 'left', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'tipotransporte', index: 'tipotransporte', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'conceptocobro', index: 'conceptocobro', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'remitente', index: 'remitente', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: true, editable: false, name: 'idpreliquidacion', index: 'idpreliquidacion', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '30', index: 'idordentrabajo', align: 'center', formatter: displayButtonGRR, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '30', index: 'idordentrabajo', align: 'center', formatter: displayButtonFiles, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '30', index: 'idordentrabajo', align: 'center', formatter: displayButtonOrdenes, classes: "grid-col" }
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
    if (rowObject["idpreliquidacion"] != 0)
        return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='renegerarpago(" + rowObject["idpreliquidacion"] + ");' href='#' > Generar </button></div>";
    else return "";
}
//function displayButtonOrdenes(cellvalue, options, rowObject) {
//    return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarorden(" + cellvalue + ");' href='#' > <i class='fa fa-edit'></i> </button>"
//                           + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='eliminarorden(" + cellvalue + ");' href='#' > <i class='fa fa-trash'></i> </button>"
//                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='copiarorden(" + cellvalue + ");' href='#' > <i class='fa fa-copy'></i> </button>"
//                        + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='imprimirot(" + cellvalue + ");' href='#' > <i class='fa fa-print'></i> </button>"
//                       + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='renegerargrt(" + cellvalue + ");' href='#' > <i class='fa fa-telegram'></i> </button></div>";
//}
function displayButtonOrdenes(cellvalue, options, rowObject) {
    return '<div class="btn-group"><button type="button" data-toggle="tooltip" data-placement="top"  class="btn-primary btn btn-xs btn-outline" onclick="verincidencias(\'' + rowObject.numcp.toString() + '\');" href="#" > <i class="fa fa-telegram"></i> </button></div>';
}
function displayButtonGRR(cellvalue, options, rowObject) {
    return '<div class="btn-group"><button type="button" data-toggle="tooltip" data-placement="top"  class="btn-primary btn btn-xs btn-outline" onclick="vergrr('+ cellvalue +');" href="#" > <i class="fa fa-telegram"></i> </button></div>';
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

function verincidencias(id) {
    reload2(id)
    $("html, body").animate({ scrollTop: "800px" });
    
}
function vergrr(id) {
    var url = UrlHelper.Action("VerDetalleOT", "Liquidacion", "Liquidacion") + "?idorden=" + id;

    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");


    })
}
function displayButtonAnular(cellvalue, options, rowObject) {
    return "<div class='btn-group'><button type='button' class='btn-primary btn btn-xs btn-outline' onclick='downloadFile(" + cellvalue + ");return false;' href='#' > Descargar  <i class='fa fa-download'></i></button>";
}

function reload2(numcp) {
    var grilla = $("#grideventos");
    var pagergrilla = $("#grideventospager");
 
    var idmaestroincidencia = $("#idmaestroincidencia").val();
    var idmaestroetapa = $("#idmaestroetapa").val();


    var vdataurl = UrlHelper.Action("JsonGetListarEventos", "Monitoreo", "Monitoreo") + "?numcp=" + numcp
    + "&idmaestroincidencia=" + idmaestroincidencia
    + "&idmaestroetapa=" + idmaestroetapa;


    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function configurarGrillaEventos() {
    $.jgrid.defaults.width = 200;
    $.jgrid.defaults.height = 300;


    var numcp = $("#numcp").val();
    var idmaestroincidencia = $("#idmaestroincidencia").val();
    var idmaestroetapa = $("#idmaestroetapa").val();


    var url = UrlHelper.Action("JsonGetListarEventos", "Monitoreo", "Monitoreo") + "?numcp=" + numcp
    + "&idmaestroincidencia=" + idmaestroincidencia
    + "&idmaestroetapa=" + idmaestroetapa;


    var grilla = $("#grideventos");
    var pagergrilla = $("#grideventospager");

    $(grilla).jqGrid({
        url: url,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'OT', 'Usuario', 'Fecha Evento', 'Tipo', 'Evento', 'Observación', 'Recurso', 'Documento', 'Estación Origen', 'Fecha Registro'],
        colModel: [
           { key: true, hidden: true, name: 'ot', index: 'ot', classes: "grid-col" },
           { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '45', align: 'center', classes: "grid-col", formatter: formatedit },
             { key: false, hidden: false, editable: false, name: 'usuario', index: 'usuario', width: '60', align: 'left', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'fechaevento', index: 'fechaevento', width: '60', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y H:i" } },
           { key: false, hidden: false, editable: false, name: 'tipoevento', index: 'tipoevento', width: '50', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'evento', index: 'evento', width: '70', align: 'left', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'observacion', index: 'observacion', width: '160', align: 'left', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'recurso', index: 'recurso', width: '70', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'documento', index: 'documento', width: '70', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'estacionorigen', index: 'estacionorigen', width: '70', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'fecharegistro', index: 'fecharegistro', width: '70', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y H:i" } },
             //{ key: false, hidden: false, editable: false ,name: 'idpreliquidacion', width:'100' , index: 'idpreliquidacion' ,  formatter:  displayButtons2,classes:"grid-col"}
        ],
        pager: $(pagergrilla),
        rowNum: 80,
        rowList: [160, 240, 320],
        autoResizing: { compact: true },
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,
        autoheight: true,
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