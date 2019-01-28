$(document).ready(function () {
    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-results': { no_results_text: 'Oops, no se encontró el ubigeo!' }
    }

    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }

    BeginDropDownlist();

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

    $("#btnBuscarPendienteDespacho").click(function (event) {
        reportependientedespacho();
    });
    $("#btnBuscarPendienteEntrega").click(function (event) {
        reportependienteentrega();
    });

    $("#btnBuscarPendienteRecepcion").click(function (event) {
        reportependienterecepcion();
    });
    $("#btnBuscarPendientexDespachar").click(function (event) {
        reportependientexdespachar();
    });
    $("#btnBuscarComercial").click(function (event) {
        reportecomercial();
    });
    $("#btnBuscarGeneral").click(function (event) {
        reportegeneral();
    });
    $("#btnBuscarProduccionCliente").click(function (event) {
        reporteProduccionCliente();
    });
    $("#btnBuscarPendienteFacturar").click(function (event) {
        reportePendienteFacturar();
    });
    $("#btnBuscarProdvsDestino").click(function (event) {
        reporteProdvsDestino();
    });

    $("#btnBuscarMonitoreoFluvial").click(function (event) {
        MonitoreoFluvial();
    });
    $("#btnBuscarProdFacturacion").click(function (event) {
        reporteProdvsFact();
    });
    $("#btnBuscarConciEntre").click(function (event) {
        reporteconciliaentrega();
    });
    $("#btnBuscarLiqDocumentaria").click(function (event) {
        reporteliqdocumentaria();
    });
    $("#btnBuscarRechazo").click(function (event) {
        ReporteRechazo();
    });
});
function reportePendienteFacturar() {
    var idcliente = $("#idcliente").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    //var idestado = $("#idestado").val();

    var url = "http://104.36.166.65/webreports/pendientefacturacion.aspx?idcliente=" + idcliente
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    //+ "&idestado=" + idestado;

    window.open(url);
}

function MonitoreoFluvial() {
    var idcliente = $("#idcliente").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idusuario = $("#idusuario").val();
    var placa = $("#placa").val();
    var codtienda = $("#codtienda").val();
    var embarcacion = $("#embarcacion").val();
    var idestado = $("#idestado").val();

    var url = "http://104.36.166.65/webreports/monitoreofluvial.aspx?idcliente=" + idcliente
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idestado=" + idestado
    + "&placa=" + placa
    + "&codtienda=" + codtienda
    + "&embarcacion=" + embarcacion
    + "&idusuario=" + idusuario;

    window.open(url);
}
function reportecomercial() {
    var idcliente = $("#idcliente").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idusuario = $("#idusuario").val();

    //if (idcliente == '') {
    //    messagebox("Faltan datos", "Debe seleccionar a un cliente", "warning");
    //    return;
    //}

    var url = "http://104.36.166.65/webreports/reportecomercial.aspx?idcliente=" + idcliente
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idusuario=" + idusuario;

    window.open(url);
}

function reporteProduccionCliente() {
    var idcliente = $("#idcliente").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idunidadmedida = $("#idunidadmedida").val()

    if (idunidadmedida == '') {
        messagebox("Faltan datos", "Debe seleccionar una unidad de medida", "warning");
        return;
    }

    var url = "http://104.36.166.65/webreports/produccioncliente.aspx?idcliente=" + idcliente
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idunidadmedida=" + idunidadmedida;
    window.open(url);
}

function reportependientexdespachar() {
    var idcliente = $("#idcliente").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();

    var url = "http://104.36.166.65/webreports/pendientepordespachar.aspx?idcliente=" + idcliente
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin;

    window.open(url);
}

function reportependienterecepcion() {
    var grr = $("#grr").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idusuario = $("#idusuario").val();
    var idestacionorigen = $("#idestacionorigen").val();
    var idestaciondestino = $("#idestaciondestino").val();
    var idcliente = $("#idcliente").val();
    var numcp = $("#numcp").val();
    var nummanifiesto = $("#nummanifiesto").val();
    var iddistrito = $("#iddistrito").val();

    var url = "http://104.36.166.65/webreports/pendienterecepcion.aspx?"
    + "fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idusuario=" + idusuario
    + "&idestacionorigen=" + idestacionorigen
    + "&idestaciondestino=" + idestaciondestino
    + "&idcliente=" + idcliente
    + "&numcp=" + numcp
    + "&iddistrito=" + iddistrito

    window.open(url);
}
function reportependienteentrega() {
    var grr = $("#grr").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idusuario = $("#idusuario").val();
    var idestacionorigen = $("#idestacionorigen").val();
    var idcliente = $("#idcliente").val();
    var numcp = $("#numcp").val();
    var nummanifiesto = $("#nummanifiesto").val();

    var url = "http://104.36.166.65/webreports/pendienteentrega.aspx?grr=" + grr
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idusuario=" + idusuario
    + "&idestacionorigen=" + idestacionorigen
    + "&idcliente=" + idcliente
    + "&numcp=" + numcp
    + "&nummanifiesto=" + nummanifiesto

    window.open(url);
}

