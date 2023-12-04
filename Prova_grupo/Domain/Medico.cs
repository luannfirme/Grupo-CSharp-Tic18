namespace Prova_grupo.Domain{

    public class Medico: Pessoa {

        public Medico(int id, string nome, DateTime dataNascimento, string cpf, string Crm)
            : base(nome, dataNascimento, cpf){

            if (string.IsNullOrWhiteSpace(cpf)){
                throw new Exception("CPF inválido");
            }

        Id = id;
        CRM = Crm;
    }
        public int Id {get; set; }
        public string CRM{get; set;}


    }
}
