using MiniBitcoin.Models;
using Xunit;

namespace MiniBitcoin.Tests.Models;

public class CarteiraTests
{
    [Fact]
    public void Construtor_ComSaldosValidos_DeveDefinirSaldosIniciais()
    {
        var carteira = new Carteira(1000m, 0.5m);

        Assert.Equal(1000m, carteira.SaldoBrl);
        Assert.Equal(0.5m, carteira.SaldoBtc);
    }

    [Fact]
    public void Construtor_ComSaldoBrlNegativo_DeveLancarExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Carteira(-1m, 0m));
    }

    [Fact]
    public void Construtor_ComSaldoBtcNegativo_DeveLancarExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Carteira(0m, -0.1m));
    }

    [Fact]
    public void DebitarBrl_ComSaldoSuficiente_DeveReduzirSaldo()
    {
        var carteira = new Carteira(1000m, 0m);

        carteira.DebitarBrl(300m);

        Assert.Equal(700m, carteira.SaldoBrl);
    }

    [Fact]
    public void DebitarBrl_ComSaldoInsuficiente_DeveLancarExcecaoESemAlterarSaldo()
    {
        var carteira = new Carteira(100m, 0m);

        Assert.Throws<InvalidOperationException>(() => carteira.DebitarBrl(150m));
        Assert.Equal(100m, carteira.SaldoBrl);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void DebitarBrl_ComValorInvalido_DeveLancarExcecao(double valor)
    {
        var carteira = new Carteira(1000m, 0m);

        Assert.Throws<ArgumentException>(() => carteira.DebitarBrl((decimal)valor));
    }

    [Fact]
    public void DebitarBtc_ComSaldoSuficiente_DeveReduzirSaldo()
    {
        var carteira = new Carteira(0m, 1m);

        carteira.DebitarBtc(0.4m);

        Assert.Equal(0.6m, carteira.SaldoBtc);
    }

    [Fact]
    public void DebitarBtc_ComSaldoInsuficiente_DeveLancarExcecaoESemAlterarSaldo()
    {
        var carteira = new Carteira(0m, 0.2m);

        Assert.Throws<InvalidOperationException>(() => carteira.DebitarBtc(0.5m));
        Assert.Equal(0.2m, carteira.SaldoBtc);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void DebitarBtc_ComValorInvalido_DeveLancarExcecao(double valor)
    {
        var carteira = new Carteira(0m, 1m);

        Assert.Throws<ArgumentException>(() => carteira.DebitarBtc((decimal)valor));
    }
    [Fact]
    public void CreditarBrl_ComValorValido_DeveAumentarSaldo()
    {
        var carteira = new Carteira(100m, 0m);

        carteira.CreditarBrl(50m);

        Assert.Equal(150m, carteira.SaldoBrl);
    }

    [Fact]
    public void CreditarBtc_ComValorValido_DeveAumentarSaldo()
    {
        var carteira = new Carteira(0m, 0.1m);

        carteira.CreditarBtc(0.2m);

        Assert.Equal(0.3m, carteira.SaldoBtc);
    }
}
    