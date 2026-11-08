using MiniBitcoin.Models;

namespace MiniBitcoin.Services;

public class ExtratoService
{
    private readonly List<Transacao> _transacoes = new();

    public IReadOnlyList<Transacao> Transacoes => _transacoes.AsReadOnly();

    public void Registrar(Transacao transacao)
    {
        ArgumentNullException.ThrowIfNull(transacao);
        _transacoes.Add(transacao);
    }

    public decimal CalcularPatrimonio(Carteira carteira, Cotacao cotacao)
    {
        ArgumentNullException.ThrowIfNull(carteira);
        ArgumentNullException.ThrowIfNull(cotacao);

        return carteira.SaldoBrl + carteira.SaldoBtc * cotacao.Preco;
    }
}