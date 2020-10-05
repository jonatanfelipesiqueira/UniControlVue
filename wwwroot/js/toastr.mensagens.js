function mensagemSucesso(mensagem) {
    toastr.options.closeButton = true;
    toastr.success(mensagem);
}

function mensagemErro(mensagem) {
    toastr.options.closeButton = true;
    toastr.error(mensagem);
}

function mensagemCuidado(mensagem) {
    toastr.options.closeButton = true;
    toastr.warning(mensagem);
}

function exibirErrosModelState(erros) {
    $.each(erros, function (idx, validationError) {
        mensagemErro(validationError.Erros[0]);
    });
}