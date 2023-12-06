using Prova_grupo.Domain;

namespace Prova_grupo.Data
{
    public class AtendimentoRepositorio
    {
        private List<Atendimento> atendimentoList = new List<Atendimento> ();

        public void AddAtendimento(Atendimento atendimento){
            atendimentoList.Add(atendimento);
        }

        public List<Atendimento> ListarAtendimento(){
            return atendimentoList;
        }
        public int TamListAtendimento(){
            return atendimentoList.Count;
        }

        public bool VerificaAtendimentoMedicoPaciente(Medico medicoAtendi, Paciente pacienteAtendida){

            var verificaAtendimento = atendimentoList.Find(x=>x.MedicoResponsavel.Nome == medicoAtendi.Nome && x._Paciente.Nome ==pacienteAtendida.Nome);
            if(verificaAtendimento == null){
                return true;
            }else{
                return false;
            }
        }

        public Atendimento buscarPorId(int AtendimentoID){
            var idEncontrado = atendimentoList.Find(x => x.Id == AtendimentoID);
            if(idEncontrado != null){
                return idEncontrado;
            }else{
                throw new InvalidOperationException($"Atendimento com ID {AtendimentoID} não encontrado.");
            }

        }

        public List<Atendimento> BuscarAtendiSuspeitaDiagnostico(string suspeitaDiagnostico){
            var buscauspeitaDiagnostico = atendimentoList.FindAll(m => m.SuspeitaInicial.Contains(suspeitaDiagnostico) || (m.DiagnosticoFinal != null && m.DiagnosticoFinal.Contains(suspeitaDiagnostico)));
            

            if(buscauspeitaDiagnostico != null){
                return buscauspeitaDiagnostico;
            }else{
                throw new InvalidOperationException($"Suspeita ou Diagnostico {suspeitaDiagnostico} não encontrado.");
            }

        }

        public List<Medico> ListarMedicosPorAtendConcl(){
            var medicoAtendimentos = atendimentoList
                .Where(x => x.Fim != null)  
                .GroupBy(x => x.MedicoResponsavel)     
                .Select(group => new {
                    Medico = group.Key,
                    QuantidadeAtendimentosConcluidos = group.Count()
                })
                .OrderByDescending(x => x.QuantidadeAtendimentosConcluidos)
                .Select(x => x.Medico)
                .ToList();

            return medicoAtendimentos;   
        }

        public List<Exame> ListaExamesMaisUtilizadas(int tamanho)
        {
            var todosOsExames = atendimentoList
                .SelectMany(atendimento => atendimento.ListaExamesResultados.Select(tupla => tupla.Item1))
                .ToList();

            var examesAgrupados = todosOsExames
                .GroupBy(exame => exame.Descricao)
                .Select(grupo => new Exame(
                    grupo.Key, 
                    grupo.Average(exame => exame.Valor), 
                    grupo.First().Descricao, 
                    grupo.First().Local
                ))
                .OrderBy(exame => exame.Valor)
                .Take(tamanho)
                .ToList();

            return examesAgrupados;
        }

        public List<Atendimento> ListarAtendimentoEmAberto(){
            return atendimentoList.Where(x => x.Fim == null)  
                                    .OrderByDescending(x => x.Inicio)
                                    .ToList();
        } 

    }
}