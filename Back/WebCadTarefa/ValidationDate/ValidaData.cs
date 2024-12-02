using System.Globalization;

namespace WebCadTarefa.ValidationDate
{
    public class ValidaData
    {
        public static bool Validar(string Datacriacao, string Dataconclusao) 
        { 
            DateTime dataIni = new DateTime();
            DateTime dataAtual = new DateTime();

            DateTime.TryParseExact(Datacriacao, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dataIni);

            DateTime.TryParseExact(Dataconclusao, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dataAtual);

            return dataAtual < dataIni;
        }
    }
}
