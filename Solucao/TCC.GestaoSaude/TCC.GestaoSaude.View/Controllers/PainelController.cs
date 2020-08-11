using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using TCC.GestaoSaude.Business;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.View.Models;
using TCC.GestaoSaude.View.SessionCustom;

namespace TCC.GestaoSaude.View.Controllers
{
	public class PainelController : Controller
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IA1UsuarioRepositorio _usuarioRepositorio;
		private readonly IA6PerfilRepositorio _perfilRepositorio;
		private readonly IA13ProfissionalRepositorio _profissionalRepositorio;
		private readonly IA2UsuarioInternoRepositorio _usuarioInternoRepositorio;
		private readonly IA29AtendimentoRepositorio _atendimentoRepositorio;
		private readonly IA9ProntuarioRepositorio _prontuarioRepositorio;
		private readonly IA10RegistroEvolucaoEnfermagemRepositorio _registroEvolucaoEnfermagemRepositorio;
		private readonly IRelHistoricoEvolucaoEnfermagemRepositorio _relHistoricoEvolucaoEnfermagemRepositorio;
		private readonly IEmailEnvio _emailEnvio;


		private Sessao _sessao;
		public PainelController(IHttpContextAccessor httpContextAccessor,
								IA1UsuarioRepositorio usuarioRepositorio,
								IA6PerfilRepositorio perfilRepositorio,
								IA13ProfissionalRepositorio profissionalRepositorio,
								IA2UsuarioInternoRepositorio usuarioInternoRepositorio,
								IA29AtendimentoRepositorio atendimentoRepositorio,
								IA9ProntuarioRepositorio prontuarioRepositorio,
								IA10RegistroEvolucaoEnfermagemRepositorio registroEvolucaoEnfermagemRepositorio,
								IRelHistoricoEvolucaoEnfermagemRepositorio relHistoricoEvolucaoEnfermagemRepositorio,
								IEmailEnvio emailEnvio)
		{
			_httpContextAccessor = httpContextAccessor;
			_usuarioRepositorio = usuarioRepositorio;
			_perfilRepositorio = perfilRepositorio;
			_profissionalRepositorio = profissionalRepositorio;
			_usuarioInternoRepositorio = usuarioInternoRepositorio;
			_atendimentoRepositorio = atendimentoRepositorio;
			_prontuarioRepositorio = prontuarioRepositorio;
			_registroEvolucaoEnfermagemRepositorio = registroEvolucaoEnfermagemRepositorio;
			_relHistoricoEvolucaoEnfermagemRepositorio = relHistoricoEvolucaoEnfermagemRepositorio;
			_emailEnvio = emailEnvio;
			_sessao = new Sessao(httpContextAccessor);

		}

		public IActionResult PaginaInicial()
		{	
			ViewBag.Session = _sessao;
			return View();
		}

		public IActionResult Logout()
		{
			_sessao.UsuarioExterno = null;
			_sessao.UsuarioInterno = null;
			return RedirectToAction("Index", "Home");
		}

