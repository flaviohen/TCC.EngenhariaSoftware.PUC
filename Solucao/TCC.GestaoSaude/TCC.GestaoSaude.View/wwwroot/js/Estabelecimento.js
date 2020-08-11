function PesquisarEstabelecimento(tipoEstabelecimento, numeroCep, codigoCNES, url, urlEditar) {
    $.ajax({
        type: "POST",
        url: url,
        data: {
            tpEstabelecimento: tipoEstabelecimento,
            numCep: numeroCep,
            CNES: codigoCNES
        },
        dataType: "JSON",
        success: function (retorno) {
            if (retorno.estabelecimentos !== "") {
                $("#TabelaEstabelecimentos").show();
                $("#FormularioNovoEstabelecimento").hide();
                MontarTabelaEstabelecimentos(retorno.estabelecimentos, urlEditar, urlDeletar);
            }
            if (retorno.mensagemErro !== "") {
                $("#TabelaEstabelecimentos").hide();
                $("#FormularioNovoEstabelecimento").hide();
                Notificacao(retorno.mensagemErro, null, null, "right", 30, 500);
            }
            if (retorno.mensagemAlerta !== "") {
                $("#TabelaEstabelecimentos").hide();
                $("#FormularioNovoEstabelecimento").hide();
                Notificacao(null, null, retorno.mensagemAlerta, "right", 30, 500);
            }
            if (retorno.mensagemSucesso !== "") {
                $("#TabelaEstabelecimentos").hide();
                $("#FormularioNovoEstabelecimento").hide();
                Notificacao(null, retorno.mensagemSucesso, null, "right", 30, 500);
            }
        }
    });
}
function MontarTabelaEstabelecimentos(dados, urlDetalhe) {
    let linhas = '';

    if (dados !== null) {
        for (var i = 0; i < dados.length; i++) {
            linhas += `<tr>`;
            linhas += `<td>${dados[i].a21EstabelecimentoId}</td>`
            linhas += `<td>${dados[i].a21EstabelecimentoNomeFantasia}</td>`;
            linhas += `<td>${dados[i].a21EstabelecimentoCodigoEstabelecimento}</td>`;
            linhas += `<td>${dados[i].a21EstabelecimentoEndereco}, ${dados[i].a21EstabelecimentoNumero}, ${dados[i].a21EstabelecimentoBairro}, Cep: ${dados[i].a21EstabelecimentoCep}</td>`;
            linhas += `<td style="text-align:center;"><button  type="button" class="btn btn-default" onclick="DetalhesEstabelecimento(${dados[i].a21EstabelecimentoId},'${urlDetalhe}')" data-toggle="modal" data-target="#ModalDetalhes"><i class="icon icon-list-alt"></i></buton></td>`;
            linhas += `<td style="text-align:center;"><button  type="button" class="btn btn-default" onclick="SelecionarEstabelecimentoExclusao(this);" data-toggle="modal" data-target="#ModalDeletar"><i class="icon icon-trash"></i></button></td>`;
        }
    }

    var tabela = `<table class="table table-bordered table-striped">
        <thead>
            <tr>
                <th>ID</th>
                <th>Nome</th>
                <th>CNES</th>
                <th>Endereço</th>
                <th>Detalhes</th>
                <th>Exclusão</th>
            </tr>
        </thead>
        <tbody>
            ${linhas}
        </tbody>
    </table>`;

    $("#tableEstabelecimento").html(tabela);

}
function CadastrarNovoEstabelecimento(url) {
    estabelecimento = {
        "A20TipoEstabelecimentoId": $("#ddlTiposEstabelecimento2").val(),
        "A21EstabelecimentoCodigoUnidade": $("#txtCodigoUnidade").val(),
        "A21EstabelecimentoCodigoEstabelecimento": $("#txtCNES").val(),
        "A21EstabelecimentoCnpj": $("#txtCNPJ").val(),
        "A21EstabelecimentoRazaoSocial": $("#txtRazaoSocial").val(),
        "A21EstabelecimentoNomeFantasia": $("#txtNomeFantasia").val(),
        "A21EstabelecimentoLogradouro": $("#txtLogradouro").val(),
        "A21EstabelecimentoEndereco": $("#txtEndereco").val(),
        "A21EstabelecimentoNumero": $("#txtNumero").val(),
        "A21EstabelecimentoComplemento": $("#txtComplemento").val(),
        "A21EstabelecimentoBairro": $("#txtBairro").val(),
        "A21EstabelecimentoCep": $("#txtCep2").val(),
        "A21EstabelecimentoTelefone": $("#txtTelefone").val(),
        "A21EstabelecimentoFax": $("#txtFax").val(),
        "A21EstabelecimentoEmail": $("#txtEmail").val(),
        "A21EstabelecimentoLatitude": $("#txtLatitude").val(),
        "A21EstabelecimentoLongitude": $("#txtLongitude").val(),
    }

    $.ajax({
        type: "POST",
        url: url,
        data: { dadosEstabelecimento: JSON.stringify(estabelecimento) },
        dataType: "JSON",
        success: function (retorno) {
            if (retorno.mensagemErro !== "") {
                Notificacao(retorno.mensagemErro, null, null, "right", 30, 500);
            }
            if (retorno.mensagemSucesso !== "") {
                $("#FormularioNovoEstabelecimento").hide();
                LimparFormularioNovoEstabelecimento();
                Notificacao(null, retorno.mensagemSucesso, null, "right", 30, 500);
            }
            if (retorno.mensagemAlerta !== "") {
                Notificacao(null, null, retorno.mensagemAlerta, "right", 30, 500);
            }
        }
    });
}

