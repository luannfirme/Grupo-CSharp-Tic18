using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaMedico
{
    public class Pessoa
    {
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? NumeroIdentificacao { get; set; }
    }

    public class Medico : Pessoa
    {
        public string? NumeroRegistro { get; set; }
    }

    public class Paciente : Pessoa
    {
        public string? Genero { get; set; }
        public string? Sintomas { get; set; }
    }

    public class Validacoes
    {
        public static bool ValidarNumeroIdentificacao(string numeroId)
        {
            if (numeroId.Length != 11 || !numeroId.All(char.IsDigit))
                return false;

            int[] multiplicadoresPrimeiroDigito = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadoresSegundoDigito = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string numeroIdSemDigitos = numeroId.Substring(0, 9);

            int CalcularDigito(string numeroIdParcial, int[] multiplicadores)
            {
                int soma = numeroIdParcial.Zip(multiplicadores, (n, m) => int.Parse(n.ToString()) * m).Sum();
                int resto = soma % 11;
                return resto < 2 ? 0 : 11 - resto;
            }

            int primeiroDigito = CalcularDigito(numeroIdSemDigitos, multiplicadoresPrimeiroDigito);
            numeroIdSemDigitos += primeiroDigito;
            int segundoDigito = CalcularDigito(numeroIdSemDigitos, multiplicadoresSegundoDigito);

            return numeroId.EndsWith($"{primeiroDigito}{segundoDigito}");
        }
    }

    public class Exame
    {
        public string? Titulo { get; set; }
        public float Valor { get; set; }
        public string? Descricao { get; set; }
        public string? Local { get; set; }
    }

    public class Atendimento
    {
        public int Id { get; set; }
        public DateTime Inicio { get; set; }
        public string? SuspeitaInicial { get; set; }
        public List<(Exame, string)> ListaExamesResultado { get; set; } = new List<(Exame, string)>();
        public float Valor { get; set; }
        public DateTime Fim { get; set; }
        public Medico? MedicoResponsavel { get; set; }
        public Paciente? Paciente { get; set; }
        public string? DiagnosticoFinal { get; set; }

        public void IniciarAtendimento(string suspeitaInicial, DateTime inicio, Medico medico, Paciente paciente)
        {
            SuspeitaInicial = suspeitaInicial;
            Inicio = inicio;
            MedicoResponsavel = medico;
            Paciente = paciente;
        }

        public void FinalizarAtendimento(string diagnosticoFinal, DateTime fim)
        {
            DiagnosticoFinal = diagnosticoFinal;
            Fim = fim;
        }
    }

    class Programa
    {
        static void Principal(string[] args)
        {
            var medicos = new List<Medico>();
            var pacientes = new List<Paciente>();
            var atendimentos = new List<Atendimento>();

            bool sair = false;

            while (!sair)
            {
                MostrarMenu();
                if (int.TryParse(Console.ReadLine(), out int opcao))
                    ExecutarOpcao(opcao, medicos, pacientes, atendimentos, ref sair);
                else
                    Console.WriteLine("Opção inválida. Tente novamente.");

                Console.WriteLine();
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("======== Menu Principal =========|");
            Console.WriteLine("|     1. Cadastrar Médico        |");
            Console.WriteLine("|     2. Cadastrar Paciente      |");
            Console.WriteLine("|     3. Iniciar Atendimento     |");
            Console.WriteLine("|     4. Finalizar Atendimento   |");
            Console.WriteLine("|     5. Relatórios              |");
            Console.WriteLine("|     6. Sair                    |");
            Console.WriteLine("|================================|");
            Console.Write("Escolha uma opção: ");
        }

        static void ExecutarOpcao(int opcao, List<Medico> medicos, List<Paciente> pacientes, List<Atendimento> atendimentos, ref bool sair)
        {
            switch (opcao)
            {
                case 1:
                    CadastrarMedico(medicos);
                    break;
                case 2:
                    CadastrarPaciente(pacientes);
                    break;
                case 3:
                    IniciarAtendimento(atendimentos, medicos, pacientes);
                    break;
                case 4:
                    FinalizarAtendimento(atendimentos);
                    break;
                case 5:
                    MenuRelatorios(medicos, pacientes, atendimentos);
                    break;
                case 6:
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }

        static void CadastrarMedico(List<Medico> medicos)
        {
            var novoMedico = new Medico();
            Console.WriteLine("===== Cadastro de Médico =====");
            PreencherDadosPessoa(novoMedico);

            string? numeroRegistro = ObterInputValidado("Número de Registro: ", Validacoes.ValidarNumeroIdentificacao);
            if (!string.IsNullOrEmpty(numeroRegistro))
                novoMedico.NumeroRegistro = numeroRegistro;

            novoMedico.NumeroRegistro = ObterInput("CRM: ");
            medicos.Add(novoMedico);
            Console.WriteLine("Médico cadastrado com sucesso!");
        }

        static void CadastrarPaciente(List<Paciente> pacientes)
        {
            var novoPaciente = new Paciente();
            Console.WriteLine("===== Cadastro de Paciente =====");
            PreencherDadosPessoa(novoPaciente);

            string? numeroIdentificacao = ObterInputValidado("Número de Identificação: ", Validacoes.ValidarNumeroIdentificacao);
            if (!string.IsNullOrEmpty(numeroIdentificacao))
                novoPaciente.NumeroIdentificacao = numeroIdentificacao;

            novoPaciente.Genero = ObterInput("Gênero: ");
            novoPaciente.Sintomas = ObterInput("Sintomas: ");
            pacientes.Add(novoPaciente);
            Console.WriteLine("Paciente cadastrado com sucesso!");
        }

        static void PreencherDadosPessoa(Pessoa pessoa)
        {
            pessoa.Nome = ObterInput("Nome: ");
            if (DateTime.TryParse(ObterInput("Data de Nascimento (dd/mm/aaaa): "), out DateTime dataNascimento))
                pessoa.DataNascimento = dataNascimento;
            else
                Console.WriteLine("Data de nascimento inválida. O cadastro será realizado sem a data de nascimento.");
        }

        static string? ObterInput(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine();
        }

        static string? ObterInputValidado(string mensagem, Func<string, bool> validacao)
        {
            string? input;
            do
            {
                input = ObterInput(mensagem);
                if (input != null && !validacao(input))
                    Console.WriteLine("Valor inválido. Tente novamente.");
            } while (input != null && !validacao(input));

            return input;
        }

        static void IniciarAtendimento(List<Atendimento> atendimentos, List<Medico> medicos, List<Paciente> pacientes)
        {
            Console.WriteLine("===== Iniciar Atendimento =====");
            var novoAtendimento = new Atendimento();

            novoAtendimento.MedicoResponsavel = SelecionarItemPorNumeroRegistro("Médico", medicos);
            novoAtendimento.Paciente = SelecionarItemPorNumeroIdentificacao("Paciente", pacientes);

            novoAtendimento.SuspeitaInicial = ObterInput("Suspeita Inicial: ");
            novoAtendimento.Inicio = DateTime.Now;

            atendimentos.Add(novoAtendimento);
            Console.WriteLine("Atendimento iniciado com sucesso!");
        }

        static void FinalizarAtendimento(List<Atendimento> atendimentos)
        {
            Console.WriteLine("===== Finalizar Atendimento =====");
            if (int.TryParse(ObterInput("Informe o ID do Atendimento a ser finalizado: "), out int idAtendimento))
            {
                Atendimento atendimento = atendimentos.FirstOrDefault(a => a.Id == idAtendimento);

                if (atendimento != null)
                {
                    atendimento.DiagnosticoFinal = ObterInput("Diagnóstico Final: ");
                    atendimento.Fim = DateTime.Now;

                    Console.WriteLine("Atendimento finalizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Atendimento não encontrado. Certifique-se de que o ID está correto.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Tente novamente.");
            }
        }

        static void MenuRelatorios(List<Medico> medicos, List<Paciente> pacientes, List<Atendimento> atendimentos)
        {
            Console.WriteLine("===== Relatórios =====");
            Console.WriteLine($"Total de Médicos: {medicos.Count}");
            Console.WriteLine($"Total de Pacientes: {pacientes.Count}");
            Console.WriteLine($"Total de Atendimentos: {atendimentos.Count}");

            MostrarLista("Médicos", medicos, m => $"{m.Nome}, CRM: {m.NumeroRegistro}");
            MostrarLista("Pacientes", pacientes, p => $"{p.Nome}, Número de Identificação: {p.NumeroIdentificacao}");
            MostrarLista("Atendimentos", atendimentos, a => $"ID: {a.Id}, Início: {a.Inicio}, Fim: {a.Fim}");
        }

        static T? SelecionarItemPorNumeroRegistro<T>(string tipo, List<T> lista) where T : Medico
        {
            T? itemSelecionado = null;
            do
            {
                string numeroRegistro = ObterInput($"{tipo} - Número de Registro: ") ?? string.Empty;
                itemSelecionado = lista.FirstOrDefault(item => (item as Medico)?.NumeroRegistro == numeroRegistro);

                if (itemSelecionado == null)
                    Console.WriteLine($"{tipo} não encontrado. Certifique-se de que o número de registro está correto.");
            } while (itemSelecionado == null);

            return itemSelecionado;
        }

        static T? SelecionarItemPorNumeroIdentificacao<T>(string tipo, List<T> lista) where T : Paciente
        {
            T? itemSelecionado = null;
            do
            {
                string numeroIdentificacao = ObterInput($"{tipo} - Número de Identificação: ") ?? string.Empty;
                itemSelecionado = lista.FirstOrDefault(item => (item as Paciente)?.NumeroIdentificacao == numeroIdentificacao);

                if (itemSelecionado == null)
                    Console.WriteLine($"{tipo} não encontrado. Certifique-se de que o número de identificação está correto.");
            } while (itemSelecionado == null);

            return itemSelecionado;
        }

        static void MostrarLista<T>(string titulo, List<T> lista, Func<T, string> formatador)
        {
            Console.WriteLine($"\n===== Lista de {titulo} =====");
            foreach (var item in lista)
            {
                Console.WriteLine(formatador(item));
            }
        }
    }
}
