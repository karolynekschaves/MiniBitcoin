namespace MiniBitcoin.Models;

public record Transacao(
    TipoTransacao Tipo,
    decimal QuantidadeBtc,
    decimal ValorBrl,
    decimal PrecoCotacao,
    DateTime Data);