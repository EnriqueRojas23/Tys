var btnNuevo = "#btnNuevo";

$(document).ready(function () {
    $("html, body").animate({ scrollTop: "100" });
    configurarGrillaOperaciones();
    configurarGrillaDetalle();

    $('#btnDesasociar').click(function (event) {
        var selIds = $("#griddetalle").jqGrid('getGridParam', 'selarrrow');
        if (selIds == '') {
            swal("No puede continuar", "Debe seleccionar un elemento", "warning");
        }
        else {
            btnDesasociar_click(this, event);
        }
    });

    $("#btnConfirmar").click(function (e) {
        var selIds = $("#griddetalle").jqGrid('getGridParam', 'selrow');
        if (selIds == null) {
            swal("No puede continuar", "Debe seleccionar un elemento", "warning");
        }
        else {
            confirmar(selIds);
        }

        //  var item =  $('griddetalle').jqGrid('getGridParam', 'selrow');
        //  confirmar(item);
    });
    $("#btnVerDetalle").click(function (e) {
        var item = $('#gridcargas').jqGrid('getGridParam', 'selarrrow');

        if (item == '') {
            swal("No puede continuar", "Debe seleccionar un elemento", "warning");
        }
        else {
            var placa = $('#gridcargas').jqGrid('getCell', item, 'placa');
            if (placa == '') {
                swal("No puede continuar", "Debe antes asignar una placa", "warning");
            }
            else {
                verdetallepopup(item);
            }
        }
    });
    $("#btnAsignarServicio").click(function (e) {
        var item = $("#gridcargas").jqGrid('getGridParam', 'selarrrow');
        if (item == '') {
            swal("No puede continuar", "Debe seleccionar un elemento", "warning");
        }
        else {
            AsignarServicios(item);
        }

        //  var item =  $('#gridcargas').jqGrid('getGridParam', 'selrow');
        //  AsignarServicios(item);
    });
    $("#btnAsignarVehiculo").click(function (e) {
        var item = $('#gridcargas').jqGrid('getGridParam', 'selrow');
        AsignarVehiculo(item);
    });

    $("#btnBuscar").button()
                      .click(function (e) {
                          reloadcarga();
                          $("#idcarga").val('');
                          reloaddetalle();
                      });

    //cargargrilladetalle(null);

    var myGrid = $("#gridcargas");
    $("#cb_" + myGrid[0].id).hide();
});

function configurarGrillaOperaciones() {
    $.jgrid.defaults.height = 220;
    $.jgrid.defaults.width = 500;
    $.jgrid.defaults.responsive = true;
    var grilla = $("#gridcargas");
    var pagergrilla = $("#gridcargaspager");

    var numcp = $('#numcp').val();
    var numcarga = $('#numcarga').val();
    var idestacion = $('#idestacion').val();

    var vdataurl = $(grilla).data("dataurl") + "?numcp=" + numcp + "&numcarga=" + numcarga + "&idestacion=" + idestacion

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Num. Carga', 'Fec. Creación', 'Volumen', 'Peso', 'Ruta', 'Placa', 'Placa', 'Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idcarga', index: 'idcarga', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcarga', index: 'numcarga', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'fecharegistro', index: 'fechacreacion', width: '60', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "ISO8601Long", newformat: "d/m/Y" } },
            { key: false, hidden: false, editable: false, name: 'vol', index: 'vol', width: '20', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'peso', index: 'peso', width: '20', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'ruta', index: 'ruta', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'placa', index: 'placa', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idcarga', index: 'idcarga', width: '60', align: 'center', classes: "grid-col", formatter: displayButtonVehiculo, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idcarga', width: '30', index: 'idcarga', formatter: displayButtonsOperacion, classes: "grid-col" }
        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        autowidth: true,
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
        editable: false,
        multiselect: true,
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
function OnCompleteTransaction_asignarvehiculo(xhr, status) {
    var jsonres = xhr.responseJSON;
    if (jsonres.res == true) {
        var url = "http://104.36.166.65/webreports/carga.aspx?idcarga=" + String(jsonres.idcarga);
        window.open(url);

        swal({
            title: "Se asignó el vehículo con éxito"
        , text: "Asignaciones del Vehículo: " + jsonres.cargas, type: "success", confirmButtonText: "Aceptar"
        });

        $("#modalcontainer").modal("hide");
        reloadcarga();
        reloaddetalle();
    }
    else {
        sweetAlert(jsonres.mensaje, null, "error");
    }
}

