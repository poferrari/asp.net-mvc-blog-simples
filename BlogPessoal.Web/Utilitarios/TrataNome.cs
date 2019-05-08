using System.Globalization;
using System.Text;

namespace BlogPessoal.Web.Utilitarios
{
    public static class TrataNome
    {
        public static string RemoverAcentos(string texto)
        {
            var s = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (var i = 0; i < s.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(s[i]);
            }
            return sb.ToString();
        }
        public static string RemoverHtml(string nome)
        {
            return nome.Replace("<b>", "").Replace("</b>", "").Replace("<mark>", "").Replace("</mark>", "").Trim();
        }

        public static string ReescreverNome(string nome)
        {
            nome = RemoverHtml(nome);
            nome = nome.Replace("-", " ");
            nome = RemoverAcentos(nome);
            nome = nome.ToLower().Replace(" ", "-");
            char[] trim = { '=', '\\', ';', '.', ':', ',', '+', '*', '&', '/', '"', '\'', '(', ')', '_', '%', '+', '<', '>', '{', '}', '[', ']', 'ª', 'º', '®', '°' };
            int pos;
            while ((pos = nome.IndexOfAny(trim)) >= 0)
                nome = nome.Remove(pos, 1);
            nome = nome.Replace("---", "-");
            nome = nome.Replace("--", "-");
            if (nome.Length > 180)
                nome = nome.Substring(0, 180);
            return nome;
        }
    }
}