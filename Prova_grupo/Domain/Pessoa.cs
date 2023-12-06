namespace Prova_grupo.Domain{
    public abstract class Pessoa{
        
        public string Nome{get; set;}
        public DateTime DataNascimento{get; set;}
        public string CPF{get; set;}

        public int IdadePessoa => DateTime.Now.Year - DataNascimento.Year;

        public Pessoa(string nome, DateTime dataNascimento, string Cpf){
            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = Cpf;
        }
    }
}