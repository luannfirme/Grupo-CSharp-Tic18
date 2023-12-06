using Prova_grupo.Domain;


namespace Prova_grupo.Data{

    public abstract class PessoaRepoitorio{
        public bool ValidaCPF(string CPF){
            return CPF.Length == 11;
        }

        public virtual void CadastrarMedico(Medico medico){}
        public virtual void CadastrarPaciente(Paciente paciente){}

        public virtual bool VerificaCPF(string CPF){return true;}

        public virtual int TamLista(){return 0;}


    }
}