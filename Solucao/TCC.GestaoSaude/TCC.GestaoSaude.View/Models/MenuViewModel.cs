using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.View.Models
{
	public class MenuViewModel
	{
		public List<ItemMenu> items { get; set; }

		public MenuViewModel(List<A6Perfil> perfis) 
		{
			this.items = MontarMenu(perfis);
		}

		private List<ItemMenu> MontarMenu(List<A6Perfil> perfis) 
		{
			List<ItemMenu> menu = new List<ItemMenu>();

			foreach (var perfil in perfis)
			{
				if (perfil.A6PerfilId == (int)Perfil.Administrador) 
				{
					menu.AddRange(MenuAdministrador());
				}
				if (perfil.A6PerfilId == (int)Perfil.Atendente) 
				{
					menu.AddRange(MenuAtendente());
				}
				if (perfil.A6PerfilId == (int)Perfil.Paciente) 
				{
					menu.AddRange(MenuPaciente());
				}
				if (perfil.A6PerfilId == (int)Perfil.ProfissionalSaude) 
				{
					menu.AddRange(MenuProfissionalSaude());
				}
			}
			menu.AddRange(MenuTodos());
			return RemoverItemMenuRepetido(menu);
		}

		private List<ItemMenu> MenuAdministrador() 
		{
			List<ItemMenu> items = new List<ItemMenu>();
			ItemMenu item = new ItemMenu();
			item.Id = 1;
			item.Acao = "EstoqueMedicamentos";
			item.Controller = "Painel";
			item.Icone = "icon icon-book";
			item.TextoMenu = "Gerenciar estoque de Medicamentos";
			items.Add(item);

			item = new ItemMenu();
			item.Id = 2;
			item.Acao = "GerenciarEstabelecimentoSaude";
			item.Controller = "Painel";
			item.Icone = "icon icon-sitemap";
			item.TextoMenu = "Gerenciar estabelecimentos de Saúde";
			items.Add(item);

			item = new ItemMenu();
			item.Id = 3;
			item.Acao = "GerenciarProfissionalSaude";
			item.Controller = "Painel";
			item.Icone = "icon icon-group";
			item.TextoMenu = "Gerenciar profissionais de saúde";
			items.Add(item);

			item = new ItemMenu();
			item.Id = 4;
			item.Acao = "GerenciarUsuarioSistema";
			item.Controller = "Painel";
			item.Icone = "icon icon-group";
			item.TextoMenu = "Gerenciar usuarios do sistema";
			items.Add(item);

			item = new ItemMenu();
			item.Id = 5;
			item.Acao = "GerenciarTiposExameProcedimento";
			item.Controller = "Painel";
			item.Icone = "icon icon-list-ol";
			item.TextoMenu = "Gerenciar tipos de exame e procedimentos";
			items.Add(item);

			return items;
		}
		private List<ItemMenu> MenuTodos() 
		{
			List<ItemMenu> items = new List<ItemMenu>();
			ItemMenu item = new ItemMenu();
			item.Id = 6;
			item.Acao = "AgendarConsultaExame";
			item.Controller = "Painel";
			item.Icone = "icon icon-book";
			item.TextoMenu = "Agendar consulta ou exame";
			items.Add(item);

			item = new ItemMenu();
			item.Id = 7;
			item.Acao = "PesquisarDoenca";
			item.Controller = "Painel";
			item.Icone = "icon icon-search";
			item.TextoMenu = "Pesquisar doença";
			items.Add(item);


			return items;
		}
		private List<ItemMenu> MenuPaciente() 
		{
			List<ItemMenu> items = new List<ItemMenu>();
			ItemMenu item = new ItemMenu();
			item.Id = 8;
			item.Acao = "VisualizarProntuario";
			item.Controller = "Painel";
			item.Icone = "icon icon-book";
			item.TextoMenu = "Visualizar Prontuário";
			items.Add(item);

			return items;
		}
		private List<ItemMenu> MenuProfissionalSaude() 
		{
			List<ItemMenu> items = new List<ItemMenu>();
			ItemMenu item = new ItemMenu();
			item.Id = 9;
			item.Acao = "VisualizarProntuario";
			item.Controller = "Painel";
			item.Icone = "icon icon-book";
			item.TextoMenu = "Visualizar Prontuário";
			items.Add(item);

			item = new ItemMenu();
			item.Id = 10;
			item.Acao = "PreencherProntuario";
			item.Controller = "Painel";
			item.Icone = "icon icon-book";
			item.TextoMenu = "Preencher Prontuário";
			items.Add(item);

			item = new ItemMenu();
			item.Id = 11;
			item.Acao = "EmissaoRelatorio";
			item.Controller = "Painel";
			item.Icone = "icon icon-book";
			item.TextoMenu = "Emissão de Relatório";
			items.Add(item);

			item = new ItemMenu();
			item.Id = 12;
			item.Acao = "Recomendar";
			item.Controller = "Painel";
			item.Icone = "icon icon-book";
			item.TextoMenu = "Recomendar tratamento ou consulta";
			items.Add(item);

			return items;
		}
		private List<ItemMenu> MenuAtendente()
		{
			List<ItemMenu> items = new List<ItemMenu>();
			ItemMenu item = new ItemMenu();
			item.Id = 13;
			item.Acao = "CadastrarAtendimento";
			item.Controller = "Painel";
			item.Icone = "icon icon-book";
			item.TextoMenu = "Novo atendimento";
			items.Add(item);

			item = new ItemMenu();
			item.Id = 14;
			item.Acao = "EmissaoRelatorio";
			item.Controller = "Painel";
			item.Icone = "icon icon-book";
			item.TextoMenu = "Emissão de Relatório";
			items.Add(item);

			return items;
		}

		private List<ItemMenu> RemoverItemMenuRepetido(List<ItemMenu> items) 
		{
			int IDVisualizarProntuarioPaciente = 8;
			int IDVisualizarProntuarioProfissionalSaude = 9;
			var menuRepetido = items.Where(c => c.Id == IDVisualizarProntuarioPaciente && c.Id == IDVisualizarProntuarioProfissionalSaude).ToList();

			if (menuRepetido.Count > 1) 
			{
				items.Remove(menuRepetido[0]);
			}

			int IDEmitirRelatorioProfissionalSaude = 11;
			int IDEmitirRelatorioAtendente = 14;

			var menuRepetido2 = items.Where(c => c.Id == IDEmitirRelatorioProfissionalSaude && c.Id == IDEmitirRelatorioAtendente).ToList();
			if (menuRepetido2.Count > 1) 
			{
				items.Remove(menuRepetido2[0]);
			}

			return items;
		}
	}

	public enum Perfil 
	{
		Administrador = 1,
		Atendente = 2,
		ProfissionalSaude = 3,
		Paciente = 4
	}

	public class ItemMenu 
	{
		public int Id { get; set; }
		public string Acao { get; set; }
		public string Controller { get; set; }
		public string Icone { get; set; }
		public string TextoMenu { get; set; }
	}
}
