using System.Collections.Specialized;

namespace Prova_grupo.Domain
{
    public class Exame
    {
        public string Titulo { get; set; }
        public float Valor { get; set; }
        public string Descricao { get; set;}
        public string Local { get; set;}
        public Exame(string titulo, float valor, string descricao, string local){
            Titulo = titulo;
            Valor = valor;
            Descricao = descricao;
            Local = local;
        }
    }
}