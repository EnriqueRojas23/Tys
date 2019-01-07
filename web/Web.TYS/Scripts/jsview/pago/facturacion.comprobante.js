 btnNuevo = "#btnNuevo";
var gridlistaots = "#gridlistaots";
var gridlistaotspager = "#gridlistaotspager";

$(document).ready(function () {
 $(btnNuevo).click(function (event) { btnBuscarDocumento_onclick(this, event); });
 $("#btnBuscar").button()
                   .click(function (e) {
                       oDocumentosTable.draw();
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

     $("#serienumero").keypress(function (event) {
                if (event.which == 13) {
                    $("#btnBuscar").click();
                }
            });

    CargaListaComprobante();
    inicializandoEventosModalDocumentos();










});
$(document).keydown(function (e) {
    if (e.which == 81 && e.ctrlKey)
       $("#btnNuevo").click();

});

function CargaListaComprobante() {
    
    oDocumentosTable =
       $('.dataTables-tblComprobante').DataTable({
           responsive: true,
           dom: '<"html5buttons"B>lTfgitp',
           "processing": true,
           "serverSide": true,
           "oLanguage": {
                "oPaginate": {
                    "sPrevious": "<< Atrás",
                    "sNext" : "Siguiente >>",
                    "sFirst": "<<",
                    "sLast": ">>"
                    },
                "sSearch" : "Búsqueda:"
                ,"sInfo": "_START_ de _END_" 
                ,"sLengthMenu":  "Paginación :  _MENU_"
                },



           "ajax": {
               "url": $('#tblComprobante').data("url"),
               "data": function (d) {
                   d.serie = $('#serienumero').val();
                   d.fecinicio = $('#fechainicio').val();
                   d.fecfinal = $('#fechafin').val();
               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idcomprobante", "name": "idcomprobante", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                   {
                       "title": "Item", "data": "idcomprobante", "name": "idcomprobante",visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                   function (data, type, full) {
                                       return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                   }
                   },
                   { "title": "N° Doc", "data": "serienumero", "name": "serienumero", "autoWidth": true, "class": "text-center" },
                   { "title": "F. Registro","format" :   'dd-MM-YYYY', "data": "fecharegistro", "name": "fecharegistro", "autoWidth": true, "class": "text-center" },
                   { "title": "F. Comprobante", "data": "fechacomprobante", "name": "fechacomprobante", "autoWidth": true, "class": "text-center" },
                   { "title": "Moneda", "data": "moneda", "name": "moneda", "autoWidth": true, "class": "text-center" },
                   { "title": "Monto", "data": "monto", "name": "monto", "autoWidth": true, "class": "text-center" },
                   { "title": "Tipo", "data": "tipocomprobante", "name": "tipocomprobante", "autoWidth": true, "class": "text-center" },
                   { "title": "Etapa", "data": "etapa", "name": "etapa", "autoWidth": true, "class": "text-center" },
                   { "title": "Modo", "data": "tipotransporte", "name": "tipotransporte", "autoWidth": true, "class": "text-center" },
                   { "title": "Proveedor", "data": "razonsocial", "name": "razonsocial", "autoWidth": true, "class": "text-center" },
                   { "title": "Origen", "data": "Origen", "name": "Origen", "autoWidth": true, "class": "text-center" },
                   { "title": "Destino", "data": "Destino", "name": "Destino", "autoWidth": true, "class": "text-center" },
                   { "title": "OTS", "data": "ots", "name": "ots", "autoWidth": true, "class": "text-center" },
                   {
                       "title": "Acciones", "class": "text-center", "data": "idcomprobante", "Width": "15%", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarcomprobante(" + data + ");' href='#' > <i class='fa fa-edit'></i> </button>" 
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='eliminarcomprobante(" + data + ");' href='#' > <i class='fa fa-trash'></i> </button></div>";
                         
                        }
                   },

           ],
           buttons: [
               //{ extend: 'copy' },
               //{ extend: 'csv' },
               { extend: 'excel', title: 'Listado de Comprobantes', exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               { extend: 'pdf', title: 'Listado de Comprobantes' , exportOptions: { columns: [ 2,3,4,5,6 ,7,8,9,10,11] } },
               
           ]

       });
}

function btnBuscarDocumento_onclick(obj, event) {

    
    var url = UrlHelper.Action("AgregarComprobanteModal", "Facturacion", "Facturacion")


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontentL").html(data);
        $("#modalcontainerL").modal("show");

        $('#dtpfechacomp .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd/mm/yyyy'
    });
        //inicializandoEventosModalDocumentos();
        configurarGrillaOT();

    });
}

