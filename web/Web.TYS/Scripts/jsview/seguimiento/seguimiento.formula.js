var btnNuevo = "#btnNuevo";

$(document).ready(function () {
 $(btnNuevo).click(function (event) { btnBuscarDocumento_onclick(this, event); });
 $("#btnBuscar").button()
                   .click(function (e) {
                       oDocumentosTable.draw();
                   });
    CargaGrilla();



 $("#criterio").keypress(function (event) {
                if (event.which == 13) {
                    $("#btnBuscar").click();
                }
            });



});

$(document).keydown(function (e) {
    if (e.which == 81 && e.ctrlKey)
       $("#btnNuevo").click();

});


function CargaGrilla() {
    
    oDocumentosTable =
       $('.dataTables-tblFormula').DataTable({
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
               "url": $('#tblFormula').data("url"),
               "data": function (d) {
                   d.criterio = $('#criterio').val();
                   d.inactivo  = $("#activo").is(':checked');
               },
               "type": "POST",
               "datatype": "json"
           },
           "columns": [
                   {
                       "key": true, "title": "Id", "data": "idformula", "name": "idformula", visible: false, "autoWidth": true, "class": "text-center",
                       "mRender":
                                  function (data, type, full) {
                                      return "<span class='label label-primary'>" + " " + data + " " + "</span>";
                                  }
                   },
                   { "title": "N°", "data": "idformula", "name": "idformula", "autoWidth": true, "class": "text-center" },
                   { "title": "Nombre", "data": "nombre", "name": "nombre", "autoWidth": true, "class": "text-center" },
                  { "title": "Fórmula", "data": "formula", "name": "formula", "autoWidth": true, "class": "text-center" },
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
                       "title": "Acciones", "class": "text-center", "data": "idformula", "Width": "15%", "mRender":

                        function (data, type, full) {
                            return "<div class='btn-group'><button type='button' data-toggle='tooltip' data-placement='top'  class='btn-primary btn btn-xs btn-outline' onclick='editarformula(" + data + ");' href='#' > <i class='fa fa-edit'></i> Editar</button>" 
                            + "<button type='button' data-toggle='tooltip' data-placement='top'  class='btn-danger btn btn-xs btn-outline' onclick='eliminarvalortabla(" + data + ");' href='#' > <i class='fa fa-trash'></i> Inactivar</button></div>";
                         
                        }
                   },

           ],
           buttons: [
               { extend: 'excel', title: 'Listado de Fórmulas', exportOptions: { columns: [ 1, 2, 3 ,5 ] } },
               { extend: 'pdf', title: 'Listado de Fórmulas', exportOptions: { columns: [ 1, 2, 3 ,5 ] } }
               
           ]

       });
}

function btnBuscarDocumento_onclick(obj, event) {

    
    var url = UrlHelper.Action("AgregarFormulaModal", "Seguimiento", "Seguimiento")


  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        inicializandoEventosModalDocumentos();
        BeginDropDownlist();
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
           text: "Se registró correctamente el dato.",
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
function editarformula(id)
{
    var url = UrlHelper.Action("EditarFormulaModal", "Seguimiento", "Seguimiento") + "?id=" + id;

  //  var url = $(obj).data("url");
    $.get(url, function (data) {
        $("#modalcontent").html(data);
        $("#modalcontainer").modal("show");
        inicializandoEventosModalDocumentos(id);
         BeginDropDownlist();
    });
}
function inicializandoEventosModalDocumentos(id)
{
    $('#hdidmoneda').val(id);
}

function eliminarvalortabla(id)
{
    var vUrl = UrlHelper.Action("EliminarFormula", "Seguimiento", "Seguimiento") + "?id=" + id;
    swal({
        title: "Eliminar Fórmula",
        text: "¿Está seguro que desea eliminar esta fórmula?",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Eliminar',
        closeOnConfirm: true,
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
                           swal("¡Se eliminó correctamente!", data.msj, "success");
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
function BeginDropDownlist()
{
  
   $("#aux").change(function ()
    {

       var value = $('select#aux option:selected').val();
       if(value == '')
        return ;

       var concepto =  $("[id*='aux'] :selected").text();
       var formulafinal =  $("#formula").val();
       formulafinal = formulafinal + ' ' +  concepto ;

       $("#formula").val(formulafinal);
      


    });
     $("#operacion").change(function ()
    {
       var value = $('select#operacion option:selected').val();
       if(value == '')
        return ;

       var concepto =  $("[id*='operacion'] :selected").text();
       var formulafinal =  $("#formula").val();
       formulafinal = formulafinal + ' ' +  concepto ;

       $("#formula").val(formulafinal);
      


    });
   
   

}

function LimpiarFormula()
{
   $("#formula").val('');
}