using Prova_grupo.Service;
using Prova_grupo.Domain;
using System.Globalization;

namespace Prova_grupo.Apresentacao{
    public class MedicoPacienteApresentacao{

        public DateTime solicitaData(){
            DateTime dataNascPaciente;
            bool dataValida = false;

            do
            {
                Console.WriteLine("Digite o data de nascimento (formato dd/MM/yyyy):");
                string inputDataNascimento = Console.ReadLine()!;

                if (DateTime.TryParseExact(inputDataNascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascPaciente)){
                    dataValida = true;
                }else{
                    Console.WriteLine("Formato de data inválido. Tente novamente.");
                }

            } while (!dataValida);

            return dataNascPaciente;

        }

        public void Menu(MedicoService medicoService, PacienteService pacienteService){
            string operador = String.Empty;
            Console.Clear();
            while(operador!="0"){
                Console.WriteLine("1 -Adicionar um novo Medico");
                Console.WriteLine("2 -Listar todos os Medicos");
                Console.WriteLine("3 -Gerar Relatorio de  entre a idade Minima e Maxima do medico");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("4 -Adicionar um novo Paciente");
                Console.WriteLine("5 -Listar todos os Pacientes");
                Console.WriteLine("6 -Listar por ordem alfabetica");
                Console.WriteLine("7 -Gerar Relatorio de  entre a idade Minima e Maxima do paciente");
                Console.WriteLine("8 -Listar Pacientes pelo sexo");
                Console.WriteLine("9 -Listar Pacientes pelos sintomas");
                Console.WriteLine("10 -Buscar Pacientes pelo mês de nascimento");
                Console.WriteLine("11 -Buscar Médico pelo mês de nascimento");


                operador = Console.ReadLine()!;

                switch(operador){
                    case "1":

                        Console.WriteLine("Digite o Nome do medico:");
                        string nomeMedico = Console.ReadLine()!;

                        var dataNascMedico = solicitaData();

                        Console.WriteLine("Digite o cpf:");
                        string cpfMedico = Console.ReadLine()!;
                        
                        Console.WriteLine("Digite o CRM:");
                        string crm = Console.ReadLine()!;

                        var response = medicoService.AddMedico(nomeMedico, dataNascMedico, cpfMedico,crm);
                        Console.WriteLine(response);
                        Console.WriteLine("---------------------");

                        break;
                    case "2":
                        var response02 = medicoService.ListarTodosMedicos();
                        Console.WriteLine(response02);
                        Console.WriteLine("---------------------");
                        break;
                    case "3":
                        var response03 = medicoService.GerarRelatorioIdadeMinMax(20, 31);
                        Console.WriteLine(response03);
                        Console.WriteLine("---------------------");
                        break;

                    case "4":
                        List<string> sintomasLista = new List<string>();

                        Console.WriteLine("Digite o Nome do paciente:");
                        string nomePaciente = Console.ReadLine()!;

                        var dataNascPaciente = solicitaData();                       

                        Console.WriteLine("Digite o cpf:");
                        string cpfPaciente = Console.ReadLine()!;
                        
                        Console.WriteLine("Digite o Sexo (Feminino/ Masculino):");
                        string sexo = Console.ReadLine()!;

                        do {
                            Console.WriteLine("Digite os sintomas:");
                            string sintoma = Console.ReadLine()!;
                            sintomasLista.Add(sintoma);
                            Console.WriteLine("Deseja adicionar mais um sintoma? (S/N)");
                        } while (Console.ReadLine()?.Trim().ToUpper() == "S");

                        var response04 = pacienteService.AddPaciente(nomePaciente, dataNascPaciente, cpfPaciente, sexo, sintomasLista);
                        Console.WriteLine(response04);
                        Console.WriteLine("---------------------");

                        break;
                    case "5":
                        var response05 = pacienteService.ListarPacientes();
                        Console.WriteLine(response05);
                        Console.WriteLine("---------------------");
                        break;

                    case "6":
                        var response06 = pacienteService.ListarPacientesOrdemAlfab();
                        Console.WriteLine(response06);
                        Console.WriteLine("---------------------");
                        break;

                    case "7":
                        var response07 = pacienteService.GerarRelatorioIdadeMinMaxPaciente(20, 35);
                        Console.WriteLine(response07);
                        Console.WriteLine("---------------------");
                        break;

                    case "8":
                        Console.WriteLine("Digite o Sexo (Feminino/ Masculino) para a busca");
                        string sexoPaciente = Console.ReadLine()!;

                        var response08 = pacienteService.BuscaPacienSexo(sexoPaciente);
                        Console.WriteLine(response08);
                        Console.WriteLine("---------------------");
                        break;

                    case "9":
                        Console.WriteLine("Digite os sintomas para a buscar");
                        string sintomasPaciente = Console.ReadLine()!;
                        var response09 = pacienteService.BuscarPacientesPorSintomas(sintomasPaciente);
                        Console.WriteLine(response09);
                        Console.WriteLine("---------------------");
                        break;


                    case "10":
                        Console.WriteLine("Digite o mês de busca.");
                        int mesPaciente = int.Parse(Console.ReadLine()!);

                        var response10 = pacienteService.BuscaPacientePorMesNascimento(mesPaciente);
                        Console.WriteLine(response10);
                        Console.WriteLine("---------------------");

                        break;

                    case "11":
                        Console.WriteLine("Digite o mês de busca.");
                        int mesMedico = int.Parse(Console.ReadLine()!);

                        var response11 = medicoService.BuscaMedicoPorMesNascimento(mesMedico);
                        Console.WriteLine(response11);
                        Console.WriteLine("---------------------");

                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            if (operador == "0"){
                    MenuDeServicos(medicoService, pacienteService);
            }
        }


        public void MenuApresentacao(MedicoService medicoService, PacienteService pacienteService){
            Console.Clear();

            const int TAM_LIST_MAIS_USADOS = 10;
            
            AtendimentoService atendimentoService = new AtendimentoService();


            string operador = String.Empty;

            while(operador!="0"){
                Console.WriteLine("1 -Iniciar Atendimento");
                Console.WriteLine("2 -Listar atendimentos em aberto");
                Console.WriteLine("3 -Buscar atendimento por suspeita ou diagnostico");
                Console.WriteLine("4 -Listar exames mais utilizados");
                Console.WriteLine("5 -Listar Medicos por quantidade de atendimento concluido");
                Console.WriteLine("6 -Finalizar atendimento");
                operador = Console.ReadLine()!;

                switch(operador){
                    case "1":
                        List<(Exame, string)> listaExamesAtendimento = new List<(Exame, string)>();

                        Console.WriteLine("Digite a suspeita inicial:");
                        string suspeitaInicial = Console.ReadLine()!;

                        do {
                            Console.WriteLine("Digite o titulo do exame:");
                            string titulo = Console.ReadLine()!;

                            Console.WriteLine("Digite o valor do  exame:");
                            float valor = float.Parse(Console.ReadLine()!);

                            Console.WriteLine("Digite a descrição do exame:");
                            string descricao = Console.ReadLine()!;


                            Console.WriteLine("Digite o local exame:");
                            string local = Console.ReadLine()!;


                            listaExamesAtendimento.Add((new Exame(titulo, valor, descricao, local), null));
                            Console.WriteLine("Deseja adicionar mais um exame? (S/N)");
                        } while (Console.ReadLine()?.Trim().ToUpper() == "S");

                        Console.WriteLine("Digite o valor do atendimento:");
                        float valorAtendimento = float.Parse(Console.ReadLine()!);

                        //Lista de medicos para o atendimento;
                        var medicosParaAtendimento = medicoService.ListarTodosMedicos();
                        Console.WriteLine(medicosParaAtendimento);

                        Console.WriteLine("Escolha o medico pelo id:");
                        int idMedico = int.Parse(Console.ReadLine()!);
                        var buscaMedicoId = medicoService.BuscarMedicoPorId(idMedico);


                        //Lista de pacienentes para o atendimento;
                       var pacientePataAtendimento = pacienteService.ListarPacientes();
                        Console.WriteLine(pacientePataAtendimento);   

                        Console.WriteLine("Escolha o paciente pelo id:");
                        int idPaciente = int.Parse(Console.ReadLine()!);
                        var buscaPaciente = pacienteService.BuscarPacientePorId(idPaciente);

                        var response = atendimentoService.iniciarAtendimento( suspeitaInicial, listaExamesAtendimento, valorAtendimento, buscaMedicoId, buscaPaciente );
                        
                        Console.WriteLine(response);
                        Console.WriteLine("---------------------");

                        break;
                    case "2":
                        var response02 = atendimentoService.ListarAtendimentosEmAbertoOrdenada();
                        Console.WriteLine(response02);
                        Console.WriteLine("---------------------");
                        break;
                    case "3":
                        Console.WriteLine("Digite a suspeita ou diagnostico para busca em nossa base de dados:");
                        string suspeitaDiagnosticoBusca = Console.ReadLine()!;

                        var response03 = atendimentoService.BuscaPorSuspeitaDiagnosticoFinal(suspeitaDiagnosticoBusca);
                        Console.WriteLine(response03);
                        Console.WriteLine("---------------------");
                        break;

                    case "4":

                        var response04 =atendimentoService.ListaExamesMaisUtiliz(TAM_LIST_MAIS_USADOS);
                        Console.WriteLine(response04);
                        Console.WriteLine("---------------------");

                        break;
                    case "5":
                        var response05 = atendimentoService.BuscaMedicosPorAtendimentosConcluidos();
                        Console.WriteLine(response05);
                        Console.WriteLine("---------------------");
                        break;

                    case "6":
                           
                            var datafim = solicitaData();

                            Console.WriteLine("Digite o diagnostico final:");
                            string diagnosticoFinal = Console.ReadLine()!;

                            Console.WriteLine("Digite o id do atendimento:");
                            int idAtendimentoFinal = int.Parse(Console.ReadLine()!);

                        var response06 = atendimentoService.fimAtendimento(datafim, diagnosticoFinal, idAtendimentoFinal);
                        Console.WriteLine(response06);
                        Console.WriteLine("---------------------");
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            if (operador == "0"){
                    MenuDeServicos(medicoService, pacienteService);
                    
            }
        }
        public void MenuDeServicos(MedicoService medicoService, PacienteService pacienteService){
            Console.Clear();
            MedicoPacienteApresentacao medicoPacienteApresentacao = new MedicoPacienteApresentacao();

            string operador = String.Empty;

            while(operador!="0"){
                Console.WriteLine("1 -Menu de Medicos e Pacientes");
                Console.WriteLine("2 -Menu de tendimentos");

                operador = Console.ReadLine()!;

                switch(operador){
                    case "1":
                        medicoPacienteApresentacao.Menu(medicoService, pacienteService );
                        break;
                    case "2":
                        medicoPacienteApresentacao.MenuApresentacao(medicoService, pacienteService );
                        break;
                    case "0":

                        break;
                    default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                }
            }
            if (operador == "0"){
                    MenuDeServicos(medicoService, pacienteService);
                    Console.Clear();
                }


 
        }

    }
}