function OnCompleteTransaction(xhr, status)
{
    var jsonres = xhr.responseJSON;
    CleanValidationError();

    if (jsonres.res == true)
    {
       swal({
           title: "Registro Exitoso",
           text: "Se registró correctamente el Comprobante.",
            type: "success"
        },
       function ()
       {
           $("#modalcontainerL").modal("hide");
           //var vurl = $('#btnAsociar').data("urlbak");
           //window.location.href = vurl;
           oDocumentosTable.draw();
       });
     
    }
    else
    {
        sweetAlert(jsonres.mensaje, null, "error");
        //CheckValidationErrorResponse(jsonres);
    }

}
function editarcomprobante(id)
{
    var url = UrlHelper.Action("EditarComprobante", "Facturacion", "Facturacion") + "?id=" + id;
    window.location.href = url;
 
}
function inicializandoEventosModalDocumentos(id)
{

     $('#dtpfechacomp .input-group.date').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        autoclose: true,
        format: 'dd/mm/yyyy',
        setDate: '10/12/2012'
        
    });
    //$(btnBuscarPersona).click(function (event)   { btnBuscarPersona_onclick(this, event); });
    $('#hdidmoneda').val(id);
     var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-results': { no_results_text: 'Oops, no se encontró el ubigeo!' }
        }

        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

     ChangeComboProveedor();
     //BeginDropDownlist();

}


