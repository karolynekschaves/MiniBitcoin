using MiniBitcoin.Models;
using MiniBitcoin.Services;
using Xunit;

namespace MiniBitcoin.Tests.Services;

public class ExtratoServiceTests
{
    [Fact]
    public void NovoExtrato_DeveEstarVazio()
    {
        var extrato = new ExtratoService();

        Assert.Empty(extrato.Transacoes);
    }

    [Fact]
    public void Registrar_DeveAdicionarTransacoesNaOrdem()
    {
        var extrato = new ExtratoService();
        var compra = new Transacao(TipoTransacao.Compra, 0.01m, 1000m, 100000m, DateTime.Now);
        var venda = new Transacao(TipoTransacao.Venda, 0.005m, 500m, 100000m, DateTime.Now);

        extrato.Registrar(compra);
        extrato.Registrar(venda);

        Assert.Equal(2, extrato.Transacoes.Count);
        Assert.Equal(compra, extrato.Transacoes[0]);
        Assert.Equal(venda, extrato.Transacoes[1]);
    }

    [Fact]
    public void Registrar_ComTransacaoNula_DeveLancarExcecao()
    {
        var extrato = new ExtratoService();

        Assert.Throws<ArgumentNullException>(() => extrato.Registrar(null!));
    }

    [Fact]
    public void CalcularPatrimonio_DeveSomarBrlMaisBtcConvertido()
    {
        var extrato = new ExtratoService();
        var carteira = new Carteira(1000m, 0.5m);
        var cotacao = new Cotacao(100000m);

        var patrimonio = extrato.CalcularPatrimonio(carteira, cotacao);

        Assert.Equal(51000m, patrimonio);
    }

    [Fact]
    public void CalcularPatrimonio_ComCotacaoAtualizada_DeveRefletirNovoPreco()
    {
        var extrato = new ExtratoService();
        var carteira = new Carteira(0m, 1m);
        var cotacao = new Cotacao(100000m);

        cotacao.Atualizar(200000m);

        Assert.Equal(200000m, extrato.CalcularPatrimonio(carteira, cotacao));
    }
}