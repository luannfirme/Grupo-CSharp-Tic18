using System;
using System.Collections.Generic;
using System.Linq;

namespace TechMedClinic
{
    public enum Sexo
    {
        Masculino,
        Feminino
    }

    public class Pessoa
    {
        private string cpf;

        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string CPF
        {
            get => cpf;
            private set
            {
                if (value.Length == 11)
                {
                    cpf = value;
                }
                else
                {
                    throw new ArgumentException("O CPF deve ter 11 dígitos.");
                }
            }
        }

        public Pessoa(string nome, DateTime dataNascimento, string cpf)
        {
            this.cpf = string.Empty;
            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = cpf;
        }

        public virtual void Apresentar()
        {
            Console.WriteLine($"Nome: {Nome}, Data de Nascimento: {DataNascimento.ToShortDateString()}, CPF: {CPF}");
        }
    }

    public class Paciente : Pessoa
    {
        public Sexo Sexo { get; private set; }
        public List<string> Sintomas { get; private set; }

        public Paciente(string nome, DateTime dataNascimento, string cpf, Sexo sexo, List<string> sintomas)
            : base(nome, dataNascimento, cpf)
        {
            Sexo = sexo;
            Sintomas = sintomas;
        }

        public override void Apresentar()
        {
            Console.WriteLine($"Paciente - Sexo: {Sexo}, {base.Nome}, {base.DataNascimento.ToShortDateString()}, CPF: {base.CPF}, Sintomas: {string.Join(", ", Sintomas)}");
        }
    }

    public class Medico : Pessoa
    {
        public string CRM { get; private set; }

        public Medico(string nome, DateTime dataNascimento, string cpf, string crm)
            : base(nome, dataNascimento, cpf)
        {
            CRM = crm;
        }

