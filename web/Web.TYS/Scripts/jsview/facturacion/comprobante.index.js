var grilla = $("#gridcomprobantes");
var pagergrilla = $("#gridcomprobantespager");
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

    configchosen();
    configurarGrilla();
    configurarBotones();

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
});
function configurarBotones() {
    $("#btnBuscar").click(function (event) {
        reload();
    });


 function fn_validarTipoComprobante(ids){
     var URL_TipoComprobante = UrlHelper.Action("ValidarTipoComprobante", "Facturacion", "Facturacion");
     let valido = false;

      $.ajax({
                url: URL_TipoComprobante,
                type: 'POST',
                dataType: 'json',
                data: { "idcomprobante":ids },
                async: false
            })
            .done(function (data) {
                if (data.res) {
                    valido = true
                }
                else {
                    messagebox('No puede continuar.', data.msj, 'warning')
                }
            })
            .fail(function () {
                console.log("error");
            });
      return valido
 }
 function fn_validarEnvioSunat(ids){
     var URL_EnvioSunat = UrlHelper.Action("ValidarEnvioSunat", "Facturacion", "Facturacion");
     let valido = false;

      $.ajax({
                url: URL_EnvioSunat,
                type: 'POST',
                dataType: 'json',
                data: { "idcomprobante":ids },
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

    $("#btnGenerarNota").click(function (event) {

        var selIds = $(grilla).jqGrid('getGridParam', 'selarrrow');
        if (selIds == '')
            messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
        else if (selIds.length > 1)
            messagebox('No puede continuar.', 'Debe seleccionar solo un elemento.', 'warning')
        else{

                if(fn_validarTipoComprobante(String(selIds)) == true)
                     if(fn_validarEnvioSunat(String(selIds)) == false)
                        generarNC(selIds)
                    else messagebox('No puede continuar', "El documento aún no ha sido enviado a SUNAT, edite el documento.", 'warning')

        }
    });



    $("#btnEditarComprobante").click(function (event) {
        var selIds = $(grilla).jqGrid('getGridParam', 'selarrrow');
        if (selIds == '')
            messagebox('No puede continuar.', 'Debe seleccionar al menos un elemento.', 'warning');
        else if (selIds.length > 1)
            messagebox('No puede continuar.', 'Debe seleccionar solo un elemento.', 'warning')
        else {
               if(fn_validarEnvioSunat(String(selIds)) == true)
                    editarcomprobante(selIds);
              else
                     messagebox('No puede continuar', 'El documento ya fue enviado a SUNAT', 'warning')
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
function editarcomprobante(idcomprobante) {
    var URL_EditarComprobante = UrlHelper.Action("EditarComprobante", "Facturacion", "Facturacion") + "?idcomprobante=" + idcomprobante;
    var url2 = UrlHelper.Action("JsonEditarComprobante", "Facturacion", "Facturacion");

    $.get(URL_EditarComprobante, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal('show');


        $("#addrow").click(function () {
            $("#griddetallefactura").jqGrid('addRowData', 0, 1, "last");
            $("#griddetallefactura").editRow(0, true);
        });


        configurarGrillaDetalleFactura()

        $("#btnActualizarComprobante").click(function (event) {
            $.ajax({
                url: url2,
                type: 'POST',
                dataType: 'json',
                data: {
                    idcomprobante: $("#idcomprobantepago").val()
                    , subtotal: $("#strsubtotal").val()
                    , igv: $("#strigv").val()
                }
            })
            .done(function (data) {
                if (data.res) {
                    var id = data.idcomprobante;
                    $("#modalcontainerL").modal("hide");
                    reload();
                }
                else {
                    swal("No puede continuar", data.msj, "warning");
                }
            })
            .fail(function () {
                console.log("error");
            })
        });

    });
}

function generarNC(idcomprobante) {

   


    var url = UrlHelper.Action("PreNotaCredito", "Facturacion", "Facturacion") + "?idcomprobantepago=" + idcomprobante;
    var URL_GenerarNC = UrlHelper.Action("JsonGenerarNotaCredito", "Facturacion", "Facturacion");

    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal('show');
        $("#txtdescripcion").val($("#descripcion").val());

        $("#strsubtotal").blur(function (event) {
            if ($("#strsubtotal").val() == '') return;
            calcularigvtotal();
        });

        $("#addrow").click(function () {
            $("#griddetallefactura").jqGrid('addRowData', 0, 1, "last");
            $("#griddetallefactura").editRow(0, true);
        });



        configurarGrillaDetalle()

        $("#btnGenNC").click(function (event) {

            $.ajax({
                url: URL_GenerarNC,
                type: 'POST',
                dataType: 'json',
                data:
                {    idcomprobante: $("#idcomprobantepago").val()
                    , subtotal: $("#strsubtotal").val()
                    , igv: $("#strigv").val()
                    , __descripcion: $("#txtdescripcion").val()
                    , idmotivoanulacion : $("#idmotivoanulacion").val()
                }
            })
            .done(function (data) {
                var id = data.idcomprobante;
                $("#modalcontainerL").modal("hide");
                reload();
                var url = "http://104.36.166.65/webreports/facturaelectronica.aspx?idcomprobante=" + String(id) + "&valorigv=" + data.valorigv;
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

function calcularigvtotal() {
    //var igv = 0.18;
    $("#strigv").val((parseFloat($("#strsubtotal").val()) * igv).toFixed(2));
    $("#strtotal").val((parseFloat($("#strsubtotal").val()) + parseFloat($("#strigv").val())).toFixed(2));
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
function reload2() {

    var idpreliquidacion = $("#idpreliquidacion").val();
    var url = UrlHelper.Action("GetListarDetalleFacturacion", "Facturacion", "Facturacion") + "?idpreliquidacion=" + idpreliquidacion;
    $('#griddetallefactura').jqGrid('setGridParam', { url: url }).trigger('reloadGrid');


}
