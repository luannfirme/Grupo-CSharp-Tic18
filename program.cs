using System;
using System.Collections.Generic;
using System.Linq;

namespace POGDevConsultorio
{
    public class Pessoa
    {
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string CPF { get; private set; }

        public Pessoa(string nome, DateTime dataNascimento, string cpf)
        {
            if (cpf.Length != 11)
            {
                throw new ArgumentException("O CPF deve ter 11 dígitos.");
            }

            Nome = nome;
            DataNascimento = dataNascimento;
            CPF = cpf;
        }

        public virtual void Apresentar()
        {
            Console.WriteLine($"Nome: {Nome}, Data de Nascimento: {DataNascimento.ToShortDateString()}, CPF: {CPF}");
        }
    }

    public enum Sexo
    {
        Masculino,
        Feminino
    }

    public class Paciente : Pessoa
    {
        public Sexo Sexo { get; private set; }
        public string Sintomas { get; private set; }

        public Paciente(string nome, DateTime dataNascimento, string cpf, Sexo sexo, string sintomas)
            : base(nome, dataNascimento, cpf)
        {
            Sexo = sexo;
            Sintomas = sintomas;
        }

        public override void Apresentar()
        {
            Console.WriteLine($"Paciente - Sexo: {Sexo}, {base.Nome}, {base.DataNascimento.ToShortDateString()}, CPF: {base.CPF}, Sintomas: {Sintomas}");
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
        public List<(Exame, string)> ListaExamesResultados { get; set; }
        public float Valor { get; set; }
        public DateTime Fim { get; set; }
        public Medico MedicoResponsavel { get; set; }
        public Paciente Paciente { get; set; }
        public string DiagnosticoFinal { get; set; }
    }

    public class Consultorio
    {
        private List<Medico> medicos;
        private List<Paciente> pacientes;
        private List<Exame> exames;
        private List<Atendimento> atendimentos;

        public Consultorio()
        {
            medicos = new List<Medico>();
            pacientes = new List<Paciente>();
            exames = new List<Exame>();
            atendimentos = new List<Atendimento>();
        }

        public void AdicionarMedico(Medico medico)
        {
            if (medicos.Any(m => m.CRM == medico.CRM))
            {
                throw new ArgumentException($"Médico com CRM {medico.CRM} já cadastrado.");
            }

            medicos.Add(medico);
        }

        public void AdicionarPaciente(Paciente paciente)
        {
            if (pacientes.Any(p => p.CPF == paciente.CPF))
            {
                throw new ArgumentException($"Paciente com CPF {paciente.CPF} já cadastrado.");
            }

            pacientes.Add(paciente);
        }

        public void AdicionarExame(Exame exame)
        {
            exames.Add(exame);
        }

        public void RemoverExame(Exame exame)
        {
            exames.Remove(exame);
        }

        public void AdicionarAtendimento(Atendimento atendimento)
        {
            atendimentos.Add(atendimento);
        }

        public void RemoverAtendimento(Atendimento atendimento)
        {
            atendimentos.Remove(atendimento);
        }

        public void IniciarAtendimento(Medico medico, Paciente paciente, DateTime inicio, string suspeitaInicial)
        {
            if (!medicos.Contains(medico) || !pacientes.Contains(paciente))
            {
                throw new ArgumentException("Médico ou Paciente não cadastrado no consultório.");
            }

            if (atendimentos.Any(a => a.MedicoResponsavel == medico && a.Paciente == paciente))
            {
                throw new InvalidOperationException("Esse médico já está atendendo esse paciente.");
            }

            if (inicio >= DateTime.Now)
            {
                throw new InvalidOperationException("A data de início do atendimento deve ser anterior à data atual.");
            }

            Atendimento atendimento = new Atendimento
            {
                Inicio = inicio,
                SuspeitaInicial = suspeitaInicial,
                MedicoResponsavel = medico,
                Paciente = paciente
            };

            atendimentos.Add(atendimento);
        }

