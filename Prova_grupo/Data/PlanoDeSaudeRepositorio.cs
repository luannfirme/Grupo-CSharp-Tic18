using Prova_grupo.Domain;

namespace Prova_grupo.Data{
    public class PlanoDeSaudeRepositorio
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
            var plano = planoList.Find(p => p.Titulo == titulo);
            if(plano != null){
                return plano;
            }else{
                throw new InvalidOperationException($"Plano de saúde com título {titulo} não encontrado");
            }
        }
    }
}