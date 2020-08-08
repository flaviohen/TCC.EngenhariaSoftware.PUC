function PesquisarPorNumeroAtendimento(numAtendimento, url) {

    $.ajax({
        type: "POST",
        url: url,
        data: {
            numeroAtendimento: numAtendimento
        },
        dataType: "JSON",
        success: function (retorno) {
            PreencherFormularioDadosBasico(retorno)
            $("#FormularioDadosBasicoPaciente").show();
            //Notificacao(null, "TESTE de Mensagem", null, "right", 30, 500);
        }
    });
}

function PreencherFormularioDadosBasico(dados) {
    $("#lblNumeroCPF").text(dados.dadosAtendimento.a3InformacaoCadastro.a1Usuario.a1UsuarioNumeroCpf);
    $("#lblNomeCompleto").text(dados.dadosAtendimento.a3InformacaoCadastro.a3InformacaoCadastroNomeCompleto)
    $("#lblDataNascimento").text(dados.dadosAtendimento.a3InformacaoCadastro.dataNascimento);
    $("#lblNomeMae").text(dados.dadosAtendimento.a3InformacaoCadastro.a3InformacaoCadastroNomeMae);
    $("#lblNomePai").text(dados.dadosAtendimento.a3InformacaoCadastro.a3InformacaoCadastroNomePai);
    $("#lblCarteiraSus").text(dados.dadosAtendimento.a3InformacaoCadastro.a3InformacaoCadastroNumeroCarteiraNacionalSaude);
    $("#lblTelefone").text(dados.dadosAtendimento.a3InformacaoCadastro.a3InformacaoCadastroTelefone);
    $("#lblCelular").text(dados.dadosAtendimento.a3InformacaoCadastro.a3InformacaoCadastroCelular);
    $("#lblEmail").text(dados.dadosAtendimento.a3InformacaoCadastro.a3InformacaoCadastroEmail);

    if (dados.dadosAtendimento.relAtendimentoProntuario.length > 0) {
        $("#btnCriarProntuario").hide();
        $("#btnCadastrarProntuario").hide();
        $("#btnAtualizar").show();
        MontarTabelaRegistroEnfermagem(dados.registrosEnfermagem,null)
        PreencherFormularioProntuario(dados)
        $("#FormularioProntuario").show();
    }
    else {
        $("#btnCriarProntuario").show();
        $("#btnCadastrarProntuario").show();
        $("#btnAtualizar").hide();
        $("#FormularioProntuario").hide();
        LimparFormularioProntuario();
    }
}

function PreencherFormularioProntuario(dados) {
    $("#textAreaRaciocinioMedico").val(dados.dadosAtendimento.relAtendimentoProntuario[0].a9Prontuario.a9ProntuarioRaciocinioMedico);
    $("#textAreaHipoteseDiagnostica").val(dados.dadosAtendimento.relAtendimentoProntuario[0].a9Prontuario.a9ProntuarioHipotesesDiagnostica);
    $("#textAreaCondutaTerapeuta").val(dados.dadosAtendimento.relAtendimentoProntuario[0].a9Prontuario.a9ProntuarioCondutaTerapeuta);
    $("#textAreaPrescricaoMedica").val(dados.dadosAtendimento.relAtendimentoProntuario[0].a9Prontuario.a9ProntuarioPrescricaoMedica);
    $("#textAreaDescricaoCirurgica").val(dados.dadosAtendimento.relAtendimentoProntuario[0].a9Prontuario.a9ProntuarioDescricaoCirurgica);
    $("#textAreaDiagnosticoMedico").val(dados.dadosAtendimento.relAtendimentoProntuario[0].a9Prontuario.a9ProntuarioDiagnosticoMedico);
}

function LimparFormularioProntuario()
{
    MontarTabelaRegistroEnfermagem(null, null);
    $("#textAreaRaciocinioMedico").val("");
    $("#textAreaHipoteseDiagnostica").val("");
    $("#textAreaCondutaTerapeuta").val("");
    $("#textAreaPrescricaoMedica").val("");
    $("#textAreaDescricaoCirurgica").val("");
    $("#textAreaDiagnosticoMedico").val("");
}

function MontarTabelaRegistroEnfermagem(dadosTabela, urlDeletarRegistro) {

    let linhas = '';

    if (dadosTabela !== null) {

        for (var i = 0; i < dadosTabela.length; i++) {
            linhas += `<tr>`;
            linhas += `<td>${dadosTabela[i].id}</td>`
            linhas += `<td>${dadosTabela[i].data}</td>`;
            linhas += `<td>${dadosTabela[i].hora}</td>`;
            linhas += `<td>${dadosTabela[i].descricao}</td>`;
            linhas += `<td>${dadosTabela[i].profissional}</td>`;
            if (dadosTabela[i].ehRegistroNovo) {
                linhas += `<td><button type="button" class="btn btn-danger" onclick="DeletarRegistro(`+ dadosTabela[i].id + `,` + `'`+urlDeletarRegistro + `'` + `);"><i class="icon icon-trash"></i></button></td>`;
            }
            else {
                linhas += `<td></td>`;
            }

            linhas += `</tr>`;
        }
    }

    let tabela =
        `<table class="table table-bordered table-striped">
        <thead>
            <tr>
                <th>ID</th>
                <th>Data</th>
                <th>Hora</th>
                <th>Descrição</th>
                <th>Profissional</th>
                <th>Exclusão</th>
            </tr>
        </thead>
        <tbody>
            ${linhas}
        </tbody>
    </table>`

    $("#tableRegistrosEnfermagem").html(tabela);
}

function AdicionarRegistroEnfermagem(descricaoRegistro, url, urlDeletarRegistro) {

    $.ajax({
        type: "POST",
        url: url,
        data: {
            descricao: descricaoRegistro
        },
        dataType: "JSON",
        success: function (retorno) {
            MontarTabelaRegistroEnfermagem(retorno.registrosEnfermagem, urlDeletarRegistro)
        }
    });
}

function DeletarRegistro(idRegDeletar, urlDeletar) {

    $.ajax({
        type: "POST",
        url: urlDeletar,
        data: {
            idRegistro: idRegDeletar
        },
        dataType: "JSON",
        success: function (retorno) {
            MontarTabelaRegistroEnfermagem(retorno.registrosEnfermagem)
        }
    });
}