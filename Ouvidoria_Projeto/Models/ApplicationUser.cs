using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ouvidoria_Projeto.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name ="Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name ="Telefone")]
        [Phone]
        public string Numero { get; set; }
        public int ESolicitante { get; set; }

        public virtual ICollection<ManifestoSolicitante> ManifestosSolicitantes { get; set; }

        public virtual ICollection<Resposta> Respostas { get; set; }

    }
}
