var btnNuevo = "#btnNuevo";
var statSend = false;

$(document).ready(function () {
    $("html, body").animate({ scrollTop: "100" });

    $("#btnBuscar").button()
                      .click(function (e) {
                          reload1();
                      });

    cargargrilla();
    cargargrilla2();

    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-results': { no_results_text: 'Oops, no se encontró el ubigeo!' }
    }

    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }
    $("#btnPlanicarDirecto").click(function () {
        ModalAsignarManifiestoVehiculo();
    })
});
function formatedit(cellvalue, options, rowObject) {
    if (cellvalue == null)
        return "";
    else
        return " " + cellvalue;
}
function cargargrilla() {
    $.jgrid.defaults.width = 200;
    $.jgrid.defaults.height = 400;

    var grilla = $("#gridordenes");
    var pagergrilla = $("#gridordenespager");

    var id = $('#idcliente').val()
    var idestacion = $('#idestacion').val();
    var iddestino = $('#iddestino').val();
    var idtipotransporte = $("#idtipotransporte").val();

    var vdataurl = $(grilla).data("dataurl") + "?idestacionorigen=" + idestacion + "&idcliente=" + id + "&iddestino=" + iddestino
    + "&idtipotransporte=" + idtipotransporte;

    var vdataedit = $(grilla).data("editurl");

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'N° OT', 'Rechazo', 'Modo', 'Remitente', 'Destinatario', 'Destino', 'Bultos'//, 'AutoServ'
        , 'Peso', 'M3', 'SubTotal'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },

            {
                key: false, hidden: false, editable: false, name: 'numcp'
                    , index: 'numcp', width: '120', align: 'center'
                    , classes: "grid-col", formatter: formatedit
            },

            {
                key: false, hidden: false, editable: false, name: 'rechazo'
                    , index: 'rechazo', width: '70', align: 'center'
                    , classes: "grid-col", formatter: VerCheckBox
            },

           {
               key: false, hidden: false, editable: false, name: 'tipotransporte'
                    , index: 'tipotransporte', width: '130', align: 'center'
                    , classes: "grid-col", formatter: formatedit
           },

           {
               key: false, hidden: false, editable: false, name: 'remitente'
                    , index: 'remitente', width: '100', align: 'center'
                    , classes: "grid-col", formatter: formatedit
           },
                 {
                     key: false, hidden: false, editable: false, name: 'destinatario'
                    , index: 'destinatario', width: '100', align: 'center'
                    , classes: "grid-col", formatter: formatedit
                 },

            { key: false, hidden: false, editable: true, name: 'destino', index: 'destino', width: '100', align: 'center' },
            { key: false, hidden: false, editable: true, name: 'bulto', index: 'bulto', width: '50', align: 'center' },
            { key: false, hidden: false, editable: true, name: 'peso', index: 'peso', width: '50', align: 'center' },
            { key: false, hidden: false, editable: true, name: 'volumen', index: 'volumen', width: '50', align: 'center' },
            { key: false, hidden: false, editable: true, name: 'subtotal', index: 'subtotal', width: '50', align: 'center' }

        ],
        pager: $(pagergrilla),
        rowNum: 200,
        rowList: [200, 300, 400],
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,
        autoheight: true,
        editable: true,
        shrinkToFit: false,
        multiselect: true,
        addParams: {
            position: "last",
            addRowParams: editOptionsNew
        },
        editParams: editOptionsNew,
        editurl: vdataedit,
        shrinkToFit: true,
        grouping: true,
        groupingView: {
            groupField: ['destino'],
            groupColumnShow: [true],
            groupCollapse: false,
            //groupText : ['<b>{0} - {1} Elemento(s)</b>'],
            //groupText : ['<b>{0}</b> Costo Total: {costo}', '{0} Costo Total: {costo}'],
            groupSummary: [false],
            showSummaryOnHide: true
        },

        onSelectRow: function (rowid, status) {
            updateIdsOfSelectedRows(rowid, status);
        },
        onSelectAll: function (aRowids, status) {
            var i, count, id;
            for (i = 0, count = aRowids.length; i < count; i++) {
                id = aRowids[i];
                updateIdsOfSelectedRows(id, status);
            }
        },

        afterInsertRow: function (id, currentData, jsondata) {
        },
        loadComplete: function (data) {
            //   setStyleCheckBoxGrid(this);

            for (var i = 0; i < idsOfSelectedRows.length; i++) {
                $("#gridordenes").setSelection(idsOfSelectedRows[i], true);
            }
            var numerofilas = $(this).getGridParam("records");
            //
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
var idsOfSelectedRows;
var updateIdsOfSelectedRows = function (id, isSelected) {
    var contains = $.inArray(id, idsOfSelectedRows) == -1 ? false : true; //.contains(id);
    if (!isSelected && contains) {
        for (var i = 0; i < idsOfSelectedRows.length; i++) {
            if (idsOfSelectedRows[i] == id) {
                idsOfSelectedRows.splice(i, 1);
                break;
            }
        }
    }
    else if (!contains) {
        idsOfSelectedRows.push(id);
    }
};
function cargargrilla2() {
    $.jgrid.defaults.width = 1000;
    $.jgrid.defaults.height = 200;

    var grilla = $("#gridordenesp");
    var pagergrilla = $("#gridordenesppager");

    var id = $('#idcliente').val()

    var vdataurl = $(grilla).data("dataurl") + "?id=" + id;
    var vdataedit = $(grilla).data("editurl");

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Número OT', 'Rechazo', 'Operación', 'Modo', 'Remitente', 'Destinatario', 'Destino', 'Bultos'//, 'AutoServ'
       , 'Peso', 'M3', 'SubTotal', 'Eliminar'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },

            {
                key: false, hidden: false, editable: false, name: 'numcp'
                    , index: 'numcp', width: '130', align: 'center'
                    , classes: "grid-col", formatter: formatedit
            },

          {
              key: false, hidden: false, editable: false, name: 'rechazo'
                    , index: 'rechazo', width: '70', align: 'center'
                    , classes: "grid-col", formatter: VerCheckBox
          },
              {
                  key: false, hidden: false, editable: false, name: 'operacion'
                    , index: 'operacion', width: '130', align: 'center'
                    , classes: "grid-col", formatter: formatedit
              },

           {
               key: false, hidden: false, editable: false, name: 'tipotransporte'
                    , index: 'tipotransporte', width: '130', align: 'center'
                    , classes: "grid-col", formatter: formatedit
           },

           {
               key: false, hidden: false, editable: false, name: 'remitente'
                    , index: 'remitente', width: '100', align: 'center'
                    , classes: "grid-col", formatter: formatedit
           },
                 {
                     key: false, hidden: false, editable: false, name: 'destinatario'
                    , index: 'destinatario', width: '100', align: 'center'
                    , classes: "grid-col", formatter: formatedit
                 },

            { key: false, hidden: false, editable: true, name: 'destino', index: 'destino', width: '100', align: 'center' },
            { key: false, hidden: false, editable: true, name: 'bulto', index: 'bulto', width: '50', align: 'center' },
            { key: false, hidden: false, editable: true, name: 'peso', index: 'peso', width: '50', align: 'center' },
            { key: false, hidden: false, editable: true, name: 'volumen', index: 'volumen', width: '50', align: 'center' },
            { key: false, hidden: false, editable: true, name: 'subtotal', index: 'subtotal', width: '50', align: 'center' },

         { key: false, hidden: false, editable: false, name: 'idordentrabajo', width: '60', index: 'idordentrabajo', formatter: displayButtonsDirecciones, classes: "grid-col" }
        ],
        pager: $(pagergrilla),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,
        autoheight: true,
        editable: true,
        shrinkToFit: false,
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
function displayButtonsDirecciones(cellvalue, options, rowObject) {
    var eliminar = '<button type="button" class="btn btn-primary btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';

    return eliminar;
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

function irEliminar(id) {
    $('#gridordenesp').jqGrid('delRowData', id);

    var url = UrlHelper.Action("EliminarOrdenAgregada", "Seguimiento", "Seguimiento");
    $.ajax(
                {
                    type: "POST",
                    async: true,
                    url: url,
                    data: { "id": id },
                    success: function (data) {
                        $("#txtpesototal").val(data.peso);
                        $("#txtbultos").val(data.bultos);
                        $("#txtcantidad").val(data.cantidad);
                        $("#txtsubtotal").val(data.subtotal);

                        reload1();
                    },
                    error: function (request, status, error) {
                        swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                    }
                });
}

function irCopiar() {
    var idcliente = $("#idclientecopia").val();
    var idoriginal = $("#idcliente").val();

    var url = UrlHelper.Action("CopiarTarifa", "Seguimiento", "Seguimiento");
    swal({
        title: "Copiar Tarifa",
        text: "¿Está seguro que desea copiar esta tarifa?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Copiar',
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
                              data: { "id": idcliente, "idoriginal": idoriginal },
                              success: function (data) {
                                  var grilla = $("#gridtarifa");
                                  var vdataurl = $(grilla).data("dataurl") + "?id=" + idcliente;
                                  $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                                  swal("¡Se ha copiado correctamente!", data.msj, "success");
                              },
                              error: function (request, status, error) {
                                  swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                              }
                          });
          }
      });
}
function VerCheckBox(cellvalue, options, rowObject) {
    var control = $("<input type='checkbox' onclick=actualizaSeleccion('" + cellvalue + "') value='" + cellvalue + "'> <br>");
    var htmlcontrol = control[0].outerHTML;
    return htmlcontrol
}

