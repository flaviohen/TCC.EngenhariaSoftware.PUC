function PesquisarPorCPF(numCPF, url) {

    $.ajax({
        type: "POST",
        url: url,
        data: {
            numeroCPF: numCPF
        },
        dataType: "JSON",
        success: function (retorno) {
            if (retorno.usuario !== "") {
                PreencherFormularioAtendimento(retorno.usuario);
                $("#FormularioAtendimento").show();
                $("#alerta").hide();
                $("#sucesso").hide();
                $("#erro").hide();
            }
            else {
                $("#FormularioAtendimento").hide();
                $("#alerta").show();
                $("#sucesso").hide();
                $("#erro").hide();
                $("#alerta").html(retorno.mensagem);
            }
        }
    });

}

function NovoAtendimento()
{
    $("#alerta").hide();
    $("#sucesso").hide();
    $("#erro").hide();
    $("#FormularioAtendimento").show();
    LimparFormulario();
}

function PreencherFormularioAtendimento(dados) {
    $("#hdnIDUsuario").val(dados.A1UsuarioId)
    $("#txtFormCPF").val(dados.a1UsuarioNumeroCpf);
    $("#txtNomeCompleto").val(dados.a3InformacaoCadastro[0].a3InformacaoCadastroNomeCompleto);
    $("#txtDataNascimento").val(dados.a3InformacaoCadastro[0].dataNascimento);
    $("#txtNomeMae").val(dados.a3InformacaoCadastro[0].a3InformacaoCadastroNomeMae);
    $("#txtNomePai").val(dados.a3InformacaoCadastro[0].a3InformacaoCadastroNomePai);
    $("#txtNumeroCarteiraSUS").val(dados.a3InformacaoCadastro[0].a3InformacaoCadastroNumeroCarteiraNacionalSaude);
    $("#txtTelefoneResidencial").val(dados.a3InformacaoCadastro[0].a3InformacaoCadastroTelefone);
    $("#txtTelefoneCelular").val(dados.a3InformacaoCadastro[0].a3InformacaoCadastroCelular);
    $("#txtEmail").val(dados.a3InformacaoCadastro[0].a3InformacaoCadastroEmail);
}

function LimparFormulario() {
    $("#hdnIDUsuario").val("0");
    $("#txtFormCPF").val("");
    $("#txtNomeCompleto").val("");
    $("#txtDataNascimento").val("");
    $("#txtNomeMae").val("");
    $("#txtNomePai").val("");
    $("#txtNumeroCarteiraSUS").val("");
    $("#txtTelefoneResidencial").val("");
    $("#txtTelefoneCelular").val("");
    $("#txtEmail").val("");
}

function CadastrarNovoAtendimento(url) {
    $("#FormularioAtendimento").show();
    $("#alerta").hide();
    $("#sucesso").hide();
    $("#erro").hide();

    var dadosFormulario = {
        "UsuarioID": $("#hdnIDUsuario").val(),
        "NumeroCPF": $("#txtFormCPF").val(),
        "NomeCompleto": $("#txtNomeCompleto").val(),
        "DataNascimento": $("#txtDataNascimento").val(),
        "NomeMae": $("#txtNomeMae").val(),
        "NomePai": $("#txtNomePai").val(),
        "NumeroCarteiraSUS": $("#txtNumeroCarteiraSUS").val(),
        "TelefoneResidencial": $("#txtTelefoneResidencial").val(),
        "TelefoneCelular": $("#txtTelefoneCelular").val(),
        "Email": $("#txtEmail").val()
    };

    $.ajax({
        type: "POST",
        url: url,
        data: { formulario: JSON.stringify(dadosFormulario) },
        dataType: "JSON",
        success: function (retorno) {
            if (retorno.numeroAtendimento !== "0") {
                $("#FormularioAtendimento").hide();
                $("#sucesso").show();
                $("#sucesso").html("O atendimento foi cadastrado. Número: <b>" + retorno.numeroAtendimento + "</b>");
            }
            if (retorno.mensagem !== "")
            {
                $("#FormularioAtendimento").hide();
                $("#alerta").show();
                $("#alerta").html(retorno.mensagem);
            }
            if (retorno.mensagemErro !== "")
            {
                $("#FormularioAtendimento").hide();
                $("#erro").show();
                $("#erro").html(retorno.mensagemErro);
            }        
        }
    });
    LimparFormulario();
}