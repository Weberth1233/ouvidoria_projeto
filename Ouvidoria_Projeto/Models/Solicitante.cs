using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ouvidoria_Projeto.Models
{
    public class Solicitante:ApplicationUser
    {
        [Display(Name="Data de Nascimento")]
        public DateTime DataNascimento;
        public int Status { get; set; }
        public Sexo Sexo { get; set; }
    }
}

namespace Ouvidoria_Projeto
{
    public enum Sexo
    {
        Masculino=1,
        Feminino=2,
        Outro=3,
    }
}