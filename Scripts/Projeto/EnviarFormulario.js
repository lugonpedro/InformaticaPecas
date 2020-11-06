var btnAcao = $("input[type='button']");
var formulario = $("#formCrud");

btnAcao.on("click", submeter);

function submeter() {
    if (formulario.valid()) {
        var url = formulario.prop("action");
        var metodo = formulario.prop("method");

        var dadosFormulario = formulario.serialize();

        $.ajax({
            url: url,
            type: metodo,
            data: dadosFormulario,
            success: tratarRetorno
        });
    }
}

function tratarRetorno(resultadoServidor) {
    if (resultadoServidor.resultado) {
        toastr['success'](resultadoServidor.mensagem);

        $("#minhaModal").modal("hide");

        $("#gridDados").bootgrid("reload");
    }
    else {
        toast['error'](resultadoServidor.mensagem);
    }
}