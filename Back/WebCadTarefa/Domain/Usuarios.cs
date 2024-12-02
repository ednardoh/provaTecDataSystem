namespace WebCadTarefa.Domain
{
    public class Usuarios
    {
        public int    ID        { get; set; }
        public string Username  { get; set; }
        public string Senha     { get; set; }
        public bool   Bloquear  { get; set; }
    }
}
