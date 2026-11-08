using System.Globalization;
using MiniBitcoin.Models;
using MiniBitcoin.Services;

var cultura = new CultureInfo("pt-BR");

var carteira = new Carteira(10000m, 0m);
var cotacao = new Cotacao(350000m);
var corretora = new CorretoraService(carteira, cotacao);
var extrato = new ExtratoService();

bool executando = true;

while (executando)
{
    Console.WriteLine();
    Console.WriteLine("===== MiniBitcoin =====");
    Console.WriteLine("1 - Consultar cotação");
    Console.WriteLine("2 - Atualizar cotação");
    Console.WriteLine("3 - Ver saldos");
    Console.WriteLine("4 - Comprar Bitcoin");
    Console.WriteLine("5 - Vender Bitcoin");
    Console.WriteLine("6 - Exibir extrato");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");

    var opcao = Console.ReadLine();

    try
    {
        switch (opcao)
        {
            case "1":
                Console.WriteLine($"Cotação atual: {cotacao.Preco.ToString("C2", cultura)} por BTC");
                break;

            case "2":
                var novoPreco = LerDecimal("Nova cotação (R$): ");
                cotacao.Atualizar(novoPreco);
                Console.WriteLine("Cotação atualizada.");
                break;

            case "3":
                Console.WriteLine($"Saldo BRL: {carteira.SaldoBrl.ToString("C2", cultura)}");
                Console.WriteLine($"Saldo BTC: {carteira.SaldoBtc.ToString("F8", cultura)}");
                break;

            case "4":
                var valorCompra = LerDecimal("Valor a investir (R$): ");
                var compra = corretora.Comprar(valorCompra);
                extrato.Registrar(compra);
                Console.WriteLine($"Compra realizada: {compra.QuantidadeBtc.ToString("F8", cultura)} BTC.");
                break;

            case "5":
                var qtdVenda = LerDecimal("Quantidade de BTC a vender: ");
                var venda = corretora.Vender(qtdVenda);
                extrato.Registrar(venda);
                Console.WriteLine($"Venda realizada: {venda.ValorBrl.ToString("C2", cultura)} recebidos.");
                break;

            case "6":
                ExibirExtrato();
                break;

            case "0":
                executando = false;
                Console.WriteLine("Até logo!");
                break;

            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Valor inválido: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Operação não realizada: {ex.Message}");
    }
}

decimal LerDecimal(string mensagem)
{
    Console.Write(mensagem);
    var entrada = Console.ReadLine();

    if (!decimal.TryParse(entrada, NumberStyles.Number, cultura, out var valor))
        throw new ArgumentException("Digite um número válido (ex.: 1500,50).");

    return valor;
}

void ExibirExtrato()
{
    Console.WriteLine();
    Console.WriteLine("--- Extrato ---");

    if (extrato.Transacoes.Count == 0)
    {
        Console.WriteLine("Nenhuma transação realizada.");
    }

    foreach (var t in extrato.Transacoes)
    {
        Console.WriteLine(
            $"{t.Data:dd/MM/yyyy HH:mm} | {t.Tipo,-6} | " +
            $"{t.QuantidadeBtc.ToString("F8", cultura)} BTC | " +
            $"{t.ValorBrl.ToString("C2", cultura)} | " +
            $"cotação {t.PrecoCotacao.ToString("C2", cultura)}");
    }

    Console.WriteLine();
    Console.WriteLine($"Patrimônio total: {extrato.CalcularPatrimonio(carteira, cotacao).ToString("C2", cultura)}");
}