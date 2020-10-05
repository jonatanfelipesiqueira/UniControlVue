function erroComValidacao(xhr, ajaxOptions, thrownError) {
    if (Array.isArray(xhr.responseJSON)) {
        exibirErrosModelState(xhr.responseJSON);
    } else {
        mensagemErro(`${xhr.responseText}`);
    }
}