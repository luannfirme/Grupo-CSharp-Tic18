namespace ProvaGrupoNET;

public class App
{
    private List<Atendimento> Atendimentos = new List<Atendimento>();
    private List<Medico> Medicos = new List<Medico>();
    private List<Paciente> Pacientes = new List<Paciente>();

    public static void Menu()
    {
        var app = new App();

        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("");
            Console.WriteLine("------- MENU -------");
            Console.WriteLine("1 - INICIAR ATENDIMENTO");
            Console.WriteLine("2 - FINALIZAR ATENDIMENTO");
            Console.WriteLine("3 - CADASTRAR MEDICO");
            Console.WriteLine("4 - CADASTRAR PACIENTE");
            Console.WriteLine("5 - CADASTRAR EXAME");
            Console.WriteLine("6 - GERAR RELATÓRIO");
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
                    app.FinalizarAtendimento();
                    break;
                case "3":
                    app.CadastrarMedico();
                    break;
                case "4":
                    app.CadastrarPaciente();
                    break;
                case "5":
                    app.CadastrarExame();;
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

    public void CadastrarExame(){

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

            if(medico == null)
                throw new AppException("\nMédico não encontrado.");

            Console.Write("Informe o CPF do paciente: ");
            string cpf = Console.ReadLine();

            Paciente paciente = Pacientes.FirstOrDefault(p => p.Cpf == cpf);

            if(paciente == null)
                throw new AppException("\nPaciente não encontrado.");

            if (!ValidarAtendimento(medico, paciente))
                throw new AppException("\nO atendimento já está em curso para esse médico e paciente.");

            Console.Write("Informe a suspeita inicial: ");
            string suspeita = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(suspeita))
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

            if(string.IsNullOrWhiteSpace(diagnostico))
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

