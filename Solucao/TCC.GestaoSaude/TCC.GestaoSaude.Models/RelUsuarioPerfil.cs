﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.GestaoSaude.Models
{
    [Table("Rel_UsuarioPerfil")]
    public partial class RelUsuarioPerfil
    {
        [Key]
        [Column("A1_Usuario_ID")]
        public int A1UsuarioId { get; set; }
        [Key]
        [Column("A6_Perfil_ID")]
        public int A6PerfilId { get; set; }

        [ForeignKey(nameof(A1UsuarioId))]
        [InverseProperty("RelUsuarioPerfil")]
        public virtual A1Usuario A1Usuario { get; set; }
        [ForeignKey(nameof(A6PerfilId))]
        [InverseProperty("RelUsuarioPerfil")]
        public virtual A6Perfil A6Perfil { get; set; }
    }
}