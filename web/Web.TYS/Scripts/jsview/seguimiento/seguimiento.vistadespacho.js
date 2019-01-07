$(document).ready(function () {



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

    //cargargrillaDespacho();
    cargargrilladetalle(null);

    $("#btnBuscar").click(function (event) {
        reload();
    });

    // var myGrid = $("#griddespacho");
    // $("#cb_"+myGrid[0].id).hide();
});

function reload() {
    var grilla = $("#griddetalle");
    var numcp = $('#numcp').val();
    var numcarga = $('#numcarga').val();
    var nummanifiesto = $('#nummanifiesto').val();
    var numgrt = $('#numgrt').val();
    var numhojaruta = $('#numhojaruta').val();


    var fecini = $('#fechainicio').val();
    var fecfin = $('#fechafin').val();



    var vdataurl = $(grilla).data("dataurl")
                    + "?fecinicio=" + fecini
                    + "&fecfin=" + fecfin
                    + "&numhojaruta=" + numhojaruta
                    + "&numcarga=" + numcarga
                    + "&nummanifiesto=" + nummanifiesto
                    + "&numcp=" + numcp
                    + "&numgrt=" + numgrt

    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
function cargargrillaDespacho() {
    $.jgrid.defaults.height = 220;
    $.jgrid.defaults.width = 500;
    $.jgrid.defaults.responsive = true;
    var grilla = $("#griddespacho");
    var pagergrilla = $("#griddespachopager");

    var numcp = $('#numcp').val();
    var numcarga = $('#numcarga').val();
    var nummanifiesto = $('#nummanifiesto').val();
    var numgrt = $('#numgrt').val();
    var numhojaruta = $('#numhojaruta').val();

    alert(numhojaruta)

    var vdataurl = $(grilla).data("dataurl") + "?numcp=" + numcp + "&numcarga=" + numcarga;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Placa', 'Transportista', 'Chofer', 'Hoja Ruta', 'Carga', 'Peso', 'Vol', 'Imprimir Carga', 'Imprimir HR'],
        colModel:
        [
            { key: true, hidden: true, name: 'idcarga', index: 'idcarga', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'placa', index: 'placa', width: '50', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'transportista', index: 'placa', width: '100', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'chofer', index: 'chofer', width: '120', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numhojaruta', index: 'numhojaruta', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcarga', index: 'numcarga', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'peso', index: 'peso', width: '30', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'vol', index: 'vol', width: '30', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idcarga', index: 'idcarga', width: '40', align: 'center', classes: "grid-col", formatter: displayButtonImprimirCarga, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idvehiculo', index: 'idvehiculo', width: '40', align: 'center', classes: "grid-col", formatter: displayButtonImprimirHojaRuta, classes: "grid-col" },

        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
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
            setStyleCheckBoxGrid(this);

            // for (var i = 0; i < idsOfSelectedRows.length; i++){
            //       $("#gridcargas").setSelection(idsOfSelectedRows[i], true);
            // }
            // var numerofilas = $(this).getGridParam("records");
            //
        },
        beforeSelectRow: function (rowid, e) {
            jQuery("#griddespacho").jqGrid('resetSelection');
            return (true);
        }
    });
}

