using MiniBitcoin.Models;

namespace MiniBitcoin.Services;

public class CorretoraService
{
    private readonly Carteira _carteira;
    private readonly Cotacao _cotacao;

    public CorretoraService(Carteira carteira, Cotacao cotacao)
    {
        _carteira = carteira ?? throw new ArgumentNullException(nameof(carteira));
        _cotacao = cotacao ?? throw new ArgumentNullException(nameof(cotacao));
    }

    public Transacao Comprar(decimal valorBrl)
    {
        if (valorBrl <= 0)
            throw new ArgumentException("O valor da compra deve ser maior que zero.", nameof(valorBrl));

        var quantidadeBtc = valorBrl / _cotacao.Preco;

        _carteira.DebitarBrl(valorBrl);   // lança exceção se não houver saldo
        _carteira.CreditarBtc(quantidadeBtc);

        return new Transacao(TipoTransacao.Compra, quantidadeBtc, valorBrl, _cotacao.Preco, DateTime.Now);
    }

    public Transacao Vender(decimal quantidadeBtc)
    {
        if (quantidadeBtc <= 0)
            throw new ArgumentException("A quantidade a vender deve ser maior que zero.", nameof(quantidadeBtc));

        var valorBrl = quantidadeBtc * _cotacao.Preco;

        _carteira.DebitarBtc(quantidadeBtc);   // lança exceção se não houver saldo
        _carteira.CreditarBrl(valorBrl);

        return new Transacao(TipoTransacao.Venda, quantidadeBtc, valorBrl, _cotacao.Preco, DateTime.Now);
    }
}