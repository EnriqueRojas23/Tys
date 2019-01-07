$(document).ready(function () {
    $("html, body").animate({ scrollTop: "100" });

    cargargrillaDespacho();
    cargargrilladetalle(null);

    var myGrid = $("#griddespacho");
    $("#cb_" + myGrid[0].id).hide();

    $('#btnManifiesto').click(function () {
        var item = $('#griddespacho').jqGrid('getGridParam', 'selarrrow');
        if (item == '') {
            swal("No puede continuar", "Debe seleccionar un elemento", "warning");
        }
        else {
            generarmanifesto(item);
        }
    });
    $('#btnTransbordo').click(function () {
        var item = $('#griddespacho').jqGrid('getGridParam', 'selarrrow');

        if (item == '') {
            swal("No puede continuar", "Debe seleccionar un elemento", "warning");
        }
        else {
            transbordovehiculo(item);
        }
    });
    $('#btnAsignarPrecinto').click(function () {
        var item = $('#griddespacho').jqGrid('getGridParam', 'selarrrow');
        if (item == '') {
            swal("No puede continuar", "Debe seleccionar un elemento", "warning");
        }
        else {
            asignarprecinto(item);
        }
    });
    $('#btnAsignarValijas').click(function () {
        var item = $('#griddespacho').jqGrid('getGridParam', 'selarrrow');

        if (item == '') {
            swal("No puede continuar", "Debe seleccionar un elemento", "warning");
        }
        else {
            asignarvalijas(item);
        }
    });
    $('#btnSalidaVehiculo').click(function () {
        var item = $('#griddespacho').jqGrid('getGridParam', 'selarrrow');
        if (item == '') {
            swal("No puede continuar", "Debe seleccionar un elemento", "warning");
        }
        else {
            salidavehiculo(item);
        }
    });
    $("#btnBuscar").click(function (event) {
        reload();
        reloadDetalle('');
    });
});

function validarordenvalija() {
    var documento = $("#txtEscanDocumento").val();

    if (documento == "") {
        swal({ title: "¡Error!", text: "¡Ingresar número de OT!", type: "error", confirmButtonText: "Aceptar" });
        document.getElementById('txtEscanDocumento').focus();
    }
    else {
        var url = UrlHelper.Action("ValidarAsignarValijas", "Seguimiento", "Seguimiento") + "?idorden=" + documento + "&idvehiculo= " + $("#idvehiculo").val();

        $.ajax({
            url: url,
            type: "post",
            datatype: "json",
            data: {},
            success: function (data) {
                if (data.res) {
                    var grilla = $("#griddetallepu");
                    var vdataurl = $(grilla).data("dataurl") + "?idvehiculo=" + $("#idvehiculo").val();

                    $("#griddetallepu").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                    $("#txtEscanDocumento").val('');
                } else {
                    $("#txtEscanDocumento").val('');
                    swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
                }
            },
            error: function (data) {
                alert(data.Errors.toString());
            }
        });
    }
}

function guardarValijas() {
    var id = $("#idvehiculo").val();

    var url = UrlHelper.Action("GuardarAsignarValijas", "Seguimiento", "Seguimiento") + "?idvehiculo= " + $("#idvehiculo").val();

    $.ajax({
        url: url,
        type: "post",
        datatype: "json",
        data: {},
        success: function (data) {
            if (data.res) {
                swal("¡Se generó correctamente!", data.msj, "success");
                $("#modalcontainer").modal("hide");

                reloadDespachos();
                $("#idcarga").val(id);
                reloadDetalle(id);
            } else {
                swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
            }
        },
        error: function (data) {
            alert(data.Errors.toString());
        }
    });
}

