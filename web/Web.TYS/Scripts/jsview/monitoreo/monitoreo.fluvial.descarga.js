

function descargarmanifiesto(item)
{

  if(validarasociados(item) == false)
         event.preventdefault();

  var url = UrlHelper.Action("DescargaFluvial","Monitoreo","Monitoreo") + "?idembarque=" + item;

   $.get(url, function (data){
       $("#modalcontent").html(data);
       $("#modalcontainer").modal("show");

      $('#horadescarga').mask('00:00');

      $('#dtpfechacomp .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'

        });
        $("#btnGuardarDescarga").click(function(event) {

               guardarDescarga(item);

        });

      $("#agregar").click(function(event) {

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

        agregar_descarga(checked_ids, item);


      });
      $("#quitar").click(function(event) {
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

      quitar_descarga(checked_idsa, item);

      });

       cargar_sindescarga(item);
       cargar_descarga(item);
   })
}


function cargar_sindescarga(item)
{
  var url =  UrlHelper.Action("JsonListarManifiestosSinDescargaFluvial","Monitoreo","Monitoreo")
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
function cargar_descarga(item)
{
  var url =  UrlHelper.Action("JsonListarManifiestosDescargaFluvial","Monitoreo","Monitoreo")
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

function agregar_descarga(items,item)
{

  var url =  UrlHelper.Action("JsonAgregarDescarga","Monitoreo","Monitoreo")  + "?ids="  + items;
  var urlvs =  UrlHelper.Action("JsonListarManifiestosSinDescargaFluvial","Monitoreo","Monitoreo")  + "?idembarque="  + item;
  var urlv   =  UrlHelper.Action("JsonListarManifiestosDescargaFluvial","Monitoreo","Monitoreo")  + "?idembarque="  + item;

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
function quitar_descarga(items, item)
{
  var url =  UrlHelper.Action("JsonQuitarDescarga","Monitoreo","Monitoreo")  + "?ids="  + items;
  var urlvs =  UrlHelper.Action("JsonListarManifiestosSinDescargaFluvial","Monitoreo","Monitoreo")  + "?idembarque="  + item;
  var urlv =  UrlHelper.Action("JsonListarManifiestosDescargaFluvial","Monitoreo","Monitoreo")  + "?idembarque="  + item;

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

function guardarDescarga(idembarque)
{

  ///idembarque
    var fechadescarga =    $("#fechadescarga").val();
    var horadescarga =    $("#horadescarga").val();
    var idpuerto =    $("#idpuerto").val();


    if(idpuerto== '')
    {
         messagebox("No puede continuar", "Seleccione un puerto", "warning" );
    }
    if(fechadescarga == '')
    {
       messagebox("No puede continuar", "Seleccione una fecha de descarga", "warning" );
    }
    if(horadescarga == '')
    {
       messagebox("No puede continuar", "Ingrese una hora de descarga.", "warning" );
    }

     var url = UrlHelper.Action("JsonAsignarDescarga","Monitoreo","Monitoreo") + "?idembarque=" + idembarque
     + "&horadescarga=" + horadescarga
     + "&fechadescarga=" + fechadescarga
     + "&idpuerto=" + idpuerto;

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
