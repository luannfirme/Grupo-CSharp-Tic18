using Prova_grupo.Domain;

namespace Prova_grupo.Data{
    public class MedicoPlanoDeSaudeRepositorioRepositorio
    {
        private List<PlanoDeSaude> planoList = new List<PlanoDeSaude>();
        public List<PlanoDeSaude> CadastrarPlano(PlanoDeSaude plano){
            planoList.Add(plano);
            return planoList;
        }

        public List<PlanoDeSaude> ListarTodos(){
            return planoList;
        }

        public PlanoDeSaude BuscaPorTitulo(string titulo){
            var medicoId = planoList.Find(p => p.Titulo == titulo);
            if(medicoId != null){
                return medicoId;
            }else{
                throw new InvalidOperationException($"Plano de saúde com título {titulo} não encontrado");
            }
        }
    }
}