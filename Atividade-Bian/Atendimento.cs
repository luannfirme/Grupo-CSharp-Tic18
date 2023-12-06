using System;
using System.Collections.Generic;

public class Atendimento
{
    public DateTime Inicio { get; set; }
    public string SuspeitaInicial { get; set; }
    public List<(Exame, string)> ExamesResultados { get; set; }
    public float Valor { get; set; }
    public DateTime Fim { get; set; }
    public Medico MedicoResponsavel { get; set; }
    public Paciente Paciente { get; set; }
    public string DiagnosticoFinal { get; set; }
}
