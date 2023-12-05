namespace ProvaGrupoNET;

public class App
{
    private List<Atendimento> Atendimentos = new List<Atendimento>();
    private List<Medico> Medicos = new List<Medico>();
    private List<Paciente> Pacientes = new List<Paciente>();
    private List<Exame> Exames = new List<Exame>();

    public static void MenuPrincipal()
    {
        var app = new App();

        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("");
            Console.WriteLine("------- MENU -------");
            Console.WriteLine("1 - INICIAR ATENDIMENTO");
            Console.WriteLine("2 - ATUALIZAR ATENDIMENTO");
            Console.WriteLine("3 - FINALIZAR ATENDIMENTO");
            Console.WriteLine("4 - CADASTRAR MEDICO");
            Console.WriteLine("5 - CADASTRAR PACIENTE");
            Console.WriteLine("6 - CADASTRAR EXAME");
            Console.WriteLine("7 - GERAR RELATÓRIO");
            Console.WriteLine("0 - SAIR");
            Console.Write("Escolha uma opção: ");
            string escolha = Console.ReadLine();
            Console.WriteLine("");

            switch (escolha)
            {
                case "1":
                    app.IniciarAtendimento();
                    break;
                case "2":
                    app.AtualizarAtendimento();
                    break;
                case "3":
                    app.FinalizarAtendimento();
                    break;
                case "4":
                    app.CadastrarMedico();
                    break;
                case "5":
                    app.CadastrarPaciente();
                    break;
                case "6":
                    app.CadastrarExame(); ;
                    break;
                case "7":
                    MenuRelatorios(app); ;
                    break;
                case "0":
                    Console.Write("Aplicação encerrada...");
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    public static void MenuRelatorios(App app)
    {
        bool sair = false;
        while (!sair)
        {
            Console.WriteLine("");
            Console.WriteLine("------- RELÓRIOS -------");
            Console.WriteLine("1. Médicos com idade entre dois valores");
            Console.WriteLine("2- Pacientes com idade entre dois valores");
            Console.WriteLine("3- Pacientes do sexo informado pelo usuário");
            Console.WriteLine("4- Pacientes em ordem alfabética");
            Console.WriteLine("5- Pacientes cujos sintomas contenha texto informado");
            Console.WriteLine("6- Médicos e Pacientes aniversariantes do mês informado");
            Console.WriteLine("7- Atendimentos em aberto");
            Console.WriteLine("8- Médicos por atendimentos concluídos");
            Console.WriteLine("9- Atendimento cuja suspeita ou diagnostico contenha texto informado");
            Console.WriteLine("10- Top 10 atendimentos mais utilizados");
            Console.WriteLine("0- voltar");
            Console.Write("Escolha uma opção: ");
            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    Console.Write("Informe a idade incial do médico: ");
                    int medicoInicial = int.Parse(Console.ReadLine());
                    Console.Write("Informe a idade final do médico: ");
                    int medicoFinal = int.Parse(Console.ReadLine());
                    var listaMedicos = app.Medicos.Where(p => p.DataDeNascimento >= DateTime.Today.AddYears(-medicoInicial) && p.DataDeNascimento <= DateTime.Today.AddYears(-medicoFinal)).ToList();
                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Write("Informe a idade incial do paciente: ");
                    int pacienteInicial = int.Parse(Console.ReadLine());
                    Console.Write("Informe a idade final do paciente: ");
                    int pacienteFinal = int.Parse(Console.ReadLine());
                    app.obterPacientesIntevaloIdade(pacienteInicial, pacienteFinal);
                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "3":
                        Console.Write("Informe o sexo do paciente: ");
                        string sexo = Console.ReadLine();
                        app.obterPacientesPorSexo(sexo);
                        Console.Write("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                    break;
                case "4":
                    app.obterPacientesPorOrdemAlfabetica();
                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Write("Informe o Sintoma que deseja buscar: ");
                    string sintoma = Console.ReadLine();
                    app.obterPacientesPorSintomas(sintoma);
                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "6":
                    Console.Write("Informe o mês de aniversário (entre 1 e 12): ");
                    int mesAniversario = int.Parse(Console.ReadLine());
                    if (mesAniversario >= 1 && mesAniversario <= 12)
                    {
                        app.obterAniversariantesPorMes(mesAniversario);
                        Console.Write("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                    break;
                case "0":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    public void CadastrarMedico()
    {
        try
        {
            Console.WriteLine("\nCadastro de Novo Medico:");

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("CPF [000.000.000-00]: ");
            string cpf = Console.ReadLine();

            Console.Write("Data de Vencimento (dd/MM/yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataNascimento))
                throw new AppException("\nData de nascimento inváldia.");

            Console.Write("CRM: ");
            string crm = Console.ReadLine();

            Medico medico = new Medico()
            {
                Nome = nome,
                Cpf = cpf,
                DataDeNascimento = dataNascimento,
                Crm = crm
            };

            var cpfs = Medicos.Select(c => c.Cpf).ToList();

            if (medico.ValidarCPF(cpfs) != null)
                throw new AppException(medico.ValidarCPF(cpfs));

            if (medico.ValidarCRM(Medicos) != null)
                throw new AppException(medico.ValidarCRM(Medicos));

            Medicos.Add(medico);

            Console.WriteLine("\nMédico cadastrado com sucesso!");
        }
        catch (FormatException)
        {
            Console.WriteLine("\nErro: Entrada inválida. Por favor, insira um valor válido.");
        }
        catch (AppException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro inesperado: {ex.Message}");
        }
    }

    public void CadastrarPaciente()
    {
        try
        {
            Console.WriteLine("\nCadastro de Novo Paciente:");

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("CPF [000.000.000-00]: ");
            string cpf = Console.ReadLine();

            Console.Write("Data de Vencimento (dd/MM/yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataNascimento))
                throw new AppException("\nData de nascimento inválida.");

            Console.Write("Sexo: ");
            string sexo = Console.ReadLine();


            Console.Write("Sintomas [utilize ',']: ");
            List<string> sintomas = Console.ReadLine().Split(',').Select(str => str.Trim()).ToList();

            Paciente paciente = new Paciente()
            {
                Nome = nome,
                Cpf = cpf,
                DataDeNascimento = dataNascimento,
                Sexo = sexo,
                Sintomas = sintomas
            };

            var cpfs = Pacientes.Select(c => c.Cpf).ToList();

            if (paciente.ValidarCPF(cpfs) != null)
                throw new AppException("\nCPF já existe.");

            Pacientes.Add(paciente);

            Console.WriteLine("\nPaciente cadastrado com sucesso!");
        }
        catch (FormatException)
        {
            Console.WriteLine("\nErro: Entrada inválida. Por favor, insira um valor válido.");
        }
        catch (AppException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErro inesperado: {ex.Message}");
        }
    }

    public void CadastrarExame()
    {
        try
        {
            Console.WriteLine("\nCadastro de Novo Exame:");

            Console.Write("Exame: ");
            string titulo = Console.ReadLine();

            Console.Write("Valor: ");
            float preco = float.Parse(Console.ReadLine());

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Local: ");
            string local = Console.ReadLine();

            Exame exame = new Exame()
            {
                Titulo = titulo,
                Valor = preco,
                Descricao = descricao,
                Local = local
            };

            Exames.Add(exame);

            Console.WriteLine("\nExame cadastrado com sucesso!");

        }
        catch (AppException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

    }

    public void IniciarAtendimento()
    {
        try
        {
            int id = Atendimentos.Count + 1;
            DateTime dataInicial = DateTime.Now;

            Console.Write("Informe o CRM do médico: ");
            string crm = Console.ReadLine();

            Medico medico = Medicos.FirstOrDefault(m => m.Crm == crm);

            if (medico == null)
                throw new AppException("\nMédico não encontrado.");

            Console.Write("Informe o CPF do paciente: ");
            string cpf = Console.ReadLine();

            Paciente paciente = Pacientes.FirstOrDefault(p => p.Cpf == cpf);

            if (paciente == null)
                throw new AppException("\nPaciente não encontrado.");

            if (!ValidarAtendimento(medico, paciente))
                throw new AppException("\nO atendimento já está em curso para esse médico e paciente.");

            Console.Write("Informe a suspeita inicial: ");
            string suspeita = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(suspeita))
                throw new AppException("\nA suspeita inicial é obrigatória.");

            Atendimento novoAtendimento = new Atendimento(id, medico, paciente, suspeita, dataInicial);
            Atendimentos.Add(novoAtendimento);
            Console.WriteLine($"Atendimento Nº {id} iniciado com sucesso.");
        }
        catch (AppException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }

    public void FinalizarAtendimento()
    {
        try
        {
            DateTime dataFinal = DateTime.Now;

            Console.Write("Informe o Nº do atendimento: ");
            int atendimentoId = int.Parse(Console.ReadLine());

            Atendimento atendimentoEmCurso = Atendimentos.FirstOrDefault(a => a.IdAtendimento == atendimentoId && a.Fim == null);

            if (atendimentoEmCurso == null)
                throw new AppException("\nNão há atendimento em curso para esse médico e paciente.");

            if (dataFinal <= atendimentoEmCurso.Inicio)
                throw new AppException("\nA data final deve ser posterior à data inicial.");

            Console.Write("Informe o diagnóstico final: ");
            string diagnostico = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(diagnostico))
                throw new AppException("\no diagnóstico final é obrigatório.");

            atendimentoEmCurso.FecharAtendimento(diagnostico, dataFinal);
            Console.WriteLine("Atendimento finalizado com sucesso.");
        }
        catch (AppException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }

    public void AtualizarAtendimento()
    {
        try
        {
            Console.Write("Informe o Nº do atendimento: ");
            int atendimentoId = int.Parse(Console.ReadLine());

            Atendimento atendimentoEmCurso = Atendimentos.FirstOrDefault(a => a.IdAtendimento == atendimentoId && a.Fim == null);

            if (atendimentoEmCurso == null)
                throw new AppException("\nNão há atendimento em curso para esse médico e paciente.");

            Console.Write("Informe o Exame realizado: ");
            string titulo = Console.ReadLine();

            Exame exameRealizado = Exames.FirstOrDefault(e => e.Titulo == titulo);

            if (exameRealizado == null)
                throw new AppException("\nNão há exames cadastrados com o título informado.");

            Console.Write("Informe o resultado do exame: ");
            string resultado = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(resultado))
                throw new AppException("\no resultado é obrigatório.");

            atendimentoEmCurso.VincularExame(resultado, exameRealizado);
            Console.WriteLine("Atendimento atualizado com sucesso.");
        }
        catch (AppException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }

    private bool ValidarAtendimento(Medico medico, Paciente paciente)
    {
        return !Atendimentos.Any(a =>
            a.Medico == medico && a.Paciente == paciente && a.Fim == null);
    }

    class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
        }
    }
}