        public void FinalizarAtendimento(Atendimento atendimento, DateTime fim, string diagnosticoFinal)
        {
            if (!atendimentos.Contains(atendimento))
            {
                throw new ArgumentException("Atendimento não encontrado.");
            }

            if (fim <= atendimento.Inicio)
            {
                throw new InvalidOperationException("A data de fim do atendimento deve ser posterior à data de início.");
            }

            atendimento.Fim = fim;
            atendimento.DiagnosticoFinal = diagnosticoFinal;
        }

        public void GerarRelatorioPessoas()
        {
            Console.WriteLine("Relatório de Médicos:");
            medicos.ForEach(m => m.Apresentar());

            Console.WriteLine("\nRelatório de Pacientes:");
            pacientes.ForEach(p => p.Apresentar());
        }

        public void GerarRelatorioExames()
        {
            Console.WriteLine("Relatório de Exames:");
            exames.ForEach(e => Console.WriteLine($"Título: {e.Titulo}, Valor: {e.Valor}, Descrição: {e.Descricao}, Local: {e.Local}"));
        }

        public void GerarRelatorioAtendimentos()
        {
            Console.WriteLine("Relatório de Atendimentos:");
            atendimentos.ForEach(a =>
            {
                Console.WriteLine($"Início: {a.Inicio}, Suspeita Inicial: {a.SuspeitaInicial}, Valor: {a.Valor}, " + $"Fim: {a.Fim}, Diagnóstico Final: {a.DiagnosticoFinal}");
                Console.WriteLine("Lista de Exames/Resultado:");
                foreach (var tuple in a.ListaExamesResultados)
                {
                    Console.WriteLine($"Exame: {tuple.Item1.Titulo}, Resultado: {tuple.Item2}");
                }
                Console.WriteLine($"Médico: {a.MedicoResponsavel.Nome}, Paciente: {a.Paciente.Nome}");
                Console.WriteLine("----------------------------");
            });
        }

        public IEnumerable<Medico> MedicosComIdadeEntre(int idadeMinima, int idadeMaxima)
        {
            return medicos.Where(m => CalculaIdade(m.DataNascimento) >= idadeMinima && CalculaIdade(m.DataNascimento) <= idadeMaxima);
        }

        public IEnumerable<Paciente> PacientesComIdadeEntre(int idadeMinima, int idadeMaxima)
        {
            return pacientes.Where(p => CalculaIdade(p.DataNascimento) >= idadeMinima && CalculaIdade(p.DataNascimento) <= idadeMaxima);
        }

