 btnNuevo = "#btnNuevo";
var gridlista = "#gridlistareporte";
var gridlistapager = "#gridlistareportepager";

$(document).ready(function () {
    //$(btnNuevo).click(function (event) { btnBuscarDocumento_onclick(this, event); });
configurarChosenSelect();
 $("#btnBuscar").button()
                   .click(function (e) {
                  var fechainicio = $('#fechainicio').val();
                  var fechafin = $('#fechafin').val();
                  var idproveedor = $('#idproveedor').val();
                  var iddestino = $('#iddestino').val();


                   var url = UrlHelper.Action("BuscarReporteGerencial", "facturacion", "facturacion") + "?iddestino=" +  iddestino +"&idproveedor=" + idproveedor +"&fechainicio=" + fechainicio+"&fechafin=" + fechafin


              //  var url = $(obj).data("url");
                $.get(url, function (data) {
                    $("#modalcontentL").html(data);
                    $("#modalcontainerL").modal("show");


                });


                   });
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

 configurarGrillaOT();


});


function configurarGrillaOT() {


    var grilla = $(gridlista);
    var pagergrilla = $(gridlistapager);

    var vdataurl = $(grilla).data("dataurl") ; //+ '?nom=' + ;


    $.jgrid.defaults.width = 1000;
    $.jgrid.defaults.height = 500;
    $.jgrid.defaults.responsive = true;



    $(grilla).jqGrid({
        url: vdataurl,
        responsive:true,
        datatype: 'json',
        mtype: 'POST',
        colNames: ['Etapa','Proveedor', 'Destino','Costo','KGS','C.U.', 'Producción','Rentabilidad','%'],
        colModel:
        [
            
            { key: false, hidden: false, name: 'etapa', index: 'etapa', width: '240', align: 'left'},
            { key: false, hidden: false, name: 'proveedor', index: 'proveedor', width: '220', align: 'left' },
            { key: false, hidden: false, name: 'destino', index: 'destino', width: '80', align: 'left'},
            { key: false, hidden: false, name: 'costo', index: 'costo', width: '120', align: 'left',formatter:'number' ,summaryTpl : 'Total:({0})' ,summaryType:'sum'},
            { key: false, hidden: false, name: 'kgs', index: 'kgs', width: '120', align: 'left',formatter:'number',summaryTpl : 'Total:({0})' ,summaryType:'sum'},
            { key: false, hidden: false, name: 'cu', index: 'cu', width: '80', align: 'left' ,formatter:'number'},
            { key: false, hidden: false, name: 'produccion', index: 'produccion', width: '120', align: 'left',formatter:'number',summaryTpl : 'Total:({0})' ,summaryType:'sum'},
            { key: false, hidden: false, name: 'rentabilidad', index: 'rentabilidad', width: '120', align: 'left' ,formatter:'number',summaryTpl : 'Total:({0})',summaryType:'sum'},
            { key: false, hidden: false, name: 'porcentaje', index: 'porcentaje', width: '50', align: 'left' },

        ],
        jsonReader: CONFIG_JQGRID.get('jsonReader'),
        pager: $(gridlistapager),
        rowNum: 10,
        rowList: [10, 20],
        emptyrecords: 'No se encontraron registros',
        autoheight: true,
        responsive : true,
        //autowidth: true,
        shrinkToFit: true,
        grouping:true,
        groupingView : {

                groupField : ['etapa', 'proveedor'],
                groupColumnShow: [true,true],
                groupCollapse: true,
                //groupText : ['<b>{0} - {1} Elemento(s)</b>'],
                groupText : ['<b>{0}</b> Costo Total: {costo}', '{0} Costo Total: {costo}'],
                groupSummary : [true],
                showSummaryOnHide: true

         },
          footerrow: true,
        beforeRequest: function () {
            var $self = $(this);
            var postData = $self.jqGrid('getGridParam', 'postData');
            $.each(postData, function (index, value) {
                if (value.name == "rows") {
                    postData[index].value = postData.rows;
                }
                if (value.name == "page") {
                    postData[index].value = postData.page;
                }
                if (value.name == "sord") {
                    postData[index].value = postData.sord;
                }
            })
            $self.jqGrid('setGridParam', { postData: postData });
        },

    });
      $(grilla).jqGrid('navButtonAdd', '#pager', {
            caption: "", buttonicon: "ui-icon-print", title: "Excel Export",
            onClickButton: function () {
                            $.post("/Documents/ExportToExcel", {}, function () {

                            });
            }
        });

    

}
var createExcelFromGrid = function(gridID,filename) {
    var grid = $(gridlista);
    var rowIDList = grid.getDataIDs();
    var row = grid.getRowData(rowIDList[0]); 
    var colNames = [];
    var i = 0;
    for(var cName in row) {
        colNames[i++] = cName; // Capture Column Names
    }
    var html = "";
    for(var j=0;j<rowIDList.length;j++) {
        row = grid.getRowData(rowIDList[j]); // Get Each Row
        for(var i = 3 ; i<colNames.length ; i++ ) {
            html += row[colNames[i]] + ';'; // Create a CSV delimited with ;
        }
        html += '\n';
    }
    html += '\n';

    var a         = document.createElement('a');
    a.id = 'ExcelDL';
    a.href        = 'data:application/vnd.ms-excel,' + html;
    a.download    = filename ? filename + ".xls" : 'ReporteGerencial.xls';
    document.body.appendChild(a);
    a.click(); // Downloads the excel document
    document.getElementById('ExcelDL').remove();
}
function exportarReporte() {
      var grilla = $("#gridlistareporte");
      var fechainicio = $('#fechainicio').val();
      var fechafin = $('#fechafin').val();
      var proveedor = $('#idproveedor').val();
      var iddestino = $('#iddestino').val();
     var vdataurl =  UrlHelper.Action("ExportarReporte", "Facturacion", "Facturacion") +  "?fecinicio=" + fechainicio + "&fecfinal=" + fechafin + "&idproveedor=" + proveedor + "&iddestino=" + iddestino ;
    $(window).attr("location", vdataurl);
}
function configurarChosenSelect()
{
    
     var config = {
            '.chosen-select': {
                  max_selected_options: 5,
                 placeholder_text_multiple: "Seleccione Conceptos",
                 no_results_text: 'Oops, no se encontró el dato!' 
               }
        }

        for (var selector in config) {

            $(selector).chosen(config[selector]);
        }


       

        
}