function LimparFormularioNovoEstabelecimento() {
    $("#ddlTiposEstabelecimento2").val("1");
    $("#txtCodigoUnidade").val("");
    $("#txtCNES").val("");
    $("#txtCNPJ").val("");
    $("#txtRazaoSocial").val("");
    $("#txtNomeFantasia").val("");
    $("#txtLogradouro").val("");
    $("#txtEndereco").val("");
    $("#txtNumero").val("");
    $("#txtComplemento").val("");
    $("#txtBairro").val("");
    $("#txtCep2").val("");
    $("#txtTelefone").val("");
    $("#txtFax").val("");
    $("#txtEmail").val("");
    $("#txtLatitude").val("");
    $("#txtLongitude").val("");
}
function DetalhesEstabelecimento(id, url)
{
    $.ajax({
        type: "POST",
        url: url,
        data: {idEstabelecimento : id},
        dataType: "JSON",
        success: function (retorno) {
            if (retorno.estabelecimento !== "")
            {
                PreencherDetalhesEstabelecimento(retorno.estabelecimento);
            }
            if (retorno.mensagemErro !== "") {
                Notificacao(retorno.mensagemErro, null, null, "right", 30, 500);
            }
            if (retorno.mensagemSucesso !== "") {
                Notificacao(null, retorno.mensagemSucesso, null, "right", 30, 500);
            }
            if (retorno.mensagemAlerta !== "") {
                Notificacao(null, null, retorno.mensagemAlerta, "right", 30, 500);
            }
        }
    });
}
function SelecionarEstabelecimentoExclusao(elemento)
{
    let linha = $(elemento).closest("tr");
    let IDEstabelecimento = linha.find("td:eq(0)").text();
    $("#hdnIDExcluir").val(IDEstabelecimento);
}

function PreencherDetalhesEstabelecimento(dados)
{
    $("#lblTipoEstabelecimento").text(dados.a20TipoEstabelecimento.a20TipoEstabelecimentoDescricao);
    $("#lblCodigoUnidade").text(dados.a21EstabelecimentoCodigoUnidade);
    $("#lblCodigoCNES").text(dados.a21EstabelecimentoCodigoEstabelecimento);
    $("#lblCNPJ").text(dados.a21EstabelecimentoCnpj);
    $("#lblRazaoSocial").text(dados.a21EstabelecimentoRazaoSocial);
    $("#lblNomeFantasia").text(dados.a21EstabelecimentoNomeFantasia);
    $("#lblLogradouro").text(dados.a21EstabelecimentoLogradouro);
    $("#lblEndereco").text(dados.a21EstabelecimentoEndereco);
    $("#lblNumero").text(dados.a21EstabelecimentoNumero);
    $("#lblComplemento").text(dados.a21EstabelecimentoComplemento);
    $("#lblBairro").text(dados.a21EstabelecimentoBairro);
    $("#lblTelefone").text(dados.a21EstabelecimentoTelefone);
    $("#lblFax").text(dados.a21EstabelecimentoFax);
    $("#lblEmail").text(dados.a21EstabelecimentoEmail);
    $("#lblLatitude").text(dados.a21EstabelecimentoLatitude);
    $("#lblLongitude").text(dados.a21EstabelecimentoLongitude);
}
function DeletarEstabelecimento(idEstabelecimentoExcluir, url) {
    $.ajax({
        type: "POST",
        url: url,
        data: { idEstabelecimento: idEstabelecimentoExcluir },
        dataType: "JSON",
        success: function (retorno) {
            if (retorno.mensagemErro !== "") {
                Notificacao(retorno.mensagemErro, null, null, "right", 30, 500);
            }
            if (retorno.mensagemSucesso !== "") {
                $("#TabelaEstabelecimentos").hide();
                MontarTabelaEstabelecimentos(null, null);
                Notificacao(null, retorno.mensagemSucesso, null, "right", 30, 500);
            }
            if (retorno.mensagemAlerta !== "") {
                Notificacao(null, null, retorno.mensagemAlerta, "right", 30, 500);
            }
        }
    });
}