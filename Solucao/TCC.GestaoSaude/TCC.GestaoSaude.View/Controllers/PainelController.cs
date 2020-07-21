using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TCC.GestaoSaude.View.Controllers
{
	public class PainelController : Controller
	{
		public IActionResult PaginaInicial()
		{
			return View();
		}
	}
}
