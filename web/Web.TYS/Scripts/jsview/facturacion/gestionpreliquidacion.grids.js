function reload() {
    var idcliente = $("#idcliente").val();
    var numerocomprobante = $("#search_numerocomprobante").val();
    var numeropreliquidacion = $("#numeropreliquidacion").val();
    var idestado = $("#idestado").val();
    var idtipocomprobante = $("#idtipocomprobante").val();

    var vdataurl = UrlHelper.Action("JsonGetListarGestionPreliquidacion", "Facturacion", "Facturacion") + "?idcliente=" + idcliente
    + "&numerocomprobante=" + numerocomprobante
    + "&idestado=" + idestado
    + "&idtipocomprobante=" + idtipocomprobante
    + "&numeropreliquidacion=" + numeropreliquidacion;

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

    var vdataedit = $(grilla).data("editurl");

    var url = UrlHelper.Action("JsonGetListarGestionPreliquidacion", "Facturacion", "Facturacion") + "?idcliente=" + idcliente
    + "&numerocomprobante=" + numerocomprobante
    + "&idestado=" + idestado
    + "&idtipocomprobante=" + idtipocomprobante
    + "&numeropreliquidacion=" + numeropreliquidacion;

    $(grilla).jqGrid({
        url: url,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'N° Liquidación', 'Tipo Comprobante', 'N° Comprobante', 'Fecha Emisión', 'Cliente', 'Bultos', 'Peso', 'Volumen', 'PesoVol'
        , 'Recargo', 'Sub Total', 'Total', 'Comprobante', 'Acciones'],
        colModel: [
           { key: true, hidden: true, name: 'idpreliquidacion', index: 'idpreliquidacion', classes: "grid-col" },
           { key: false, hidden: false, editable: false, name: 'numeropreliquidacion', index: 'numeropreliquidacion', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'tipocomprobantepago', index: 'tipocomprobantepago', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'numerocomprobante', index: 'numerocomprobante', width: '90', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'fechaemision', index: 'fechaemision', width: '90', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "d/m/Y", newformat: "d/m/Y" } },
           { key: false, hidden: false, editable: false, name: 'cliente', index: 'cliente', width: '90', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'totalbulto', index: 'totalbulto', width: '50', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'totalpeso', index: 'totalpeso', width: '40', align: 'center', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'totalvolumen', index: 'totalvolumen', width: '40', align: 'center', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'totalpesovol', index: 'totalpesovol', width: '40', align: 'center', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'recargo', index: 'recargo', width: '60', align: 'center', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'subtotal', index: 'subtotal', width: '50', align: 'center', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'total', index: 'total', width: '50', align: 'center', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: true, editable: false, name: 'idcomprobantepago', index: 'idcomprobantepago', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'idpreliquidacion', width: '60', index: 'idpreliquidacion', formatter: displayButtons, classes: "grid-col" }
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
        beforeSelectRow: function (rowid, e) {
            $(grilla).jqGrid('resetSelection');
            return (true);
        }
    });
}
function displayButtons(cellvalue, options, rowObject) {
    if (rowObject["tipocomprobantepago"] != null && rowObject["tipocomprobantepago"] != 'Boleta') {
        var eliminar = '<button type="button" class="btn btn-primary btn-xs btn-outline" onclick="irReimprimir(' + rowObject["idcomprobantepago"] + ')"><i class="fa fa-print"></i></button>';
        return eliminar;
    }
    else {
        return "";
    }
}
function irReimprimir(item) {
    var url = "http://104.36.166.65/webreports/factura.aspx?idcomprobante=" + String(item) + "&valorigv=" + 0.18;
    window.open(url);
}