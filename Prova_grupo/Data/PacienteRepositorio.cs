using Prova_grupo.Domain;
using Prova_grupo.Interfaces;

namespace Prova_grupo.Data
{
    public class PacienteRepositorio: PessoaRepoitorio, IPagamento
    {
        private List<Paciente> pacienteList = new List<Paciente>();
        private List<string> sintomas = new List<string> ();

        public override void CadastrarPaciente(Paciente paciente)
        {
            base.CadastrarPaciente(paciente);
            pacienteList.Add(paciente);

        }

        public void AddSintomas(string sintoma){
            sintomas.Add(sintoma);
        }

        public List<Paciente> ListaTodosPacientes(){
            return pacienteList;
        }

        public override int TamLista()
        {
            base.TamLista();
            return pacienteList.Count;
        }

        public override bool VerificaCPF(string CPF){
            base.ValidaCPF(CPF);
            foreach (var paciente in pacienteList) {
                if(paciente.CPF == CPF){
                    return false;
                }
            }
            return true;
        }

        public List<Paciente> GerarRelatorioIdadeMinMaxPaciente(int idadeMinimo, int idadeMaximo){
            var buscaMinMax = pacienteList.FindAll(m => m.IdadePessoa >= idadeMinimo && m.IdadePessoa <= idadeMaximo);

            if(buscaMinMax != null){
                return buscaMinMax;
            }else{
                throw new InvalidOperationException($"Paciente com idade entre {idadeMinimo} e {idadeMaximo} não encontrado");
            }       
        }

        public List<Paciente> BuscarPacientesPeloSexo(string sexo){
            var buscaSexo = pacienteList.FindAll(m => m.Sexo == sexo);

            if(buscaSexo  != null){
                return buscaSexo ;
            }else{
                throw new InvalidOperationException($"Pacientes do sexo {sexo} não encontrado");
            } 
            
        }

        public List<Paciente> ImprimiPacienteOrdemAlfabetica(){
            
            var pacienteOrdenado =  pacienteList.OrderBy(p => p.Nome).ToList();
            if(pacienteOrdenado  != null){
                return pacienteOrdenado ;
            }else{
                throw new InvalidOperationException($"Listas de Pacientes vazia");
            } 
            
        }

        public List<Paciente> BuscaPacienteSintomas(string sintoma){
            
            var buscaSintomas =  pacienteList.FindAll(p => p.Sintomas.Contains(sintoma));
            if(buscaSintomas  != null){
                return buscaSintomas ;
            }else{
                throw new InvalidOperationException($"Pacientes com sintoma {sintoma} não encontrado");
            }            
        }

        public Paciente BuscaPaciPorId(int id){
            
            var buscaID = pacienteList.Find(p=>p.IdPaciente == id);
            if(buscaID  != null){
                return buscaID ;
            }else{
                throw new InvalidOperationException($"Pacientes com id {id} não encontrado");
            }                        
        }

        public Paciente BuscaPaciPorCpf(string cpf){
            
            var buscaID = pacienteList.Find(p=>p.CPF == cpf);
            if(buscaID  != null){
                return buscaID ;
            }else{
                throw new InvalidOperationException($"Pacientes com CPF {cpf} não encontrado");
            }                        
        }

        public List<Paciente>  ImprimiPorMesNscimento(int mes) {
            var pacientesOrdenadosMesNasci = pacienteList.OrderBy(p => p.DataNascimento.Month == mes).ToList();

            if(pacientesOrdenadosMesNasci  != null){
                return pacientesOrdenadosMesNasci;
            }else{
                throw new InvalidOperationException($"Pacientes do {mes} não encontrado");
            }  
        }

        public void RealizarPagamento(int id, Pagamento pagamento)
        {
            var paciente = pacienteList.FirstOrDefault(p => p.IdPaciente == id);
            if(paciente  == null){
                throw new InvalidOperationException($"Pacientes com ID: {id} não encontrado");
            }else{
                paciente.Pagamentos.Add(pagamento);
            }
        }

        public void addPlano(int id, PlanoDeSaude plano)
        {
            var paciente = pacienteList.FirstOrDefault(p => p.IdPaciente == id);
            if(paciente  == null || plano == null){ 
                throw new InvalidOperationException($"Pacientes com ID: {id} não encontrado");
            }else{
                paciente.PlanoPaciente = plano;
            }
        }
    }
}
