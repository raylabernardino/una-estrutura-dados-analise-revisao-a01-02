using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Produto> produtos = new List<Produto>();

        while (true)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Sistema de Gerenciamento de Estoque");
            Console.WriteLine("---------------------------------------------");

            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1 - Adicionar um novo produto");
            Console.WriteLine("2 - Atualizar um produto");
            Console.WriteLine("3 - Localizar um produto");
            Console.WriteLine("4 - Calcular valor total do estoque");
            Console.WriteLine("5 - Emitir relatório de produtos a vencer");
            Console.WriteLine("6 - Remover um produto");
            Console.WriteLine("0 - Encerrar aplicação");

            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "0":
                    Console.WriteLine("---> Aplicação encerrada!");
                    return;
                case "1":
                    ProdutoService.Adicionar(produtos);
                    break;
                case "2":
                    ProdutoService.Atualizar(produtos);
                    break;
                case "3":
                    ProdutoService.Buscar(produtos);
                    break;
                case "4":
                    ProdutoService.CalcularTotalEstoque(produtos);
                    break;
                case "5":
                    ProdutoService.RelatorioValidadeProdutos(produtos);
                    break;
                case "6":
                    ProdutoService.Remover(produtos);
                    break;
                default:
                    Console.WriteLine("---> Opção inválida!");
                    break;
            }
        }
    }
}
