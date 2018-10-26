using BlogPessoal.Web.Models.Artigos;
using System.Collections.Generic;

namespace BlogPessoal.Web.Models.CategoriasDeArtigo
{
    public class CategoriaDeArtigo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Artigo> Artigos { get; set; }
    }
}