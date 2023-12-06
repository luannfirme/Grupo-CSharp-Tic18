using Prova_grupo.Data;
using Prova_grupo.Domain;
using System.Text;

namespace Prova_grupo.Service
{
    public class PlanoDeSaudeService
    {
        PlanoDeSaudeRepositorio planoRepositorio = new PlanoDeSaudeRepositorio();

        public string AddPlano(string titulo, double valor)
        {
            planoRepositorio.CadastrarPlano(new PlanoDeSaude(titulo, valor));
            return "Plano adicionado com sucesso!";
        }
    }
}