using MiniBitcoin.Models;
using MiniBitcoin.Services;
using Xunit;

namespace MiniBitcoin.Tests.Services;

public class CorretoraServiceTests
{
    private static (Carteira carteira, CorretoraService corretora) Criar(decimal brl, decimal btc, decimal preco = 100000m)
    {
        var carteira = new Carteira(brl, btc);
        var corretora = new CorretoraService(carteira, new Cotacao(preco));
        return (carteira, corretora);
    }

    [Fact]
    public void Comprar_ComSaldoSuficiente_DeveAtualizarSaldosERetornarTransacao()
    {
        var (carteira, corretora) = Criar(1000m, 0m);

        var transacao = corretora.Comprar(500m);

        Assert.Equal(500m, carteira.SaldoBrl);
        Assert.Equal(0.005m, carteira.SaldoBtc);
        Assert.Equal(TipoTransacao.Compra, transacao.Tipo);
        Assert.Equal(0.005m, transacao.QuantidadeBtc);
        Assert.Equal(500m, transacao.ValorBrl);
    }

    [Fact]
    public void Comprar_ComSaldoInsuficiente_DeveLancarExcecaoESemAlterarSaldos()
    {
        var (carteira, corretora) = Criar(100m, 0m);

        Assert.Throws<InvalidOperationException>(() => corretora.Comprar(500m));
        Assert.Equal(100m, carteira.SaldoBrl);
        Assert.Equal(0m, carteira.SaldoBtc);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Comprar_ComValorInvalido_DeveLancarExcecao(double valor)
    {
        var (_, corretora) = Criar(1000m, 0m);

        Assert.Throws<ArgumentException>(() => corretora.Comprar((decimal)valor));
    }

    [Fact]
    public void Vender_ComSaldoSuficiente_DeveAtualizarSaldosERetornarTransacao()
    {
        var (carteira, corretora) = Criar(0m, 1m);

        var transacao = corretora.Vender(0.5m);

        Assert.Equal(50000m, carteira.SaldoBrl);
        Assert.Equal(0.5m, carteira.SaldoBtc);
        Assert.Equal(TipoTransacao.Venda, transacao.Tipo);
        Assert.Equal(0.5m, transacao.QuantidadeBtc);
        Assert.Equal(50000m, transacao.ValorBrl);
    }

    [Fact]
    public void Vender_ComSaldoInsuficiente_DeveLancarExcecaoESemAlterarSaldos()
    {
        var (carteira, corretora) = Criar(0m, 0.1m);

        Assert.Throws<InvalidOperationException>(() => corretora.Vender(1m));
        Assert.Equal(0m, carteira.SaldoBrl);
        Assert.Equal(0.1m, carteira.SaldoBtc);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Vender_ComQuantidadeInvalida_DeveLancarExcecao(double quantidade)
    {
        var (_, corretora) = Criar(0m, 1m);

        Assert.Throws<ArgumentException>(() => corretora.Vender((decimal)quantidade));
    }
}