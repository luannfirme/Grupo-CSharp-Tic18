namespace Prova_grupo.Domain
{
    public class PlanoDeSaude
    {
        public string Titulo { get; set; }
        public double ValorMensal { get; set; }

         public PlanoDeSaude(string titulo, double valor){
            Titulo = titulo;
            ValorMensal = valor;
        }
    }
}
