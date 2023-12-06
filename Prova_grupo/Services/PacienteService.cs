using Prova_grupo.Data;
using Prova_grupo.Domain;
using System.Text;

namespace Prova_grupo.Service
{    public class PacienteService
    {
        PacienteRepositorio pacienteRepositorio = new PacienteRepositorio();

        public string AddPaciente(string nome, DateTime dataNascPaciente, string cpfPaciente, string sexo, List<string> sintomas)
        {
            var bulder = new StringBuilder();
            var validaCPF = pacienteRepositorio.ValidaCPF(cpfPaciente);
            var verificaCpfLista = pacienteRepositorio.ValidaCPF(cpfPaciente);
            var pacienteId = pacienteRepositorio.TamLista() + 1;

            if (!validaCPF)
            {
                return bulder.Append("CPF invalidado, tente novamento!").ToString();

            }
            if (!verificaCpfLista)
            {
                return bulder.Append("CPF já está cadastrado, tente novamento!").ToString();
            }
            else
            {
                pacienteRepositorio.CadastrarPaciente(new Paciente(pacienteId, nome, dataNascPaciente, cpfPaciente, sexo, sintomas));
                return "Paciente adicionado com sucesso!";
            }
        }

        public string ListarPacientes()
        {
            var bulder = new StringBuilder();
            var pacienteLista = pacienteRepositorio.ListaTodosPacientes();
            var tamListaPacientes = pacienteLista.Count;

            if (tamListaPacientes == 0)
            {
                return bulder.Append("Lista vazia!").ToString();
            }
            else
            {
                foreach (Paciente paciente in pacienteLista)
                {
                    bulder.AppendLine($"--Paciente--: \nCPF: {paciente.CPF}, \nNome: {paciente.Nome}, \nDataNasc: {paciente.DataNascimento}, \nSexo: {paciente.Sexo}");
                    bulder.AppendLine("Sintomas:");
                    foreach (string sintomaPaciente in paciente.Sintomas)
                    {
                        bulder.AppendLine($"Sintomas:{sintomaPaciente}");
                    }
                }
                return bulder.ToString();
            }
        }

        public string GerarRelatorioIdadeMinMaxPaciente(int idadeMinimo, int idadeMaximo)
        {
            var bulder = new StringBuilder();
            var pacienteIdadeMinMax = pacienteRepositorio.GerarRelatorioIdadeMinMaxPaciente(idadeMinimo, idadeMaximo);
            var tamanhoLista = pacienteIdadeMinMax.Count;

            if (tamanhoLista == 0)
            {
                return bulder.Append("Não existe paciente mas essa faixa etária!").ToString();
            }
            else
            {
                Console.WriteLine($"Médicos com idade entre {idadeMinimo} e {idadeMaximo}:");

                foreach (Paciente paciente in pacienteIdadeMinMax)
                {
                    bulder.AppendLine($"Paciente-> Nome: {paciente.Nome}, Idade: {paciente.IdadePessoa}");
                }
                return bulder.ToString();
            }
        }

        public string BuscaPacienSexo(string sexo)
        {
            var bulder = new StringBuilder();
            var pacientePorSexo = pacienteRepositorio.BuscarPacientesPeloSexo(sexo);
            var tamanhoLista = pacientePorSexo.Count;

            if (tamanhoLista == 0)
            {
                return bulder.Append("Não existe pacientes para o sexo selecionado!").ToString();
            }
            else
            {
                Console.WriteLine($"Pacientes do sexo {sexo}:");
                foreach (Paciente paciente in pacientePorSexo)
                {
                    bulder.AppendLine($"--Paciente--: \nCPF: {paciente.CPF}, \nNome: {paciente.Nome}, \nDataNasc: {paciente.DataNascimento.ToString("dd/MM/yyyy")}, \nSexo: {paciente.Sexo}");
                    bulder.AppendLine("Sintomas:");
                    foreach (string sintomaPaciente in paciente.Sintomas)
                    {
                        bulder.AppendLine($"->{sintomaPaciente}");
                    }
                }
                return bulder.ToString();
            }
        }

        public string ListarPacientesOrdemAlfab()
        {
            var bulder = new StringBuilder();
            var pacientesOrdemAlfab = pacienteRepositorio.ImprimiPacienteOrdemAlfabetica();
            var tamanhoLista = pacientesOrdemAlfab.Count;

            if (tamanhoLista == 0)
            {
                return bulder.Append("Lista vazia!").ToString();
            }
            else
            {
                Console.WriteLine($"Pacientes por ordem alfabetica:");
                foreach (Paciente paciente in pacientesOrdemAlfab)
                {
                    bulder.AppendLine($"--Paciente--: \nCPF: {paciente.CPF}, \nNome: {paciente.Nome}, \nDataNasc: {paciente.DataNascimento.ToString("dd/MM/yyyy")}, \nSexo: {paciente.Sexo}");
                    bulder.AppendLine("Sintomas:");
                    foreach (string sintomaPaciente in paciente.Sintomas)
                    {
                        bulder.AppendLine($"Sintomas:{sintomaPaciente}");
                    }
                }
                return bulder.ToString();
            }

        }

        public string BuscarPacientesPorSintomas(string sintoma)
        {
            var bulder = new StringBuilder();
            var pacientesPorSintomas = pacienteRepositorio.BuscaPacienteSintomas(sintoma);
            var tamanhoLista = pacientesPorSintomas.Count;

            if (tamanhoLista == 0)
            {
                return bulder.Append("Sem pacientes com esse sintomas!").ToString();
            }
            else
            {
                Console.WriteLine($"Pacientes com os os sintomas {sintoma}:");
                foreach (Paciente paciente in pacientesPorSintomas)
                {
                    bulder.AppendLine($"--Paciente--: \nCPF: {paciente.CPF}, \nNome: {paciente.Nome}, \nDataNasc: {paciente.DataNascimento.ToString("dd/MM/yyyy")}, \nSexo: {paciente.Sexo}");
                    bulder.AppendLine("Sintomas:");
                    foreach (string sintomaPaciente in paciente.Sintomas)
                    {
                        bulder.AppendLine($"Sintomas:{sintomaPaciente}");
                    }
                }
                return bulder.ToString();
            }
        }

        public Paciente BuscarPacientePorId(int id)
        {
            var pacienteId = pacienteRepositorio.BuscaPaciPorId(id);

            if (pacienteId == null)
            {
                throw new ArgumentException("Paciente não encontrado", nameof(id));
            }
            else
            {
                return pacienteId;
            }
        }

        public string BuscaPacientePorMesNascimento(int mes)
        {
            var bulder = new StringBuilder();
            var pacientesPorMesNascimento = pacienteRepositorio.ImprimiPorMesNscimento(mes);
            var tamanhoLista = pacientesPorMesNascimento.Count;

            if (tamanhoLista == 0)
            {
                return bulder.Append("Lista vazia!").ToString();
            }
            else
            {
                Console.WriteLine($"Pacientes do mês de {mes}:");
                foreach (Paciente paciente in pacientesPorMesNascimento)
                {
                    bulder.AppendLine($"--Paciente--: \nCPF: {paciente.CPF}, \nNome: {paciente.Nome}, \nDataNasc: {paciente.DataNascimento.ToString("dd/MM/yyyy")}, \nSexo: {paciente.Sexo}");
                    bulder.AppendLine("Sintomas:");
                    foreach (string sintomaPaciente in paciente.Sintomas)
                    {
                        bulder.AppendLine($"Sintomas:{sintomaPaciente}");
                    }
                }
                return bulder.ToString();
            }
        }

        public string RealizarPagamento(string cpf, Pagamento pagamento)
        {
            var paciente = pacienteRepositorio.BuscaPaciPorCpf(cpf);

            if (paciente == null)
            {
                throw new ArgumentException("Paciente não encontrado", nameof(paciente));
            }
            else
            {
                pagamento.Valor = paciente.PlanoPaciente.ValorMensal - pagamento.Desconto;
                pagamento.Data = DateTime.Now;
                pacienteRepositorio.RealizarPagamento(paciente.IdPaciente, pagamento);
                return "Pagamento realizado com sucesso!";
            }
        }

        public string ListarPagamentos(string cpf)
        {
            var bulder = new StringBuilder();
            var pacientePagamento = pacienteRepositorio.BuscaPaciPorCpf(cpf);

            if (pacientePagamento == null)
            {
                throw new ArgumentException("Paciente não encontrado", nameof(pacientePagamento));
            }
            else
            {
                bulder.AppendLine($"--Paciente--: \nCPF: {pacientePagamento.CPF}, \nNome: {pacientePagamento.Nome}, \nDataNasc: {pacientePagamento.DataNascimento.ToString("dd/MM/yyyy")}, \nSexo: {pacientePagamento.Sexo}");
                bulder.AppendLine("Pagamentos:");
                foreach (var pagamento in pacientePagamento.Pagamentos)
                {
                    bulder.AppendLine($"Data:{pagamento.Data.ToString("dd/MM/yyyy HH:mm")}  Valor: R${pagamento.Valor}  Desconto: R${pagamento.Desconto} Forma: {pagamento.Tipo}");
                }
                return bulder.ToString();
            }
        }

        public string addPlano(string cpf, PlanoDeSaude plano)
        {
            var paciente = pacienteRepositorio.BuscaPaciPorCpf(cpf);

            if (paciente == null)
            {
                throw new ArgumentException("Paciente não encontrado", nameof(paciente));
            }
            else
            {
                pacienteRepositorio.addPlano(paciente.IdPaciente, plano);
                return "Paciente atualizado com sucesso!";
            }
        }
    }
}