//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////
function reload() {
    var grilla = $("#griddespacho");

    var numcp = $('#numcp').val();
    var numcarga = $('#numcarga').val();

    var vdataurl = $(grilla).data("dataurl") + "?numcp=" + numcp + "&numcarga=" + numcarga;
    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function cargargrillaDespacho() {
    $.jgrid.defaults.height = 220;
    $.jgrid.defaults.width = 500;
    $.jgrid.defaults.responsive = true;
    var grilla = $("#griddespacho");
    var pagergrilla = $("#griddespachopager");

    var numcp = $('#numcp').val();
    var numcarga = $('#numcarga').val();

    var vdataurl = $(grilla).data("dataurl") + "?numcp=" + numcp + "&numcarga=" + numcarga;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Placa', 'Manifiesto', 'Precintos'
        , 'Valija', 'Estado', 'Proveedor', 'Chofer', 'Tipo Operación', 'Rutas', 'Peso', 'Vol', 'Det.'],
        colModel:
        [
            { key: true, hidden: true, name: 'idvehiculo', index: 'idvehiculo', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'placa', index: 'placa', width: '50', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'estado', index: 'estado', width: '60', align: 'center', classes: "grid-col", formatter: displayButtonsManifiesto, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'cantidadprecintos', index: 'cantidadprecintos', width: '60', align: 'center', classes: "grid-col", formatter: displayButtonsEstados, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'cantidadvalija', index: 'cantidadvalija', width: '60', align: 'center', classes: "grid-col", formatter: displayButtonsEstados, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'estado', index: 'estado', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'proveedor', index: 'proveedor', width: '90', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'chofer', index: 'chofer', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'tiposoperacion', index: 'tiposoperacion', width: '90', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'rutas', index: 'rutas', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'peso', index: 'peso', width: '30', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'vol', index: 'vol', width: '30', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idvehiculo', width: '30', index: 'idvehiculo', align: 'center', formatter: displayButtonsOperacion, classes: "grid-col" }
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
            //  setStyleCheckBoxGrid(this);

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

    var idvehiculo = $("#idcarga").val();

    var vdataurl = $(grilla).data("dataurl") + "?idvehiculo=" + idvehiculo;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Manifiesto', '# Orden', 'Tipo Operación'
        , 'Origen', 'Destino', 'Remitente', 'Destinatario', 'Bulto', 'Peso', 'Voumen', 'Acciones'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'nummanifiesto', index: 'nummanifiesto', width: '50', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'tipooperacion', index: 'tipooperacion', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'origen', index: 'origen', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'remitente', index: 'remitente', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '90', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'bulto', index: 'bulto', width: '60', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'peso', index: 'peso', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'volumen', index: 'volumen', width: '40', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'idordentrabajo', index: 'idordentrabajo', width: '60', align: 'center', classes: "grid-col", formatter: displayButtonAnular, classes: "grid-col" },

        ],
        pager: $(pagergrilla),
        rowNum: 50,
        rowList: [50, 100, 150, 200],
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
            //setStyleCheckBoxGrid(this);

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

function anularmanifiesto(id) {
    //
    if (validarexistemanifiesto2(id) == false)
        event.preventdefault();

    var vUrl = UrlHelper.Action("JsonAnularManifiesto", "Seguimiento", "Seguimiento") + "?idorden=" + id;
    swal({
        title: "Desasociar Orden de Manifiesto",
        text: "¿Está seguro que desea desasociar la Orden?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Desasociar',
        closeOnConfirm: false,
        closeOnCancel: true
    }
      , function () {
          $.ajax({
              url: vUrl,
              type: "post",
              datatype: "json",
              data: { id: id },
              success: function (data) {
                  if (data.res) {
                      swal("¡Se desasoció correctamente!", data.msj, "success");
                      reloadDespachos();
                      reloadDetalle($("#idcarga").val());
                      $.each(data.ids, function (i, val) {
                          var url = "http://104.36.166.65/webreports/manifiesto.aspx?idmanifiesto=" + String(val);
                          window.open(url);
                      })
                  }
                  else {
                      swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
                  }
              },
              error: function (data) {
                  alert(data.Errors.toString());
              }
          });
      });
}

function generarmanifesto(id) {
    if (validarcargaspendientes(id) == false)
        event.preventdefault();

    var vUrl = UrlHelper.Action("JsonGenerarManifiesto", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id;
    swal({
        title: "Generar Manifiesto",
        text: "¿Está seguro que desea generar el Manifiesto?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Generar',
        closeOnConfirm: true,
        closeOnCancel: true
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: vUrl,
                type: "post",
                datatype: "json",
                data: { id: id },
                success: function (data) {
                    if (data.res) {
                        $.each(data.ids, function (i, val) {
                            var url = "http://104.36.166.65/webreports/manifiesto.aspx?idmanifiesto=" + String(val);
                            window.open(url);
                        })
                        swal("¡Se generó correctamente!", data.msj, "success");
                        var id = $("#idcarga").val(id);
                        reloadDespachos();
                        reloadDetalle(id);
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
function validarcargaspendientes(id) {
    var noexiste = true;
    var vUrl = UrlHelper.Action("ValidarCargasPendientes", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id
    $.ajax({
        url: vUrl,
        type: "post",
        datatype: "json",
        data: {},
        async: false,
        success: function (data) {
            if (!data.res) {
                noexiste = false;
                swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
            }
        },
        error: function (data) {
            alert(data.Errors.toString());
        }
    });

    return noexiste;
}

function transbordovehiculo(id) {
    var url = UrlHelper.Action("TransbordoVehiculo", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id;

    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        $("#idvehiculo_new").change(function () {
            var idvehiculo = $("#idvehiculo_new").val();
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
function asignarprecinto(id) {
    if (validarexistemanifiesto(id) == false)
        event.preventdefault();

    var url = UrlHelper.Action("AsignarPrecinto", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id;

    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        var dlbAccesoriosODS = $('select[name="AccesoriosSeleccionados"]').bootstrapDualListbox({
            nonSelectedListLabel: 'Disponibles',
            selectedListLabel: 'Seleccionados',
            showFilterInputs: true,
            moveOnSelect: true,
        });
    });
}

function validarexistemanifiesto(id) {
    var noexiste = true;
    var vUrl = UrlHelper.Action("ValidarExisteManifiesto2", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id;
    $.ajax({
        url: vUrl,
        type: "post",
        datatype: "json",
        data: {},
        async: false,
        success: function (data) {
            if (!data.res) {
                noexiste = false;
                swal({ title: "No puede continuar", text: data.msj, type: "warning", confirmButtonText: "Aceptar" });
            }
        },
        error: function (data) {
            alert(data.Errors.toString());
        }
    });
    return noexiste;
}
function validarexistemanifiesto2(id) {
    var noexiste = true;
    var vUrl = UrlHelper.Action("ValidarExisteManifiesto", "Seguimiento", "Seguimiento");
    $.ajax({
        url: vUrl,
        type: "post",
        datatype: "json",
        data: { "idorden": id },
        async: false,
        success: function (data) {
            if (!data.res) {
                noexiste = false;
                swal({ title: "No puede continuar", text: data.msj, type: "warning", confirmButtonText: "Aceptar" });
            }
        },
        error: function (data) {
            alert(data.Errors.toString());
        }
    });
    return noexiste;
}
function verdetalle(id) {
    $("#idcarga").val(id);
    reloadDetalle(id);
}
function reloadDetalle(id) {
    var vdataurl = $("#griddetalle").data("dataurl") + "?idvehiculo=" + id;
    $("#griddetalle").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function reloadDespachos(id) {
    var numcp = $('#numcp').val();
    var numcarga = $('#numcarga').val();

    var vdataurl = $("#griddespacho").data("dataurl") + "?idvehiculo=" + numcp + "&numcarga=" + numcarga;
    $("#griddespacho").jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function cargargrilladetallePU(id) {
    $.jgrid.defaults.height = 220;
    $.jgrid.defaults.width = 500;
    $.jgrid.defaults.responsive = true;
    var grilla = $("#griddetallepu");
    var pagergrilla = $("#griddetallepupager");

    var idvehiculo = $("#idcarga").val();

    var vdataurl = $(grilla).data("dataurl") + "?idvehiculo=" + id;

    $(grilla).jqGrid({
        url: vdataurl,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', '# Orden', 'Origen', 'Destino', 'Destinatario', 'Escaneado'],
        colModel:
        [
            { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '120', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'origen', index: 'origen', width: '100', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '100', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '120', align: 'center', classes: "grid-col", formatter: formatedit, classes: "grid-col" },
            { key: false, hidden: false, editable: false, name: 'valija', index: 'valija', width: '90', align: 'center', classes: "grid-col", formatter: formatscan, classes: "grid-col" },
        ],
        pager: $(pagergrilla),
        rowNum: 50,
        rowList: [50, 100, 150, 200],
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
    // oOperacionesDetalleTablePU =
    //    $('.dataTables-tblDetallePU').DataTable({
    //        responsive: true,
    //        dom: '<"html5buttons"B>lTfgitp',
    //        "processing": true,
    //        "serverSide": true,
    //         "searching": false,

    //         "oLanguage": {
    //             "oPaginate": {
    //                 "sPrevious": "<< Atrás",
    //                 "sNext" : "Siguiente >>",
    //                 "sFirst": "<<",
    //                 "sLast": ">>"
    //                 },
    //             "sInfo": "_START_ de _END_"
    //             ,"sLengthMenu":  "Paginación :  _MENU_"
    //             },

    //        "ajax": {
    //            "url": $('#tblDetallePU').data("url"),
    //            "data": function (d) {
    //                d.idvehiculo = id;
    //            },
    //            "type": "POST",
    //            "datatype": "json"
    //        },

    //        "columns": [
    //                {
    //                    "key": true, "title": "Id", "data": "idordentrabajo", "name": "idordentrabajo", visible: false, "autoWidth": true, "class": "text-center"

    //                },
    //                {
    //                    "title": "Item", "data": "idordentrabajo", "name": "idordentrabajo",visible: false, "autoWidth": true, "class": "text-center",
    //                    "mRender":
    //                                function (data, type, full) {
    //                                    return "<span class='label label-primary'>" + " " + data + " " + "</span>";
    //                                }
    //                },

    //                { "title": "OT", "data": "numcp", "name": "numcp", "autoWidth": true, "class": "text-center" },
    //                { "title": "Origen", "data": "origen", "name": "origen", "autoWidth": true, "class": "text-center" },
    //                { "title": "Destino", "data": "destino", "name": "destino", "autoWidth": true, "class": "text-center" },
    //                { "title": "Destinatario", "data": "destinatario", "name": "destinatario", "autoWidth": true, "class": "text-center" },
    //                {
    //                    "title": "Escaneado", "data": "valija", "name": "valija",visible: true, "autoWidth": true, "class": "text-center",
    //                    "mRender":
    //                                function (data, type, full) {
    //                                 if(data == true)
    //                                 {
    //                                    return "<span class='label label-primary'>" + " Sí " + "</span>";
    //                                 }
    //                                 else
    //                                 {
    //                                   return "<span class='label label-danger'>" + " Pendiente " + "</span>";
    //                                 }
    //                                }
    //                },

    //        ],
    //        buttons: [
    //            //{ extend: 'copy' },
    //            //{ extend: 'csv' },

    //        ]

    //    });
}
function registrarPrecinto() {
    var seleccionados = $('#AccesoriosSeleccionados').val();
    var id = $('#idvehiculo').val();

    var url = UrlHelper.Action("GuardarAsignarPrecinto", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id + "&seleccionados=" + seleccionados;

    $.ajax({
        url: url,
        type: "post",
        datatype: "json",
        data: {},
        success: function (data) {
            if (data.res) {
                swal("¡Se asignó correctamente!", data.msj, "success");
                $("#modalcontainer").modal("hide");
                reloadDespachos();
                $("#idcarga").val(id);
                reloadDetalle(id);
            } else {
                swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
            }
        },
        error: function (data) {
            alert(data.Errors.toString());
        }
    });
}

function asignarvalijas(id) {
    if (validarexistemanifiesto(id) == false)
        event.preventdefault();

    var url = UrlHelper.Action("AsignarValijas", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id;

    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        $("#btnGuardarValijas").click(function () {
            guardarValijas();
        });

        document.getElementById('txtEscanDocumento').focus();
        $('#txtEscanDocumento').bind('keyup', function (e) {
            var key = e.keyCode || e.which;
            if (key === 13) {
                validarordenvalija();
            };
        });

        cargargrilladetallePU(id);
    });
}

function salidavehiculo(id) {
    if (validarprecintosvalijas(id) == false)
        event.preventdefault();

    var url = UrlHelper.Action("SalidaVehiculo", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id;

    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        $('#horasalida').mask('00:00');

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

function validarprecintosvalijas(id) {
    var noexiste = true;
    var vUrl = UrlHelper.Action("ValidarExistePrecintosValijas", "Seguimiento", "Seguimiento") + "?idvehiculo=" + id;
    $.ajax({
        url: vUrl,
        type: "post",
        datatype: "json",
        data: {},
        async: false,
        success: function (data) {
            if (!data.res) {
                noexiste = false;
                swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
            }
        },
        error: function (data) {
            alert(data.Errors.toString());
        }
    });
    return noexiste;
}
function OnCompleteTransaction_salidavehiculo(xhr, status) {
    var jsonres = xhr.responseJSON;
    CleanValidationError();

    if (jsonres.res == true) {
        $("#modalcontainer").modal("hide");
        var id = $("#idcarga").val();
        reloadDespachos();
        reloadDetalle(id);

        swal({
            title: 'Hoja de Ruta',
            text: "¿Desea imprimir la hoja de ruta?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, Imprimirla',
        },
        function (confirm) {
            if (confirm) {
                var url = "http://104.36.166.65/webreports/hojaruta.aspx?iddespacho=" + String(jsonres.iddespacho);
                window.open(url);
            }
        })
    }
    else {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }
}
function OnCompleteTransaction(xhr, status) {
    var jsonres = xhr.responseJSON;
    CleanValidationError();

    if (jsonres.res == true) {
        $("#modalcontainer").modal("hide");

        swal({
            title: "Registro Exitoso",
            text: "Se registró correctamente el dato.",
            type: "success"
        },
        function () {
            var id = $("#idcarga").val();
            reloadDespachos();
            reloadDetalle(id);
        });
    }
    else {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }
}
function formatedit(cellvalue, options, rowObject) {
    if (cellvalue == null)
        return "";
    else
        return " " + cellvalue;
}
function displayButtonVehiculo(cellvalue, options, rowObject) {
    var asignar = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='AsignarVehiculo(" + cellvalue + ");' href='#' > <i class='fa fa-plus'></i> Asignar</button>";
    return asignar;
}
function displayButtonsOperacion(cellvalue, options, rowObject) {
    var ver = "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='verdetalle(" + cellvalue + ");' href='#' > <i class='fa fa-magic'></i></button>"
    var anular = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='anularoperacion(" + cellvalue + ");' href='#' > <i class='fa fa-trash'></i></button></div>";
    return ver;
}
function displayButtonsEstados(cellvalue, options, rowObject) {
    if (cellvalue > 0)
        return "<span class='label label-primary'>" + " Sí " + "</span>";
    else
        return "<span class='label label-info'>" + " Pendiente " + "</span>";
}
function displayButtonsManifiesto(cellvalue, options, rowObject) {
    if (cellvalue != null)
        return "<span class='label label-primary'>" + " Sí " + "</span>";
    else
        return "<span class='label label-info'>" + " Pendiente " + "</span>";
}
function formatscan(cellvalue, options, rowObject) {
    if (cellvalue > 0)
        return "<span class='label label-primary'>" + " Sí " + "</span>";
    else
        return "<span class='label label-info'>" + " No " + "</span>";
}
function displayButtonAnular(cellvalue, options, rowObject) {
    var asignar = "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='anularmanifiesto(" + cellvalue + ");' href='#' > <i class='fa fa-chain-broken'></i> Desasociar</button>";
    return asignar;
}