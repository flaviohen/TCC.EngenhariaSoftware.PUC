function PesquisarEstabelecimento(tipoEstabelecimento, numeroCep, codigoCNES, url, urlDeletar, urlEditar)
{
    $.ajax({
        type: "POST",
        url: url,
        data: {
            tpEstabelecimento: tipoEstabelecimento,
            numCep: numeroCep,
            CNES : codigoCNES
        },
        dataType: "JSON",
        success: function (retorno) {
            if (retorno.estabelecimentos !== "")
            {
                $("#TabelaEstabelecimentos").show();
                $("#FormularioNovoEstabelecimento").hide();
                MontarTabelaEstabelecimentos(retorno.estabelecimentos, urlEditar, urlDeletar);
            }
            if (retorno.mensagemErro !== "")
            {
                Notificacao(retorno.mensagemErro,null, null, "right", 30, 500);
            }
            if (retorno.mensagemAlerta !== "")
            {
                Notificacao(null, null, retorno.mensagemAlerta, "right", 30, 500);
            }
            if (retorno.mensagemSucesso !== "")
            {
                Notificacao(null, retorno.mensagemSucesso, null, "right", 30, 500);
            }  
        }
    });
}

function MontarTabelaEstabelecimentos(dados, urlEditar, urlDeletar)
{
    let linhas = '';

    if (dados !== null)
    {
        for (var i = 0; i < dados.length; i++) {
            linhas += `<tr>`;
            linhas += `<td>${dados[i].a21EstabelecimentoId}</td>`
            linhas += `<td>${dados[i].a21EstabelecimentoNomeFantasia}</td>`;
            linhas += `<td>${dados[i].a21EstabelecimentoCodigoEstabelecimento}</td>`;
            linhas += `<td>${dados[i].a21EstabelecimentoEndereco}, ${dados[i].a21EstabelecimentoNumero}, ${dados[i].a21EstabelecimentoBairro}, Cep: ${dados[i].a21EstabelecimentoCep}</td>`;
            linhas += `<td style="text-align:center;"><button type="button" onclick="DetalhesEstabelecimento(${dados[i].a21EstabelecimentoId},'${urlEditar}')" class="btn btn-default"><i class="icon icon-list-alt"></i></button></td>`;
            linhas += `<td style="text-align:center;"><button type="button" onclick="DeletarEstabelecimento(${dados[i].a21EstabelecimentoId},'${urlDeletar}')" class="btn btn-default"><i class="icon icon-trash"></i></button></td>`
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