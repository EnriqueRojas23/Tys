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

    $("#subtotal_label").html(String(parseFloat($("#strsubtotal").val()).toFixed(2)));
    $("#igv_label").html(String(parseFloat($("#strigv").val()).toFixed(2)));
    $("#total_label").html(String(parseFloat($("#strtotal").val()).toFixed(2)));

    $("#peso_label").html(String(parseFloat($("#totalpeso").val()).toFixed(2)));
    $("#volumen_label").html(String(parseFloat($("#totalvolumen").val()).toFixed(2)));
    $("#bultos_label").html(String(parseFloat($("#totalbulto").val()).toFixed(2)));

    $("#cantidadot_label").html($("#cantidad").val());
    $("#recargos_label").html(String(parseFloat($("#recargo").val()).toFixed(2)));
});
function configurarBotones() {
    $("#btnEliminar").click(function (event) {
        var items = $(grilla).jqGrid('getGridParam', 'selarrrow');
        if (items == '')
            messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
        else
            EliminarOTs(items);
    });
    $("#btnBuscar").click(function (event) {
        reload();
    });

    $("#btnAgregar").click(function (event) {
        AgregarOt();
    });

    $("#btnReporte").click(function (event) {
        var id = $("#idpreliquidacion").val();
        var url = "http://104.36.166.65/webreports/preliquidacion.aspx?idpreliquidacion=" + String(id);
        window.open(url);
    });
    $("#btnRegresar").click(function (event) {
        var url = UrlHelper.Action("GestionPreliquidacion", "Facturacion", "Facturacion");
        window.location.href = url;
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
}

function AgregarOt() {
    var idpreliquidacion = $("#idpreliquidacion").val();
    var url = UrlHelper.Action("AgregarOtsPreliquidacion", "Facturacion", "Facturacion")
    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal("show");
        configurarGrillaP();

        $("#btnAgregarOrdenes").click(function (event) {
            var selIds = $("#gridpreliquidacionp").jqGrid('getGridParam', 'selarrrow');
            if (selIds == '')
                messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
            else agregarOtsPopUP(selIds);
        });
    });
}
function AgregarRecargo(idorden) {
    var idpre = $("#idpreliquidacion").val()

    var url = UrlHelper.Action("AgregarRecargo", "Facturacion", "Facturacion");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");

        $("#idordentrabajo").val(idorden);
        $("#idestado").val(idpre);
    })
}

function EliminarOTs(items) {
    var url = UrlHelper.Action("JsonElminarOts", "Facturacion", "Facturacion") + "?ids=" + String(items) + "&idpreliquidacion=" + $("#idpreliquidacion").val();

    swal({
        title: "Eliminar Ordenes de Transporte",
        text: "¿Está seguro que desea eliminar las ordenes seleccionadas?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Eliminar',
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
               .done(function (data) {
                   if (data.res) {
                       reload();

                       //  $("#subtotal_label").html(data.subtotal);
                       //  $("#igv_label").html(data.igv);
                       //  $("#total_label").html(data.total);
                       //  $("#peso_label").html(data.peso);
                       //  $("#volumen_label").html(data.volumen);
                       //  $("#bultos_label").html(data.bultos);
                       //  $("#cantidadot_label").html(data.cantidad);
                       //  $("#recargos_label").html(data.recargo).val();

                       $("#subtotal_label").html(String(parseFloat(data.subtotal).toFixed(2)));
                       $("#igv_label").html(String(parseFloat(data.igv).toFixed(2)));
                       $("#total_label").html(String(parseFloat(data.total).toFixed(2)));
                       $("#peso_label").html(String(parseFloat(data.peso).toFixed(2)));
                       $("#volumen_label").html(String(parseFloat(data.volumen).toFixed(2)));
                       $("#bultos_label").html(String(parseFloat(data.bultos).toFixed(2)));
                       $("#cantidadot_label").html(data.cantidad);
                       $("#recargos_label").html(String(parseFloat(data.recargo).toFixed(2)));

                       if (data.cantidad === 0) {    
                           var url = UrlHelper.Action("GestionPreliquidacion", "Facturacion", "Facturacion");
                           window.location.href = url;
                       }

                       messagebox('Registro Exitoso', 'Se ha eliminado las OTs.', 'success');
                   }
               })
               .fail(function () {
                   messagebox('No se pudo generar', 'No se pudo eliminar las OTs', 'error');
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

            $("#subtotal_label").html(String(parseFloat(jsonres.subtotal).toFixed(2)));
            $("#igv_label").html(String(parseFloat(jsonres.igv).toFixed(2)));
            $("#total_label").html(String(parseFloat(jsonres.total).toFixed(2)));
            $("#peso_label").html(String(parseFloat(jsonres.peso).toFixed(2)));
            $("#volumen_label").html(String(parseFloat(jsonres.volumen).toFixed(2)));
            $("#bultos_label").html(String(parseFloat(jsonres.bultos).toFixed(2)));
            $("#cantidadot_label").html(jsonres.cantidad);
            $("#recargos_label").html(String(parseFloat(jsonres.recargo).toFixed(2)));

            reload();
        });
    }
    else {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }
}
function agregarOtsPopUP(items) {
    var url = UrlHelper.Action("JsonAgregarOtsPopUP", "Facturacion", "Facturacion");
    var idpreliquidacion = $("#idpreliquidacion").val();
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        data: { "ids": String(items), "idpreliquidacion": idpreliquidacion }
    })
    .done(function (data) {
        if (data.res) {
            $('#modalcontainerL').modal('hide');

            $("#subtotal_label").html(String(parseFloat(data.subtotal).toFixed(2)));
            $("#igv_label").html(String(parseFloat(data.igv).toFixed(2)));
            $("#total_label").html(String(parseFloat(data.total).toFixed(2)));
            $("#peso_label").html(String(parseFloat(data.peso).toFixed(2)));
            $("#volumen_label").html(String(parseFloat(data.volumen).toFixed(2)));
            $("#bultos_label").html(String(parseFloat(data.bultos).toFixed(2)));
            $("#cantidadot_label").html(data.cantidad);
            $("#recargos_label").html(String(parseFloat(data.recargo).toFixed(2)));
            messagebox("Registro Exitoso", "Se ha agregado las OTs seleccionadas", "success");
            reload();
        }
    })
    .fail(function () {
        console.log("error");
    })
}