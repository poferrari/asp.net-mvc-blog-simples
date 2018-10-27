using System;
using System.Diagnostics;
using System.Linq;
using BlogPessoal.Web.Data.Contexto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoal.Tests
{
    [TestClass]
    public class ContextoTest
    {
        [TestMethod]
        public void Testar_Entidade_Categoria_De_Artigo()
        {
            try
            {               
                using (var ctx = new BlogPessoalContexto())
                {
                    var categoria = ctx.CategoriasDeArtigo.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Assert.Fail();
            }
        }
    }
}
