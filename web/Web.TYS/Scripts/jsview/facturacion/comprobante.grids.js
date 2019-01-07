
var rowsToColor = [];

function reload() {
    let idcliente = $("#idcliente").val();
    let numerocomprobante = $("#search_numerocomprobante").val();
    let numeropreliquidacion = $("#numeropreliquidacion").val();
    let idestado = $("#idestado").val();
    let idtipocomprobante = $("#idtipocomprobante").val();
    let fecinicio = $('#fechainicio').val();
    let fecfin = $('#fechafin').val();

    var vdataurl = UrlHelper.Action("JsonGetListarComprobantes", "Facturacion", "Facturacion") + "?idcliente=" + idcliente
    + "&numerocomprobante=" + numerocomprobante
    + "&idestado=" + idestado
    + "&idtipocomprobante=" + idtipocomprobante
    + "&numeropreliquidacion=" + numeropreliquidacion
    + "&fecinicio=" + fecinicio + "&fecfin=" + fecfin;

    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function configurarGrilla() {
    $.jgrid.defaults.width = 200;
    $.jgrid.defaults.height = 300;

    var idcliente = $("#idcliente").val();
    var numerocomprobante = $("#search_numerocomprobante").val();
    var idestado = $("#idestado").val();
    var idtipocomprobante = $("#idtipocomprobante").val();
    var numeropreliquidacion = $("#numeropreliquidacion").val();
    let fecinicio = $('#fechainicio').val();
    let fecfin = $('#fechafin').val();

    var vdataedit = $(grilla).data("editurl");

    var url = UrlHelper.Action("JsonGetListarComprobantes", "Facturacion", "Facturacion") + "?idcliente=" + idcliente
    + "&numerocomprobante=" + numerocomprobante
    + "&idtipocomprobante=" + idtipocomprobante
    + "&numeropreliquidacion=" + numeropreliquidacion
    + "&fecinicio=" + fecinicio + "&fecfin=" + fecfin;

    $(grilla).jqGrid({
        url: url,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'N° Comprobante', 'N° Liquidación', 'Tipo Comprobante', 'Fecha Emisión', 'Cliente', 'Bultos', 'Peso', 'Volumen'
        , 'Sub Total', 'Total', 'N° Comprobante Vinculado', 'Sunat', '' , ''],
        colModel: [
           { key: true, hidden: true, name: 'idcomprobantepago', index: 'idcomprobantepago', classes: "grid-col" },
           { key: false, hidden: false, editable: false, name: 'numerocomprobante', index: 'numerocomprobante', width: '60', align: 'center', classes: "grid-col", formatter: rowColorFormatter },
           { key: false, hidden: false, editable: false, name: 'numeropreliquidacion', index: 'numeropreliquidacion', width: '60', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'tipocomprobantepago', index: 'tipocomprobantepago', width: '60', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'fechaemision', index: 'fechaemision', width: '70', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "d/m/Y", newformat: "d/m/Y" } },
           { key: false, hidden: false, editable: false, name: 'cliente', index: 'cliente', width: '150', align: 'left', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'totalbulto', index: 'totalbulto', width: '30', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'totalpeso', index: 'totalpeso', width: '40', align: 'center', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'totalvolumen', index: 'totalvolumen', width: '40', align: 'center', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'subtotal', index: 'subtotal', width: '60', align: 'right', classes: "grid-col",formatter:'currency',  formatoptions: {prefix:'S/. ', suffix:'', thousandsSeparator:',' , decimalSeparator: '.'} },
           { key: false, hidden: false, editable: false, name: 'total', index: 'total', width: '60', align: 'right', classes: "grid-col", formatter:'currency', formatoptions: {prefix:'S/. ', suffix:'', thousandsSeparator:',' , decimalSeparator: '.'}},
           { key: false, hidden: false, editable: false, name: 'numerocomprobantevinculada', index: 'numerocomprobantevinculada', width: '60', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'estadosunat', index: 'estadosunat', width: '60', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: true, editable: false, name: 'idcomprobantepago', index: 'idcomprobantepago', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'idcomprobantepago', width: '60', index: 'idcomprobantepago', formatter: displayButtons, classes: "grid-col" }
             //{ key: false, hidden: false, editable: false ,name: 'idpreliquidacion', width:'100' , index: 'idpreliquidacion' ,  formatter:  displayButtons2,classes:"grid-col"}
        ],
        pager: $(pagergrilla),
        rowNum: 30,
        rowList: [30, 60, 90],
        autoResizing: { compact: true },
        emptyrecords: 'No se encontraron registros',
        autowidth: true,
        viewrecords: true,
        autoheight: true,
        multiselect: true,
        addParams: {
            position: "last",
            addRowParams: editOptionsNew
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
          gridComplete: function () {
                for (var i = 0; i < rowsToColor.length; i++) {
                    var status = $("#" + rowsToColor[i]).find("td").eq(2).html();
                    if (status !== "5") {
                        $("#" + rowsToColor[i]).find("td").eq(2).css("background-color", "#0288D1");
                        $("#" + rowsToColor[i]).find("td").eq(2).css("font-size", "10px");
                        $("#" + rowsToColor[i]).find("td").eq(2).css("color", "white");


                        $("#" + rowsToColor[i]).find("td").eq(11).css("color", "#303F9F");
                        $("#" + rowsToColor[i]).find("td").eq(11).css("font-size", "10px");
                        //$("#" + rowsToColor[i]).find("td").eq(2).css("color", "white");



                    }
                    else {
                        $("#" + rowsToColor[i]).find("td").eq(2).css("background-color", "#048d06");
                        $("#" + rowsToColor[i]).find("td").eq(2).css("color", "white");
                    }

                }
             },
        beforeSelectRow: function (rowid, e) {
            $(grilla).jqGrid('resetSelection');
            return true;
        }
    });
}
function displayButtons(cellvalue, options, rowObject) {
    //if (rowObject["tipocomprobantepago"] != null && rowObject["tipocomprobantepago"] != 'Boleta') {
        if (rowObject["tipocomprobantepago"] !== null) {
        var eliminar = '<button type="button" class="btn btn-primary btn-xs btn-outline" onclick="irReimprimir(' + rowObject["idcomprobantepago"] + ')"><i class="fa fa-print"></i></button>';
        return eliminar;
    }
    else {
        return "";
    }
}
function irReimprimir(item) {
    var url = "http://104.36.166.65/webreports/facturaelectronica.aspx?idcomprobante=" + String(item);
    window.open(url);
}
function configurarGrillaDetalleFactura() {

  alert('entre')
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
        mtype: 'Post',
        colNames: ['', 'Cantidad', 'Unidad Medida', 'Descripción', 'Valor Unitario', '', 'Acciones'],
        colModel: [
           { key: true, hidden: true, name: 'iddetallecomprobante', index: 'iddetallecomprobante', classes: "grid-col" },
           { key: false, hidden: false, editable: false, name: 'cantidad', index: 'cantidad', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'unidadmedida', index: 'unidadmedida', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: true, editrules: { maxlength: 250, required: true }, name: 'descripcion', index: 'descripcion', width: '490', align: 'center', classes: "grid-col", formatter: formatedit },
           {
               key: false, hidden: false, editable: true, editrules: { custom_func: mypricecheck, custom: true }, name: 'subtotal', index: 'subtotal', width: '90', align: 'center', classes: "grid-col", formatter: 'number', formatoptions: { decimalPlaces: 2 }
                 , editoptions: {
                     dataInit: function (element) {
                         $(element).keypress(function (e) {
                             var resp = validateFloatKeyPress(this, e);
                             if (resp === false)
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
            return true;
        },
        loadComplete: function () {
            sumarTotales();
        }
    });


}
function configurarGrillaDetalle() {
    alert('entre')
    var grilladetalle = $("#griddetallefactura");
    var pagergrilladetalle = $("#griddetallefacturapager");

    $.jgrid.defaults.width = 1000;
    $.jgrid.defaults.height = 100;

    var idpreliquidacion = $("#idpreliquidacion").val();

    var vdataedit = $(grilladetalle).data("editurl");
    var url = UrlHelper.Action("GetListarDetalleNotaCredito", "Facturacion", "Facturacion") + "?idpreliquidacion=" + idpreliquidacion;


    $(grilladetalle).jqGrid({
        url: url,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'Cantidad', 'Unidad Medida', 'Descripción', 'Valor Unitario', '', 'Acciones'],
        colModel: [
           { key: true, hidden: true, name: 'iddetallecomprobante', index: 'iddetallecomprobante', classes: "grid-col" },
           { key: false, hidden: false, editable: false, name: 'cantidad', index: 'cantidad', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'unidadmedida', index: 'unidadmedida', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: true, editrules: { maxlength: 250, required: true }, name: 'descripcion', index: 'descripcion', width: '490', align: 'center', classes: "grid-col", formatter: formatedit },
           {
               key: false, hidden: false, editable: true, editrules: { custom_func: mypricecheck, custom: true }, name: 'subtotal', index: 'subtotal', width: '90', align: 'center', classes: "grid-col", formatter: 'number', formatoptions: { decimalPlaces: 2 }
                 , editoptions: {
                     dataInit: function (element) {
                         $(element).keypress(function (e) {
                             var resp = validateFloatKeyPress(this, e);
                             if (resp === false)
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
        beforeSelectRow: function (_rowid, _e) {
            $(grilladetalle).jqGrid('resetSelection');
            return true;
        },
        loadComplete: function () {
            sumarTotales();
        }
    });


}
function mypricecheck(value, _colname) {

    if (value <= 0)
        return [false, "Ingrese un valor mayor a 0"];
    else
        return [true, ""];
}
function displayButtons2(cellvalue, options, rowObject) {
    var editar = "<button type='button' title='Editar' class='btn btn-success btn-xs btn-outline' onclick=\"$('#griddetallefactura').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
    var guardar = "<button type='button' title='Guardar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#griddetallefactura').saveRow('" + options.rowId + "') ;      reload2();        \";><i class='fa fa-save'></i> </button>";
    var control = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
    var restore = "<button type='button' title='Cancelar' class='btn btn-danger btn-xs btn-outline' onclick=\"$('#griddetallefactura').restoreRow('" + options.rowId + "'); reload2(); \"><i class='fa fa-times-circle'></i> </button>";

    return "<div class='btn-group'>" + editar + guardar + control + restore + "</div>";
}
function sumarTotales() {


    let colSum = $('#griddetallefactura').jqGrid('getCol', 'subtotal', false, 'sum');

    let subtotal = numberWithCommas(colSum);
    let igv = colSum * 0.18;
    let total = colSum + igv;


    $("#strsubtotal").val(numberWithCommas(colSum));
    $("#strigv").val(numberWithCommas(igv));
    $("#strtotal").val(numberWithCommas(total));


}
function reload2() {


    var grilladetalle = $("#griddetallefactura");
    var pagergrilladetalle = $("#griddetallefacturapager");

    var idcomprobante = $("#idcomprobantepago").val();
    var url = UrlHelper.Action("GetListarDetalleFacturacion", "Facturacion", "Facturacion") + "?idcomprobante=" + idcomprobante;
    grilladetalle.jqGrid('setGridParam', { url: url }).trigger('reloadGrid');


}
function rowColorFormatter(cellValue, options, rowObject) {

    rowsToColor[rowsToColor.length] = options.rowId;
    return cellValue;
}