function OnCompleteTransaction(xhr, status) {
    var jsonres = xhr.responseJSON;
    CleanValidationError();
    var url = UrlHelper.Action("Jsongenerarguiastransportista", "Seguimiento", "Seguimiento");
    $("#modalcontainer").modal("hide");

    if (jsonres.res == true) {
        reloadcarga();

        swal({
            title: "Registro Exitoso",
            text: "Se registró correctamente. Ahora ingrese el número de GRT Inicial ",
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
                  swal.showInputError("Debe ingresar un número de Guia!");
                  return false;
              }
              else {
                  $.ajax({
                      url: url,
                      type: "post",
                      async: true,
                      datatype: "json",
                      data: { "guia": inputValue, "idcarga": $("#idcarga").val() },
                      success: function (data) {
                          if (data.res) {
                              var url = "http://104.36.166.65/webreports/guiatransportista.aspx?idcarga=" + String($("#idcarga").val());
                              window.open(url);
                              $("#idcarga").val('');
                              reloaddetalle();

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
    else {
        sweetAlert(jsonres.mensaje, null, "error");

        //CheckValidationErrorResponse(jsonres);
    }
}

function planificarcarga() {
    var url = UrlHelper.Action("PlanificarCarga", "Seguimiento", "Seguimiento");
    window.location.href = url;
}
function reloadcarga() {
    var numcp = $('#numcp').val();
    var numcarga = $('#numcarga').val();
    var idestacion = $('#idestacion').val();

    var vdataurl = $("#gridcargas").data("dataurl") + "?numcp=" + numcp + "&numcarga=" + numcarga + "&idestacion=" + idestacion
    $("#gridcargas").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function reloadservicios() {
    var vdataurl = $("#gridservicios").data("dataurl") + "?idcarga=" + $('#idcarga').val();

    $("#gridservicios").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function reloaddetalle() {
    var vdataurl = $("#griddetalle").data("dataurl") + "?idcarga=" + $("#idcarga").val();
    $("#griddetalle").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function verdetalle(id) {
    $("#idcarga").val(id);
    var vdataurl = $("#griddetalle").data("dataurl") + "?idcarga=" + $("#idcarga").val()
    $("#griddetalle").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function configurarGrillaDetalle() {
    $.jgrid.defaults.height = 420;
    $.jgrid.defaults.width = 500;
    $.jgrid.defaults.responsive = true;
    var grilla = $("#griddetalle");
    var pagergrilla = $("#griddetallepager");

    var vdataurl = $(grilla).data("dataurl") + "?idcarga=" + $("#idcarga").val()

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', '# Orden', 'Origen', 'Destino', 'Tipo Operación', 'Destinatario', 'Bulto', 'Peso', 'Volumen'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'origen', index: 'origen', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'tipooperacion', index: 'tipooperacion', width: '90', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '90', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'bulto', index: 'bulto', width: '20', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'peso', index: 'peso', width: '20', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'volumen', width: '20', index: 'volumen', formatter: formatedit, classes: "grid-col" }
        ],
        pager: $(pagergrilla),
        rowNum: 200,
        rowList: [200, 400, 600, 800],
        autowidth: true,
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
        editable: false,
        multiselect: true,
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

function anularoperacion(id) {
    var vUrl = UrlHelper.Action("AnularOperacion", "Seguimiento", "Seguimiento") + "?idcarga=" + id;
    swal({
        title: "Anular Operación",
        text: "¿Está seguro que desea anular esta operación?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Anular',
        closeOnConfirm: false,
        closeOnCancel: true
    },
       function (isConfirm) {
           if (isConfirm) {
               $.ajax({
                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {
                           swal("¡Se ha anulado correctamente!", data.msj, "success");
                           $("#modalcontainer").modal("hide");
                           reloadcarga();
                           $("#idcarga").val('');
                           reloaddetalle();
                       } else {
                           swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       alert(data.Errors.toString());
                   }
               });
           }
       });
}
function AsignarVehiculo(id) {
    var url = UrlHelper.Action("AsignarVehiculo", "Seguimiento", "Seguimiento") + "?idcarga=" + id;

    //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        $("#idvehiculo").change(function () {
            var idvehiculo = $("#idvehiculo").val();
            var urlinit = UrlHelper.Action("ObtenerDatosVehiculo", "Orden", "Seguimiento");

            $.ajax(
                  {
                      type: "POST",
                      async: true,
                      url: urlinit,
                      data: { "idvehiculo": idvehiculo },
                      success: function (data) {
                          $("#proveedor").html(data.proveedor);
                          $("#chofer").html(data.chofer);
                      },
                      error: function (request, status, error) {
                          swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                      }
                  });
        });
    });
}

function AsignarServicios(id) {
    var url = UrlHelper.Action("AsignarServicios", "Seguimiento", "Seguimiento") + "?idcarga=" + id;

    //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        $("#idcarga").val(id);

        configurarGrilla(id);
        $("#addrow").click(function () {
            $("#gridservicios").jqGrid('addRowData', 0, 1, "first");
            $("#gridservicios").editRow(0, true, null);
        });
    });
}
function configurarGrilla(id) {
    $.jgrid.defaults.height = 220;
    $.jgrid.defaults.width = 500;
    $.jgrid.defaults.responsive = true;
    var grilla = $("#gridservicios");
    var pagergrilla = $("#gridserviciospager");

    var vdataedit = $(grilla).data("editurl");
    var vdataurl = $(grilla).data("dataurl") + "?idcarga=" + id;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Servicio', 'Cantidad', 'Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idserviciooperacion', index: 'idserviciooperacion', classes: "grid-col" },
            { key: false, hidden: false, editable: true, name: 'servicio', index: 'servicio', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, edittype: "select", editoptions: { dataUrl: fcnUrlControlGrid('idservicio') }, classes: "grid-col" },
            {
                key: false, hidden: false, editable: true, name: 'cantidad', index: 'cantidad', width: '30', align: 'center', classes: "grid-col",

                editoptions: {
                    aftersavefunc: function (id) { },
                    keys: true,
                }
            },
            { key: false, hidden: false, editable: false, name: 'idserviciooperacion', width: '30', index: 'idserviciooperacion', formatter: displayButtons, classes: "grid-col" }
        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
        editable: true,
        //        addedrow: "last",
        addParams: {
            position: "last",
            addRowParams: editOptionsNew
        },
        editParams: editOptionsNew,
        editurl: vdataedit,

        onSelectRow: function (rowid, status) {
            //updateIdsOfSelectedRows(rowid, status);
        },

        afterInsertRow: function (id, currentData, jsondata) {
        },
        beforeSubmit: function () {
        },

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
function displayButtonVehiculo(cellvalue, options, rowObject) {
    var asignar = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='AsignarVehiculo(" + cellvalue + ");' href='#' > <i class='fa fa-plus'></i> Asignar</button>";

    return asignar;
}

function formatedit(cellvalue, options, rowObject) {
    if (cellvalue == null)
        return "";
    else return " " + cellvalue;
}
function displayButtons(cellvalue, options, rowObject) {
    var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"rowSave('" + options.rowId + "', '' );\"><i class='fa fa-save'></i> </button>";
    var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminarServicio(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
    var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#gridasignacion').restoreRow('" + options.rowId + "'); reload();\"><i class='fa fa-times-circle'></i> </button>";

    return guardar + control + restore;
}
function displayButtonsOperacion(cellvalue, options, rowObject) {
    var ver = "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='verdetalle(" + cellvalue + ");' href='#' > <i class='fa fa-magic'></i></button>"
    var anular = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='anularoperacion(" + cellvalue + ");' href='#' > <i class='fa fa-trash'></i></button></div>";
    return ver + anular;
}

var editOptionsNew = {
    keys: true,
    successfunc: function () {
        var $self = $(this);
        setTimeout(function () {
            $self.trigger("reloadGrid");
        }, 50);
    }
};

function rowSave(id, str) {
    $("#gridservicios").jqGrid('saveRow', id);

    var vdataurl = $("#gridservicios").data("dataurl") + "?idcarga=" + $('#idcarga').val();

    $("#gridservicios").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function verdetallepopup(id) {
    var url = UrlHelper.Action("VerDetalleOperacion", "Seguimiento", "Seguimiento") + "?idcarga=" + id;

    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal("show");

        configurarGrillaP(id);
        $("#addrow").click(function () {
            $("#gridservicios").jqGrid('addRowData', 0, 1, "first");
            $("#gridservicios").editRow(0, true, null);
        });
    });
}
function configurarGrillaP(id) {
    $.jgrid.defaults.responsive = true;
    var grilla = $("#griddetallepu");
    var pagergrilla = $("#griddetallepupager");

    var vdataurl = $(grilla).data("dataurl") + "?idcarga=" + id;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', '# Carga', 'Origen', 'Destino', 'Remitente', 'destinatario', 'Bulto', 'Peso', 'Volumen'],
        colModel:
        [
            { key: true, hidden: true, name: 'idcarga', index: 'idcarga', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcarga', index: 'numcarga', width: '120', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'origen', index: 'origen', width: '100', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '100', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'remitente', index: 'remitente', width: '140', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '140', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'bulto', index: 'bulto', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'peso', index: 'peso', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'vol', index: 'vol', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },

        ],
        caption: 'Listado de Cargas',
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,

        onSelectRow: function (rowid, status) {
            //updateIdsOfSelectedRows(rowid, status);
        },

        afterInsertRow: function (id, currentData, jsondata) {
        },
        beforeSubmit: function () {
        },

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
function confirmar(id) {
    var item = $('#griddetalle').jqGrid('getGridParam', 'selarrrow');

    id = $("#idcarga").val();
    var url = UrlHelper.Action("ConfirmarCarga", "Seguimiento", "Seguimiento") + "?idcarga=" + id + "&ids=" + item

    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        $('#horaconfirmacion').mask('00:00');

        $('#dtpfechacomp .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
    });
}
function btnDesasociar_click(obj, event) {
    var item = $('#griddetalle').jqGrid('getGridParam', 'selrow');

    var url = UrlHelper.Action("DesasociarOrdenes", "Seguimiento", "Seguimiento") + "?ordenes=" + item + "&idcarga= " + $("#idcarga").val();

    swal({
        title: "Desasociar Ordenes",
        text: "¿Está seguro que desea desasociar estas ordenes? ",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Desasociar',
        closeOnConfirm: true,
        closeOnCancel: true
    },
    function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: url,
                type: "post",
                datatype: "json",

                success: function (data) {
                    if (data.res) {
                        swal("¡Operación realizada con éxito!", data.msj, "success");
                        $("#modalcontainer").modal("hide");
                        reloadcarga();
                        reloaddetalle();
                    }
                    else {
                        swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
                    }
                },
                error: function (data) {
                    alert(data.Errors.toString());
                }
            });
        }
    });
}
function irEliminarServicio(id) {
    var url = UrlHelper.Action("EliminarServicio", "Seguimiento", "Seguimiento") + "?idcarga=" + $('#idcarga').val() + "&idserviciooperacion= " + id;

    $.ajax({
        url: url,
        type: "post",
        datatype: "json",
        success: function (data) {
            if (data.res) {
                var vdataurl = $("#gridservicios").data("dataurl") + "?idcarga=" + $('#idcarga').val();
                $("#gridservicios").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
            } else {
                swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
            }
        }
    })
}
// function reload1()
// {
//       var grilla = $("#gridordenes");
//       var idestacion =  $('#idestacion').val() ;
//       var idcliente =  $('#idcliente').val() ;
//       var iddestino =  $('#iddestino').val() ;

//       var vdataurl =  UrlHelper.Action("GetListarPlanificarOrden", "Seguimiento", "Seguimiento") + "?idestacionorigen=" + idestacion
//       + "&idcliente=" + idcliente + "&iddestino= " + iddestino
//       $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
// }