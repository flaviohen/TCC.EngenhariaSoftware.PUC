﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC.GestaoSaude.Common;

namespace TCC.GestaoSaude.Models
{
    [Table("A13_Profissional")]
    public partial class A13Profissional
    {
        public A13Profissional()
        {
            A10RegistroEvolucaoEnfermagem = new HashSet<A10RegistroEvolucaoEnfermagem>();
            A11RegistroEvolucaoMedicaDiaria = new HashSet<A11RegistroEvolucaoMedicaDiaria>();
            A15CargaHorariaProfissional = new HashSet<A15CargaHorariaProfissional>();
            A24RecomendacaoMedica = new HashSet<A24RecomendacaoMedica>();
            RelProfissionalOcupacao = new HashSet<RelProfissionalOcupacao>();
            RelUsuarioInternoProfissional = new HashSet<RelUsuarioInternoProfissional>();
            Mensagens = new List<Mensagem>();
        }

        [Key]
        [Column("A13_Profissional_CodigoCNS")]
        [StringLength(350)]
        public string A13ProfissionalCodigoCns { get; set; }
        [Column("A13_Profissional_CodigoSus")]
        [StringLength(350)]
        public string A13ProfissionalCodigoSus { get; set; }
        [Column("A13_Profissional_Nome")]
        [StringLength(350)]
        public string A13ProfissionalNome { get; set; }
        [Column("A13_Profissional_Data", TypeName = "datetime2(0)")]
        public DateTime? A13ProfissionalData { get; set; }

        [InverseProperty("A13ProfissionalCodigoCnsNavigation")]
        public virtual ICollection<A10RegistroEvolucaoEnfermagem> A10RegistroEvolucaoEnfermagem { get; set; }
        [InverseProperty("A13ProfissionalCodigoCnsNavigation")]
        public virtual ICollection<A11RegistroEvolucaoMedicaDiaria> A11RegistroEvolucaoMedicaDiaria { get; set; }
        [InverseProperty("A13ProfissionalCodigoCnsNavigation")]
        public virtual ICollection<A15CargaHorariaProfissional> A15CargaHorariaProfissional { get; set; }
        [InverseProperty("A13ProfissionalCodigoCnsNavigation")]
        public virtual ICollection<A24RecomendacaoMedica> A24RecomendacaoMedica { get; set; }
        [InverseProperty("A13ProfissionalCodigoCnsNavigation")]
        public virtual ICollection<RelProfissionalOcupacao> RelProfissionalOcupacao { get; set; }
        [InverseProperty("A13ProfissionalCodigoCnsNavigation")]
        public virtual ICollection<RelUsuarioInternoProfissional> RelUsuarioInternoProfissional { get; set; }

        [NotMapped]
        public List<Mensagem> Mensagens { get; set; }
    }
}