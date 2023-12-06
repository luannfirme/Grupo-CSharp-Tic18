using System;
using System.Collections.Generic;
using System.Linq;

public class Consultorio
{
    public List<Pessoa> Pessoas { get; private set; } = new List<Pessoa>();
    public List<Medico> Medicos => Pessoas.OfType<Medico>().ToList();
    public List<Paciente> Pacientes => Pessoas.OfType<Paciente>().ToList();
    public List<Atendimento> Atendimentos { get; private set; } = new List<Atendimento>();

    public void InserirPessoa(Pessoa pessoa)
    {
        if (Pessoas.Any(p => p.DataNascimento == pessoa.DataNascimento && p.Nome == pessoa.Nome))
        {
            throw new Exception("Pessoa com nome e data de nascimento duplicados.");
        }

        Pessoas.Add(pessoa);
    }

    public void InserirMedico(Medico medico)
    {
        InserirPessoa(medico);
    }

    public void InserirPaciente(Paciente paciente)
    {
        InserirPessoa(paciente);
    }

    public void IniciarAtendimento(Atendimento atendimento)
    {
        if (Atendimentos.Any(a => a.MedicoResponsavel == atendimento.MedicoResponsavel && a.Paciente == atendimento.Paciente && a.Fim == default(DateTime)))
        {
            throw new Exception("Este médico já está atendendo este paciente.");
        }

        Atendimentos.Add(atendimento);
    }

    public void FinalizarAtendimento(Atendimento atendimento)
    {
        Atendimento atendimentoEmAndamento = Atendimentos.FirstOrDefault(a => a == atendimento && a.Fim == default(DateTime));

        if (atendimentoEmAndamento == null)
        {
            throw new Exception("O atendimento não foi iniciado ou já foi finalizado.");
        }

        atendimentoEmAndamento.Fim = atendimento.Fim;
        atendimentoEmAndamento.DiagnosticoFinal = atendimento.DiagnosticoFinal;
        atendimentoEmAndamento.ExamesResultados = atendimento.ExamesResultados;
    }

    public List<Pessoa> RelatorioAniversariantesDoMes(int mes)
    {
        return Pessoas.Where(p => p.DataNascimento.Month == mes)
                      .OrderBy(p => p.DataNascimento.Day)
                      .ToList();
    }

    public List<Atendimento> RelatorioAtendimentosEmAberto()
    {
        return Atendimentos.Where(a => a.Fim == default(DateTime))
                          .OrderByDescending(a => a.Inicio)
                          .ToList();
    }

    public List<Medico> RelatorioMedicosPorAtendimentosConcluidos()
    {
        return Medicos.OrderByDescending(m => Atendimentos.Count(a => a.MedicoResponsavel == m && a.Fim != default(DateTime)))
                      .ToList();
    }

    public List<Atendimento> RelatorioAtendimentosPorPalavraChave(string palavraChave)
    {
        return Atendimentos.Where(a => a.SuspeitaInicial.Contains(palavraChave, StringComparison.OrdinalIgnoreCase) ||
                                        a.DiagnosticoFinal.Contains(palavraChave, StringComparison.OrdinalIgnoreCase))
                          .ToList();
    }

    public List<Exame> RelatorioTop10ExamesMaisUtilizados()
    {
        var examesUtilizados = Atendimentos.SelectMany(a => a.ExamesResultados.Select(er => er.Item1));
        var top10Exames = examesUtilizados.GroupBy(e => e.Titulo)
                                         .OrderByDescending(g => g.Count())
                                         .Take(10)
                                         .Select(g => g.First())
                                         .ToList();
        return top10Exames;
    }
}