        public override void Apresentar()
        {
            Console.WriteLine($"Médico - CRM: {CRM}, {base.Nome}, {base.DataNascimento.ToShortDateString()}, CPF: {base.CPF}");
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
        private List<Medico> medicos;
        private List<Paciente> pacientes;
        private List<Exame> exames;
        private List<Atendimento> atendimentos;

        public ClinicaTechMed()
        {
            medicos = new List<Medico>();
            pacientes = new List<Paciente>();
            exames = new List<Exame>();
            atendimentos = new List<Atendimento>();
        }

        public void AdicionarMedico(Medico medico)
        {
            medicos.Add(medico);
        }

        public void RemoverMedico(Medico medico)
        {
            medicos.Remove(medico);
        }

        public void AdicionarPaciente(Paciente paciente)
        {
            pacientes.Add(paciente);
        }

        public void RemoverPaciente(Paciente paciente)
        {
            pacientes.Remove(paciente);
        }

        public void AdicionarExame(Exame exame)
        {
            exames.Add(exame);
        }

        public void RemoverExame(Exame exame)
        {
            exames.Remove(exame);
        }

        public void IniciarAtendimento(Atendimento atendimento)
        {
            atendimento.Inicio = DateTime.Now;
            atendimentos.Add(atendimento);
        }

        public void FinalizarAtendimento(Atendimento atendimento, string diagnosticoFinal)
        {
            atendimento.Fim = DateTime.Now;
            atendimento.DiagnosticoFinal = diagnosticoFinal;
        }

        public void GerarRelatorioMedicosIdade(int idadeMinima, int idadeMaxima)
        {
            var medicosFiltrados = medicos.Where(medico =>
                (DateTime.Now.Year - medico.DataNascimento.Year) >= idadeMinima &&
                (DateTime.Now.Year - medico.DataNascimento.Year) <= idadeMaxima);

            Console.WriteLine($"\nRelatório de Médicos com idade entre {idadeMinima} e {idadeMaxima} anos:");
            foreach (var medico in medicosFiltrados)
            {
                medico.Apresentar();
            }
        }

        public void GerarRelatorioPacientesIdade(int idadeMinima, int idadeMaxima)
        {
            var pacientesFiltrados = pacientes.Where(paciente =>
                (DateTime.Now.Year - paciente.DataNascimento.Year) >= idadeMinima &&
                (DateTime.Now.Year - paciente.DataNascimento.Year) <= idadeMaxima);

            Console.WriteLine($"\nRelatório de Pacientes com idade entre {idadeMinima} e {idadeMaxima} anos:");
            foreach (var paciente in pacientesFiltrados)
            {
                paciente.Apresentar();
            }
        }

        public void GerarRelatorioPacientesSexo(Sexo sexo)
        {
            var pacientesFiltrados = pacientes.Where(paciente => paciente.Sexo == sexo);

            Console.WriteLine($"\nRelatório de Pacientes do sexo {sexo}:");
            foreach (var paciente in pacientesFiltrados)
            {
                paciente.Apresentar();
            }
        }

        public void GerarRelatorioPacientesOrdemAlfabetica()
        {
            var pacientesOrdenados = pacientes.OrderBy(paciente => paciente.Nome);

            Console.WriteLine("\nRelatório de Pacientes em ordem alfabética:");
            foreach (var paciente in pacientesOrdenados)
            {
                paciente.Apresentar();
            }
        }

        public void GerarRelatorioPacientesPorSintomas(string textoSintomas)
        {
            var pacientesFiltrados = pacientes.Where(paciente => paciente.Sintomas.Contains(textoSintomas));

            Console.WriteLine($"\nRelatório de Pacientes com sintomas contendo '{textoSintomas}':");
            foreach (var paciente in pacientesFiltrados)
            {
                paciente.Apresentar();
            }
        }

        public void GerarRelatorioAniversariantesDoMes(int mes)
        {
            var aniversariantes = medicos.Concat<Pessoa>(pacientes)
                .Where(p => p.DataNascimento.Month == mes)
                .OrderBy(p => p.DataNascimento.Day);

            Console.WriteLine($"\nRelatório de Aniversariantes do mês {mes}:");
            foreach (var pessoa in aniversariantes)
            {
                pessoa.Apresentar();
            }
        }

        public void RelatorioAtendimentosEmAberto()
        {
            var atendimentosAbertos = atendimentos
                .Where(atendimento => atendimento.Fim == DateTime.MinValue)
                .OrderByDescending(atendimento => atendimento.Inicio);

            Console.WriteLine("\nRelatório de Atendimentos em Aberto:");
            foreach (var atendimento in atendimentosAbertos)
            {
                Console.WriteLine($"Paciente: {atendimento.Paciente.Nome}, Médico: {atendimento.MedicoResponsavel.Nome}, Início: {atendimento.Inicio}");
            }
        }

        public void RelatorioMedicosQuantidadeAtendimentos()
        {
            var medicoAtendimentos = medicos
                .Select(medico => new
                {
                    Medico = medico,
                    QuantidadeAtendimentos = atendimentos.Count(atendimento => atendimento.MedicoResponsavel == medico && atendimento.Fim != DateTime.MinValue)
                })
                .OrderByDescending(entry => entry.QuantidadeAtendimentos);

            Console.WriteLine("\nRelatório de Médicos em Ordem Decrescente da Quantidade de Atendimentos Concluídos:");
            foreach (var entry in medicoAtendimentos)
            {
                Console.WriteLine($"Médico: {entry.Medico.Nome}, Quantidade de Atendimentos: {entry.QuantidadeAtendimentos}");
            }
        }

        public void RelatorioAtendimentosPalavraChave(string palavraChave)
        {
            var atendimentosComPalavraChave = atendimentos
                .Where(atendimento => atendimento.SuspeitaInicial.Contains(palavraChave) || atendimento.DiagnosticoFinal.Contains(palavraChave))
                .OrderBy(atendimento => atendimento.Inicio);

            Console.WriteLine($"\nRelatório de Atendimentos com a Palavra-chave '{palavraChave}':");
            foreach (var atendimento in atendimentosComPalavraChave)
            {
                Console.WriteLine($"Paciente: {atendimento.Paciente.Nome}, Médico: {atendimento.MedicoResponsavel.Nome}, Início: {atendimento.Inicio}");
            }
        }

        public void RelatorioTop10ExamesUtilizados()
        {
            var examesUtilizados = atendimentos
                .SelectMany(atendimento => atendimento.ListaExamesResultado)
                .GroupBy(entry => entry.Item1)
                .Select(group => new
                {
                    Exame = group.Key,
                    QuantidadeUtilizada = group.Count()
                })
                .OrderByDescending(entry => entry.QuantidadeUtilizada)
                .Take(10);

            Console.WriteLine("\nRelatório dos 10 Exames Mais Utilizados nos Atendimentos:");
            foreach (var entry in examesUtilizados)
            {
                Console.WriteLine($"Exame: {entry.Exame.Titulo}, Quantidade Utilizada: {entry.QuantidadeUtilizada}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                ClinicaTechMed clinica = new ClinicaTechMed();

                Medico medico1 = new Medico("Dr. Murilo", new DateTime(1992, 12, 3), "12345678901", "CRM12345");
                Medico medico2 = new Medico("Dr. Carlos", new DateTime(1995, 3, 11), "54398432109", "CRM65421");
                Medico medico3 = new Medico("Dra. Maria", new DateTime(1985, 11, 10), "98765432109", "CRM54321");
                Medico medico4 = new Medico("Dra. Carla", new DateTime(1991, 4, 7), "82514276598", "CRM73416");

                Paciente paciente1 = new Paciente("Goku", new DateTime(2001, 11, 1), "11223344556", Sexo.Masculino, new List<string> { "Enjoo" });
                Paciente paciente2 = new Paciente("Naruto", new DateTime(1988, 9, 16), "99352466554", Sexo.Masculino, new List<string> { "Dor de barriga" });

                Exame exame1 = new Exame { Titulo = "Hemograma", Valor = 80.0f, Descricao = "Exame de sangue", Local = "Laboratório ABC" };

                Atendimento atendimento1 = new Atendimento
                {
                    SuspeitaInicial = "Gastrite",
                    ListaExamesResultado = new List<(Exame, string)> { (exame1, "Normal") },
                    Valor = 150.0f,
                    MedicoResponsavel = medico1,
                    Paciente = paciente1
                };

                clinica.AdicionarMedico(medico1);
                clinica.AdicionarMedico(medico2);
                clinica.AdicionarMedico(medico3);
                clinica.AdicionarMedico(medico4);

                clinica.AdicionarPaciente(paciente1);
                clinica.AdicionarPaciente(paciente2);

                clinica.AdicionarExame(exame1);

                clinica.IniciarAtendimento(atendimento1);
                clinica.FinalizarAtendimento(atendimento1, "Gastrite");
                clinica.GerarRelatorioPacientesSexo(Sexo.Masculino);
                clinica.GerarRelatorioPacientesOrdemAlfabetica();
                clinica.GerarRelatorioPacientesPorSintomas("Dor");
                clinica.GerarRelatorioAniversariantesDoMes(12);
                clinica.RelatorioAtendimentosEmAberto();
                clinica.RelatorioMedicosQuantidadeAtendimentos();
                clinica.RelatorioAtendimentosPalavraChave("Gastrite");
                clinica.RelatorioTop10ExamesUtilizados();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}