		public IActionResult CadastrarNovoAtendimento()
		{
			ViewBag.Session = _sessao;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CadastrarNovoAtendimento(string formulario)
		{
			try
			{
				int indiceUnicoInformacaoUsuario = 0;
				string usuarioNaoExisteNoSistema = "0";

				UsuarioExternoViewModel usuario = JsonConvert.DeserializeObject<UsuarioExternoViewModel>(formulario);
				A29Atendimento atendimento = new A29Atendimento();
				A1Usuario usuarioCadastro = new A1Usuario();
				A29AtendimentoBusiness atendimentoNegocio = new A29AtendimentoBusiness(_atendimentoRepositorio, null, null, _usuarioRepositorio, _profissionalRepositorio, _usuarioInternoRepositorio);
				A1UsuarioBusiness usuarioNegocio = new A1UsuarioBusiness(_usuarioRepositorio, null, _perfilRepositorio, null);
				EmailBusiness emailNegocio = new EmailBusiness(_emailEnvio);

				if (usuario.UsuarioID != usuarioNaoExisteNoSistema)
				{
					var informacaoID = usuarioNegocio.BuscarUsuarioPorCPF(usuario.NumeroCPF).A3InformacaoCadastro.ToList()[indiceUnicoInformacaoUsuario].A3InformacaoCadastroId;
					atendimento.A3InformacaoCadastroId = informacaoID;
					atendimento.A29Data = DateTime.Now;
					atendimentoNegocio.CadastrarAtendimento(atendimento);
					if (atendimento.Mensagens.Count == 0)
					{
						return Json(new { NumeroAtendimento = atendimento.A29AtendimentoId, Mensagem = "" });
					}
					else
					{
						return Json(new { NumeroAtendimento = "0", Mensagem = atendimento.Mensagens[0].DescricaoMensagem });
					}
				}
				else
				{
					usuarioCadastro.A1UsuarioNumeroCpf = usuario.NumeroCPF;
					usuarioCadastro.A1UsuarioNome = usuario.NomeCompleto;
					usuarioCadastro.A1UsuarioSenha = "123456";

					A3InformacaoCadastro informacaoCadastro = new A3InformacaoCadastro();
					informacaoCadastro.A3InformacaoCadastroCelular = usuario.TelefoneCelular;
					informacaoCadastro.A3InformacaoCadastroDataNascimento = Convert.ToDateTime(usuario.DataNascimento);
					informacaoCadastro.A3InformacaoCadastroEmail = usuario.Email;
					informacaoCadastro.A3InformacaoCadastroNomeCompleto = usuario.NomeCompleto;
					informacaoCadastro.A3InformacaoCadastroNomeMae = usuario.NomeMae;
					informacaoCadastro.A3InformacaoCadastroNomePai = usuario.NomePai;
					informacaoCadastro.A3InformacaoCadastroNumeroCarteiraNacionalSaude = usuario.NumeroCarteiraSUS;
					informacaoCadastro.A3InformacaoCadastroTelefone = usuario.TelefoneResidencial;

					usuarioCadastro.A3InformacaoCadastro = new List<A3InformacaoCadastro>();
					usuarioCadastro.A3InformacaoCadastro.Add(informacaoCadastro);

					var loginCriado = usuarioNegocio.CriarLogin(usuarioCadastro);

					if (loginCriado)
					{
						atendimento.A29Data = DateTime.Now;
						atendimento.A3InformacaoCadastroId = usuarioCadastro.A3InformacaoCadastro.ToList()[indiceUnicoInformacaoUsuario].A3InformacaoCadastroId;
						atendimentoNegocio.CadastrarAtendimento(atendimento);
						if (atendimento.Mensagens.Count == 0)
						{
							EmailModel email = new EmailModel();
							email.Assunto = "Cadastro no sistema gestão de saúde";
							email.CorpoEmail = string.Format("Foi criado um cadastro no sistema de saude. <p>Usuario: {0}</p><p>Senha: {1} </p> ", usuarioCadastro.A1UsuarioNumeroCpf, usuarioCadastro.A1UsuarioSenha);
							email.DoEmail = "flavio.henriq@live.com";
							email.ParaEmail = "flavio.henriq@live.com";
							await emailNegocio.EnviarEmailAsync(email);

							return Json(new { NumeroAtendimento = atendimento.A29AtendimentoId, Mensagem = "", MensagemErro = "" });
						}
						else
						{
							return Json(new { NumeroAtendimento = "0", MensagemErro = "", Mensagem = atendimento.Mensagens[0].DescricaoMensagem });
						}
					}
					else
					{
						return Json(new { NumeroAtendimento = "0", MensagemErro = "", Mensagem = usuarioCadastro.Mensagens[0].DescricaoMensagem });
					}
				}
			}
			catch (Exception ex)
			{
				return Json(new { NumeroAtendimento = "0", Mensagem = "", MensagemErro = string.Format("Ocorreu um erro inesperado, contate seu administrador. Erro: {0}", ex.Message) });
			}
		}

		[HttpPost]
		public IActionResult PesquisarPorCPF(string numeroCPF)
		{
			try
			{
				A1UsuarioBusiness usuarioNegocio = new A1UsuarioBusiness(_usuarioRepositorio, _usuarioInternoRepositorio, _perfilRepositorio, _profissionalRepositorio);
				var usuarioPaciente = usuarioNegocio.BuscarUsuarioPorCPF(numeroCPF);

				if (usuarioPaciente.Mensagens.Count > 0)
				{
					return Json(new { Usuario = "", Mensagem = usuarioPaciente.Mensagens[0].DescricaoMensagem });
				}
				else
				{
					var jsonUsuario = JsonConvert.SerializeObject(usuarioPaciente, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					var usuarioDeserializado = JsonConvert.DeserializeObject<A1Usuario>(jsonUsuario);
					return Json(new { Usuario = usuarioDeserializado, Mensagem = "" });
				}
			}
			catch (Exception ex)
			{
				return Json(new { Usuario = "", Mensagem = ex.Message });
			}
		}

		public IActionResult PreencherProntuario()
		{
			ViewBag.Session = _sessao;
			return View();
		}

		[HttpPost]
		public IActionResult PesquisarPorCodigoAtendimento(string numeroAtendimento)
		{
			try
			{
				A29AtendimentoBusiness atendimentoNegocio = new A29AtendimentoBusiness(_atendimentoRepositorio, _prontuarioRepositorio, _registroEvolucaoEnfermagemRepositorio, _usuarioRepositorio, _profissionalRepositorio, _usuarioInternoRepositorio);
				var atendimento = atendimentoNegocio.BuscarAtendimento(Convert.ToInt32(numeroAtendimento));
				if (atendimento.Mensagens.Count == 0)
				{
					_sessao.IDAtendimento = atendimento.A29AtendimentoId;
					_sessao.RegistrosEvolucaoEnfermagem = null;
					string atendimentoJson = JsonConvert.SerializeObject(atendimento, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					var dados = JsonConvert.DeserializeObject<A29Atendimento>(atendimentoJson);
					List<RegistroEnfermagemViewModel> lstRegistroEnfermagemJaCadastrado = new List<RegistroEnfermagemViewModel>(); ;
					if (dados.RelAtendimentoProntuario.Count > 0)
					{
						var prontuario = dados.RelAtendimentoProntuario.ToList()[0].A9Prontuario;
						_sessao.IDProntuario = dados.RelAtendimentoProntuario.ToList()[0].A9Prontuario.A9ProntuarioId;
						foreach (var item in prontuario.RelHistoricoEvolucaoEnfermagem)
						{
							RegistroEnfermagemViewModel registro = new RegistroEnfermagemViewModel();
							registro.ID = item.A10RegistroEvolucaoEnfermagem.A10RegistroEvolucaoEnfermagemId;
							registro.Data = Convert.ToDateTime(item.A10RegistroEvolucaoEnfermagem.A10RegistroEvolucaoEnfermagemData).ToString("dd/MM/yyyy");
							registro.Hora = ((TimeSpan)item.A10RegistroEvolucaoEnfermagem.A10RegistroEvolucaoEnfermagemHora).ToString(@"hh\:mm\:ss");
							registro.Descricao = item.A10RegistroEvolucaoEnfermagem.A10RegistroEvolucaoEnfermagemDescrição;
							registro.Profissional = item.A10RegistroEvolucaoEnfermagem.A13ProfissionalCodigoCnsNavigation.A13ProfissionalNome;
							registro.EhRegistroNovo = item.A10RegistroEvolucaoEnfermagem.EhRegistroNovo;
							lstRegistroEnfermagemJaCadastrado.Add(registro);
						}
					}
					_sessao.RegistrosEvolucaoEnfermagem = lstRegistroEnfermagemJaCadastrado;

					return Json(new { DadosAtendimento = dados, RegistrosEnfermagem = _sessao.RegistrosEvolucaoEnfermagem.OrderBy(c => c.ID), MensagemSuceso = "", MensagemErro = "", MensagemAlerta = "" });
				}
				else
				{
					return Json(new { DadosAtendimento = "", MensagemErro = "", MensagemAlerta = atendimento.Mensagens[0].DescricaoMensagem, MensagemSucesso = "" });
				}
			}
			catch (Exception ex)
			{
				return Json(new { DadosAtendimento = "", MensagemErro = ex.Message, MensagemAlerta = "", MensagemSucesso = "" });
			}
		}

		[HttpPost]
		public IActionResult AdicionarRegistroEnfermagem(string descricao)
		{
			try
			{
				RegistroEnfermagemViewModel registro = new RegistroEnfermagemViewModel();
				if (_sessao.RegistrosEvolucaoEnfermagem != null && _sessao.RegistrosEvolucaoEnfermagem.Count > 0)
				{
					registro.ID = _sessao.RegistrosEvolucaoEnfermagem.OrderBy(c => c.ID).LastOrDefault().ID + 1;
				}
				else
				{
					registro.ID = 1;
				}
				registro.Descricao = descricao;
				registro.Data = DateTime.Now.ToString("dd/MM/yyyy");
				registro.Hora = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).ToString(@"hh\:mm\:ss");
				registro.Profissional = _sessao.UsuarioInterno.RelUsuarioInternoProfissional.ToList()[0].A13ProfissionalCodigoCnsNavigation.A13ProfissionalNome;
				registro.CodigoCNSProfissional = _sessao.UsuarioInterno.RelUsuarioInternoProfissional.ToList()[0].A13ProfissionalCodigoCns;
				registro.EhRegistroNovo = true;
				var listaRegistros = _sessao.RegistrosEvolucaoEnfermagem;
				listaRegistros.Add(registro);
				_sessao.RegistrosEvolucaoEnfermagem = listaRegistros;

				return Json(new { RegistrosEnfermagem = _sessao.RegistrosEvolucaoEnfermagem.OrderBy(c => c.ID), MensagemErro = "" });
			}
			catch (Exception ex)
			{
				return Json(new { RegistrosEnfermagem = "", MensagemErro = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult DeletarRegistroEnfermagem(int idRegistro)
		{
			try
			{
				var listaRegistro = _sessao.RegistrosEvolucaoEnfermagem;
				var registroDeletado = listaRegistro.Where(c => c.ID == idRegistro).FirstOrDefault();
				listaRegistro.Remove(registroDeletado);
				_sessao.RegistrosEvolucaoEnfermagem = listaRegistro;
				return Json(new { RegistrosEnfermagem = _sessao.RegistrosEvolucaoEnfermagem.OrderBy(c => c.ID), MensagemErro = "" });
			}
			catch (Exception ex)
			{
				return Json(new { RegistrosEnfermagem = "", MensagemErro = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult CadastrarProntuario(string prontuario)
		{
			try
			{
				ProntuarioViewModel prontuarioDados = JsonConvert.DeserializeObject<ProntuarioViewModel>(prontuario);
				prontuarioDados.RegistrosEnfermagem = _sessao.RegistrosEvolucaoEnfermagem.Where(c => c.EhRegistroNovo == true).ToList();

				A9Prontuario prontuarioHaCadastrar = new A9Prontuario();
				
				prontuarioHaCadastrar.RelAtendimentoProntuario = new List<RelAtendimentoProntuario>();
				prontuarioHaCadastrar.RelAtendimentoProntuario.Add(new RelAtendimentoProntuario() { A29AtendimentoId = _sessao.IDAtendimento });

				prontuarioHaCadastrar.A9ProntuarioCondutaTerapeuta = prontuarioDados.CondutaTerapeuta;
				prontuarioHaCadastrar.A9ProntuarioDescricaoCirurgica = prontuarioDados.DescricaoCirurgica;
				prontuarioHaCadastrar.A9ProntuarioDiagnosticoMedico = prontuarioDados.DiagnosticoMedico;
				prontuarioHaCadastrar.A9ProntuarioHipotesesDiagnostica = prontuarioDados.HipoteseDiagnostica;
				prontuarioHaCadastrar.A9ProntuarioPrescricaoMedica = prontuarioDados.PrescricaoMedica;
				prontuarioHaCadastrar.A9ProntuarioRaciocinioMedico = prontuarioDados.RaciocinioMedico;
				List<A10RegistroEvolucaoEnfermagem> lstRegistrosNovos = new List<A10RegistroEvolucaoEnfermagem>();
				foreach (var item in prontuarioDados.RegistrosEnfermagem)
				{
					A10RegistroEvolucaoEnfermagem registroEnfermagemNovo = new A10RegistroEvolucaoEnfermagem();
					registroEnfermagemNovo.A10RegistroEvolucaoEnfermagemData = Convert.ToDateTime(item.Data);
					registroEnfermagemNovo.A10RegistroEvolucaoEnfermagemDescrição = item.Descricao;
					string[] hora = item.Hora.Split(":");
					registroEnfermagemNovo.A10RegistroEvolucaoEnfermagemHora = new TimeSpan(Convert.ToInt32(hora[0]), Convert.ToInt32(hora[1]), Convert.ToInt32(hora[2]));
					registroEnfermagemNovo.A13ProfissionalCodigoCns = item.CodigoCNSProfissional;
					lstRegistrosNovos.Add(registroEnfermagemNovo);
				}
				List<int> IdRegistroNovos = new A10RegistroEvolucaoEnfermagemBusiness(_registroEvolucaoEnfermagemRepositorio, _profissionalRepositorio, _usuarioInternoRepositorio).CadastrarRegistrosEnfermagem(lstRegistrosNovos);

				foreach (var item in IdRegistroNovos)
				{
					prontuarioHaCadastrar.RelHistoricoEvolucaoEnfermagem.Add(new RelHistoricoEvolucaoEnfermagem { A10RegistroEvolucaoEnfermagemId = item });
				}

				bool resultado = new A9ProntuarioBusiness(_prontuarioRepositorio, _registroEvolucaoEnfermagemRepositorio, _relHistoricoEvolucaoEnfermagemRepositorio, _profissionalRepositorio, _usuarioInternoRepositorio).CadastrarProntuario(prontuarioHaCadastrar);

				if (resultado)
				{
					return Json(new { MensagemSucesso = Common.MensagensSistema.MsgsSistema.MsgProntuarioCadastrado, MensagemErro = "", MensagemAlerta = "" });
				}
				else 
				{
					return Json(new { MensagemSucesso = "", MensagemErro = "", MensagemAlerta = prontuarioHaCadastrar.Mensagens[0].DescricaoMensagem });
				}		
			}
			catch (Exception ex)
			{
				return Json(new { MensagemSucesso = "", MensagemErro = ex.Message, MensagemAlerta = "" });
			}
		}

		[HttpPost]
		public IActionResult AtualizarProntuario(string prontuario)
		{
			try
			{
				ProntuarioViewModel prontuarioDados = JsonConvert.DeserializeObject<ProntuarioViewModel>(prontuario);
				prontuarioDados.RegistrosEnfermagem = _sessao.RegistrosEvolucaoEnfermagem.Where(c => c.EhRegistroNovo == true).ToList();

				A9Prontuario prontuarioHaCadastrar = new A9Prontuario();
				prontuarioHaCadastrar.A9ProntuarioId = _sessao.IDProntuario;
				prontuarioHaCadastrar.A9ProntuarioCondutaTerapeuta = prontuarioDados.CondutaTerapeuta;
				prontuarioHaCadastrar.A9ProntuarioDescricaoCirurgica = prontuarioDados.DescricaoCirurgica;
				prontuarioHaCadastrar.A9ProntuarioDiagnosticoMedico = prontuarioDados.DiagnosticoMedico;
				prontuarioHaCadastrar.A9ProntuarioHipotesesDiagnostica = prontuarioDados.HipoteseDiagnostica;
				prontuarioHaCadastrar.A9ProntuarioPrescricaoMedica = prontuarioDados.PrescricaoMedica;
				prontuarioHaCadastrar.A9ProntuarioRaciocinioMedico = prontuarioDados.RaciocinioMedico;
				List<A10RegistroEvolucaoEnfermagem> lstRegistrosNovos = new List<A10RegistroEvolucaoEnfermagem>();
				foreach (var item in prontuarioDados.RegistrosEnfermagem)
				{
					A10RegistroEvolucaoEnfermagem registroEnfermagemNovo = new A10RegistroEvolucaoEnfermagem();
					registroEnfermagemNovo.A10RegistroEvolucaoEnfermagemData = Convert.ToDateTime(item.Data);
					registroEnfermagemNovo.A10RegistroEvolucaoEnfermagemDescrição = item.Descricao;
					string[] hora = item.Hora.Split(":");
					registroEnfermagemNovo.A10RegistroEvolucaoEnfermagemHora = new TimeSpan(Convert.ToInt32(hora[0]), Convert.ToInt32(hora[1]), Convert.ToInt32(hora[2]));
					registroEnfermagemNovo.A13ProfissionalCodigoCns = item.CodigoCNSProfissional;
					lstRegistrosNovos.Add(registroEnfermagemNovo);
				}

				bool resultado = new A9ProntuarioBusiness(_prontuarioRepositorio, _registroEvolucaoEnfermagemRepositorio, _relHistoricoEvolucaoEnfermagemRepositorio, _profissionalRepositorio, _usuarioInternoRepositorio).AtualizarProntuario(prontuarioHaCadastrar, lstRegistrosNovos);

				if (resultado)
				{
					return Json(new { MensagemSucesso = Common.MensagensSistema.MsgsSistema.MsgProntuarioAtualizado, MensagemErro = "", MensagemAlerta = "" });
				}
				else
				{
					return Json(new { MensagemSucesso = "", MensagemErro = "", MensagemAlerta = prontuarioHaCadastrar.Mensagens[0].DescricaoMensagem });
				}
			}
			catch (Exception ex)
			{
				return Json(new { MensagemSucesso = "", MensagemErro = ex.Message, MensagemAlerta = "" });
			}
		}
	}
}
