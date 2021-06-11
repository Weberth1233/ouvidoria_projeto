using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ouvidoria_Projeto.Models
{
    public class TipoManifesto
    {
        [Key]
        public int TipoManifestoId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Manifesto> Manifestos { get; set; }
    }
}
