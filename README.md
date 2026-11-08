# 🪙 MiniBitcoin

Aplicação de console em C# (.NET 10) que simula uma carteira de Bitcoin. O projeto foi desenvolvido com **TDD** (testes xUnit escritos antes da implementação) e com o padrão **Conventional Commits**.

**Responsável:** Karolyne S

## ✨ Funcionalidades

- 👛 **Carteira:** saldo em BRL e em BTC, com validações para evitar saldos negativos.
- 📈 **Cotação:** consulta e atualização manual do preço do Bitcoin (simulado).
- 💱 **Compra e venda:** ordens validadas pelo saldo disponível, com atualização automática dos saldos.
- 🧾 **Extrato:** histórico das operações realizadas e patrimônio total consolidado em BRL.

## 🛠️ Tecnologias

- C# com .NET 10
- xUnit (testes unitários)
- Git e Conventional Commits

## 🏗️ Arquitetura

```
MiniBitcoin/
├── MiniBitcoin.Console/
│   ├── Models/
│   │   ├── Carteira.cs          Saldos em BRL e BTC, débitos e créditos validados
│   │   ├── Cotacao.cs           Preço atual do Bitcoin
│   │   ├── Transacao.cs         Registro de uma operação (record)
│   │   └── TipoTransacao.cs     Enum: Compra ou Venda
│   ├── Services/
│   │   ├── CorretoraService.cs  Lógica de compra e venda
│   │   └── ExtratoService.cs    Histórico de transações e patrimônio total
│   └── Program.cs               Menu interativo no console
└── MiniBitcoin.Tests/
    ├── Models/                  Testes da Carteira
    └── Services/                Testes da Corretora e do Extrato
```

**Fluxo geral:** o `Program.cs` cria a `Carteira`, a `Cotacao` e os serviços. A `CorretoraService` executa a compra ou a venda usando a cotação atual e devolve uma `Transacao`, que o `ExtratoService` registra no histórico.

## 📋 Regras de negócio

- A carteira não pode ser criada com saldo negativo.
- Débitos e créditos exigem valor maior que zero.
- Não é possível debitar mais do que o saldo disponível.
- A cotação do Bitcoin deve ser maior que zero.
- Compra: debita BRL e credita BTC (`valor em BRL ÷ cotação`).
- Venda: debita BTC e credita BRL (`quantidade em BTC × cotação`).
- Patrimônio total: `saldo BRL + saldo BTC × cotação atual`.

## 🚀 Como executar

Pré-requisito: [.NET SDK 10](https://dotnet.microsoft.com/download).

```bash
dotnet run --project MiniBitcoin.Console/MiniBitcoin.Console.csproj
```

A carteira começa com R$ 10.000,00, 0 BTC e cotação de R$ 350.000,00.

## 🧪 Como rodar os testes

```bash
dotnet test
```

## 📝 Padrão de commits

O projeto usa Conventional Commits: `feat`, `fix`, `test`, `refactor`, `docs` e `chore`.

Exemplo: `feat(carteira): implement wallet entity and initial balance rules`

## 👩‍💻 Responsável

**Karolyne S**
