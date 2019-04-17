function reload() {
    var idcliente = $("#idcliente").val();
    var iddestino = $("#iddestino").val();
    var numcp = $("#numcp").val();

    var vdataurl = UrlHelper.Action("JsonGetListarPreliquidacion", "Facturacion", "Facturacion")
    + "?idcliente=" + idcliente
    + "&iddestino=" + iddestino
    + "&numcp=" + numcp;

    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}
function configurarGrilla() {
    $.jgrid.defaults.height = 600;

    var idcliente = $("#idcliente").val();
    var iddestino = $("#iddestino").val();
    var numcp = $("#numcp").val();
    var url = UrlHelper.Action("JsonGetListarPreliquidacion", "Facturacion", "Facturacion") + "?idcliente=" + idcliente + "&iddestino=" + iddestino + "&numcp=" + numcp;

    $(grilla).jqGrid({
        url: url,
        datatype: 'json',
        mtype: 'Get',
        colNames: ['', 'OT', 'Fecha OT',  'Remitente', 'Destinatario', 'Modo Transporte', 'Origen'
        , 'Destino', 'Tarifa', 'Bul.', 'Peso', 'Vol', 'PesoVol', 'Base', 'Sub Total', 'Recar.', 'SubTotal Final'],
        colModel: [
           { key: true, hidden: true, name: 'idordentrabajo', index: 'idordentrabajo', classes: "grid-col" },
           { key: false, hidden: false, editable: false, name: 'numcp', index: 'numcp', width: '70', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'fecharegistro', index: 'fecharegistro', width: '70', align: 'center', classes: "grid-col", formatter: 'date', formatoptions: { srcformat: "d/m/Y", newformat: "d/m/Y" } },
           //{ key: false, hidden: false, editable: false, name: 'guiatransportista', index: 'guiatransportista', width: '60', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'remitente', index: 'remitente', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'destinatario', index: 'destinatario', width: '80', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'modotransporte', index: 'modotransporte', width: '70', align: 'center', classes: "grid-col", formatter: formatedit },
           //{ key: false, hidden: false, editable: false, name: 'conceptocobro', index: 'conceptocobro', width: '70', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'origen', index: 'origen', width: '90', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'destino', index: 'destino', width: '90', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'tarifa', index: 'tarifa', width: '40', align: 'right', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'bulto', index: 'bulto', width: '40', align: 'center', classes: "grid-col", formatter: formatedit },
           { key: false, hidden: false, editable: false, name: 'peso', index: 'peso', width: '40', align: 'right', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'volumen', index: 'volumen', width: '50', align: 'right', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'totalpesovol', index: 'totalpesovol', width: '40', align: 'center', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'base1', index: 'base1', width: '40', align: 'right', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'subtotal', index: 'subtotal', width: '50', align: 'right', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'recargo', index: 'recargo', width: '40', align: 'right', classes: "grid-col", formatter: ToFixed },
           { key: false, hidden: false, editable: false, name: 'total', index: 'total', width: '50', align: 'right', classes: "grid-col", formatter: ToFixed },

        ],
        pager: $(pagergrilla),
        rowNum: 500,
        rowList: [500, 1000, 1500],
        autoResizing: { compact: true },
        emptyrecords: 'No se encontraron registros',
        viewrecords: true,
        autoheight: true,
        editable: true,
        shrinkToFit: true,
        multiselect: true,
        jsonReader:
        {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: 0
        },
    })
}
function displayButtons(cellvalue, options, rowObject) {
    var eliminar = '<button type="button" class="btn btn-warning btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
    return eliminar;
}