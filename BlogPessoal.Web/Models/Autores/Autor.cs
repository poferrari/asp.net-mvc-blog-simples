using BlogPessoal.Web.Models.Artigos;
using System;
using System.Collections.Generic;

namespace BlogPessoal.Web.Models.Autores
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Administrador { get; set; }
        public DateTime DataDeCadastro { get; set; }

        public virtual ICollection<Artigo> Artigos { get; set; }
    }
}