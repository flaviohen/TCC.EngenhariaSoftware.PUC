﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCC.GestaoSaude.Models
{
    [Table("Rel_ProfissionalOcupacao")]
    public partial class RelProfissionalOcupacao
    {
        [Key]
        [Column("A13_Profissional_CodigoSus")]
        [StringLength(350)]
        public string A13ProfissionalCodigoSus { get; set; }
        [Key]
        [Column("A12_Ocupacao_Codigo")]
        [StringLength(250)]
        public string A12OcupacaoCodigo { get; set; }
        [Column("A21_Estabelecimento_ID")]
        public int A21EstabelecimentoId { get; set; }

        [ForeignKey(nameof(A12OcupacaoCodigo))]
        [InverseProperty(nameof(A12Ocupacao.RelProfissionalOcupacao))]
        public virtual A12Ocupacao A12OcupacaoCodigoNavigation { get; set; }
        [ForeignKey(nameof(A13ProfissionalCodigoSus))]
        [InverseProperty(nameof(A13Profissional.RelProfissionalOcupacao))]
        public virtual A13Profissional A13ProfissionalCodigoSusNavigation { get; set; }
        [ForeignKey(nameof(A21EstabelecimentoId))]
        [InverseProperty("RelProfissionalOcupacao")]
        public virtual A21Estabelecimento A21Estabelecimento { get; set; }
    }
}