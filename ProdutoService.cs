using System;
using System.Globalization;

class ProdutoService
{

    public static void Adicionar(List<Produto> produtos)
    {
        Produto produto = new Produto();
        produto = ReceberDadosProduto(produto);
        produtos.Add(produto);
        Console.WriteLine("PRODUTO ADICIONADO COM SUCESSO!");
    }

    public static void Remover(List<Produto> produtos)
    {
        int codigo;
        Console.WriteLine(" - Informe o código de barras do produto que deseja remover:");
        int.TryParse(Console.ReadLine(), out codigo);
        Produto produtoParaRemover = produtos.FirstOrDefault(p => p.Codigo == codigo);

        if (produtoParaRemover != null)
        {
            produtos.Remove(produtoParaRemover);
        }
        else
        {
            Console.WriteLine("PRODUTO NÃO ENCONTRADO COM ESTE CÓDIGO.");
        }
        Console.WriteLine("PRODUTO REMOVIDO COM SUCESSO!");
    }

    public static void Atualizar(List<Produto> produtos)
    {
        int codigo;
        Console.WriteLine(" - Informe o código de barras do produto que deseja atualizar:");
        int.TryParse(Console.ReadLine(), out codigo);

        Produto produtoParaAtualizar = produtos.FirstOrDefault(p => p.Codigo == codigo);

        if (produtoParaAtualizar == null)
        {
            Console.WriteLine("PRODUTO NÃO ENCONTRADO COM ESTE CÓDIGO.");
        }
        else
        {
            Console.WriteLine(" - Atualize as informações do produto:");
            produtoParaAtualizar = ReceberDadosProduto(produtoParaAtualizar);

            Console.WriteLine("PRODUTO ATUALIZADO COM SUCESSO!");
        }
    }

    public static void Buscar(List<Produto> produtos)
    {
        Console.WriteLine("Escolha uma opção de busca:");
        Console.WriteLine("1 - Buscar por nome");
        Console.WriteLine("2 - Buscar por código de barras");
        Console.WriteLine("3 - Buscar por data de validade");
        string opcaoBusca = Console.ReadLine();

        switch (opcaoBusca)
        {
            case "1":
                Console.WriteLine(" - Informe o nome do produto a ser pesquisado:");
                string nomeBusca = Console.ReadLine();
                List<Produto> produtosEncontradosNome = produtos.Where(p => p.Nome.Contains(nomeBusca)).ToList();
                ImprimirListaDeProdutos(produtosEncontradosNome);
                break;
            case "2":
                Console.WriteLine(" - Informe o código de barras do produto a ser pesquisado:");
                int codigoBusca;
                int.TryParse(Console.ReadLine(), out codigoBusca);
                Produto produtoEncontradoCodigo = produtos.FirstOrDefault(p => p.Codigo == codigoBusca);
                if (produtoEncontradoCodigo != null)
                {
                    Console.WriteLine("Produto encontrado:");
                    ImprimirProduto(produtoEncontradoCodigo);
                }
                else
                {
                    Console.WriteLine("PRODUTO NÃO ENCONTRADO COM ESTE CÓDIGO.");
                }
                break;
            case "3":
                Console.WriteLine("Informe a data de validade (no formato dd/mm/aaaa) para pesquisar produtos:");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataBusca))
                {
                    List<Produto> produtosEncontradosData = produtos.Where(p => p.DataValidade <= dataBusca).ToList();
                    ImprimirListaDeProdutos(produtosEncontradosData);
                }
                else
                {
                    Console.WriteLine("Data de pesquisa inválida. Use o formato dd/mm/aaaa.");
                }
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    public static void CalcularTotalEstoque(List<Produto> produtos)
    {
        decimal valorTotal = produtos.Sum(p => p.Quantidade * p.PrecoUnitario);
        Console.WriteLine($"O valor total do estoque é: R${valorTotal:F2}");
    }

    public static void RelatorioValidadeProdutos(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("NÃO EXISTE PRODUTOS NO ESTOQUE.");
            return;
        }

        List<Produto> produtosOrdenados = produtos.OrderBy(p => p.DataValidade).ToList();

        Console.WriteLine("Relatório de Produtos ordenados pela data de Validade:");
        ImprimirListaDeProdutos(produtosOrdenados);

    }

    public static Produto ReceberDadosProduto(Produto produto)
    {
        string nome;
        int codigo;
        int quantidade;
        decimal precoUnitario;
        DateTime dataValidade;

        do
        {
            Console.WriteLine(" - Informe o nome do produto:");
            nome = Console.ReadLine();

            if (string.IsNullOrEmpty(nome))
            {
                Console.WriteLine("Erro: O nome do produto não pode ser vazio. Por favor, insira um nome válido.");
            }
        } while (string.IsNullOrEmpty(nome));

        do
        {
            Console.WriteLine(" - Informe o código de barras do produto:");
            if (!int.TryParse(Console.ReadLine(), out codigo))
            {
                Console.WriteLine("Erro: O código deve ser um número inteiro.");
            }
        } while (codigo <= 0);

        do
        {
            Console.WriteLine(" - Informe a quantidade disponível:");
            if (!int.TryParse(Console.ReadLine(), out quantidade))
            {
                Console.WriteLine("Erro: A quantidade disponível deve ser um número inteiro.");
            }
        } while (quantidade <= 0);

        do
        {
            Console.WriteLine(" - Informe o preço unitário:");
            if (!decimal.TryParse(Console.ReadLine(), out precoUnitario))
            {
                Console.WriteLine("Erro: O preço unitário deve ser um número decimal.");
            }
        } while (precoUnitario <= 0);

        do
        {
            Console.WriteLine(" - Informe a data de validade (no formato dd/mm/aaaa):");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataValidade))
            {
                Console.WriteLine("Erro: Data de validade inválida. Use o formato dd/mm/aaaa.");
            }
        } while (dataValidade <= DateTime.Now);

        produto.Nome = nome;
        produto.Codigo = codigo;
        produto.Quantidade = quantidade;
        produto.PrecoUnitario = precoUnitario;
        produto.DataValidade = dataValidade;

        return produto;
    }

    public static void ImprimirListaDeProdutos(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("NÃO FOI ENCONTRADO PRODUTOS COM AS INFORMAÇÕES INSERIDAS.");
            return;
        }

        Console.WriteLine("Produtos encontrados:");
        foreach (var produto in produtos)
        {
            ImprimirProduto(produto);
        }
    }

    public static void ImprimirProduto(Produto produto)
    {
        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("-- Nome: " + produto.Nome);
        Console.WriteLine("-- Código de Barras: " + produto.Codigo);
        Console.WriteLine("-- Quantidade Disponível: " + produto.Quantidade);
        Console.WriteLine("-- Preço Unitário: R$" + produto.PrecoUnitario.ToString("F2"));
        Console.WriteLine("-- Data de Validade: " + produto.DataValidade.ToString("dd/MM/yyyy"));
        Console.WriteLine("--------------------------------------------------------------------");
    }
}