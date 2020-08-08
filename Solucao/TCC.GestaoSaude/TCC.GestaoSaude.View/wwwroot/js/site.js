// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Notificacao(mensagemErro, mensagemSucesso, mensagemAlerta, posicaoNotificacao, altura, largura){
    let tipoMensagem = '';
    let icone = '';
    let mensagem = ''
    if (mensagemErro != null)
    {
        tipoMensagem = "error";
        icone = "<i class=''></i>";
        mensagem = mensagemErro;
    }
    if (mensagemSucesso != null)
    {
        tipoMensagem = "success";
        icone = "<i class=''></i>";
        mensagem = mensagemSucesso
    }
    if (mensagemAlerta != null)
    {
        tipoMensagem = "Info";
        icone = "<i class=''></i>";
        mensagem = mensagemAlerta;
    }

    notif({
        type: tipoMensagem,
        width: largura,
        heigth: altura,
        position: posicaoNotificacao,
        autohide: true,
        msg: icone + " " + mensagem,
        timeout: 5000,
        animation: 'slide'
    });
}
