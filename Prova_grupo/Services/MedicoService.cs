using Prova_grupo.Data;
using Prova_grupo.Domain;
using System.Text;

namespace Prova_grupo.Service{
    public class MedicoService{
        MedicoRepositorio medicoRepositorio = new MedicoRepositorio();

        public string AddMedico(string nome, DateTime dataNascMedico, string cpfMedico, string crm){
            var bulder = new StringBuilder();
            var validaCPF = medicoRepositorio.ValidaCPF(cpfMedico);
            var medicoId = medicoRepositorio.TamLista() +1;
            var verificaCrmMedica= medicoRepositorio.VerificaCRM(crm);

            if(!validaCPF){
                return bulder.Append("CPF invalidado, tente novamento!").ToString();

            }if(!verificaCrmMedica){
                return bulder.Append("CRM já está cadastrado, tente novamento!").ToString();
            }else{
                medicoRepositorio.CadastrarMedico(new Medico(medicoId, nome, dataNascMedico, cpfMedico,crm));
                return "Medico adicionado com sucesso!";
            }  
        }

        public string ListarTodosMedicos(){
            var bulder = new StringBuilder();
            var medicosList = medicoRepositorio.ListarTodos();
            var tamanhoLista = medicosList.Count;

            if(tamanhoLista ==0){
                return bulder.Append("Lista vazia!").ToString();
            }else{
                bulder.AppendLine($"--Medicos--:");
                foreach(Medico medico in medicosList){
                    bulder.AppendLine($"Id: {medico.Id}, \nCPF: {medico.CPF}, \nNome: {medico.Nome}, \nDataNasc: {medico.DataNascimento.ToString("dd/MM/yyyy")}, \nCRM: {medico.CRM}");
                }
                return bulder.ToString();
            }    
        }

        public string GerarRelatorioIdadeMinMax(int idadeMinimo, int idadeMaximo){

            var bulder = new StringBuilder();
            var medicosIdadeMinMax = medicoRepositorio.GerarRelatorioIdadeMinMaxMedico(idadeMinimo, idadeMaximo);
            var tamanhoLista = medicosIdadeMinMax.Count;

            if(tamanhoLista ==0){
                return bulder.Append("Não existe medicos mas essa faixa etária!").ToString();
            }else{
                Console.WriteLine($"Médicos com idade entre {idadeMinimo} e {idadeMaximo}:");

                foreach(Medico medico in medicosIdadeMinMax){
                    bulder.AppendLine($"Medico-> Nome: {medico.Nome}, Idade: {medico.IdadePessoa}");
                }
                return bulder.ToString();
            }
        }

        public Medico BuscarMedicoPorId(int id){
            var medicoId = medicoRepositorio.BuscaPorId(id);

            if(medicoId ==null){
                throw new ArgumentException("Médico não encontrado", nameof(id));
            }else{
                return medicoId;
            }
        }
    }
}