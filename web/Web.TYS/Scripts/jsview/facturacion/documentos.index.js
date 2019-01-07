var grilla = $("#griddocumentos");
var pagergrilla = $("#griddocumentospager");
var editOptionsNew = {
        keys: true,
        successfunc: function () {
            var $self = $(this);
            setTimeout(function () {
                $self.trigger("reloadGrid");
            }, 50);
        }
    };
$(document).ready(function() {

    configurarGrilla();
    $("#addrow").click( function() {
                 $("#griddocumentos").jqGrid('addRowData',0,1,"first");
                 $("#griddocumentos").editRow(0,true);
});
});

function reload()
{
    var idtipocomprobante = $("#idtipocomprobante").val();
    var vdataurl = UrlHelper.Action("JsonGetListarDocumentos","Facturacion", "Facturacion") + "?idtipocomprobante=" + idtipocomprobante;
    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');
}

function  configurarGrilla()
{

  $.jgrid.defaults.width = 200;
  $.jgrid.defaults.height = 300;


  var idtipocomprobante = $("#idtipocomprobante").val();
  var vdataedit = $(grilla).data("editurl");

  var url = UrlHelper.Action("JsonGetListarDocumentos","Facturacion", "Facturacion") + "?idtipocomprobante=" + idtipocomprobante;


  $(grilla).jqGrid({
     url: url,
     datatype: 'json',
     mtype: 'Get',
     colNames: ['', 'Tipo Documento', 'Serie','Inicio', 'Ult. Impreso', 'Usuario' , 'Estacion' ,'Acciones'],
     colModel: [
        { key: true, hidden: true, name: 'idnumerodocumento', index: 'idnumerodocumento' ,classes:"grid-col" },
        { key: false, hidden: false, editable: true ,edittype: "select",name: 'tipodocumento', index: 'tipodocumento', width: '80', align: 'center' , classes:"grid-col",formatter: formatedit , editoptions: { dataUrl: fcnUrlControlGrid('tipodocumento')} },
        { key: false, hidden: false, editable: true ,name: 'serie', index: 'serie', width: '80', align: 'center' , classes:"grid-col",formatter: formatedit },
        { key: false, hidden: false, editable: true ,name: 'primernumero', index: 'primernumero', width: '90', align: 'center' , classes:"grid-col",formatter: formatedit },
        { key: false, hidden: false, editable: true ,name: 'ultimonumero', index: 'ultimonumero', width: '90', align: 'center' , classes:"grid-col",formatter: formatedit },
        { key: false, hidden: false, editable: true   , edittype: "select" ,name: 'usuario', index: 'usuario', width: '90', align: 'center' , classes:"grid-col",formatter: formatedit  , editoptions: { dataUrl: fcnUrlControlGrid('usuario')}},
        { key: false, hidden: false, editable: true, edittype: "select" ,name: 'estacionorigen', index: 'estacionorigen', width: '90', align: 'center' , classes:"grid-col",formatter: formatedit , editoptions: { dataUrl: fcnUrlControlGrid('estacion')}},
      //  { key: false, hidden: false, editable: false ,name: 'idpreliquidacion', width:'60' , index: 'idpreliquidacion' ,  formatter:  displayButtons,classes:"grid-col"}
          { key: false, hidden: false, editable: false ,name: 'idnumerodocumento', width:'100' , index: 'idnumerodocumento' ,  formatter:  displayButtons,classes:"grid-col"}
     ],
     pager: $(pagergrilla),
     rowNum: 30,
     rowList: [30, 60, 90],
     autoResizing : { compact : true},
     emptyrecords: 'No se encontraron registros',
     autowidth: true,
     viewrecords: true,
     editable:true,
     autoheight: true,
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
   });
}


function displayButtons(cellvalue, options, rowObject)
{
        var editar = "<div class='btn-group'><button type='button' title='Editar' class='btn btn-primary btn-outline btn-xs ' onclick=\"$('#griddocumentos').editRow('" + options.rowId + "')\";><i class='fa fa-edit'></i> </button>";
        var guardar = "<button type='button' title='Guardar' class='btn btn-primary btn-outline btn-xs ' onclick=\"$('#griddocumentos').saveRow('" + options.rowId + "') ;      reload();         \";><i class='fa fa-save'></i> </button>";
        var control = '<button type="button" class="btn btn-primary btn-xs btn-outline" onclick="irEliminar(' + cellvalue + ')"><i class="fa fa-trash"></i></button>';
        var restore = "<button type='button' title='Cancelar' class='btn btn-primary btn-xs btn-outline' onclick=\"$('#griddocumentos').restoreRow('" + options.rowId + "'); reload(); \"><i class='fa fa-times-circle'></i> </button></div>";

        return   editar + guardar + control + restore;
}
