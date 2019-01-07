var btnNuevo = "#btnNuevo";

$(document).ready(function () {
 $(btnNuevo).click(function (event) { btnBuscarDocumento_onclick(this, event); });
 $("#btnBuscar").button()
                   .click(function (e) {
                       oDocumentosTable.draw();
                   });
    CargaListaEtapa();

     $("#etapa").keypress(function (event) {
                if (event.which == 13) {
                    $("#btnBuscar").click();
                }
            });
});

$(document).keydown(function (e) {
    if (e.which == 81 && e.ctrlKey)
       $("#btnNuevo").click();

});

function CargaListaEtapa() {

    oDocumentosTable =
       $('.dataTables-tblEtapa').DataTable({
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
               "url": $('#tblEtapa').data("url"),
               "data": function (d) {
                   d.criterio = $('#etapa').val();
                   d.inactivo  = $("#activo").is(':checked');
               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idetapa", "name": "idetapa", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                      { "title": "N°", "data": "idetapa", "name": "idetapa", "autoWidth": true, "class": "text-center" },
                   { "title": "Etapa", "data": "etapa", "name": "etapa", "autoWidth": true, "class": "text-center" },
                   {
                       "title": "¿Requiere OT?", "data": "requiereot", "name": "requiereot", "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                       if(data==true)
                                         return "<div><input type='checkbox' checked disabled name='your-group' value='unit-in-group' /> </div>";
                                      else
                                         return "<div><input type='checkbox'  disabled name='your-group' value='unit-in-group' /> </div>";
                                  }
                   },
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
                       "title": "¿Requiere OT?", visible: false, "data": "requiereot", "name": "requiereot", "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                       if(data==true)
                                         return "<span class='label label-primary'>" + "Si" + "</span>";
                                      else
                                         return "<span class='label label-primary'>" + "No" + "</span>";
                                  }
                     },
                     {
                       "title": "Acciones", "class": "text-center", "data": "idetapa", "Width": "15%", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editaretapa(" + data + ");' href='#' > <i class='fa fa-edit'></i> Editar</button>"
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='eliminaretapa(" + data + ");' href='#' > <i class='fa fa-trash'></i> Inactivar</button></div>";

                     }
                   },

           ],
           buttons: [
               //{ extend: 'copy' },
               //{ extend: 'csv' },
              { extend: 'excel', title: 'Listado de Etapas', exportOptions: { columns: [ 1,2,5,6 ] } },
               { extend: 'pdf', title: 'Listado de Etapas', exportOptions: { columns: [  1,2,5,6 ] } }

           ]

       });
}

function btnBuscarDocumento_onclick(obj, event) {


    var url = UrlHelper.Action("AgregarEtapaModal", "Pago", "Pago")


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
           text: "Se registró correctamente la Etapa.",
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
        swal({
           title: "Ya existe",
           text: "El nombre de la etapa ya existe.",
            type: "error"
        },
       function ()
       {

          // $("#modalcontainer").modal("hide");
           //var vurl = $('#btnAsociar').data("urlbak");
           //window.location.href = vurl;
            $("#modalcontainer").modal("hide");
           oDocumentosTable.draw();
       });
    }

}
function editaretapa(id)
{
    var url = UrlHelper.Action("EditarEtapaModal", "Pago", "Pago") + "?id=" + id;


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        inicializandoEventosModalDocumentos(id);
    });
}
function inicializandoEventosModalDocumentos(id)
{
    $('#hdidmoneda').val(id);
}

function eliminaretapa(id)
{
    var vUrl = UrlHelper.Action("EliminarEtapa", "Pago", "Pago") + "?id=" + id;
    swal({
        title: "Inactivar Etapa",
        text: "¿Está seguro que desea inactivar esta Etapa?",
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
                       alert(data.Errors.toString());
                   }
               });
           }
     });
}
