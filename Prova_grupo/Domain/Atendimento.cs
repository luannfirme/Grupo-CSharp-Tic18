
namespace Prova_grupo.Domain
{
    public class Atendimento
    {
        public int Id {get; set; }
        public DateTime Inicio { get; set; }
        public string SuspeitaInicial { get; set; }
        public List<(Exame, string)>ListaExamesResultados { get; set; }  
        public float Valor { get; set; }  
        public DateTime? Fim { get; set; }  
        public Medico MedicoResponsavel { get; set; }  
        public Paciente _Paciente { get; set; }  
        public string? DiagnosticoFinal { get; set; } 

        public Atendimento(int id, DateTime inicio, string suspeitaInicial, List<(Exame, string)> examesResultado, float valor,Medico medicoResponsavel, Paciente paciente){
            Id = id;
            Inicio = inicio;
            SuspeitaInicial = suspeitaInicial;
            ListaExamesResultados = examesResultado;
            Valor = valor;
            Fim = null;
            MedicoResponsavel = medicoResponsavel;
            _Paciente = paciente;
            DiagnosticoFinal = null;
        }
    }
}