function agregarorden() {
    if ($('#idtipooperacion').val() == '') {
        swal({ title: "Seleccione un tipo de operación", text: "Debe seleccionar un tipo de operación.", type: "error", confirmButtonText: "Aceptar" });
        return;
    }

    if ($('#idtipooperacion').val() == '112') {
        if ($('#idruta').val() == '') {
            swal({ title: "Seleccione una ruta", text: "Debe seleccionar una ruta.", type: "error", confirmButtonText: "Aceptar" });
            return;
        }
    }
    if ($('#idtipooperacion').val() == '124') {
        if ($('#idestaciondestino').val() == '') {
            swal({ title: "Seleccione una estación destino", text: "Debe seleccionar una estación destino.", type: "error", confirmButtonText: "Aceptar" });
            return;
        }
    }

    var cellValues = [];
    var selIds = $("#gridordenes").jqGrid('getGridParam', 'selarrrow');
    for (i = 0, n = selIds.length; i < n; i++) {
        cellValues.push($("#gridordenes").jqGrid("getCell", selIds[i], "idordentrabajo"));
    }

    if (cellValues == '') {
        swal({ title: "Seleccione Ordenes de Trabajo", text: "Debe seleccionar  uno o mas  Ordenes de Trabajo.", type: "error", confirmButtonText: "Aceptar" });
        return;
    }
    var nombreop = $('#idtipooperacion option:selected').text();

    var ordenes = String(cellValues);
    var grilla = $("#gridordenesp");
    var vdataurl = $(grilla).data("dataurl");

    var url = UrlHelper.Action("validarordenes", "Seguimiento", "Seguimiento");

    $.ajax({
        type: "POST",
        async: true,
        url: url,
        data: {
            "ids": ordenes,
            "idruta": $('#idruta').val(),
            "idtipooperacion": $('#idtipooperacion').val(),
            "idagencia": $('#idagencia').val(),
            "idestacion": $('#idestaciondestino').val(),
            "operacion": nombreop
        },
        success: function (data) {
            if (data.resp != true) {
                swal("¡Error!", data.msj, "error");
                return;
            }
            else {
                $("#txtpesototal").val(data.peso);
                $("#txtbultos").val(data.bultos);
                $("#txtcantidad").val(data.cantidad);
                $("#txtsubtotal").val(data.subtotal);

                $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                reload1();
            }

            //
        },
        error: function (request, status, error) {
            swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
        }
    });
}
// function planificarcarga()
// {
//       var   idtipooperacion =  $('#idtipooperacion').val() ;
//       var   idagencia =  $('#idagencia').val() ;
//       var   idestacion =  $('#idestaciondestino').val() ;
//       var   idruta =  $('#idruta').val() ;
//       var cantidad =  $("#gridordenesp").getGridParam("records");
//
//
//       var url = UrlHelper.Action("PlanificarCarga", "Seguimiento", "Seguimiento");
//
//
//       if(idtipooperacion == '' || idagencia == '' ||  idestacion == '' ||  idruta == '')
//       {
//         swal({ title: "¡Faltan datos!", text: "¡Seleccione los datos necesarios para el registro!", type: "error", confirmButtonText: "Aceptar" });
//         return ;
//       }
//
//
//       if(cantidad == 0)
//       {
//          swal({ title: "¡Faltan datos!", text: "¡No existen ordenes de transporte agregadas!", type: "error", confirmButtonText: "Aceptar" });
//          return ;
//       }
//
//
//
//
//
// }
function reload1() {
    var grilla = $("#gridordenes");
    var idestacion = $('#idestacion').val();
    var idcliente = $('#idcliente').val();
    var iddestino = $('#iddestino').val();
    var idtipotransporte = $("#idtipotransporte").val();

    var vdataurl = $(grilla).data("dataurl") + "?idestacionorigen=" + idestacion + "&idcliente=" + idcliente + "&iddestino=" + iddestino
    + "&idtipotransporte=" + idtipotransporte;

    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function OnCompleteTransaction(xhr, status) {
    var jsonres = xhr.responseJSON;
    CleanValidationError();

    if (jsonres.res == true) {
        swal({
            title: "Registro Exitoso",
            text: "Se registró correctamente.",
            type: "success"
        },
        function () {
            // var url = "http://104.36.166.65/webreports/carga.aspx?idcarga=" + String(jsonres.idcarga);

            //   window.open(url);

            var url = UrlHelper.Action("Operaciones", "Seguimiento", "Seguimiento");
            window.location.href = url;
        });
    }
    else {
        sweetAlert(jsonres.mensaje, null, "error");
        //CheckValidationErrorResponse(jsonres);
    }
}
function ModalAsignarManifiestoVehiculo() {
    var url = UrlHelper.Action("AsociarManifiestoVehiculo", "Seguimiento", "Seguimiento");

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
function OnCompleteTransaction_asignarvehiculo(xhr, status) {
    var jsonres = xhr.responseJSON;
    if (jsonres.res == true) {
        $.each(jsonres.ids, function (i, val) {
            var url = "http://104.36.166.65/webreports/manifiesto.aspx?idmanifiesto=" + String(val);
            window.open(url);
        })

        var url = UrlHelper.Action("Despacho", "Seguimiento", "Seguimiento");
        window.location.href = url;
    }
    else {
        //  sweetAlert(jsonres.mensaje, null, "error");
    }
}