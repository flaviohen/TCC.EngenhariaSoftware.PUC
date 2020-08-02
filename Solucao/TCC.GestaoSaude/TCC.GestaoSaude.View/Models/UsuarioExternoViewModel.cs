using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.GestaoSaude.Models;

namespace TCC.GestaoSaude.View.Models
{
	public class UsuarioExternoViewModel
	{

        public int A1UsuarioId { get; set; }

        public string A1UsuarioNumeroCpf { get; set; }
 
        public string A1UsuarioNome { get; set; }

        public string A1UsuarioSenha { get; set; }
        public List<A3InformacaoCadastro> A3InformacaoCadastro { get; set; }
 
        public List<RelUsuarioPerfil> RelUsuarioPerfil { get; set; }
    }

    public partial class RelUsuarioPerfil 
    {
        public A6Perfil A6Perfil { get; set; }
    }
}
