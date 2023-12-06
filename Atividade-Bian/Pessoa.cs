using System;

public class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }

    public int Idade => DateTime.Now.Year - DataNascimento.Year;
}
