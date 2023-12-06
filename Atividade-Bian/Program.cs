using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Consultorio consultorio = new Consultorio();

        try
        {
            consultorio.InserirMedico(new Medico("Dr. João", new DateTime(1980, 1, 1), "12345678901", "CRM123"));
            consultorio.InserirPaciente(new Paciente("Maria", new DateTime(1990, 5, 10), "98765432109", "F", new List<string> { "Febre", "Dor de cabeça" }));
            consultorio.InserirPaciente(new Paciente("Carlos", new DateTime(1985, 3, 15), "87654321098", "M", new List<string> { "Tosse", "Dor de garganta" }));

            Atendimento atendimento = new Atendimento
            {
                Inicio = DateTime.Now,
                SuspeitaInicial = "Febre alta",
                MedicoResponsavel = consultorio.Medicos.First(),
                Paciente = consultorio.Pacientes.First()
            };
            consultorio.IniciarAtendimento(atendimento);

            atendimento.Fim = DateTime.Now.AddHours(1);
            atendimento.DiagnosticoFinal = "Gripe";
            atendimento.ExamesResultados = new List<(Exame, string)>
            {
                (new Exame { Titulo = "Raio-X", Valor = 50.0f, Descricao = "Tórax", Local = "Clínica X" }, "Normal")
            };
            consultorio.FinalizarAtendimento(atendimento);

            Console.WriteLine("1. Médicos com idade entre 35 e 45 anos:");
            var relatorioMedicosIdade = consultorio.RelatorioMedicosComIdadeEntre(35, 45);
            ImprimirPessoas(relatorioMedicosIdade);

            Console.WriteLine("\n2. Pacientes com idade entre 25 e 35 anos:");
            var relatorioPacientesIdade = consultorio.RelatorioPacientesComIdadeEntre(25, 35);
            ImprimirPessoas(relatorioPacientesIdade);

            Console.WriteLine("\n3. Pacientes do sexo masculino:");
            var relatorioPacientesSexo = consultorio.RelatorioPacientesDoSexo("M");
            ImprimirPessoas(relatorioPacientesSexo);

            Console.WriteLine("\n4. Pacientes em ordem alfabética:");
            var relatorioPacientesAlfabeticos = consultorio.RelatorioPacientesEmOrdemAlfabetica();
            ImprimirPessoas(relatorioPacientesAlfabeticos);

            Console.WriteLine("\n5. Pacientes com sintomas contendo 'Dor':");
            var relatorioPacientesSintomas = consultorio.RelatorioPacientesComSintoma("Dor");
            ImprimirPessoas(relatorioPacientesSintomas);

            Console.WriteLine("\n6. Médicos e Pacientes aniversariantes do mês atual:");
            var relatorioAniversariantes = consultorio.RelatorioAniversariantesDoMes(DateTime.Now.Month);
            ImprimirPessoas(relatorioAniversariantes);

            Console.WriteLine("\n7. Atendimentos em aberto (sem finalizar) em ordem decrescente pela data de início:");
            var relatorioAtendimentosAbertos = consultorio.RelatorioAtendimentosEmAberto();
            ImprimirAtendimentos(relatorioAtendimentosAbertos);

            Console.WriteLine("\n8. Médicos em ordem decrescente da quantidade de atendimentos concluídos:");
            var relatorioMedicosPorAtendimentosConcluidos = consultorio.RelatorioMedicosPorAtendimentosConcluidos();
            ImprimirPessoas(relatorioMedicosPorAtendimentosConcluidos);

            Console.WriteLine("\n9. Atendimentos cuja suspeita ou diagnóstico final contenham 'Gripe':");
            var relatorioAtendimentosPalavraChave = consultorio.RelatorioAtendimentosPorPalavraChave("Gripe");
            ImprimirAtendimentos(relatorioAtendimentosPalavraChave);

            Console.WriteLine("\n10. Os 10 exames mais utilizados nos atendimentos:");
            var relatorioTop10Exames = consultorio.RelatorioTop10ExamesMaisUtilizados();
            ImprimirExames(relatorioTop10Exames);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void ImprimirPessoas(List<Pessoa> pessoas)
    {
        foreach (var pessoa in pessoas)
        {
            Console.WriteLine($"{pessoa.Nome}, Idade: {pessoa.Idade}");
        }
    }

    static void ImprimirAtendimentos(List<Atendimento> atendimentos)
    {
        foreach (var atendimento in atendimentos)
        {
            Console.WriteLine($"Início: {atendimento.Inicio}, Médico: {atendimento.MedicoResponsavel.Nome}, Paciente: {atendimento.Paciente.Nome}");
        }
    }

    static void ImprimirExames(List<Exame> exames)
    {
        foreach (var exame in exames)
        {
            Console.WriteLine($"Título: {exame.Titulo}, Valor: {exame.Valor}, Descrição: {exame.Descricao}, Local: {exame.Local}");
        }
    }
}
