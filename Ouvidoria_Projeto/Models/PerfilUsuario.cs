using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ouvidoria_Projeto.Models
{
    public class PerfilUsuario
    {
        [Display(Name = "Código")]

        [Column("Id")]
        public int Id { get; set; }
        [Display(Name = "TipoUsuario")]
        [ForeignKey("TipoUsuario")]
        [Column(Order = 1)]
        public int IdTipoUsuario { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }  
        
        [Display(Name = "Usuário")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}
