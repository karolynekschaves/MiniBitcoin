namespace MiniBitcoin.Models;

public class Cotacao
{
    public decimal Preco { get; private set; }

    public Cotacao(decimal preco)
    {
        Validar(preco);
        Preco = preco;
    }

    public void Atualizar(decimal novoPreco)
    {
        Validar(novoPreco);
        Preco = novoPreco;
    }

    private static void Validar(decimal preco)
    {
        if (preco <= 0)
            throw new ArgumentException("A cotação deve ser maior que zero.", nameof(preco));
    }
}