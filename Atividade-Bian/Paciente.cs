using System.Collections.Generic;

public class Paciente : Pessoa
{
    public string CPF { get; set; }
    public string Sexo { get; set; }
    public List<string> Sintomas { get; set; }

    public Paciente(string nome, DateTime dataNascimento, string cpf, string sexo, List<string> sintomas)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
        Sexo = sexo;
        Sintomas = sintomas;
    }
}
