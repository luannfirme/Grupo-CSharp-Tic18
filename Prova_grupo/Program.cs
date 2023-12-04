using Prova_grupo.Service;

using Prova_grupo.Apresentacao;
namespace Prova_grupo{
    class Program{
        static void Main(){
            MedicoPacienteApresentacao medicoPacienteApresentacao = new MedicoPacienteApresentacao();

            MedicoService medicoService = new MedicoService();
            PacienteService pacienteService = new PacienteService();


            medicoPacienteApresentacao.MenuDeServicos(medicoService, pacienteService);

 
        }
    }
}
