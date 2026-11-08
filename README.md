# MiniBitcoin

Aplicação de console em C# (.NET 10) que simula uma carteira de Bitcoin. O projeto foi desenvolvido com TDD (testes xUnit escritos antes da implementação) e com o padrão Conventional Commits.

## Funcionalidades

- **Carteira:** saldo em BRL e em BTC, com validações para evitar saldos negativos.
- **Cotação:** consulta e atualização manual do preço do Bitcoin (simulado).
- **Compra e venda:** ordens de compra e venda com validação de saldo e atualização dos saldos.
- **Extrato:** histórico de operações realizadas e patrimônio total consolidado em BRL.

## Arquitetura

```
MiniBitcoin/
├── MiniBitcoin.Console/
│   ├── Models/
│   │   ├── Carteira.cs         Saldos em BRL e BTC, débitos e créditos validados
│   │   ├── Cotacao.cs          Preço atual do Bitcoin
│   │   ├── Transacao.cs        Registro de uma operação (record)
│   │   └── TipoTransacao.cs    Enum: Compra ou Venda
│   ├── Services/
│   │   ├── CorretoraService.cs Lógica de compra e venda
│   │   └── ExtratoService.cs   Histórico de transações e patrimônio total
│   └── Program.cs              Menu interativo no console
└── MiniBitcoin.Tests/
    ├── Models/                 Testes da Carteira
    └── Services/               Testes da Corretora e do Extrato
```

**Fluxo geral:** o `Program.cs` cria a `Carteira`, a `Cotacao` e os serviços. A `CorretoraService` executa a compra ou venda usando a cotação atual e devolve uma `Transacao`, que o `ExtratoService` registra no histórico.

## Como executar

Pré-requisito: [.NET SDK 10](https://dotnet.microsoft.com/download).

```bash
dotnet run --project MiniBitcoin.Console/MiniBitcoin.Console.csproj
```

A carteira começa com R$ 10.000,00, 0 BTC e cotação de R$ 350.000,00.

## Como rodar os testes

```bash
dotnet test
```

## Padrão de commits

O projeto usa Conventional Commits: `feat`, `fix`, `test`, `refactor`, `docs` e `chore`.

Exemplo: `feat(carteira): implement wallet entity and initial balance rules`