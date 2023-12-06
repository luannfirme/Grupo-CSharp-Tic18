using System;
using System.Collections.Generic;
using System.Linq;

namespace TechMedClinic
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }

        public Pessoa(string nome, DateTime dataNascimento, string cpf)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = cpf;
        }
    }

    public class Medico : Pessoa
    {
        public string CRM { get; set; }

        public Medico(string nome, DateTime dataNascimento, string cpf, string crm)
            : base(nome, dataNascimento, cpf)
        {
            CRM = crm;
        }
    }

    public class Paciente : Pessoa
    {
        public string Sexo { get; set; }
        public List<string> Sintomas { get; set; }

        public Paciente(string nome, DateTime dataNascimento, string cpf, string sexo, List<string> sintomas)
            : base(nome, dataNascimento, cpf)
        {
            Sexo = sexo;
            Sintomas = sintomas;
        }
    }

    public class Exame
    {
        public string Titulo { get; set; }
        public float Valor { get; set; }
        public string Descricao { get; set; }
        public string Local { get; set; }
    }

    public class Atendimento
    {
        public DateTime Inicio { get; set; }
        public string SuspeitaInicial { get; set; }
        public List<(Exame, string)> ListaExamesResultado { get; set; }
        public float Valor { get; set; }
        public DateTime Fim { get; set; }
        public Medico MedicoResponsavel { get; set; }
        public Paciente Paciente { get; set; }
        public string DiagnosticoFinal { get; set; }
    }

    public class ClinicaTechMed
    {
        public List<Medico> Medicos { get; set; }
        public List<Paciente> Pacientes { get; set; }
        public List<Exame> Exames { get; set; }
        public List<Atendimento> Atendimentos { get; set; }

        public ClinicaTechMed()
        {
            Medicos = new List<Medico>();
            Pacientes = new List<Paciente>();
            Exames = new List<Exame>();
            Atendimentos = new List<Atendimento>();
        }
    }

    class Program
    {
        static void Main()
        {
        }
    }
}