function cargargrilladetalle(id) {
    $.jgrid.defaults.height = 220;
    $.jgrid.defaults.width = 500;
    $.jgrid.defaults.responsive = true;
    var grilla = $("#griddetalle");
    var pagergrilla = $("#griddetallepager");

    var numcp = $('#numcp').val();
    var numcarga = $('#numcarga').val();
    var nummanifiesto = $('#nummanifiesto').val();
    var numgrt = $('#numgrt').val();
    var numhojaruta = $('#numhojaruta').val();

    

    var fecini = $('#fechainicio').val();
    var fecfin = $('#fechafin').val();

    var vdataurl = $(grilla).data("dataurl")
                    + "?fecinicio=" + fecini
                    + "&fecfin=" + fecfin
                    + "&numhojaruta" + numhojaruta
                    + "&numcarga=" + numcarga
                    + "&nummanifiesto=" + nummanifiesto
                    + "?numcp=" + numcp
                    + "&numgrt=" + numgrt
                    
    
    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Manifiesto', '# Orden', 'Destino', 'Remitente', 'Destinatario', 'Bulto', 'Peso', 'Volumen', 'Imprimir Carga', 'Imprimir GRT', 'Imprimir Manifiesto', 'Imprimir HR'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'nummanifiesto', index: 'nummanifiesto', width: '50', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'remitente', index: 'remitente', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '90', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'bulto', index: 'bulto', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'peso', index: 'peso', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'volumen', index: 'volumen', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idcarga', index: 'idcarga', width: '40', align: 'center', classes: "grid-col", formatter: displayButtonImprimirCarga, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idcarga', index: 'idcarga', width: '40', align: 'center', classes: "grid-col", formatter: displayButtonImprimirGRT, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idmanifiesto', index: 'idmanifiesto', width: '40', align: 'center', classes: "grid-col", formatter: displayButtonImprimirManifiesto, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'iddespacho', index: 'iddespacho', width: '40', align: 'center', classes: "grid-col", formatter: displayButtonImprimirHojaRuta, classes: "grid-col" }

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
            setStyleCheckBoxGrid(this);

            // for (var i = 0; i < idsOfSelectedRows.length; i++){
            //       $("#gridcargas").setSelection(idsOfSelectedRows[i], true);
            // }
            // var numerofilas = $(this).getGridParam("records");
            //
        },
        beforeSelectRow: function (rowid, e) {
            jQuery("#griddespacho").jqGrid('resetSelection');
            return (true);
        }
    });
}

function verdetalle(id) {
    $("#idoperacion").val(id);
    reloadDetalle(id);
}
function reloadDetalle(id) {
    //var vdataurl = $("#griddetalle").data("dataurl") + "?idvehiculo=" + id;

    var vdataurl = $(grilla).data("dataurl")
                + "?fecinicio=" + fecini
                + "&fecfin=" + fecfin
                + "&numhojaruta" + numhojaruta
                + "&numcarga=" + numcarga
                + "&nummanifiesto=" + nummanifiesto
                + "?numcp=" + numcp
                + "&numgrt=" + numgrt

    $("#griddetalle").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function reloadDespachos(id) {
    var numcp = $('#numcp').val();
    var nuncarga = $('#numcarga').val();

    var vdataurl = $("#griddespacho").data("dataurl") + "?idvehiculo=" + numcp + "&numcarga=" + numcarga;
    $("#griddespacho").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function displayButtonImprimirCarga(cellvalue, options, rowObject) {
    var asignar = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='imprimircarga(" + cellvalue + ");' href='#' > <i class='fa fa-print'></i> </button>";
    return asignar;
}
function displayButtonImprimirManifiesto(cellvalue, options, rowObject) {
    var asignar = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='imprimirmanifiesto(" + cellvalue + ");' href='#' > <i class='fa fa-print'></i> </button>";
    return asignar;
}
function displayButtonImprimirHojaRuta(cellvalue, options, rowObject) {
    var asignar = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='imprimirhojaruta(" + cellvalue + ");' href='#' > <i class='fa fa-print'></i> </button>";
    return asignar;
}
function displayButtonImprimirGRT(cellvalue, options, rowObject) {
    var asignar = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='imprimirgrt(" + cellvalue + ");' href='#' > <i class='fa fa-print'></i> </button>";
    return asignar;
}
function imprimirhojaruta(item) {
    var url = "http://104.36.166.65/webreports/hojaruta.aspx?iddespacho=" + String(item);
    window.open(url);
}
function imprimircarga(item) {
    var url = "http://104.36.166.65/webreports/carga.aspx?idcarga=" + String(item);
    window.open(url);
}
function imprimirmanifiesto(item) {
    var url = "http://104.36.166.65/webreports/manifiesto.aspx?idmanifiesto=" + String(item);
    window.open(url);
}
function imprimirgrt(item) {
    var url = UrlHelper.Action("Jsongenerarguiastransportista", "Seguimiento", "Seguimiento");
    swal({
        title: "Reimprimir la GRT",
        text: "Ingrese el número de GRT Inicial ",
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
                  data: { "guia": inputValue, "idcarga": item },
                  success: function (data) {
                      if (data.res) {
                          var url = "http://104.36.166.65/webreports/guiatransportista.aspx?idcarga=" + String(item);
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