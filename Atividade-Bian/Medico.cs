public class Medico : Pessoa
{
    public string CPF { get; set; }
    public string CRM { get; set; }

    public Medico(string nome, DateTime dataNascimento, string cpf, string crm)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        CRM = crm;
    }
}