function reportependientedespacho() {
    var iddistrito = $("#iddistrito").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idusuario = $("#idusuario").val();
    var idestacionorigen = $("#idestacionorigen").val();
    var idcliente = $("#idcliente").val();

    var url = "http://104.36.166.65/webreports/pendientedespacho.aspx?iddistrito=" + iddistrito
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idusuario=" + idusuario
    + "&idestacionorigen=" + idestacionorigen
    + "&idcliente=" + idcliente

    window.open(url);
}
function reportegeneral() {
    var idcliente = $("#idcliente").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idusuario = $("#idusuario").val();
    var idestacionorigen = $("#idestacionorigen").val();

    var url = "http://104.36.166.65/webreports/reportegeneral.aspx?idcliente=" + idcliente
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idusuario=" + idusuario
    + "&idestacionorigen=" + idestacionorigen;

    window.open(url);
}

function BeginDropDownlist() {
    $("#iddepartamento").change(function () {
        var iddepartamento = $("#iddepartamento").val();
        var url = UrlHelper.Action("ListarProvincias", "Seguimiento", "Seguimiento");
        $.ajax(
                     {
                         type: "POST",
                         async: true,
                         url: url,
                         data: { "iddepartamento": iddepartamento },
                         success: function (data) {
                             var $select = $('#idprovincia');
                             $select.empty();
                             $("#idprovincia").append('<option value="">[Provincia]</option>');
                             $.each(data, function (i, state) {
                                 $('<option>', {
                                     value: state.Value
                                 }).html(state.Text).appendTo($select);
                             });
                         },
                         error: function (request, status, error) {
                             swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                         }
                     });
    });
    $("#idprovincia").change(function () {
        var id = $("#idprovincia").val();

        var idprovincia = $("#idprovincia").val();
        var url = UrlHelper.Action("JsonGetListarDistritos", "Reporte", "Reporte");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url,
                   data: { "idprovincia": idprovincia },
                   success: function (data) {
                       var $select = $('#iddistrito');
                       $select.empty();
                       $("#iddistrito").append('<option value="">[Distrito]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                   },
                   error: function (request, status, error) {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
    });
}
function reporteProdvsDestino() {
    var iddistrito = $("#iddistrito").val();
    var idprovincia = $("#idprovincia").val();
    var iddepartamento = $("#iddepartamento").val();

    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();

    var idunidadmedida = $("#idunidadmedida").val();
    var idtipotransporte = $("#idtipotransporte").val();
    var idcliente = $("#idcliente").val();

    if (idunidadmedida === '') {
        messagebox("Faltan datos", "Debe seleccionar una unidad de medida", "warning");
        return;
    }
    var url = "http://104.36.166.65/webreports/produccionvsdestino.aspx?iddistrito=" + iddistrito
        + "&idprovincia=" + idprovincia
        + "&iddepartamento=" + iddepartamento
        + "&fecinicio=" + fecinicio
        + "&fecfin=" + fecfin
        + "&idunidadmedida=" + idunidadmedida
        + "&idtipotransporte=" + idtipotransporte
        + "&idcliente=" + idcliente;

    window.open(url);
}
function reporteProdvsFact() {
    var idcliente = $("#idcliente").val();
    var anio = $("#anio").val();
    

    //if(idcliente=='')
    //{
    //    messagebox("Faltan datos" , "Debe seleccionar un cliente." , "warning");
    //    return;
    //  }

    var url = "http://104.36.166.65/webreports/reporteproduccionvsproduccion.aspx?idcliente=" + idcliente
        + "&anio=" + anio;
    //+ "&fecfin=" + fecfin;

    window.open(url);
}
function reporteconciliaentrega() {
    var idcliente = $("#idcliente").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idusuario = $("#idusuario").val();

    var url = "http://104.36.166.65/webreports/reporteconciliacionentrega.aspx?idcliente=" + idcliente
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idusuario=" + idusuario;

    window.open(url);
}
function reporteliqdocumentaria() {
    var idcliente = $("#idcliente").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idusuario = $("#idusuario").val();
    var grr = $("#grr").val();
    var numcp = $("#numcp").val();

    var url = "http://104.36.166.65/webreports/reporteliqdocumentaria.aspx?idcliente=" + idcliente
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idusuario=" + idusuario
    + "&grr=" + grr
    + "&numcp=" + numcp

    window.open(url);
}
function ReporteRechazo() {
    var idcliente = $("#idcliente").val();
    var fecinicio = $("#fecinicio").val();
    var fecfin = $("#fecfin").val();
    var idusuario = $("#idusuario").val();

    if (idcliente == '') {
        messagebox("Faltan datos", "Debe seleccionar un cliente.", "warning");
        return;
    }

    var url = "http://104.36.166.65/webreports/reporterechazo.aspx?idcliente=" + idcliente
    + "&fecinicio=" + fecinicio
    + "&fecfin=" + fecfin
    + "&idusuario=" + idusuario;

    window.open(url);
}