function btnBuscarPersona_onclick(obj, event) {

  var url = UrlHelper.Action("AgregarOTModal", "Facturacion", "Facturacion") ;
    $.get(url, function (data)
    {
        $("#modalcontentot").html(data);
        $("#modalcontainerot").modal("show");
        configurarGrillaOT();
        
    });
    
}
function eliminarcomprobante(id)
{
    var vUrl = UrlHelper.Action("EliminarComprobante", "Facturacion", "Facturacion") + "?id=" + id;
    swal({
        title: "Eliminar Comprobante",
        text: "¿Está seguro que desea eliminar este Comprobante?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Eliminar',
        closeOnConfirm: false,
        closeOnCancel: true
    },
       function (isConfirm) {
           if (isConfirm) {
               $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {
                            swal("¡Se ha eliminado correctamente!", data.msj, "success");
                           $("#modalcontainer").modal("hide");
                            oDocumentosTable.draw();

                       } else {
                           swal({ title: "Error", text: data.msj , type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       alert(data.Errors.toString());
                   }
               });
           }
     });
}
function cerrarAgregarOT()
{
   $("#modalcontainerot").modal("hide"); 
}
function configurarGrillaOT() {



    var grilla = $(gridlistaots);
    var pagergrilla = $(gridlistaotspager);
    //var nombre = $('#NombreRol').val();
    var vdataurl = $(grilla).data("dataurl") ; //+ '?nom=' + ;


    $.jgrid.defaults.width = 400;
    $.jgrid.defaults.height = 1460;
    $.jgrid.defaults.responsive = true;



    $(grilla).jqGrid({
        url: vdataurl,
        responsive:true,
        datatype: 'json',
        mtype: 'POST',
        colNames: ['','OT', 'Bultos','Peso', 'Valoriz.','Acciones',],
        colModel:
        [
            { key: true, hidden: true, name: 'PKID', index: 'PKID' },
            { key: false, hidden: false, name: 'NumCp', index: 'NumCp', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'TotalBultos', index: 'TotalBultos', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'TotalPeso', index: 'TotalPeso', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'Precio', index: 'Precio', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'PKID', index: 'PKID', width: '100', align: 'center', formatter: bottonasignarrol_formatter }

        ],
        jsonReader: CONFIG_JQGRID.get('jsonReader'),
        pager: $(gridlistaotspager),
        rowNum: 10,
        rowList: [10, 20],
        emptyrecords: 'No se encontraron registros',
        autoheight: true,
        responsive : true,
        //autowidth: true,
        shrinkToFit: true,
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

}


function bottonasignarrol_formatter(cellvalue, options, rowObject) {
    var acciones = $("<div class='btn-group'></div>");

    var control = $("<button type='button'></button>");
    control.append("<i class='fa fa-trash-o'></i>")
    control.attr("title", "Eliminar OT");
    control.addClass("btn btn-primary btn-xs btn-outline")
    control.attr("id", "lnk" + cellvalue);
    control.attr("onclick", "javascript:irEliminarOT('" + cellvalue + "')");

    // var btnGridM = $("#templatemod").clone(false)[0];
    // $(btnGridM).attr("id", "btngrid_mod_" + cellvalue);
    // $(btnGridM).attr("onclick", "irModificarRol(this, " + cellvalue + ")");
    // $(btnGridM).show();



    // var btnGridT = $("#templatetrash").clone(false)[0];
    // $(btnGridT).attr("id", "btngrid_trash_" + cellvalue);
    // $(btnGridT).attr("onclick", "eliminarRol(this, " + cellvalue + ")");
    // $(btnGridT).show();


    acciones.append(control);
    //acciones.append(btnGridT.outerHTML);
    //acciones.append(btnGridM.outerHTML);

    var htmlcontrol = acciones[0].outerHTML;
    return htmlcontrol
}
function BuscarOTPopUp_onClick() {


   var id = $('#txtnumcp').val();
  var vUrl = UrlHelper.Action("AgregarOT", "Facturacion", "Facturacion");
   $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {

                    var grilla = $("#gridlistaots");
                    //var tipodocumento = $("#txtnumcp").val().trim();
                    var numdocumento = $("#txtnumcp").val();
                    var vdataurl = $(grilla).data("dataurl") + "?numcp=" + numdocumento ;

                    $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');

                    $('#ots').append( $('<option></option>').val(data.ids).html(data.value) ) ;
                    $('#txtnumcp').val("");

                   
                       } else {
                           swal({ title: "Error", text: data.msj, type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       alert(data.Errors.toString());
                   }
               });

}
function irEliminarOT(id)
{

  var vUrl = UrlHelper.Action("ElimnarOT", "Facturacion", "Facturacion");
   $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {

                      var grilla = $("#gridlistaots");
                      var numdocumento = $("#txtnumcp").val();
                      var vdataurl = $(grilla).data("dataurl") + "?numcp=" + numdocumento ;
                      $(grilla).jqGrid('setGridParam', { url: vdataurl }).trigger('reloadGrid');



                      event.preventDefault();
                      // var selectobject = $('#ots');

                      //   for (var i=0; i<=selectobject.length; i++){
                            
                      //        if (String(selectobject[0].options[i].value) == String(id) )
                      //        {
                      //            selectobject[0].remove(i);
                      //        }
                      //   }
                                                            

                   
                       } else {
                           swal({ title: "Error", text: data.msj , type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       
                   }
               });




}        
function CancelarOTP_onClick()
{
      $("#modalcontainernew").modal("hide"); 
}
function GuardarOTPopUp_onClick()
{
     $("#modalcontainerot").modal("hide");
     
}


function ChangeComboProveedor() {

    $("#idproveedor").chosen().change(function () {


       
    })
    
    $("#idvehiculo").chosen().change(function () {


       
    })
}


function agregarProveedor()
{
  var url = UrlHelper.Action("AgregarProveedorModalHelp", "Facturacion", "Facturacion") ;
    $.get(url, function (data)
    {
        $("#modalcontentnew").html(data);
        $("#modalcontainernew").modal("show");
        //nfigurarGrillaOT();
        
    });  
}

function Guardar_NuevoProveedor()
{
        var ruc=$("#ruc").val(); 
        var razon   = $("#razonsocial").val();
        var url = UrlHelper.Action("AgregarProveedorAux", "Facturacion", "Facturacion") ;


        $.ajax(
            {
                type: "POST",
                async: true,
                url: url,
                data: { "ruc": ruc , "razonsocial" : razon },
                success: function (data) {
                        $("#modalcontentnew").modal("hide"); 
                    
                },
                error: function (request, status, error) {
                    swal({ title: "Error!", text: "¡No se pudo cargar los datos de la dirección!", type: "error", confirmButtonText: "Aceptar" });
                }
            });
}


