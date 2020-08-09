using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
				A20TipoEstabelecimento tipoEstabelecimento = new A20TipoEstabelecimentoBusiness(_tipoEstabelecimentoRepositorio).
																RetornarTiposEstabelecimento().Where(c => c.A20TipoEstabelecimentoId == Convert.ToInt32(tpEstabelecimento)).FirstOrDefault();
				var estabelecimentos = new A21EstabelecimentoBusiness(_estabelecimentoRepositorio).BuscarEstabelecimento(tipoEstabelecimento, numCep, CNES);

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
	}
}
