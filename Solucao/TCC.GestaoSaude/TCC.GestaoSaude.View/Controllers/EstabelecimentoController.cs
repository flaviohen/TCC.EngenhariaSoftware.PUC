using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using TCC.GestaoSaude.Business;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.Models;
using TCC.GestaoSaude.View.SessionCustom;

namespace TCC.GestaoSaude.View.Controllers
{
	public class EstabelecimentoController : Controller
	{
		private readonly IA21EstabelecimentoRepositorio _estabelecimentoRepositorio;
		private readonly IA20TipoEstabelecimentoRepositorio _tipoEstabelecimentoRepositorio;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private Sessao _sessao;
		public EstabelecimentoController(IHttpContextAccessor httpContextAccessor,IA21EstabelecimentoRepositorio estabelecimentoRepositorio, IA20TipoEstabelecimentoRepositorio tipoEstabelecimentoRepositorio) 
		{
			_httpContextAccessor = httpContextAccessor;
			_estabelecimentoRepositorio = estabelecimentoRepositorio;
			_tipoEstabelecimentoRepositorio = tipoEstabelecimentoRepositorio;
			_sessao = new Sessao(httpContextAccessor);
		}
		public IActionResult GerenciarEstabelecimento()
		{
			ViewBag.Session = _sessao;
			ViewBag.TiposEstabelecimento = new A20TipoEstabelecimentoBusiness(_tipoEstabelecimentoRepositorio).RetornarTiposEstabelecimento();

			return View();
		}

		[HttpPost]
		public IActionResult PesquisarEstabelecimento(string tpEstabelecimento, string numCep, string CNES) 
		{
			try
			{
				A20TipoEstabelecimento tipoEstabelecimento = null;
				if (tpEstabelecimento != "0") 
				{
					tipoEstabelecimento = new A20TipoEstabelecimentoBusiness(_tipoEstabelecimentoRepositorio)
										  .RetornarTiposEstabelecimento().Where(c => c.A20TipoEstabelecimentoId == Convert.ToInt32(tpEstabelecimento)).FirstOrDefault();
				}
				
				var estabelecimentos = new A21EstabelecimentoBusiness(_estabelecimentoRepositorio,_tipoEstabelecimentoRepositorio).BuscarEstabelecimento(tipoEstabelecimento, numCep, CNES);

				if (estabelecimentos != null && estabelecimentos.Count > 0)
				{
					string jsonEstabelecimentos = JsonConvert.SerializeObject(estabelecimentos, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					estabelecimentos = JsonConvert.DeserializeObject<List<A21Estabelecimento>>(jsonEstabelecimentos);
					return Json(new { Estabelecimentos = estabelecimentos, MensagemErro = "", MensagemAlerta = "", MensagemSucesso = "" });
				}
				else 
				{
					return Json(new { Estabelecimentos = "", MensagemErro = "", MensagemAlerta = Common.MensagensSistema.MsgsSistema.MsgEstabelecimentosNaoEncontrado, MensagemSucesso = ""});
				}	
			}
			catch (Exception ex)
			{
				return Json(new { Estabelecimentos = "", MensagemErro = ex.Message, MensagemAlerta = "", MensagemSucesso = "" });
			}		
		}

		[HttpPost]
		public IActionResult CadastrarEstabelecimento(string dadosEstabelecimento) 
		{
			try
			{
				A21Estabelecimento estabelecimento = JsonConvert.DeserializeObject<A21Estabelecimento>(dadosEstabelecimento);
				bool resultado = new A21EstabelecimentoBusiness(_estabelecimentoRepositorio,_tipoEstabelecimentoRepositorio).CadastrarEstabelecimento(estabelecimento);
				if (resultado)
				{
					return Json(new { MensagemSucesso = Common.MensagensSistema.MsgsSistema.MsgEstabelecimentoCadastradoSucesso, MensagemErro = "", MensagemAlerta = "" });
				}
				else 
				{
					return Json(new { MensagemSucesso = "", MensagemErro = "", MensagemAlerta = estabelecimento.Mensagens[0].DescricaoMensagem });
				}
				
			}
			catch (Exception ex)
			{

				return Json(new { MensagemErro = ex.Message, MensagemSucesso = "", MensagemAlerta = "" });
			}
			
		}

		[HttpPost]
		public IActionResult DetalhesEstabelecimento(int idEstabelecimento) 
		{
			try
			{
				var estabelecimento = new A21EstabelecimentoBusiness(_estabelecimentoRepositorio,_tipoEstabelecimentoRepositorio).BuscarEstabelecimentoPorCodigo(idEstabelecimento);
				if (estabelecimento.Mensagens.Count == 0) 
				{
					string estabelecimentoJson = JsonConvert.SerializeObject(estabelecimento, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					estabelecimento = JsonConvert.DeserializeObject<A21Estabelecimento>(estabelecimentoJson);
					return Json(new { Estabelecimento = estabelecimento, MensagemErro = "", MensagemAlerta = "", MensagemSucesso = "" });
				}
				else
					return Json(new { Estabelecimento = "", MensagemErro = "", MensagemAlerta = estabelecimento.Mensagens[0].DescricaoMensagem, MensagemSucesso = "" });
			}
			catch (Exception ex)
			{
				return Json(new { MensagemErro = ex.Message, MensagemAlerta = "", MensagemSucesso = "" });
			}		
		}

		[HttpPost]
		public IActionResult DeletarEstabelecimento(int idEstabelecimento)
		{
			try
			{
				var estabelecimento = new A21EstabelecimentoBusiness(_estabelecimentoRepositorio,_tipoEstabelecimentoRepositorio).ExcluirEstabelecimento(idEstabelecimento);
				if (estabelecimento)
					return Json(new { MensagemErro = "", MensagemAlerta = "", MensagemSucesso = Common.MensagensSistema.MsgsSistema.MSgEstabelecimentoExcluidoSucesso }) ;
				else
					return Json(new { MensagemErro = "", MensagemAlerta = Common.MensagensSistema.MsgsSistema.MSgNaoPossivelExclusaoEstabelecimento, MensagemSucesso = "" });
			}
			catch (Exception ex)
			{
				return Json(new { MensagemErro = ex.Message, MensagemAlerta = "", MensagemSucesso = "" });
			}
		}
	}
}
