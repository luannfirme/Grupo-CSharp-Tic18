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
        app.Inicializar();

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

    public void Inicializar(){
        Medicos.Add(new Medico {Nome = "Pedro Severino", Cpf = "156.168.966-03", DataDeNascimento = new DateTime(1989, 9, 15), Crm = "15699-66" });
        Medicos.Add(new Medico {Nome = "Maria Laura", Cpf = "155.965.896-02", DataDeNascimento = new DateTime(1992, 12, 08), Crm = "11556-78" });
        Medicos.Add(new Medico {Nome = "Laerth Filho", Cpf = "326.968.163-45", DataDeNascimento = new DateTime(1994, 11, 24), Crm = "96589-63" });

        Pacientes.Add(new Paciente {Nome = "Luann Firme", Cpf = "155.545.696-77", DataDeNascimento = new DateTime(1992, 12, 19), Sexo = "MASCULINO", Sintomas = new List<string>(){"dor de cabeça","febre" }});
        Pacientes.Add(new Paciente {Nome = "Théo Bento", Cpf = "222.123.676-96", DataDeNascimento = new DateTime(2020, 12, 15), Sexo = "MASCULINO", Sintomas = new List<string>(){"coriza","febre" }});
        Pacientes.Add(new Paciente {Nome = "Camila Bento", Cpf = "674.987.057-05", DataDeNascimento = new DateTime(1990, 07, 22), Sexo = "FEMININO", Sintomas = new List<string>(){"enjôo","sonolência" }});

        Exames.Add(new Exame { Titulo = "Ultrassonografia Obstétrica", Valor = float.Parse("149.99"), Descricao = "Ultrassom intra"});
        Exames.Add(new Exame { Titulo = "Hemograma", Valor = float.Parse("69.99"), Descricao = "Hemograma Completo"});
        Exames.Add(new Exame { Titulo = "Glicemia", Valor = float.Parse("84.50"), Descricao = "Glicemia em jejum"});
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
                    if (listaMedicos.Any())
                    {
                        foreach (var medico in listaMedicos)
                            Console.WriteLine($"Paciente: {medico.Nome}       Data de Nascimento: {medico.DataDeNascimento.ToString("dd/MM/yyyy")}      CPF: {medico.Cpf}     CRM: {medico.Crm}");

                        Console.Write("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                    break;
                case "2":
                    Console.Write("Informe a idade incial do paciente: ");
                    int pacienteInicial = int.Parse(Console.ReadLine());
                    Console.Write("Informe a idade final do paciente: ");
                    int pacienteFinal = int.Parse(Console.ReadLine());
                    var listaPaciente = app.Pacientes.Where(p => p.DataDeNascimento >= DateTime.Today.AddYears(-pacienteInicial) && p.DataDeNascimento <= DateTime.Today.AddYears(-pacienteFinal)).ToList();
                    if (listaPaciente.Any())
                    {
                        foreach (var paciente in listaPaciente)
                            Console.WriteLine($"Paciente: {paciente.Nome}       Data de Nascimento: {paciente.DataDeNascimento.ToString("dd/MM/yyyy")}      CPF: {paciente.Cpf}     Sexo: {paciente.Sexo}        Sintoma: {String.Join(", ", paciente.Sintomas)}");

                        Console.Write("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                    break;
                case "3":
                    Console.Write("Informe o sexo do paciente: ");
                    string sexo = Console.ReadLine();
                    var listaPacienteSexo = app.Pacientes.Where(p => p.Sexo == sexo.ToUpper()).ToList();
                    if (listaPacienteSexo.Any())
                    {
                        foreach (var paciente in listaPacienteSexo)
                            Console.WriteLine($"Paciente: {paciente.Nome}       Data de Nascimento: {paciente.DataDeNascimento.ToString("dd/MM/yyyy")}      CPF: {paciente.Cpf}     Sexo: {paciente.Sexo}        Sintoma: {String.Join(", ", paciente.Sintomas)}");

                        Console.Write("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                    break;
                case "4":
                    var listaPacienteNome = app.Pacientes.OrderBy(p => p.Nome).ToList();
                    foreach (var paciente in listaPacienteNome)
                        Console.WriteLine($"Paciente: {paciente.Nome}       Data de Nascimento: {paciente.DataDeNascimento.ToString("dd/MM/yyyy")}      CPF: {paciente.Cpf}     Sexo: {paciente.Sexo}        Sintoma: {String.Join(", ", paciente.Sintomas)}");

                    Console.Write("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Write("Informe o Sintoma que deseja buscar: ");
                    string sintoma = Console.ReadLine();
                    var listaPacienteSintoma = app.Pacientes.Where(p => p.Sintomas.Contains(sintoma)).ToList();
                    if (listaPacienteSintoma.Any())
                    {
                        foreach (var paciente in listaPacienteSintoma)
                            Console.WriteLine($"Paciente: {paciente.Nome}       Data de Nascimento: {paciente.DataDeNascimento.ToString("dd/MM/yyyy")}      CPF: {paciente.Cpf}     Sexo: {paciente.Sexo}        Sintoma: {String.Join(", ", paciente.Sintomas)}");

                        Console.Write("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                    break;
                case "6":
                    Console.Write("Informe o mês de aniversário (entre 1 e 12): ");
                    int mesAniversario = int.Parse(Console.ReadLine());
                    if (mesAniversario >= 1 && mesAniversario <= 12)
                    {
                        var listaPessoas = new List<Pessoa>();
                        listaPessoas.AddRange(app.Pacientes.Where(p => p.DataDeNascimento.Month == mesAniversario));
                        listaPessoas.AddRange(app.Medicos.Where(p => p.DataDeNascimento.Month == mesAniversario));

                        listaPessoas.OrderBy(p => p.Nome);

                        foreach (var pessoa in listaPessoas)
                            Console.WriteLine($"Nome: {pessoa.Nome}       Data de Nascimento: {pessoa.DataDeNascimento.ToString("dd/MM/yyyy")}      CPF: {pessoa.Cpf}");

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
                Sexo = sexo.ToUpper(),
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

