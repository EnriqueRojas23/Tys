

function controlsunat(item)
{

  if(validarasociados(item) == false)
         event.preventdefault();

  var url = UrlHelper.Action("ControlSunat","Monitoreo","Monitoreo") + "?idembarque=" + item;

   $.get(url, function (data){
       $("#modalcontent").html(data);
       $("#modalcontainer").modal("show");

      $('#horacontrolsunat').mask('00:00');
      $("#btnGuardarControl").click(function(event) {

             guardarControl(item);

      });
      $('#dtpfechacomp .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'

        });

      $("#agregar_sunat").click(function(event) {

        var selectedElms = $('div#jstree_di').jstree("get_selected", true);
        var checked_ids = [];
        if(selectedElms=='')
        {
          messagebox("No se puede continuar","Debe seleccionar al menos un elemento","warning");
          return;
        }


        $.each(selectedElms, function() {
             if(this.data != null)
               checked_ids.push(this.data.idordentrabajo);
        });

        agregar_sunat(checked_ids, item);


      });
      $("#quitar_sunat").click(function(event) {
        var selectedElms = $('div#jstree_nod').jstree("get_selected", true);
        var checked_idsa = [];
        if(selectedElms=='')
        {
          messagebox("No se puede continuar","Debe seleccionar al menos un elemento","warning");
          return;
        }


        $.each(selectedElms, function() {
             if(this.data != null)
               checked_idsa.push(this.data.idordentrabajo);
        });

      quitar_sunat(checked_idsa, item);

      });

       cargar_sincontrol(item);
       cargar_control(item);
   })
}


function cargar_sincontrol(item)
{
  var url =  UrlHelper.Action("JsonListarManifiestosSinControlSunat","Monitoreo","Monitoreo")
  + "?idembarque="  + item
       $.ajax({
         url: url,
         type: 'POST',
         dataType: 'json',
         data: {}
       })
       .done(function(data) {
            tree_sinasignar(data);
       })
}
function cargar_control(item)
{
  var url =  UrlHelper.Action("JsonListarManifiestosControlSunat","Monitoreo","Monitoreo")
  + "?idembarque="  + item
       $.ajax({
         url: url,
         type: 'POST',
         dataType: 'json',
         data: {}
       })
       .done(function(data) {
            traer_asignado(data);
       })
}

function agregar_sunat(items,item)
{

  var url =  UrlHelper.Action("JsonAgregarControl","Monitoreo","Monitoreo")  + "?ids="  + items;
  var urlvs =  UrlHelper.Action("JsonListarManifiestosSinControlSunat","Monitoreo","Monitoreo")  + "?idembarque="  + item;
  var urlv   =  UrlHelper.Action("JsonListarManifiestosControlSunat","Monitoreo","Monitoreo")  + "?idembarque="  + item;

       $.ajax({
         url: url,
         type: 'POST',
         dataType: 'json',
         data: {}
       })
       .done(function(data) {

              $.ajax({
                url: urlv,
                type: 'POST',
                dataType: 'json',
                data: {}
              })
              .done(function(data) {
                var tree = $("div#jstree_nod").jstree();
                var datas = data;
                tree.settings.core.data = datas;
                tree.refresh();
              })
              $.ajax({
                url: urlvs,
                type: 'POST',
                dataType: 'json',
                data: {}
              })
              .done(function(data) {
                var trees = $("div#jstree_di").jstree();
                var datasa = data;
                trees.settings.core.data = datasa;
                trees.refresh();
              })


       })



}
function quitar_sunat(items, item)
{
  var url =  UrlHelper.Action("JsonQuitarControl","Monitoreo","Monitoreo")  + "?ids="  + items;
  var urlvs =  UrlHelper.Action("JsonListarManifiestosSinControlSunat","Monitoreo","Monitoreo")  + "?idembarque="  + item;
  var urlv =  UrlHelper.Action("JsonListarManifiestosControlSunat","Monitoreo","Monitoreo")  + "?idembarque="  + item;

       $.ajax({
         url: url,
         type: 'POST',
         dataType: 'json',
         data: {}
       })
       .done(function(data) {
              $.ajax({
                url: urlv,
                type: 'POST',
                dataType: 'json',
                data: {}
              })
              .done(function(data) {
                var tree = $("div#jstree_nod").jstree();
                var datas = data;
                tree.settings.core.data = datas;
                tree.refresh();

              })

              $.ajax({
                url: urlvs,
                type: 'POST',
                dataType: 'json',
                data: {}
              })
              .done(function(data) {
                var trees = $("div#jstree_di").jstree();
                var datasa = data;
                trees.settings.core.data = datasa;
                trees.refresh();

              })
       })

}
function guardarControl(idembarque)
{

  ///idembarque
    var fechacontrolsunat =    $("#fechacontrolsunat").val();
    var horacontrol =    $("#horacontrolsunat").val();



    if(fechacontrolsunat == '')
    {
       messagebox("No puede continuar", "Seleccione una fecha de descarga", "warning" );
    }
    if(horacontrol == '')
    {
       messagebox("No puede continuar", "Ingrese una hora de descarga.", "warning" );
    }

     var url = UrlHelper.Action("JsonAsignarControl","Monitoreo","Monitoreo") + "?idembarque=" + idembarque
     + "&horacontrol=" + horacontrol
     + "&fechacontrol=" + fechacontrolsunat;

     $.ajax({
       url: url,
       type: 'POST',
       dataType: 'json',
       data: {}
     })
     .done(function(data) {
      if(data.res)
      {
        messagebox("Registro Exitoso.","Se ha registrado con éxito.", "success");
        $("#modalcontainer").modal("hide");
        reload();
        reloaddetalle();

      }
      else
      {
         messagebox("No puede continuar", data.msj, "warning" );
      }

    });

}
