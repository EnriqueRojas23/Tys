$(document).ready(function () {
     if(history.forward(1)){
      location.replace( history.forward(1) );
    }
    $("#ModoAutenticacion").change(function (evt) {
        var cboval = $(this).val();
        switch (cboval)
        {
            case "AD":
                getUserNamePc(this);
                break;
            case "EX":
                setUserName("");
                break;
        }

    });


});

function getUserNamePc(obj)
{
    var vUrl = $(obj).data("url");
    $.ajax({
        url: vUrl,
        type: "post",
        datatype: "json",
        success: function (data) {
            if (data.res) {
                setUserName(data.username);
            }
            else {
                setUserName("");
                alert("No se puede obtener el usuario de la PCs");
            }
        },
        error: function (data) {
            alert(data.error.toString());
        }
    });
}

function setUserName(usr)
{
    var txt = $("#CodigoUsuario");
    txt.removeAttr("readonly");
    txt.val("");

    if ($.trim(usr) != "")
    {
        txt.val(usr);
        //txt.attr('readonly', true);
    }
}
function regresar()
{
	var url = $('#btnRegresar').data("url");
   $(document).attr("location", url) ;  //+ '?ReturnUrl=/'
}
