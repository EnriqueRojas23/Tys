var grilla = $("#gridpreliquidacion");
var pagergrilla = $("#gridpreliquidacionpager");



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
    configchosen();
    configurarGrilla();
    configurarBotones();

});
function configurarBotones() {
    $("#btnGenerar").click(function (event) {
        var items = $(grilla).jqGrid('getGridParam', 'selarrrow');
        if (items == '')
            messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
        else
            GenerarPreliquidacion(items);
    });

    $("#btnBuscar").click(function (event) {
        reload();
    });

    $('#btnRecargo').click(function (event) {
        var selIds = $(grilla).jqGrid('getGridParam', 'selarrrow');
        if (selIds == '')
            messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
        else if (selIds.length > 1)
            messagebox('No puede continuar.', 'Debe seleccionar solo un elemento.', 'warning')
        else
            AgregarRecargo(selIds);
    });
    // $("#btnEditarComprobante").click(function(event) {
    //   var selIds =  $(grilla).jqGrid('getGridParam', 'selarrrow');
    //   if(selIds == '')
    //       messagebox('No puede continuar.','Debe seleccionar al menos un elemento.','warning');
    //   else if(selIds.length>1)
    //     messagebox('No puede continuar.','Debe seleccionar solo un elemento.','warning')
    //   else
    //    editarcomprobante(selIds);
    //
    // });
    $("#btnEditarBoleta").click(function (event) {
        var url = UrlHelper.Action("ValidarComprobante", "Facturacion", "Facturacion");

        var selIds = $(grilla).jqGrid('getGridParam', 'selarrrow');
        if (selIds == '')
            messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
        else if (selIds.length > 1)
            messagebox('No puede continuar.', 'Debe seleccionar solo un elemento.', 'warning')
        else {
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                data: { "idpreliquidacion": String(selIds) }
            })
            .done(function (data) {
                if (data.idcomprobante == 0) {
                    editarboleta(selIds);
                }
                else {
                    messagebox('No puede continuar.', 'Ya se ha generado un comprobante.', 'warning')
                }
            })
            .fail(function () {
                console.log("error");
            });
        }
    });

    $("#btnEditarPreliquidacion").click(function (event) {
        var url = UrlHelper.Action("ValidarComprobante", "Facturacion", "Facturacion");
        var urlAnular = UrlHelper.Action("JsonAnularComprobante", "Facturacion", "Facturacion");
        
        var selIds = $(grilla).jqGrid('getGridParam', 'selarrrow');
        if (selIds == '')
            messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
        else if (selIds.length > 1)
            messagebox('No puede continuar.', 'Debe seleccionar solo un elemento.', 'warning');
        else {
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                data: { "idpreliquidacion": String(selIds) }
            })
            .done(function (data) {
                if (data.idcomprobante === 0) {
                    var url = UrlHelper.Action("EditarPreliquidacion", "Facturacion", "Facturacion") + "?id=" + selIds;
                    window.location.href = url;
                }
                else {
                    //messagebox('No puede continuar.', 'Ya se ha generado un comprobante.', 'warning')
                    if (fn_validarEnvioSunat(String(selIds)) === false) {
                        console.log('');
                    }

                    swal({
                        title: "Editar Preliquidación",
                        text: "Esta operación eliminará el comprobante generado. ¿Desea continuar?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Si, eliminar",
                        cancelButtonText: "Cancelar",
                        closeOnConfirm: false,
                        closeOnCancel: true
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                $.ajax(
                                    {
                                        type: "POST",
                                        async: false,
                                        url: urlAnular,
                                        data: { "idpreliquidacion": String(selIds) },
                                        success: function (data) {
                                            if (data.res === true) {
                                                var url = UrlHelper.Action("EditarPreliquidacion", "Facturacion", "Facturacion") + "?id=" + selIds;
                                                window.location.href = url;
                                            }
                                            else {
                                                swal({ title: "¡Error!", text: data.mensaje, type: "error", confirmButtonText: "Aceptar" });
                                            }
                                        },
                                        error: function (request, status, error) {
                                            swal({ title: "¡Error!", text: "¡Ocurrió un error al guardar la evaluación!", type: "error", confirmButtonText: "Aceptar" });
                                        }
                                    });
                            }
                        });




                }
            })
            .fail(function () {
                console.log("error");
            });
        }
    });
    $("#btnAnularPreliquidacion").click(function (event) {
        var url = UrlHelper.Action("ValidarComprobante", "Facturacion", "Facturacion");

        var selIds = $(grilla).jqGrid('getGridParam', 'selarrrow');

        if (selIds == '')
            messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
        else if (selIds.length > 1)
            messagebox('No puede continuar.', 'Debe seleccionar solo un elemento.', 'warning')
        else {
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                data: { "idpreliquidacion": String(selIds) }
            })
            .done(function (data) {
                if (data.idcomprobante == 0) {
                    AnularPreliquidacion(selIds);
                }
                else {
                    messagebox('No puede continuar.', 'Ya se ha generado un comprobante.', 'warning')
                }
            })
            .fail(function () {
                console.log("error");
            });
        }
    });

    $("#btnEditarComprobante").click(function (event) {


        var url = UrlHelper.Action("ValidarComprobante", "Facturacion", "Facturacion");

        var selIds = $(grilla).jqGrid('getGridParam', 'selarrrow');
        if (selIds == '')
            messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
        else if (selIds.length > 1)
            messagebox('No puede continuar.', 'Debe seleccionar solo un elemento.', 'warning')
        else {
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                data: { "idpreliquidacion": String(selIds) }
            })
            .done(function (data) {
                if (data.idcomprobante == 0) {
                    editarcomprobante(selIds);
                }
                else {
                    messagebox('No puede continuar.', 'Ya se ha generado un comprobante.', 'warning')
                }
            })
            .fail(function () {
                console.log("error");
            });
        }
    });
}
function AgregarRecargo(idorden) {
    var url = UrlHelper.Action("AgregarRecargo", "Facturacion", "Facturacion");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        $("#idordentrabajo").val(idorden);
    })
}
function fn_validarEnvioSunat(ids) {
    var URL_EnvioSunat = UrlHelper.Action("ValidarEnvioSunat_preliquidacion", "Facturacion", "Facturacion");
    let valido = false;

    $.ajax({
        url: URL_EnvioSunat,
        type: 'POST',
        dataType: 'json',
        data: { "idpreliquidacion": ids },
        async: false
    })
        .done(function (data) {
            if (data.res) {
                valido = true
            }
        })
        .fail(function () {
            console.log("error");
        });
    return valido
}
function GenerarPreliquidacion(items) {
    var url = UrlHelper.Action("JsonGenerarPreliquidacion", "Facturacion", "Facturacion");

    swal({
        title: "Generar Preliquidación",
        text: "¿Está seguro que desea generar la preliquidación?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Generar',
        closeOnConfirm: true,
        closeOnCancel: true
    },
       function (isConfirm) {
           if (isConfirm) {
               $.ajax({
                   url: url,
                   type: 'POST',
                   dataType: 'json',
                   data: { 'ordenes': String(items) }
               })
               .done(function () {
                   reload();
                   messagebox('Registro Exitoso', 'Se ha generado la preliquidación.', 'success');
               })
               .fail(function () {
                   messagebox('No se pudo generar', 'No se pudo registrar la Preliquidación', 'error');
               })
           }
       });
}
function configchosen() {
    var config = {
        '.chosen-select': {
            max_selected_options: 5,
            allow_single_deselect: false,
            no_results_text: 'Oops, no se encontró el ubigeo!'
        }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }
}
function OnCompleteTransaction_Recargo(xhr, status) {
    var jsonres = xhr.responseJSON;
    if (jsonres.res == true) {
        swal({
            title: "Registro Exitoso",
            text: "Se registró correctamente el dato.",
            type: "success"
        },
        function () {
            $("#modalcontainer").modal("hide");
            reload();
        });
    }
    else {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }
}
function editarcomprobante(idpreliquidacion) {



    var url = UrlHelper.Action("PreFactura", "Facturacion", "Facturacion") + "?idpreliquidacion=" + idpreliquidacion;
    var url2 = UrlHelper.Action("JsonGenerarComprobante", "Facturacion", "Facturacion");

    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal('show');

        $("#strsubtotal").blur(function () {
            $("#strigv").val(($("#strsubtotal").val() * 0.18).toFixed(2));
            $("#strtotal").val((parseFloat($("#strsubtotal").val()) + parseFloat($("#strigv").val())).toFixed(2));
        })


        $("#addrow").click(function () {
            $("#griddetallefactura").jqGrid('addRowData', 0, 1, "last");
            $("#griddetallefactura").editRow(0, true);
        });




        $("#idnumerodocumento").change(function () {
            var url3 = UrlHelper.Action("JsonObtenerNumeroDocumento", "Facturacion", "Facturacion") + "?id=" + $("#idnumerodocumento").val();

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

        configurarGrillaDetalle()

        $("#txtdescripcion").val($("#descripcion").val());
        $("#btnGenFactura").click(function (event) {
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
                else {
                    swal("No puede continuar", data.msj , "warning");

                    // if ($("#strsubtotal").val() == "")
                    //     swal("No puede continuar", "Debe ingresar un subtotal", "warning");
                    // if ($("#idnumerodocumento").val() == "")
                    //     swal("No puede continuar", "Debe seleccionar una serie", "warning");
                    // if ($("#fecharegistro").val() == "")
                    //     swal("No puede continuar", "Debe ingresar una fecha", "warning");
                }
            })
            .fail(function () {
                console.log("error");
            })
        });
        $(".editor").jqte({
        });
    });
}
function editarboleta(idpreliquidacion) {
    var url = UrlHelper.Action("PreBoleta", "Facturacion", "Facturacion") + "?idpreliquidacion=" + idpreliquidacion;
    var url2 = UrlHelper.Action("JsonGenerarBoleta", "Facturacion", "Facturacion");
    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal('show');

        configurarGrillaDetalle()

        $("#addrow").click(function () {
            $("#griddetallefactura").jqGrid('addRowData', 0, 1, "last");
            $("#griddetallefactura").editRow(0, true);
        });


        $("#idnumerodocumento").change(function () {
            var url3 = UrlHelper.Action("JsonObtenerNumeroDocumento", "Facturacion", "Facturacion") + "?id=" + $("#idnumerodocumento").val();

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
function AnularPreliquidacion(item) {
    var url = UrlHelper.Action("JsonAnularPreliquidacion", "Facturacion", "Facturacion") + "?idpreliquidacion=" + item;

    swal({
        title: "Anular Preliquidación",
        text: "¿Está seguro que desea anular la preliquidación?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Anular',
        closeOnConfirm: true,
        closeOnCancel: true
    },
       function (isConfirm) {
           if (isConfirm) {
               $.ajax({
                   url: url,
                   type: 'POST',
                   dataType: 'json',
                   data: {}
               })
               .done(function () {
                   reload();
                   messagebox('Registro Exitoso', 'Se ha anulado la preliquidación.', 'success');
               })
               .fail(function () {
                   messagebox('No se pudo generar', 'No se pudo anular la Preliquidación', 'error');
               })
           }
       });
}
$("#btnReporte").click(function (event) {
    var selIds = $(grilla).jqGrid('getGridParam', 'selarrrow');

    if (selIds == '')
        messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
    else if (selIds.length > 1)
        messagebox('No puede continuar.', 'Debe seleccionar solo un elemento.', 'warning')
    else {
        var url = "http://104.36.166.65/webreports/preliquidacion.aspx?idpreliquidacion=" + String(selIds);
        window.open(url);
    }
});

function configurarGrillaDetalle() {




    var grilladetalle = $("#griddetallefactura");
    var pagergrilladetalle = $("#griddetallefacturapager");

    $.jgrid.defaults.width = 1000;
    $.jgrid.defaults.height = 100;

    var idpreliquidacion = $("#idpreliquidacion").val();

    var vdataedit = $(grilladetalle).data("editurl");
    var url = UrlHelper.Action("GetListarDetalleFacturacion", "Facturacion", "Facturacion") + "?idpreliquidacion=" + idpreliquidacion;


    $(grilladetalle).jqGrid({
        url: url,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Cantidad', 'Unidad Medida', 'Descripción', 'Valor Unitario','', 'Acciones'],
        colModel: [
           { key: true, hidden: true, name: 'iddetallecomprobante', index: 'iddetallecomprobante', classes: "grid-col" },
           { key: false, hidden: false, editable: false, name: 'cantidad', index: 'cantidad', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'unidadmedida', index: 'unidadmedida', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: true,editrules: {maxlength: 250, required: true}, name: 'descripcion', index: 'descripcion', width: '490', align: 'center', classes: "grid-col", formatter: formatedit },
           {
               key: false, hidden: false, editable: true, editrules: { custom_func :mypricecheck, custom:true,} ,name: 'subtotal', index: 'subtotal', width: '90', align: 'center', classes: "grid-col", formatter: 'number', formatoptions: { decimalPlaces: 2 }
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
        loadComplete : function () {
                 sumarTotales()
          }
    });


}
function mypricecheck(value, colname) {

if (value <= 0 )
   return [false,"Ingrese un valor mayor a 0"];
else
   return [true,""];
}

function displayButtons2(cellvalue, options, rowObject) {


    var editar = "<button type='button' title='Editar' class='btn btn-success btn-xs btn-outline' onclick=\"$('#griddetallefactura').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
    var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#griddetallefactura').saveRow('" + options.rowId + "') ;      reload2();        \";><i class='fa fa-save'></i> </button>";
    var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
    var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#griddetallefactura').restoreRow('" + options.rowId + "'); reload2(); \"><i class='fa fa-times-circle'></i> </button>";

    return "<div class='btn-group'>" + editar + guardar + control + restore + "</div>" ;
}

function reload2() {



    var idpreliquidacion = $("#idpreliquidacion").val();
    var url = UrlHelper.Action("GetListarDetalleFacturacion", "Facturacion", "Facturacion") + "?idpreliquidacion=" + idpreliquidacion;
    $('#griddetallefactura').jqGrid('setGridParam', { url: url }).trigger('reloadGrid');


}
function sumarTotales(){


   let colSum = $('#griddetallefactura').jqGrid('getCol', 'subtotal', false, 'sum');

   let  subtotal = numberWithCommas(colSum);
   let igv = colSum * 0.18;
   let total = colSum + igv;


   $("#strsubtotal").val(numberWithCommas(colSum));
   $("#strigv").val(numberWithCommas(igv));
   $("#strtotal").val(numberWithCommas(total));






}




function irEliminar(id) {
     let URL_EliminarDetalle = UrlHelper.Action("EliminarDetalleFactura","Facturacion","Facturacion")



    $.ajax({
        url: URL_EliminarDetalle,
        type: 'POST',
        dataType: 'json',
        data: { id : id},
    })
    .done(function() {
        reload2()
    })
    .fail(function() {
        console.log("error");
    })






}
