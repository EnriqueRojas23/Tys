var btnNuevo = "#btnNuevo";

$(document).ready(function () {



 $(btnNuevo).click(function (event) { btnBuscarDocumento_onclick(this, event); });

 $("#btnBuscar").button()
                   .click(function (e) {
                       oDocumentosTable.draw();
                   });

                   
    CargaListaProveedores();

     $("#razonsocial").keypress(function (event) {
                if (event.which == 13) {
                    $("#btnBuscar").click();
                }
            });
});
$(document).keydown(function (e) {
    if (e.which == 81 && e.ctrlKey)
       $("#btnNuevo").click();

});

function CargaListaProveedores() {


    
    oDocumentosTable =
       $('.dataTables-tblProveedores').DataTable({
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
                ,"sLengthMenu":  ""  }
                ,

           "ajax": {
               "url": $('#tblProveedores').data("url"),
               "data": function (d) {
                   d.criterio = $('#razonsocial').val();
                   d.inactivo  = $("#activo").is(':checked');
               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idproveedor", "name": "idproveedor", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                   { "title": "N°", "data": "idproveedor", "name": "idproveedor", "autoWidth": true, "class": "text-center" },
                   { "title": "RUC", "data": "ruc", "name": "ruc", "autoWidth": true, "class": "text-center" },
                   { "title": "Razón Social", "data": "razonsocial", "name": "razonsocial", "autoWidth": true, "class": "text-center" },
                   { "title": "Placa Asociada", "data": "placaasociada", "name": "placaasociada", "autoWidth": true, "class": "text-center" },
         {
                       "title": "Activo", "data": "activo", "name": "activo", "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                       if(data==true)
                                         return "<div><input type='checkbox' checked disabled name='your-group' value='unit-in-group' /> </div>";
                                      else 
                                         return "<div><input type='checkbox'  disabled name='your-group' value='unit-in-group' /> </div>";
                                  }
                   },
                    {
                       "title": "Activo", visible: false, "data": "activo", "name": "activo", "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                       if(data==true)
                                         return "<span class='label label-primary'>" + "Activo" + "</span>";
                                      else 
                                         return "<span class='label label-primary'>" + "Inactivo" + "</span>";
                                  }
                   },
                   {
                       "title": "Acciones", "class": "text-center", "data": "idproveedor", "Width": "15%", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarproveedor(" + data + ");' href='#' > <i class='fa fa-edit'></i> Editar</button>" 
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='eliminarProveedor(" + data + ");' href='#' > <i class='fa fa-trash'></i> Inactivar</button></div>";
                         
                        }
                   },

           ],
           buttons: [
               //{ extend: 'copy' },
               //{ extend: 'csv' },
              { extend: 'excel', title: 'Listado de Proveedores', exportOptions: { columns: [ 1,2, 3, 4 ,6 ] } },
               { extend: 'pdf', title: 'Listado de Proveedores', exportOptions: { columns: [  1,2, 3 ,4,6 ] } }
               
           ]

       });
}

function btnBuscarDocumento_onclick(obj, event) {

    
    var url = UrlHelper.Action("AgregarProveedorModal", "Facturacion", "Facturacion")


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        inicializandoEventosModalDocumentos();
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
           text: "Se registró correctamente el Proveedor.",
            type: "success"
        },
       function ()
       {
           $("#modalcontainer").modal("hide");
           //var vurl = $('#btnAsociar').data("urlbak");
           //window.location.href = vurl;
           oDocumentosTable.draw();
       });
     
    }
    else
    {
        sweetAlert("Verificar Errores", null, "error");
        CheckValidationErrorResponse(jsonres);
    }

}
function editarproveedor(id)
{
  var url = UrlHelper.Action("EditarProveedorModal", "Facturacion", "Facturacion") + "?id=" + id;


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        inicializandoEventosModalDocumentos(id);
    });
}
function inicializandoEventosModalDocumentos(propuesta)
{
    $('#hdproveedor').val(propuesta);




}

function eliminarProveedor(id)
{
    var vUrl = UrlHelper.Action("EliminarProveedor", "Facturacion", "Facturacion") + "?id=" + id;
    


    swal({
        title: "Inactivar Proveedor",
        text: "¿Está seguro que desea inactivar este proveedor?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Inactivar',
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
                           swal("¡Se inactivó correctamente!", data.msj, "success");
                           $("#modalcontainer").modal("hide");
                            oDocumentosTable.draw();

                       } else {
                           swal({ title: "Error", text: data.msj , type: "error", confirmButtonText: "Aceptar" });
                       }
                   },
                   error: function (data) {
                       alert(data.error.toString());
                   }
               });
           }
     });
}
function validateFloatKeyPress(el, evt)
{
  
   var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (charCode == 46 && el.value.indexOf(".") !== -1) {
        return false;
    }

    if (el.value.indexOf(".") !== -1)
    {
        var range = document.selection.createRange();

        if (range.text != ""){
        }
        else
        {
            var number = el.value.split('.');
            if (number.length == 2 && number[1].length > 1)
                return false;
        }
    }

    return true;
}