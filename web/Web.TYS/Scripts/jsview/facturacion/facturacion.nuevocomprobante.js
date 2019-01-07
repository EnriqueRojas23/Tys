 btnNuevo = "#btnNuevo";
var gridlistaots = "#gridlistaots";
var gridlistaotspager = "#gridlistaotspager";

$(document).ready(function () {
 
 


      $('#dtpfechacomp .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });


      $('#dtpfechaven .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });


      
    inicializandoEventosModalDocumentos();
    BeginDropDownlist();
    configurarGrillaOT();
    CargaListaots();



});
function inicializandoEventosModalDocumentos()
{
    
     var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-results': { no_results_text: 'Oops, no se encontró el ubigeo!' }
        }

        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

        ChangeComboProveedor();
}
function ChangeComboProveedor() {

    $("#idproveedor").chosen().change(function () {



    });

    $("#idvehiculo").chosen().change(function () {


       
    })


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
           
           var url = UrlHelper.Action("NuevoComprobante", "Pago", "Pago");
            window.location.href = url;

          setTimeout(function(){

            $('html, body').animate({ scrollTop: 0 }, 'fast');
          }, 4200 ); 

       });
     
    }
    else
    {
        sweetAlert(jsonres.mensaje, null, "error");
        //CheckValidationErrorResponse(jsonres);
    }

}



