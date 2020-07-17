﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.GestaoSaude.Models
{
    [Table("A18_ProcedimentoExame")]
    public partial class A18ProcedimentoExame
    {
        public A18ProcedimentoExame()
        {
            RelHistoricoExamePaciente = new HashSet<RelHistoricoExamePaciente>();
        }

        [Key]
        [Column("A18_ProcedimentoExame_ID")]
        public int A18ProcedimentoExameId { get; set; }
        [Column("A18_ProcedimentoExame_Nome")]
        [StringLength(400)]
        public string A18ProcedimentoExameNome { get; set; }
        [Column("A18_ProcedimentoExame_SubGrupo")]
        [StringLength(250)]
        public string A18ProcedimentoExameSubGrupo { get; set; }
        [Column("A18_ProcedimentoExame_Grupo")]
        [StringLength(250)]
        public string A18ProcedimentoExameGrupo { get; set; }
        [Column("A18_ProcedimentoExame_Capitulo")]
        [StringLength(250)]
        public string A18ProcedimentoExameCapitulo { get; set; }

        [InverseProperty("A18ProcedimentoExame")]
        public virtual ICollection<RelHistoricoExamePaciente> RelHistoricoExamePaciente { get; set; }
    }
}