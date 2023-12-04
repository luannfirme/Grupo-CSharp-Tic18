using Prova_grupo.Data;
using Prova_grupo.Domain;
using System.Text;

namespace Prova_grupo.Service
{
    public class ExameService
    {
        ExameRepositorio exameRepositorio = new ExameRepositorio();

        public List<Exame> AddExame(Exame exame){
                var listExames = exameRepositorio.AdicionarExame(exame);
                return listExames;
        }

        public string ListarExames(){
            var bulder = new StringBuilder();
            var examesLista = exameRepositorio.ListaExames();
            var tamListaExames = examesLista.Count;

            if(tamListaExames == 0){
                return bulder.Append("Lista de exames vazia!").ToString();
            }else{
                foreach(Exame exame in examesLista){
                    bulder.AppendLine($" Titulo: {exame.Titulo}, Valor: {exame.Valor}, Descrição: {exame.Descricao}, Local: {exame.Local}");
                }
                return bulder.ToString();
            }
        }


    }
}