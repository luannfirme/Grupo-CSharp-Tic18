using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<(int, string, int, double)> estoque = new List<(int, string, int, double)>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Cadastro de Produtos");
            Console.WriteLine("2. Consulta de Produtos");
            Console.WriteLine("3. Atualização de Estoque");
            Console.WriteLine("4. Relatórios");
            Console.WriteLine("5. Sair");

            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarProduto();
                    break;

                case "2":
                    ConsultarProduto();
                    break;

                case "3":
                    AtualizarEstoque();
                    break;

                case "4":
                    GerarRelatorios();
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void CadastrarProduto()
    {
        try
        {
            Console.Write("Digite o código do produto: ");
            int codigo = int.Parse(Console.ReadLine());

            Console.Write("Digite o nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a quantidade em estoque: ");
            int quantidade = int.Parse(Console.ReadLine());

            Console.Write("Digite o preço unitário: ");
            double preco = double.Parse(Console.ReadLine());

            estoque.Add((codigo, nome, quantidade, preco));
            Console.WriteLine("Produto cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar produto: {ex.Message}");
        }
    }

    static void ConsultarProduto()
    {
        try
        {
            Console.Write("Digite o código do produto: ");
            int codigo = int.Parse(Console.ReadLine());

            var produto = estoque.FirstOrDefault(p => p.Item1 == codigo);

            if (produto == default)
            {
                throw new ProdutoNaoEncontradoException();
            }

            Console.WriteLine($"Produto encontrado: {produto}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao consultar produto: {ex.Message}");
        }
    }

    static void AtualizarEstoque()
    {
        try
        {
            Console.Write("Digite o código do produto: ");
            int codigo = int.Parse(Console.ReadLine());

            var produto = estoque.FirstOrDefault(p => p.Item1 == codigo);

            if (produto == default)
            {
                throw new ProdutoNaoEncontradoException();
            }

            Console.Write("Digite a quantidade a ser adicionada (positiva) ou removida (negativa): ");
            int quantidade = int.Parse(Console.ReadLine());

            if (produto.Item3 + quantidade < 0)
            {
                throw new EstoqueInsuficienteException();
            }

            estoque.Remove(produto);
            estoque.Add((produto.Item1, produto.Item2, produto.Item3 + quantidade, produto.Item4));
            Console.WriteLine("Estoque atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar estoque: {ex.Message}");
        }
    }

    static void GerarRelatorios()
    {
        try
        {
            Console.Write("Digite o limite de quantidade para o relatório 1: ");
            int limiteQuantidade = int.Parse(Console.ReadLine());

            var relatorio1 = estoque.Where(p => p.Item3 < limiteQuantidade);
            Console.WriteLine("Relatório 1: Produtos com quantidade em estoque abaixo do limite");
            ImprimirRelatorio(relatorio1);

            Console.Write("Digite o valor mínimo para o relatório 2: ");
            double minimo = double.Parse(Console.ReadLine());

            Console.Write("Digite o valor máximo para o relatório 2: ");
            double maximo = double.Parse(Console.ReadLine());

            var relatorio2 = estoque.Where(p => p.Item4 >= minimo && p.Item4 <= maximo);
            Console.WriteLine("Relatório 2: Produtos com valor entre mínimo e máximo");
            ImprimirRelatorio(relatorio2);

            var relatorio3 = from produto in estoque
                             select new
                             {
                                 Codigo = produto.Item1,
                                 Nome = produto.Item2,
                                 ValorTotal = produto.Item3 * produto.Item4
                             };

            Console.WriteLine("Relatório 3: Valor total do estoque e valor total de cada produto");
            foreach (var item in relatorio3)
            {
                Console.WriteLine($"Produto: {item.Codigo} - {item.Nome}, Valor Total: {item.ValorTotal}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao gerar relatórios: {ex.Message}");
        }
    }

    static void ImprimirRelatorio(IEnumerable<(int, string, int, double)> relatorio)
    {
        foreach (var produto in relatorio)
        {
            Console.WriteLine($"Produto: {produto.Item1} - {produto.Item2}, Quantidade: {produto.Item3}, Preço: {produto.Item4}");
        }
    }
}

class ProdutoNaoEncontradoException : Exception
{
    public ProdutoNaoEncontradoException() : base("Produto não encontrado.")
    {
    }
}

class EstoqueInsuficienteException : Exception
{
    public EstoqueInsuficienteException() : base("Estoque insuficiente para a saída desejada.")
    {
    }
}
