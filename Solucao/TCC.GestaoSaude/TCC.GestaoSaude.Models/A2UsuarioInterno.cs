﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC.GestaoSaude.Common;

namespace TCC.GestaoSaude.Models
{
    [Table("A2_UsuarioInterno")]
    public partial class A2UsuarioInterno
    {
        public A2UsuarioInterno()
        {
            RelUsuarioInternoPerfil = new HashSet<RelUsuarioInternoPerfil>();
            RelUsuarioInternoProfissional = new HashSet<RelUsuarioInternoProfissional>();
            Mensagens = new List<Mensagem>();
        }

        [Key]
        [Column("A2_UsuarioInterno_ID")]
        public int A2UsuarioInternoId { get; set; }
        [Column("A2_UsuarioInterno_Nome")]
        [StringLength(100)]
        public string A2UsuarioInternoNome { get; set; }
        [Column("A2_UsuarioInterno_Email")]
        [StringLength(100)]
        public string A2UsuarioInternoEmail { get; set; }
        [Column("A2_UsuarioInterno_Telefone")]
        [StringLength(20)]
        public string A2UsuarioInternoTelefone { get; set; }
        [Column("A2_UsuarioInterno_Senha")]
        [StringLength(20)]
        public string A2UsuarioInternoSenha { get; set; }

        [InverseProperty("A2UsuarioInterno")]
        public virtual ICollection<RelUsuarioInternoPerfil> RelUsuarioInternoPerfil { get; set; }
        [InverseProperty("A2UsuarioInterno")]
        public virtual ICollection<RelUsuarioInternoProfissional> RelUsuarioInternoProfissional { get; set; }

        [NotMapped]
        public List<Mensagem> Mensagens { get; set; }
    }
}