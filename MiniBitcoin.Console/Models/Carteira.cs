namespace MiniBitcoin.Models;

public class Carteira
{
    public decimal SaldoBrl { get; private set; }
    public decimal SaldoBtc { get; private set; }

    public Carteira(decimal saldoBrl, decimal saldoBtc)
    {
        if (saldoBrl < 0)
            throw new ArgumentException("O saldo inicial em BRL não pode ser negativo.", nameof(saldoBrl));

        if (saldoBtc < 0)
            throw new ArgumentException("O saldo inicial em BTC não pode ser negativo.", nameof(saldoBtc));

        SaldoBrl = saldoBrl;
        SaldoBtc = saldoBtc;
    }

    public void DebitarBrl(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do débito deve ser maior que zero.", nameof(valor));

        if (valor > SaldoBrl)
            throw new InvalidOperationException("Saldo em BRL insuficiente.");

        SaldoBrl -= valor;
    }

    public void DebitarBtc(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do débito deve ser maior que zero.", nameof(valor));

        if (valor > SaldoBtc)
            throw new InvalidOperationException("Saldo em BTC insuficiente.");

        SaldoBtc -= valor;
    }

    public void CreditarBrl(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do crédito deve ser maior que zero.", nameof(valor));

        SaldoBrl += valor;
    }

    public void CreditarBtc(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do crédito deve ser maior que zero.", nameof(valor));

        SaldoBtc += valor;
    }
}