        private int CalculaIdade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.Month < dataNascimento.Month || (DateTime.Now.Month == dataNascimento.Month && DateTime.Now.Day < dataNascimento.Day))
            {
                idade--;
            }
            return idade;
        }
        public IEnumerable<Medico> MedicosComIdadeEntre(int idadeMinima, int idadeMaxima)
{
    return medicos.Where(m => CalculaIdade(m.DataNascimento) >= idadeMinima && CalculaIdade(m.DataNascimento) <= idadeMaxima);
}

public IEnumerable<Paciente> PacientesComIdadeEntre(int idadeMinima, int idadeMaxima)
{
    return pacientes.Where(p => CalculaIdade(p.DataNascimento) >= idadeMinima && CalculaIdade(p.DataNascimento) <= idadeMaxima);
}

public IEnumerable<Paciente> PacientesPorSexo(Sexo sexo)
{
    return pacientes.Where(p => p.Sexo == sexo);
}

public IEnumerable<Paciente> PacientesEmOrdemAlfabetica()
{
    return pacientes.OrderBy(p => p.Nome);
}

public IEnumerable<Paciente> PacientesComSintoma(string textoSintoma)
{
    return pacientes.Where(p => p.Sintomas.Contains(textoSintoma));
}

public IEnumerable<Pessoa> AniversariantesDoMes(int mes)
{
    return medicos.Concat<Pessoa>(pacientes).Where(p => p.DataNascimento.Month == mes).OrderBy(p => p.DataNascimento.Day);
}

public IEnumerable<Atendimento> AtendimentosEmAberto()
{
    return atendimentos.Where(a => a.Fim == default(DateTime)).OrderByDescending(a => a.Inicio);
}

public IEnumerable<Medico> MedicosPorQuantidadeDeAtendimentosConcluidos()
{
    return medicos.OrderByDescending(m => atendimentos.Count(a => a.MedicoResponsavel == m && a.Fim != default(DateTime)));
}

public IEnumerable<Atendimento> AtendimentosComPalavraChave(string palavraChave)
{
    return atendimentos.Where(a => a.SuspeitaInicial.Contains(palavraChave) || a.DiagnosticoFinal.Contains(palavraChave));
}

public IEnumerable<Exame> OsDezExamesMaisUtilizados()
{
    var examesUtilizados = atendimentos.SelectMany(a => a.ListaExamesResultados.Select(e => e.Item1));
    var agrupadosPorExame = examesUtilizados.GroupBy(e => e)
                                            .Select(g => new { Exame = g.Key, Quantidade = g.Count() })
                                            .OrderByDescending(g => g.Quantidade)
                                            .Take(10)
                                            .Select(g => g.Exame);

    return agrupadosPorExame;
}
    }

    class Program
    {
        static void Main()
        {
            try
            {
                Consultorio consultorio = new Consultorio();

                Medico medico1 = new Medico("Dra. Erika", new DateTime(2000, 7, 7), "08527907510", "CRM54321");
                Medico medico2 = new Medico("Dr. Fernando", new DateTime(1999, 8, 10), "74927230597", "CRM76541");
                Medico medico3 = new Medico("Dra. Julia", new DateTime(1977, 5, 13), "98765436821", "CRM89123");
                Medico medico4 = new Medico("Dr. Luis", new DateTime(1978, 7, 4), "12514368791", "CRM78965");

                Paciente paciente1 = new Paciente("Lara", new DateTime(2010, 7, 8), "25725678321", Sexo.Feminino, "Cólica");
                Paciente paciente2 = new Paciente("Carlos", new DateTime(2000, 11, 15), "98764578976", Sexo.Masculino, "Dor de Cabeça");
                Paciente paciente3 = new Paciente("Raissa", new DateTime(2000, 3, 16), "08578936572", Sexo.Feminino, "Dor de Barriga");
                Paciente paciente4 = new Paciente("Lucas", new DateTime(1978, 8, 24), "22346789642", Sexo.Feminino, "Febre");

                Exame exame1 = new Exame { Titulo = "Exame de Sangue", Valor = 150.0f, Descricao = "Hemograma completo", Local = "Laboratório A" };
                Exame exame2 = new Exame { Titulo = "Ressonância Magnética", Valor = 800.0f, Descricao = "Cérebro", Local = "Clínica de Imagens B" };

                    Console.WriteLine("1. Médicos com idade entre 30 e 40 anos:");
    consultorio.MedicosComIdadeEntre(30, 40).ToList().ForEach(m => m.Apresentar());

    Console.WriteLine("\n2. Pacientes com idade entre 20 e 30 anos:");
    consultorio.PacientesComIdadeEntre(20, 30).ToList().ForEach(p => p.Apresentar());

    Console.WriteLine("\n3. Pacientes do sexo feminino:");
    consultorio.PacientesPorSexo(Sexo.Feminino).ToList().ForEach(p => p.Apresentar());

    Console.WriteLine("\n4. Pacientes em ordem alfabética:");
    consultorio.PacientesEmOrdemAlfabetica().ToList().ForEach(p => p.Apresentar());

    Console.WriteLine("\n5. Pacientes cujos sintomas contenham 'Dor':");
    consultorio.PacientesComSintoma("Dor").ToList().ForEach(p => p.Apresentar());

    Console.WriteLine("\n6. Aniversariantes do mês de janeiro:");
    consultorio.AniversariantesDoMes(1).ToList().ForEach(p => p.Apresentar());

    Console.WriteLine("\n7. Atendimentos em aberto:");
    consultorio.AtendimentosEmAberto().ToList().ForEach(a => Console.WriteLine($"Início: {a.Inicio}, Suspeita Inicial: {a.SuspeitaInicial}"));

    Console.WriteLine("\n8. Médicos em ordem decrescente da quantidade de atendimentos concluídos:");
    consultorio.MedicosPorQuantidadeDeAtendimentosConcluidos().ToList().ForEach(m => m.Apresentar());

    Console.WriteLine("\n9. Atendimentos com 'febre' no diagnóstico ou suspeita:");
    consultorio.AtendimentosComPalavraChave("febre").ToList().ForEach(a => Console.WriteLine($"Diagnóstico/Suspeita: {a.DiagnosticoFinal ?? a.SuspeitaInicial}"));

    Console.WriteLine("\n10. Os 10 exames mais utilizados nos atendimentos:");
    consultorio.OsDezExamesMaisUtilizados().ToList().ForEach(e => Console.WriteLine($"Exame: {e.Titulo}, Quantidade de Utilizações: {atendimentos.Count(a => a.ListaExamesResultados.Any(er => er.Item1 == e))}"));
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

                Atendimento atendimento1 = new Atendimento
                {
                    Inicio = new DateTime(2023, 1, 15, 10, 0, 0),
                    SuspeitaInicial = "Dor de cabeça persistente",
                    ListaExamesResultados = new List<(Exame, string)> { (exame1, "Normal") },
                    Valor = 200.0f,
                    Fim = new DateTime(2023, 1, 15, 12, 0, 0),
                    MedicoResponsavel = medico1,
                    Paciente = paciente2,
                    DiagnosticoFinal = "Enxaqueca crônica"
                };

                Atendimento atendimento2 = new Atendimento
                {
                    Inicio = new DateTime(2023, 2, 5, 9, 0, 0),
                    SuspeitaInicial = "Febre alta",
                    ListaExamesResultados = new List<(Exame, string)> { (exame2, "Anormal") },
                    Valor = 500.0f,
                    Fim = new DateTime(2023, 2, 5, 11, 0, 0),
                    MedicoResponsavel = medico4,
                    Paciente = paciente4,
                    DiagnosticoFinal = "Infecção bacteriana"
                };

                consultorio.AdicionarMedico(medico1);
                consultorio.AdicionarMedico(medico2);
                consultorio.AdicionarMedico(medico3);
                consultorio.AdicionarMedico(medico4);

                consultorio.AdicionarPaciente(paciente1);
                consultorio.AdicionarPaciente(paciente2);
                consultorio.AdicionarPaciente(paciente3);
                consultorio.AdicionarPaciente(paciente4);

                consultorio.AdicionarExame(exame1);
                consultorio.AdicionarExame(exame2);

                consultorio.AdicionarAtendimento(atendimento1);
                consultorio.AdicionarAtendimento(atendimento2);

                consultorio.IniciarAtendimento(medico1, paciente3, new DateTime(2023, 3, 10, 8, 0, 0), "Dor de Estômago");
                consultorio.IniciarAtendimento(medico4, paciente4, new DateTime(2023, 3, 12, 9, 0, 0), "Dor nas Costas");

                consultorio.FinalizarAtendimento(atendimento1, new DateTime(2023, 3, 10, 12, 0, 0), "Gastrite crônica");
                consultorio.FinalizarAtendimento(atendimento2, new DateTime(2023, 3, 12, 11, 0, 0), "Hérnia de disco");

                Console.WriteLine("Médicos com idade entre 30 e 40 anos:");
                consultorio.MedicosComIdadeEntre(30, 40).ToList().ForEach(m => m.Apresentar());

                Console.WriteLine("\nPacientes com idade entre 20 e 30 anos:");
                consultorio.PacientesComIdadeEntre(20, 30).ToList().ForEach(p => p.Apresentar());

                consultorio.GerarRelatorioPessoas();
                consultorio.GerarRelatorioExames();
                consultorio.GerarRelatorioAtendimentos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
