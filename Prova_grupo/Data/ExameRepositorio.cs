using Prova_grupo.Domain;

namespace Prova_grupo.Data
{
    public class ExameRepositorio
    {
        private List<Exame> listaExames = new List<Exame>();

        public List<Exame> AdicionarExame(Exame novoExame)
        {
            listaExames.Add(novoExame);
            return listaExames;
        }

        public List<Exame> ListaExames(){
            return listaExames;
        }

        public int TamListaExame(){
            return listaExames.Count;
        }


        
    }
}