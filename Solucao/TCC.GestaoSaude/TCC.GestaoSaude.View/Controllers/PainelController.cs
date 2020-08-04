using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		private readonly IEmailEnvio _emailEnvio;


		private Sessao _sessao;
		public PainelController(IHttpContextAccessor httpContextAccessor,
								IA1UsuarioRepositorio usuarioRepositorio,
								IA6PerfilRepositorio perfilRepositorio,
								IA13ProfissionalRepositorio profissionalRepositorio,
								IA2UsuarioInternoRepositorio usuarioInternoRepositorio,
								IA29AtendimentoRepositorio atendimentoRepositorio,
								IEmailEnvio emailEnvio)
		{
			_httpContextAccessor = httpContextAccessor;
			_usuarioRepositorio = usuarioRepositorio;
			_perfilRepositorio = perfilRepositorio;
			_profissionalRepositorio = profissionalRepositorio;
			_usuarioInternoRepositorio = usuarioInternoRepositorio;
			_atendimentoRepositorio = atendimentoRepositorio;
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
				A29AtendimentoBusiness atendimentoNegocio = new A29AtendimentoBusiness(_atendimentoRepositorio, null, null);
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

							return Json(new { NumeroAtendimento = atendimento.A29AtendimentoId, Mensagem = "" , MensagemErro = "" });
						}
						else
						{
							return Json(new { NumeroAtendimento = "0", MensagemErro = "", Mensagem = atendimento.Mensagens[0].DescricaoMensagem });
						}
					}
					else
					{
						return Json(new { NumeroAtendimento = "0", MensagemErro = "", Mensagem = usuarioCadastro.Mensagens[0].DescricaoMensagem});
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
	}
}
