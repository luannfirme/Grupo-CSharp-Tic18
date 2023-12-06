namespace Prova_grupo.Domain{
  public class Paciente: Pessoa{

    public int IdPaciente {get; set; }
    public string Sexo{get; set;}
    public PlanoDeSaude PlanoPaciente { get; set; } = new PlanoDeSaude();
    public List<string> Sintomas{get; set;}

        public Paciente(int idPaciente, string nome, DateTime dataNascimento, string cpf, string sexo, List<string> sintomas, Plano)
            : base(nome, dataNascimento, cpf){
                
            if (string.IsNullOrWhiteSpace(cpf)){
                throw new Exception("CPF inválido");
            }
            IdPaciente = idPaciente;
            Sexo = sexo;
            Sintomas = sintomas;
        }
    }
}