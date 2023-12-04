using Prova_grupo.Data;
using Prova_grupo.Domain;
using Prova_grupo.Service;
using System.Text;

namespace Prova_grupo.Service
{
    public class AtendimentoService
    {
        AtendimentoRepositorio atendimentoRepositorio = new AtendimentoRepositorio();

        public string iniciarAtendimento( string suspeitaInicial, List<(Exame, string)> examesResultado, float valor, Medico medicoResponsavel, Paciente paciente){
            var bulder = new StringBuilder();
            bool verificaAtendimento = atendimentoRepositorio.VerificaAtendimentoMedicoPaciente(medicoResponsavel, paciente);

            if (!verificaAtendimento){
                return bulder.Append("Médico já atendeu essa paciente!").ToString();
            }else{
                var atendimentoid = atendimentoRepositorio.TamListAtendimento() + 1;
                atendimentoRepositorio.AddAtendimento(new Atendimento(atendimentoid, DateTime.Now, suspeitaInicial, examesResultado, valor, medicoResponsavel, paciente));
                bulder.Append("Atendimento adicionado com sucesso!").ToString(); 
            }

            return bulder.ToString();
        }



        public string fimAtendimento(DateTime fimData, string diagnostico, int id){
            var bulder = new StringBuilder();
            var fimAtend = atendimentoRepositorio.buscarPorId(id);
            var tamanhoLista = atendimentoRepositorio.TamListAtendimento();

            if(tamanhoLista == 0){
                return bulder.Append("Lista vazia!").ToString();
            }if(fimData <= fimAtend.Inicio){
                return bulder.Append("Data final só poderá ser posterior à data inicial!").ToString();
            }else{
                fimAtend.Fim = fimData;
                fimAtend.DiagnosticoFinal = diagnostico;
                bulder.Append("Atendimento finalizado com sucesso");
            }
            return bulder.ToString();
        }

        public string BuscaMedicosPorAtendimentosConcluidos(){
            var bulder = new StringBuilder();
            var buscaPorAtendimentoConclui = atendimentoRepositorio.ListarMedicosPorAtendConcl();
            var tamanhoLista = atendimentoRepositorio.TamListAtendimento();

            if(tamanhoLista == 0){
                return bulder.Append("Lista vazia!").ToString();
            }else{
                bulder.AppendLine($"--Medicos--: ");
                foreach(Medico medico in buscaPorAtendimentoConclui){
                    bulder.AppendLine($"{medico.Id} -> CPF: {medico.CPF}, \nNome: {medico.Nome}, \nDataNasc: {medico.DataNascimento}, \nCRM: {medico.CRM}");
                }
                return bulder.ToString();
            }
        }


        public string BuscaPorSuspeitaDiagnosticoFinal(string suspeitaDiagnostico){
            var bulder = new StringBuilder();
            var buscaPorSuspeitaDiag = atendimentoRepositorio.BuscarAtendiSuspeitaDiagnostico(suspeitaDiagnostico);
            var tamanhoLista = atendimentoRepositorio.TamListAtendimento();

            if(tamanhoLista == 0){
                return bulder.Append("Lista vazia!").ToString();
            }else{
                 bulder.AppendLine($"--Medicos--: ");
                foreach(Atendimento atendimento in buscaPorSuspeitaDiag){
                    bulder.AppendLine($"--Paciente--: \nInicio: {atendimento.Inicio}, \nSuspeita Inicial: {atendimento.SuspeitaInicial}, \nValor: {atendimento.Valor}, \nFim: {atendimento.Fim}, \nDiagnostico: {atendimento.DiagnosticoFinal}, \n");
                    foreach ((Exame exame, string resultado) in atendimento.ListaExamesResultados){
                        bulder.AppendLine($"- Exame - \nTítulo: {exame.Titulo}, \nValor: {exame.Valor}, \nDescrição: {exame.Descricao}, \nLocal: {exame.Local}, \nResultado: {resultado}");
                    }
                    bulder.AppendLine($"\nMédico: {atendimento.MedicoResponsavel.Nome} - CRM: {atendimento.MedicoResponsavel.CRM} \nPaciente: {atendimento._Paciente.Nome} - {atendimento._Paciente.CPF},"); 

                }
                return bulder.ToString();
            }
        }

        public string ListarAtendimentosEmAbertoOrdenada(){
            var bulder = new StringBuilder();
            var listaOrdenadaPoratendeNaoConcluido = atendimentoRepositorio.ListarAtendimentoEmAberto();
            var tamanhoLista = atendimentoRepositorio.TamListAtendimento();

            if(tamanhoLista == 0){
                return bulder.Append("Lista vazia!").ToString();
            }else{
                foreach(Atendimento atendimento in listaOrdenadaPoratendeNaoConcluido){
                    bulder.AppendLine($"--Paciente--: \nInicio: {atendimento.Inicio}, \nSuspeita Inicial: {atendimento.SuspeitaInicial}, \nValor: {atendimento.Valor}");
                    foreach ((Exame exame, string resultado) in atendimento.ListaExamesResultados){
                        bulder.AppendLine($"- Exame - \nTítulo: {exame.Titulo}, \nValor: {exame.Valor}, \nDescrição: {exame.Descricao}, \nLocal: {exame.Local}, \nResultado: {resultado}");
                    }
                    bulder.AppendLine($"\nMédico: {atendimento.MedicoResponsavel.Nome} - CRM: {atendimento.MedicoResponsavel.CRM} \nPaciente: {atendimento._Paciente.Nome} - {atendimento._Paciente.CPF},"); 
                }
                return bulder.ToString();
            }
        }

        public string ListaExamesMaisUtiliz(int tamanho){
            var bulder = new StringBuilder();
            var listaExamesMaisUti = atendimentoRepositorio.ListaExamesMaisUtilizadas(tamanho);
            var tamanhoLista = atendimentoRepositorio.TamListAtendimento();

            if(tamanhoLista == 0){
                return bulder.Append("Lista vazia!").ToString();
            }else{
                foreach (Exame exame in listaExamesMaisUti){
                    bulder.AppendLine($"- Exame - \nTítulo: {exame.Titulo}, \nValor: {exame.Valor}, \nDescrição: {exame.Descricao}, \nLocal: {exame.Local}");
                }
                return bulder.ToString();
            }
                
        }




    }
}

