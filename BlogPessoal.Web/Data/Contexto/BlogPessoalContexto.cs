using BlogPessoal.Web.Data.Mapeamento;
using BlogPessoal.Web.Models.CategoriasDeArtigo;
using System.Data.Entity;

namespace BlogPessoal.Web.Data.Contexto
{
    public class BlogPessoalContexto : DbContext
    {
        public DbSet<CategoriaDeArtigo> CategoriaDeArtigo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoriaDeArtigoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}