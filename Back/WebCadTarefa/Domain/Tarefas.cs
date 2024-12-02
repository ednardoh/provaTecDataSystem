namespace WebCadTarefa.Domain
{
    public class Tarefas
    {
        public int     ID               { get; set; }
        public string  Titulotarefa     { get; set; }
        public string  Descricaotarefa  { get; set; }
        public string  Datacriacao      { get; set; }
        public string  Dataconclusao    { get; set; }
        public string  Status           { get; set; }
    }
}