function BeginDropDownlist()
{


   $("#idtipofacturacion").change(function ()
    {
        var val = $("#idtipofacturacion").val();
        if(val == 86)
        {
          $("#idetapa").attr("required", false);
          $("#idvehiculo").attr("required", false);
          $("#divacta").css('display','');
        }
        else
        {
          $("#divacta").css('display','none');
        }


        if(val == 99)
        {
           $("#idorigen").attr("required", false);
           $("#iddestino").attr("required", false);
           $("#divfecvencimiento").css('display','');
           $("#divot").css('display','none');
        }
        else
        {
            $("#idorigen").attr("required", true);
            $("#idproveedor").attr("required", true);
            $("#iddestino").attr("required", true);
            $("#divfecvencimiento").css('display','none');
            $("#divot").css('display','');
        }

       if(val == 99)
       {

           $("#idetapa").attr("required", false);
           $("#idtipotransporte").attr("required", false);
           $("#idtipocomprobante").attr("required", false);

           var url = UrlHelper.Action("ListarMonedaVarios", "Pago", "Pago");
           $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { },
                   success: function (data) {
                       var $select = $('#idmoneda');
                       $select.empty();
                       $("#idmoneda").append('<option value="">[Moneda]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });

                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });

           var urlaux = UrlHelper.Action("ListarTipoComprobanteVarios", "Pago", "Pago");

               $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: urlaux ,
                   data: {  },
                   success: function (data) {
                       var $select = $('#idtipocomprobante');
                       $select.empty();
                       $("#idtipocomprobante").append('<option value="">[Tipo Comprobante]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                          var length = $('#idtipocomprobante > option').length;
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });

          }





        });



    $("#idproveedor").change(function ()
    {
        var idproveedor = $("#idproveedor").val();
        var urlinit = UrlHelper.Action ("ListarPlacas","Pago","Pago");

        $.ajax(
                { 
                 type: "POST",
                 async: true,
                 url: urlinit ,
                 data: { "idproveedor": idproveedor },
                 success: function (data) {
                       var $select = $('#idvehiculo');
                       $select.empty();
                       $("#idvehiculo").append('<option value="">[Placa]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                    $('#idvehiculo').trigger("chosen:updated");

                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }

                });





        var url = UrlHelper.Action("ListarEtapas", "Pago", "Pago");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "idproveedor": idproveedor },
                   success: function (data) {
                       var $select = $('#idetapa');
                       $select.empty();
                       $("#idetapa").append('<option value="">[Etapa]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                        var length = $('#idetapa > option').length;
                        if(length==2)
                        {
                           $select[0].selectedIndex = 1;
                           $select.change();
                        }


                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
    });


    $("#idetapa").change(function ()
    {
        var idetapa = $("#idetapa").val();
       var idproveedor = $("#idproveedor").val();
        var url = UrlHelper.Action("ListarTipoTransporte", "Pago", "Pago");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "idetapa": idetapa , "idproveedor" :idproveedor},
                   success: function (data) {
                       var $select = $('#idtipotransporte');
                       $select.empty();
                       $("#idtipotransporte").append('<option value="">[Modo Transporte]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                       var length = $('#idtipotransporte > option').length;
                          if(length==2)
                          {
                           $select[0].selectedIndex = 1;
                           $select.change();
                          }
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
    });
     $("#idtipotransporte").change(function ()
    {
        var idtipotransporte = $("#idtipotransporte").val();
        var idetapa = $("#idetapa").val();
       var idproveedor = $("#idproveedor").val();
       var url = UrlHelper.Action("ListarTipoComprobante", "Pago", "Pago");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "idtipotransporte": idtipotransporte , "idproveedor" :idproveedor ,"idetapa": idetapa },
                   success: function (data) {
                       var $select = $('#idtipocomprobante');
                       $select.empty();
                       $("#idtipocomprobante").append('<option value="">[Tipo Comprobante]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                          var length = $('#idtipocomprobante > option').length;
                          if(length==2)
                          {
                           $select[0].selectedIndex = 1;
                           $select.change();
                          }
                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
    });
   $("#idtipocomprobante").change(function ()
    {


        if($('#idtipofacturacion').val() == 99)
          return ;
        var idtipocomprobante = $("#idtipocomprobante").val();
        var idtipotransporte = $("#idtipotransporte").val();
        var idetapa = $("#idetapa").val();
       var idproveedor = $("#idproveedor").val();

       var url = UrlHelper.Action("ListarMoneda", "Pago", "Pago");
        $.ajax(
               {
                   type: "POST",
                   async: true,
                   url: url ,
                   data: { "idtipocomprobante": idtipocomprobante ,"idtipotransporte": idtipotransporte , "idproveedor" :idproveedor ,"idetapa": idetapa},
                   success: function (data) {
                       var $select = $('#idmoneda');
                       $select.empty();
                       $("#idmoneda").append('<option value="">[Moneda]</option>');
                       $.each(data, function (i, state) {
                           $('<option>', {
                               value: state.Value
                           }).html(state.Text).appendTo($select);
                       });
                          var length = $('#idmoneda > option').length;
                          if(length==2)
                          {
                           $select[0].selectedIndex = 1;
                           $select.change();
                          }


                   },
                   error: function (request, status, error)
                   {
                       swal({ title: "¡Error!", text: "¡Ha ocurrido un error, intentelo mas tarde!", type: "error", confirmButtonText: "Aceptar" });
                   }
               });
    });
}
function btnBuscarPersona_onclick(obj, event) {

    var url = UrlHelper.Action("AgregarOTModal", "Pago", "Pago");
    $.get(url, function (data)
    {
        $("#modalcontentot").html(data);
        $("#modalcontainerot").modal("show");
        configurarGrillaOT();
        
    });
    
}
function eliminarcomprobante(id)
{
    var vUrl = UrlHelper.Action("EliminarComprobante", "Pago", "Pago") + "?id=" + id;
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


    //$.jgrid.defaults.width = 1200;
    $.jgrid.defaults.height = 160;
    $.jgrid.defaults.responsive = true;



    $(grilla).jqGrid({
        url: vdataurl,
        responsive:true,
        datatype: 'json',
        mtype: 'POST',
        colNames: ['','Hoja de Ruta','Manifiesto','GR' , 'OT', 'Bultos','Peso', 'Valoriz.','Acciones',],
        colModel:
        [
            { key: true, hidden: true, name: 'PKID', index: 'PKID' },
            { key: false, hidden: false, name: 'hojaruta', index: 'hojaruta', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'manifiesto', index: 'manifiesto', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'nroguia', index: 'nroguia', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'NumCp', index: 'NumCp', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'TotalBultos', index: 'TotalBultos', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'TotalPeso', index: 'TotalPeso', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'ValorVenta', index: 'ValorVenta', width: '100', align: 'left' },
            { key: false, hidden: false, name: 'PKID', index: 'PKID', width: '100', align: 'center', formatter: bottonasignarrol_formatter }

        ],
        jsonReader: CONFIG_JQGRID.get('jsonReader'),
        pager: $(gridlistaotspager),
        rowNum: 10,
        rowList: [10, 20],
        emptyrecords: 'No se encontraron registros',
        autoheight: true,
        responsive : true,
        autowidth: true,
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


       var id = null;
       var guia = null;
       var idvehiculo = $('#idvehiculo').val();
       var idetapa = $('#idetapa').val();
        if(idetapa == '')
        {
          swal({ title: "Error", text: "Ingrese una etapa", type: "error", confirmButtonText: "Aceptar" });
           return ;

        }


       if( $('#txtnumcp').val() == '')
       {
         swal({ title: "Error", text: "Ingrese un valor de búsqueda", type: "error", confirmButtonText: "Aceptar" });
         return ;

       }

       var val = $("#idtipofacturacion").val();
        if(val == 87)
        {

               var guia = $('#txtnumcp').val();
               if( idvehiculo == '')
               {
                  swal({ title: "Error", text: "Ingrese una placa", type: "error", confirmButtonText: "Aceptar" });
                   return ;
              }
        }
        else 
        {
              var id = $('#txtnumcp').val();
        }

       debugger
        var vUrl = UrlHelper.Action("AgregarOT", "Pago", "Pago");
       $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: {   id: id 
                           , guia : guia
                           , idvehiculo : idvehiculo 
                           , idetapa : idetapa },
                   success: function (data) {
                       if (data.res) {

                    var grilla = $("#gridlistaots");
                    //var tipodocumento = $("#txtnumcp").val().trim();
                    var numdocumento = $("#txtnumcp").val();

                    $("#txtpesototal").val(data.pesototal);
                    $("#txtvalorizadototal").val(data.valorizadototal);
                    $("#txtcantidad").val(data.cantidad);



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

    var vUrl = UrlHelper.Action("ElimnarOT", "Pago", "Pago");
   $.ajax({

                   url: vUrl,
                   type: "post",
                   datatype: "json",
                   data: { id: id },
                   success: function (data) {
                       if (data.res) {

                      var grilla = $("#gridlistaots");
                      var numdocumento = $("#txtnumcp").val();

                      var vdataurl = $(grilla).data("dataurl")  ;
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




function agregarProveedor()
{
    var url = UrlHelper.Action("AgregarProveedorModalHelp", "Pago", "Pago");
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
        var url = UrlHelper.Action("AgregarProveedorAux", "Pago", "Pago");


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





function CargaListaots() {
    
    oDocumentosTable =
       $('.dataTables-tblOrdenes').DataTable({
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
               "url": $('#tblOrdenes').data("url"),
               "data": function (d) {
                   d.numcp = $('#numcp').val();
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

function cancelar()
{

    
    var url = UrlHelper.Action("Comprobante", "Pago", "Pago");
    window.location.href = url;
}